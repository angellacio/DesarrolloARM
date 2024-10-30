use MotorTraductor
GO

SET NOCOUNT ON

--- ** * * * * * * * * CatConceptoDyP  * * * * * * * * 
PRINT 'CatConceptoDyP'
GO

INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0139', 'ISR/ISR POR ENAJENACIÓN DE ACCIONES EN VOLSA DE VALORES',1);
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0145', 'ISR OPCIÓN DE ACUMULACIÓN DE INGRESOS POR PERSONAS MORALES',1)
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0147', 'ISR/PERSONAS FÍSICAS, ACTIVIDAD EMPRESARIAL/RF', 1)
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0148', 'ISR/PERSONAS FÍSICAS, SERVICIOS PROFESIONALES', 1)
INSERT INTO CatConceptoDyP (ConceptoDyP, Descripcion, Activo) VALUES ('0159', 'ISR/PERSONAS MORALES REGIÓN FRONTERIZA NORTE (RFN)', 1)
GO





--- CatConceptoPersonaPeriodicidad
PRINT 'CatConceptoPersonaPeriodicidad'
GO
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0101', 4, 'S');

insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0113', 2, 'Q');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0113', 4, 'Q');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('303147', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('303147', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308126', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308126', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308127', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308127', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308128', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308128', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308129', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308129', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308130', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308130', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308131', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308131', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308132', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308132', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308133', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308133', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308134', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('308134', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('167', 4, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('168', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0139', 2, 'Y');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0131', 4, 'Y');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0145', 4, 'M');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0145', 4, 'Y');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0147', 2, 'M');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0147', 2, 'T');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0147', 2, 'S');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0147', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0147', 2, 'Y');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0148', 2, 'M');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0148', 2, 'T');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0148', 2, 'S');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0148', 2, 'N');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0148', 2, 'Y');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0159', 4, 'M');
insert into CatConceptoPersonaPeriodicidad (ConceptoDyP, IdTipoPersona, IdPeriodicidadDyP) VALUES ('0159', 4, 'Y');


GO

--- CatConceptoEquivalencia
PRINT 'CatConceptoEquivalencia'
GO

INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (2, '110076', 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', '0113', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (3, '110076', 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', '0113', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (4, '110076', 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', '0113', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (5, '110076', 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', '0113', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (2, '110063', 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', '0139', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (3, '110063', 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', '0139', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (4, '110063', 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', '0139', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (5, '110063', 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', '0139', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (2, '110064', 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', '0131', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (3, '110064', 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', '0131', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (4, '110064', 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', '0131', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (5, '110064', 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', '0131', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (2, '110073', 'ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', '0147', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (3, '110073', 'ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', '0147', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (4, '110073', 'ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', '0147', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (5, '110073', 'ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', '0147', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (2, '110074', 'ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', '0148', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (3, '110074', 'ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', '0148', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (4, '110074', 'ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', '0148', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (5, '110074', 'ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', '0148', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (2, '110001', 'ISR OPCION DE ACUMULACION DE INGRESOS POR PERSONAS MORALES', '0145', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (3, '110001', 'ISR OPCION DE ACUMULACION DE INGRESOS POR PERSONAS MORALES', '0145', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (4, '110001', 'ISR OPCION DE ACUMULACION DE INGRESOS POR PERSONAS MORALES', '0145', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (5, '110001', 'ISR OPCION DE ACUMULACION DE INGRESOS POR PERSONAS MORALES', '0145', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (2, '110001', 'ISR/ ISR PERSONAS MORALES REGION FRONTERIZA NORTE (RFN)', '0159', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (3, '110001', 'ISR/ ISR PERSONAS MORALES REGION FRONTERIZA NORTE (RFN)', '0159', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (4, '110001', 'ISR/ ISR PERSONAS MORALES REGION FRONTERIZA NORTE (RFN)', '0159', 1);
INSERT INTO CatConceptoEquivalencia (IdAplicacion, ConceptoOrigen, DescripcionOrigen, ConceptoDyP, Activo) VALUES (5, '110001', 'ISR/ ISR PERSONAS MORALES REGION FRONTERIZA NORTE (RFN)', '0159', 1);

GO





---  CatConceptosOriginal 
PRINT 'CatConceptosOriginal'
GO
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110076', 2, 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110076', 3, 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110076', 4, 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110076', 5, 'IMPUESTO SOBRE LA RENTA, PAGOS PROVISIONALES, RETENEDORES, PERSONAS MORALES Y FÍSICAS POR ASIMILADOS A SALARIOS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110063', 2, 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110063', 3, 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110063', 4, 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110063', 5, 'ISR  POR ENAJENACIÓN DE ACCIONES EN BOLSA DE VALORES, PERSONAS FÍSICAS', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110064', 2, 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110064', 3, 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110064', 4, 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110064', 5, 'ISR PERSONAS MORALES. RÉGIMEN OPCIONAL PARA GRUPOS DE SOCIEDADES', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110073', 2, 'ISR / ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110073', 3, 'ISR / ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110073', 4, 'ISR / ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110073', 5, 'ISR / ISR PERSONAS FÍSICAS. ACTIVIDAD EMPRESARIAL / RF', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110074', 2, 'ISR / ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110074', 3, 'ISR / ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110074', 4, 'ISR / ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', 1);
INSERT INTO CatConceptosOriginal (Clave, IdAplicacion, Descripcion, Activo) VALUES ('110074', 5, 'ISR / ISR PERSONAS FÍSICAS. SERVICIOS PROFESIONALES', 1);

GO

SET NOCOUNT OFF
--ROLLBACK TRAN