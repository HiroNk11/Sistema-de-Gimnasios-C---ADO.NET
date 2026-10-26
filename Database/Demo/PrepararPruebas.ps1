[CmdletBinding()]
param(
 [ValidatePattern('^DatabaseGym_Pruebas(?:_[A-Za-z0-9]+)?$')][string]$Base='DatabaseGym_Pruebas',
 [string]$Servidor='(localdb)\MSSQLLocalDB',
 [string]$Proyecto=(Split-Path (Split-Path $PSScriptRoot -Parent) -Parent)
)
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Data
$original=$env:GYM_CONNECTION_STRING
function Conexion([string]$catalogo){
 $b=New-Object System.Data.SqlClient.SqlConnectionStringBuilder
 $b['Data Source']=$Servidor;$b['Initial Catalog']=$catalogo;$b['Integrated Security']=$true
 $b['Encrypt']=-not ($Servidor -match '^\(localdb\)\\');$b['Connect Timeout']=10
 return New-Object System.Data.SqlClient.SqlConnection($b.ConnectionString)
}
function Escalar($cn,[string]$sql){$cmd=$cn.CreateCommand();$cmd.CommandText=$sql;try{return $cmd.ExecuteScalar()}finally{$cmd.Dispose()}}
function Ejecutar($cn,[string]$sql){$cmd=$cn.CreateCommand();$cmd.CommandText=$sql;$cmd.CommandTimeout=180;try{[void]$cmd.ExecuteNonQuery()}finally{$cmd.Dispose()}}
try{
 $release=Join-Path $Proyecto 'CapaIU\bin\Release'
 if(-not(Test-Path -LiteralPath (Join-Path $release 'Sistema Body Sport Demo.exe'))){throw 'Falta compilar Release en el proyecto indicado.'}
 $master=Conexion 'master'
 try{$master.Open();$cmd=$master.CreateCommand();$cmd.CommandText='SELECT DB_ID(@b)';[void]$cmd.Parameters.AddWithValue('@b',$Base);$id=$cmd.ExecuteScalar();$cmd.Dispose();$existe=($null -ne $id -and $id -isnot [DBNull])}finally{$master.Dispose()}
 if(-not $existe){
  & (Join-Path $Proyecto 'Database\InstalarOActualizar.ps1') -Servidor $Servidor -Base $Base -Modo Instalar
  $cn=Conexion $Base
  try{$cn.Open();Ejecutar $cn "CREATE TABLE dbo.Demo_Entorno(Version varchar(20) NOT NULL);INSERT dbo.Demo_Entorno VALUES('20261026');"}finally{$cn.Dispose()}
 }
 $cn=Conexion $Base
 try{
  $cn.Open()
  if((Escalar $cn "SELECT COUNT(*) FROM sys.tables WHERE name='Demo_Entorno' AND schema_id=SCHEMA_ID('dbo')") -ne 1){throw 'La base existe pero no fue creada por este asistente. Use otro nombre con -Base DatabaseGym_Pruebas_02.'}
  if((Escalar $cn "SELECT COUNT(*) FROM dbo.Demo_Entorno WHERE Version='20261026'") -ne 1){throw 'Marcador de prueba incompatible.'}
  $tieneDemo=(Escalar $cn "SELECT COUNT(*) FROM dbo.Usuario WHERE LoginName=N'demo'") -gt 0
 }finally{$cn.Dispose()}
 if(-not $tieneDemo){
  Write-Output 'Elegí una contraseña propia para el usuario demo. No se usa la cuenta hironk.'
  & (Join-Path $Proyecto 'Database\ConfigurarAdministrador.ps1') -Servidor $Servidor -Base $Base -Usuario demo -Correo 'demo@example.invalid'
 }
 $cn=Conexion $Base
 try{
  $cn.Open();Ejecutar $cn ([IO.File]::ReadAllText((Join-Path $PSScriptRoot 'DatosDemo.sql')))
  $cmd=$cn.CreateCommand();$cmd.CommandText=[IO.File]::ReadAllText((Join-Path $PSScriptRoot 'VerificarDatosDemo.sql'))
  $da=New-Object System.Data.SqlClient.SqlDataAdapter($cmd);$ds=New-Object System.Data.DataSet
  try{[void]$da.Fill($ds);foreach($tabla in $ds.Tables){$tabla | Format-Table -AutoSize | Out-Host}}finally{$da.Dispose();$cmd.Dispose()}
  if(@($ds.Tables[2].Rows | Where-Object {[int]$_.Problemas -gt 0}).Count -gt 0){throw 'Se detectaron inconsistencias en la muestra. Revise el informe; no se corrigen datos automaticamente.'}
  $cmd=$cn.CreateCommand()
  $cmd.CommandText="SELECT d.Codigo AS NroDemo,s.Id_Socio AS IdReal,s.Nombre,s.Apellido,CASE WHEN d.Codigo<=80 THEN 'Vigente pagada' WHEN d.Codigo<=100 THEN 'Pago parcial' WHEN d.Codigo<=110 THEN 'Impaga' WHEN d.Codigo<=120 THEN 'Vencida' WHEN d.Codigo<=130 THEN 'Vigencia futura' WHEN d.Codigo<=140 THEN 'Cortesia sin cargo' ELSE 'Sin membresia' END AS EstadoInicial FROM dbo.Demo_Referencia d JOIN dbo.Socio s ON s.Id_Socio=d.Id WHERE d.Tipo='Socio' ORDER BY d.Codigo"
  $da=New-Object System.Data.SqlClient.SqlDataAdapter($cmd);$socios=New-Object System.Data.DataTable
  try{[void]$da.Fill($socios)}finally{$da.Dispose();$cmd.Dispose()}
  $cadena=$cn.ConnectionString
 }finally{$cn.Dispose()}
 $destino=Join-Path $Proyecto ('EntornosPrueba\'+$Base)
 New-Item -ItemType Directory -Force -Path $destino | Out-Null
 $socios | Select-Object NroDemo,IdReal,Nombre,Apellido,EstadoInicial | Export-Csv -LiteralPath (Join-Path $destino 'SociosDemo.csv') -NoTypeInformation -Delimiter ';' -Encoding UTF8
 Get-ChildItem -LiteralPath $release -File | Copy-Item -Destination $destino -Force
 $config=Join-Path $destino 'Sistema Body Sport Demo.exe.config'
 [xml]$xml=Get-Content -LiteralPath $config
 $entrada=$xml.configuration.connectionStrings.add | Where-Object {$_.name -eq 'cadena_conexion'} | Select-Object -First 1
 $entrada.connectionString=$cadena;$xml.Save($config)
 $lanzador=@'
$ErrorActionPreference='Stop'
$anterior=$env:GYM_CONNECTION_STRING
try {
 $env:GYM_CONNECTION_STRING=$null
 Start-Process -FilePath (Join-Path $PSScriptRoot 'Sistema Body Sport Demo.exe') -WorkingDirectory $PSScriptRoot
} finally {$env:GYM_CONNECTION_STRING=$anterior}
'@
 [IO.File]::WriteAllText((Join-Path $destino 'AbrirPruebas.ps1'),$lanzador,(New-Object Text.UTF8Encoding($true)))
 Write-Output ('LISTO. Base aislada: '+$Base)
 Write-Output 'Acceso: demo y la contraseña que elegiste. No se enviaron correos.'
 Write-Output ('Para abrirla: powershell.exe -NoProfile -File "'+(Join-Path $destino 'AbrirPruebas.ps1')+'"')
 Write-Output 'La base habitual y su configuración permanecen sin cambios. Volver a ejecutar conserva los ejercicios ya realizados.'
}finally{$env:GYM_CONNECTION_STRING=$original}
