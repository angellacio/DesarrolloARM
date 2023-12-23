
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Busquedas:SAT.CreditosFiscales.Motor.AccesoDatos.Busquedas.BusquedaLineaCaptura:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DatosProcesamiento = SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Busquedas
{
    /// <summary>
    /// Clase de acceso a datos durante la búsqueda de una línea de captura
    /// </summary>
    public class BusquedaLineaCaptura
    {
        private const string SP_OBTIENEAGRUPADORESPORDG = "pObtieneAgrupadoresPorDatosGenerales";
        private const string SP_OBTIENECONCEPTOSPORAGRUPADOR = "pObtieneConceptosOriginalPorAgrupador";
        private const string SP_OBTIENECONCEPTOSHIJOPORAGRUPADOR = "pObtieneConceptosOriginalHijoPorAgrupador";
        private const string SP_OBTIENEDESCUENTOSPORCONCEPTO = "pObtieneDescuentosOriginalPorConcepto";
        private const string SP_OBTIENEDESCUENTOSPORCONCEPTOHIJO = "pObtieneDescuentosOriginalHijoPorConcepto";
        private const string SP_OBTIENELINEACAPTURAPROCESAMIENTO = "pObtieneLineaCapturaPorProcesamiento";
        private const string SP_OBTIENERESOLUCIONESENLINEACAPTURA = "pMotorObtieneAgrupadoresEnLC";
        private const string SP_OBTIENELINEASCAPTURAPORAGRUPADOR = "pMotorObtieneLineasCapturaPorAgrupador";
        private const string SP_OBTIENEPROCESAMIENTOPORDATOSGENERALES = "pObtieneProcesamientosPorDatosGenerales";
        private const string SP_OBTIENEXMLORIGINAL = "pObtieneXMLOriginal";
        private const string SP_OBTIENEFOLIOPORLINEACAPTURA = "pMotorObtieneFolioPorLineaCaptura";
        private const string SP_OBTIENECONSULTAFORMATOS = "pConsultaFormatos";


        /// <summary>
        /// Consulta Formatos XFiltros
        /// </summary>
      
        /// <returns>Lista de Formatos que coincidan con la búsqueda</returns>
        public static List<RespuestaListaFormatos> ConsultaFormatosXFiltros(int ADR,string RFC,
            string Folio, 
            string LineaDeCaptura, 
            string No_de_Resolucion, 
            int rango_de_emision_FechaPago, DateTime fecha_ini, DateTime fecha_fin)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENECONSULTAFORMATOS);
            var lstFormatos = new List<RespuestaListaFormatos>();
            db.AddInParameter(cmd, "@ADR", System.Data.DbType.Int32, ADR);
            db.AddInParameter(cmd, "@RFC", System.Data.DbType.String, RFC);
            db.AddInParameter(cmd, "@Folio", System.Data.DbType.String, Folio);
            db.AddInParameter(cmd, "@LineaDeCaptura", System.Data.DbType.String, LineaDeCaptura);
            db.AddInParameter(cmd, "@No_de_Resolucion", System.Data.DbType.String, No_de_Resolucion);
            db.AddInParameter(cmd, "@rango_de_emision_FechaPago", System.Data.DbType.Int32, rango_de_emision_FechaPago);
            db.AddInParameter(cmd, "@fecha_ini", System.Data.DbType.DateTime, fecha_ini);
            db.AddInParameter(cmd, "@fecha_fin", System.Data.DbType.DateTime, fecha_fin);

            using (var Formato = db.ExecuteReader(cmd))
            {
                while (Formato.Read())
                {
                    lstFormatos.Add(
                        new RespuestaListaFormatos
                        {
                            ADR = Convert.ToString(Formato["ADR"]),
                            RFC =Convert.ToString(Formato["RFC"]),
                            RazonSocial = Convert.ToString(Formato["Nombre"]) + Convert.ToString(Formato["ApellidoPaterno"]) + Convert.ToString(Formato["ApellidoMaterno"]) + Convert.ToString(Formato["RazonSocial"]),
                            Folio = Convert.ToString(Formato["FolioDyP"]),
                            fechaEmision = Convert.ToString(Formato["FechaEmision"]),
                            IdResolucion = Convert.ToString(Formato["IdResolucion"]),
                            NumResolucion = (Convert.ToString(Formato["ValorAgrupador"]).Split('|').Length>0)?Convert.ToString(Formato["ValorAgrupador"]).Split('|')[0]:"",//regla de split
                            LineaDeCaptura = Convert.ToString(Formato["LineaCaptura"]),
                            Importe=Convert.ToString(Formato["ImporteTotalPagar"]),
                            FormaDePago = Convert.ToString(Formato["FormaPago"]),
                            FechaDeVigencia=Convert.ToString(Formato["FechaVigencia"]),
                            FechaDePago = Convert.ToString(Formato["FechaPago"]),
                            TipoDePago= Convert.ToDouble(Formato["ImporteTotalPagar"])==0 ? "V":"E",
                            Aplicativo = Convert.ToString(Formato["Aplicacion"]),
                             Modulo="",
                             RFC_Usr_Generador="",
                             XML=""
                                                  
                        }
                        );
                }
            }
            return lstFormatos;
          
        }

        /// <summary>
        /// Consulta en base de datos los procesamientos que cumplan con el filtro
        /// </summary>
        /// <param name="RFC">RFC a consultar</param>
        /// <param name="ALR">ALR a consultar</param>
        /// <param name="TipoDocumento">Tipo de documento a consultar</param>
        /// <param name="FechaVigencia">Fecha de vigencia a consultar</param>
        /// <returns>Lista de string de los id's de procesamiento</returns>
        public static List<string> ConsultaProcesamientPorDatosGenerales(string RFC
                                                                                         , int ALR
                                                                                         , int TipoDocumento
                                                                                         , string FechaVigencia
                                                                                         , string FormaPago
                                                                                         , long ImporteTotal
                                                                                         , int numeroParcialidad
                                                                                         , int idAplicacion)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEPROCESAMIENTOPORDATOSGENERALES);          

            var lstProcesamiento = new List<string>();

            db.AddInParameter(cmd, "@RFC", System.Data.DbType.String, RFC);
            db.AddInParameter(cmd, "@ALR", System.Data.DbType.Int32, ALR);
            db.AddInParameter(cmd, "@TipoDocumento", System.Data.DbType.Int32, TipoDocumento);
            db.AddInParameter(cmd, "@FechaVigencia", DbType.String, FechaVigencia);
            db.AddInParameter(cmd, "@FormaPago", DbType.String, FormaPago);
            db.AddInParameter(cmd, "@ImporteTotal", DbType.Int64, Convert.ToInt64(ImporteTotal));
            db.AddInParameter(cmd, "@NumeroParcialidad", DbType.Int32, numeroParcialidad);
            db.AddInParameter(cmd, "@idAplicacion", DbType.Int32, idAplicacion);

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstProcesamiento.Add(rdReglas["IdProcesamiento"].ToString());
                }
            }
            return lstProcesamiento;
        }

        /// <summary>
        /// Consulta todos los agrupadores de un procesamiento
        /// </summary>
        /// <param name="IdProcesamiento">Id del procesamiento a consultar</param>
        /// <returns>Lista de los agrupadores que coinciden con la búsqueda</returns>
        public static List<DatosProcesamiento.AgrupadorDB> ConsultaAgrupadoresPorDatosGenerales(string IdProcesamiento)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEAGRUPADORESPORDG);
           
            var lstAgrupadores = new List<DatosProcesamiento.AgrupadorDB>();

            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, Guid.Parse(IdProcesamiento));

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstAgrupadores.Add(
                        new DatosProcesamiento.AgrupadorDB
                        {
                            IdProcesamiento = new Guid(rdReglas["IdProcesamiento"].ToString()),
                            IdAgrupador = Convert.ToInt32(rdReglas["IdAgrupador"].ToString()),
                            ValorAgrupador = rdReglas["ValorAgrupador"].ToString(),
                            ImporteTotalPagar = Convert.ToDecimal(rdReglas["ImporteTotalPagar"])
                        }
                        );
                }
            }
            return lstAgrupadores;
        }

        /// <summary>
        /// Consulta los Conceptos Originales que coincidan con el id de agrupador
        /// </summary>
        /// <param name="idAgrupador">Id de agrupador a consultar</param>
        /// <param name="idProcesamiento">Id del procesamiento a consultar</param>
        /// <returns>Lista de ConceptosOriginales que coincidan con la búsqueda</returns>
        public static List<DatosProcesamiento.ConceptoOriginalDB> ConsultaConceptosPorAgrupador(int idAgrupador
                                                                                          , Guid idProcesamiento)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENECONCEPTOSPORAGRUPADOR);            
            var lstConceptoss = new List<DatosProcesamiento.ConceptoOriginalDB>();
            db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, idAgrupador);
            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, idProcesamiento);

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstConceptoss.Add(
                        new DatosProcesamiento.ConceptoOriginalDB
                        {
                            IdConceptoOriginal = Convert.ToInt32(rdReglas["IdConceptoOriginal"]),
                            IdProcesamiento = new Guid(rdReglas["IdProcesamiento"].ToString()),
                            IdAgrupador = Convert.ToInt32(rdReglas["IdAgrupador"]),
                            ConceptoOrigen = rdReglas["ConceptoOrigen"].ToString(),
                            Ejercicio = rdReglas["Ejercicio"] != DBNull.Value ? Convert.ToInt32(rdReglas["Ejercicio"]) : 0,
                            FechaCausacion = rdReglas["FechaCausacion"] != DBNull.Value ? Convert.ToDateTime(rdReglas["FechaCausacion"]).ToString("dd/MM/yyyy") : null,
                            CreditoSir = Convert.ToInt32(rdReglas["CreditoSir"]),
                            ImportePagar = Convert.ToDecimal(rdReglas["ImportePagar"])
                        }
                        );
                }
            }
            return lstConceptoss;
        }

        /// <summary>
        /// Consulta los Conceptos Hijo que coincidad con el filtro siguiente
        /// </summary>
        /// <param name="idAgrupador">Id del agrupador a consultar</param>
        /// <param name="idProcesamiento">Id del procesamiento a consultar</param>
        /// <param name="claveConceptoOriginal">Clave del concepto original</param>
        /// <param name="idConceptoOriginal">Id del concepto Original</param>
        /// <returns>Lista de ConceptosHijo que coincidan con la búsqueda</returns>
        public static List<DatosProcesamiento.ConceptoOriginalHijo> ConsultaConceptosHijoPorAgrupador(int idAgrupador
                                                                                          , Guid idProcesamiento
                                                                                          , string claveConceptoOriginal
                                                                                          , int idConceptoOriginal)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENECONCEPTOSHIJOPORAGRUPADOR);
            
            var lstConceptoss = new List<DatosProcesamiento.ConceptoOriginalHijo>();

            db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, idAgrupador);
            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
            db.AddInParameter(cmd, "@IdConceptoOriginal", DbType.Int32, idConceptoOriginal);
            db.AddInParameter(cmd, "@ClaveConceptoOriginal", System.Data.DbType.String, claveConceptoOriginal);

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstConceptoss.Add(
                        new DatosProcesamiento.ConceptoOriginalHijo
                        {
                            IdProcesamiento = new Guid(rdReglas["IdProcesamiento"].ToString()),
                            IdAgrupador = Convert.ToInt32(rdReglas["IdAgrupador"].ToString()),
                            IdConceptoOriginalHijo = Convert.ToInt32(rdReglas["IdConceptoOriginalHijo"].ToString()),
                            ConceptoOrigen = rdReglas["ConceptoOrigen"].ToString(),
                            ConceptoOrigenHijo = rdReglas["ConceptoOriginalHijo"].ToString(),
                            ImportePagar = Convert.ToDecimal(rdReglas["ImportePagar"].ToString()),
                            Ejercicio = rdReglas["Ejercicio"] != DBNull.Value ? Convert.ToInt32(rdReglas["Ejercicio"]) : 0,
                            FechaCausacion = rdReglas["FechaCausacion"] != DBNull.Value ? Convert.ToDateTime(rdReglas["FechaCausacion"]).ToString("dd/MM/yyyy") : null,
                        }
                        );
                }
            }
            return lstConceptoss;
        }

        /// <summary>
        /// Consulta Los descuentos del Concepto Original que coincidan con el filtro
        /// </summary>
        /// <param name="idAgrupador">Id del agrupador</param>
        /// <param name="idProcesamiento">Id del procesamiento</param>
        /// <param name="idConceptoOriginal">Id del concepto original</param>
        /// <returns>Lista de descuentos original que coincidan con la búsqueda</returns>
        public static List<DatosProcesamiento.DescuentosOriginal> ConsultaDescuentosPorConcepto(int idAgrupador
                                                                                          , Guid idProcesamiento
                                                                                          , int idConceptoOriginal)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEDESCUENTOSPORCONCEPTO);

            var lstDescuentos = new List<DatosProcesamiento.DescuentosOriginal>();

            db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, idAgrupador);
            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
            db.AddInParameter(cmd, "@IdConceptoOriginal", System.Data.DbType.Int32, idConceptoOriginal);

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    lstDescuentos.Add(
                        new DatosProcesamiento.DescuentosOriginal
                        {
                            IdProcesamiento = new Guid(rdReglas["IdProcesamiento"].ToString()),
                            IdAgrupador = Convert.ToInt32(rdReglas["IdAgrupador"].ToString()),
                            ConceptoOriginal = rdReglas["ConceptoOrigen"].ToString(),
                            IdDescuento = rdReglas["IdDescuento"].ToString(),
                            ImporteDescuento = Convert.ToDecimal(rdReglas["ImporteDescuento"].ToString()),
                        }
                        );
                }
            }
            return lstDescuentos;
        }

        /// <summary>
        /// Consulta los Descuentos de un concepto hijo
        /// </summary>
        /// <param name="idAgrupador">Id del agrupador</param>
        /// <param name="idProcesamiento">Id del procesamiento</param>
        /// <param name="idConceptoOriginal">Id del concepto original</param>
        /// <param name="idConceptoOriginalHijo">Id del concepto hijo</param>
        /// <returns>Lista de descuentos original hijo que coincidan con la búsqueda</returns>
        public static List<DatosProcesamiento.DescuentoOriginalHijo> ConsultaDescuentosPorConceptoHijo(int idAgrupador
                                                                                         , Guid idProcesamiento
                                                                                         , int idConceptoOriginal
                                                                                         , int idConceptoOriginalHijo)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEDESCUENTOSPORCONCEPTOHIJO);

            var lstDescuentos = new List<DatosProcesamiento.DescuentoOriginalHijo>();

            db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, idAgrupador);
            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
            db.AddInParameter(cmd, "@IdConceptoOriginal", System.Data.DbType.Int32, idConceptoOriginal);
            db.AddInParameter(cmd, "@IdConceptoOriginalHijo", System.Data.DbType.Int32, idConceptoOriginalHijo);

            using (var rdReglas = db.ExecuteReader(cmd))
            {

                while (rdReglas.Read())
                {

                    lstDescuentos.Add(
                        new DatosProcesamiento.DescuentoOriginalHijo
                        {
                            IdProcesamiento = new Guid(rdReglas["IdProcesamiento"].ToString()),
                            IdAgrupador = Convert.ToInt32(rdReglas["IdAgrupador"].ToString()),
                            ConceptoOriginal = rdReglas["ConceptoOrigen"].ToString(),
                            ConceptoOriginalHijo = rdReglas["ConceptoOriginalHijo"].ToString(),
                            IdDescuento = rdReglas["IdDescuento"].ToString(),
                            ImporteDescuento = Convert.ToDecimal(rdReglas["ImporteDescuento"].ToString()),
                        }
                        );
                }
            }

            return lstDescuentos;
        }

        /// <summary>
        /// Consulta los datos de la línea de captura por Id de procesamiento
        /// </summary>
        /// <param name="idProcesamiento">Id de procesamiento</param>
        /// <returns>Respuesta LC</returns>
        public static RespuestaLC ConsultaLineaCapturaPorProcesamiento(Guid idProcesamiento)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENELINEACAPTURAPROCESAMIENTO);
            Byte idEstatusPago = 1;
            Byte idAplicacion=0;
            var lineaCaptura = new RespuestaLC();

            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, idProcesamiento);

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                lineaCaptura.LineasCaptura = new RespuestaLCDatosLinea[1];
                var DatosLineaCaptura = new RespuestaLCDatosLinea();

                while (rdReglas.Read())
                {
                    DatosLineaCaptura.Folio = rdReglas["FolioDyP"].ToString();
                    DatosLineaCaptura.LineaCaptura = rdReglas["LineaCaptura"].ToString();
                    idEstatusPago = Byte.Parse(rdReglas["IdEstatus"].ToString());
                    idAplicacion = Byte.Parse(rdReglas["IdAplicacion"].ToString());
                }

                lineaCaptura.LineasCaptura[0] = DatosLineaCaptura;

                if (idEstatusPago == 2 && idAplicacion==5)
                {
                    lineaCaptura.ListaErrores = new String[1];
                    lineaCaptura.ListaErrores[0] = "La línea de captura fue previamente pagada";
                }
            }

            return lineaCaptura;
        }

        public static RespuestaSolicitudOriginal ConsultaXMLOriginal(string Folio)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEXMLORIGINAL);
            var respuesta = new RespuestaSolicitudOriginal();

            db.AddInParameter(cmd, "@Folio", DbType.String, Folio);

            using (var rdXMLOriginal = db.ExecuteReader(cmd))
            {

                while (rdXMLOriginal.Read())
                {
                    respuesta.XmlOriginal = rdXMLOriginal["Mensaje"].ToString();
                    respuesta.LineaCaptura = rdXMLOriginal["LineaCaptura"].ToString();
                    respuesta.FechaPago = rdXMLOriginal["FechaPago"].ToString();
                    respuesta.FechaEmision = rdXMLOriginal["FechaEmision"].ToString();
                }

            }
            return respuesta;

        }


        /// <summary>
        /// Consulta los datos de resoluciones de una línea de captura
        /// </summary>
        /// <param name="lineaCaptura">Línea de captura</param>
        /// <returns>RespuestaResolucionesEnLC</returns>
        public static RespuestaResolucionesEnLC ConsultaResolucionesEnLineaCaptura(string lineaCaptura)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENERESOLUCIONESENLINEACAPTURA);
            
            var respuestaResoluciones = new RespuestaResolucionesEnLC();

            var resoluciones = new List<RespuestaResolucionesEnLCResolucion>();

            db.AddInParameter(cmd, "@LineaCaptura", System.Data.DbType.String, lineaCaptura);

            using (var rdResoluciones = db.ExecuteReader(cmd))
            {

                while (rdResoluciones.Read())
                {
                    var resolucion = new RespuestaResolucionesEnLCResolucion();
                    resolucion.Canonico = rdResoluciones["ValorAgrupador"].ToString();
                    resolucion.NumeroResolucion = resolucion.Canonico.Split('|')[0];
                    resoluciones.Add(resolucion);
                }
            }
            respuestaResoluciones.Resoluciones = resoluciones.ToArray();
            return respuestaResoluciones;
        }

        /// <summary>
        /// Consulta lineas de captura existentes por agrupador
        /// </summary>
        /// <param name="numeroAgrupador">número de agrupador</param>
        /// <param name="fecha">fecha</param>
        /// <param name="autoridadId">autoridad id</param>
        /// <param name="alr">alr</param>
        /// <param name="rfc">rfc</param>
        /// <returns>Lista de RespuestaLineasCapturaExistentesDatosLinea</returns>
        public static List<RespuestaLineasCapturaExistentesDatosLinea> ConsultaLineasDeCapturaExistentes(string numeroAgrupador, string fecha, int autoridadId, string alr, string rfc)
        {
            string valorAgrupador = numeroAgrupador + "|" + fecha + "|" + autoridadId.ToString() + "|" + alr + "|" + rfc;

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENELINEASCAPTURAPORAGRUPADOR);            

            var lineasCaptura = new List<RespuestaLineasCapturaExistentesDatosLinea>();
            db.AddInParameter(cmd, "@ValorAgrupador", System.Data.DbType.String, valorAgrupador);

            using (var rdResoluciones = db.ExecuteReader(cmd))
            {

                while (rdResoluciones.Read())
                {
                    var lineaCaptura = new RespuestaLineasCapturaExistentesDatosLinea();
                    lineaCaptura.NumeroResolucion = numeroAgrupador;
                    lineaCaptura.FechaDocumento = fecha;
                    lineaCaptura.Autoridad = autoridadId.ToString();
                    lineaCaptura.ALR = alr;
                    lineaCaptura.RFC = rfc;
                    lineaCaptura.FechaGeneracion = rdResoluciones["FechaEmision"].ToString();
                    lineaCaptura.FechaVigenecia = rdResoluciones["FechaVigencia"].ToString();
                    lineaCaptura.Folio = rdResoluciones["FolioDyP"].ToString();
                    lineaCaptura.LineaCaptura = rdResoluciones["LineaCaptura"].ToString();
                    lineaCaptura.Importe = rdResoluciones["ImporteTotalPagar"].ToString();
                    lineasCaptura.Add(lineaCaptura);
                }
            }

            return lineasCaptura;
        }

        /// <summary>
        /// Consulta el folio de una línea de captura
        /// </summary>
        /// <param name="lineaCaptura">Línea de captura</param>
        /// <returns>Folio</returns>
        public static String ConsultaFolioDyPPorLineaCaptura(String lineaCaptura)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEFOLIOPORLINEACAPTURA);
            String folio= String.Empty;
            db.AddInParameter(cmd, "@LineaCaptura", System.Data.DbType.String, lineaCaptura);

            using (var res = db.ExecuteReader(cmd))
            {
                while (res.Read())
                {
                    folio= res["FolioDyP"].ToString();
                }
            }
            return folio;
        }
    }

}
