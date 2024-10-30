using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Datos.AccesoDatos.ReglasEquivalencia;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ReglasEquivalencia
{
    public class LogicaReglas
    {
        public static List<ReglaEquivalencia> ObtieneReglas(int idAplicacion, int idTipoDocumento, int idTipoObjeto, string valorObjeto)
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {
                var lista = dal.ObtieneReglas(cadenaConexion, idAplicacion, idTipoDocumento, idTipoObjeto, valorObjeto);
                return lista;
            }
        }

        public static List<ConceptoEquivalencia> ObtieneConceptos(int idAplicacion, string conceptoOrigen, string conceptoDyP)
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {
                var lista = dal.ObtieneConceptos(cadenaConexion, idAplicacion, conceptoOrigen, conceptoDyP);
                return lista;
            }
        }

        public static List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatAplicacion()
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {

                var lista = dal.ObtieneCatAplicacion(cadenaConexion);
                return lista;
            }
        }

        public static List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatTipoDocumento()
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {

                var lista = dal.ObtieneCatTipoDocumento(cadenaConexion);
                return lista;
            }
        }

        public static List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatTipoObjeto()
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {

                var lista = dal.ObtieneCatTipoObjeto(cadenaConexion);
                return lista;
            }
        }

        public static List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatConceptoDyP()
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {

                var lista = dal.ObtieneCatConceptoDyP(cadenaConexion);
                return lista;
            }
        }

        public static void ActualizaRegla(ReglaEquivalencia regla)
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {
                dal.ActualizaRegla(cadenaConexion, regla);
            }
        }

        public static void AgregaRegla(ReglaEquivalencia regla)
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {
                dal.AgregaRegla(cadenaConexion, regla);
            }
        }

        public static void AgregaConcepto(ConceptoEquivalencia concepto)
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {
                dal.AgregaConcepto(cadenaConexion, concepto);
            }
        }

        public static void ActualizaConcepto( ConceptoEquivalencia concepto, ConceptoEquivalencia conceptoOriginal)
        {
            string cadenaConexion = (string)ApplicationSettings.ConsultaConfiguracion("Log:ConnStrMotorTraductor");
            using (DalReglasEquivalencia dal = new DalReglasEquivalencia())
            {
                dal.ActualizaConcepto(cadenaConexion, concepto,conceptoOriginal);
            }
        }
    }
}
