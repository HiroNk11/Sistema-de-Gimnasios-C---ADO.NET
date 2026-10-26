param([string]$MSBuild,[string]$Destino)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
if(-not $MSBuild){$vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe';$MSBuild=& $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1}
if(-not $MSBuild){throw 'No se encontro MSBuild.'}
& (Join-Path $root 'Compilar.ps1') -MSBuild $MSBuild
$bin=Join-Path $PSScriptRoot 'bin\monitoreo';New-Item -ItemType Directory -Force -Path $bin | Out-Null
Get-ChildItem -LiteralPath (Join-Path $root 'CapaIU\bin\Release') -Filter 'Capa*.dll' | Copy-Item -Destination $bin -Force
$refs=Get-ChildItem -LiteralPath $bin -Filter 'Capa*.dll' | ForEach-Object {'/reference:'+ $_.FullName}
$csc=Join-Path (Split-Path $MSBuild -Parent) 'Roslyn\csc.exe'
& $csc /nologo /target:exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /reference:System.Data.dll /reference:System.Core.dll "/out:$bin\PruebasMonitoreo.exe" @refs (Join-Path $PSScriptRoot 'PruebasMonitoreo.cs')
if($LASTEXITCODE -ne 0){throw 'Fallo la compilacion de pruebas.'}
if(-not $Destino){$Destino=Join-Path $bin 'vistas'}
& (Join-Path $bin 'PruebasMonitoreo.exe') (Join-Path $root 'CapaIU\bin\Release\Sistema Body Sport Demo.exe') $Destino
if($LASTEXITCODE -ne 0){throw 'Fallaron las pruebas de monitoreo.'}
