SET NOCOUNT ON;
SELECT DB_NAME() AS BaseSeleccionada;
SELECT name AS Tabla FROM sys.tables WHERE name LIKE 'App[_]%' OR name IN('Usuario_Credencial','Recuperacion_Acceso');
SELECT name, is_disabled FROM sys.triggers WHERE name LIKE 'TR_App_%';
SELECT 'Sesiones con sobrecupo' AS Problema,s.Id FROM dbo.App_Sesion s JOIN dbo.App_Reserva r ON r.Id_Sesion=s.Id AND r.Estado='Confirmada' GROUP BY s.Id,s.Capacidad HAVING COUNT(*)>s.Capacidad;
SELECT 'Pagos que superan la cuota' AS Problema,m.Id FROM dbo.App_Membresia m JOIN dbo.App_Pago p ON p.Id_Membresia=m.Id GROUP BY m.Id,m.Importe HAVING SUM(p.Importe)>m.Importe;
