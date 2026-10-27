> Perfiles Administración de cuentas y Recepción: ver PERFILES_Y_MULTIUSUARIO.md. La asignación a cuentas es explícita.

> Programación de actividades actualizada: ver GUIA_PLANIFICACION.md.

> Interfaz actualizada el 22/10/2026: ver REDISENO_INTERFAZ.md. No requiere migraciones nuevas.

# Body Sport Gym — versión 2, adaptada a DatabaseGym

Actualización del 21/10/2026 a partir del proyecto y del archivo `scriptfinal.sql` proporcionados. La carpeta del Escritorio `TP CAMPO Mejorado` fue actualizada. Los archivos originales no se modificaron y no se ejecutaron cambios sobre su base de datos.

## Cambios de esta versión

Se revisaron las 19 tablas y 53 procedimientos del script original. La aplicación y los scripts nuevos usan ese esquema real.

- Conexión predeterminada a `DatabaseGym_Mejorado`, una base separada, en LocalDB. Se puede configurar otro servidor.

- Compatibilidad con los permisos originales `PuedeAccederBackup` y `PuedeAccederRestore`; cada acción conserva su autorización. Tener permiso de backup no concede restauración.

- Secuencias SQL para usuarios e idiomas, en reemplazo de `MAX + 1`. No se renumeran registros existentes.

- Unicidad de usuario y correo. Si hay duplicados en una copia existente, la migración se detiene antes de alterarlos; no elimina cuentas automáticamente.

- Correo de hasta 254 caracteres, búsqueda de usuario Unicode y listados SQL sin la columna de contraseña.

- Historial de reservas con ID de clase entero, fechas `datetime2` e importes `decimal(18,2)`. Se interpretan explícitamente las fechas con meses en inglés que trae el script y el formato día/mes/año con barras. Ante fechas no convertibles, la migración se detiene.

- Bitácora con actividad de hasta 512 caracteres y fecha UTC para los eventos nuevos; se conservan las columnas históricas para la interfaz anterior.

- Corrección del procedimiento de traducciones y del registro de cambios de socios.

- Corrección de la selección y edición de usuarios en la grilla y del ID real de las inscripciones.

- Bloqueo en SQL de los accesos antiguos que comparaban o cambiaban contraseñas sin el flujo seguro.

Se mantienen las mejoras anteriores: contraseñas PBKDF2 con sal aleatoria, recuperación con tokens de 15 minutos y cinco intentos, controles de permisos en BLL, inscripciones transaccionales y módulos de membresías, pagos, sesiones por fecha, lista de espera y asistencia.

## Instalación nueva para probar

Se entrega el esquema original y sus catálogos de idiomas, traducciones y permisos. NO se importan los usuarios, correos, socios, profesores, clases, reservas o bitácoras personales que estaban en el script. Puede crear datos de prueba desde la aplicación. La instalación no incluye una contraseña universal.

1. Abrir PowerShell en la carpeta `TP CAMPO` del proyecto.

2. Crear la base nueva (el asistente se niega a sobrescribir una base existente y protege el nombre original `DatabaseGym`):

```powershell

.\Database\InstalarOActualizar.ps1 -Servidor '(localdb)\MSSQLLocalDB' -Base DatabaseGym_Mejorado -Modo Instalar -ConfiarCertificadoDePrueba

```

El último parámetro es para una instancia de prueba con certificado autofirmado. Para un servidor con certificado válido, omitirlo. Para otra instancia, cambiar `-Servidor`. La cuenta Windows que ejecute el asistente debe poder crear la base.

3. El repositorio incluye fuentes. Restaurar los paquetes NuGet y compilar con Visual Studio y las herramientas .NET Framework 4.7.2:

```powershell

.\Compilar.ps1

```

4. Crear su propio administrador, indicando un usuario y correo elegidos por usted:

```powershell

.\Database\ConfigurarAdministrador.ps1 -Servidor '(localdb)\MSSQLLocalDB' -Base DatabaseGym_Mejorado -Usuario 'administrador' -Correo 'administrador@example.invalid' -ConfiarCertificadoDePrueba

```

El script solicita y confirma la contraseña en pantalla sin mostrarla; debe tener entre 15 y 128 caracteres. No recibe la contraseña en la línea de comandos, no imprime el hash y no envía correos. No sobrescribe cuentas existentes. Asigna la familia Administrador del catálogo original. Esa familia no tenía asignados backup y restore: se pueden asignar desde la pantalla de permisos si se necesitan.

5. Para iniciar contra la instancia local de pruebas desde la misma terminal:

```powershell

$env:GYM_CONNECTION_STRING='Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseGym_Mejorado;Integrated Security=True;Encrypt=False;TrustServerCertificate=True'

& '.\CapaIU\bin\Release\Sistema Body Sport Demo.exe'

```

Esta variable solo afecta a esa terminal y a la aplicación que abre desde allí. Para usar el ejecutable directamente, configurar `cadena_conexion` en su archivo `.exe.config`, o una variable de entorno de usuario. Para un servidor real, validar su certificado y usar `TrustServerCertificate=False`.

