/* SOLO para GymTests_. Requiere 000_esquema_original.sql ya aplicado. Datos artificiales. */
IF DB_NAME() NOT LIKE 'GymTests[_]%' THROW 51000,'Solo bases GymTests_.',1;
IF EXISTS(SELECT 1 FROM dbo.Usuario) OR EXISTS(SELECT 1 FROM dbo.Socio) THROW 51000,'Las tablas deben estar vacías.',1;
INSERT dbo.Idioma VALUES(1,N'Español',1);
INSERT dbo.Usuario VALUES(1,1,'admin','inutilizado','Administrador','Prueba','admin@example.invalid','');
SET IDENTITY_INSERT dbo.Entrenamiento ON;
INSERT dbo.Entrenamiento(Id_Entrenamiento,Nombre) VALUES(1,'Entrenamiento de prueba');
SET IDENTITY_INSERT dbo.Entrenamiento OFF;
SET IDENTITY_INSERT dbo.Entrenador ON;
INSERT dbo.Entrenador(Id_Entrenador,Nombre,Apellido) VALUES(1,'Profesor','Uno'),(2,'Profesor','Dos');
SET IDENTITY_INSERT dbo.Entrenador OFF;
SET IDENTITY_INSERT dbo.Piso ON;
INSERT dbo.Piso(Id_Piso,Nivel,Descripcion) VALUES(1,'1','Sala A'),(2,'2','Sala B');
SET IDENTITY_INSERT dbo.Piso OFF;
SET IDENTITY_INSERT dbo.Clase ON;
INSERT dbo.Clase(Id_Clase,Id_Entrenamiento,Id_Entrenador,Id_Piso,Descripcion,Capacidad) VALUES(1,1,1,1,'Clase A',2),(2,1,2,2,'Clase B',2);
SET IDENTITY_INSERT dbo.Clase OFF;
INSERT dbo.Digito_Verificador VALUES(1,'Usuario','80000');
SET IDENTITY_INSERT dbo.Permiso ON;
INSERT dbo.Permiso(Id_Permiso,Nombre,Permiso) VALUES(1,'Administrador',NULL),(2,'Negocio','PuedeAccederANegocio'),(3,'Backup','PuedeAccederBackup'),(4,'Restore','PuedeAccederRestore');
SET IDENTITY_INSERT dbo.Permiso OFF;
INSERT dbo.Usuario_Permiso VALUES(1,1);
INSERT dbo.Permiso_Permiso VALUES(1,2),(1,3),(1,4);
SET IDENTITY_INSERT dbo.Socio ON;
DECLARE @i int=1;
WHILE @i<=12 BEGIN INSERT dbo.Socio(Id_Socio,Nombre,Apellido,Fecha_Nacimiento) VALUES(@i,'Socio','Prueba','1990-01-01T00:00:00');SET @i+=1;END;
SET IDENTITY_INSERT dbo.Socio OFF;
INSERT dbo.Reserva(Id_Socio,Id_Clase,Fecha_Inicio,Fecha_Fin,Precio_Clase,Total) VALUES
(1,'1','Nov 24 2022 12:00AM','Dec  1 2022 12:00AM',100,200),
(2,'1','30/11/2022','07/12/2022',100,200);
