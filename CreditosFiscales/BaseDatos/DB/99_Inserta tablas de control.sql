--- HEAD
set IDENTITY_INSERT tblControlPagosHead ON;

INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230402' ,'25/01/2024 11:48:00' ,'25/01/2024 11:50:12' ,'26' ,'40002' ,'3' ,'99' ,'2408' ,'16810' ,'40002230401' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230607' ,'25/01/2024 15:48:52' ,'25/01/2024 15:49:51' ,'27' ,'40025' ,'0' ,'3' ,'2408' ,'4812' ,'40025230607' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230410' ,'25/01/2024 23:51:31' ,'25/01/2024 23:55:00' ,'28' ,'40072' ,'0' ,'3' ,'2408' ,'29166' ,'40072230410' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230402' ,'26/01/2024 09:24:42' ,'26/01/2024 09:24:01' ,'29' ,'40002' ,'3' ,'99' ,'2410' ,'16810' ,'40002230401' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230531' ,'26/01/2024 09:27:36' ,'26/01/2024 09:24:02' ,'30' ,'40002' ,'7' ,'99' ,'2410' ,'195250' ,'40002230531' ,'ADGARCIA9J7' ,'14' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230607' ,'26/01/2024 09:33:00' ,'26/01/2024 09:24:03' ,'31' ,'40025' ,'0' ,'3' ,'2410' ,'4812' ,'40025230607' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230410' ,'26/01/2024 09:35:12' ,'26/01/2024 09:24:04' ,'32' ,'40072' ,'0' ,'3' ,'2410' ,'29166' ,'40072230410' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230402' ,'29/01/2024 11:03:11' ,'29/01/2024 10:47:05' ,'33' ,'40002' ,'3' ,'99' ,'2412' ,'16810' ,'40002230401' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230607' ,'29/01/2024 11:07:23' ,'29/01/2024 10:47:06' ,'34' ,'40025' ,'0' ,'3' ,'2412' ,'4812' ,'40025230607' ,'ADGARCIA9J7' ,'1' )
INSERT INTO dbo.tblControlPagosHead ( [FechaPago], [FechaProceso], [FechaRegistro], [idArchivo], [IdBanco], [IdError], [IdEstado], [IdProceso], [Importe], [NombreArchivo], [NombreEquipo], [NumRegistros]) VALUES ('20230410' ,'29/01/2024 11:07:25' ,'29/01/2024 10:47:07' ,'35' ,'40072' ,'0' ,'3' ,'2412' ,'29166' ,'40072230410' ,'ADGARCIA9J7' ,'1' )
--- DETALLLE
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230401' ,'07:49' ,'26' ,'99' ,'99' ,null ,'0' ,'16810' ,'04230BAE174438216270' ,'1' ,'' ,'1' ,'3397' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230607' ,'15:00' ,'27' ,'0' ,'3' ,null ,'0' ,'4812' ,'04230000984438823249' ,'1' ,'' ,'1' ,'712300000010' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230410' ,'11:39' ,'28' ,'0' ,'3' ,null ,'0' ,'29166' ,'04230BXM044438520222' ,'3' ,'' ,'1' ,'368512410023' ,'1' ,'54 ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230401' ,'07:49' ,'29' ,'99' ,'99' ,null ,'0' ,'16810' ,'04230BAE174438216270' ,'1' ,'' ,'1' ,'3397' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230531' ,'09:48' ,'30' ,'99' ,'99' ,null ,'0' ,'14849' ,'04230FEO834438744230' ,'3' ,'' ,'1' ,'16633' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('3' ,'20230531' ,'12:15' ,'30' ,'99' ,'99' ,null ,'0' ,'6201' ,'04230G8U944438741229' ,'1' ,'' ,'2' ,'44349' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('4' ,'20230531' ,'13:21' ,'30' ,'99' ,'99' ,null ,'0' ,'3540' ,'04230H16904438828277' ,'3' ,'' ,'3' ,'58860' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('5' ,'20230531' ,'19:48' ,'30' ,'99' ,'99' ,null ,'0' ,'1042' ,'04230H26134438743266' ,'1' ,'' ,'4' ,'114522' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('6' ,'20230531' ,'13:36' ,'30' ,'99' ,'99' ,null ,'0' ,'1164' ,'04230HDT914439044435' ,'1' ,'' ,'5' ,'62331' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('7' ,'20230531' ,'13:06' ,'30' ,'99' ,'99' ,null ,'0' ,'87722' ,'04230HDX124438740215' ,'3' ,'' ,'6' ,'55608' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('8' ,'20230531' ,'14:35' ,'30' ,'99' ,'99' ,null ,'0' ,'6119' ,'04230HEV994438829269' ,'3' ,'' ,'7' ,'75464' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('9' ,'20230531' ,'11:51' ,'30' ,'99' ,'99' ,null ,'0' ,'5867' ,'04230HFA534438820217' ,'1' ,'' ,'8' ,'39409' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('10','20230531' ,'16:15' ,'30' ,'99' ,'99' ,null ,'0' ,'17314' ,'04230HIM824438826423' ,'1' ,'' ,'9' ,'95770' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('11','20230531' ,'11:53' ,'30' ,'99' ,'99' ,null ,'0' ,'24975' ,'04230HIS924438749254' ,'3' ,'' ,'10' ,'39750' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('12','20230531' ,'12:33' ,'30' ,'99' ,'99' ,null ,'0' ,'6669' ,'04230HJ5734438829223' ,'3' ,'' ,'11' ,'48412' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('13','20230531' ,'13:24' ,'30' ,'99' ,'99' ,null ,'0' ,'329' ,'04230HJH864438742213' ,'3' ,'' ,'12' ,'59618' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('14','20230531' ,'15:06' ,'30' ,'99' ,'99' ,null ,'0' ,'15200' ,'04230HKG014438820479' ,'1' ,'' ,'13' ,'82081' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('15','20230531' ,'14:36' ,'30' ,'99' ,'99' ,null ,'0' ,'4258' ,'04230HKM524438741236' ,'3' ,'' ,'14' ,'75599' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230607' ,'15:00' ,'31' ,'0' ,'4' ,null ,'0' ,'4812' ,'04230000984438823249' ,'1' ,'' ,'1' ,'712300000010' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230410' ,'11:39' ,'32' ,'0' ,'4' ,null ,'0' ,'29166' ,'04230BXM044438520222' ,'3' ,'' ,'1' ,'368512410023' ,'1' ,'54 ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230401' ,'07:49' ,'33' ,'99' ,'99' ,null ,'0' ,'16810' ,'04230BAE174438216270' ,'1' ,'' ,'1' ,'3397' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230607' ,'15:00' ,'34' ,'0' ,'4' ,null ,'0' ,'4812' ,'04230000984438823249' ,'1' ,'' ,'1' ,'712300000010' ,'1' ,'4  ' )
INSERT INTO dbo.tblControlPagosDet ( [Consecutivo], [FechaPago], [HoraPago], [IdArchivo], [IdError], [IdEstado], [IdProcesamiento], [IdZip], [Importe], [LineaCaptura], [MedioRecepcion], [NombreXML], [NumLinea], [NumOperacion], [TipoPago], [Version]) VALUES ('2' ,'20230410' ,'11:39' ,'35' ,'0' ,'4' ,null ,'0' ,'29166' ,'04230BXM044438520222' ,'3' ,'' ,'1' ,'368512410023' ,'2' ,'54 ' )
