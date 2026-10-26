param([string]$ScriptDom)
$ErrorActionPreference='Stop'
if(-not $ScriptDom) {
 $ScriptDom=Get-ChildItem 'C:\Program Files\Microsoft Visual Studio' -Filter Microsoft.SqlServer.TransactSql.ScriptDom.dll -Recurse -ErrorAction SilentlyContinue | Where-Object FullName -Like '*SQLDB\DAC\*' | Select-Object -First 1 -ExpandProperty FullName
}
if(-not $ScriptDom) {throw 'Instale SQL Server Data Tools o indique -ScriptDom con la ruta de su DLL.'}
Add-Type -Path $ScriptDom
$parser=New-Object Microsoft.SqlServer.TransactSql.ScriptDom.TSql160Parser($true)
$files=@(Get-ChildItem (Join-Path $PSScriptRoot '..\Database') -Filter '*.sql') + @(Get-Item (Join-Path $PSScriptRoot 'fixture.sql'))
foreach($file in $files) {
 $errors=New-Object 'System.Collections.Generic.List[Microsoft.SqlServer.TransactSql.ScriptDom.ParseError]'
 $reader=New-Object System.IO.StringReader([IO.File]::ReadAllText($file.FullName))
 $null=$parser.Parse($reader,[ref]$errors)
 if($errors.Count) {$errors | ForEach-Object {Write-Output "$($file.Name):$($_.Line) $($_.Message)"};throw 'SQL inválido'}
 Write-Output "OK SQL: $($file.Name)"
}

$count=0
foreach($name in @('OperacionRepository.cs','CredencialRepository.cs','UsuarioRepository.cs','PermisosRepository.cs','Negocio\SocioRepository.cs')) {
 $path=Join-Path (Join-Path $PSScriptRoot '..\CapaRepositorio') $name
 $source=[IO.File]::ReadAllText($path)
 foreach($m in [regex]::Matches($source,'@"((?:""|[^"])*)"|"((?:\\.|[^"\\])*)"')) {
  if($m.Groups[1].Success) {$sql=$m.Groups[1].Value.Replace('""','"')} else {$sql=$m.Groups[2].Value.Replace('\"','"').Replace('\n',"`n").Replace('\r',"`r")}
  if($sql -notmatch '^\s*(;?WITH |SELECT |UPDATE |DELETE |INSERT |IF EXISTS)' -or $sql.Contains('{')) {continue}
  $errors=New-Object 'System.Collections.Generic.List[Microsoft.SqlServer.TransactSql.ScriptDom.ParseError]'
  $reader=New-Object System.IO.StringReader($sql)
  $null=$parser.Parse($reader,[ref]$errors)
  if($errors.Count) {$errors | ForEach-Object {Write-Output "$name $($_.Message)"};throw 'Consulta SQL inválida'}
  $count++
 }
}
Write-Output "OK SQL: $count consultas del código C#"
