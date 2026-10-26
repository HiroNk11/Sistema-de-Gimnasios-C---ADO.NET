param([string]$MSBuild)
$ErrorActionPreference='Stop'
if(-not $MSBuild){
 $vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
 if(Test-Path -LiteralPath $vswhere){$MSBuild=& $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1}
}
if(-not $MSBuild){throw 'Indique -MSBuild con su ruta de Visual Studio.'}
& $MSBuild (Join-Path $PSScriptRoot 'CapaIU.sln') /t:Build /p:Configuration=Release /m:1 /nr:false /v:minimal /nologo
if($LASTEXITCODE -ne 0){throw 'No se pudo compilar la solución.'}
