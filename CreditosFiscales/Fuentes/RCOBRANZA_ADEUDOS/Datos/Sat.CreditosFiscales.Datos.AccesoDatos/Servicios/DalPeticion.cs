
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.Servicios:Sat.CreditosFiscales.Datos.AccesoDatos.Servicios.DalPeticion:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.Servicios
{
    public class DalPeticion
    {
        public void GuardaPeticion(Peticion peticion)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pPeticionesInserta");
            db.AddInParameter(cmd, "@idTipoOrigen", System.Data.DbType.Int16, peticion.TipoOrigen);
            db.AddInParameter(cmd, "@idAccionOrigen", System.Data.DbType.Int16, peticion.Accion);
            db.AddInParameter(cmd, "@fechaPeticion", System.Data.DbType.DateTime, peticion.Fecha);
            db.AddInParameter(cmd, "@rfc", System.Data.DbType.String, peticion.RFC);
            db.AddInParameter(cmd, "@huboError", System.Data.DbType.Boolean, peticion.HuboError);
            db.AddInParameter(cmd, "@xmlPeticion", System.Data.DbType.Xml, peticion.XmlPeticion);
            db.AddInParameter(cmd, "@xmlRespuesta", System.Data.DbType.Xml, peticion.XmlRespuesta);
            db.AddInParameter(cmd, "@observaciones", System.Data.DbType.String, peticion.Observaciones);
            db.AddInParameter(cmd, "@duracion", System.Data.DbType.Decimal, peticion.Duracion);
            db.AddOutParameter(cmd, "@idPeticion", System.Data.DbType.Int64, 0);
            
            db.ExecuteNonQuery(cmd);

            Int64 idPeticion = Convert.ToInt64(db.GetParameterValue(cmd, "@idPeticion"));
            
        }


       
    }
}
