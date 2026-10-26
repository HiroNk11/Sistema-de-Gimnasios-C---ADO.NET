param([string]$MSBuild)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
if(-not $MSBuild) {
 $vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
 if(Test-Path -LiteralPath $vswhere) {$MSBuild=& $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1}
}
if(-not $MSBuild){throw 'Indique -MSBuild con la ruta al MSBuild de Visual Studio.'}
& $MSBuild (Join-Path $root 'CapaIU.sln') /t:Build /p:Configuration=Release /m:1 /nr:false /v:minimal /nologo
if($LASTEXITCODE -ne 0){throw 'Falló la compilación'}
$release=Join-Path $root 'CapaIU\bin\Release'
$bin=Join-Path $PSScriptRoot 'bin'
New-Item -ItemType Directory -Force -Path $bin | Out-Null
Copy-Item (Join-Path $release '*.dll') $bin -Force
$csc=Join-Path (Split-Path $MSBuild -Parent) 'Roslyn\csc.exe'
$compilerArgs=@('/nologo','/target:exe','/langversion:7.3',"/out:$bin\Pruebas.exe")
foreach($dll in @('CapaServicios','CapaDominio','CapaInterfaces.INTERFACES','CapaAplicacion','CapaRepositorio')){$compilerArgs+="/reference:$bin\$dll.dll"}
$compilerArgs+=(Join-Path $PSScriptRoot 'Pruebas.cs')
& $csc @compilerArgs
if($LASTEXITCODE -ne 0){throw 'Falló la compilación de pruebas'}
& (Join-Path $bin 'Pruebas.exe') $root
if($LASTEXITCODE -ne 0){throw 'Fallaron las pruebas'}
