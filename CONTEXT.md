# AgroControl — TODO guardado

============================================================================
## 1. UBICACIÓN DEL PROYECTO (LOCAL)
============================================================================
Ruta: C:\Users\Kleverson\Desktop\proyectoflor

Estructura:
  ├── AgroControl/          → App WinForms (UI)
  │   ├── dashboard.cs      → Dashboard con cards, chart, tablas
  │   ├── interfaz.cs       → Menú lateral con 6 botones
  │   ├── interfaz.Designer.cs → Layout del menú
  │   ├── charts.cs         → Pantalla Charts
  │   ├── readingLog.cs     → Log de lecturas
  │   ├── plantRecord.cs    → Registro de plantas
  │   ├── settings.cs       → Configuración
  │   ├── helpForm.cs       → Manual de usuario
  │   ├── Form1.cs          → Login
  │   └── AgroControl.csproj
  ├── BusinessLogic/        → Capa de negocio
  │   ├── plantBus.cs       → CRUD plantas
  │   ├── sensorBus.cs      → CRUD sensores
  │   └── usuarioBus.cs     → Login usuarios
  ├── DataAccess/           → Capa de datos
  │   └── DataAccess.cs     → Conexión a Supabase (Npgsql)
  ├── Entities/             → Modelos
  │   ├── plant.cs          → Planta entity
  │   ├── sensor.cs         → Sensor entity
  │   └── usuario.cs        → Usuario entity
  ├── panel/                → Panel web HTML
  │   └── index.html        → Interfaz web mobile
  ├── docs/                 → GitHub Pages (copia de panel/)
  │   ├── index.html
  │   └── .nojekyll
  ├── arduino/              → Código ESP32
  │   ├── agrocontrol.ino   → Sketch principal
  │   └── platformio.ini    → Config PlatformIO
  └── supabase_schema.sql   → Schema BD PostgreSQL

============================================================================
## 2. GITHUB
============================================================================
URL repo:     https://github.com/Dilankl30/ProyectoControl
Autenticado como: Dilankl30 (Kleverson)
Token local (gh CLI): autenticado en C:\Users\Kleverson\AppData\Roaming\gh

Panel web publicado en GitHub Pages:
  https://dilankl30.github.io/ProyectoControl/
  Build desde: branch main, carpeta /docs
  Último commit: e3b9c03

Comandos git:
  cd C:\Users\Kleverson\Desktop\proyectoflor
  git add -A
  git commit -m "mensaje"
  git push

Otros contributors/colaboradores:
  - daalc (amigo, necesita hacer git pull para actualizar)

============================================================================
## 3. SUPABASE — CONEXIÓN Y CREDENCIALES
============================================================================
Project ref:      ezuqrhdtpwbxnatzlzrt
Region:           us-west-2
Dashboard URL:    https://supabase.com/dashboard/project/ezuqrhdtpwbxnatzlzrt

CONEXIÓN DIRECTA (IPv6 — requiere WARP si ISP no tiene IPv6):
  Host:     db.ezuqrhdtpwbxnatzlzrt.supabase.co
  Database: postgres
  Username: postgres                          ← SIN el sufijo .ezuqrhdtpwbxnatzlzrt
  Password: EAx26oUQhsregViV
  SSL Mode: Require
  Trust Server Certificate: true

CADENA DE CONEXIÓN COMPLETA (usar en DataAccess.cs línea 10):
  Host=db.ezuqrhdtpwbxnatzlzrt.supabase.co; Database=postgres; Username=postgres; Password=EAx26oUQhsregViV; SSL Mode=Require; Trust Server Certificate=true;

⚠ POOLER NO FUNCIONA para este proyecto:
  aws-0-us-west-2.pooler.supabase.com
  El pooler requiere username = "postgres.ezuqrhdtpwbxnatzlzrt" y da error ENOTFOUND

SUPABASE MANAGEMENT API:
  Token: sbp_12f48e92c39480e1c4c3853af57d11fbdefe0e2c
  URL:   https://api.supabase.com/v1/projects/ezuqrhdtpwbxnatzlzrt

SUPABASE ANON KEY (para el panel web y Arduino — pública, no es secreta):
  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImV6dXFyaGR0cHdieG5hdHpsenJ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3ODE1NjQ2NjksImV4cCI6MjA5NzE0MDY2OX0.TfTMZaKQG80jalMpoGwPuteGyFiAsnMs1mvvuNhnxt8

SUPABASE SERVICE_ROLE KEY (solo para backend — NO exponer):
  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImV6dXFyaGR0cHdieG5hdHpsenJ0Iiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc4MTU2NDY2OSwiZXhwIjoyMDk3MTQwNjY5fQ.JpgB0_hEfXRnJsxRsJhGulCTnmiNLYjQBoKxskKaBGo

SUPABASE REST API BASE:
  https://ezuqrhdtpwbxnatzlzrt.supabase.co/rest/v1/

