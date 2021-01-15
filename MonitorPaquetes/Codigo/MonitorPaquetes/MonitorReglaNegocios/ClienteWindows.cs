using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MonitorEntidades;

namespace MonitorReglaNegocios
{

    public static class ClienteWindows
    {
        private static string sAreaConsulta()
        {
            string result = "", sConfig = ConfigurationManager.AppSettings["Area"].ToString().Trim().ToUpper();

            if (sConfig != "" && sConfig != "ALL")
            {
                if (sConfig.Split('|').Length > 0)
                {
                    List<string> sAreasConfig = sConfig.Split('|').ToList();

                    for (int nR = 0; nR < sAreasConfig.Count; nR++)
                    {
                        if (result == "")
                            result = string.Format("'{0}'", sAreasConfig[nR].Trim());
                        else
                            result = string.Format("{0}, '{1}'", result, sAreasConfig[nR].Trim());
                    }
                }
            }

            return result;
        }

        public static List<ent_CatalogoSencillo> Lista_Desarrolladores()
        {
            Datos.ConsultaCatalogos cc = null;
            List<ent_CatalogoSencillo> result = null;
            try
            {
                cc = new Datos.ConsultaCatalogos();
                result = cc.Consulta_Catalogos(2, sAreaConsulta());
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return result;
        }
        public static List<ent_CatalogoSencillo> Lista_TrabajoAreas()
        {
            Datos.ConsultaCatalogos cc = null;
            List<ent_CatalogoSencillo> result = null;
            try
            {
                cc = new Datos.ConsultaCatalogos();
                result = cc.Consulta_Catalogos(1, sAreaConsulta());
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return result;
        }
        public static ent_Incidentes Incs_Lista(string sIncidente, string sPaquete)
        {
            Datos.ConsultaCatalogos cc = null;
            ent_Incidentes result = null;
            List<ent_Incidentes> datExplota = null;
            try
            {
                result = new ent_Incidentes();
                cc = new Datos.ConsultaCatalogos();
                datExplota = cc.Consulta_RMA(sIncidente, sPaquete);

                if (datExplota.Count >= 0)
                    result = datExplota[0];
            }
            catch (InvalidOperationException ex) { throw ex; }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
            return result;
        }
        public static List<ent_IncidentesPaquetes> Incs_ListaPaquetes(Boolean bolMuestraTodo)
        {
            Datos.ConsultaCatalogos cc = null;
            List<ent_IncidentesPaquetes> result = null;
            try
            {
                cc = new Datos.ConsultaCatalogos();
                result = cc.Consulta_RMAPaquetes(sAreaConsulta(), bolMuestraTodo);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return result;
        }

        public static void Incs_Act_RMA(string nIdIncidente, string sIdIncidenteNew)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, "", null, null, null, null, null, null, null, null, sIdIncidenteNew, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_Area(string nIdIncidente, string nArea)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, "", null, null, null, null, null, nArea, null, null, null, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_Retroalimentado(string nIdIncidente, Boolean bRetroalimentada)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, "", bRetroalimentada, null, null, null, null, null, null, null, null, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_PPMC(string nIdIncidente, Boolean bPPMC)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, "", null, null, null, null, null, null, null, bPPMC, null, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_Desarrollador(string nIdIncidente, string sPaquete, int nDesarollador)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, sPaquete, null, null, null, null, null, null, nDesarollador, null, null, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_DocPru(string nIdIncidente, string sPaquete, Boolean bDocPru)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, sPaquete, null, bDocPru, null, null, null, null, null, null, null, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_Notificado(string nIdIncidente, string sPaquete, Boolean bNotificado)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, sPaquete, null, null, null, null, null, null, null, null, null, null, bNotificado, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_Identificador(string nIdIncidente, string sIdentificador)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, "", null, null, sIdentificador, null, null, null, null, null, null, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_NotaLibera(string nIdIncidente, string sNotaLibera)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, "", null, null, null, null, sNotaLibera, null, null, null, null, null, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_Paquete(string nIdIncidente, string sPaquete, string sPaqueteNew)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, "", null, null, null, null, null, null, null, null, null, sPaqueteNew, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void Incs_Act_PaqIdentificador(string nIdIncidente, string sPaquete, string sIdentificador)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, sPaquete, null, null, null, null, null, null, null, null, null, sIdentificador, null, null, null, null);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static List<ent_Observaciones> Incs_Act_IncObservaciones(int nId, string sIdIncidente, string sIdPaquete, string sObservacion, int nAccion)
        {
            List<ent_Observaciones> result;
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                result = md.ManejoDatosObservaciones(nId, sIdIncidente, sIdPaquete, sObservacion, nAccion);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
            return result;
        }

        public static void Incs_Act_Identificador(string nIdIncidente, string sPaquete, string sIdentificador)
        {
            Datos.ModificaInformacion md = null;
            try
            {
                md = new Datos.ModificaInformacion();

                md.ActualizaDatos(nIdIncidente, sPaquete, null, null, null, null, null, null, null, null, null, null, null, null, null, sIdentificador);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
    }
}
