USE [MotorTraductor];
BEGIN TRAN

delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=400236 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS CON FINES TURÍSTICOS (DNR-T)' and ConceptoDyP=307182;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=400249 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS SIN FINES TURÍSTICOS (DNR-O)' and ConceptoDyP=307186;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=180003 and DescripcionOrigen='IMPUESTO POR LA ACTIVIDAD DE EXPLORACIÓN Y EXTRACCIÓN DE HIDROCARBUROS' and ConceptoDyP=0901;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=700193 and DescripcionOrigen='MULTAS PREVISTAS EN EL ART. 160 DE LA LEY NACIONAL DE EJECUCIÓN PENAL' and ConceptoDyP=506158;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=700194 and DescripcionOrigen='MULTAS IMPUESTAS POR LA OMISIÓN EN EL CUMPLIMIENTO DE LOS ARTÍCULOS 17 Y 18 DE LA LEY FEDERAL PARA LA PREVENCIÓN E IDENTIFICACIÓN DE OPERACIONES CON RECURSOS DE PROCEDENCIA ILÍCITA' and ConceptoDyP=50547;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=140042 and DescripcionOrigen='OTROS, GASOLINA (IMPORTACIÓN DE GASOLINA)' and ConceptoDyP=0442;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=140043 and DescripcionOrigen='PEMEX, DIÉSEL(IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0443;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=140044 and DescripcionOrigen='OTROS, DIÉSEL (IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0444;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=140045 and DescripcionOrigen='IMPORTACIÓN DE COMBUSTIBLES NO FÓSILES ' and ConceptoDyP=0445;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=500155 and DescripcionOrigen='IMPUESTO ESPECIAL SOBRE PRODUCCIÓN Y SERVICIOS (IMPUESTOS NO COMPRENDIDOS EN LA LEY DE INGRESOS VIGENTE, CAUSADOS EN EJERCICIOS FISCALES ANTERIORES PENDIENTES DE LIQUIDACIÓN)' and ConceptoDyP=0446;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=110081 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS FÍSICAS' and ConceptoDyP=0168;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=110083 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS MORALES' and ConceptoDyP=0167;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=130023 and DescripcionOrigen='IVA RÉGIMEN SIMPLIFICADO DE CONFIANZA' and ConceptoDyP=0318;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=400295 and DescripcionOrigen='SECRETARIA DE SALUD.' and ConceptoDyP=308126;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=400296 and DescripcionOrigen='SECRETARIA DE SEGURIDAD Y PROTECCION CIUDADANA.' and ConceptoDyP=308127;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=400297 and DescripcionOrigen='SECRETARIA DEL BIENESTAR.' and ConceptoDyP=308128;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=400298 and DescripcionOrigen='POR LA OBTENCION DE CODIGOS DE SEGURIDAD QUE SE IMPRIMAN EN LAS CAJETILLAS, ESTUCHES, EMPAQUES, ENVOLTURAS O CUALQUIER OTRO OBJETO QUE CONTENGA CIGARROS U OTROS TABACOS LABRADOS PARA VENTA EN MEXICO (ART.  53-I  DE LA LFD).' and ConceptoDyP=308129;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=400299 and DescripcionOrigen='POR LA OBTENCION DE FOLIOS ELECTRONICOS PARA LA IMPRESION DE MARBETES ELECTRONICOS QUE SE ADHIERAN A LOS ENVASES QUE CONTENGAN BEBIDAS ALCOHOLICAS A QUE SE REFIERE LA LEY DEL IMPUESTO ESPECIAL SOBRE PRODUCCION Y SERVICIOS (ART. 53-K, FRACCIÓN II DE LA LFD).' and ConceptoDyP=308130;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=500165 and DescripcionOrigen='DERECHOS DEROGADOS DE LA FISCALIA GENERAL DE LA REPUBLICA.' and ConceptoDyP=308131;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=500166 and DescripcionOrigen='DERECHO POR AUTORIZACION DE ARRIBO, DESPACHO, MANIOBRA DE FONDEO O ENMIENDA (ART. 170 DE LA LFD).' and ConceptoDyP=308132;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=500167 and DescripcionOrigen='POR EL DERECHO DE CUMPLIMIENTO DEL CODIGO INTERNACIONAL PARA LA PROTECCION DE LOS BUQUES Y DE LAS INSTALACIONES PORTUARIAS (ART. 170-G DE LA LFD).' and ConceptoDyP=308133;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=3 and ConceptoOrigen=500168 and DescripcionOrigen='DERECHOS DEROGADOS DE LA SECRETARIA DE COMUNICACIONES Y TRANSPORTES, MEDIANTE EL DECRETO DEL 7 DE DICIEMBRE DE 2020.' and ConceptoDyP=308134;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=400236 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS CON FINES TURÍSTICOS (DNR-T)' and ConceptoDyP=307182;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=400249 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS SIN FINES TURÍSTICOS (DNR-O)' and ConceptoDyP=307186;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=180003 and DescripcionOrigen='IMPUESTO POR LA ACTIVIDAD DE EXPLORACIÓN Y EXTRACCIÓN DE HIDROCARBUROS' and ConceptoDyP=0901;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=700193 and DescripcionOrigen='MULTAS PREVISTAS EN EL ART. 160 DE LA LEY NACIONAL DE EJECUCIÓN PENAL' and ConceptoDyP=506158;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=700194 and DescripcionOrigen='MULTAS IMPUESTAS POR LA OMISIÓN EN EL CUMPLIMIENTO DE LOS ARTÍCULOS 17 Y 18 DE LA LEY FEDERAL PARA LA PREVENCIÓN E IDENTIFICACIÓN DE OPERACIONES CON RECURSOS DE PROCEDENCIA ILÍCITA' and ConceptoDyP=50547;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=140042 and DescripcionOrigen='OTROS, GASOLINA (IMPORTACIÓN DE GASOLINA)' and ConceptoDyP=0442;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=140043 and DescripcionOrigen='PEMEX, DIÉSEL(IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0443;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=140044 and DescripcionOrigen='OTROS, DIÉSEL (IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0444;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=140045 and DescripcionOrigen='IMPORTACIÓN DE COMBUSTIBLES NO FÓSILES ' and ConceptoDyP=0445;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=500155 and DescripcionOrigen='IMPUESTO ESPECIAL SOBRE PRODUCCIÓN Y SERVICIOS (IMPUESTOS NO COMPRENDIDOS EN LA LEY DE INGRESOS VIGENTE, CAUSADOS EN EJERCICIOS FISCALES ANTERIORES PENDIENTES DE LIQUIDACIÓN)' and ConceptoDyP=0446;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=110081 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS FÍSICAS' and ConceptoDyP=0168;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=110083 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS MORALES' and ConceptoDyP=0167;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=130023 and DescripcionOrigen='IVA RÉGIMEN SIMPLIFICADO DE CONFIANZA' and ConceptoDyP=0318;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=400295 and DescripcionOrigen='SECRETARIA DE SALUD.' and ConceptoDyP=308126;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=400296 and DescripcionOrigen='SECRETARIA DE SEGURIDAD Y PROTECCION CIUDADANA.' and ConceptoDyP=308127;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=400297 and DescripcionOrigen='SECRETARIA DEL BIENESTAR.' and ConceptoDyP=308128;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=400298 and DescripcionOrigen='POR LA OBTENCION DE CODIGOS DE SEGURIDAD QUE SE IMPRIMAN EN LAS CAJETILLAS, ESTUCHES, EMPAQUES, ENVOLTURAS O CUALQUIER OTRO OBJETO QUE CONTENGA CIGARROS U OTROS TABACOS LABRADOS PARA VENTA EN MEXICO (ART.  53-I  DE LA LFD).' and ConceptoDyP=308129;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=400299 and DescripcionOrigen='POR LA OBTENCION DE FOLIOS ELECTRONICOS PARA LA IMPRESION DE MARBETES ELECTRONICOS QUE SE ADHIERAN A LOS ENVASES QUE CONTENGAN BEBIDAS ALCOHOLICAS A QUE SE REFIERE LA LEY DEL IMPUESTO ESPECIAL SOBRE PRODUCCION Y SERVICIOS (ART. 53-K, FRACCIÓN II DE LA LFD).' and ConceptoDyP=308130;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=500165 and DescripcionOrigen='DERECHOS DEROGADOS DE LA FISCALIA GENERAL DE LA REPUBLICA.' and ConceptoDyP=308131;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=500166 and DescripcionOrigen='DERECHO POR AUTORIZACION DE ARRIBO, DESPACHO, MANIOBRA DE FONDEO O ENMIENDA (ART. 170 DE LA LFD).' and ConceptoDyP=308132;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=500167 and DescripcionOrigen='POR EL DERECHO DE CUMPLIMIENTO DEL CODIGO INTERNACIONAL PARA LA PROTECCION DE LOS BUQUES Y DE LAS INSTALACIONES PORTUARIAS (ART. 170-G DE LA LFD).' and ConceptoDyP=308133;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=5 and ConceptoOrigen=500168 and DescripcionOrigen='DERECHOS DEROGADOS DE LA SECRETARIA DE COMUNICACIONES Y TRANSPORTES, MEDIANTE EL DECRETO DEL 7 DE DICIEMBRE DE 2020.' and ConceptoDyP=308134;

delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=400236 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS CON FINES TURÍSTICOS (DNR-T)' and ConceptoDyP=307182;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=400249 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS SIN FINES TURÍSTICOS (DNR-O)' and ConceptoDyP=307186;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=180003 and DescripcionOrigen='IMPUESTO POR LA ACTIVIDAD DE EXPLORACIÓN Y EXTRACCIÓN DE HIDROCARBUROS' and ConceptoDyP=0901;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=700193 and DescripcionOrigen='MULTAS PREVISTAS EN EL ART. 160 DE LA LEY NACIONAL DE EJECUCIÓN PENAL' and ConceptoDyP=506158;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=700194 and DescripcionOrigen='MULTAS IMPUESTAS POR LA OMISIÓN EN EL CUMPLIMIENTO DE LOS ARTÍCULOS 17 Y 18 DE LA LEY FEDERAL PARA LA PREVENCIÓN E IDENTIFICACIÓN DE OPERACIONES CON RECURSOS DE PROCEDENCIA ILÍCITA' and ConceptoDyP=50547;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=140042 and DescripcionOrigen='OTROS, GASOLINA (IMPORTACIÓN DE GASOLINA)' and ConceptoDyP=0442;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=140043 and DescripcionOrigen='PEMEX, DIÉSEL(IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0443;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=140044 and DescripcionOrigen='OTROS, DIÉSEL (IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0444;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=140045 and DescripcionOrigen='IMPORTACIÓN DE COMBUSTIBLES NO FÓSILES ' and ConceptoDyP=0445;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=500155 and DescripcionOrigen='IMPUESTO ESPECIAL SOBRE PRODUCCIÓN Y SERVICIOS (IMPUESTOS NO COMPRENDIDOS EN LA LEY DE INGRESOS VIGENTE, CAUSADOS EN EJERCICIOS FISCALES ANTERIORES PENDIENTES DE LIQUIDACIÓN)' and ConceptoDyP=0446;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=110081 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS FÍSICAS' and ConceptoDyP=0168;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=110083 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS MORALES' and ConceptoDyP=0167;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=130023 and DescripcionOrigen='IVA RÉGIMEN SIMPLIFICADO DE CONFIANZA' and ConceptoDyP=0318;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=400295 and DescripcionOrigen='SECRETARIA DE SALUD.' and ConceptoDyP=308126;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=400296 and DescripcionOrigen='SECRETARIA DE SEGURIDAD Y PROTECCION CIUDADANA.' and ConceptoDyP=308127;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=400297 and DescripcionOrigen='SECRETARIA DEL BIENESTAR.' and ConceptoDyP=308128;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=400298 and DescripcionOrigen='POR LA OBTENCION DE CODIGOS DE SEGURIDAD QUE SE IMPRIMAN EN LAS CAJETILLAS, ESTUCHES, EMPAQUES, ENVOLTURAS O CUALQUIER OTRO OBJETO QUE CONTENGA CIGARROS U OTROS TABACOS LABRADOS PARA VENTA EN MEXICO (ART.  53-I  DE LA LFD).' and ConceptoDyP=308129;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=400299 and DescripcionOrigen='POR LA OBTENCION DE FOLIOS ELECTRONICOS PARA LA IMPRESION DE MARBETES ELECTRONICOS QUE SE ADHIERAN A LOS ENVASES QUE CONTENGAN BEBIDAS ALCOHOLICAS A QUE SE REFIERE LA LEY DEL IMPUESTO ESPECIAL SOBRE PRODUCCION Y SERVICIOS (ART. 53-K, FRACCIÓN II DE LA LFD).' and ConceptoDyP=308130;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=500165 and DescripcionOrigen='DERECHOS DEROGADOS DE LA FISCALIA GENERAL DE LA REPUBLICA.' and ConceptoDyP=308131;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=500166 and DescripcionOrigen='DERECHO POR AUTORIZACION DE ARRIBO, DESPACHO, MANIOBRA DE FONDEO O ENMIENDA (ART. 170 DE LA LFD).' and ConceptoDyP=308132;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=500167 and DescripcionOrigen='POR EL DERECHO DE CUMPLIMIENTO DEL CODIGO INTERNACIONAL PARA LA PROTECCION DE LOS BUQUES Y DE LAS INSTALACIONES PORTUARIAS (ART. 170-G DE LA LFD).' and ConceptoDyP=308133;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=4 and ConceptoOrigen=500168 and DescripcionOrigen='DERECHOS DEROGADOS DE LA SECRETARIA DE COMUNICACIONES Y TRANSPORTES, MEDIANTE EL DECRETO DEL 7 DE DICIEMBRE DE 2020.' and ConceptoDyP=308134;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=400236 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS CON FINES TURÍSTICOS (DNR-T)' and ConceptoDyP=307182;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=400249 and DescripcionOrigen='DERECHO DE VISITANTE SIN PERMISO PARA REALIZAR ACTIVIDADES REMUNERADAS SIN FINES TURÍSTICOS (DNR-O)' and ConceptoDyP=307186;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=180003 and DescripcionOrigen='IMPUESTO POR LA ACTIVIDAD DE EXPLORACIÓN Y EXTRACCIÓN DE HIDROCARBUROS' and ConceptoDyP=0901;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=700193 and DescripcionOrigen='MULTAS PREVISTAS EN EL ART. 160 DE LA LEY NACIONAL DE EJECUCIÓN PENAL' and ConceptoDyP=506158;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=700194 and DescripcionOrigen='MULTAS IMPUESTAS POR LA OMISIÓN EN EL CUMPLIMIENTO DE LOS ARTÍCULOS 17 Y 18 DE LA LEY FEDERAL PARA LA PREVENCIÓN E IDENTIFICACIÓN DE OPERACIONES CON RECURSOS DE PROCEDENCIA ILÍCITA' and ConceptoDyP=50547;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=140042 and DescripcionOrigen='OTROS, GASOLINA (IMPORTACIÓN DE GASOLINA)' and ConceptoDyP=0442;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=140043 and DescripcionOrigen='PEMEX, DIÉSEL(IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0443;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=140044 and DescripcionOrigen='OTROS, DIÉSEL (IMPORTACIÓN DE DIÉSEL)' and ConceptoDyP=0444;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=140045 and DescripcionOrigen='IMPORTACIÓN DE COMBUSTIBLES NO FÓSILES ' and ConceptoDyP=0445;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=500155 and DescripcionOrigen='IMPUESTO ESPECIAL SOBRE PRODUCCIÓN Y SERVICIOS (IMPUESTOS NO COMPRENDIDOS EN LA LEY DE INGRESOS VIGENTE, CAUSADOS EN EJERCICIOS FISCALES ANTERIORES PENDIENTES DE LIQUIDACIÓN)' and ConceptoDyP=0446;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=110081 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS FÍSICAS' and ConceptoDyP=0168;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=110083 and DescripcionOrigen='ISR RÉGIMEN SIMPLIFICADO DE CONFIANZA, PERSONAS MORALES' and ConceptoDyP=0167;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=130023 and DescripcionOrigen='IVA RÉGIMEN SIMPLIFICADO DE CONFIANZA' and ConceptoDyP=0318;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=400295 and DescripcionOrigen='SECRETARIA DE SALUD.' and ConceptoDyP=308126;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=400296 and DescripcionOrigen='SECRETARIA DE SEGURIDAD Y PROTECCION CIUDADANA.' and ConceptoDyP=308127;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=400297 and DescripcionOrigen='SECRETARIA DEL BIENESTAR.' and ConceptoDyP=308128;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=400298 and DescripcionOrigen='POR LA OBTENCION DE CODIGOS DE SEGURIDAD QUE SE IMPRIMAN EN LAS CAJETILLAS, ESTUCHES, EMPAQUES, ENVOLTURAS O CUALQUIER OTRO OBJETO QUE CONTENGA CIGARROS U OTROS TABACOS LABRADOS PARA VENTA EN MEXICO (ART.  53-I  DE LA LFD).' and ConceptoDyP=308129;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=400299 and DescripcionOrigen='POR LA OBTENCION DE FOLIOS ELECTRONICOS PARA LA IMPRESION DE MARBETES ELECTRONICOS QUE SE ADHIERAN A LOS ENVASES QUE CONTENGAN BEBIDAS ALCOHOLICAS A QUE SE REFIERE LA LEY DEL IMPUESTO ESPECIAL SOBRE PRODUCCION Y SERVICIOS (ART. 53-K, FRACCIÓN II DE LA LFD).' and ConceptoDyP=308130;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=500165 and DescripcionOrigen='DERECHOS DEROGADOS DE LA FISCALIA GENERAL DE LA REPUBLICA.' and ConceptoDyP=308131;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=500166 and DescripcionOrigen='DERECHO POR AUTORIZACION DE ARRIBO, DESPACHO, MANIOBRA DE FONDEO O ENMIENDA (ART. 170 DE LA LFD).' and ConceptoDyP=308132;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=500167 and DescripcionOrigen='POR EL DERECHO DE CUMPLIMIENTO DEL CODIGO INTERNACIONAL PARA LA PROTECCION DE LOS BUQUES Y DE LAS INSTALACIONES PORTUARIAS (ART. 170-G DE LA LFD).' and ConceptoDyP=308133;
delete [MotorTraductor].[dbo].[CatConceptoEquivalencia] where IdAplicacion=2 and ConceptoOrigen=500168 and DescripcionOrigen='DERECHOS DEROGADOS DE LA SECRETARIA DE COMUNICACIONES Y TRANSPORTES, MEDIANTE EL DECRETO DEL 7 DE DICIEMBRE DE 2020.' and ConceptoDyP=308134;
DELETE [MotorTraductor].[dbo].[CatConceptoDyP] WHERE [ConceptoDyP] IN (307186
,0901
,506158
,50547
,0442
,0443
,0444
,0445
,0446
,0168
,0167
,0318
,308126
,308127
,308128
,308129
,308130
,308131
,308132
,308133
,308134);
COMMIT TRAN
go

SELECT * FROM [MotorTraductor].[dbo].[CatConceptoEquivalencia]
GO