TABLAS EN LA BD (PostgreSQL, todas en public):
  - invernadero  → idInvernadero, nombre, ubicacion
  - usuarios     → idUsuario, nombre, contrasena, tipoUsuario
  - planta       → idPlanta, nombre, nombreCien, descripcion
  - sensor       → idSensor, tipo, idInvernadero
  - lectura      → idLectura, idSensor, fechaHora, valor
  - comandos     → idComando, accion, parametro, estado, creadoEn, ejecutadoEn

FUNCIÓN RPC CREADA:
  insertar_lectura(p_tipo VARCHAR, p_valor NUMERIC) RETURNS JSON
  Busca el sensor por tipo e inserta la lectura. Llama así:
    POST https://ezuqrhdtpwbxnatzlzrt.supabase.co/rest/v1/rpc/insertar_lectura
    Body: {"p_tipo": "tempAire", "p_valor": 28.5}
    Headers: apikey + Authorization: Bearer [anon key]

RLS POLICIES ACTIVAS (anon key puede):
  - SELECT en lectura, sensor, planta
  - ALL (SELECT, INSERT, UPDATE, DELETE) en comandos

DATOS SEMILLA:
  Invernaderos: Central (Zona A), Norte (Zona B)
  Usuarios:     admin/admin123 (administrador), operador1/123456 (operador)
  Plantas:      Rosa, Orquídea
  Sensores:     humSuelo(idSensor=1), tempAire(idSensor=2), humAire(idSensor=3), luz(idSensor=4)
  Lecturas:     4 lecturas de ejemplo (1 por sensor)

LOGIN APP:
  admin / admin123
  test  / test123

============================================================================
## 4. APP WINDOWS FORMS — DETALLES TÉCNICOS
============================================================================
Framework: .NET 8.0 (net8.0-windows)
Target: WinExe
Nullable: enable
Paquetes NuGet:
  - Npgsql 8.0.5                 → Conexión PostgreSQL
  - FontAwesome.Sharp 6.6.0      → Iconos
  - System.Windows.Forms.DataVisualization 1.0.0-prerelease → Charts
  - System.Data.SqlClient 4.9.1  → Requerido por DataVisualization

ARQUITECTURA:
  Form1.cs (Login)
    → Interfaz.cs (Menú principal con panelDesktop)
        → dashboard.cs (Cards + Chart + Tablas + Alertas)
        → readingLog.cs
        → charts.cs
        → plantRecord.cs
        → settings.cs
        → helpForm.cs

USUARIO LOGEADO:
  Se pasa como parámetro Usuario desde Form1 a Interfaz(Usuario)
  Se muestra en label2 (top right)

CONEXIÓN A BD:
  DataAccess.DataAccess.cadenaConexion (static string)
  getQuery(SQL) → DataTable
  getQuery(SQL, parametros) → DataTable
  execQuery(SQL) → int
  execQuery(SQL, parametros) → int

ESTADO DE COMPILACIÓN:
  0 errores, 0 warnings
  CS8981 suprimido en csproj (NoWarn)
  #nullable disable en dashboard.cs y BusinessLogic/*.cs

COMANDO PARA CORRER:
  cd C:\Users\Kleverson\Desktop\proyectoflor
  dotnet run --project AgroControl

COMANDO PARA COMPILAR:
  cd C:\Users\Kleverson\Desktop\proyectoflor
  dotnet build

PC DEL AMIGO (daalc):
  - SDK .NET 10.0.301 instalado
  - Necesita hacer: dotnet add package System.Data.SqlClient
    (porque DataVisualization lo requiere y no está en su caché)
  - Conexión falla si usa pooler → debe usar la cadena DIRECTA
  - IPv6: debe tener Cloudflare WARP activo

============================================================================
## 5. PANEL WEB MOBILE
============================================================================
URL:              https://dilankl30.github.io/ProyectoControl/
Código fuente:    panel/index.html (también en docs/index.html)
Librerías CDN:    @supabase/supabase-js@2 (desde unpkg.com)
Auto-refresh:     cada 5 segundos
Responsive:       Si, mobile-first con media queries

SECCIONES:
  1. Topbar con logo + indicador online/offline
  2. Grid de 4 tarjetas de sensores (tempAire, humSuelo, humAire, luz)
     - Muestra valor actual, rango ideal, indicador (Good/Low/High)
  3. Controls — 3 toggles: LED Grow Light, Water Pump, Fan
     - Cada toggle inserta en COMANDOS con accion = "led_on"/"led_off" etc.
  4. Recent Readings — tabla con últimas 20 lecturas
  5. Command Log — últimos 10 comandos enviados

TABLAS QUE USA (vía REST API anon key):
  - lectura (SELECT)
  - sensor (SELECT)
  - comandos (SELECT, INSERT)

