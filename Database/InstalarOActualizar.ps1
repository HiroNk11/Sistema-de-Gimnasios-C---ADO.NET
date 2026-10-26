[CmdletBinding()]
param(
 [string]$Servidor='(localdb)\MSSQLLocalDB',
 [ValidatePattern('^[A-Za-z][A-Za-z0-9_]{0,100}$')][string]$Base='DatabaseGym_Mejorado',
 [ValidateSet('Instalar','Actualizar')][string]$Modo='Instalar',
 [switch]$ConfiarCertificadoDePrueba
)
$ErrorActionPreference='Stop'

if($Base -in @('master','model','msdb','tempdb','DatabaseGym')) {throw 'Use una copia con otro nombre. Este asistente protege la base original DatabaseGym.'}

Add-Type -AssemblyName System.Data
$builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder

# CORRECCIÓN: Usar los nombres de las claves exactas de SQL Server usando sintaxis de diccionario
$builder['Data Source'] = $Servidor
$builder['Initial Catalog'] = 'master'
$builder['Integrated Security'] = $true
$builder['Encrypt'] = -not ($Servidor -match '^\(localdb\)\\')
$builder['TrustServerCertificate'] = $ConfiarCertificadoDePrueba.IsPresent

$master = New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
try {
 $master.Open()
 $cmd = $master.CreateCommand()
 $cmd.CommandText = 'SELECT DB_ID(@base)'
 [void]$cmd.Parameters.AddWithValue('@base',$Base)
 $value = $cmd.ExecuteScalar()
 $existe = $null -ne $value -and $value -isnot [DBNull]

 if($Modo -eq 'Instalar') {
  if($existe){throw 'La base ya existe. Para migrar una COPIA existente use -Modo Actualizar; no se sobrescribe ninguna base.'}
  $cmd.CommandText="CREATE DATABASE [$Base]"
  $cmd.Parameters.Clear()
  $cmd.CommandTimeout=120
  [void]$cmd.ExecuteNonQuery()
 } elseif(-not $existe) {
     throw 'La copia de la base indicada no existe.'
 }
} finally {
    $master.Dispose()
}

# CORRECCIÓN: Apuntamos ahora a la base recién creada
$builder['Initial Catalog'] = $Base
$cn = New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)

function EjecutarArchivo([string]$archivo) {
 $text = [IO.File]::ReadAllText((Join-Path $PSScriptRoot $archivo))
 foreach($batch in [regex]::Split($text,'(?im)^\s*GO\s*$')) {
  if([string]::IsNullOrWhiteSpace($batch)){continue}
  $command = $cn.CreateCommand()
  $command.Transaction = $tx
  $command.CommandTimeout = 180
  $command.CommandText = $batch
  try { [void]$command.ExecuteNonQuery() } finally { $command.Dispose() }
 }
 Write-Output "Aplicado: $archivo"
}

try {
 $cn.Open()
 $tx = $cn.BeginTransaction()
 try {
  if($Modo -eq 'Instalar'){ EjecutarArchivo '000_esquema_original.sql'; EjecutarArchivo '000_catalogos_originales.sql' }
  EjecutarArchivo '001_actualizacion.sql'
  EjecutarArchivo '003_compatibilidad_DatabaseGym.sql'
  $tx.Commit()
 } catch {
     try { $tx.Rollback() } catch {}
     throw
 }
} finally {
    $cn.Dispose()
}

Write-Output "Listo: $Servidor / $Base. La base DatabaseGym original no fue modificada."
Write-Output 'En instalación nueva: compile y ejecute ConfigurarAdministrador.ps1 para crear una cuenta propia.'
