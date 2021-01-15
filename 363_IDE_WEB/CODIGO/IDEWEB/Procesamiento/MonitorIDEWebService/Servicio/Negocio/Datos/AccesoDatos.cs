using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.DyP.Util.Data;
using System.Data;
using System.Data.SqlClient;
using IdeMonitorService.Negocio.Contratos;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text;


namespace IdeMonitorService.Negocio.Datos
{
    public class AccesoDatos
    {
        public const int idError = 6382;

        public DataAccessHelper AbrirConexion()
        {
            DataAccessHelper con = new DataAccessHelper("Sat.DyP.IDE::BaseDatos", DataProviderType.SqlServer);
            return con;
        }

        public List<DeclaracionSalida> DatosDeclaraciones(DeclaracionEntrada declaracion)
        {
            DeclaracionSalida ren = null;
            List<DeclaracionSalida> Lren = new List<DeclaracionSalida>();
            IDataParameter[] parametros = new IDataParameter[21];
            string sql = "ConsultaDeclaraciones";
            if (declaracion.Estatus == null) { declaracion.Estatus = ""; }
            if (declaracion.NombreArchivo == null) { declaracion.NombreArchivo = ""; }
            if (declaracion.Rfc == null) { declaracion.Rfc = ""; }
            if (declaracion.Banco == null) { declaracion.Banco = ""; }
            if (declaracion.TipoArchivo == null) { declaracion.TipoArchivo = ""; }
            if (declaracion.MedioRecepcion == null) { declaracion.MedioRecepcion = ""; }
            if (declaracion.UltimoEstado == null) { declaracion.UltimoEstado = ""; }
            if (declaracion.Sector == null) { declaracion.Sector = ""; }
            if (declaracion.Formato == null) { declaracion.Formato = ""; }
            if (declaracion.MotivoRechazo == null) { declaracion.MotivoRechazo = ""; }
            if (declaracion.RazonSocial == null) { declaracion.RazonSocial = ""; }
            if (declaracion.fechaInicio == null) { declaracion.fechaInicio = ""; }
            if (declaracion.fechaFin == null) { declaracion.fechaFin = ""; }
            if (declaracion.FormaDeclaracion == null) { declaracion.FormaDeclaracion = ""; }
            if (declaracion.EstadoDeclaracion == null) { declaracion.EstadoDeclaracion = ""; }


            parametros[0] = new SqlParameter("@Estatus", declaracion.Estatus);
            parametros[1] = new SqlParameter("@Folio", declaracion.Folio);
            parametros[2] = new SqlParameter("@NombreArchivo", declaracion.NombreArchivo);
            parametros[3] = new SqlParameter("@Rfc", declaracion.Rfc);
            parametros[4] = new SqlParameter("@Banco", declaracion.Banco);
            parametros[5] = new SqlParameter("@TipoArchivo", declaracion.TipoArchivo);
            parametros[6] = new SqlParameter("@MedioRecepcion", declaracion.MedioRecepcion);
            parametros[7] = new SqlParameter("@fechaInicio", declaracion.fechaInicio);
            parametros[8] = new SqlParameter("@fechaFin", declaracion.fechaFin);
            parametros[9] = new SqlParameter("@UltimoEstado", declaracion.UltimoEstado);
            parametros[10] = new SqlParameter("@Sector", declaracion.Sector);
            parametros[11] = new SqlParameter("@Formato", declaracion.Formato);
            parametros[12] = new SqlParameter("@MotivoRechazo", declaracion.MotivoRechazo);
            parametros[13] = new SqlParameter("@RazonSocial", declaracion.RazonSocial);
            parametros[14] = new SqlParameter("@Periodo", declaracion.Periodo);
            parametros[15] = new SqlParameter("@Ejercicio", declaracion.Ejercicio);
            parametros[16] = new SqlParameter("@Operaciones", declaracion.Operaciones);
            parametros[17] = new SqlParameter("@FormaDeclaracion", declaracion.FormaDeclaracion);
            parametros[18] = new SqlParameter("@EstadoDeclaracion", declaracion.EstadoDeclaracion);
            parametros[19] = new SqlParameter("@Inicio", declaracion.Inicio);
            parametros[20] = new SqlParameter("@Bloque", declaracion.Bloque);

            DataAccessHelper c = null;
            try
            {
                c = this.AbrirConexion();

                using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    while (reader.Read())
                    {
                        ren = new DeclaracionSalida();
                        ren.Folio = reader.GetInt32(0);
                        ren.NombreArchivo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        ren.Rfc = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        ren.Banco = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        ren.TipoArchivo = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        ren.MedioRecepcion = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        ren.fechaRecepcion = reader.GetDateTime(6);
                        ren.UltimoEstado = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        ren.Sector = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        ren.Formato = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        ren.MotivoRechazo = reader.IsDBNull(10) ? string.Empty : HttpUtility.HtmlEncode(reader.GetString(10));
                        ren.Estatus = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        if (reader.GetBoolean(12) == true) { ren.EstadoDeclaracion = "Anual"; } else { ren.EstadoDeclaracion = "Mensual"; }
                        ren.RazonSocial = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        ren.Periodo = reader.GetInt32(14);
                        ren.sPeriodo = reader.GetString(15);
                        ren.Operaciones = reader.GetInt32(16);
                        ren.Ejercicio = reader.GetInt32(17);
                        if (reader.GetBoolean(18) == true) { ren.FormaDeclaracion = "Normal"; } else { ren.FormaDeclaracion = "Complementaria"; }
                        ren.fechaModificacion = ren.fechaRecepcion;
                        Lren.Add(ren);
                    }
                }
            }
            catch (Exception e)
            {
                SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
                return null;
            }
            finally
            {
                if (c != null)
                    c.CloseConnection();

            }


