--EXECUTE PROCEDURE spServioMicrosoftObten (500, '6LVL0N2', 5)

--EXECUTE PROCEDURE spServioMicrosoftActualiza(8577, 0, -1, '0', '', '3', 'La tabla de valores de configuración de la base de datos PagosBD en la instancia de SQLServer teiadbdac.southcentralus.cloudapp.azure.com no estuvo disponible al intentar leer el valor de la variable de configuración PAGOS.BD::PAGOS.', '', '', '', '', '', '2020-07-28 16:26:24', 3, NULL, NULL);
--EXECUTE PROCEDURE spServioMicrosoftActualiza(8577, 0, -1, '0', '', '3', 'La tabla de valores de configuración de la base de datos PagosBD en la instancia de SQLServer teiadbdac.southcentralus.cloudapp.azure.com no estuvo disponible al intentar leer el valor de la variable de configuración PAGOS.BD::PAGOS.', '', '', '', '', '', '2020-07-28 16:28:45', 3, NULL, NULL);
--EXECUTE PROCEDURE spServioMicrosoftActualiza(8577, 0, -1, '0', '', '3', 'La tabla de valores de configuración de la base de datos PagosBD en la instancia de SQLServer teiadbdac.southcentralus.cloudapp.azure.com no estuvo disponible al intentar leer el valor de la variable de configuración PAGOS.BD::PAGOS.', '', '', '', '', '', '2020-07-28 16:31:05', 3, NULL, NULL);
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '01140001195597949489', '2', '', '18038017', '20140204', '17:05', '1666', '20150409', '2020-07-28 20:36:59', 2, '2014-02-04 17:05:00', '2015-04-09 00:00:00');
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '01140001195597949489', '2', '', '18038017', '20140204', '17:05', '1666', '20150409', '2020-07-28 20:36:59', 2, '2014-02-04 17:05:00', '2015-04-09 00:00:00');
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '01140001195597949489', '2', '', '18038017', '20140205', '17:05', '1666', '20150409', '2020-07-28 20:36:59', 2, '2014-02-05 17:05:00', '2015-04-09 00:00:00');
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '0213003L404402868233', '2', '', '11358184', '20130621', '17:51', '1666', '20150409', '2020-07-28 20:36:59', 2, '2013-06-21 17:51:00', '2015-04-09 00:00:00');
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '0213003L404402868233', '2', '', '11358184', '20130621', '17:51', '1666', '20150409', '2020-07-28 20:36:59', 2, '2013-06-21 17:51:00', '2015-04-09 00:00:00');
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '0213003L404402868233', '2', '', '11358184', '20130926', '17:51', '1666', '20150409', '2020-07-28 20:36:59', 2, '2013-09-26 17:51:00', '2015-04-09 00:00:00');
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '0213003L404402868233', '2', '', '11358184', '20130927', '17:51', '1666', '20150409', '2020-07-28 20:36:59', 2, '2013-09-27 17:51:00', '2015-04-09 00:00:00');
--EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 0, -1, '1', '0213003L404402868233', '2', '', '11358184', '20130927', '17:51', '1666', '20150409', '2020-07-28 20:36:59', 2, '2013-09-27 17:51:00', '2015-04-09 00:00:00');

EXECUTE PROCEDURE spServioMicrosoftActualiza(7449, 8, 2, '', '', '', '', '', '', '', '', '', '2020-07-28 20:36:59', NULL, NULL, NULL);
EXECUTE PROCEDURE spServioMicrosoftActualiza(8577, 1, 3, '', '', '', '', '', '', '', '', '', '2020-07-28 16:31:05', NULL, NULL, NULL);

--SELECT * FROM SerSustitutivas WHERE sEquipo = '6LVL0N2' AND nId = 8577
--SELECT * FROM SerSustitutivasRes WHERE nIdSusWSM = 8577

