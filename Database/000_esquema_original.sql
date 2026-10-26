/* Esquema extraído de scriptfinal.sql. Sin USE y sin datos personales.
   Ejecutar SOLO en una base nueva vacía, antes de las migraciones.
   Se conserva el modelo original; se omite una opción de índices desactivada para compatibilidad con SQL Server 2016 SP1+. */
IF EXISTS(SELECT 1 FROM sys.tables WHERE is_ms_shipped=0) THROW 51000,'La base debe estar vacía para crear el esquema original.',1;
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](

	[Id_Bitacora] [int] IDENTITY(1,1) NOT NULL,

	[Id_Usuario] [int] NOT NULL,

	[Actividad] [nvarchar](50) NOT NULL,

	[Criticidad] [nvarchar](50) NULL,

	[Fecha] [nvarchar](50) NOT NULL,

	[Hora] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 

(

	[Id_Bitacora] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bitacora_Socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora_Socio](

	[Id_Control] [int] IDENTITY(1,1) NOT NULL,

	[Id_Socio] [int] NULL,

	[Nombre] [nvarchar](50) NULL,

	[Apellido] [nvarchar](50) NULL,

	[Fecha_Nacimiento] [nvarchar](50) NULL,

	[Descripcion_Cambio] [nvarchar](50) NULL,

	[Fecha_Cambio] [nvarchar](50) NULL,

 CONSTRAINT [PK_Bitacora_Socio] PRIMARY KEY CLUSTERED 

(

	[Id_Control] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clase]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clase](

	[Id_Clase] [int] IDENTITY(1,1) NOT NULL,

	[Id_Entrenamiento] [int] NOT NULL,

	[Id_Entrenador] [int] NOT NULL,

	[Id_Piso] [int] NOT NULL,

	[Descripcion] [nvarchar](50) NOT NULL,

	[Capacidad] [int] NOT NULL,

 CONSTRAINT [PK_Clase] PRIMARY KEY CLUSTERED 

(

	[Id_Clase] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Día]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Día](

	[Id_Dia] [int] IDENTITY(1,1) NOT NULL,

	[Nombre] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_Día] PRIMARY KEY CLUSTERED 

(

	[Id_Dia] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Digito_Verificador]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Digito_Verificador](

	[IdTabla] [int] NOT NULL,

	[Nombre_Tabla] [nvarchar](50) NOT NULL,

	[DVV] [nvarchar](max) NOT NULL,

 CONSTRAINT [PK_Digito_Verificador] PRIMARY KEY CLUSTERED 

(

	[IdTabla] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entrenador]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrenador](

	[Id_Entrenador] [int] IDENTITY(1,1) NOT NULL,

	[Nombre] [nvarchar](50) NULL,

	[Apellido] [nvarchar](50) NULL,

 CONSTRAINT [PK_Entrenador] PRIMARY KEY CLUSTERED 

(

	[Id_Entrenador] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entrenamiento]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrenamiento](

	[Id_Entrenamiento] [int] IDENTITY(1,1) NOT NULL,

	[Nombre] [nvarchar](50) NULL,

 CONSTRAINT [PK_Entrenamiento] PRIMARY KEY CLUSTERED 

(

	[Id_Entrenamiento] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etiqueta]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etiqueta](

	[Id_Etiqueta] [int] NOT NULL,

	[Nombre] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_Etiqueta] PRIMARY KEY CLUSTERED 

(

	[Id_Etiqueta] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horario](

	[Id_Horario] [int] IDENTITY(1,1) NOT NULL,

	[Id_Clase] [int] NOT NULL,

	[Id_Dia] [int] NOT NULL,

	[Horario] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_Horario] PRIMARY KEY CLUSTERED 

