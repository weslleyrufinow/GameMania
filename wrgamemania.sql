BEGIN TRANSACTION;

CREATE TABLE IF NOT EXISTS "Jogo" (
	"ID"				INTEGER NOT NULL UNIQUE,
	"Nome"				TEXT NOT NULL,
	"Edicao"			TEXT NOT NULL,
	"Descricao"			TEXT,
	"Disponibilidade"	BOOLEAN NOT NULL DEFAULT 1,
	PRIMARY KEY("ID" AUTOINCREMENT),
	UNIQUE ("Nome", "Edicao")
);

CREATE TABLE IF NOT EXISTS "Genero" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Nome"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Studio" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Nome"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Plataforma" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Nome"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Nota" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Nota"	INTEGER NOT NULL UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);

CREATE TABLE IF NOT EXISTS "JogoGenero" (
	"JogoID"	INTEGER NOT NULL,
	"GeneroID"	INTEGER NOT NULL,
	PRIMARY KEY("JogoID", "GeneroID"),
	FOREIGN KEY("JogoID") REFERENCES "Jogo"("ID"),
	FOREIGN KEY("GeneroID") REFERENCES "Genero"("ID")
);

CREATE TABLE IF NOT EXISTS "JogoStudio" (
	"JogoID"	INTEGER NOT NULL,
	"StudioID"	INTEGER NOT NULL,
	PRIMARY KEY("JogoID", "StudioID"),
	FOREIGN KEY("JogoID") REFERENCES "Jogo"("ID"),
	FOREIGN KEY("StudioID") REFERENCES "Studio"("ID")
);
CREATE TABLE IF NOT EXISTS "JogoPlataforma" (
	"JogoID"	INTEGER NOT NULL,
	"PlataformaID"	INTEGER NOT NULL,
	PRIMARY KEY("JogoID", "PlataformaID"),
	FOREIGN KEY("JogoID") REFERENCES "Jogo"("ID"),
	FOREIGN KEY("PlataformaID") REFERENCES "Plataforma"("ID")
);
CREATE TABLE IF NOT EXISTS "JogoNota" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"JogoID"	INTEGER NOT NULL,
	"NotaID"	INTEGER NOT NULL,
	PRIMARY KEY("ID"),
	FOREIGN KEY("JogoID") REFERENCES "Jogo"("ID"),
	FOREIGN KEY("NotaID") REFERENCES "Nota"("ID")
);

COMMIT;


BEGIN TRANSACTION;
INSERT INTO "Genero" ("Nome") 
VALUES	("acao"),("aventura"),("rpg"),("fps"),("corrida"),("tps"),
		("singleplayer"),("multiplayer"),("mmo"),("sobrevivencia"),("terror");

INSERT INTO "Studio" ("Nome") 
VALUES	("ubisoft"),("rockstar north"),("sabotage"),("devolver"),("ea"),("capcom"),("sega"),("niantic"),("bethesda");

INSERT INTO "Plataforma" ("Nome") 
VALUES	("windows"),("mac"),("linux"),
		("android"),("ios"),
		("xbox"),("xbox 360"),("xbox one"),("xbox sx"),
		("ps 1"),("ps 3"),("ps 4"),("ps 5"),
		("switch");

INSERT INTO "Nota" ("Nota") 
VALUES (0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10);
COMMIT;

/*
BEGIN TRANSACTION;
DROP TABLE IF EXISTS Jogo;
DROP TABLE IF EXISTS Genero;
DROP TABLE IF EXISTS Studio;
DROP TABLE IF EXISTS Plataforma;
DROP TABLE IF EXISTS Nota;
DROP TABLE IF EXISTS JogoGenero;
DROP TABLE IF EXISTS JogoStudio;
DROP TABLE IF EXISTS JogoPlataforma;
DROP TABLE IF EXISTS JogoNota;
COMMIT;
*/