[CmdletBinding()]
param(
 [ValidatePattern('^DatabaseGym_Pruebas(?:_[A-Za-z0-9]+)?$')][string]$Base='DatabaseGym_Pruebas',
 [string]$Servidor='(localdb)\MSSQLLocalDB'
)
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Data
$b=New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$b['Data Source']=$Servidor;$b['Initial Catalog']=$Base;$b['Integrated Security']=$true
$b['Encrypt']=-not ($Servidor -match '^\(localdb\)\\');$b['Connect Timeout']=10
$cn=New-Object System.Data.SqlClient.SqlConnection($b.ConnectionString)
try{
 $cn.Open();$cmd=$cn.CreateCommand()
 $cmd.CommandText=[IO.File]::ReadAllText((Join-Path $PSScriptRoot 'VerificarDatosDemo.sql'))
 $da=New-Object System.Data.SqlClient.SqlDataAdapter($cmd);$ds=New-Object System.Data.DataSet
 try{[void]$da.Fill($ds);foreach($t in $ds.Tables){$t | Format-Table -AutoSize | Out-Host}}finally{$da.Dispose();$cmd.Dispose()}
 if(@($ds.Tables[2].Rows | Where-Object {[int]$_.Problemas -gt 0}).Count -gt 0){throw 'Hay inconsistencias. Conserve el resultado para revisar el bug. No se modifico la base.'}
 Write-Output 'Sin inconsistencias en las cinco comprobaciones consultadas. Esto no certifica todos los circuitos de la aplicacion.'
}finally{$cn.Dispose()}
