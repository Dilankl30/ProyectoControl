#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <DHT.h>

// ===== CONFIGURATION =====
const char* WIFI_SSID = "TU_WIFI";
const char* WIFI_PASS = "TU_CONTRASENA";

const char* SUPABASE_URL = "https://ezuqrhdtpwbxnatzlzrt.supabase.co";
const char* SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImV6dXFyaGR0cHdieG5hdHpsenJ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3ODE1NjQ2NjksImV4cCI6MjA5NzE0MDY2OX0.TfTMZaKQG80jalMpoGwPuteGyFiAsnMs1mvvuNhnxt8";

// ===== SENSOR PINS =====
#define DHTPIN 4
#define DHTTYPE DHT22
DHT dht(DHTPIN, DHTTYPE);

#define SOIL_PIN 34
#define LDR_PIN 35

// ===== ACTUATOR PINS =====
#define LED_PIN 16
#define PUMP_PIN 17
#define FAN_PIN 18

// ===== TIMING (milliseconds) =====
const unsigned long SEND_INTERVAL = 30000;
const unsigned long COMMAND_INTERVAL = 10000;

unsigned long lastSend = 0;
unsigned long lastCommandCheck = 0;

void setup() {
  Serial.begin(115200);
  pinMode(LED_PIN, OUTPUT);
  pinMode(PUMP_PIN, OUTPUT);
  pinMode(FAN_PIN, OUTPUT);
  digitalWrite(LED_PIN, LOW);
  digitalWrite(PUMP_PIN, LOW);
  digitalWrite(FAN_PIN, LOW);
  dht.begin();
  connectWiFi();
}

void loop() {
  unsigned long now = millis();

  if (now - lastSend >= SEND_INTERVAL) {
    lastSend = now;
    sendReadings();
  }

  if (now - lastCommandCheck >= COMMAND_INTERVAL) {
    lastCommandCheck = now;
    checkCommands();
  }
}

void connectWiFi() {
  Serial.print("Connecting to WiFi");
  WiFi.begin(WIFI_SSID, WIFI_PASS);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi connected. IP: " + WiFi.localIP().toString());
}

void sendReadings() {
  float temp = dht.readTemperature();
  float hum = dht.readHumidity();
  int soilRaw = analogRead(SOIL_PIN);
  int ldrRaw = analogRead(LDR_PIN);

  float soilPercent = map(constrain(soilRaw, 1200, 3600), 1200, 3600, 100, 0);
  float lux = map(ldrRaw, 0, 4095, 0, 2000);

  if (isnan(temp) || isnan(hum)) {
    Serial.println("DHT sensor error");
    return;
  }

  sendReading("tempAire", temp);
  delay(100);
  sendReading("humAire", hum);
  delay(100);
  sendReading("humSuelo", soilPercent);
  delay(100);
  sendReading("luz", lux);
}

void sendReading(const char* tipo, float valor) {
  HTTPClient http;
  String url = String(SUPABASE_URL) + "/rest/v1/rpc/insertar_lectura";
  http.begin(url);
  http.addHeader("apikey", SUPABASE_KEY);
  http.addHeader("Authorization", "Bearer " + String(SUPABASE_KEY));
  http.addHeader("Content-Type", "application/json");

  StaticJsonDocument<128> doc;
  doc["p_tipo"] = tipo;
  doc["p_valor"] = valor;
  String body;
  serializeJson(doc, body);

  int code = http.POST(body);
  Serial.printf("[SEND] %s = %.1f → HTTP %d\n", tipo, valor, code);
  http.end();
}

void checkCommands() {
  HTTPClient http;
  String url = String(SUPABASE_URL) + "/rest/v1/comandos?select=idcomando,accion&estado=eq.pendiente&order=creadoen.asc&limit=1";
  http.begin(url);
  http.addHeader("apikey", SUPABASE_KEY);
  http.addHeader("Authorization", "Bearer " + String(SUPABASE_KEY));

  int code = http.GET();
  if (code != 200) {
    http.end();
    return;
  }

  String response = http.getString();
  http.end();

  DynamicJsonDocument doc(256);
  DeserializationError err = deserializeJson(doc, response);
  if (err || !doc.is<JsonArray>() || doc.as<JsonArray>().size() == 0) return;

  JsonObject cmd = doc.as<JsonArray>()[0];
  int idComando = cmd["idcomando"];
  const char* accion = cmd["accion"];

  Serial.printf("[CMD] #%d → %s\n", idComando, accion);

  if (strcmp(accion, "led_on") == 0) { digitalWrite(LED_PIN, HIGH); Serial.println("  → LED ON"); }
  else if (strcmp(accion, "led_off") == 0) { digitalWrite(LED_PIN, LOW); Serial.println("  → LED OFF"); }
  else if (strcmp(accion, "bomba_on") == 0) { digitalWrite(PUMP_PIN, HIGH); Serial.println("  → PUMP ON"); }
  else if (strcmp(accion, "bomba_off") == 0) { digitalWrite(PUMP_PIN, LOW); Serial.println("  → PUMP OFF"); }
  else if (strcmp(accion, "ventilador_on") == 0) { digitalWrite(FAN_PIN, HIGH); Serial.println("  → FAN ON"); }
  else if (strcmp(accion, "ventilador_off") == 0) { digitalWrite(FAN_PIN, LOW); Serial.println("  → FAN OFF"); }
  else { Serial.println("  → Unknown command"); return; }

  markExecuted(idComando);
}

void markExecuted(int idComando) {
  HTTPClient http;
  String url = String(SUPABASE_URL) + "/rest/v1/comandos?idcomando=eq." + String(idComando);
  http.begin(url);
  http.addHeader("apikey", SUPABASE_KEY);
  http.addHeader("Authorization", "Bearer " + String(SUPABASE_KEY));
  http.addHeader("Content-Type", "application/json");

  StaticJsonDocument<64> patch;
  patch["estado"] = "ejecutado";
  String body;
  serializeJson(patch, body);

  int code = http.PATCH(body);
  Serial.printf("  → Marked executed (HTTP %d)\n", code);
  http.end();
}
