# Programación de actividades — 22/10/2026

## Cómo usarla

1. Cerrá el programa abierto y ejecutá la versión actualizada de CapaIU/bin/Release/Sistema Body Sport Demo.exe, o iniciá Debug desde Visual Studio.
2. En **Actividades**, registrá nombre, tipo, profesor, espacio y cupo inicial. Se muestran también las actividades que aún no tienen horarios. Seleccioná una fila para editarla.
3. En **Horarios**, elegí la actividad, el período inclusive, los días de la semana, las horas de inicio y fin y el cupo de cada sesión.
4. Pulsá **Revisar propuesta**. Revisá las fechas y luego **Guardar sesiones**. Se pide confirmación antes de escribir.
5. En **Agenda guardada**, consultá las sesiones de esa actividad. Sin selección de actividad se ve toda la agenda. Las sesiones futuras pueden cancelarse con sus reservas, previa confirmación.
6. En **Operación diaria**, gestioná membresías, pagos, reservas, espera y asistencia. Una reserva requiere membresía vigente y pagada para esa fecha, como en la versión anterior.

Ejemplo: lunes y miércoles, de 18:00 a 19:00, durante cuatro semanas genera ocho sesiones con fechas concretas.

## Comportamiento

- El período admite hasta un año y 200 sesiones por operación. Horas en formato de 24 horas, dentro del mismo día.
- Cambiar actividad, fechas, días, horas o cupo invalida la propuesta; es necesario revisarla otra vez.
- Las sesiones deben ser futuras. Se rechazan duraciones nulas, intervalos invertidos y propuestas duplicadas.
- Los cruces se comprueban por intervalo real y por identificadores de profesor y espacio. Dos sesiones contiguas son válidas: 18:00–19:00 y 19:00–20:00.
- El repositorio comprueba todas las fechas antes de insertar. Si hay cruces, informa fechas y actividades, y no se guarda ninguna del lote. Se comprueba otra vez al guardar, dentro de una transacción serializable.
- Programar una sesión desde cualquier circuito nuevo utiliza el mismo repositorio. La pantalla diaria ya no tiene otro editor de horarios.
- El catálogo usa IDs para profesor, tipo y espacio. Los homónimos no se eligen comparando nombres.
- Cambiar profesor o espacio de una actividad con sesiones futuras u horarios semanales asociados se bloquea. Creá otra actividad con la nueva configuración para conservar el historial.
- El cupo inicial solo se usa al crear la actividad. Cada sesión tiene su cupo propio. Editar una actividad no modifica contadores de inscripciones antiguas.

## Limpieza del proyecto

Retirados los formularios FrmCrearClase, FrmGestionarClase, FrmAsignarHorarioClase, FrmAdministrarClase y FrmVerCalendario, junto con sus diseñadores y recursos. Sus rutas llevan al catálogo, la planificación y la operación diaria.
Retirados los modelos Horario y Clases, y 29 métodos sin consumidores de ClaseBLL/ClaseRepository, incluidos los de asignación por texto y búsquedas de IDs por nombre.
El código eliminado se respaldó antes de modificar el proyecto, en el espacio de trabajo: work/antes_planificacion.zip.

No se eliminaron tablas, procedimientos almacenados ni registros históricos de la base. Las lecturas de clases semanales y detalle histórico se conservan. Los horarios antiguos NO se convierten automáticamente en sesiones con fecha y NO participan del control de cruces de App_Sesion. Antes de operar con actividades heredadas, revisá las inscripciones semanales vigentes y trasladá su planificación para evitar dobles agendas. No se crean inscripciones semanales nuevas desde el menú principal.

## Código y patrones

- CapaDominio/Planificacion.cs: cálculo puro de fechas e intervalos, independiente de SQL y WinForms.
- PlanificacionBLL: autorización y validación del lote; recibe IPlanificacionRepository por constructor.
- PlanificacionRepository: transacción, control de cruces y escritura; consultas parametrizadas con tipos explícitos. Se comparte para sesiones individuales y recurrentes.
- ActividadesBLL/ActividadesRepository: catálogo y restricciones al modificar recursos.
- Controles/PantallaGestion: tratamiento compartido de espera, errores y acciones asíncronas para las dos pantallas nuevas.
- Presentacion/NavegadorMdi: reutilización de ventanas.

El bloqueo de planificación es global sobre App_Sesion: prioriza consistencia para un gimnasio y lotes acotados; no se hicieron pruebas de carga o concurrencia con múltiples terminales.
Las pantallas nuevas se construyen en C# y usan textos en español. No se agregaron dependencias de paquetes ni un framework de interfaz.

## Base y acceso

Usa las tablas App_* que ya incluía la versión mejorada; no requiere una migración nueva. Se conserva la conexión LocalDB con Encrypt=False. No se cambiaron contraseñas, cuentas ni configuración de correo en esta actualización.

## Verificación

Pruebas de planificación: 28. Regresión de interfaz: 16. Presentador de entrenadores y navegación: 17. Total: 61.
Compilación Release y Debug. Diez consultas SQL nuevas comprobadas con ScriptDom. Capturas de las pantallas nuevas revisadas con datos ficticios.
Las pruebas no ejecutaron escrituras ni comprobaron transacciones sobre la LocalDB real, que no es accesible desde el entorno del agente. La validación de SQL es sintáctica, no una prueba de integración.

Para repetir pruebas: Tests/ProbarPlanificacion.ps1, Tests/ProbarInterfaz.ps1 y Tests/ProbarPatrones.ps1.
Para comprobar solo las consultas con la base local, sin modificar datos: powershell.exe -NoProfile -File "Tests/ComprobarPlanificacion.ps1" desde la carpeta del proyecto.
La prueba real pendiente consiste en crear una sesión futura de prueba y volver a intentar un horario superpuesto con el mismo profesor o espacio. El segundo intento debe rechazarse; después se puede cancelar la sesión de prueba.
