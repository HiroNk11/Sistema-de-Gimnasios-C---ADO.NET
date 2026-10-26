param([string]$MSBuild)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
& (Join-Path $root 'Compilar.ps1') -MSBuild $MSBuild
if(-not $MSBuild){
 $vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
 $MSBuild=& $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
}
$bin=Join-Path $PSScriptRoot 'bin\bitacora'
New-Item -ItemType Directory -Force -Path $bin | Out-Null
Get-ChildItem -LiteralPath (Join-Path $root 'CapaIU\bin\Release') -Filter 'Capa*.dll' | Copy-Item -Destination $bin -Force
$refs=Get-ChildItem -LiteralPath $bin -Filter 'Capa*.dll' | ForEach-Object {'/reference:'+ $_.FullName}
$csc=Join-Path (Split-Path $MSBuild -Parent) 'Roslyn\csc.exe'
& $csc /nologo /target:exe /reference:System.Data.dll /reference:System.Core.dll "/out:$bin\PruebasBitacora.exe" @refs (Join-Path $PSScriptRoot 'PruebasBitacora.cs')
if($LASTEXITCODE -ne 0){throw 'Fallo la compilacion de pruebas.'}
& (Join-Path $bin 'PruebasBitacora.exe')
if($LASTEXITCODE -ne 0){throw 'Fallaron las pruebas de bitacora.'}