(

	[Id_Horario] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idioma]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idioma](

	[Id_Idioma] [int] NOT NULL,

	[Nombre] [nvarchar](50) NOT NULL,

	[Idioma_Default] [bit] NOT NULL,

 CONSTRAINT [PK_Idioma] PRIMARY KEY CLUSTERED 

(

	[Id_Idioma] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](

	[Id_Permiso] [int] IDENTITY(1,1) NOT NULL,

	[Nombre] [nvarchar](50) NULL,

	[Permiso] [nvarchar](50) NULL,

 CONSTRAINT [PK_permiso_1] PRIMARY KEY CLUSTERED 

(

	[Id_Permiso] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso_Permiso]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso_Permiso](

	[id_permiso_padre] [int] NULL,

	[id_permiso_hijo] [int] NULL

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Piso]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Piso](

	[Id_Piso] [int] IDENTITY(1,1) NOT NULL,

	[Nivel] [nvarchar](50) NOT NULL,

	[Descripcion] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_Piso] PRIMARY KEY CLUSTERED 

(

	[Id_Piso] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](

	[Id_Reserva] [int] IDENTITY(1,1) NOT NULL,

	[Id_Socio] [int] NOT NULL,

	[Id_Clase] [nchar](10) NOT NULL,

	[Fecha_Inicio] [nvarchar](50) NOT NULL,

	[Fecha_Fin] [nvarchar](50) NOT NULL,

	[Precio_Clase] [int] NOT NULL,

	[Total] [int] NOT NULL,

 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 

(

	[Id_Reserva] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Socio](

	[Id_Socio] [int] IDENTITY(1,1) NOT NULL,

	[Nombre] [nvarchar](50) NOT NULL,

	[Apellido] [nvarchar](50) NOT NULL,

	[Fecha_Nacimiento] [datetime] NOT NULL,

 CONSTRAINT [PK_Socio] PRIMARY KEY CLUSTERED 

(

	[Id_Socio] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Socio_Clase]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Socio_Clase](

	[Id] [int] IDENTITY(1,1) NOT NULL,

	[Id_Socio] [int] NULL,

	[Id_Piso] [int] NULL,

	[Id_Clase] [int] NULL,

	[Precio] [int] NULL,

	[Cant_Dias] [int] NULL,

	[Fecha_Inicio] [datetime] NULL,

	[Fecha_Fin] [datetime] NULL,

 CONSTRAINT [PK_Alumno_Clase] PRIMARY KEY CLUSTERED 

(

	[Id] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Traduccion]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Traduccion](

	[Id_Traduccion] [int] IDENTITY(1,1) NOT NULL,

	[Id_Idioma] [int] NOT NULL,

	[Id_Etiqueta] [int] NOT NULL,

	[Traduccion] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_Traduccion] PRIMARY KEY CLUSTERED 

(

	[Id_Traduccion] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](

	[Id_Usuario] [int] NOT NULL,

	[Id_Idioma] [int] NULL,

	[LoginName] [nvarchar](50) NOT NULL,

	[Password] [nvarchar](50) NOT NULL,

	[FirstName] [nvarchar](50) NOT NULL,

	[LastName] [nvarchar](50) NOT NULL,

	[Email] [nvarchar](50) NOT NULL,

	[DVH] [nvarchar](50) NULL,

 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 

(

	[Id_Usuario] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario_Permiso]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Permiso](

	[Id_Usuario] [int] NOT NULL,

	[Id_Permiso] [int] NOT NULL,

 CONSTRAINT [PK_Usuario_Permiso] PRIMARY KEY CLUSTERED 

(

	[Id_Usuario] ASC,

	[Id_Permiso] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clase]  WITH CHECK ADD  CONSTRAINT [FK_Clase_Entrenador] FOREIGN KEY([Id_Entrenador])

REFERENCES [dbo].[Entrenador] ([Id_Entrenador])
GO
ALTER TABLE [dbo].[Clase] CHECK CONSTRAINT [FK_Clase_Entrenador]
GO
ALTER TABLE [dbo].[Clase]  WITH CHECK ADD  CONSTRAINT [FK_Clase_Entrenamiento] FOREIGN KEY([Id_Entrenamiento])

REFERENCES [dbo].[Entrenamiento] ([Id_Entrenamiento])
GO
ALTER TABLE [dbo].[Clase] CHECK CONSTRAINT [FK_Clase_Entrenamiento]
GO
ALTER TABLE [dbo].[Clase]  WITH CHECK ADD  CONSTRAINT [FK_Clase_Piso] FOREIGN KEY([Id_Piso])

REFERENCES [dbo].[Piso] ([Id_Piso])
GO
ALTER TABLE [dbo].[Clase] CHECK CONSTRAINT [FK_Clase_Piso]
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD  CONSTRAINT [FK_Horario_Clase] FOREIGN KEY([Id_Clase])

REFERENCES [dbo].[Clase] ([Id_Clase])
GO
ALTER TABLE [dbo].[Horario] CHECK CONSTRAINT [FK_Horario_Clase]
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD  CONSTRAINT [FK_Horario_Día] FOREIGN KEY([Id_Dia])

REFERENCES [dbo].[Día] ([Id_Dia])
GO
ALTER TABLE [dbo].[Horario] CHECK CONSTRAINT [FK_Horario_Día]
GO
ALTER TABLE [dbo].[Permiso_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_permiso_permiso_Permiso] FOREIGN KEY([id_permiso_padre])

REFERENCES [dbo].[Permiso] ([Id_Permiso])
GO
ALTER TABLE [dbo].[Permiso_Permiso] CHECK CONSTRAINT [FK_permiso_permiso_Permiso]
GO
ALTER TABLE [dbo].[Socio_Clase]  WITH CHECK ADD  CONSTRAINT [FK_Alumno_Clase_Clase] FOREIGN KEY([Id_Clase])

REFERENCES [dbo].[Clase] ([Id_Clase])
GO
ALTER TABLE [dbo].[Socio_Clase] CHECK CONSTRAINT [FK_Alumno_Clase_Clase]
GO
ALTER TABLE [dbo].[Socio_Clase]  WITH CHECK ADD  CONSTRAINT [FK_Alumno_Clase_Piso] FOREIGN KEY([Id_Piso])

REFERENCES [dbo].[Piso] ([Id_Piso])
GO
ALTER TABLE [dbo].[Socio_Clase] CHECK CONSTRAINT [FK_Alumno_Clase_Piso]
GO
ALTER TABLE [dbo].[Socio_Clase]  WITH CHECK ADD  CONSTRAINT [FK_Alumno_Clase_Socio] FOREIGN KEY([Id_Socio])

REFERENCES [dbo].[Socio] ([Id_Socio])
GO
ALTER TABLE [dbo].[Socio_Clase] CHECK CONSTRAINT [FK_Alumno_Clase_Socio]
GO
ALTER TABLE [dbo].[Traduccion]  WITH CHECK ADD  CONSTRAINT [FK_Traduccion_Etiqueta] FOREIGN KEY([Id_Etiqueta])

REFERENCES [dbo].[Etiqueta] ([Id_Etiqueta])
GO
ALTER TABLE [dbo].[Traduccion] CHECK CONSTRAINT [FK_Traduccion_Etiqueta]
GO
ALTER TABLE [dbo].[Traduccion]  WITH CHECK ADD  CONSTRAINT [FK_Traduccion_Idioma] FOREIGN KEY([Id_Idioma])

REFERENCES [dbo].[Idioma] ([Id_Idioma])
GO
ALTER TABLE [dbo].[Traduccion] CHECK CONSTRAINT [FK_Traduccion_Idioma]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Idioma] FOREIGN KEY([Id_Idioma])

REFERENCES [dbo].[Idioma] ([Id_Idioma])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Idioma]
GO
ALTER TABLE [dbo].[Usuario_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Permiso_Permiso] FOREIGN KEY([Id_Permiso])

