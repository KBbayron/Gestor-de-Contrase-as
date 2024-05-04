create database SecureKeyGenData;

use SecureKeyGenData;

CREATE TABLE pssw(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [name] VARCHAR(25) NOT NULL,
	[user] VARCHAR(50),
    pass VARCHAR(50) NOT NULL,
    [url] VARCHAR(250),
	[status] INT DEFAULT 1 -- Establece el valor predeterminado en 1 para la columna 'status'
);

INSERT INTO pssw ([name], [user], pass, [url])
VALUES ('Correo Personal', 'usuario_personal', 'contrase�a_personal', 'http://www.correo.com'), 
       ('Redes Sociales', 'usuario_redes', 'contrase�a_redes', 'http://www.redes.com'), 
       ('Banca en L�nea', 'usuario_banca', 'contrase�a_banca', 'http://www.banca.com'), 
       ('Correo Corporativo', 'usuario_corporativo', 'contrase�a_corporativa', 'http://www.corporativo.com'), 
       ('Compras en L�nea', 'usuario_compras', 'contrase�a_compras', 'http://www.compras.com');

SELECT * FROM pssw;


