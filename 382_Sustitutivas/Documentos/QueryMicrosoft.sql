use PagosBD

declare @rfc varchar(13)
declare @ejercicio int
declare @idperiodo varchar(3)
declare @concepto varchar(6)

set @rfc = 'BCM120905UC7'
set @ejercicio = 2019
set @idperiodo = '006'
set @concepto = '0112'

       select distinct
             'DYP' Aplicacion,
             d.idDeclaracion, 
             d.NumOperacion,
             rfc, 
             Ejercicio, 
             idPeriodo, 
             do.idObligacion,
             d.MontoAcuse,
             d.FechaRecepcion,
             idTipoDeclaracion, 
             d.idComplementaria, 
             tc.Descripcion "tipoComplementaria", 
             idDeclaracionAntVigente,
             op.NumeroOperacion,
             abc.idBanco,
             abc.FechaPresentacion
       from 
             Declaracion d
             join TipoComplementaria tc on
             tc.idComplementaria = d.idComplementaria
             join DeclaracionObligacion do
             on do.idDeclaracion = d.idDeclaracion
             left join OperacionesPagos op
             on op.idDocumento = d.idDeclaracion
             left join ArchivoBancosCifras abc
             on abc.idProcesoPago = op.idProcesoPago
       where
             --d.FechaRecepcion >= @fecha
             d.Rfc = @rfc
             and
             d.Ejercicio = @ejercicio
             and
             d.idPeriodo = @idperiodo
             and
             do.idObligacion = @concepto
             --and 
             --abc.idBanco = @banco
             --and
             --abc.FechaPresentacion = @fechapresentacion
       order by 
             rfc, 
             Ejercicio desc, 
             idPeriodo, 
             idDeclaracion desc