SELECT 'SELECT ' + CAST(nID AS nVarChar) + ', nID FROM m_cbSerSustitutivas WHERE sRFC = ''' + sRFC + ''' AND nEjercicio = ' + CAST(nEjercicio AS nVarChar) + ' AND sIDPeriodo = ''' + sIDPeriodo + ''' AND sIDBanco = ''' + sIDBanco + ''' 
UNION', * 
FROM m_cbSerSustitutivas
WHERE nID IN(8250, 9579, 9583, 9591, 7017, 9544, 9383, 6917, 6983, 2683, 9542, 6238, 9517, 9479, 8326, 9478, 7004, 9522, 1531, 9472, 2866, 9331, 2870, 9623, 979, 8573, 147, 9427, 8657, 236, 237, 238, 239, 240, 245)

SELECT * FROM SerSustitutivas WHERE sRFC = 'AAA000103U53' AND nEjercicio = 2016 AND sIDPeriodo = '001' AND sIDBanco = '40112'
--SELECT * FROM SerSustitutivas WHERE sRFC = 'AAA000103U53' AND nEjercicio = 2016 AND sIDPeriodo = '001' AND sIDBanco = '40112'
/*
SELECT 147, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAA000103U53' AND nEjercicio = 2016 AND sIDPeriodo = '001' AND sIDBanco = '40112' 
UNION
SELECT 236, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAA7911268M6' AND nEjercicio = 2016 AND sIDPeriodo = '018' AND sIDBanco = '40112' 
UNION
SELECT 237, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAA821008D10' AND nEjercicio = 2016 AND sIDPeriodo = '003' AND sIDBanco = '40112' 
UNION
SELECT 238, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAA821008D10' AND nEjercicio = 2016 AND sIDPeriodo = '003' AND sIDBanco = '40072' 
UNION
SELECT 239, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAA921218CL3' AND nEjercicio = 2017 AND sIDPeriodo = '001' AND sIDBanco = '40112' 
UNION
SELECT 240, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAA921218CL3' AND nEjercicio = 2017 AND sIDPeriodo = '001' AND sIDBanco = '40132' 
UNION
SELECT 245, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAA940616CI6' AND nEjercicio = 2017 AND sIDPeriodo = '002' AND sIDBanco = '40132' 
UNION
SELECT 979, nID FROM m_cbSerSustitutivas WHERE sRFC = 'ACM011221783' AND nEjercicio = 2016 AND sIDPeriodo = '006' AND sIDBanco = '40112' 
UNION
SELECT 1531, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AGL910208AC2' AND nEjercicio = 2017 AND sIDPeriodo = '012' AND sIDBanco = '40072' 
UNION
SELECT 2683, nID FROM m_cbSerSustitutivas WHERE sRFC = 'CACL7104198I0' AND nEjercicio = 2014 AND sIDPeriodo = '038' AND sIDBanco = '40036' 
UNION
SELECT 2866, nID FROM m_cbSerSustitutivas WHERE sRFC = 'CDR140212S68' AND nEjercicio = 2017 AND sIDPeriodo = '003' AND sIDBanco = '40112' 
UNION
SELECT 2870, nID FROM m_cbSerSustitutivas WHERE sRFC = 'CDR140212S68' AND nEjercicio = 2017 AND sIDPeriodo = '003' AND sIDBanco = '40132' 
UNION
SELECT 6238, nID FROM m_cbSerSustitutivas WHERE sRFC = 'TAAA820526CEA' AND nEjercicio = 2014 AND sIDPeriodo = '037' AND sIDBanco = '40036' 
UNION
SELECT 6917, nID FROM m_cbSerSustitutivas WHERE sRFC = 'AAL881221AB2' AND nEjercicio = 2016 AND sIDPeriodo = '005' AND sIDBanco = '40032' 
UNION
SELECT 6983, nID FROM m_cbSerSustitutivas WHERE sRFC = 'HERG651216TG1' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40036' 
UNION
SELECT 7004, nID FROM m_cbSerSustitutivas WHERE sRFC = 'MARA581116TP4' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40012' 
UNION
SELECT 7017, nID FROM m_cbSerSustitutivas WHERE sRFC = 'MUCS490218E67' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40036' 
UNION
SELECT 8250, nID FROM m_cbSerSustitutivas WHERE sRFC = 'BAAI7903018X1' AND nEjercicio = 2016 AND sIDPeriodo = '035' AND sIDBanco = '40002' 
UNION
SELECT 8326, nID FROM m_cbSerSustitutivas WHERE sRFC = 'BEBA261206FM7' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40021' 
UNION
SELECT 8573, nID FROM m_cbSerSustitutivas WHERE sRFC = 'CARH381209LZA' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40106' 
UNION
SELECT 8657, nID FROM m_cbSerSustitutivas WHERE sRFC = 'CUSR890313UJ4' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40021' 
UNION
SELECT 9331, nID FROM m_cbSerSustitutivas WHERE sRFC = 'LOHR750301RS2' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40062' 
UNION
SELECT 9383, nID FROM m_cbSerSustitutivas WHERE sRFC = 'MAPL740803PR8' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40030' 
UNION
SELECT 9427, nID FROM m_cbSerSustitutivas WHERE sRFC = 'MASI790312LE4' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '99001' 
UNION
SELECT 9472, nID FROM m_cbSerSustitutivas WHERE sRFC = 'MOAM290613P15' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40037' 
UNION
SELECT 9478, nID FROM m_cbSerSustitutivas WHERE sRFC = 'MORJ780517LU9' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40021' 
UNION
SELECT 9479, nID FROM m_cbSerSustitutivas WHERE sRFC = 'MORJ780517LU9' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40014' 
UNION
SELECT 9517, nID FROM m_cbSerSustitutivas WHERE sRFC = 'PAPA3903041F2' AND nEjercicio = 2016 AND sIDPeriodo = '035' AND sIDBanco = '37019' 
UNION
SELECT 9522, nID FROM m_cbSerSustitutivas WHERE sRFC = 'PAPA3903041F2' AND nEjercicio = 2016 AND sIDPeriodo = '035' AND sIDBanco = '40143' 
UNION
SELECT 9542, nID FROM m_cbSerSustitutivas WHERE sRFC = 'PEAJ500702IG3' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40132' 
UNION
SELECT 9544, nID FROM m_cbSerSustitutivas WHERE sRFC = 'PECM6510258E4' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40012' 
UNION
SELECT 9579, nID FROM m_cbSerSustitutivas WHERE sRFC = 'ROMJ770502P51' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40021' 
UNION
SELECT 9583, nID FROM m_cbSerSustitutivas WHERE sRFC = 'ROMJ770502P51' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40021' 
UNION
SELECT 9591, nID FROM m_cbSerSustitutivas WHERE sRFC = 'ROMJ770502P51' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40030' 
UNION
SELECT 9623, nID FROM m_cbSerSustitutivas WHERE sRFC = 'SARE630428UR5' AND nEjercicio = 2013 AND sIDPeriodo = '035' AND sIDBanco = '40062' 
*/