[CmdletBinding()]
param(
 [string]$Servidor='(localdb)\MSSQLLocalDB',
 [ValidatePattern('^[A-Za-z][A-Za-z0-9_]{0,100}$')][string]$Base='DatabaseGym_Mejorado',
 [Parameter(Mandatory=$true)][ValidateLength(1,50)][string]$Usuario,
 [Parameter(Mandatory=$true)][ValidateLength(3,254)][string]$Correo,
 [switch]$ConfiarCertificadoDePrueba
)
$ErrorActionPreference='Stop'
if($Base -in @('master','model','msdb','tempdb','DatabaseGym')){throw 'Seleccione una copia de la base, no DatabaseGym original.'}
$null=New-Object System.Net.Mail.MailAddress($Correo)
$release=Join-Path $PSScriptRoot '..\CapaIU\bin\Release'
Add-Type -Path (Join-Path $release 'CapaInterfaces.INTERFACES.dll')
Add-Type -Path (Join-Path $release 'CapaServicios.dll')
$secret=Read-Host 'Contraseña del nuevo administrador (12 a 128 caracteres)' -AsSecureString
$confirm=Read-Host 'Repetir contraseña' -AsSecureString
$first=[Runtime.InteropServices.Marshal]::SecureStringToBSTR($secret)
$second=[Runtime.InteropServices.Marshal]::SecureStringToBSTR($confirm)
try {
 $plain=[Runtime.InteropServices.Marshal]::PtrToStringBSTR($first)
 if($plain -cne [Runtime.InteropServices.Marshal]::PtrToStringBSTR($second)){throw 'Las contraseñas no coinciden.'}
 $hash=[CapaServicios.Encriptador]::Hash($plain)
} finally {
 [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($first);[Runtime.InteropServices.Marshal]::ZeroFreeBSTR($second)
 $plain=$null;$secret.Dispose();$confirm.Dispose()
}
$campos=New-Object CapaServicios.DV.UsuDv
$campos.usuario=$Usuario;$campos.contraseña='RESTABLECER';$campos.nombre='Administrador';$campos.apellido='Sistema';$campos.email=$Correo
$dvh=[CapaServicios.DV.DV]::GenerarDVH($campos)
$builder=New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source']=$Servidor;$builder['Initial Catalog']=$Base;$builder['Integrated Security']=$true
$builder['Encrypt'] = -not ($Servidor -match '^\(localdb\)\\');$builder['TrustServerCertificate']=$ConfiarCertificadoDePrueba.IsPresent
$cn=New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
try {
 $cn.Open();$tx=$cn.BeginTransaction([System.Data.IsolationLevel]::Serializable)
 try {
  $cmd=$cn.CreateCommand();$cmd.Transaction=$tx
  $cmd.CommandText=@'
IF OBJECT_ID('dbo.Seq_Usuario','SO') IS NULL THROW 51000,'Falta la migración 003.',1;
IF EXISTS(SELECT 1 FROM dbo.Usuario WHERE LoginName=@u OR Email=@e) THROW 51000,'El usuario o correo ya existe. No se cambia una cuenta existente con este asistente.',1;
DECLARE @idioma int=(SELECT TOP(1) Id_Idioma FROM dbo.Idioma ORDER BY Idioma_Default DESC,Id_Idioma);
DECLARE @rol int=(SELECT MIN(Id_Permiso) FROM dbo.Permiso WHERE Nombre=N'Administrador' AND Permiso IS NULL);
IF @idioma IS NULL OR @rol IS NULL THROW 51000,'Faltan idioma o familia Administrador. Instale los catálogos originales.',1;
DECLARE @id int=NEXT VALUE FOR dbo.Seq_Usuario;
INSERT dbo.Usuario(Id_Usuario,Id_Idioma,LoginName,Password,FirstName,LastName,Email,DVH)
VALUES(@id,@idioma,@u,'RESTABLECER',N'Administrador',N'Sistema',@e,@dvh);
INSERT dbo.Usuario_Credencial(Id_Usuario,PasswordHash) VALUES(@id,@hash);
INSERT dbo.Usuario_Permiso(Id_Usuario,Id_Permiso) VALUES(@id,@rol);
UPDATE dbo.Digito_Verificador SET DVV=CONVERT(nvarchar(50),(SELECT COUNT_BIG(*) FROM dbo.Usuario)*80000) WHERE IdTabla=1;
'@
  [void]$cmd.Parameters.AddWithValue('@u',$Usuario);[void]$cmd.Parameters.AddWithValue('@e',$Correo)
  [void]$cmd.Parameters.AddWithValue('@dvh',$dvh);[void]$cmd.Parameters.AddWithValue('@hash',$hash)
  [void]$cmd.ExecuteNonQuery();$tx.Commit()
 } catch {try {$tx.Rollback()}catch{};throw}
} finally {$cn.Dispose();$hash=$null}
Write-Output 'Administrador creado en la copia indicada. Ingrese con sus credenciales propias. No se envió ningún correo.'