============================================================================
## 6. ARDUINO / ESP32
============================================================================
Archivo:    arduino/agrocontrol.ino
Plataforma: ESP32 (espressif32)
Framework:  Arduino
Monitor:    115200 baud

LIBRERÍAS REQUERIDAS:
  - ArduinoJson v7
  - DHT sensor library v1.4 (Adafruit)
  - WiFi.h (built-in ESP32)
  - HTTPClient.h (built-in ESP32)

PINES:
  GPIO 4  → DHT22 (temperatura + humedad)
  GPIO 34 → Sensor humedad suelo (analog input)
  GPIO 35 → LDR / fotoresistencia (analog input)
  GPIO 16 → Relé LED grow light (output)
  GPIO 17 → Relé Bomba de agua (output)
  GPIO 18 → Relé Ventilador (output)

LO QUE HACE:
  1. Conecta WiFi al iniciar
  2. Cada 30s: lee sensores, envía 4 lecturas a Supabase
     - sendReading("tempAire", temp)
     - sendReading("humAire", hum)
     - sendReading("humSuelo", soilPercent)
     - sendReading("luz", lux)
  3. Cada 10s: consulta COMANDOS por comandos pendientes (GET)
     - Filtra: ?estado=eq.pendiente&order=creadoen.asc&limit=1
  4. Ejecuta comando en GPIO según la acción
  5. Marca comando como "ejecutado" (PATCH)

ENDPOINTS REST QUE USA EL ARDUINO:
  - POST /rest/v1/rpc/insertar_lectura  → enviar lectura
  - GET  /rest/v1/comandos?select=idcomando,accion&estado=eq.pendiente&order=creadoen.asc&limit=1
  - PATCH /rest/v1/comandos?idcomando=eq.{id}  → marcar ejecutado (Body: {"estado":"ejecutado"})

PARA CARGAR:
  Opción A: Arduino IDE
    1. Instalar soporte ESP32 (preferencias → URL placas)
    2. Instalar librerías: ArduinoJson + DHT sensor library
    3. Editar WIFI_SSID y WIFI_PASS
    4. Seleccionar placa "ESP32 Dev Module", puerto COM
    5. Subir

  Opción B: PlatformIO (recomendado)
    1. Instalar VSCode + PlatformIO extension
    2. File → Open Folder → arduino/
    3. PlataformaIO ya tiene platformio.ini configurado
    4. Compilar y subir

============================================================================
## 7. HISTORIAL DE TRABAJO (resumen)
============================================================================
DÍA 1:
  - Clonado repo original de DalejoCh07/ProyectoIntegradorPAO3
  - Analizada arquitectura original (SQL Server, 4 capas)
  - Migrado de System.Data.SqlClient a Npgsql 8.0.5
  - Creado schema PostgreSQL y subido a Supabase
  - Conectada app WinForms a Supabase
  - Rediseñado dashboard con cards, chart, tablas
  - Creado menú lateral moderno

DÍA 2:
  - Arregladas todas las warnings (50 → 0)
  - Agregado System.Data.SqlClient (para chart en PC amigo)
  - Limpiado DataAccess.cs (try/catch redundantes)
  - Creada tabla COMANDOS para control de dispositivos
  - Creado panel web mobile (HTML + Supabase JS)
  - Desplegado en GitHub Pages
  - Creada función RPC insertar_lectura()
  - Creado sketch ESP32 completo
  - Configuradas RLS policies para anon key
  - Creado este archivo CONTEXT.md

============================================================================
## 8. COSAS PENDIENTES PARA MAÑANA
============================================================================
[ ] 1. Probar panel web desde el celular (abrir la URL)
[ ] 2. Cablear físicamente el ESP32 con sensores y relés
[ ] 3. Subir el sketch al ESP32 y verificar que envía lecturas
[ ] 4. Probar que los comandos desde el panel web llegan al ESP32
[ ] 5. Probar la app WinForms desde la PC del amigo (daalc)
[ ] 6. Ajustar mapeo sensor suelo (invertir 0-100 si es necesario)
[ ] 7. Agregar Timer de refresh automático en dashboard.cs
[ ] 8. Mejorar readingLog.cs con filtros por fecha y tipo
[ ] 9. Mover cadena de conexión a appsettings.json
[ ] 10. Opcional: dominio personalizado para el panel web

============================================================================
## 9. COMANDOS RÁPIDOS
============================================================================
Compilar app:      dotnet build (desde C:\Users\Kleverson\Desktop\proyectoflor)
Ejecutar app:      dotnet run --project AgroControl
Git add+commit+push: git add -A; git commit -m "msg"; git push
Iniciar opencode:  opencode (en la terminal)
Ver panel web:     ir a https://dilankl30.github.io/ProyectoControl/
Dashboard Supabase: https://supabase.com/dashboard/project/ezuqrhdtpwbxnatzlzrt
Repositorio:       https://github.com/Dilankl30/ProyectoControl
