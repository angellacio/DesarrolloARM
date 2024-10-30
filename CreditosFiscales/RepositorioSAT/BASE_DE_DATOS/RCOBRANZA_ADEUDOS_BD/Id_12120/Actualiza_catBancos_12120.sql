use MotorTraductor
go


ALTER TABLE	catBanco alter column Descripcion nvarchar(100)
GO


if not exists (select idBanco from catBanco where idBanco = 37166)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(37166, 'Banco del Bienestar, SNC., Institucion de Banca de Desarrollo');


if not exists (select idBanco from catBanco where idBanco = 40147)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40147, 'Bankaool, S.A., Institucion de Banca Multiple');

go


