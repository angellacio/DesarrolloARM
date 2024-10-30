using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos
{    
    public class DalConceptoPersonaPeriodicidad
    {
        public static List<CatConceptoPersonaPeriodicidad> ObtenerCatConceptoPersonaPeriodicidad(int idTipoPersona)
        {
            var lstPeridicidadEquivalenciaConceptoPersonaPeriodicidad = new List<CatConceptoPersonaPeriodicidad>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pObtenerCatConceptoPersonaPeriodicidad");
            db.AddInParameter(dbCommand, "@pIdTipoPersona", System.Data.DbType.Int32, idTipoPersona);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var periodicidad = new CatConceptoPersonaPeriodicidad();                    
                    periodicidad.ConceptoDyP = (string)dataReader["ConceptoDyP"];
                    periodicidad.IdTipoPersona = Convert.ToInt32(dataReader["IdTipoPersona"]);
                    periodicidad.IdPeriodicidadDyP = (string)dataReader["IdPeriodicidadDyP"];
                    lstPeridicidadEquivalenciaConceptoPersonaPeriodicidad.Add(periodicidad);
                }
            }

            return lstPeridicidadEquivalenciaConceptoPersonaPeriodicidad;
        }
    }
}
