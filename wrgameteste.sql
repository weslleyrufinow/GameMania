/*
BEGIN TRANSACTION;

INSERT INTO "Jogo" ("Nome", "Edicao")
VALUES ("far cry", "3");

INSERT INTO "JogoGenero" ("JogoID", "GeneroID")
VALUES 
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Genero" WHERE "Nome" = "acao")),
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Genero" WHERE "Nome" = "aventura")),
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Genero" WHERE "Nome" = "fps"));

INSERT INTO "JogoStudio" ("JogoID", "StudioID")
VALUES ((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Studio" WHERE "Nome" = "ubisoft"));

INSERT INTO "JogoPlataforma" ("JogoID", "PlataformaID")
VALUES 
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "windows")),
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "xbox 360")),
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "ps 3"));

INSERT INTO "JogoNota" ("JogoID", "NotaID")
VALUES 
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Nota" WHERE "Nota" = 5)),
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Nota" WHERE "Nota" = 4)),
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Nota" WHERE "Nota" = 3)),
((SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3"), (SELECT "ID" FROM "Nota" WHERE "Nota" = 5));

COMMIT;
*/
BEGIN TRANSACTION;

INSERT INTO "Jogo" ("Nome", "Edicao")
VALUES ("far cry", "3");

CREATE TEMPORARY TABLE TempIDs AS
SELECT 
    (SELECT "ID" FROM "Jogo" WHERE "Nome" = "far cry" AND "Edicao" = "3") AS JogoID,
    (SELECT "ID" FROM "Genero" WHERE "Nome" = "acao") AS GeneroAcaoID,
    (SELECT "ID" FROM "Genero" WHERE "Nome" = "aventura") AS GeneroAventuraID,
    (SELECT "ID" FROM "Genero" WHERE "Nome" = "fps") AS GeneroFPSID,
    (SELECT "ID" FROM "Studio" WHERE "Nome" = "ubisoft") AS StudioUbisoftID,
    (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "windows") AS PlataformaWindowsID,
    (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "xbox 360") AS PlataformaXbox360ID,
    (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "ps 3") AS PlataformaPS3ID,
    (SELECT "ID" FROM "Nota" WHERE "Nota" = 10) AS Nota5ID,
    (SELECT "ID" FROM "Nota" WHERE "Nota" = 9) AS Nota4ID,
    (SELECT "ID" FROM "Nota" WHERE "Nota" = 8) AS Nota3ID;

INSERT INTO "JogoGenero" ("JogoID", "GeneroID")
    VALUES 
        ((SELECT JogoID FROM TempIDs), (SELECT GeneroAcaoID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT GeneroAventuraID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT GeneroFPSID FROM TempIDs));

INSERT INTO "JogoStudio" ("JogoID", "StudioID")
    VALUES ((SELECT JogoID FROM TempIDs), (SELECT StudioUbisoftID FROM TempIDs));

INSERT INTO "JogoPlataforma" ("JogoID", "PlataformaID")
    VALUES 
        ((SELECT JogoID FROM TempIDs), (SELECT PlataformaWindowsID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT PlataformaXbox360ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT PlataformaPS3ID FROM TempIDs));

INSERT INTO "JogoNota" ("JogoID", "NotaID")
    VALUES 
        ((SELECT JogoID FROM TempIDs), (SELECT Nota5ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT Nota4ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT Nota3ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT Nota5ID FROM TempIDs));

DROP TABLE TempIDs;

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "Jogo" ("Nome", "Edicao")
VALUES ("fallout", "3");

CREATE TEMPORARY TABLE TempIDs AS
SELECT 
    (SELECT "ID" FROM "Jogo" WHERE "Nome" = "fallout" AND "Edicao" = "3") AS JogoID,
    (SELECT "ID" FROM "Genero" WHERE "Nome" = "rpg") AS GeneroAcaoID,
    (SELECT "ID" FROM "Genero" WHERE "Nome" = "tps") AS GeneroAventuraID,
    (SELECT "ID" FROM "Studio" WHERE "Nome" = "bethesda") AS StudioUbisoftID,
    (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "windows") AS PlataformaWindowsID,
    (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "xbox one") AS PlataformaXbox360ID,
    (SELECT "ID" FROM "Plataforma" WHERE "Nome" = "ps 4") AS PlataformaPS3ID,
    (SELECT "ID" FROM "Nota" WHERE "Nota" = 8) AS Nota5ID,
    (SELECT "ID" FROM "Nota" WHERE "Nota" = 7) AS Nota4ID,
    (SELECT "ID" FROM "Nota" WHERE "Nota" = 8) AS Nota3ID;

INSERT INTO "JogoGenero" ("JogoID", "GeneroID")
    VALUES 
        ((SELECT JogoID FROM TempIDs), (SELECT GeneroAcaoID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT GeneroAventuraID FROM TempIDs));

INSERT INTO "JogoStudio" ("JogoID", "StudioID")
    VALUES ((SELECT JogoID FROM TempIDs), (SELECT StudioUbisoftID FROM TempIDs));

INSERT INTO "JogoPlataforma" ("JogoID", "PlataformaID")
    VALUES 
        ((SELECT JogoID FROM TempIDs), (SELECT PlataformaWindowsID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT PlataformaXbox360ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT PlataformaPS3ID FROM TempIDs));

INSERT INTO "JogoNota" ("JogoID", "NotaID")
    VALUES 
        ((SELECT JogoID FROM TempIDs), (SELECT Nota5ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT Nota4ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT Nota3ID FROM TempIDs)),
        ((SELECT JogoID FROM TempIDs), (SELECT Nota5ID FROM TempIDs));

DROP TABLE TempIDs;

COMMIT;

