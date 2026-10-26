# Mejoras de mantenimiento e interfaz — 22/10/2026

Aplicadas sobre la copia TPCAMPO del escritorio. El correo queda pendiente.

## Uso

Cerrar la aplicación antes de abrir la versión actualizada desde CapaIU/bin/Release/Sistema Body Sport Demo.exe (o ejecutar Debug desde Visual Studio).
Entrenadores ahora incluye búsqueda por nombre, apellido y número; ficha de alta/edición; eliminación con confirmación; mensajes en pantalla y operaciones asíncronas.
La barra lateral marca la sección activa. Los menús reutilizan las ventanas abiertas.

## Organización

- Presentacion/NavegadorMdi.cs centraliza la apertura de formularios mediante fábricas. Evita duplicados y permite volver a abrir una ventana cerrada.
- Presentacion/EntrenadoresPresentador.cs contiene la búsqueda y coordinación asíncrona. Recibe IServicioEntrenadores por constructor para permitir pruebas con un servicio falso.
- ServicioEntrenadores adapta esa interfaz a EntrenadorBLL. No se accede al repositorio SQL desde el formulario.
- FrmEntrenadores se limita a controles, selección, confirmación y presentación de errores.
- EntrenadorBLL valida nombre, apellido, longitud e identificadores para proteger también a otros consumidores.

Este patrón se aplica primero a entrenadores; los otros formularios conservan su arquitectura actual. No se afirma haber convertido todo el sistema a MVP. Se mantiene el Composite de permisos y el Observer de idiomas existentes. La pantalla nueva usa textos en español, igual que los últimos formularios rediseñados; su traducción aún no está implementada.

## Limpieza y correcciones

Se retiró el antiguo FrmEntrenador (código, diseñador y recurso), reemplazado por FrmEntrenadores. Se guardó respaldo en el espacio de trabajo antes de retirarlo.
Se eliminaron manejadores sin uso, una comprobación de fechas vacía y un cálculo de dígitos que no se utilizaba antes de la reparación autorizada en BLL.
Los permisos usan directamente el identificador de la sesión en vez de cargar todos los usuarios y compararlos por nombre.
Se corrigió @nombre con espacio sobrante en Editar_Entrenador y se reutilizó AccesoRepository.Leer para liberar los recursos de búsqueda correctamente.
Se conservan los módulos históricos de clases porque aún usan sus propias tablas; retirarlos requiere resolver previamente la transición de datos.

## Validación

Release y Debug compilados. 17 pruebas nuevas y 16 de regresión de interfaz aprobadas. Captura de entrenadores revisada visualmente con datos ficticios.
Ejecutar Tests/ProbarPatrones.ps1 y Tests/ProbarInterfaz.ps1 para repetirlas con Visual Studio instalado.
No se ejecutaron escrituras ni pruebas de integración sobre la base real. Falta comprobar altas, ediciones y eliminación con la LocalDB del usuario.
No requiere una migración SQL nueva. Se conserva Encrypt=False para la conexión LocalDB.
