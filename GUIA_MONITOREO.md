# Calendario semanal y seguimiento de saldos

Disponible para Recepción y para cuentas heredadas que tengan acceso al negocio. Administración de cuentas conserva únicamente el alta de usuarios.

## Inicio

Inicio muestra cuatro indicadores generales: socios registrados, socios con saldo pendiente, importe total pendiente y sesiones programadas del día. Cada socio se cuenta una sola vez aunque tenga varios planes impagos.

El saldo contempla todas las membresías con importe todavía pendiente, incluidas vencidas y futuras. No significa necesariamente una deuda vencida. El primer vencimiento corresponde al plan pendiente con fecha de finalización más temprana. El pago se registra contra una membresía específica.

Seleccioná una persona en la tabla y pulsá **Ver membresías / cobrar**. Se abre Operación diaria en Membresías, filtrada por ese socio. Elegí el plan correspondiente y registrá el pago en su panel; el acceso rápido no genera cobros automáticos.

El botón **Saldos pendientes** abre un listado que permite buscar por nombre, apellido o número de socio. La búsqueda admite tildes y mayúsculas indistintamente. Inicio y el listado se actualizan al volver a activarlos; también podés pulsar Actualizar.

## Calendario semanal

1. Abrí **Calendario semanal** desde Inicio, la barra lateral o el menú de negocio.
2. Usá Semana anterior/siguiente, Hoy o elegí una fecha y pulsá **Ver semana**. Se muestra lunes a domingo.
3. Cada tarjeta ocupa su intervalo horario. Las actividades simultáneas se muestran una al lado de otra, y las que cruzan medianoche aparecen en ambos días.
4. Elegí una actividad para filtrar. Las canceladas están ocultas inicialmente; **Incluir canceladas** permite revisarlas.
5. Hacé clic en una tarjeta: debajo aparecen actividad, profesor, espacio, ocupación, lista de espera y socios con estado y asistencia. Las reservas canceladas también se conservan en ese detalle.
6. Pasá el cursor para ver el detalle completo. También podés recorrer sesiones con las flechas y Enter. Usá las barras de desplazamiento para horarios alejados o pantallas pequeñas.

Ámbar indica cupo completo y gris una sesión cancelada. Los intervalos sin tarjetas no tienen sesiones programadas según el filtro actual. Los números de una tarjeta cuentan confirmadas y lista de espera por separado, sin sumar canceladas. El calendario consulta sesiones por fecha de las tablas App_*; no convierte las inscripciones semanales históricas.

Pulsá **Ver semana** para recargar datos después de cambios realizados en otra ventana o computadora. El calendario es de consulta: las altas de horarios siguen en Horarios y las reservas/cancelaciones en Operación diaria.

## Regla de horarios de un socio

No se permite reservar dos sesiones cuyos intervalos se crucen, total o parcialmente, incluso cuando alguna reserva esté en espera. Se permite terminar una actividad a las 19:00 e iniciar otra a las 19:00. La regla considera fechas y horas completas, incluidas sesiones que cruzan medianoche.

Las reservas canceladas y las sesiones canceladas no bloquean nuevos horarios. Al promocionar una reserva desde espera se vuelve a comprobar la compatibilidad. No se corrigen automáticamente cruces previos: revisalos antes de operar si ya había datos cargados con las reglas antiguas.

Las altas, cancelaciones y promociones usan un bloqueo común de reservas dentro de su transacción, pensado para la recepción de uno o dos puestos. Ante una coincidencia que SQL revierta por interbloqueo, el programa pide actualizar y volver a intentar. Esto no sustituye la prueba real con dos procesos y una base central.

## Verificación

No se requiere una migración nueva: estas funciones consultan las tablas existentes. Compilá antes de usar los nuevos formularios.

```powershell
powershell.exe -NoProfile -File .\Tests\ProbarMonitoreo.ps1
powershell.exe -NoProfile -File .\Tests\ComprobarMonitoreo.ps1
```

El primer script prueba lógica e interfaz con datos ficticios. El segundo consulta la conexión del ejecutable Release, comprueba que contadores y saldos coincidan con los listados y detecta cruces existentes; no modifica datos. Para revisar otra copia, usá `-Configuracion` con la ruta de su archivo `.exe.config`.

`Tests/EjecutarPruebas.ps1` incluye casos de integración para deudas múltiples, cruces de confirmadas/espera, actividades consecutivas, promociones y reservas concurrentes. Solo instala datos cuando `GYM_TEST_CONNECTION_STRING` apunta a una base **vacía** cuyo nombre empieza con `GymTests_`. La base se conserva para inspección. No apuntes esas pruebas a una base de uso habitual.

Validación local: compilación, pruebas de interfaz y distribución semanal, regresiones y análisis estático SQL. La ejecución de integración SQL y concurrencia queda pendiente en la instancia del usuario.
