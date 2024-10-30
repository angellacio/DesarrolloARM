use MotorTraductor

INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('802', 'ISR / ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL. PEQUEÑOS CONTRIBUYENTES',1);
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('138', 'ISR / ISR RETENCIONES POR DIVIDENDOS',1)
GO

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '802' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'M')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('802', 2, 'M');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '802' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'T')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('802', 2, 'T');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '802' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'B')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('802', 2, 'B');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '802' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'S')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('802', 2, 'S');

if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '802' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'Q')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('802', 2, 'Q');


if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '138' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'Y')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('138', 2, 'Y');



if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '138' and IdTipoPersona = 4 and IdPeriodicidadDyP = 'Y')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('138', 4, 'Y');



if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '318' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'M')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('318', 2, 'M');



if not exists (select conceptoDyP from CatConceptoPersonaPeriodicidad where conceptoDyP = '318' and IdTipoPersona = 2 and IdPeriodicidadDyP = 'Y')
INSERT INTO CatConceptoPersonaPeriodicidad (conceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES('318', 2, 'Y');




