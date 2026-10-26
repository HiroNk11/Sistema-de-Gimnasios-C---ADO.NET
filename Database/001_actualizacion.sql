/* Ejecutar en una COPIA de la base original, con respaldo previo.
   SQL Server 2016+; no modifica MyCompany por defecto ni crea la base.
   Las tablas App_* son independientes del circuito semanal heredado.
   Los horarios Inicio/Fin se interpretan en la zona local del gimnasio.
*/
SET XACT_ABORT ON;
SET NOCOUNT ON;
IF OBJECT_ID('dbo.Usuario','U') IS NULL OR OBJECT_ID('dbo.Socio','U') IS NULL OR OBJECT_ID('dbo.Clase','U') IS NULL
    THROW 51000,'Primero restaure una copia de la base original: faltan Usuario, Socio o Clase.',1;
IF COL_LENGTH('dbo.Usuario','Id_Usuario') IS NULL OR COL_LENGTH('dbo.Clase','Id_Entrenador') IS NULL OR COL_LENGTH('dbo.Clase','Id_Piso') IS NULL
    THROW 51000,'El esquema original no coincide con el código revisado.',1;
BEGIN TRANSACTION;
IF OBJECT_ID('dbo.Usuario_Credencial','U') IS NULL
CREATE TABLE dbo.Usuario_Credencial(
 Id_Usuario int NOT NULL CONSTRAINT PK_Usuario_Credencial PRIMARY KEY,
 PasswordHash nvarchar(256) NOT NULL,
 ActualizadoUtc datetime2 NOT NULL CONSTRAINT DF_Credencial_Fecha DEFAULT SYSUTCDATETIME(),
 CONSTRAINT FK_Credencial_Usuario FOREIGN KEY(Id_Usuario) REFERENCES dbo.Usuario(Id_Usuario) ON DELETE CASCADE
);
IF OBJECT_ID('dbo.Recuperacion_Acceso','U') IS NULL
BEGIN
 CREATE TABLE dbo.Recuperacion_Acceso(
 Id int IDENTITY NOT NULL CONSTRAINT PK_Recuperacion PRIMARY KEY,
 Usuario nvarchar(256) NOT NULL,TokenHash varchar(64) NOT NULL,
 ExpiraUtc datetime2 NOT NULL,Intentos int NOT NULL CONSTRAINT DF_Recuperacion_Intentos DEFAULT 0,
 Usado bit NOT NULL CONSTRAINT DF_Recuperacion_Usado DEFAULT 0,
 CreadoUtc datetime2 NOT NULL CONSTRAINT DF_Recuperacion_Fecha DEFAULT SYSUTCDATETIME()
 );
 CREATE INDEX IX_Recuperacion_Usuario ON dbo.Recuperacion_Acceso(Usuario,Usado,Id DESC);
END;
IF OBJECT_ID('dbo.App_Membresia','U') IS NULL
CREATE TABLE dbo.App_Membresia(
 Id int IDENTITY CONSTRAINT PK_App_Membresia PRIMARY KEY,Id_Socio int NOT NULL,
 [Plan] nvarchar(100) NOT NULL,Desde date NOT NULL,Hasta date NOT NULL,Importe decimal(18,2) NOT NULL,
 Id_Usuario int NOT NULL,CreadoUtc datetime2 NOT NULL CONSTRAINT DF_Membresia_Fecha DEFAULT SYSUTCDATETIME(),
 CONSTRAINT FK_Membresia_Socio FOREIGN KEY(Id_Socio) REFERENCES dbo.Socio(Id_Socio),
 CONSTRAINT CK_Membresia_Importe CHECK(Importe>=0),CONSTRAINT CK_Membresia_Fecha CHECK(Hasta>=Desde)
);
IF OBJECT_ID('dbo.App_Pago','U') IS NULL
CREATE TABLE dbo.App_Pago(
 Id int IDENTITY CONSTRAINT PK_App_Pago PRIMARY KEY,Id_Membresia int NOT NULL,
 Importe decimal(18,2) NOT NULL,Medio nvarchar(30) NOT NULL,Referencia nvarchar(100) NOT NULL,
 Id_Usuario int NOT NULL,CreadoUtc datetime2 NOT NULL CONSTRAINT DF_Pago_Fecha DEFAULT SYSUTCDATETIME(),
 CONSTRAINT FK_Pago_Membresia FOREIGN KEY(Id_Membresia) REFERENCES dbo.App_Membresia(Id),
 CONSTRAINT CK_Pago_Importe CHECK(Importe>0),
 CONSTRAINT CK_Pago_Medio CHECK(Medio IN('Efectivo','Transferencia','Tarjeta'))
);
IF OBJECT_ID('dbo.App_Sesion','U') IS NULL
CREATE TABLE dbo.App_Sesion(
 Id int IDENTITY CONSTRAINT PK_App_Sesion PRIMARY KEY,Id_Clase int NOT NULL,
 Inicio datetime2 NOT NULL,Fin datetime2 NOT NULL,Capacidad int NOT NULL,
 Estado varchar(20) NOT NULL CONSTRAINT DF_Sesion_Estado DEFAULT 'Programada',
 Id_Usuario int NOT NULL,CreadoUtc datetime2 NOT NULL CONSTRAINT DF_Sesion_Fecha DEFAULT SYSUTCDATETIME(),
 CONSTRAINT FK_Sesion_Clase FOREIGN KEY(Id_Clase) REFERENCES dbo.Clase(Id_Clase),
 CONSTRAINT CK_Sesion_Fecha CHECK(Fin>Inicio),CONSTRAINT CK_Sesion_Cupo CHECK(Capacidad BETWEEN 1 AND 1000),
 CONSTRAINT CK_Sesion_Estado CHECK(Estado IN('Programada','Cancelada'))
);
IF OBJECT_ID('dbo.App_Reserva','U') IS NULL
BEGIN
 CREATE TABLE dbo.App_Reserva(
 Id int IDENTITY CONSTRAINT PK_App_Reserva PRIMARY KEY,Id_Sesion int NOT NULL,Id_Socio int NOT NULL,
 Estado varchar(20) NOT NULL,Asistio bit NULL,
 Id_Usuario int NOT NULL,CreadoUtc datetime2 NOT NULL CONSTRAINT DF_Reserva_Fecha DEFAULT SYSUTCDATETIME(),
 ActualizadoUtc datetime2 NULL,
 CONSTRAINT FK_Reserva_Sesion FOREIGN KEY(Id_Sesion) REFERENCES dbo.App_Sesion(Id),
 CONSTRAINT FK_Reserva_Socio FOREIGN KEY(Id_Socio) REFERENCES dbo.Socio(Id_Socio),
 CONSTRAINT CK_Reserva_Estado CHECK(Estado IN('Confirmada','Espera','Cancelada'))
 );
 CREATE UNIQUE INDEX UX_Reserva_Activa ON dbo.App_Reserva(Id_Sesion,Id_Socio) WHERE Estado<>'Cancelada';
 CREATE INDEX IX_Reserva_Socio ON dbo.App_Reserva(Id_Socio,Estado,Id_Sesion);
