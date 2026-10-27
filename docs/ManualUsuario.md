# Manual de uso - Body Sport Gym

## Acceso y seguridad

Usá una cuenta individual creada por el administrador. La contraseña inicial se entrega por un canal privado y debe cambiarse en el primer ingreso. Las claves nuevas requieren al menos 15 caracteres Unicode y admiten frases largas; no uses una contraseña compartida. Las credenciales ya guardadas siguen siendo compatibles.

Después de cinco fallos puede aparecer un bloqueo de 15 minutos en este proceso. No compartas cuentas. Cerrá sesión cuando termines. El bloqueo de la aplicación no sustituye la protección de Windows ni un servidor de autenticación.

Si olvidaste el usuario o la contraseña, usá las opciones de recuperación. La pantalla responde de forma genérica para no revelar si una cuenta existe. El envío requiere SMTP configurado por un administrador; la credencial de SMTP es distinta de la contraseña del gimnasio. El código de recuperación vence a los 15 minutos y es de un solo uso.

## Perfiles

Administración de cuentas permite crear usuarios con un perfil predefinido. Recepción gestiona socios, profesores, actividades, horarios, membresías, pagos, reservas y asistencia. Ambos perfiles pueden cambiar su propia contraseña. Los perfiles nuevos no incluyen restauración de respaldos ni administración arbitraria de permisos.

No habilites funciones técnicas solo para resolver un error de uso. Solicitá al responsable que revise el permiso y la configuración. Las asignaciones heredadas requieren la migración de perfiles descrita en PERFILES_Y_MULTIUSUARIO.md.

---

## Operación diaria

Cargá primero socios, profesores y actividades. Completá los datos necesarios para prestar el servicio y revisá los registros antes de guardar. Las búsquedas permiten filtrar y seleccionar una persona sin recorrer listados completos. No uses datos reales para probar nuevas versiones.

En planificación definí actividad, profesor, espacio, fecha y horario. Revisá la propuesta antes de guardar un lote. Los cruces de profesor o espacio se rechazan; un socio tampoco puede reservar sesiones superpuestas. Las sesiones consecutivas no se consideran superpuestas.

El calendario semanal muestra sesiones y participantes. Cambiá de semana para consultar disponibilidad. Revisá el detalle de la sesión antes de registrar asistencia, reservar o cancelar. Cancelar una sesión también cancela sus reservas; revisá el mensaje de confirmación.

Cuando se agotan los cupos, las nuevas reservas pasan a lista de espera. Al cancelar una reserva confirmada, se promueve al primer socio elegible. No repitas una operación si todavía está procesándose; ante un error, actualizá y comprobá el resultado antes de reintentar.

## Membresías y caja

Registrá una membresía con fechas, plan e importe. Luego registrá el pago, su medio y la referencia correspondiente. Los pagos no deben superar el saldo. Inicio y Saldos pendientes permiten revisar la deuda por socio sin multiplicar los pagos cuando existen varias membresías.

Verificá socio, período e importe antes de confirmar. Los movimientos y cambios se consultan en los historiales disponibles. Si un dato financiero está equivocado, solicitá una corrección al responsable; no modifiques tablas directamente.

---

## Exportaciones y datos personales

Los reportes y exportaciones pueden contener nombres, correos u otros datos de socios. Guardalos únicamente en ubicaciones aprobadas, con acceso limitado, y eliminá las copias cuando ya no sean necesarias. No subas bases, archivos exportados, capturas con datos reales ni respaldos a GitHub o a un chat público.

El reporte PDF trata los campos como texto, sin interpretar etiquetas HTML introducidas en nombres o descripciones. Aun así, el PDF resultante contiene los datos que seleccionaste: revisá destinatario y contenido antes de compartirlo.

Los ejemplos del repositorio usan direcciones reservadas de ejemplo. El correo de recuperación se envía solo a la dirección registrada, nunca a un destino alternativo ingresado al pedir la recuperación.

## Configuración y pruebas

La instalación de desarrollo usa LocalDB bajo una cuenta Windows de confianza. Para instalar, restaurá los paquetes NuGet, compilá y seguí README.md y LEEME.md. Los scripts de instalación rechazan sobrescribir una base existente. Respaldá antes de cualquier migración sobre datos de trabajo.

Los datos de demostración se crean en una base separada. Desde una terminal situada en la raíz del repositorio, el responsable puede ejecutar Database/Demo/PrepararPruebas.ps1. La cuenta de demostración recibe una contraseña elegida al preparar esa base; no existe una clave pública predeterminada.

Las conexiones SQL remotas requieren cifrado y validación del certificado. No desactives esa validación para resolver un error. La arquitectura actual tiene acceso directo a SQL: quien controla la cuenta propietaria de LocalDB puede superar las restricciones del cliente. Para operadores no confiables hace falta una frontera de servidor.

---

## Resolver problemas

Si el acceso falla, comprobá usuario y contraseña; no implica que el correo sea incorrecto ni que la cuenta exista. Si aparece un bloqueo temporal, esperá y solicitá asistencia si persiste.

Si una operación informa un conflicto, actualizá la consulta y revisá la agenda, los cupos o el saldo antes de reintentar. No repitas cobros sin verificar el movimiento anterior.

Los errores de conexión pueden indicar una instancia SQL detenida, una base sin migraciones, falta de permisos o un certificado inválido. Entregá al responsable la referencia del error y los pasos para reproducirlo, sin incluir contraseñas, tokens ni información de socios.

Los verificadores de integridad heredados detectan determinadas inconsistencias; no constituyen una firma criptográfica ni impiden que el propietario de SQL cambie la información. La reparación debe quedar a cargo de un responsable autorizado.

## Responsabilidades del administrador

Crear cuentas individuales, asignar el mínimo perfil necesario, proteger la configuración SMTP, mantener Windows y SQL Server actualizados y verificar restauraciones de respaldo en un entorno separado. Nunca distribuir una cuenta SQL con permisos de propietario a operadores que no sean de confianza.

Ante una credencial expuesta, revocarla o cambiarla en su proveedor. Quitarla del código no invalida copias ni el historial. Consultar AUDITORIA-SEGURIDAD.md, AUDITORIA-PRIVACIDAD.md y SECURITY.md antes de distribuir una versión.

Este manual no contiene rutas de perfiles Windows, cuentas operativas ni credenciales reales. La fuente editable es docs/ManualUsuario.md.
