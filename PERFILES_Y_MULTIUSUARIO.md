# Perfiles y reglas de operación

Edición 26 de octubre de 2026

## Reglas acordadas

Administración de cuentas solo da de alta usuarios y asigna Recepción o Administración de cuentas durante el alta. No gestiona socios, cobros, planes, actividades, horarios ni reservas. No cambia permisos arbitrarios ni modifica cuentas existentes desde la aplicación.

Recepción registra y consulta socios, crea y renueva membresías, consulta saldos, registra pagos, gestiona profesores y tipos de actividad, crea y edita actividades, programa sesiones, inscribe, cancela reservas, atiende la lista de espera y registra asistencia. No crea usuarios ni administra permisos. No elimina físicamente socios ni profesores. Ninguno de estos perfiles recibe backup, restauración ni reparación de integridad.

Ambos pueden cambiar su propia clave y cerrar sesión. Cada persona debe tener una cuenta individual. Las cuentas nuevas requieren elegir un perfil y una clave inicial; deben cambiar esa clave antes de entrar a la operación. El alta no depende de SMTP. Se registra el autor del alta.

Los cobros no se editan ni se borran. La anulación, devolución o corrección compensatoria sigue pendiente de definir responsable y procedimiento; no está implementada ni se habilita a estos perfiles. No se creó una baja lógica de socios: por ahora se impide su eliminación física.

## Activación recomendada en la base de pruebas

Los archivos del proyecto ya están actualizados. La asignación de perfiles en LocalDB NO fue ejecutada desde el entorno del agente. Las cuentas antiguas conservan sus permisos hasta que se les asigna un perfil explícitamente.

1. Cerrá todas las ventanas del gimnasio, incluida la copia de pruebas.

2. El asistente siguiente actualiza los archivos de la copia de pruebas, conservando su conexión, datos y clave. Si detecta otra base o servidor, o la aplicación está abierta y bloquea archivos, se detiene antes de asignar permisos.

3. Prepará las familias y asigná a demo el perfil de Administración:

powershell.exe -NoProfile -File ".\Database\ConfigurarPerfil.ps1" -Usuario demo -Perfil Administracion -Base DatabaseGym_Pruebas

El asistente aplica 004_perfiles.sql y cambia únicamente los permisos de la cuenta indicada. No cambia su contraseña. Guarda sus asignaciones anteriores en App_CambioPerfil. No reasigna otras cuentas.

4. Abrí la aplicación de pruebas con AbrirPruebas.ps1, como indica el instalador. Ingresá con demo y su clave actual. Debe aparecer Alta de usuarios y no la operación del gimnasio.

5. Creá recepcion01 con nombre, apellido, correo válido y perfil Recepción. Elegí una clave inicial de 15 a 128 caracteres y repetila. Para pruebas se puede usar recepcion01@example.invalid; no se envían correos.

6. Cerrá la sesión de demo e ingresá con recepcion01. Debe pedir cambiar la clave inicial por otra diferente. Si se cancela, no deja entrar.

7. Confirmá que recepcion01 puede operar socios, membresías, pagos, actividades y horarios, y no puede abrir el alta de usuarios. El botón Eliminar no debe ofrecerse para socios o profesores.

8. Volvé a iniciar con demo. Debe poder crear cuentas y no entrar a los datos operativos.

Para usar otra base de muestra, indicá su nombre en ambos comandos. Si una cuenta recibe el perfil equivocado, el responsable técnico puede volver a ejecutar ConfigurarPerfil.ps1 con el perfil correcto. Esto no es una función otorgada a Recepción ni al administrador de cuentas dentro de la aplicación.

## Paso posterior a la base habitual

Después de comprobar los dos perfiles en la muestra, el responsable puede asignar a la cuenta existente Administración en DatabaseGym_Mejorado con el mismo asistente, cambiando -Usuario y -Base. Luego se crea la cuenta real de Recepción desde Alta de usuarios. No se asignaron automáticamente estos permisos a la cuenta existente para evitar alterar el acceso actual antes de la prueba.

No combines los perfiles nuevos con la familia Administrador antigua. El asistente sustituye las asignaciones de la cuenta por un único perfil, evitando conservar privilegios anteriores por accidente. Los usuarios y familias antiguos se mantienen para compatibilidad y soporte hasta que se los migre expresamente; no corresponden a los perfiles operativos nuevos.

## Una o dos computadoras

En una computadora, cada empleado usa su cuenta por turno y cierra sesión antes del siguiente. La sesión actual es por proceso de la aplicación. Se bloquea cambiar de usuario o cerrar una pantalla nueva mientras su operación está pendiente.

Para dos computadoras simultáneas se necesita una base central compartida. No se deben copiar bases independientes a cada puesto. La aplicación admite configurar la conexión, pero todavía no se instaló un servidor central, un servicio de aplicación ni las credenciales de red. La LocalDB actual continúa siendo un entorno local de desarrollo y pruebas.

Antes del despliegue en dos puestos hay que definir servidor y acceso, permisos de conexión, respaldos y manejo de caídas. Las validaciones de la aplicación no impiden que una persona con acceso directo de propietario a SQL altere la base. Para un límite de seguridad frente a clientes no confiables se necesita una capa de servicio que mantenga la autorización fuera del ejecutable de escritorio.

Las transacciones existentes protegen cupos y saldos; queda pendiente probar con dos procesos reales que dos intentos sobre el último lugar o el último saldo no excedan los límites. No se afirma que esa prueba de concurrencia ya haya sido realizada.

## Cambios de código

PuedeCrearUsuarios se agregó al final del enum, sin mover valores existentes. Las familias fijas APP Administracion de cuentas y APP Recepcion agrupan los permisos; las operaciones evalúan patentes, no el nombre del perfil. Para estos dos puestos se conserva un permiso común de negocio para Recepción y se separan altas de usuarios y eliminación física.

CuentasBLL valida datos y autorización. CuentasRepository guarda cuenta, hash moderno, marca de cambio inicial, familia y auditoría en una transacción. La asignación de perfil no es una segunda operación que pueda dejar cuentas incompletas.

Los métodos de lectura de negocio también exigen permiso; no se depende solo de ocultar menús. Se actualizaron los avisos de acceso denegado en las pantallas nuevas y se conserva el usuario autor al promover una reserva desde la lista de espera.

La migración agrega DebeCambiarClave con valor inicial cero a las credenciales existentes. El código de inicio y cambio de clave contempla bases que todavía no recibieron esa columna. Las nuevas cuentas creadas desde el formulario se marcan para cambiarla obligatoriamente.

## Validación y límites

Pruebas de alta e interfaz administrativa, regresiones de las pantallas existentes y validación sintáctica de SQL. La validación real de perfiles en LocalDB, la transacción de alta y el primer cambio obligatorio deben comprobarse con el recorrido descrito arriba. No se ejecutó la migración ni se alteraron usuarios de la base del usuario desde este entorno.

