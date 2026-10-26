# Rediseño de interfaz — 22/10/2026

Se actualizó el proyecto de TPCAMPO. No se modificó la base, las credenciales, las reglas de negocio ni la configuración de LocalDB (Encrypt=False para esta instancia de pruebas).

## Qué cambia

- **Acceso:** campos con etiquetas separadas, contraseña oculta y opción para mostrarla, ingreso con Enter, validación en pantalla y estado de carga. El formulario se muestra antes de consultar SQL Server.
- **Inicio y navegación:** barra lateral con accesos a Inicio, Operación diaria, Socios, Entrenadores y Clases. Se mantienen los menús de seguridad y gestión. Las opciones respetan el perfil del usuario. Socios y Operación diaria reutilizan su ventana cuando ya está abierta.
- **Socios:** buscador por nombre, apellido o número; tabla y ficha de edición separadas; validación de nombres y nacimiento; fecha de nacimiento elegida explícitamente; confirmación de eliminación. Los registros asociados impiden una baja, conservando el historial. El acceso a historial y exportación conserva el formulario anterior y sus permisos.
- **Resumen:** indicadores de vencimientos, saldo, sesiones y lista de espera, con accesos directos a las tareas habituales.
- **Membresías:** tabla simplificada, creación de membresías y cobro en paneles separados. Seleccionar una membresía abre su panel de pago y muestra el socio, total, abonado y pendiente. El botón para completar el saldo evita copiar importes a mano.
- **Agenda:** paneles separados para reservar y programar, con fecha, hora y cupo. La tabla destaca estado, lugares disponibles y espera.
- **Reservas y asistencia:** acciones habilitadas según el estado y la fecha de la reserva. Las cancelaciones piden confirmación.
- **Pagos:** consulta de movimientos filtrada por socio.
- **Controles comunes:** tipografía Segoe UI, contraste claro, tablas legibles, botones consistentes, estados deshabilitados diferenciados y campos con nombres accesibles. Se aplicó el tema común a 20 formularios adicionales; esos formularios conservan su distribución original.

Los listados se cargan sin bloquear el hilo de la interfaz y las operaciones evitan doble clic mientras están en curso. Los botones principales permanecen al pie del panel, también en la ventana compacta. El panel permite desplazarse para ver los campos restantes.

## Cómo probar

Cerrar la aplicación y volver a abrir `CapaIU/bin/Release/Sistema Body Sport Demo.exe` desde la carpeta del proyecto. Ingresar con la cuenta que creó el administrador. No ejecutar otra vez los scripts de instalación: este cambio no requiere migraciones SQL.

En la pantalla nueva de socios, completar nombre y apellido, activar el selector de nacimiento y elegir la fecha correcta. Seleccionar una fila permite editarla; Nuevo socio limpia la ficha.

En Operación diaria, el filtro superior determina a qué socio se aplican las membresías, pagos y reservas. Ver todos vuelve al listado general. Para cobrar, seleccionar una membresía; para reservar, seleccionar un socio y una sesión. Los indicadores del resumen son generales.

## Verificación

Se compiló la solución y se aprobaron 16 pruebas de interfaz con datos ficticios, incluyendo validación del acceso, ocultamiento de contraseña, filtro de socios, selección explícita para cobrar, panel de pago, acciones protegidas y navegación principal. Se generaron y revisaron capturas del acceso, inicio, socios, resumen, membresías, pagos, agenda y asistencia, incluyendo una ventana compacta de 940 x 660 de área cliente.

Las vistas previas usan datos artificiales y no consultan ni modifican SQL. Se conservan las validaciones y autorizaciones de BLL para las operaciones reales. La conexión y los flujos completos con la base del usuario aún requieren una prueba en su entorno; las capturas no representan sus registros.

Para repetir las pruebas y generar vistas, ejecutar `Tests/ProbarInterfaz.ps1`. Requiere Visual Studio/MSBuild y Windows Forms. Los formularios de prueba se renderizan fuera del área visible con operaciones de escritura deshabilitadas; la conexión del proceso se reemplaza por una dirección inválida para impedir consultas accidentales.

No se tradujeron los nuevos textos a otros idiomas; están en español. La traducción de los formularios anteriores se mantiene. El tema es WinForms nativo y puede variar ligeramente según la escala y el tema de Windows.

Los formularios nuevos y el tema compartido se construyen en C# (Controles/TemaVisual.cs, FrmInicio.cs y Negocio/FrmSocios.cs, FrmOperacionDiaria.cs). La distribución del acceso está en FrmLogin.designer.cs. Los ajustes de estos formularios deben hacerse en código; el diseñador visual de Visual Studio no representa necesariamente esta construcción dinámica.
