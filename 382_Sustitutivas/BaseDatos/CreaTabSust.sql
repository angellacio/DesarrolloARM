DROP TABLE d_cbSerSustitutivasRes;
DROP TABLE m_cbSerSustitutivas;
DROP TABLE c_cvInformaticaConcepto;

CREATE TABLE c_cvInformaticaConcepto(nIDCveInformatica INT PRIMARY KEY,
sIDConcepto nVarChar(6) NOT NULL) IN dbtransac02 EXTENT SIZE 16 NEXT SIZE 16;

CREATE INDEX inx_CatConcepto_CveInformatica ON c_cvInformaticaConcepto(sIDConcepto) IN dbixtransac02;

CREATE TABLE m_cbSerSustitutivas(nID SERIAL PRIMARY KEY,
sRFC nVarChar(13) NOT NULL,
nEjercicio Int NOT NULL,
sIDPeriodo nVarChar(3) NOT NULL,
dRecepcion DATETIME YEAR TO SECOND NOT NULL,
sIDBanco nVarChar(5) NOT NULL,
nIDCveInformatica int NOT NULL,
nRenglones INT,
dModificacion DATETIME YEAR TO SECOND,
nEstatus Int NOT NULL,
sEquipo nVarChar(250),
nIntentos Int,
FOREIGN KEY (nIDCveInformatica) REFERENCES c_cvInformaticaConcepto(nIDCveInformatica))
IN dbtransac02 EXTENT SIZE 16 NEXT SIZE 16;

CREATE INDEX inx_SerSustitutivas_sRFC ON m_cbSerSustitutivas(sRFC)
IN dbixtransac02;

CREATE TABLE d_cbSerSustitutivasRes(nID Int NOT NULL,
nConsecutivo Int NOT NULL,
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
FOREIGN KEY (nId) REFERENCES m_cbSerSustitutivas(nID))
IN dbtransac02 EXTENT SIZE 16 NEXT SIZE 16;

ALTER TABLE d_cbSerSustitutivasRes ADD CONSTRAINT PRIMARY KEY (nId,nConsecutivo);
