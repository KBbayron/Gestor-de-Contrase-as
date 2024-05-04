CREATE DATABASE IF NOT EXISTS SecureKeyGenData;

USE SecureKeyGenData;

CREATE TABLE pssw (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(25) NOT NULL,
    `user` VARCHAR(50),
    pass VARCHAR(50) NOT NULL,
    url VARCHAR(250),
    `status` INT DEFAULT 1
);

INSERT INTO pssw (name, user, pass, url)
VALUES ('Correo Personal', 'usuario_personal', 'contraseña_personal', 'http://www.correo.com'), 
       ('Redes Sociales', 'usuario_redes', 'contraseña_redes', 'http://www.redes.com'), 
       ('Banca en Línea', 'usuario_banca', 'contraseña_banca', 'http://www.banca.com'), 
       ('Correo Corporativo', 'usuario_corporativo', 'contraseña_corporativa', 'http://www.corporativo.com'), 
       ('Compras en Línea', 'usuario_compras', 'contraseña_compras', 'http://www.compras.com');

select * from pssw;