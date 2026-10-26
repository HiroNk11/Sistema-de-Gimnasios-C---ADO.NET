# Muestra de prueba de Body Sport Gym

Preparada el 26/10/2026. La carga en LocalDB debe ejecutarse desde el PowerShell del usuario. No se ejecutó desde el entorno del asistente.

## Cargar en el proyecto actual

powershell.exe -NoProfile -File ".\Database\Demo\PrepararPruebas.ps1"

El asistente crea DatabaseGym_Pruebas, solicita una contraseña nueva para demo y deja una copia ejecutable en EntornosPrueba/DatabaseGym_Pruebas. Al final imprime el comando para abrirla. No cambia DatabaseGym_Mejorado, la cuenta existente ni la configuración habitual. No envía correos.

Si el proyecto está en otra carpeta, indicá -Proyecto con su ruta. Si preferís otra base nueva, agregá -Base DatabaseGym_Pruebas_02. Volver a usar una base creada por el asistente conserva los ejercicios; no duplica ni reinicia los datos. Si existe una base del mismo nombre que no pertenece al asistente, se rechaza su uso.

## Datos iniciales

150 socios, 12 profesores, 4 espacios, 8 actividades, 140 membresías, 120 pagos, 17 sesiones y 7 reservas. Hay homónimos, tildes, apellidos compuestos y largos, planes pagados/parciales/impagos/vencidos/futuros/sin cargo, socios sin plan, lista de espera y asistencias.

Las fechas son relativas al día de carga. Para repetir pruebas de futuro días después, usá una base nueva. SociosDemo.csv se genera al instalar y relaciona los códigos de muestra con los IDs reales.

## Revisar inconsistencias

powershell.exe -NoProfile -File ".\Database\Demo\ComprobarPruebas.ps1"

La consulta comprueba sobrecupos, sobrepagos, confirmadas de sesiones canceladas y cruces de recursos o de socios. No modifica datos. Para otra base indicá -Base con el mismo nombre usado en la carga. No reemplaza las pruebas de uso ni certifica todos los circuitos.

PlanDePruebas.csv contiene 24 ejercicios y columnas para resultado obtenido, estado y observaciones. Consultá el Manual de usuario PDF para los pasos de cada módulo. F1 abre el manual instalado junto al programa actualizado.

## Validación realizada

Sintaxis de los scripts PowerShell y de los dos SQL comprobada. Verificadas cantidades, límites de nombres, homónimos, apóstrofos, protección de base, transacción y CSV de 24 casos. Compilación del enlace F1 en Release y Debug. Manual de 14 páginas renderizado y revisado visualmente.

Pendiente: instalar la muestra y ejecutar las consultas y ejercicios contra la LocalDB del usuario. No se afirma haber probado escrituras ni concurrencia real con estos datos.

