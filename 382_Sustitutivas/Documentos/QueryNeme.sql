use PagosBD

declare @fecha varchar(8)

set @fecha = '20140101'

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
             tc.Descripcion "tipo complementaria", 
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
             d.FechaRecepcion >= @fecha
             --d.Rfc = @rfc
             --and
             --d.Ejercicio = @ejercicio
             --and
             --d.idPeriodo = @idperiodo
             --and
             --do.idObligacion = @concepto
             --and 
             --abc.idBanco = @banco
             --and
             --abc.FechaPresentacion = @fechapresentacion
       order by 
             rfc, 
             Ejercicio desc, 
             idPeriodo, 
             idDeclaracion desc

select distinct
       'SIT' Aplicacion,
       d.idDocumento,
          d.idSolicitudDocumento as NumOperacion,
       d.rfc, 
       dcpp.Ejercicio, 
       dcpp.idPeriodo, 
       dcp.idConceptoPago,
       dt.MontoCargo,
       d.FechaRecepcion,
       d.idTipoDeclaracion,
       d.idComplementaria,
       d.idSolicitudDocumento,
       abc.idBanco,
       abc.FechaPresentacion
from
Documentos d
inner join OrigenInformacion oi
on d.idOrigen = d.idOrigen
inner join DocumentosPeriodicidad dcpp
on d.idDocumento = dcpp.idDocumento
inner join DocumentosConceptosPago dcp
on dcp.idDocumento = d.idDocumento
inner join DocumentosTransacciones dt
on d.idDocumento = dt.idDocumento
and dt.idConceptoPago = dcp.idConceptoPago
left join OperacionesPagos op
on op.idDocumento = d.idDocumento
join ArchivoBancosCifras abc
on abc.idProcesoPago = op.idProcesoPago
where
       oi.idAplicacion = 5
       and
       d.FechaRecepcion >= @fecha
--and
--rfc = @rfc 
--and
--Ejercicio = @ejercicio
--and
--dcp.idConceptoPago = @concepto
--and
--idPeriodo = @idperiodo
--and
--idBanco = @banco
--and
--abc.FechaPresentacion = @fechapresentacion
----group by
----   d.rfc, 
----   dcpp.Ejercicio, 
----   dcpp.idPeriodo, 
----   dcp.idConceptoPago,
----   d.FechaRecepcion,
----   d.idTipoDeclaracion,
----   d.idComplementaria
--     --,
--     --abc.idBanco,
--     --abc.FechaRecepcion
--having
--COUNT(*) > 3
--order by total desc
order by 
             rfc, 
             Ejercicio desc, 
             idPeriodo,
             idTipoDeclaracion asc,
             idComplementaria,
             d.idDocumento desc


select distinct
       'Anuales' Aplicacion,
       d.idDeclaAnual, 
          d.NumOperacion,
       rfc, 
       Ejercicio, 
       idPeriodo, 
       do.idConceptoPago,
       d.MontoAcuse,
       d.FechaRecepcion,
       idTipoDeclaracion, 
       d.idComplementaria, 
       tc.Descripcion "tipo complementaria", 
       idDeclaracionAntVigente,
       d.NumOperacion,
       abc.idBanco,
       abc.FechaPresentacion
from 
       DeclaracionesAnuales d
       join TipoComplementaria tc on
       tc.idComplementaria = d.idComplementaria
       join AnualesConceptosPago do
       on do.idDeclaAnual = d.idDeclaAnual
       left join OperacionesPagos op
       on op.idDocumento = d.idDeclaAnual
       left join ArchivoBancosCifras abc
       on abc.idProcesoPago = op.idProcesoPago
       left join ControlEnvioLotesCeros celc
       on celc.idDocumento = d.idDeclaAnual
where
       d.FechaRecepcion >= @fecha

--     d.Rfc = @rfcanuales
--     and
--     d.Ejercicio = @ejercicioanuales
--     and
--     d.idPeriodo = @idperiodoanuales
--     and
--     do.idConceptoPago = @conceptoanuales
--     and 
--     abc.idBanco = @bancoanuales
--     and
--     abc.FechaPresentacion = @fechapresentacionanuales
order by 
       rfc, 
       Ejercicio desc, 
       idPeriodo, 
       idDeclaAnual desc