END;
IF OBJECT_ID('dbo.App_Auditoria','U') IS NULL
CREATE TABLE dbo.App_Auditoria(
 Id bigint IDENTITY CONSTRAINT PK_App_Auditoria PRIMARY KEY,
 Tabla varchar(50) NOT NULL,Registro int NOT NULL,Operacion varchar(10) NOT NULL,
 Id_Usuario int NULL,FechaUtc datetime2 NOT NULL CONSTRAINT DF_Auditoria_Fecha DEFAULT SYSUTCDATETIME()
);
IF OBJECT_ID('dbo.Socio_Clase','U') IS NOT NULL AND COL_LENGTH('dbo.Socio_Clase','Precio') IS NOT NULL
BEGIN
 IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.Socio_Clase') AND name='Precio' AND is_nullable=1)
  ALTER TABLE dbo.Socio_Clase ALTER COLUMN Precio decimal(18,2) NULL;
 ELSE ALTER TABLE dbo.Socio_Clase ALTER COLUMN Precio decimal(18,2) NOT NULL;
END;
COMMIT;
GO
CREATE OR ALTER TRIGGER dbo.TR_App_Membresia_Auditoria ON dbo.App_Membresia AFTER INSERT,UPDATE,DELETE AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.App_Auditoria(Tabla,Registro,Operacion,Id_Usuario)
 SELECT 'Membresia',COALESCE(i.Id,d.Id),CASE WHEN i.Id IS NULL THEN 'DELETE' WHEN d.Id IS NULL THEN 'INSERT' ELSE 'UPDATE' END,COALESCE(i.Id_Usuario,d.Id_Usuario)
 FROM inserted i FULL JOIN deleted d ON d.Id=i.Id;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_App_Pago_Auditoria ON dbo.App_Pago AFTER INSERT,UPDATE,DELETE AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.App_Auditoria(Tabla,Registro,Operacion,Id_Usuario)
 SELECT 'Pago',COALESCE(i.Id,d.Id),CASE WHEN i.Id IS NULL THEN 'DELETE' WHEN d.Id IS NULL THEN 'INSERT' ELSE 'UPDATE' END,COALESCE(i.Id_Usuario,d.Id_Usuario)
 FROM inserted i FULL JOIN deleted d ON d.Id=i.Id;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_App_Sesion_Auditoria ON dbo.App_Sesion AFTER INSERT,UPDATE,DELETE AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.App_Auditoria(Tabla,Registro,Operacion,Id_Usuario)
 SELECT 'Sesion',COALESCE(i.Id,d.Id),CASE WHEN i.Id IS NULL THEN 'DELETE' WHEN d.Id IS NULL THEN 'INSERT' ELSE 'UPDATE' END,COALESCE(i.Id_Usuario,d.Id_Usuario)
 FROM inserted i FULL JOIN deleted d ON d.Id=i.Id;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_App_Reserva_Auditoria ON dbo.App_Reserva AFTER INSERT,UPDATE,DELETE AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.App_Auditoria(Tabla,Registro,Operacion,Id_Usuario)
 SELECT 'Reserva',COALESCE(i.Id,d.Id),CASE WHEN i.Id IS NULL THEN 'DELETE' WHEN d.Id IS NULL THEN 'INSERT' ELSE 'UPDATE' END,COALESCE(i.Id_Usuario,d.Id_Usuario)
 FROM inserted i FULL JOIN deleted d ON d.Id=i.Id;
END;
GO
PRINT 'Actualización completada. Las cuentas heredadas requieren restablecer la contraseña.';
