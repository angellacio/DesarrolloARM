DROP TABLE CatMateriaConcepto;
DROP TABLE SerSustitutivasRes;
DROP TABLE SerSustitutivas;

CREATE TABLE SerSustitutivas(nID SERIAL PRIMARY KEY,
sRFC nVarChar(13) NOT NULL,
nEjercicio Int NOT NULL,
sIDPeriodo nVarChar(3) NOT NULL,
dRecepcion DATETIME YEAR TO SECOND NOT NULL,
sIDBanco nVarChar(5) NOT NULL,
nIDCveInformatica INT NOT NULL,
sIDConcepto nVarChar(6),
nRenglones INT,
dModificacion DATETIME YEAR TO SECOND,
nEstatus Int NOT NULL,
sEquipo nVarChar(250),
nIntentos Int);

CREATE INDEX inx_SerSustitutivas_sRFC ON SerSustitutivas(sRFC);

CREATE TABLE SerSustitutivasRes(nId SERIAL PRIMARY KEY,
nIdSusWSM Int NOT NULL,
sPagado nVarChar(2) NOT NULL,
sLineaCaptura nVarChar(20),
sTipoOperacion nVarChar(2) NOT NULL,
sMensaje nVarChar(250) NOT NULL,
sNumOperBanco nVarChar(14),
sFOperacion nVarChar(10),
sHOperacion nVarChar(5),
sNumOpeDecla nVarChar(14),
sPresentacion nVarChar(8),
dModificacion DATETIME YEAR TO SECOND,
nEstatus Int,
dOperacion DATETIME YEAR TO SECOND,
dPresentacion DATETIME YEAR TO SECOND,
FOREIGN KEY (nIdSusWSM) REFERENCES SerSustitutivas(nID)
);

CREATE TABLE CatInformaConcepto(nIDCveInformatica INT PRIMARY KEY,
sIDConcepto nVarChar(6) NOT NULL,
);
CREATE INDEX inx_CatInformaConcepto_nIdMateria ON CatInformaConcepto(nIDCveInformatica);