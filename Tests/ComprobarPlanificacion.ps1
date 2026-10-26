[CmdletBinding()]
param([string]$Proyecto=(Split-Path $PSScriptRoot -Parent))
$ErrorActionPreference='Stop'
$anterior=$env:GYM_CONNECTION_STRING
try{
 $bin=Join-Path $Proyecto 'CapaIU\bin\Release'
 if(-not $env:GYM_CONNECTION_STRING){[xml]$xml=Get-Content -LiteralPath (Join-Path $bin 'Sistema Body Sport Demo.exe.config');$entrada=$xml.configuration.connectionStrings.add | Where-Object {$_.name -eq 'cadena_conexion'} | Select-Object -First 1;$env:GYM_CONNECTION_STRING=$entrada.connectionString}
 foreach($dll in @('CapaInterfaces.INTERFACES.dll','CapaDominio.dll','CapaServicios.dll','CapaRepositorio.dll')){Add-Type -Path (Join-Path $bin $dll)}
 $catalogo=New-Object CapaRepositorio.ActividadesRepository
 $ds=$catalogo.Cargar()
 Write-Output ('Actividades: '+$ds.Tables[0].Rows.Count)
 Write-Output ('Tipos: '+$ds.Tables[1].Rows.Count)
 Write-Output ('Profesores: '+$ds.Tables[2].Rows.Count)
 Write-Output ('Espacios: '+$ds.Tables[3].Rows.Count)
 $operaciones=New-Object CapaRepositorio.OperacionRepository
 $tabla=$operaciones.Listar('Sesiones',0)
 Write-Output ('Sesiones guardadas: '+$tabla.Rows.Count)
 Write-Output 'Consultas de catalogo y agenda correctas. No se modificaron datos.'
}catch{Write-Output ('No se pudo comprobar: '+$_.Exception.Message);exit 1}finally{$env:GYM_CONNECTION_STRING=$anterior}