## Actualizar una copia que ya tiene datos

Primero crear/restaurar una copia de la base original con otro nombre. El asistente NO hace el respaldo ni copia una base existente por sí solo. Luego ejecutar, indicando el servidor y el nombre de esa copia:

```powershell

.\Database\InstalarOActualizar.ps1 -Servidor '.' -Base DatabaseGym_Copia -Modo Actualizar

```

Solo se aplican `001_actualizacion.sql` y `003_compatibilidad_DatabaseGym.sql`; no se vuelven a importar catálogos ni datos. El asistente usa una transacción para los cambios. Si falla una conversión o una validación, conserva los datos originales de la copia. Una instalación nueva fallida puede dejar la base nueva vacía para inspección; no la borra automáticamente.

Los usuarios importados conservan sus IDs y permisos, pero deben restablecer la contraseña por correo porque el formato MD5 anterior no se acepta. Configurar `GYM_SMTP_USER`, `GYM_SMTP_PASSWORD` y opcionalmente `GYM_SMTP_HOST`; puerto 587 con TLS. Se eliminaron las credenciales que venían incrustadas en el código; si siguen vigentes, revocarlas. El alta de administrador del asistente funciona sin SMTP.

No ejecutar el script original con `USE [DatabaseGym]` sobre una instalación existente: ese archivo crea objetos e inserta datos. Los archivos entregados no contienen esa selección automática de la base original.

## Uso de los módulos nuevos

Ingresar y abrir **Negocio → Operación diaria**:

1. Crear un socio en el ABM de socios.

2. Crear su membresía con plan, fechas e importe; registrar el pago total o parcial. La reserva exige una membresía pagada y vigente en la fecha de la sesión.

3. Crear profesores y clases desde los formularios de negocio; luego programar sesiones con fecha, hora y cupo desde Operación diaria.

4. Reservar para un socio, gestionar la lista de espera y cancelar reservas o sesiones futuras. Al liberar un lugar se promueve al primer socio elegible.

5. Registrar presente o ausente después de iniciada la sesión. Consultar pagos, saldos y vencimientos en los demás paneles.

El circuito semanal antiguo y el circuito nuevo `App_*` se conservan separados. Las reservas y cuotas viejas no se convierten automáticamente al modelo nuevo. No usar ambos para administrar el mismo cupo. Los horarios se interpretan en la zona local del gimnasio: SQL Server y los clientes deben usar la misma zona. Los timestamps de auditoría y recuperación se almacenan en UTC.

## Verificación realizada

- Solución Release compilada.

- 31 pruebas sin conexión a SQL Server aprobadas.

- Sintaxis de todos los scripts SQL y de 71 consultas C# validada con ScriptDom.

- Modelo final proyectado de 100 definiciones validado con DacFx: cero mensajes de error o advertencia de referencias.

- Los 48 procedimientos utilizados por el acceso a datos están definidos en los scripts entregados.

- Sintaxis de los asistentes PowerShell validada.

Las pruebas de integración ahora usan EL ESQUEMA REAL extraído del script, con datos artificiales. Verifican fechas inglesas y día/mes, migraciones repetibles, último cupo concurrente, lista de espera, pagos, rollback, recuperación y altas concurrentes con secuencia. Para ejecutarlas, crear una base vacía cuyo nombre comience por `GymTests_`, configurar `GYM_TEST_CONNECTION_STRING` y ejecutar `Tests/EjecutarPruebas.ps1`.

No se ejecutó esa integración: el entorno de esta sesión no permite acceder a la configuración de LocalDB. Tampoco se ejecutaron los asistentes de instalación ni se creó ninguna cuenta real. Validar el modelo y compilar no sustituyen una prueba funcional con SQL Server, correo y la interfaz.

Para repetir las verificaciones estáticas: `Tests/ValidarSql.ps1` requiere ScriptDom de SQL Server Data Tools; `powershell.exe -NoProfile -File Tests\ValidarModelo.ps1` requiere DacFx y Python 3. El modelo se genera a partir de los archivos de esta entrega.

## Límites pendientes

Esta es una versión para revisión y pruebas, aún no una puesta en producción. Quedan: integración completa con SQL Server y correo, validación visual, conciliación del circuito semanal, permisos específicos de caja, bloqueo de login persistente, devoluciones/cierre de caja, feriados y reemplazos, portal/API, instalador de escritorio y migración a .NET moderno. El registro de pagos no emite comprobantes fiscales ni se conecta al banco. Se mantienen los paquetes originales hasta validar sus actualizaciones. La auditoría nueva registra autor, operación y fecha, pero no todos los valores previos.

## Ajuste de LocalDB del 22/10/2026

El diagnóstico confirmó que esta instancia de LocalDB no admite cifrado. La conexión de pruebas usa Encrypt=False; los asistentes mantienen Encrypt=True para servidores que no sean LocalDB. Las copias locales de configuraciones anteriores se excluyen de Git.
