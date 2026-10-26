$ErrorActionPreference='Stop'
& python (Join-Path $PSScriptRoot 'GenerarModelo.py')
if($LASTEXITCODE -ne 0){throw 'No se pudo generar el modelo proyectado.'}
if($PSVersionTable.PSEdition -eq 'Core'){throw 'Ejecute este script con Windows PowerShell: powershell.exe -NoProfile -File Tests\ValidarModelo.ps1'}
$dac='C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\Extensions\Microsoft\SQLDB\DAC'
Add-Type -Path (Join-Path $dac 'Microsoft.SqlServer.Dac.dll')
Add-Type -Path (Join-Path $dac 'Microsoft.SqlServer.Dac.Extensions.dll')
$model=[Microsoft.SqlServer.Dac.Model.TSqlModel]::new([Microsoft.SqlServer.Dac.Model.SqlServerVersion]::Sql160,[Microsoft.SqlServer.Dac.Model.TSqlModelOptions]::new())
try {
 $model.AddObjects([IO.File]::ReadAllText((Join-Path $PSScriptRoot 'modelo_proyectado.sql')))
 $messages=$model.Validate()
 $messages | ForEach-Object {Write-Output ($_.Severity.ToString()+': '+$_.Message)}
 Write-Output ('MENSAJES: '+$messages.Count)
 if(@($messages | Where-Object Severity -eq 'Error').Count){exit 1}
} finally {$model.Dispose()}