            return Lren;
        }

        public List<DeclaracionSalida> DatosBitacora(DeclaracionEntrada declaracion)
        {
            DeclaracionSalida ren = null;
            List<DeclaracionSalida> Lren = new List<DeclaracionSalida>();
            IDataParameter[] parametros = new IDataParameter[21];
            string sql = "ConsultaBitacora";
            if (declaracion.Estatus == null) { declaracion.Estatus = ""; }
            if (declaracion.NombreArchivo == null) { declaracion.NombreArchivo = ""; }
            if (declaracion.Rfc == null) { declaracion.Rfc = ""; }
            if (declaracion.Banco == null) { declaracion.Banco = ""; }
            if (declaracion.TipoArchivo == null) { declaracion.TipoArchivo = ""; }
            if (declaracion.MedioRecepcion == null) { declaracion.MedioRecepcion = ""; }
            if (declaracion.UltimoEstado == null) { declaracion.UltimoEstado = ""; }
            if (declaracion.Sector == null) { declaracion.Sector = ""; }
            if (declaracion.Formato == null) { declaracion.Formato = ""; }
            if (declaracion.MotivoRechazo == null) { declaracion.MotivoRechazo = ""; }
            if (declaracion.RazonSocial == null) { declaracion.RazonSocial = ""; }
            if (declaracion.fechaInicio == null) { declaracion.fechaInicio = ""; }
            if (declaracion.fechaFin == null) { declaracion.fechaFin = ""; }
            if (declaracion.FormaDeclaracion == null) { declaracion.FormaDeclaracion = ""; }
            if (declaracion.EstadoDeclaracion == null) { declaracion.EstadoDeclaracion = ""; }

            parametros[0] = new SqlParameter("@Estatus", declaracion.Estatus);
            parametros[1] = new SqlParameter("@Folio", declaracion.Folio);
            parametros[2] = new SqlParameter("@NombreArchivo", declaracion.NombreArchivo);
            parametros[3] = new SqlParameter("@Rfc", declaracion.Rfc);
            parametros[4] = new SqlParameter("@Banco", declaracion.Banco);
            parametros[5] = new SqlParameter("@TipoArchivo", declaracion.TipoArchivo);
            parametros[6] = new SqlParameter("@MedioRecepcion", declaracion.MedioRecepcion);
            parametros[7] = new SqlParameter("@fechaInicio", declaracion.fechaInicio);
            parametros[8] = new SqlParameter("@fechaFin", declaracion.fechaFin);
            parametros[9] = new SqlParameter("@UltimoEstado", declaracion.UltimoEstado);
            parametros[10] = new SqlParameter("@Sector", declaracion.Sector);
            parametros[11] = new SqlParameter("@Formato", declaracion.Formato);
            parametros[12] = new SqlParameter("@MotivoRechazo", declaracion.MotivoRechazo);
            parametros[13] = new SqlParameter("@RazonSocial", declaracion.RazonSocial);
            parametros[14] = new SqlParameter("@Periodo", declaracion.Periodo);
            parametros[15] = new SqlParameter("@Ejercicio", declaracion.Ejercicio);
            parametros[16] = new SqlParameter("@Operaciones", declaracion.Operaciones);
            parametros[17] = new SqlParameter("@FormaDeclaracion", declaracion.FormaDeclaracion);
            parametros[18] = new SqlParameter("@EstadoDeclaracion", declaracion.EstadoDeclaracion);
            parametros[19] = new SqlParameter("@Inicio", declaracion.Inicio);
            parametros[20] = new SqlParameter("@Bloque", declaracion.Bloque);


            DataAccessHelper c = null;

            try
            {
                c = this.AbrirConexion();

                using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    while (reader.Read())
                    {
                        ren = new DeclaracionSalida();
                        ren.Folio = reader.GetInt32(0);
                        ren.NombreArchivo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        ren.Rfc = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        ren.Banco = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        ren.TipoArchivo = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        ren.MedioRecepcion = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        ren.fechaRecepcion = reader.GetDateTime(6);
                        ren.UltimoEstado = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        ren.Sector = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        ren.Formato = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        ren.MotivoRechazo = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        ren.Estatus = string.Empty;
                        ren.Bitacora = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        if (reader.GetBoolean(12) == true) { ren.EstadoDeclaracion = "Anual"; } else { ren.EstadoDeclaracion = "Mensual"; }
                        ren.RazonSocial = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        ren.Periodo = reader.GetInt32(14);
                        ren.sPeriodo = reader.GetString(15);
                        ren.Operaciones = reader.GetInt32(16);
                        ren.Ejercicio = reader.GetInt32(17);
                        ren.fechaModificacion = reader.GetDateTime(18);
                        if (reader.GetBoolean(19) == true) { ren.FormaDeclaracion = "Normal"; } else { ren.EstadoDeclaracion = "Complementaria"; }
                        Lren.Add(ren);
                    }
                }
            }
            catch (Exception e)
            {
                SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
                return null;
            }
            finally
            {
                if (c != null)
                    c.CloseConnection();

            }


