SET NOCOUNT ON;
IF DB_NAME() NOT LIKE N'DatabaseGym[_]Pruebas%' OR OBJECT_ID('dbo.Demo_Info','U') IS NULL
 THROW 51000,'Esta verificacion requiere la base de muestra.',1;
SELECT Version,FechaBase,CreadoUtc FROM dbo.Demo_Info;
SELECT Tipo,COUNT(*) AS Cantidad FROM dbo.Demo_Referencia GROUP BY Tipo ORDER BY Tipo;
SELECT 'Reservas confirmadas que superan cupo' AS Verificacion,COUNT(*) AS Problemas
FROM dbo.App_Sesion s WHERE (SELECT COUNT(*) FROM dbo.App_Reserva r WHERE r.Id_Sesion=s.Id AND r.Estado='Confirmada')>s.Capacidad
UNION ALL SELECT 'Pagos superiores al importe de membresia',COUNT(*) FROM dbo.App_Membresia m WHERE COALESCE((SELECT SUM(p.Importe) FROM dbo.App_Pago p WHERE p.Id_Membresia=m.Id),0)>m.Importe
UNION ALL SELECT 'Confirmadas de sesiones canceladas',COUNT(*) FROM dbo.App_Reserva r JOIN dbo.App_Sesion s ON s.Id=r.Id_Sesion WHERE r.Estado='Confirmada' AND s.Estado='Cancelada'
UNION ALL SELECT 'Cruces de profesor o espacio en sesiones activas',COUNT(*) FROM dbo.App_Sesion a JOIN dbo.App_Sesion b ON a.Id<b.Id AND a.Inicio<b.Fin AND a.Fin>b.Inicio JOIN dbo.Clase ca ON ca.Id_Clase=a.Id_Clase JOIN dbo.Clase cb ON cb.Id_Clase=b.Id_Clase WHERE a.Estado='Programada' AND b.Estado='Programada' AND (ca.Id_Entrenador=cb.Id_Entrenador OR ca.Id_Piso=cb.Id_Piso)
UNION ALL SELECT 'Cruces de reservas confirmadas de un socio',COUNT(*) FROM dbo.App_Reserva a JOIN dbo.App_Reserva b ON a.Id<b.Id AND a.Id_Socio=b.Id_Socio JOIN dbo.App_Sesion sa ON sa.Id=a.Id_Sesion JOIN dbo.App_Sesion sb ON sb.Id=b.Id_Sesion WHERE a.Estado='Confirmada' AND b.Estado='Confirmada' AND sa.Inicio<sb.Fin AND sa.Fin>sb.Inicio;
SELECT r.Codigo AS NroDemo,s.Id_Socio,s.Nombre,s.Apellido FROM dbo.Demo_Referencia r JOIN dbo.Socio s ON s.Id_Socio=r.Id WHERE r.Tipo='Socio' AND r.Codigo IN(1,2,3,4,6,71,81,101,111,121,131,141,150) ORDER BY r.Codigo;
SELECT r.Codigo AS NroDemo,s.Id,c.Descripcion,s.Inicio,s.Fin,s.Capacidad,s.Estado FROM dbo.Demo_Referencia r JOIN dbo.App_Sesion s ON s.Id=r.Id JOIN dbo.Clase c ON c.Id_Clase=s.Id_Clase WHERE r.Tipo='Sesion' ORDER BY r.Codigo;
