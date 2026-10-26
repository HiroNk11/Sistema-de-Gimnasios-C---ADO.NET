$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Data
Add-Type -AssemblyName System.Configuration
$archivo=Join-Path $PSScriptRoot 'CapaIU\bin\Release\Sistema Body Sport Demo.exe.config'
$map=New-Object System.Configuration.ExeConfigurationFileMap
$map.ExeConfigFilename=$archivo
$cfg=[System.Configuration.ConfigurationManager]::OpenMappedExeConfiguration($map,[System.Configuration.ConfigurationUserLevel]::None)
$cadena=$env:GYM_CONNECTION_STRING
if([string]::IsNullOrWhiteSpace($cadena)){$cadena=$cfg.ConnectionStrings.ConnectionStrings['cadena_conexion'].ConnectionString}
$builder=New-Object System.Data.SqlClient.SqlConnectionStringBuilder($cadena)
$builder['Connect Timeout']=10
Write-Output ('Servidor: '+$builder.DataSource)
Write-Output ('Base: '+$builder.InitialCatalog)
Write-Output ('Cifrado exigido: '+$builder.Encrypt)
Write-Output ('Confía certificado local: '+$builder.TrustServerCertificate)
$cn=New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
try {
 $cn.Open()
 Write-Output 'Conexión correcta.'
 $cmd=$cn.CreateCommand()
 $cmd.CommandText="SELECT CASE WHEN OBJECT_ID('dbo.Usuario','U') IS NOT NULL AND OBJECT_ID('dbo.Usuario_Credencial','U') IS NOT NULL AND OBJECT_ID('dbo.App_Sesion','U') IS NOT NULL AND OBJECT_ID('dbo.Seq_Usuario','SO') IS NOT NULL THEN 1 ELSE 0 END"
 if($cmd.ExecuteScalar() -eq 1){Write-Output 'Se encontraron los objetos principales de la versión mejorada.'}
 else{Write-Output 'Conectado, pero faltan objetos principales: compruebe las migraciones 001 y 003.'}
} catch {
 $errorReal=$_.Exception
 while($null -ne $errorReal.InnerException){$errorReal=$errorReal.InnerException}
 if($errorReal -is [System.Data.SqlClient.SqlException]){Write-Output ('Código SQL: '+$errorReal.Number)}
 Write-Output ('Detalle de conexión: '+$errorReal.Message)
} finally {$cn.Dispose()}
