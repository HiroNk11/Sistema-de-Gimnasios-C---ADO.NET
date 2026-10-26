/* Catálogos del script original. No contiene usuarios, socios, clases, reservas ni bitácoras. Solo para una instalación nueva. */

SET IDENTITY_INSERT [dbo].[Día] ON 

INSERT [dbo].[Día] ([Id_Dia], [Nombre]) VALUES (1, N'Lunes')
INSERT [dbo].[Día] ([Id_Dia], [Nombre]) VALUES (2, N'Martes')
INSERT [dbo].[Día] ([Id_Dia], [Nombre]) VALUES (3, N'Miércoles')
INSERT [dbo].[Día] ([Id_Dia], [Nombre]) VALUES (4, N'Jueves')
INSERT [dbo].[Día] ([Id_Dia], [Nombre]) VALUES (5, N'Viernes')
INSERT [dbo].[Día] ([Id_Dia], [Nombre]) VALUES (6, N'Sábado')
INSERT [dbo].[Día] ([Id_Dia], [Nombre]) VALUES (7, N'Domingo')
SET IDENTITY_INSERT [dbo].[Día] OFF

GO

INSERT [dbo].[Digito_Verificador] ([IdTabla], [Nombre_Tabla], [DVV]) VALUES (1, N'Usuario', N'240000')

GO

SET IDENTITY_INSERT [dbo].[Entrenamiento] ON 

INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (18, N'Zumba')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (19, N'Crossfit')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1019, N'Baila Conmigo')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1020, N'Entrenamiento localizado')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1021, N'Entrenamiento Funcional')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1022, N'Race Bike')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1023, N'Jumping Box')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1024, N'Spinning')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1025, N'Desafio Combat')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1026, N'Jump Fitness')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1027, N'Musculacion')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1028, N'TaekWonDo')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1029, N'Pilates')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1030, N'Danza')
INSERT [dbo].[Entrenamiento] ([Id_Entrenamiento], [Nombre]) VALUES (1031, N'Boxeo')
SET IDENTITY_INSERT [dbo].[Entrenamiento] OFF

GO

INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (1, N'sesion')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (2, N'cambiar_contraseña')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (3, N'cerrarsesion')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (4, N'idioma')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (5, N'seleccionar_idioma')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (6, N'gestionar_idioma')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (7, N'actualizar_lista_idiomas')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (8, N'seguridad')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (9, N'famypat')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (10, N'usuarios')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (11, N'gestion')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (12, N'usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (13, N'negocio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (14, N'socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (15, N'ges_socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (16, N'asignar_socio_clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (17, N'entrenador')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (18, N'gestionar_entrenador')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (19, N'asignar_entrenador_clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (20, N'entrenamiento')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (21, N'gestionar_entrenamiento')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (22, N'asignar_horario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (23, N'CambiarContraseña ')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (24, N'contraseña_actual')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (25, N'contraseña_nueva')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (26, N'confirmar_contraseña')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (27, N'aceptar')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (28, N'cancelar')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (29, N'ver clases')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (30, N'clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (31, N'crear clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (32, N'ver detalle clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (33, N'ver calendario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (34, N'Idioma ')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (35, N'agregar')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (36, N'gestionar idioma')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (37, N'seleccionar idioma')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (38, N'buscar')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (39, N'etiqueta idioma default')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (40, N'traduccion')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (41, N'guardar cambios')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (42, N'patentes')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (43, N'todas las patentes')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (44, N'agregar patente>>')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (45, N'nuevo permiso')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (46, N'permiso')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (47, N'nombre')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (48, N'guardar')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (49, N'familias')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (50, N'todas las familias')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (51, N'configurar')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (52, N'agregar >>')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (53, N'nueva familia')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (54, N'nombre familia')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (55, N'guardar nueva familia')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (56, N'configurar familia')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (57, N'guardar familia')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (58, N'seleccion usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (59, N'todos los usuarios')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (60, N'configurar usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (61, N'agregar patentes')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (62, N'agregar patente')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (63, N'agregar familia')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (64, N'nombre de usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (65, N'nombre:')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (66, N'apellido:')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (67, N'email')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (68, N'id usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (69, N'buscar usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (70, N'alta usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (71, N'baja usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (72, N'modificar usuario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (73, N'formulario gestion usuarios')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (74, N'registro socios')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (75, N'nombre socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (76, N'apellido socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (77, N'fecha nacimiento ')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (78, N'lista socios')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (79, N'buscar socios')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (80, N'id socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (81, N'agregar socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (82, N'eliminar socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (83, N'modificar socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (84, N'asignar socio a clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (85, N'seleccionar socio')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (86, N'seleccionar clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (87, N'precio por clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (88, N'detalles clase')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (89, N'actividad')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (90, N'entrenador personal')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (91, N'nivel piso')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (92, N'descripcion piso')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (93, N'dia')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (94, N'horario')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (95, N'listado de clases incriptas')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (96, N'registro de entrenadores')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (97, N'nombre entrenador')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (98, N'apellido entrenador')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (99, N'lista de entrenadores')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (100, N'buscar entrenadores')

GO

INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (101, N'id entrenador')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (102, N'agregar entrenador')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (103, N'eliminar entrenador')
INSERT [dbo].[Etiqueta] ([Id_Etiqueta], [Nombre]) VALUES (104, N'modificar entrenador')

GO

INSERT [dbo].[Idioma] ([Id_Idioma], [Nombre], [Idioma_Default]) VALUES (1, N'Español', 1)
INSERT [dbo].[Idioma] ([Id_Idioma], [Nombre], [Idioma_Default]) VALUES (2, N'Ingles', 0)

GO

SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3032, N'Administrador', NULL)
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3033, N'Gestionar Usuarios', N'PuedeGestionarUsuarios')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3034, N'Cambiar Idioma', N'PuedeCambiarIdioma')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3035, N'Gestionar Idioma', N'PuedeGestionarIdioma')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3036, N'Configurar Patentes y Familias', N'PuedeConfigurarSeguridadPatentesyFamilias')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3037, N'Configurar Permisos Usuarios', N'PuedeConfigurarSeguridadUsuarios')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3038, N'Permisos de Negocio', N'PuedeAccederANegocio')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3039, N'Baja Usuario', N'DarBajaUsuario')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3040, N'Bitacora', N'PuedeAccederBitacora')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3041, N'Backup', N'PuedeAccederBackup')
INSERT [dbo].[Permiso] ([Id_Permiso], [Nombre], [Permiso]) VALUES (3042, N'Restore', N'PuedeAccederRestore')
SET IDENTITY_INSERT [dbo].[Permiso] OFF

GO

INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3033)
INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3034)
INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3035)
INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3036)
INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3037)
INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3038)
INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3039)
INSERT [dbo].[Permiso_Permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (3032, 3040)

GO

SET IDENTITY_INSERT [dbo].[Piso] ON 

INSERT [dbo].[Piso] ([Id_Piso], [Nivel], [Descripcion]) VALUES (1, N'PB', N'Planta Baja')
INSERT [dbo].[Piso] ([Id_Piso], [Nivel], [Descripcion]) VALUES (2, N'1', N'Primer Piso')
INSERT [dbo].[Piso] ([Id_Piso], [Nivel], [Descripcion]) VALUES (4, N'2', N'Segundo Piso')
INSERT [dbo].[Piso] ([Id_Piso], [Nivel], [Descripcion]) VALUES (5, N'3', N'Tercer Piso')
INSERT [dbo].[Piso] ([Id_Piso], [Nivel], [Descripcion]) VALUES (6, N'4', N'Cuarto Piso')
SET IDENTITY_INSERT [dbo].[Piso] OFF

GO

SET IDENTITY_INSERT [dbo].[Traduccion] ON 

INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (1, 1, 1, N'Sesión')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (2, 1, 2, N'Cambiar Contraseña')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (3, 1, 3, N'Cerrar Sesion')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (4, 1, 4, N'Idioma')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (5, 1, 5, N'Seleccionar Idioma')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (6, 1, 6, N'Gestionar Idioma')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (7, 1, 7, N'Actualizar Lista Idiomas')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (8, 1, 8, N'Seguridad')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (9, 1, 9, N'Familia y Patentes')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (10, 1, 10, N'Usuarios')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (11, 1, 11, N'Gestión')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (12, 1, 12, N'Usuarios')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (13, 1, 13, N'Negocio')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (14, 1, 14, N'Socio')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (15, 1, 15, N'Gestionar Socio')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (16, 1, 16, N'Asignar Socio Clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (17, 1, 17, N'Entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (18, 1, 18, N'Gestionar Entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (19, 1, 19, N'Asignar Entrenador Clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (20, 1, 20, N'Entrenamiento')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (21, 1, 21, N'Gestionar Entrenamiento')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (22, 1, 22, N'Asignar Horario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (23, 1, 23, N'CAMBIAR CONTRASEÑA')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (24, 1, 24, N'Contraseña Actual :')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (25, 1, 25, N'Contraseña Nueva :')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (26, 1, 26, N'Confirmar Nueva Contraseña :')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (27, 1, 27, N'Aceptar')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (28, 1, 28, N'Cancelar')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (394, 1, 29, N'Ver Clases')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (396, 1, 30, N'Clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (397, 1, 31, N'Crear Clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (398, 1, 32, N'Ver Detalle Clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (399, 1, 33, N'Ver Calendario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (400, 1, 34, N'Idioma')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (401, 1, 35, N'Agregar')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (402, 2, 1, N'Session')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (403, 2, 2, N'Change Password')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (404, 2, 3, N'Sign off')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (405, 2, 4, N'Language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (406, 2, 5, N'Select Language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (407, 2, 6, N'Manage_Language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (408, 2, 7, N'Update_List_Languages')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (409, 2, 8, N'Security')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (410, 2, 9, N'Family and Permissions')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (411, 2, 10, N'Users')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (412, 2, 11, N'management')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (413, 2, 12, N'Username')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (414, 2, 13, N'negocio')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (415, 2, 14, N'business')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (416, 2, 15, N'
Inglés
gestionar_socio
manage_partner')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (417, 2, 16, N'inscribir_socio_clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (418, 2, 17, N'coach')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (419, 2, 18, N'manage_coach')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (420, 2, 19, N'assign_trainer_class')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (421, 2, 20, N'training')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (422, 2, 21, N'manage_training')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (423, 2, 22, N'assign_schedule')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (424, 2, 23, N'Change Password
')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (425, 2, 24, N'Current password')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (426, 2, 25, N'new password')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (427, 2, 26, N'Confirm Password')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (428, 2, 27, N'
to accept')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (429, 2, 28, N'Cancel')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (430, 2, 29, N'see classes')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (431, 2, 30, N'class')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (432, 2, 31, N'create class')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (433, 2, 32, N'see detail class')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (434, 2, 33, N'see calendar')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (435, 2, 34, N'language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (436, 2, 35, N'add')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (437, 1, 36, N'Manage language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (438, 1, 37, N'Select language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (439, 1, 38, N'Search')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (440, 1, 39, N'Label Language Default')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (441, 1, 40, N'Translation')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (442, 1, 41, N'Save Changes')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (445, 2, 36, N'change language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (446, 2, 37, N'select language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (447, 2, 38, N'search')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (448, 2, 39, N'default tag language')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (449, 2, 40, N'translate')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (450, 2, 41, N'save changes')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (453, 1, 42, N'patents')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (454, 1, 43, N'all patents')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (456, 1, 44, N'add patent>>')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (457, 1, 45, N'new permit')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (458, 1, 46, N'permission')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (459, 1, 47, N'Name')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (460, 1, 48, N'save patent')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (461, 1, 49, N'families')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (462, 1, 50, N'Every family')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (463, 1, 51, N'set up')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (464, 1, 52, N'agregar >>')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (466, 1, 53, N'add >>')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (467, 1, 54, N'family name')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (468, 1, 55, N'save family')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (469, 1, 56, N'set up family')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (470, 1, 57, N'guardar familia')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (471, 2, 42, N'patents')

GO

INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (473, 2, 43, N'b')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (475, 2, 44, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (476, 2, 45, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (477, 2, 46, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (478, 2, 47, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (479, 2, 48, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (480, 2, 49, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (481, 2, 50, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (482, 2, 51, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (483, 2, 52, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (484, 2, 53, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (485, 2, 54, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (486, 2, 55, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (487, 2, 56, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (488, 2, 57, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (489, 1, 58, N'usuarios')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (490, 1, 59, N'todos los usuarios')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (491, 1, 60, N'configurar usuario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (492, 1, 61, N'agregar patentes')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (493, 1, 62, N'agregar patente')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (494, 1, 63, N'agregar familia')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (495, 2, 58, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (496, 2, 59, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (497, 2, 60, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (498, 2, 61, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (499, 2, 62, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (500, 2, 63, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (501, 1, 64, N'Nombre de Usuario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (502, 1, 65, N'Nombre')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (503, 1, 66, N'apellido:')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (504, 1, 67, N'email')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (505, 1, 68, N'id usuario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (506, 1, 69, N'Buscar')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (507, 1, 70, N'alta usuario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (508, 1, 71, N'baja usuario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (509, 1, 72, N'modificar usuario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (510, 1, 73, N'formulario gestion usuarios')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (512, 2, 64, N'Username')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (513, 2, 65, N'Name:')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (514, 2, 66, N'surname:')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (515, 2, 67, N'mail')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (516, 2, 68, N'user id')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (517, 2, 69, N'search user')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (518, 2, 70, N'High user')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (519, 2, 71, N'unsubscribe user')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (520, 2, 72, N'modify user')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (521, 2, 73, N'user management form')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (522, 1, 74, N'Registro Socios')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (523, 1, 75, N'Nombre')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (524, 1, 76, N'Apellido')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (525, 1, 77, N'Fecha de Nacimiento')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (526, 1, 78, N'Lista Socios')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (527, 1, 79, N'Buscar')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (528, 1, 80, N'Id Socio')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (529, 1, 81, N'Agregar ')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (530, 1, 82, N'Eliminar')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (531, 1, 83, N'Modificar ')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (532, 2, 74, N'partner registration')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (533, 2, 75, N'partner name')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (534, 1, 84, N'asignar socio a clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (535, 1, 85, N'seleccionar socio')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (536, 1, 86, N'seleccionar clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (537, 1, 87, N'precio por clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (538, 1, 88, N'detalles clase')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (539, 1, 89, N'actividad')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (540, 1, 90, N'entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (541, 1, 91, N'nivel piso')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (542, 1, 92, N'descripcion piso')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (543, 1, 93, N'dia')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (544, 1, 94, N'horario')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (545, 1, 95, N'Listado Clases Inscriptas')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (546, 1, 96, N'registro de entrenadores')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (547, 1, 97, N'Nombre Entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (548, 1, 98, N'apellido entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (549, 1, 99, N'lista de entrenadores')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (550, 1, 100, N'buscar entrenadores')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (551, 1, 101, N'id entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (553, 1, 102, N'agregar entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (554, 1, 103, N'eliminar entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (555, 1, 104, N'modificar entrenador')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (556, 2, 76, N'last name partner')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (557, 2, 77, N'birth date')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (558, 2, 78, N'partners list')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (559, 2, 79, N'look for partners')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (560, 2, 80, N'partner id')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (561, 2, 81, N'add partner')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (562, 2, 82, N'remove partner')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (563, 2, 83, N'modify partner')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (564, 2, 84, N'assign partner to class')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (565, 2, 85, N'select partner')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (566, 2, 86, N'select class')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (567, 2, 87, N'price per class')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (568, 2, 88, N'class details')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (569, 2, 89, N'exercise')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (570, 2, 90, N'personal trainer')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (571, 2, 91, N'floor level')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (572, 2, 92, N'floor description')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (573, 2, 93, N'day')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (574, 2, 94, N'schedule')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (575, 2, 95, N'list of enrolled classes')

GO

INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (576, 2, 96, N'coach registration')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (577, 2, 97, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (578, 2, 98, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (579, 2, 99, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (580, 2, 100, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (581, 2, 101, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (582, 2, 102, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (583, 2, 103, N'a')
INSERT [dbo].[Traduccion] ([Id_Traduccion], [Id_Idioma], [Id_Etiqueta], [Traduccion]) VALUES (584, 2, 104, N'a')
SET IDENTITY_INSERT [dbo].[Traduccion] OFF

GO
