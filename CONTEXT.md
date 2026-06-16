# AgroControl — Contexto de trabajo

## Estado actual
- App WinForms (.NET 8) funcional, 0 errores, 0 warnings
- Conectada a Supabase PostgreSQL (nube)
- Panel web mobile: https://dilankl30.github.io/ProyectoControl/
- Código Arduino ESP32 listo en `arduino/agrocontrol.ino`

## Proyecto
- Repo: https://github.com/Dilankl30/ProyectoControl
- Supabase project ref: `ezuqrhdtpwbxnatzlzrt` (us-west-2)
- DB: `postgres`, user: `postgres`, pass: `EAx26oUQhsregViV`
- Anon key: `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImV6dXFyaGR0cHdieG5hdHpsenJ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3ODE1NjQ2NjksImV4cCI6MjA5NzE0MDY2OX0.TfTMZaKQG80jalMpoGwPuteGyFiAsnMs1mvvuNhnxt8`
- Login app: `admin`/`admin123` o `test`/`test123`

## Lo que falta / Puede seguir mañana
1. Probar el panel web desde el celular de verdad
2. Cablear y subir el sketch al ESP32
3. Probar que Arduino mande lecturas y reciba comandos
4. Probar la app WinForms desde la PC de tu amigo (`daalc`)
5. Ajustar el mapeo del sensor de suelo (invertir si es necesario)
6. Configurar refresh automático en la app WinForms
7. Mejorar la interfaz de `readingLog.cs` con filtros
8. Agregar `appsettings.json` para la cadena de conexión
9. Desplegar el panel web con dominio personalizado (opcional)

## Comandos útiles
```bash
cd C:\Users\Kleverson\Desktop\proyectoflor
dotnet run --project AgroControl
git add -A && git commit -m "mensaje" && git push
```

## Notas
- Conexión Supabase requiere IPv6 o Cloudflare WARP
- Pooler NO funciona para este proyecto, usar directo
- Arduino inserta lecturas vía RPC: `insertar_lectura(tipo, valor)`
- Comandos se leen de tabla `COMANDOS` y se marcan `ejecutado`
