[CmdletBinding()]
param([string]$Proyecto=(Split-Path $PSScriptRoot -Parent),[string]$Configuracion)
$ErrorActionPreference='Stop'
$anterior=$env:GYM_CONNECTION_STRING
try{
 $bin=Join-Path $Proyecto 'CapaIU\bin\Release'
 if(-not $Configuracion){$Configuracion=Join-Path $bin 'Sistema Body Sport Demo.exe.config'}
 [xml]$xml=Get-Content -LiteralPath $Configuracion
 $entrada=$xml.configuration.connectionStrings.add | Where-Object {$_.name -eq 'cadena_conexion'} | Select-Object -First 1
 if(-not $entrada){throw 'La configuracion no contiene cadena_conexion.'}
 $env:GYM_CONNECTION_STRING=$entrada.connectionString
 foreach($dll in @('CapaInterfaces.INTERFACES.dll','CapaDominio.dll','CapaServicios.dll','CapaRepositorio.dll')){Add-Type -Path (Join-Path $bin $dll)}
 $repo=New-Object CapaRepositorio.MonitoreoRepository
 $ds=$repo.Inicio()
 $r=$ds.Tables['Resumen'].Rows[0]
 Write-Output ('Socios registrados: '+$r.Socios)
 Write-Output ('Socios con saldo: '+$r.SociosConSaldo)
 Write-Output ('Saldo pendiente: '+$r.SaldoPendiente)
 $tabla=$ds.Tables['Pendientes']
 if($tabla.Rows.Count -ne [int]$r.SociosConSaldo){throw 'El contador no coincide con el listado.'}
 $suma=0D;foreach($row in $tabla.Rows){$suma += [decimal]$row.Saldo}
 if($suma -ne [decimal]$r.SaldoPendiente){throw 'El saldo total no coincide con el listado.'}
 $hoy=[DateTime]::Today;$lunes=$hoy.AddDays(-(([int]$hoy.DayOfWeek+6)%7))
 $semana=$repo.Semana($lunes)
 Write-Output ('Sesiones en la semana: '+$semana.Tables['Sesiones'].Rows.Count)
 Write-Output ('Registros de participantes: '+$semana.Tables['Participantes'].Rows.Count)
 Add-Type -AssemblyName System.Data
 $cn=New-Object System.Data.SqlClient.SqlConnection($entrada.connectionString)
 try{
  $cn.Open();$cmd=$cn.CreateCommand();$cmd.CommandText=@'
SELECT COUNT(*) FROM dbo.App_Reserva a JOIN dbo.App_Reserva b ON a.Id<b.Id AND a.Id_Socio=b.Id_Socio
JOIN dbo.App_Sesion sa ON sa.Id=a.Id_Sesion JOIN dbo.App_Sesion sb ON sb.Id=b.Id_Sesion
WHERE a.Estado IN('Confirmada','Espera') AND b.Estado IN('Confirmada','Espera')
AND sa.Estado='Programada' AND sb.Estado='Programada' AND sa.Inicio<sb.Fin AND sa.Fin>sb.Inicio;
'@
  $cruces=[int]$cmd.ExecuteScalar();Write-Output ('Cruces de reservas activas existentes: '+$cruces)
  if($cruces -gt 0){Write-Warning 'Hay cruces previos: reviselos desde el calendario. Esta comprobacion no los modifica.'}
 }finally{if($cmd){$cmd.Dispose()};$cn.Dispose()}
 Write-Output 'Comprobacion de lectura completada. No se modificaron datos.'
}finally{$env:GYM_CONNECTION_STRING=$anterior}
