# Body Sport Gym

Sistema de gestión de gimnasios en C#, Windows Forms y ADO.NET, con SQL Server. Incluye gestión de socios, profesores, actividades, planificación de sesiones, membresías, pagos, reservas, lista de espera y asistencia.

## Requisitos

- Windows y Visual Studio con desarrollo de escritorio .NET y herramientas de .NET Framework 4.7.2.
- SQL Server LocalDB para desarrollo, o una instancia SQL Server configurada.
- Restaurar los paquetes NuGet de `CapaIU.sln` desde Visual Studio antes de compilar. Los paquetes y ejecutables no se guardan en Git.

## Primer inicio

Abrir PowerShell en la raíz del repositorio. Después de restaurar NuGet:

```powershell
.\Compilar.ps1
.\Database\InstalarOActualizar.ps1 -Base DatabaseGym_Mejorado
.\Database\ConfigurarAdministrador.ps1 -Usuario administrador -Correo administrador@example.invalid
```

El último comando solicita una contraseña propia de 12 a 128 caracteres sin mostrarla. No existe una contraseña predeterminada. La instalación crea una base nueva y rechaza sobrescribir una existente. Los scripts no importan datos personales de la base original.

Para activar el perfil limitado de administración y poder crear cuentas de Recepción:

```powershell
.\Database\ConfigurarPerfil.ps1 -Usuario administrador -Perfil Administracion
& '.\CapaIU\bin\Release\Sistema Body Sport Demo.exe'
```

Este paso aplica `004_perfiles.sql` y sustituye los permisos anteriores de la cuenta indicada. Antes de aplicarlo, la cuenta de instalación tiene el perfil técnico heredado. Las cuentas creadas desde el formulario deben cambiar su contraseña inicial al ingresar.

## Perfiles

| Perfil | Funciones |
| --- | --- |
| Administración de cuentas | Crear usuarios con un perfil fijo. |
| Recepción | Socios, profesores, actividades, horarios, membresías, pagos, reservas y asistencia. |

Ambos pueden cambiar su propia contraseña. Los perfiles nuevos no conceden eliminación física de socios/profesores, gestión arbitraria de permisos ni respaldo/restauración. Los permisos heredados requieren migración explícita; ver [reglas y activación](PERFILES_Y_MULTIUSUARIO.md).

## Datos de prueba

```powershell
.\Database\Demo\PrepararPruebas.ps1
.\Database\Demo\ComprobarPruebas.ps1
```

La muestra usa una base separada `DatabaseGym_Pruebas`, con 150 socios y casos de planes, pagos y reservas. Solicita una clave propia para `demo`. No reinicia datos existentes. Ver [guía de pruebas](Database/Demo/LEEME.md) y [24 casos de uso](Database/Demo/PlanDePruebas.csv).

## Estructura

- `CapaIU`: formularios, controles y presentación.
- `CapaAplicacion`: reglas de negocio y autorización.
- `CapaDominio`: entidades y contratos del dominio.
- `CapaRepositorio`: persistencia ADO.NET y transacciones.
- `CapaServicios`: autenticación, permisos y servicios comunes.
- `CapaInterfaces.INTERFACES`: abstracciones compartidas.
- `Database`: esquema, migraciones e instalación.
- `Tests`: verificaciones y regresiones.

## Configuración

La conexión predeterminada utiliza autenticación integrada de Windows y LocalDB. Puede cambiarse con `GYM_CONNECTION_STRING` o `cadena_conexion` en la configuración del ejecutable. La excepción `Encrypt=False` corresponde al entorno LocalDB; para un servidor compartido, configurar cifrado y certificado válidos.

El correo es opcional para la operación diaria. La recuperación por correo requiere `GYM_SMTP_USER` y `GYM_SMTP_PASSWORD`; `GYM_SMTP_HOST` es opcional. No guardar secretos en el código, archivos versionados ni comandos compartidos.

## Documentación y validación

El [manual PDF](CapaIU/Documentacion/ManualUsuario.pdf) también se abre con F1. Sus ejemplos de rutas del Escritorio corresponden a una instalación local: reemplazarlos por la ubicación del clon. Consultar [planificación](GUIA_PLANIFICACION.md), [buscadores](GUIA_BUSCADORES.md) y [diseño y patrones](MEJORAS_PATRONES.md).

Para ejecutar las pruebas de interfaz y lógica desde Windows PowerShell:

```powershell
powershell.exe -NoProfile -File .\Tests\ProbarPerfiles.ps1
powershell.exe -NoProfile -File .\Tests\ProbarInterfaz.ps1
powershell.exe -NoProfile -File .\Tests\ProbarPlanificacion.ps1
powershell.exe -NoProfile -File .\Tests\ProbarPatrones.ps1
```

Estas pruebas no certifican la instalación SQL ni la concurrencia entre dos puestos. LocalDB es local: para dos computadoras se necesita una base central y validar acceso, transacciones simultáneas y respaldo. El sistema continúa en evolución; la anulación de pagos y el despliegue compartido requieren definiciones adicionales.
