# Auditoría de seguridad - Gimnasios

Revisión del 27/10/2026. Alcance: código y archivos versionados, historial alcanzable de ramas/etiquetas descargadas, configuración, recuperación, exportación y dependencias. No incluye una prueba de penetración, SQL del usuario ni envío SMTP real.

## Hallazgos corregidos

| Hallazgo | Corrección |
|---|---|
| BouncyCastle 1.8.9 y System.Text.Json 7.0.0 tenían avisos aplicables. | iTextSharp 5.5.13.6, BouncyCastle.Cryptography 2.7.0 y System.Text.Json 10.0.14. Se retiraron dependencias sin uso y XMLWorker. |
| El reporte interpretaba nombres y descripciones como HTML. | Tabla PDF de texto, sin parser HTML que pueda interpretar URLs o rutas introducidas en los campos. |
| La aplicación aceptaba conexiones remotas sin cifrado o sin validar certificados. | Fuera de LocalDB exige Encrypt=True y TrustServerCertificate=False. También limita la conexión y desactiva PersistSecurityInfo. |
| La recuperación de usuario distinguía correos registrados y no registrados. | Respuesta genérica, límite de intentos local y diagnóstico SMTP sin mensajes ni datos personales. La recuperación de contraseña también oculta fallos de SMTP al solicitante. |
| Algunas consultas BLL de usuarios no exigían permisos. | Autorización para buscar correos, usuarios y listados administrativos antes de acceder a SQL. |
| Faltaban cotas antes de procesar hashes/tokens y el diccionario de intentos podía crecer sin límite. | Hashes de hasta 256 caracteres, tokens acotados, comparación sin salida anticipada y máximo de 4096 entradas con limpieza de vencidas. |
| Política de contraseñas nuevas de 12 caracteres. | Mínimo 15 caracteres Unicode, máximo 128 unidades UTF-16, rechazo de controles y lista básica de valores comunes. Los hashes existentes siguen siendo compatibles. |
| Dependencias sin bloqueo y sin control continuo. | PackageReference, packages.lock.json, auditoría de todo el grafo y workflow que falla si NuGet no puede completar la revisión. |

Los avisos originales se verificaron en el catálogo oficial de NuGet: [BouncyCastle, aviso 1](https://github.com/advisories/GHSA-v435-xc8x-wvr9), [aviso 2](https://github.com/advisories/GHSA-m44j-cfrm-g8qc), [aviso 3](https://github.com/advisories/GHSA-8xfc-gm6g-vgpv), [System.Text.Json](https://github.com/advisories/GHSA-hh2w-p6rv-4g7w). La revisión no demuestra que todos fueran explotables en este flujo concreto; se actualizaron las versiones afectadas.

## Comprobaciones

- Compilación completa de .NET Framework 4.7.2 con MSBuild de Visual Studio, sin errores observados.
- 181 comprobaciones .NET aprobadas: 44 generales/seguridad, 21 perfiles, 31 monitoreo, 28 planificación, 16 interfaz, 17 patrones, 15 buscadores y nueve bitácora. Son pruebas sin SQL real.
- Siete pruebas del comparador de avisos aprobadas. Doce versiones resueltas sin avisos aplicables en la instantánea oficial guardada en AUDITORIA-DEPENDENCIAS.json.
- Gitleaks 8.30.1 con valores redactados y revisión adicional de privacidad. Ver AUDITORIA-PRIVACIDAD.md.
- Manual regenerado desde Markdown, texto comprobado y sus cuatro páginas revisadas visualmente.

La restauración local usó un feed temporal descargado de NuGet mediante TLS validado. Cada paquete se cotejó con el SHA512 de su entrada en el catálogo oficial. La auditoría por Python se hizo en línea con TLS; la auditoría nativa de NuGet se exige además en CI. Preparar un workflow no demuestra que ya haya pasado en GitHub.

## Instalar los cambios

Restaurar NuGet con el lockfile antes de compilar, desde Visual Studio o un Developer PowerShell:

```powershell
msbuild CapaIU.sln /t:Restore /p:RestoreLockedMode=true /p:SecurityAudit=true
.\Compilar.ps1
.\Tests\EjecutarPruebas.ps1
```

No cambiar automáticamente los hashes existentes ni reinstalar una base con datos. Esta etapa no modifica el esquema SQL ni crea cuentas. Las claves nuevas y las restablecidas usan la política actual. No versionar GYM_SMTP_PASSWORD ni cadenas con credenciales; configurar secretos solo en el entorno protegido de la instalación.

## Límites pendientes

El acceso directo desde WinForms a SQL conserva la confianza en la cuenta Windows. Los permisos de la aplicación no protegen frente al propietario de LocalDB o una persona que ejecute código bajo la misma identidad. Hace falta una API y otra identidad de servidor antes de distribuirlo entre operadores no confiables; no se implementó esa migración aquí.

El bloqueo de intentos es local al proceso y se reinicia al cerrar. La sesión no tiene una frontera de autenticación remota ni revocación central; no exponer este cliente como un servicio de Internet. La respuesta de recuperación es genérica, pero los tiempos de consulta y SMTP pueden diferir: todavía no hay una cola independiente para recuperación.

Los verificadores MD5 heredados son controles de compatibilidad/integridad, no almacenamiento de contraseñas ni prueba de autenticidad. Las contraseñas usan PBKDF2-SHA256 con 600000 iteraciones y sal aleatoria. La lista de contraseñas comunes es básica, no un catálogo completo de credenciales filtradas.

Las exportaciones contienen datos personales por diseño. No se probaron cuentas/correos reales ni se extrajeron datos de la base del usuario. La ausencia de avisos en una instantánea no garantiza ausencia de vulnerabilidades desconocidas.
