param([string]$MSBuild,[string]$Destino)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
if(-not $MSBuild){
 $vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
 if(Test-Path -LiteralPath $vswhere){$MSBuild=& $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1}
}
if(-not $MSBuild){throw 'Indique -MSBuild con la ruta de Visual Studio.'}
& $MSBuild (Join-Path $root 'CapaIU.sln') /t:Build /p:Configuration=Release /m:1 /nr:false /v:minimal /nologo
if($LASTEXITCODE -ne 0){throw 'Falló la compilación de la aplicación.'}
$bin=Join-Path $PSScriptRoot 'bin'
New-Item -ItemType Directory -Force -Path $bin | Out-Null
Get-ChildItem -LiteralPath (Join-Path $root 'CapaIU\bin\Release') -Filter 'Capa*.dll' | Copy-Item -Destination $bin
if(-not $Destino){$Destino=Join-Path $bin 'vistas-interfaz'}
$csc=Join-Path (Split-Path $MSBuild -Parent) 'Roslyn\csc.exe'
& $csc /nologo /target:exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /reference:System.Data.dll "/out:$bin\PruebasInterfaz.exe" (Join-Path $PSScriptRoot 'PruebasInterfaz.cs')
if($LASTEXITCODE -ne 0){throw 'Falló la compilación de las pruebas de interfaz.'}
& (Join-Path $bin 'PruebasInterfaz.exe') (Join-Path $root 'CapaIU\bin\Release\Sistema Body Sport Demo.exe') $Destino
if($LASTEXITCODE -ne 0){throw 'Fallaron las pruebas de interfaz.'}
