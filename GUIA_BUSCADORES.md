# Búsqueda en selectores — 26/10/2026

En Operación diaria, pulsá **Buscar socio**. Escribí parte del nombre, apellido o número, elegí el resultado y pulsá Seleccionar. También podés usar flecha abajo y Enter. Escape o Cancelar conserva la selección anterior.

Los selectores de Actividades (tipo, profesor y espacio), Horarios (actividad) y otros campos creados con el tema visual tienen un botón **…** al lado para abrir el mismo buscador. F3 o Ctrl+F funcionan cuando el selector tiene el foco. Los selectores antiguos que reciben el tema compartido también tienen el atajo F3.

La búsqueda ignora mayúsculas y tildes, admite varias palabras en cualquier orden y muestra el número del registro para distinguir homónimos. Por ejemplo, `maria 42` permite distinguir dos socias llamadas María Gómez. Filtrar no modifica el origen de datos ni guarda información: la selección se confirma expresamente.

El componente reutilizable está en CapaIU/Controles/SelectorBuscable.cs. Trabaja con los elementos ya cargados en el selector; no consulta SQL por cada tecla. Conserva el índice e identificador del registro y notifica al filtro de Operación diaria cuando cambia el socio.

Pruebas: 15 del buscador con 100 socios ficticios, 16 de interfaz, 28 de planificación y 17 de presentador/navegación. Compilación Release y Debug. Captura del buscador y pantalla diaria revisadas. No se modificaron tablas, datos ni contraseñas.

Para repetir las pruebas del buscador: Tests/ProbarBuscador.ps1.

La copia del escritorio ya está actualizada. Cerrá y volvé a abrir la aplicación para cargar los cambios.
