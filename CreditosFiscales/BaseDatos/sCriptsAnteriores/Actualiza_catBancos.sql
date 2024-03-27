use MotorTraductor
go

if not exists (select idBanco from catBanco where idBanco = 40003)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40003, 'Banca Serfin, S.A.');

if not exists (select idBanco from catBanco where idBanco = 40025)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40025, 'Banco del Sureste, S.A.');

if not exists (select idBanco from catBanco where idBanco = 40090)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40090, 'TESOFE');

if not exists (select idBanco from catBanco where idBanco = 40107)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40107, 'BankBoston, S.A.');

if not exists (select idBanco from catBanco where idBanco = 40113)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40113, 'Banco Ve por Mas, S. A.');

if not exists (select idBanco from catBanco where idBanco = 40116)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40116, 'ING Bank (Mexico), S.A.');

if not exists (select idBanco from catBanco where idBanco = 40127)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40127, 'Banco Azteca, S.A.');

if not exists (select idBanco from catBanco where idBanco = 40128)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40128, 'Banco Autofin Mexico, S.A.');

if not exists (select idBanco from catBanco where idBanco = 40133)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40133, 'Banco Actinver, S.A., Grupo Financiero');

if not exists (select idBanco from catBanco where idBanco = 40136)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40136, 'Intercam Banco, S.A. de Banca Multiple');

if not exists (select idBanco from catBanco where idBanco = 40145)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40145, 'Banco Base, S.A.');

if not exists (select idBanco from catBanco where idBanco = 40152)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40152, 'Banco Bancrea, S.A.');

if not exists (select idBanco from catBanco where idBanco = 40161)
INSERT INTO catBanco (idBanco, Descripcion) VALUES(40161, 'Bancrecer, S.A.');


go