REFERENCES [dbo].[Permiso] ([Id_Permiso])
GO
ALTER TABLE [dbo].[Usuario_Permiso] CHECK CONSTRAINT [FK_Usuario_Permiso_Permiso]
GO
ALTER TABLE [dbo].[Usuario_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Permiso_Users] FOREIGN KEY([Id_Usuario])

REFERENCES [dbo].[Usuario] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Usuario_Permiso] CHECK CONSTRAINT [FK_Usuario_Permiso_Users]
GO
/****** Object:  StoredProcedure [dbo].[Borrar_Entrenador]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Borrar_Entrenador]

@eId int

as

begin

delete from Entrenador

where Id_Entrenador = @eId

end
GO
/****** Object:  StoredProcedure [dbo].[Borrar_Entrenamiento]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Borrar_Entrenamiento]

@eId int

as

begin

delete from Entrenamiento

where Id_Entrenamiento = @eId

end
GO
/****** Object:  StoredProcedure [dbo].[Borrar_Permisos_Usuario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Borrar_Permisos_Usuario]

@id int

as

begin

delete from Usuario_Permiso

where Id_Usuario = @id

end
GO
/****** Object:  StoredProcedure [dbo].[Borrar_Socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Borrar_Socio]

@sId int

as

begin 

Delete from Socio where Id_Socio = @sId

end
GO
/****** Object:  StoredProcedure [dbo].[Borrar_Usuario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Borrar_Usuario]

@UserId int

as

begin 

Delete from Usuario where Id_Usuario = @UserId

end
GO
/****** Object:  StoredProcedure [dbo].[Buscar_Entrenador]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Buscar_Entrenador]

@nombre nvarchar(50)

as

begin

select Id_Entrenador,Nombre,Apellido from Entrenador where Nombre like @nombre + '%'

end
GO
/****** Object:  StoredProcedure [dbo].[Buscar_Entrenamiento]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Buscar_Entrenamiento]

@nombre nvarchar(50)

as

begin

select Id_Entrenamiento,Nombre from Entrenamiento where Nombre like @nombre + '%'

end
GO
/****** Object:  StoredProcedure [dbo].[Buscar_Socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Buscar_Socio]

@nombre nvarchar(50)

as

begin

select * from Socio where Nombre like @nombre + '%'

end
GO
/****** Object:  StoredProcedure [dbo].[Buscar_User]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Buscar_User]

@l nvarchar(50)

as

begin

select Id_Usuario,LoginName,FirstName,LastName,Email from Usuario where LoginName like @l + '%'

end
GO
/****** Object:  StoredProcedure [dbo].[Buscar_Usuario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Buscar_Usuario]

@nombre nvarchar(50)

as

begin

select * from Usuario where LoginName like @nombre + '%'

end
GO
/****** Object:  StoredProcedure [dbo].[Change_Password]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Change_Password]

@user nvarchar(50)

as

begin

select Password from Usuario

where LoginName = @user

end
GO
/****** Object:  StoredProcedure [dbo].[crit_bitacora]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[crit_bitacora]

@c nvarchar(50)

as

begin

select LoginName,Actividad,Criticidad,Fecha,Hora from Bitacora bita inner join Usuario usu on bita.Id_Usuario = usu.Id_Usuario where Criticidad=@c

end
GO
/****** Object:  StoredProcedure [dbo].[Editar_Capacidad]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Editar_Capacidad]

@cap int,@idclase int

as

begin

update Clase

set Capacidad = @cap

where Id_Clase = @idclase

end
GO
/****** Object:  StoredProcedure [dbo].[Editar_Clase]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Editar_Clase]

@ide int,  @ident int,@idp int,@d nvarchar(50),@Cap int,@idc int

as

begin 

Update Clase 

set Id_Entrenamiento = @ide,Id_Entrenador = @ident, Id_Piso = @idp,Descripcion = @d,Capacidad=@Cap

where Id_Clase = @idc

end
GO
/****** Object:  StoredProcedure [dbo].[Editar_Entrenador]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Editar_Entrenador]

@nombre nvarchar(50),@apellido nvarchar(50),@eId int

as

begin

update Entrenador set Nombre=@nombre, Apellido = @apellido 

where Id_Entrenador = @eId

end
GO
/****** Object:  StoredProcedure [dbo].[Editar_Entrenamiento]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Editar_Entrenamiento]

@Nombre nvarchar(50),@Ide int

as

begin 

update Entrenamiento set Nombre=@Nombre

where Id_Entrenamiento = @Ide

end
GO
/****** Object:  StoredProcedure [dbo].[Editar_Horario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Editar_Horario]

@dia int,@horario nvarchar(50),@idclase int,@nd int,@nh nvarchar(50)

as

begin

update Horario 

set Id_Dia = @nd,Horario =@nh

where Id_Clase = @idclase and Id_Dia = @dia and Horario = @horario

end
GO
/****** Object:  StoredProcedure [dbo].[Editar_Socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Editar_Socio]

@Nombre nvarchar(50),@Apellido nvarchar(50), @Fecha_Nacimiento datetime,@id int

as

begin 

update Socio set Nombre=@Nombre, Apellido = @Apellido , Fecha_Nacimiento = @Fecha_Nacimiento

where Id_Socio = @id

end



select * from Socio
GO
/****** Object:  StoredProcedure [dbo].[Editar_Traduccion]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Editar_Traduccion]

@Nombre nvarchar(50),@id int,@Trad nvarchar(50)

as

begin

UPDATE Traduccion

SET Traduccion.Traduccion = @Trad

FROM Traduccion t

INNER JOIN Idioma id

on t.Id_Idioma = id.Id_Idioma

where id.Nombre = @Nombre and Id_Etiqueta=@id 

end
GO
/****** Object:  StoredProcedure [dbo].[Editar_Usuario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Editar_Usuario]

@NombreUsuario nvarchar(50),@Nombre nvarchar(50), @Apellido nvarchar(50),@Email nvarchar(50),@id int

as

begin 

update Usuario set LoginName=@NombreUsuario, FirstName = @Nombre , LastName = @Apellido, Email = @Email

where Id_Usuario = @id

end
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Sc]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Eliminar_Sc]

@fecha datetime

as

begin

delete from Socio_Clase

where Fecha_Fin = @fecha

end
GO
/****** Object:  StoredProcedure [dbo].[Grabar_Bitacora]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Grabar_Bitacora]

@Id_Usuario int,@Actividad nvarchar(50),@Criticidad nvarchar(50),@Fecha nvarchar(50),@Hora nvarchar(50)

as

begin

insert into Bitacora

values(@Id_Usuario,@Actividad,@Criticidad,@Fecha,@Hora)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Clase]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Insertar_Clase]

@ide int,@ident int,@idp int,@d nvarchar(50),@c int

as

begin

insert into Clase

values(@ide,@ident,@idp,@d,@c)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Entrenador]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Insertar_Entrenador]

@nombre nvarchar(50),@apellido nvarchar(50)

as

begin

insert into Entrenador

values (@nombre,@apellido)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Entrenamiento]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Insertar_Entrenamiento]

@nombre nvarchar(50)

as

begin

insert into Entrenamiento

values (@nombre)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Etiquetas]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Insertar_Etiquetas]

@idI int,@idE int,@T nvarchar(50)

as

begin

insert into Traduccion

values (@idI,@idE,@T)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Horario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Insertar_Horario]

@idc int,@idd int,@h nvarchar(50)

as

begin

insert into Horario

values(@idc,@idd,@h)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Idioma]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Insertar_Idioma]

@nombre nvarchar(50),@default bit

as

begin

declare @UId int

SET @UId = (SELECT MAX(Id_Idioma) FROM Idioma)+1



    if @UId is null

        select @UId = 1

insert into Idioma

values(@UId,@nombre,@default)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Reserva]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Insertar_Reserva]

@ids int,@idc int,@fi datetime,@ff datetime,@pc int,@t int

as

begin

insert into Reserva

values(@ids,@idc,@fi,@ff,@pc,@t)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Insertar_Socio]

@nombre nvarchar(50),@apellido nvarchar(50),@fecha datetime

as

begin

insert into Socio

values(@nombre,@apellido,@fecha)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Socio_Clase]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Insertar_Socio_Clase]

@ids int,@idp int,@idc int,@p int,@cd int,@fi datetime,@ff datetime

as

begin

insert into Socio_Clase

values(@ids,@idp,@idc,@p,@cd,@fi,@ff)

end
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Usuario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Insertar_Usuario]

@LoginName nvarchar(50),@Password nvarchar(50),@FirstName nvarchar(50),@LastName nvarchar(50),@Email nvarchar(50),@Dvh nvarchar(50)

as

begin

declare @UserId int, @ididioma int 

SET @UserId = (SELECT MAX(Id_Usuario) FROM Usuario)+1



    if @UserId is null

        select @UserId = 1

		if @ididioma is null

		select @ididioma = 1

insert into Usuario

values(@UserId,@ididioma,@LoginName,@Password,@FirstName,@LastName,@Email,@Dvh)

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Clase]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_Clase]

as

begin

select * from Clase

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Clases]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_Clases]

as

begin

select c.Descripcion,p.Descripcion Descripcion_Piso,p.Nivel,c.Capacidad,ent.Nombre + ' ' + ent.Apellido as Entrenador,e.Nombre as Actividad from Clase c

inner join Piso p

on c.Id_Piso = p.Id_Piso

inner join Entrenador ent

on c.Id_Entrenador = ent.Id_Entrenador

inner join  Entrenamiento e

on c.Id_Entrenamiento = e.Id_Entrenamiento

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Dias]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Listar_Dias]

as

begin

select * from Día

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Entrenador]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_Entrenador]

as

begin

select * from Entrenador

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Entrenamiento]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_Entrenamiento]

as

begin

select * from Entrenamiento

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_HistorialCambios]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_HistorialCambios]

as

begin

select * from Bitacora_Socio

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Piso]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_Piso]

as

begin

select * from Piso

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Sc]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_Sc]

as

begin

select * from Socio_Clase

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Listar_Socio]

as 

begin

select * from Socio

end
GO
/****** Object:  StoredProcedure [dbo].[Listar_Usuario]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Listar_Usuario]

  as

  select * from Usuario
GO
/****** Object:  StoredProcedure [dbo].[ObtenerIdTraduccion]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ObtenerIdTraduccion]

@nombre nvarchar(50)

as

select Id_Etiqueta from Etiqueta

where Nombre=@nombre
GO
/****** Object:  StoredProcedure [dbo].[Search_EmailUser]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Search_EmailUser]

@em nvarchar(50)

as

begin

select Email from Usuario 

where Email= @em

end
GO
/****** Object:  StoredProcedure [dbo].[Search_idclase]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Search_idclase]

@n nvarchar(50)

as

begin

select Id_Clase from Clase

where Descripcion=@n

end
GO
/****** Object:  StoredProcedure [dbo].[Search_iddia]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Search_iddia]

@n nvarchar(50)

as

begin

select Id_Dia from Día

where Nombre=@n

end
GO
/****** Object:  StoredProcedure [dbo].[Search_idPiso]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Search_idPiso]

@d nvarchar(50)

as

begin

select Id_Piso from Clase

where Descripcion = @d

end
GO
/****** Object:  StoredProcedure [dbo].[Search_Level]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Search_Level]

@lvl nvarchar(50)

as

begin

select Id_Piso from Piso

where Nivel = @lvl

end
GO
/****** Object:  StoredProcedure [dbo].[Search_Trainer]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Search_Trainer]

@n nvarchar(50),@a nvarchar(50)

as

begin

select Id_Entrenador from Entrenador

where Nombre = @n and Apellido = @a

end
GO
/****** Object:  StoredProcedure [dbo].[Search_Training]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Search_Training]

@t nvarchar(50)

as

begin

select Id_Entrenamiento from Entrenamiento

where Nombre = @t

end
GO
/****** Object:  StoredProcedure [dbo].[Search_User]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Search_User]

@usu varchar(50)

as

select Id_Usuario,LoginName,Password from Usuario where LoginName=@usu
GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_login]

@em nvarchar(50),

@pas nvarchar(50)

as

select Id_Usuario,Email,Password from Usuario where Email=@em and Password=@pas
GO
/****** Object:  StoredProcedure [dbo].[update_password]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[update_password]

@user nvarchar(50),@pass nvarchar(50)

as

begin

UPDATE Usuario

SET Password = @pass

where LoginName=@user

end
GO
/****** Object:  Trigger [dbo].[trigger_update_socio]    Script Date: 1/12/2022 02:55:12 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[trigger_update_socio]

on [dbo].[Socio]

AFTER update

NOT FOR REPLICATION

as

insert into Bitacora_Socio(Id_Socio,Nombre,Apellido,Fecha_Nacimiento,Descripcion_Cambio,Fecha_Cambio)

select Id_Socio,Nombre,Apellido,Fecha_Nacimiento,'OLD DATA',GETDATE()

from deleted;

insert into Bitacora_Socio(Id_Socio,Nombre,Apellido,Fecha_Nacimiento,Descripcion_Cambio,Fecha_Cambio)

select Id_Socio,Nombre,Apellido,Fecha_Nacimiento,'NEW DATA',GETDATE()

from inserted;
GO
ALTER TABLE [dbo].[Socio] ENABLE TRIGGER [trigger_update_socio]
GO
