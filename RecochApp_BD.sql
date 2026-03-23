CREATE DATABASE RecochApp
GO
USE RecochApp


CREATE TABLE usuarios (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    correo VARCHAR(100) UNIQUE,
    password_hash VARCHAR(255),
    fecha_nacimiento DATE
);

CREATE TABLE modos_juego (
    id_modo INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    cantidad_jugadores INT,
    tiempo_minutos INT
);


CREATE TABLE salas (
    id_sala INT IDENTITY(1,1) PRIMARY KEY,
    codigo VARCHAR(50) UNIQUE,
    id_anfitrion INT,
    FOREIGN KEY (id_anfitrion) REFERENCES usuarios(id_usuario)
);

CREATE TABLE participantes (
    id_participante INT IDENTITY(1,1) PRIMARY KEY,
    id_usuario INT,
    id_sala INT,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    FOREIGN KEY (id_sala) REFERENCES salas(id_sala)
);


CREATE TABLE categorias (
    id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100)
);


CREATE TABLE contenidos (
    id_contenido INT IDENTITY(1,1) PRIMARY KEY,
    texto VARCHAR(255),
    tipo VARCHAR(50), -- pregunta o reto
    id_categoria INT,
    FOREIGN KEY (id_categoria) REFERENCES categorias(id_categoria)
);

-- Tabla intermedia (MUCHOS A MUCHOS)
CREATE TABLE modos_contenidos (
    id_modo INT,
    id_contenido INT,
    PRIMARY KEY (id_modo, id_contenido),
    FOREIGN KEY (id_modo) REFERENCES modos_juego(id_modo),
    FOREIGN KEY (id_contenido) REFERENCES contenidos(id_contenido)
);


CREATE TABLE turnos (
    id_turno INT IDENTITY(1,1) PRIMARY KEY,
    id_sala INT,
    id_participante INT,
    orden INT,
    estado VARCHAR(50), -- pendiente, completado
    FOREIGN KEY (id_sala) REFERENCES salas(id_sala),
    FOREIGN KEY (id_participante) REFERENCES participantes(id_participante)
);