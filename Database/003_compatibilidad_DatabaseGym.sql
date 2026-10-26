/* Adaptación al esquema real de scriptfinal.sql.
   Aplicar DESPUÉS de 001_actualizacion.sql en una copia de la base.
   No cambia de base, no borra usuarios ni asigna contraseñas predeterminadas.
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;
SET LANGUAGE us_english;
IF OBJECT_ID('dbo.Usuario_Credencial','U') IS NULL THROW 51000,'Aplique primero 001_actualizacion.sql.',1;
IF EXISTS(SELECT LoginName FROM dbo.Usuario GROUP BY LoginName HAVING COUNT(*)>1)
 THROW 51000,'Hay nombres de usuario duplicados. Corríjalos antes de migrar.',1;
IF EXISTS(SELECT Email FROM dbo.Usuario GROUP BY Email HAVING COUNT(*)>1)
 THROW 51000,'Hay correos duplicados. Corríjalos antes de migrar.',1;
IF EXISTS(SELECT 1 FROM dbo.Reserva WHERE TRY_CONVERT(int,Id_Clase) IS NULL)
 THROW 51000,'Reserva contiene clases que no se pueden convertir a un ID entero.',1;
IF EXISTS(SELECT 1 FROM dbo.Reserva WHERE (CASE WHEN CONVERT(nvarchar(50),Fecha_Inicio,126) LIKE '%/%' THEN TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),103) ELSE COALESCE(TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),126),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),121),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),0)) END) IS NULL
 OR (CASE WHEN CONVERT(nvarchar(50),Fecha_Fin,126) LIKE '%/%' THEN TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),103) ELSE COALESCE(TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),126),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),121),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),0)) END) IS NULL)
 THROW 51000,'Reserva contiene fechas inválidas. No se modificaron esos datos.',1;
BEGIN TRANSACTION;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.Usuario') AND name='Email' AND max_length<508)
 ALTER TABLE dbo.Usuario ALTER COLUMN Email nvarchar(254) NOT NULL;
ALTER TABLE dbo.Bitacora ALTER COLUMN Actividad nvarchar(512) NOT NULL;
IF COL_LENGTH('dbo.Bitacora','MomentoUtc') IS NULL ALTER TABLE dbo.Bitacora ADD MomentoUtc datetime2 NULL;
ALTER TABLE dbo.Reserva ALTER COLUMN Precio_Clase decimal(18,2) NOT NULL;
ALTER TABLE dbo.Reserva ALTER COLUMN Total decimal(18,2) NOT NULL;
ALTER TABLE dbo.Reserva ALTER COLUMN Id_Clase int NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.Reserva') AND name='Fecha_Inicio' AND system_type_id IN(167,175,231,239))
BEGIN
 UPDATE dbo.Reserva SET Fecha_Inicio=CONVERT(nvarchar(50),(CASE WHEN CONVERT(nvarchar(50),Fecha_Inicio,126) LIKE '%/%' THEN TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),103) ELSE COALESCE(TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),126),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),121),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Inicio,126),0)) END),126);
 ALTER TABLE dbo.Reserva ALTER COLUMN Fecha_Inicio datetime2 NOT NULL;
END;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.Reserva') AND name='Fecha_Fin' AND system_type_id IN(167,175,231,239))
BEGIN
 UPDATE dbo.Reserva SET Fecha_Fin=CONVERT(nvarchar(50),(CASE WHEN CONVERT(nvarchar(50),Fecha_Fin,126) LIKE '%/%' THEN TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),103) ELSE COALESCE(TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),126),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),121),TRY_CONVERT(datetime2,CONVERT(nvarchar(50),Fecha_Fin,126),0)) END),126);
 ALTER TABLE dbo.Reserva ALTER COLUMN Fecha_Fin datetime2 NOT NULL;
END;
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.Usuario') AND name='UX_Usuario_LoginName')
 CREATE UNIQUE INDEX UX_Usuario_LoginName ON dbo.Usuario(LoginName);
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.Usuario') AND name='UX_Usuario_Email')
 CREATE UNIQUE INDEX UX_Usuario_Email ON dbo.Usuario(Email);
DECLARE @next bigint,@ddl nvarchar(max);
IF OBJECT_ID('dbo.Seq_Usuario','SO') IS NULL
BEGIN
 SELECT @next=COALESCE(MAX(CONVERT(bigint,Id_Usuario)),0)+1 FROM dbo.Usuario;
 IF @next>2147483647 THROW 51000,'No hay más IDs de usuario disponibles.',1;
 SET @ddl=N'CREATE SEQUENCE dbo.Seq_Usuario AS int START WITH '+CONVERT(nvarchar(20),@next)+N' INCREMENT BY 1 NO CYCLE';
 EXEC sys.sp_executesql @ddl;
END;
IF OBJECT_ID('dbo.Seq_Idioma','SO') IS NULL
BEGIN
 SELECT @next=COALESCE(MAX(CONVERT(bigint,Id_Idioma)),0)+1 FROM dbo.Idioma;
 IF @next>2147483647 THROW 51000,'No hay más IDs de idioma disponibles.',1;
 SET @ddl=N'CREATE SEQUENCE dbo.Seq_Idioma AS int START WITH '+CONVERT(nvarchar(20),@next)+N' INCREMENT BY 1 NO CYCLE';
 EXEC sys.sp_executesql @ddl;
END;
DECLARE @padre int=(SELECT MIN(Id_Permiso) FROM dbo.Permiso WHERE Nombre=N'Administrador' AND Permiso IS NULL);
DECLARE @nuevos TABLE(Nombre nvarchar(50),Permiso nvarchar(50));
INSERT @nuevos VALUES(N'Reparar verificadores',N'PuedeRestaurardv'),(N'Exportar socios',N'PuedeSerializar'),(N'Ver historial de socios',N'PuedeVerhistorialCambios');
INSERT dbo.Permiso(Nombre,Permiso) SELECT n.Nombre,n.Permiso FROM @nuevos n WHERE NOT EXISTS(SELECT 1 FROM dbo.Permiso p WHERE p.Permiso=n.Permiso);
IF @padre IS NOT NULL
 INSERT dbo.Permiso_Permiso(id_permiso_padre,id_permiso_hijo)
 SELECT @padre,p.Id_Permiso FROM dbo.Permiso p JOIN @nuevos n ON n.Permiso=p.Permiso
 WHERE NOT EXISTS(SELECT 1 FROM dbo.Permiso_Permiso pp WHERE pp.id_permiso_padre=@padre AND pp.id_permiso_hijo=p.Id_Permiso);
UPDATE dbo.Digito_Verificador SET DVV=CONVERT(nvarchar(50),(SELECT COUNT_BIG(*) FROM dbo.Usuario)*80000) WHERE IdTabla=1;
COMMIT;
GO
CREATE OR ALTER PROCEDURE dbo.Insertar_Usuario
 @LoginName nvarchar(50),@Password nvarchar(50),@FirstName nvarchar(50),@LastName nvarchar(50),@Email nvarchar(254),@Dvh nvarchar(50)
AS
BEGIN
 SET NOCOUNT ON;
 DECLARE @idioma int=(SELECT TOP(1) Id_Idioma FROM dbo.Idioma ORDER BY Idioma_Default DESC,Id_Idioma);
 IF @idioma IS NULL THROW 51000,'Debe existir al menos un idioma.',1;
 INSERT dbo.Usuario(Id_Usuario,Id_Idioma,LoginName,Password,FirstName,LastName,Email,DVH)
 VALUES(NEXT VALUE FOR dbo.Seq_Usuario,@idioma,@LoginName,@Password,@FirstName,@LastName,@Email,@Dvh);
END;
GO
CREATE OR ALTER PROCEDURE dbo.Insertar_Idioma @nombre nvarchar(50),@default bit
AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.Idioma(Id_Idioma,Nombre,Idioma_Default) VALUES(NEXT VALUE FOR dbo.Seq_Idioma,@nombre,@default);
END;
GO
CREATE OR ALTER PROCEDURE dbo.Editar_Usuario
 @NombreUsuario nvarchar(50),@Nombre nvarchar(50),@Apellido nvarchar(50),@Email nvarchar(254),@id int
AS
BEGIN
 SET NOCOUNT ON;
 UPDATE dbo.Usuario SET LoginName=@NombreUsuario,FirstName=@Nombre,LastName=@Apellido,Email=@Email WHERE Id_Usuario=@id;
 IF @@ROWCOUNT<>1 THROW 51000,'El usuario no existe.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.Search_User @usu nvarchar(50)
AS
 SELECT Id_Usuario,Id_Idioma,LoginName,Password FROM dbo.Usuario WHERE LoginName=@usu;
GO
CREATE OR ALTER PROCEDURE dbo.Search_EmailUser @em nvarchar(254)
AS SELECT Email FROM dbo.Usuario WHERE Email=@em;
GO
CREATE OR ALTER PROCEDURE dbo.Listar_Usuario
AS SELECT Id_Usuario,Id_Idioma,LoginName,FirstName,LastName,Email FROM dbo.Usuario ORDER BY Id_Usuario;
GO
CREATE OR ALTER PROCEDURE dbo.Grabar_Bitacora
 @Id_Usuario int,@Actividad nvarchar(512),@Criticidad nvarchar(50),@Fecha nvarchar(50),@Hora nvarchar(50),@MomentoUtc datetime2=NULL
AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.Bitacora(Id_Usuario,Actividad,Criticidad,Fecha,Hora,MomentoUtc)
 VALUES(@Id_Usuario,@Actividad,@Criticidad,@Fecha,@Hora,COALESCE(@MomentoUtc,SYSUTCDATETIME()));
END;
GO
CREATE OR ALTER PROCEDURE dbo.Insertar_Reserva
 @ids int,@idc int,@fi datetime2,@ff datetime2,@pc decimal(18,2),@t decimal(18,2)
AS
BEGIN
 SET NOCOUNT ON;
 IF @ff<=@fi OR @pc<0 OR @t<0 THROW 51000,'Fechas o importes inválidos.',1;
 INSERT dbo.Reserva(Id_Socio,Id_Clase,Fecha_Inicio,Fecha_Fin,Precio_Clase,Total) VALUES(@ids,@idc,@fi,@ff,@pc,@t);
END;
GO
CREATE OR ALTER PROCEDURE dbo.Insertar_Socio_Clase
 @ids int,@idp int,@idc int,@p decimal(18,2),@cd int,@fi datetime,@ff datetime
AS
BEGIN
 SET NOCOUNT ON;SET XACT_ABORT ON;
 IF @p<0 OR @cd<1 OR @ff<=@fi THROW 51000,'Inscripción inválida.',1;
 BEGIN TRANSACTION;
 UPDATE dbo.Clase WITH(UPDLOCK,HOLDLOCK) SET Capacidad=Capacidad-1 WHERE Id_Clase=@idc AND Id_Piso=@idp AND Capacidad>0;
 IF @@ROWCOUNT<>1 BEGIN ROLLBACK;THROW 51000,'La clase no tiene cupo.',1;END;
 IF EXISTS(SELECT 1 FROM dbo.Socio_Clase WITH(UPDLOCK,HOLDLOCK) WHERE Id_Clase=@idc AND Id_Socio=@ids AND Fecha_Inicio<@ff AND Fecha_Fin>@fi)
 BEGIN ROLLBACK;THROW 51000,'El socio ya tiene una inscripción vigente.',1;END;
 INSERT dbo.Socio_Clase(Id_Socio,Id_Piso,Id_Clase,Precio,Cant_Dias,Fecha_Inicio,Fecha_Fin) VALUES(@ids,@idp,@idc,@p,@cd,@fi,@ff);
 COMMIT;
END;
GO
CREATE OR ALTER PROCEDURE dbo.Eliminar_Sc @fecha datetime
AS THROW 51000,'La eliminación masiva por fecha fue deshabilitada. Use la cancelación por ID.',1;
GO
CREATE OR ALTER PROCEDURE dbo.Editar_Traduccion @Nombre nvarchar(50),@id int,@Trad nvarchar(50)
AS
BEGIN
 SET NOCOUNT ON;
 UPDATE t SET Traduccion=@Trad FROM dbo.Traduccion t JOIN dbo.Idioma i ON i.Id_Idioma=t.Id_Idioma WHERE i.Nombre=@Nombre AND t.Id_Etiqueta=@id;
END;
GO
CREATE OR ALTER TRIGGER dbo.trigger_update_socio ON dbo.Socio AFTER UPDATE NOT FOR REPLICATION
AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.Bitacora_Socio(Id_Socio,Nombre,Apellido,Fecha_Nacimiento,Descripcion_Cambio,Fecha_Cambio)
 SELECT Id_Socio,Nombre,Apellido,CONVERT(nvarchar(50),Fecha_Nacimiento,126),'OLD DATA',CONVERT(nvarchar(50),GETDATE(),126) FROM deleted;
 INSERT dbo.Bitacora_Socio(Id_Socio,Nombre,Apellido,Fecha_Nacimiento,Descripcion_Cambio,Fecha_Cambio)
 SELECT Id_Socio,Nombre,Apellido,CONVERT(nvarchar(50),Fecha_Nacimiento,126),'NEW DATA',CONVERT(nvarchar(50),GETDATE(),126) FROM inserted;
END;
GO
/* El login antiguo no admite el nuevo formato de contraseñas. No debe seguir activo. */
CREATE OR ALTER PROCEDURE dbo.sp_login @em nvarchar(254),@pas nvarchar(50)
AS THROW 51000,'Utilice la autenticación PBKDF2 de la aplicación actualizada.',1;
GO
CREATE OR ALTER PROCEDURE dbo.update_password @user nvarchar(50),@pass nvarchar(50)
AS THROW 51000,'Utilice el cambio autenticado o la recuperación con código de la aplicación.',1;
GO
PRINT 'Compatibilidad con DatabaseGym aplicada. Backup y restore mantienen permisos separados.';
