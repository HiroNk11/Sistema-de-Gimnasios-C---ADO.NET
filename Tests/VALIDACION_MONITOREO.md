# Validación de calendario y saldos — 26/10/2026

- Compilaciones Release y Debug: correctas.
- Monitoreo: 31 comprobaciones de semanas, distribución de simultáneas, medianoche, filtros, selección mediante clic/teclado, participantes, búsquedas e indicadores con datos ficticios.
- Regresión: 21 de perfiles, 16 de interfaz, 28 de planificación, 17 de presentación/navegación y 9 de bitácora.
- Verificaciones generales: 31 de credenciales, permisos y configuración.
- Total: 153 comprobaciones sin SQL real.
- Análisis sintáctico: 72 consultas del código y 7 archivos SQL sin errores.
- Modelo estático con consultas nuevas: 102 definiciones y cero mensajes de validación.
- Revisión visual: Inicio, calendario semanal, calendario compacto y saldos pendientes.

No se ejecutaron escrituras, integración ni concurrencia contra LocalDB desde este entorno. El ejecutor general informa explícitamente que omite la integración cuando falta GYM_TEST_CONNECTION_STRING. Se añadieron casos para una base GymTests_ vacía, pero su resultado continúa pendiente.

La copia ejecutable de pruebas se actualiza conservando su archivo de conexión. No se modifican usuarios, claves, permisos ni datos de la base.
