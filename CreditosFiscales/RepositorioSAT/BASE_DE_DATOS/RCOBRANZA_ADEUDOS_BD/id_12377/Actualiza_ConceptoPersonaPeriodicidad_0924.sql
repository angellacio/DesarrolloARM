use MotorTraductor
go

set nocount on


INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('306182', 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD).',1)
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0162', 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO',1)
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0163', 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS',1)
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0311', 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS',1)


GO


--- RN001


if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '306182' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'S')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('306182', 2, 'S');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '306182' and IdTipoPersona = 4 and IdPeriodicidadDyP = 'S')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('306182', 4, 'S');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '306182' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'N')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('306182', 2, 'N');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '306182' and IdTipoPersona = 4 and IdPeriodicidadDyP = 'N')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('306182', 4, 'N');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '302123' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'S')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('302123', 2, 'S');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '302123' and IdTipoPersona = 4 and IdPeriodicidadDyP = 'S')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('302123', 4, 'S');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '162' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'M')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('0162', 2, 'M');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '163' and IdTipoPersona = 4 and IdPeriodicidadDyP = 'M')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('0163', 4, 'M');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '0311' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'M')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('0311', 2, 'M');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '0311' and IdTipoPersona = 4 and IdPeriodicidadDyP = 'M')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('0311', 4, 'M');


---catconceptosoriginal   RN002

insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('400242', 1, 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD).', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('400242', 2, 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD).', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('400242', 3, 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD).', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('400242', 4, 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD).', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('400242', 5, 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD).', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110078', 1, 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110078', 2, 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110078', 3, 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110078', 4, 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110078', 5, 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110077', 1, 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110077', 2, 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110077', 3, 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110077', 4, 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110077', 5, 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('130019', 1, 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('130019', 2, 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('130019', 3, 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('130019', 4, 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('130019', 5, 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110075', 1, 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110075', 2, 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110075', 3, 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110075', 4, 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', 1);
insert into catconceptosoriginal (clave, idAplicacion, descripcion, activo) Values('110075', 5, 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', 1);



--- RN002 (update)

update catconceptosoriginal set activo = 0 where clave = 110018 and idAplicacion = 1
update catconceptosoriginal set activo = 0 where clave = 110018 and idAplicacion = 2
update catconceptosoriginal set activo = 0 where clave = 110018 and idAplicacion = 3
update catconceptosoriginal set activo = 0 where clave = 110018 and idAplicacion = 4
update catconceptosoriginal set activo = 0 where clave = 110018 and idAplicacion = 5
update catconceptosoriginal set activo = 0 where clave = 500077 and idAplicacion = 1
update catconceptosoriginal set activo = 0 where clave = 500077 and idAplicacion = 2
update catconceptosoriginal set activo = 0 where clave = 500077 and idAplicacion = 3
update catconceptosoriginal set activo = 0 where clave = 500077 and idAplicacion = 4
update catconceptosoriginal set activo = 0 where clave = 500077 and idAplicacion = 5

--- RN003


insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(1, '400242', 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD). ', '306182', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(2, '400242', 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD). ', '306182', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(3, '400242', 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD). ', '306182', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(4, '400242', 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD). ', '306182', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(5, '400242', 'DER / DERECHO ADICIONAL SOBRE MINERIA (ART. 269 DE LA LFD). ', '306182', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(1, '110078', 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', '0162', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(2, '110078', 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', '0162', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(3, '110078', 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', '0162', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(4, '110078', 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', '0162', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(5, '110078', 'ISR/ ISR PERSONAS FÍSICAS PLATAFORMAS TECNOLÓGICAS PAGO DEFINITIVO', '0162', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(1, '110077', 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', '0163', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(2, '110077', 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', '0163', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(3, '110077', 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', '0163', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(4, '110077', 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', '0163', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(5, '110077', 'ISR/ ISR RETENCIONES POR EL USO DE PLATAFORMAS TECNOLÓGICAS', '0163', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(1, '130019', 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', '0311', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(2, '130019', 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', '0311', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(3, '130019', 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', '0311', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(4, '130019', 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', '0311', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(5, '130019', 'IVA/ IVA RETENCIONES POR UTILIZAR PLATAFORMAS TECNOLÓGICAS', '0311', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(1, '110075', 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', '0113', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(2, '110075', 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', '0113', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(3, '110075', 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', '0113', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(4, '110075', 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', '0113', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(5, '110075', 'ISR / ISR RETENCIONES POR ASIMILADOS A SALARIOS', '0113', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(1, '110038', 'ISR / ISR RETENCIONES POR SALARIOS', '0112', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(2, '110038', 'ISR / ISR RETENCIONES POR SALARIOS', '0112', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(3, '110038', 'ISR / ISR RETENCIONES POR SALARIOS', '0112', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(4, '110038', 'ISR / ISR RETENCIONES POR SALARIOS', '0112', 1);
insert into CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen,  DescripcionOrigen, ConceptoDyP,  Activo)  VALUES(5, '110038', 'ISR / ISR RETENCIONES POR SALARIOS', '0112', 1);



--- RN003 (update)

update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 1 and ConceptoOrigen = '500007' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 2 and ConceptoOrigen = '500007' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 3 and ConceptoOrigen = '500007' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 4 and ConceptoOrigen = '500007' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 5 and ConceptoOrigen = '500007' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 1 and ConceptoOrigen = '110018' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 2 and ConceptoOrigen = '110018' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 3 and ConceptoOrigen = '110018' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 4 and ConceptoOrigen = '110018' and ConceptoDyP = '0112'
update CatConceptoEquivalencia set Activo = 0 where IdAplicacion = 5 and ConceptoOrigen = '110018' and ConceptoDyP = '0112'



