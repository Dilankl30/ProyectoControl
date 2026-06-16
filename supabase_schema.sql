-- ============================================
-- Script para crear la BD en Supabase (PostgreSQL)
-- Proyecto: AgroControl
-- ============================================

-- 1. Tabla de invernaderos
CREATE TABLE INVERNADERO (
    idInvernadero SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    ubicacion VARCHAR(255)
);

-- 2. Tabla de usuarios
CREATE TABLE USUARIOS (
    idUsuario SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    tipoUsuario VARCHAR(50) NOT NULL DEFAULT 'operador'
);

-- 3. Tabla de plantas
CREATE TABLE PLANTA (
    idPlanta SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    nombreCien VARCHAR(150),
    descripcion TEXT
);

-- 4. Tabla de sensores
CREATE TABLE SENSOR (
    idSensor SERIAL PRIMARY KEY,
    tipo VARCHAR(50) NOT NULL,
    idInvernadero INT NOT NULL REFERENCES INVERNADERO(idInvernadero) ON DELETE CASCADE
);

-- 5. Tabla de lecturas
CREATE TABLE LECTURA (
    idLectura SERIAL PRIMARY KEY,
    idSensor INT NOT NULL REFERENCES SENSOR(idSensor) ON DELETE CASCADE,
    fechaHora TIMESTAMP NOT NULL DEFAULT NOW(),
    valor NUMERIC(10, 2) NOT NULL
);

-- Índices para mejorar rendimiento en consultas frecuentes
CREATE INDEX idx_lectura_fecha ON LECTURA(fechaHora DESC);
CREATE INDEX idx_lectura_sensor ON LECTURA(idSensor);
CREATE INDEX idx_sensor_invernadero ON SENSOR(idInvernadero);

-- Datos iniciales de ejemplo
INSERT INTO INVERNADERO (nombre, ubicacion) VALUES
    ('Invernadero Central', 'Zona A'),
    ('Invernadero Norte', 'Zona B');

INSERT INTO USUARIOS (nombre, contrasena, tipoUsuario) VALUES
    ('admin', 'admin123', 'administrador'),
    ('operador1', '123456', 'operador');

INSERT INTO PLANTA (nombre, nombreCien, descripcion) VALUES
    ('Rosa', 'Rosa spp.', 'Rosa de jardín variedad híbrida'),
    ('Orquídea', 'Orchidaceae', 'Orquídea tropical');

INSERT INTO SENSOR (tipo, idInvernadero) VALUES
    ('humSuelo', 1),
    ('tempAire', 1),
    ('humAire', 1),
    ('luz', 1);

INSERT INTO LECTURA (idSensor, fechaHora, valor) VALUES
    (1, NOW() - INTERVAL '1 hour', 45.5),
    (2, NOW() - INTERVAL '1 hour', 28.3),
    (3, NOW() - INTERVAL '1 hour', 65.0),
    (4, NOW() - INTERVAL '1 hour', 1200.0);