            return Lren;
        }

        public List<CatalogoSalida> DatosCatalogo(string opc)
        {
            List<CatalogoSalida> LCS = new List<CatalogoSalida>();
            CatalogoSalida CS = null;
            String sql = opc;
            IDataParameter[] parametros = new IDataParameter[0];

            if (sql == "")
            {
                LCS = null;
            }
            else
            {
                DataAccessHelper c = null;
                try
                {
                    c = this.AbrirConexion();

                    using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
                    {
                        while (reader.Read())
                        {
                            CS = new CatalogoSalida();
                            CS.IdCatalogo = reader.IsDBNull(1) ? string.Empty : reader.GetString(0);
                            CS.Descripcion = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            LCS.Add(CS);
                        }
                    }
                }
                catch (Exception e)
                {
                    SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
                    return null;
                }
                finally
                {
                    if (c != null)
                        c.CloseConnection();

                }
            }

            return LCS;
        }

        public List<EstadisticaSalida> DatosEstadistica(EstadisticaEntrada estadistica)
        {
            List<EstadisticaSalida> LCS = new List<EstadisticaSalida>();
            EstadisticaSalida CS = null;
            String sql = "ConsultaEstadistica";

            IDataParameter[] parametros = new IDataParameter[6];
            if (estadistica.TipoArchivo == null) { estadistica.TipoArchivo = ""; }
            if (estadistica.MedioRecepcion == null) { estadistica.MedioRecepcion = ""; }
            if (estadistica.Periodo == null) { estadistica.Periodo = ""; }
            if (estadistica.fechaInicio == null) { estadistica.fechaInicio = ""; }
            if (estadistica.fechaFin == null) { estadistica.fechaFin = ""; }
            if (estadistica.Rfc == null) { estadistica.Rfc = ""; }

            parametros[0] = new SqlParameter("@TipoArchivo", estadistica.TipoArchivo);
            parametros[1] = new SqlParameter("@MedioRecepcion", estadistica.MedioRecepcion);
            parametros[2] = new SqlParameter("@Periodo", estadistica.Periodo);
            parametros[3] = new SqlParameter("@fechaInicio", estadistica.fechaInicio);
            parametros[4] = new SqlParameter("@fechaFin", estadistica.fechaFin);
            parametros[5] = new SqlParameter("@Rfc", estadistica.Rfc);
            if (sql == "")
            {
                LCS = null;
            }
            else
            {
                DataAccessHelper c = null;
                try
                {
                    c = this.AbrirConexion();

                    using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
                    {
                        while (reader.Read())
                        {
                            CS = new EstadisticaSalida();
                            CS.Id = reader.GetInt32(0);
                            CS.Descripcion = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            CS.Valor = reader.GetInt32(2);
                            LCS.Add(CS);
                        }
                    }
                }
                catch (Exception e)
                {
                    SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
                    return null;
                }
                finally
                {
                    if (c != null)
                        c.CloseConnection();

                }
            }

            return LCS;
        }

        public String generarAcuse(int Folio)
        {

            String sql = "ConsultaAcuse";
            IDataParameter[] parametros = new IDataParameter[2];
            parametros[0] = new SqlParameter("@Folio", Folio);
            parametros[1] = new SqlParameter("@SMotivos", "0");
            IDataParameter[] parametros2 = new IDataParameter[2];
            parametros2[0] = new SqlParameter("@Folio", Folio);
            parametros2[1] = new SqlParameter("@SMotivos", "1");
            XDocument xx = null;
            String html = "";
            DataAccessHelper c = null;
            DataAccessHelper c2 = null;
            try
            {
                c = this.AbrirConexion();

                using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) == 4) //ACEPTADA
                        {
                            xx = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                            //xx.Add(new XDeclaration("1.0", "utf-8", "yes"));

                            string rfc = reader.IsDBNull(1) ? " " : reader.GetString(1);
                            string denominacion = reader.IsDBNull(2) ? " " : reader.GetString(2);
                            string fechaPresentacion = reader.IsDBNull(3) ? " " : reader.GetString(3);
                            string folioRecepcion = reader.IsDBNull(4) ? " " : reader.GetInt32(4).ToString();
                            string numeroOperacion = reader.IsDBNull(5) ? " " : reader.GetString(5);
                            string nombreArchivo = reader.IsDBNull(6) ? " " : reader.GetString(6);
                            string tamanoArchivo = reader.IsDBNull(7) ? " " : reader.GetInt32(7).ToString();
                            string ejercicio = reader.IsDBNull(8) ? " " : reader.GetInt32(8).ToString();
                            string periodo = reader.IsDBNull(9) ? " " : reader.GetInt32(9).ToString();
                            string tipo = reader.IsDBNull(10) ? " " : reader.GetInt32(10).ToString();
                            string totalRecaudado = reader.IsDBNull(11) ? " " : reader.GetInt64(11).ToString();
                            string totalEnterado = reader.IsDBNull(12) ? " " : reader.GetInt64(12).ToString();
                            string fechaHoraEmisionAcuse = reader.IsDBNull(13) ? " " : reader.GetString(13);
                            string sello = reader.IsDBNull(14) ? " " : reader.GetString(14);
                            string cadenaOriginal = reader.IsDBNull(15) ? " " : reader.GetString(15);

                            if (reader.GetInt32(16) == 0)
                            {

                                xx.Add(new XElement("AcuseAceptacionMensualIDE",
                                         new XAttribute("version", "1.0"),
                                         new XAttribute("rfc", rfc),
                                         new XAttribute("denominacion", denominacion),
                                         new XAttribute("fechaPresentacion", fechaPresentacion),
                                         new XAttribute("folioRecepcion", folioRecepcion),
                                         new XAttribute("numeroOperacion", numeroOperacion),
                                         new XAttribute("nombreArchivo", nombreArchivo),
                                         new XAttribute("tamanoArchivo", tamanoArchivo),
                                         new XAttribute("ejercicio", ejercicio),
                                         new XAttribute("periodo", getPeriodo(periodo)),
                                         new XAttribute("tipo", getTipoDecla(Convert.ToInt32(tipo))),
                                         new XAttribute("totalRecaudado", totalRecaudado),
                                         new XAttribute("totalEnterado", totalEnterado),
                                         new XAttribute("fechaHoraEmisionAcuse", fechaHoraEmisionAcuse),
                                         new XAttribute("sello", sello),
                                         new XAttribute("cadenaOriginal", cadenaOriginal)));
                            }
                            else
                            {
                                xx.Add(new XElement("AcuseAceptacionAnualIDE",
                                   new XAttribute("version", "1.0"),
                                         new XAttribute("rfc", rfc),
                                         new XAttribute("denominacion", denominacion),
                                         new XAttribute("fechaPresentacion", fechaPresentacion),
                                         new XAttribute("folioRecepcion", folioRecepcion),
                                         new XAttribute("numeroOperacion", numeroOperacion),
                                         new XAttribute("nombreArchivo", nombreArchivo),
                                         new XAttribute("tamanoArchivo", tamanoArchivo),
                                         new XAttribute("ejercicio", ejercicio),
                                         new XAttribute("periodo", getPeriodo(periodo)),
                                         new XAttribute("tipo", getTipoDecla(Convert.ToInt32(tipo))),
                                         new XAttribute("totalRecaudado", totalRecaudado),
                                         new XAttribute("totalEnterado", totalEnterado),
                                         new XAttribute("fechaHoraEmisionAcuse", fechaHoraEmisionAcuse),
                                         new XAttribute("sello", sello),
                                         new XAttribute("cadenaOriginal", cadenaOriginal)));
                            }
                        }
                        else if (reader.GetInt32(0) == 5) //RECHAZADA
                        {
                            xx = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                            string rfc = reader.IsDBNull(1) ? " " : reader.GetString(1);
                            string denominacion = reader.IsDBNull(2) ? " " : reader.GetString(2);
                            string fechaPresentacion = reader.IsDBNull(3) ? " " : reader.GetString(3);
                            string folioRecepcion = reader.IsDBNull(4) ? " " : reader.GetInt32(4).ToString();
                            string nombreArchivo = reader.IsDBNull(5) ? " " : reader.GetString(5);
                            string ejercicio = reader.IsDBNull(6) ? " " : reader.GetInt32(6).ToString();
                            string tamanoArchivo = reader.IsDBNull(7) ? " " : reader.GetInt32(7).ToString();
                            string tipo = reader.IsDBNull(8) ? " " : reader.GetInt32(8).ToString();
                            string periodo = reader.IsDBNull(9) ? " " : reader.GetInt32(9).ToString();
                            string fechaRechazo = reader.IsDBNull(10) ? " " : reader.GetString(10);


                            String[] desc = new String[30];
                            int i = 0;
                            c2 = this.AbrirConexion();
                            using (IDataReader reader2 = c2.ExecuteReaderStoreProcedure(sql, parametros2))
                            {
                                while (reader2.Read())
                                {
                                    desc[i] = reader2.IsDBNull(0) ? " " : reader2.GetString(0);
                                    i++;
                                }
                            }
                            if (c2 != null) c2.CloseConnection();
                            // xx.Add(new XDeclaration("1.0", "", string.Empty));
                            if (reader.GetInt32(11) == 0)
                            {
                                xx.Add(new XElement("AcuseRechazoMensualIDE",
                                   new XAttribute("version", "1.0"),
                                   new XAttribute("rfc", rfc),
                                   new XAttribute("denominacion", denominacion),
                                   new XAttribute("fechaPresentacion", fechaPresentacion),
                                   new XAttribute("folioRecepcion", folioRecepcion),
                                   new XAttribute("nombreArchivo", nombreArchivo),
                                   new XAttribute("ejercicio", ejercicio),
                                   new XAttribute("tamanoArchivo", tamanoArchivo),
                                   new XAttribute("tipo", getTipoDecla(Convert.ToInt32(tipo))),
                                   new XAttribute("periodo", getPeriodo(periodo)),
                                   new XAttribute("fechaRechazo", fechaRechazo)));
                                int s = 0;
                                while (desc[s] != null)
                                {
                                    xx.Element("AcuseRechazoMensualIDE").Add(new XElement("Error", new XAttribute("descripcion", desc[s].ToString())));
                                    s++;
                                }

                            }
                            else
                            {
                                xx.Add(new XElement("AcuseRechazoMensualIDE",
                                   new XAttribute("version", "1.0"),
                                   new XAttribute("rfc", rfc),
                                   new XAttribute("denominacion", denominacion),
                                   new XAttribute("fechaPresentacion", fechaPresentacion),
                                   new XAttribute("folioRecepcion", folioRecepcion),
                                   new XAttribute("nombreArchivo", nombreArchivo),
                                   new XAttribute("ejercicio", ejercicio),
                                   new XAttribute("tamanoArchivo", tamanoArchivo),
                                   new XAttribute("tipo", getTipoDecla(Convert.ToInt32(tipo))),
                                   new XAttribute("periodo", getPeriodo(periodo)),
                                   new XAttribute("fechaRechazo", fechaRechazo)));
                                int s = 0;
                                while (desc[s] != null)
                                {
                                    xx.Element("AcuseRechazoMensualIDE").Add(new XElement("Error", new XAttribute("descripcion", desc[s].ToString())));
                                    s++;
                                }
                            }
                        }



                        else { xx = null; }
                    }
                }
            }
            catch (Exception e)
            {
                SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
                return null;
            }
            finally
            {
                if (c != null)
                    c.CloseConnection();

            }

            String pathXSLT = Path.Combine(HttpRuntime.AppDomainAppPath, "Negocio/Esquema/PlantillaAcuse.xslt");
            if (xx == null)
            { html = ""; }
            else { html = runXSLT(pathXSLT, xx.ToString()); }
            return html;

        }

        public static string getValorString(string settingName)
        {
            return SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(settingName);
        }

        public string runXSLT(string xsltFile, string inputXML)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XslCompiledTransform xslt = new XslCompiledTransform(true);
            xslt.Load(xsltFile);
            StringReader StrReader = new StringReader(inputXML);
            XmlTextReader XmlReader = new XmlTextReader(StrReader);
            Stream stream = new MemoryStream();
            XmlWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
            xslt.Transform(XmlReader, writer);
            stream.Position = 0;
            XmlDoc.Load(stream);
            return XmlDoc.InnerXml;

        }
        private string getPeriodo(string periodo)
        {
            try
            {
                int intPeriodo = Convert.ToInt32(periodo);

                switch (intPeriodo)
                {
                    case 1:
                        periodo = "ENERO";
                        break;
                    case 2:
                        periodo = "FEBRERO";
                        break;
                    case 3:
                        periodo = "MARZO";
                        break;
                    case 4:
                        periodo = "ABRIL";
                        break;
                    case 5:
                        periodo = "MAYO";
                        break;
                    case 6:
                        periodo = "JUNIO";
                        break;
                    case 7:
                        periodo = "JULIO";
                        break;
                    case 8:
                        periodo = "AGOSTO";
                        break;
                    case 9:
                        periodo = "SEPTIEMBRE";
                        break;
                    case 10:
                        periodo = "OCTUBRE";
                        break;
                    case 11:
                        periodo = "NOVIEMBRE";
                        break;
                    case 12:
                        periodo = "DICIEMBRE";
                        break;
                    default:
                        periodo = "";
                        break;
                }
            }
            catch
            {
                periodo = periodo;
                return periodo;
            }
            return periodo;
        }

        private string getTipoDecla(int tipo)
        {
            string tipodecla = string.Empty;
            if (tipo == 1)
            {
                tipodecla = "Normal";
            }
            else
            {
                tipodecla = "Complementaria";
            }
            return tipodecla;
        }
    }
}