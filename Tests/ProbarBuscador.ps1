param([string]$MSBuild,[string]$Destino)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
if(-not $MSBuild){$vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe';if(Test-Path -LiteralPath $vswhere){$MSBuild=& $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1}}
if(-not $MSBuild){throw 'Indique -MSBuild con la ruta de Visual Studio.'}
$bin=Join-Path $PSScriptRoot 'bin\buscadores';New-Item -ItemType Directory -Force -Path $bin | Out-Null
$csc=Join-Path (Split-Path $MSBuild -Parent) 'Roslyn\csc.exe'
& $csc /nologo /target:exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll "/out:$bin\PruebasBuscador.exe" (Join-Path $PSScriptRoot 'PruebasBuscador.cs') (Join-Path $root 'CapaIU\Controles\TemaVisual.cs') (Join-Path $root 'CapaIU\Controles\SelectorBuscable.cs')
if($LASTEXITCODE -ne 0){throw 'Fallo la compilacion de pruebas.'}
if(-not $Destino){$Destino=Join-Path $bin 'vistas'}
& (Join-Path $bin 'PruebasBuscador.exe') $Destino
if($LASTEXITCODE -ne 0){throw 'Fallaron las pruebas.'}
