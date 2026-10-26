[CmdletBinding()]

param(

 [Parameter(Mandatory=$true)][ValidateLength(1,50)][string]$Usuario,

 [Parameter(Mandatory=$true)][ValidateSet('Administracion','Recepcion')][string]$Perfil,

 [ValidatePattern('^[A-Za-z][A-Za-z0-9_]{0,100}$')][string]$Base='DatabaseGym_Mejorado',

 [string]$Servidor='(localdb)\MSSQLLocalDB',

 [string]$Proyecto=(Split-Path $PSScriptRoot -Parent)

)

$ErrorActionPreference='Stop'

if($Base -in @('master','model','msdb','tempdb','DatabaseGym')){throw 'Indique la copia mejorada o una base de pruebas.'}

Add-Type -AssemblyName System.Data

$b=New-Object System.Data.SqlClient.SqlConnectionStringBuilder

$b['Data Source']=$Servidor;$b['Initial Catalog']=$Base;$b['Integrated Security']=$true

$b['Encrypt']=-not ($Servidor -match '^\(localdb\)\\');$b['Connect Timeout']=10

$cn=New-Object System.Data.SqlClient.SqlConnection($b.ConnectionString)

try{

 if($Base -like 'DatabaseGym_Pruebas*'){

  $destino=Join-Path $Proyecto ('EntornosPrueba\'+$Base)

  $config=Join-Path $destino 'Sistema Body Sport Demo.exe.config'

  if(-not(Test-Path -LiteralPath $config)){throw 'Primero cree la copia con PrepararPruebas.ps1. No se cambiaron permisos.'}

  [xml]$xml=Get-Content -LiteralPath $config

  $entrada=$xml.configuration.connectionStrings.add | Where-Object {$_.name -eq 'cadena_conexion'} | Select-Object -First 1

  $actual=New-Object System.Data.SqlClient.SqlConnectionStringBuilder($entrada.connectionString)

  if($actual.InitialCatalog -ne $Base -or $actual.DataSource -ne $Servidor){throw 'La copia de prueba no apunta al servidor y base indicados. No se cambiaron permisos.'}

  $release=Join-Path $Proyecto 'CapaIU\bin\Release'

  if(-not(Test-Path -LiteralPath (Join-Path $release 'Sistema Body Sport Demo.exe'))){throw 'Falta compilar la aplicacion actualizada.'}

  Get-ChildItem -LiteralPath $release -File | Where-Object {$_.Name -ne 'Sistema Body Sport Demo.exe.config'} | Copy-Item -Destination $destino -Force

  Write-Output 'Copia de pruebas actualizada sin cambiar su conexion ni sus datos.'

 }

 $cn.Open()

 $cmd=$cn.CreateCommand();$cmd.CommandText='SELECT COUNT(*) FROM dbo.Usuario WHERE LoginName=@u';[void]$cmd.Parameters.AddWithValue('@u',$Usuario.Trim())

 try{if([int]$cmd.ExecuteScalar() -ne 1){throw 'No existe una cuenta unica con ese nombre. No se cambio el perfil.'}}finally{$cmd.Dispose()}

 $cmd=$cn.CreateCommand();$cmd.CommandText=[IO.File]::ReadAllText((Join-Path $Proyecto 'Database\004_perfiles.sql'));$cmd.CommandTimeout=120

 try{[void]$cmd.ExecuteNonQuery()}finally{$cmd.Dispose()}

 $familia=if($Perfil -eq 'Administracion'){'APP Administracion de cuentas'}else{'APP Recepcion'}

 $tx=$cn.BeginTransaction([System.Data.IsolationLevel]::Serializable)

 try{

  $cmd=$cn.CreateCommand();$cmd.Transaction=$tx

  $cmd.CommandText=@'

DECLARE @id int=(SELECT Id_Usuario FROM dbo.Usuario WITH(UPDLOCK,HOLDLOCK) WHERE LoginName=@u);

DECLARE @rol int=(SELECT Id_Permiso FROM dbo.Permiso WHERE Nombre=@familia AND Permiso IS NULL);

IF @id IS NULL OR @rol IS NULL THROW 51000,'No se encontro la cuenta o el perfil.',1;

DECLARE @antes nvarchar(max)=(SELECT Id_Permiso AS Id FROM dbo.Usuario_Permiso WHERE Id_Usuario=@id FOR XML PATH('Permiso'),ROOT('Permisos'));

INSERT dbo.App_CambioPerfil(Id_Usuario,PermisosAnterioresXml,PerfilNuevo) VALUES(@id,@antes,@familia);

DELETE dbo.Usuario_Permiso WHERE Id_Usuario=@id;

INSERT dbo.Usuario_Permiso(Id_Usuario,Id_Permiso) VALUES(@id,@rol);

'@

  [void]$cmd.Parameters.AddWithValue('@u',$Usuario.Trim());[void]$cmd.Parameters.AddWithValue('@familia',$familia)

  try{[void]$cmd.ExecuteNonQuery()}finally{$cmd.Dispose()}

  $tx.Commit()

 }catch{try{$tx.Rollback()}catch{};throw}finally{$tx.Dispose()}

 Write-Output ('Perfil asignado: '+$Usuario+' / '+$Perfil+' / '+$Base)

 Write-Output 'La clave no cambio. Se guardaron los permisos anteriores en App_CambioPerfil. Cerra todas las sesiones antes de volver a ingresar.'

}finally{$cn.Dispose()}

