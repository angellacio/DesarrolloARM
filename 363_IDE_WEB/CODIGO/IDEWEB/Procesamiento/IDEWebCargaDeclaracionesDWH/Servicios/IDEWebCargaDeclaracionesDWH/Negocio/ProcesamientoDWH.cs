using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAT.DyP.Util.Configuration;
using System.IO;
using SAT.DyP.Util.Logging;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;

namespace IDEWebCargaDeclaracionesDWH
{
    class ProcesamientoDWH
    {
        public static XDocument declaracion = new XDocument();

        public static DatosDeclaracion ObtenerDeclaracionAProcesar()
        {
            return AccesoDatos.ObtenerDeclaracionAProcesar();
        }

        public static void IniciarCargaDWH(DatosDeclaracion datosDeclaracion)
        {
            SQLXMLBULKLOADLib.SQLXMLBulkLoadClass objBL = null;

            try
            {
                declaracion = null;

                string archivoEnFileShare = getPathDeclaracion(datosDeclaracion);
                string fileShare = getFileShare();

                if (archivoEnFileShare != string.Empty && fileShare != string.Empty)
                {
                    string archivoResumen = Path.Combine(fileShare, Path.GetFileNameWithoutExtension(datosDeclaracion.ArchivoFisico) + "_temp" + Path.GetExtension(datosDeclaracion.ArchivoFisico));


                    declaracion = new XDocument();
                    declaracion = XDocument.Load(archivoEnFileShare);

                    XDocument resumen = new XDocument();

                    if (!datosDeclaracion.EsAnual)
                    {
                        resumen = generarResumen(getDatosGenerales(), generarDetallesDIM(), generarEnterosDIM(), datosDeclaracion);
                    }
                    if (datosDeclaracion.EsAnual)
                    {
                        resumen = generarResumen(getDatosGenerales(), generarDetallesDIA(), datosDeclaracion);
                    }

                    resumen.Save(archivoResumen);



                    declaracion = null;
                    resumen = null;

                    string strConexion = ConfigurationManager.ApplicationSettings.ReadSetting(Constantes.SettingNames.BaseDatos);

                    objBL = new SQLXMLBULKLOADLib.SQLXMLBulkLoadClass();
                    objBL.ConnectionString = "provider=sqloledb;" + strConexion;
                    objBL.KeepIdentity = false;

                    int intentos = 1;
                    while (intentos <= 3)
                    {
                        try
                        {
                            objBL.Execute(getPathEsquema(), archivoResumen);

                            AccesoDatos.ActualizarControlCargaDWH((int)datosDeclaracion.Folio, Constantes.EstatusDWH.Exitoso, "Proceso terminado.");

                            File.Delete(archivoResumen);

                            break;
                        }
                        catch (Exception ex)
                        {
                            EventLogHelper.WriteEntry(Constantes.Source, Constantes.ErrorIDEWebCargaDeclaracionesDWH + ex.Message, System.Diagnostics.EventLogEntryType.Error, Constantes.Materia);
                            AccesoDatos.ActualizarControlCargaDWH((int)datosDeclaracion.Folio, Constantes.EstatusDWH.Fallido, ex.Message);

                            if (ex.Message.ToString().Contains("Access to the path") &&
                                ex.Message.ToString().Contains("denied"))
                            {
                                intentos++;
                                System.Threading.Thread.Sleep(10000);
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                }
                else
                {
                    string folio = datosDeclaracion != null && datosDeclaracion.Folio != null ? datosDeclaracion.Folio.ToString() : "0";
                    string mensaje = "Verificar que exista la Declaración en el Fileshare correspondiente al Folio: " + folio;
                    EventLogHelper.WriteEntry(Constantes.Source, mensaje, System.Diagnostics.EventLogEntryType.Warning, Constantes.Materia);
                    AccesoDatos.ActualizarControlCargaDWH((int)datosDeclaracion.Folio, Constantes.EstatusDWH.Fallido, mensaje);

                }
            }
            catch (Exception ex)
            {
                EventLogHelper.WriteEntry(Constantes.Source, Constantes.ErrorIDEWebCargaDeclaracionesDWH + ex.Message, System.Diagnostics.EventLogEntryType.Error, Constantes.Materia);
                AccesoDatos.ActualizarControlCargaDWH((int)datosDeclaracion.Folio, Constantes.EstatusDWH.Fallido, ex.Message + ex.InnerException.ToString());
            }
            finally
            {
                if (objBL != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBL);
            }
        }

        private static List<Atributo> getDatDec(XElement element)
        {
            List<Atributo> atts = new List<Atributo>();

            foreach (XAttribute att in element.Attributes())
            {
                switch (att.Name.ToString())
                {
                    case "version":
                        atts.Add(new Atributo("cveVersion", att.Value));
                        break;
                    case "rfcDeclarante":
                        atts.Add(new Atributo("rfcDeclarante", att.Value));
                        break;
                    case "denominacion":
                        atts.Add(new Atributo("denominacionDeclarante", att.Value));
                        break;
                }
            }

            return atts;
        }

        private static List<Atributo> getRepresentanteLegal(XElement element)
        {
            List<Atributo> atts = new List<Atributo>();

            foreach (XAttribute att in element.Attributes())
            {
                switch (att.Name.ToString())
                {
                    case "rfc":
                        atts.Add(new Atributo("rfcRepresentante", att.Value));
                        break;
                    case "curp":
                        atts.Add(new Atributo("curpRepresentante", att.Value));
                        break;
                }
            }

            var e = element.Descendants("Nombre");

            foreach (XElement el in e.Elements())
            {
                switch (el.Name.ToString())
                {
                    case "NombreCompleto":
                        atts.Add(new Atributo("nombreCompletoRepresentante", el.Value));
                        break;
                    case "Nombres":
                        atts.Add(new Atributo("nombreRepresentante", el.Value));
                        break;
                    case "PrimerApellido":
                        atts.Add(new Atributo("apPatRepresentante", el.Value));
                        break;
                    case "SegundoApellido":
                        atts.Add(new Atributo("apMatRepresentante", el.Value));
                        break;
                }
            }

            return atts;
        }

        private static List<Atributo> getNormal(XElement element)
        {
            List<Atributo> atts = new List<Atributo>();

            foreach (XAttribute att in element.Attributes())
            {
                switch (att.Name.ToString())
                {
                    case "ejercicio":
                        atts.Add(new Atributo("ejercicio", att.Value));
                        break;
                    case "periodo":
                        atts.Add(new Atributo("cvePeriodo", att.Value));
                        break;

                }
            }

            return atts;
        }

        private static List<Atributo> getComplementaria(XElement element)
        {
            List<Atributo> atts = new List<Atributo>();

            foreach (XAttribute att in element.Attributes())
            {
                switch (att.Name.ToString())
                {
                    case "ejercicio":
                        atts.Add(new Atributo("ejercicio", att.Value));
                        break;
                    case "periodo":
                        atts.Add(new Atributo("cvePeriodo", att.Value));
                        break;
                    case "opAnterior":
                        atts.Add(new Atributo("numeroOpAnterior", att.Value));
                        break;
                    case "fechaPresentacion":
                        atts.Add(new Atributo("fechaPresentacionOpAnterior", att.Value));
                        break;

                }
            }

            return atts;
        }

        private static List<Atributo> getTotales(XElement element)
        {
            List<Atributo> atts = new List<Atributo>();

            foreach (XAttribute att in element.Attributes())
            {
                switch (att.Name.ToString())
                {
                    case "operacionesRelacionadas":
                        atts.Add(new Atributo("operacionesRelacionadas", att.Value));
                        break;
                    case "importeExcedenteDepositos":
                        atts.Add(new Atributo("depositosCausanImpuesto", att.Value));
                        break;
                    case "importeDeterminadoDepositos":
                        atts.Add(new Atributo("impuestoDeterminadoPeriodo", att.Value));
                        break;
                    case "importeRecaudadoDepositos":
                        atts.Add(new Atributo("impuestoRecaudado", att.Value));
                        break;
                    case "importePendienteRecaudacion":
                        atts.Add(new Atributo("impuestoPendienteRecaudar", att.Value));
                        break;
                    case "importePendienteDepositos":
                        atts.Add(new Atributo("impuestoPendienteRecaudar", att.Value));
                        break;
                    case "importeRemanenteDepositos":
                        atts.Add(new Atributo("remanenteRecaudadoPeriodos", att.Value));
                        break;
                    case "importeEnterado":
                        atts.Add(new Atributo("impuestoEnteradoPeriodo", att.Value));
                        break;
                    case "importeCheques":
                        atts.Add(new Atributo("recaudadoChequesCaja", att.Value));
                        break;
                    case "importeSaldoPendienteRecaudar":
                        atts.Add(new Atributo("importeSaldoPendienteRecaudar", att.Value));
                        break;
                }
            }

            return atts;
        }

        private static XElement getDatosGenerales()
        {
            XElement datosGenerales = new XElement("DatosGenerales");

            datosGenerales.Add(new XAttribute("llaveDetalle", "1"));
            datosGenerales.Add(new XAttribute("N_DEC_BCIDWHA1", "0"));
            datosGenerales.Add(new XAttribute("fechaRegistro", DateTime.Now.ToString()));


            var DIA = from c in declaracion.Descendants(Constantes.NodosXML.DIA) select c;
            var DIM = from c in declaracion.Descendants(Constantes.NodosXML.DIM) select c;






            if (DIA.Count() > 0)
            {

                XElement element = DIA.ElementAt(0);
                foreach (Atributo at in getDatDec(element))
                {
                    datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                }



                foreach (XElement repLegal in DIA.Descendants("RepresentanteLegal"))
                {
                    foreach (Atributo at in getRepresentanteLegal(repLegal))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }

                foreach (XElement normal in DIA.Descendants("Normal"))
                {
                    datosGenerales.Add(new XAttribute("cveTipoDeclaracion", "1"));
                    foreach (Atributo at in getNormal(normal))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }

                foreach (XElement complementario in DIA.Descendants("Complementaria"))
                {
                    datosGenerales.Add(new XAttribute("cveTipoDeclaracion", "2"));
                    foreach (Atributo at in getComplementaria(complementario))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }


                if (DIA.Descendants(Constantes.NodosXML.IDC).Count() > 0)
                    datosGenerales.Add(new XAttribute("cveTipoInstitucion", "1"));

                if (DIA.Descendants(Constantes.NodosXML.IDDC).Count() > 0)
                    datosGenerales.Add(new XAttribute("cveTipoInstitucion", "2"));

                var totales = DIA.Descendants("Totales");

                foreach (XElement total in totales)
                {
                    foreach (Atributo at in getTotales(total))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }

            }

            if (DIM.Count() > 0)
            {

                XElement element = DIM.ElementAt(0);
                foreach (Atributo at in getDatDec(element))
                {
                    datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                }



                foreach (XElement repLegal in DIM.Descendants("RepresentanteLegal"))
                {
                    foreach (Atributo at in getRepresentanteLegal(repLegal))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }

                foreach (XElement normal in DIM.Descendants("Normal"))
                {
                    datosGenerales.Add(new XAttribute("cveTipoDeclaracion", "1"));
                    foreach (Atributo at in getNormal(normal))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }

                foreach (XElement complementario in DIM.Descendants("Complementaria"))
                {
                    datosGenerales.Add(new XAttribute("cveTipoDeclaracion", "2"));
                    foreach (Atributo at in getComplementaria(complementario))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }


                if (DIM.Descendants(Constantes.NodosXML.IDC).Count() > 0)
                    datosGenerales.Add(new XAttribute("cveTipoInstitucion", "1"));

                if (DIM.Descendants(Constantes.NodosXML.IDDC).Count() > 0)
                    datosGenerales.Add(new XAttribute("cveTipoInstitucion", "2"));


                var totales = DIM.Descendants("Totales");

                foreach (XElement total in totales)
                {
                    foreach (Atributo at in getTotales(total))
                    {
                        datosGenerales.Add(new XAttribute(at.Nombre, at.Valor));
                    }
                }


            }

            return datosGenerales;
        }

        private static XDocument generarResumen(XElement datosGenerales, List<XElement> detallesDIM, List<XElement> enterosDIM, DatosDeclaracion datosDeclaracion)
        {
            XDocument resumen = new XDocument();

            datosGenerales.Add(new XAttribute("folio", datosDeclaracion.Folio.ToString()));
            datosGenerales.Add(new XAttribute("cveEntidadReceptora", datosDeclaracion.IdEntidadReceptora.ToString()));

            string ejercicio = "0";
            string periodo = "0";
            foreach (XAttribute att in datosGenerales.Attributes())
            {
                switch (att.Name.ToString())
                {
                    case "ejercicio":
                        ejercicio = att.Value;
                        break;
                    case "cvePeriodo":
                        periodo = att.Value;
                        break;
                }
            }

            foreach (XElement detalle in detallesDIM)
            {
                detalle.Add(new XAttribute("ejercicio", ejercicio));
                detalle.Add(new XAttribute("cvePeriodo", periodo));
                datosGenerales.Add(detalle);
            }

            foreach (XElement enteros in enterosDIM)
            {
                datosGenerales.Add(enteros);
            }

            resumen.Add(datosGenerales);
            return resumen;

        }

        private static XDocument generarResumen(XElement datosGenerales, List<XElement> detallesDIA, DatosDeclaracion datosDeclaracion)
        {
            XDocument resumen = new XDocument();

            datosGenerales.Add(new XAttribute("folio", datosDeclaracion.Folio.ToString()));
            datosGenerales.Add(new XAttribute("cveEntidadReceptora", datosDeclaracion.IdEntidadReceptora.ToString()));

            string ejercicio = "0";
            string periodo = "0";
            foreach (XAttribute att in datosGenerales.Attributes())
            {
                switch (att.Name.ToString())
                {
                    case "ejercicio":
                        ejercicio = att.Value;
                        break;
                    case "cvePeriodo":
                        periodo = att.Value;
                        break;
                }
            }

            foreach (XElement detalle in detallesDIA)
            {
                detalle.Add(new XAttribute("ejercicio", ejercicio));
                detalle.Add(new XAttribute("cvePeriodo", periodo));
                datosGenerales.Add(detalle);
            }

            resumen.Add(datosGenerales);

            return resumen;


        }

        private static List<XElement> generarDetallesDIM()
        {
            List<XElement> detalles = new List<XElement>();

            var elementosDIM = from c in declaracion.Descendants("DeclaracionInformativaMensualIDE") select c;

            int consecutivoDetalle = 0;
            foreach (var reporteRecDiaria in elementosDIM.Descendants("ReporteDeRecaudacionYEnteroDiaria"))
            {

                string fechaCorte = reporteRecDiaria.Attribute("fechaDeCorte").Value;


                foreach (var registroDetalle in reporteRecDiaria.Descendants("RegistroDeDetalle"))
                {
                    XElement detalle = null;
                    detalle = new XElement("Detalle");
                    detalle.Add(new XAttribute("fechaCorte", fechaCorte));
                    consecutivoDetalle++;
                    detalle.Add(new XAttribute("consecutivoDet", consecutivoDetalle.ToString()));
                    detalle.Add(new XAttribute("fechaRegistro", DateTime.Now.ToString()));


                    XElement personaFisica = registroDetalle.Element("PersonaFisica");
                    XElement personaMoral = registroDetalle.Element("PersonaMoral");

                  
                    if (personaFisica != null)
                    {
                        foreach (Atributo atributo in getPersonaFisica(personaFisica))
                        {
                            detalle.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                        }

                        var cuentasDePersonaMensuales = personaFisica.Descendants("numeroCuenta");

                        foreach (XElement numeroCuenta in cuentasDePersonaMensuales)
                        {
                            XElement cuentasMensuales = new XElement("CuentasMensuales");
                            cuentasMensuales.Add(new XAttribute("numeroCuenta", numeroCuenta.Value));
                            detalle.Add(cuentasMensuales);
                        }
                       
                    }

                    if (personaMoral != null)
                    {
                        foreach (Atributo atributo in getPersonaMoral(personaMoral))
                        {
                            detalle.Add(new XAttribute(atributo.Nombre, atributo.Valor));                          
                        }

                        var cuentasDePersonaMensuales = personaMoral.Descendants("numeroCuenta");

                        foreach (XElement numeroCuenta in cuentasDePersonaMensuales)
                        {
                            XElement cuentasMensuales = new XElement("CuentasMensuales");
                            cuentasMensuales.Add(new XAttribute("numeroCuenta", numeroCuenta.Value));
                            detalle.Add(cuentasMensuales);
                        }
                    }

                    XElement depositoEnEfectivo = registroDetalle.Element("DepositoEnEfectivo");

                    if (depositoEnEfectivo != null)
                    {
                        foreach (Atributo atributo in getDepositoEnEfectivo(depositoEnEfectivo))
                        {
                            detalle.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                        }
                    }

                    XElement chequeDeCaja = registroDetalle.Element("ChequeDeCaja");
                    string efectivoOCheque = "1";
                    if (chequeDeCaja != null)
                    {
                        foreach (Atributo atributo in getChequeDeCaja(chequeDeCaja))
                        {
                            detalle.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                            efectivoOCheque = "2";
                        }
                    }
                    detalle.Add(new XAttribute("efectivoOCheque", efectivoOCheque));

                    var lstCotitulares = registroDetalle.Descendants("Cotitulares");

                    foreach (var cotitulares in lstCotitulares)
                    {
                        var atts = cotitulares.Attributes();
                        string numeroCuenta = string.Empty;
                        string numeroCotitulares = string.Empty;
                        foreach (var att in atts)
                        {
                            switch (att.Name.ToString())
                            {
                                case "numeroCuenta":
                                    numeroCuenta = att.Value;
                                    break;
                                case "numeroCotitulares":
                                    numeroCotitulares = att.Value;
                                    break;
                            }
                        }

                        var cotitular = cotitulares.Descendants("tCotitular");

                        foreach (XElement datCotitular in cotitular)
                        {

                            XElement cotitularElement = new XElement("Cotitular");

                            cotitularElement.Add(new XAttribute("numCuenta", numeroCuenta));
                            cotitularElement.Add(new XAttribute("noCotitulares", numeroCotitulares));

                            foreach (Atributo atributo in getCotitular(datCotitular))
                            {
                                cotitularElement.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                            }

                            detalle.Add(cotitularElement);
                        }
                    }

                    //Se agrega el detalle a la lista de detalles
                    detalles.Add(detalle);
                }
            }
            return detalles;
        }

        private static List<XElement> generarDetallesDIA()
        {
            List<XElement> detalles = new List<XElement>();

            var elementosDIA = from c in declaracion.Descendants("DeclaracionInformativaAnualIDE") select c;


            int consecutivoDetalle = 0;

            foreach (var registroDetalle in elementosDIA.Descendants("RegistroDeDetalle"))
            {
                XElement detalle = null;
                detalle = new XElement("Detalle");

                consecutivoDetalle++;
                detalle.Add(new XAttribute("consecutivoDet", consecutivoDetalle.ToString()));
                detalle.Add(new XAttribute("fechaRegistro", DateTime.Now.ToString()));
                string efectivoOCheque = "1";
                detalle.Add(new XAttribute("efectivoOCheque", efectivoOCheque));



                XElement personaFisica = registroDetalle.Element("PersonaFisica");
                XElement personaMoral = registroDetalle.Element("PersonaMoral");

                if (personaFisica != null)
                {
                    foreach (Atributo atributo in getPersonaFisica(personaFisica))
                    {
                        detalle.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                    }
                }

                if (personaMoral != null)
                {
                    foreach (Atributo atributo in getPersonaMoral(personaMoral))
                    {
                        detalle.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                    }
                }

                XElement depositoEnEfectivo = registroDetalle.Element("DepositoEnEfectivo");

                if (depositoEnEfectivo != null)
                {
                    foreach (Atributo atributo in getDepositoEnEfectivo(depositoEnEfectivo))
                    {
                        detalle.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                    }

                    var cuentas = depositoEnEfectivo.Descendants("Cuenta");

                    foreach (XElement cuenta in cuentas)
                    {
                        XElement cuentaAdd = null;
                        cuentaAdd = new XElement("Cuenta");

                        foreach (Atributo atributo in getCuenta(cuenta))
                        {
                            cuentaAdd.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                        }

                        int consecutivoMovimiento = 0;
                        foreach (XElement movimiento in cuenta.Descendants("Movimiento"))
                        {
                            consecutivoMovimiento++;
                            XElement movimientoAdd = null;
                            movimientoAdd = new XElement("Movimiento");

                            movimientoAdd.Add(new XAttribute("consecutivoMov", consecutivoMovimiento));

                            foreach (Atributo atributo in getMovimiento(movimiento))
                            {
                                movimientoAdd.Add(new XAttribute(atributo.Nombre, atributo.Valor));
                            }

                            cuentaAdd.Add(movimientoAdd);
                        }

                        detalle.Add(cuentaAdd);

                    }
                }

                //Se agrega el detalle a la lista de detalles
                detalles.Add(detalle);

            }


            return detalles;

        }

        private static string getPathDeclaracion(DatosDeclaracion datosDeclaracion)
        {

            string directorioEnvio = ConfigurationManager.ApplicationSettings.ReadSetting(Constantes.SettingNames.DirectorioEnvio);
            string strPath = Path.Combine(directorioEnvio, datosDeclaracion.ArchivoFisico);
            if (File.Exists(strPath))
                return strPath;

            return string.Empty;
        }

        private static string getPathEsquema()
        {

            string pathApp = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string pathEsquema = Path.Combine(Path.Combine(pathApp, "Esquema"), "EsquemaDWH.xml");
            return pathEsquema;
        }

        private static string getFileShare()
        {
            string directorioEnvio = ConfigurationManager.ApplicationSettings.ReadSetting(Constantes.SettingNames.DirectorioEnvio);
            if (Directory.Exists(directorioEnvio))
                return directorioEnvio;

            return string.Empty;
        }

        private static List<Atributo> getChequeDeCaja(XElement chequeDeCaja)
        {
            List<Atributo> attrs = new List<Atributo>();

            string datos = string.Empty;
            var atts = chequeDeCaja.Attributes();

            foreach (var att in atts)
            {
                switch (att.Name.ToString())
                {
                    case "montoCheque":
                        attrs.Add(new Atributo("montoCheque", att.Value));
                        break;
                    case "montoRecaudado":
                        attrs.Add(new Atributo("montoRecaudado", att.Value));
                        break;
                }
            }
            return attrs;
        }

        private static List<Atributo> getDepositoEnEfectivo(XElement depositoEnEfectivo)
        {
            List<Atributo> attrs = new List<Atributo>();

            string datos = string.Empty;
            var atts = depositoEnEfectivo.Attributes();

            foreach (var att in atts)
            {
                switch (att.Name.ToString())
                {
                    case "montoExcedente":
                        attrs.Add(new Atributo("montoExcedenteDepositos", att.Value));
                        break;
                    case "impuestoDeterminado":
                        attrs.Add(new Atributo("impuestoDeterminado", att.Value));
                        break;
                    case "impuestoRecaudado":
                        attrs.Add(new Atributo("impuestoRecaudado", att.Value));
                        break;
                    case "recaudacionPendiente":
                        attrs.Add(new Atributo("impuestoPendienteRecaudar", att.Value));
                        break;
                    case "remanentePeriodosAnteriores":
                        attrs.Add(new Atributo("remanenteRecaudadoPeriodos", att.Value));
                        break;
                    case "saldoPendienteRecaudar":
                        attrs.Add(new Atributo("saldoPendienteRecaudar", att.Value));
                        break;
                }
            }
            return attrs;
        }

        private static List<Atributo> getPersonaFisica(XElement personaFisica)
        {
            List<Atributo> attrs = new List<Atributo>();

            attrs.Add(new Atributo("cveTipoPersona", "F"));

            var atts = personaFisica.Attributes();

            foreach (var att in atts)
            {
                switch (att.Name.ToString())
                {
                    case "rfc":
                        attrs.Add(new Atributo("rfc", att.Value));
                        break;
                    case "curp":
                        attrs.Add(new Atributo("curp", att.Value));
                        break;
                    case "NumeroCliente":
                        attrs.Add(new Atributo("numeroCliente", att.Value));
                        break;
                    case "correoElectronico":
                        attrs.Add(new Atributo("correoElectronico", att.Value));
                        break;
                    case "telefono1":
                        attrs.Add(new Atributo("telefono1", att.Value));
                        break;
                    case "telefono2":
                        attrs.Add(new Atributo("telefono2", att.Value));
                        break;
                }
            }

            var dec = personaFisica.Descendants();

            foreach (var el in dec)
            {
                switch (el.Name.ToString())
                {
                    case "NombreCompleto":
                        attrs.Add(new Atributo("nombreCompleto", el.Value));
                        break;
                    case "Nombres":
                        attrs.Add(new Atributo("nombres", el.Value));
                        break;
                    case "PrimerApellido":
                        attrs.Add(new Atributo("apPat", el.Value));
                        break;
                    case "SegundoApellido":
                        attrs.Add(new Atributo("apMat", el.Value));
                        break;
                    case "DomicilioCompleto":
                        attrs.Add(new Atributo("domicilioCompleto", el.Value));
                        break;
                    case "Calle":
                        attrs.Add(new Atributo("calle", el.Value));
                        break;
                    case "NoExterior":
                        attrs.Add(new Atributo("numExterior", el.Value));
                        break;
                    case "NoInterior":
                        attrs.Add(new Atributo("numInterior", el.Value));
                        break;
                    case "Colonia":
                        attrs.Add(new Atributo("colonia", el.Value));
                        break;
                    case "CodigoPostal":
                        attrs.Add(new Atributo("codigoPostal", el.Value));
                        break;
                }
            }
            return attrs;
        }

        private static List<Atributo> getPersonaMoral(XElement personaMoral)
        {
            List<Atributo> attrs = new List<Atributo>();

            attrs.Add(new Atributo("cveTipoPersona", "M"));

            var atts = personaMoral.Attributes();

            foreach (var att in atts)
            {
                switch (att.Name.ToString())
                {
                    case "rfc":
                        attrs.Add(new Atributo("rfc", att.Value));
                        break;
                    case "NumeroCliente":
                        attrs.Add(new Atributo("numeroCliente", att.Value));
                        break;
                    case "correoElectronico":
                        attrs.Add(new Atributo("correoElectronico", att.Value));
                        break;
                    case "telefono1":
                        attrs.Add(new Atributo("telefono1", att.Value));
                        break;
                    case "telefono2":
                        attrs.Add(new Atributo("telefono2", att.Value));
                        break;
                }

            }

            var dec = personaMoral.Descendants();

            foreach (var el in dec)
            {
                switch (el.Name.ToString())
                {
                    case "Denominacion":
                        attrs.Add(new Atributo("denominacion", el.Value));
                        break;
                    case "DomicilioCompleto":
                        attrs.Add(new Atributo("domicilioCompleto", el.Value));
                        break;
                    case "Calle":
                        attrs.Add(new Atributo("calle", el.Value));
                        break;
                    case "NoExterior":
                        attrs.Add(new Atributo("numExterior", el.Value));
                        break;
                    case "NoInterior":
                        attrs.Add(new Atributo("numInterior", el.Value));
                        break;
                    case "Colonia":
                        attrs.Add(new Atributo("colonia", el.Value));
                        break;
                    case "CodigoPostal":
                        attrs.Add(new Atributo("codigoPostal", el.Value));
                        break;
                }
            }
            return attrs;
        }

        private static List<Atributo> getCotitular(XElement datCotitular)
        {
            List<Atributo> attCotitular = new List<Atributo>();

            var attCot = datCotitular.Attributes();
            foreach (var att in attCot)
            {
                switch (att.Name.ToString())
                {
                    case "RFC":
                        attCotitular.Add(new Atributo("rfc", att.Value));
                        break;
                    case "curp":
                        attCotitular.Add(new Atributo("curp", att.Value));
                        break;
                    case "Proporcion":
                        attCotitular.Add(new Atributo("proporcion", att.Value));
                        break;
                }
            }

            var elemCot = datCotitular.Descendants();

            foreach (var el in elemCot)
            {
                switch (el.Name.ToString())
                {
                    case "NombreCompleto":
                        attCotitular.Add(new Atributo("nombreCompleto", el.Value));
                        break;
                    case "Nombres":
                        attCotitular.Add(new Atributo("nombres", el.Value));
                        break;
                    case "PrimerApellido":
                        attCotitular.Add(new Atributo("apPat", el.Value));
                        break;
                    case "SegundoApellido":
                        attCotitular.Add(new Atributo("apMat", el.Value));
                        break;
                }
            }

            return attCotitular;
        }

        private static List<Atributo> getCuenta(XElement cuenta)
        {
            List<Atributo> attrs = new List<Atributo>();

            var atts = cuenta.Attributes();

            foreach (var att in atts)
            {
                switch (att.Name.ToString())
                {
                    case "numeroCuenta":
                        attrs.Add(new Atributo("numeroCuenta", att.Value));
                        break;
                    case "cotitulares":
                        attrs.Add(new Atributo("cotitulares", att.Value));
                        break;
                    case "proporcion":
                        attrs.Add(new Atributo("proporcion", att.Value));
                        break;
                    case "impuestoRecaudado":
                        attrs.Add(new Atributo("impuestoRecaudado", att.Value));
                        break;
                    case "tipoCuenta":
                        attrs.Add(new Atributo("tipoCuenta", att.Value));
                        break;
                    case "tipoMoneda":
                        attrs.Add(new Atributo("tipoMoneda", att.Value));
                        break;
                }
            }

            return attrs;
        }

        private static List<Atributo> getMovimiento(XElement movimiento)
        {
            List<Atributo> attrs = new List<Atributo>();

            var atts = movimiento.Attributes();

            foreach (var att in atts)
            {
                switch (att.Name.ToString())
                {
                    case "tipoOperacion":
                        attrs.Add(new Atributo("tipoOperacion", att.Value));
                        break;
                    case "fechaOperacion":
                        attrs.Add(new Atributo("fechaOperacion", att.Value));
                        break;
                    case "montoOperacion":
                        attrs.Add(new Atributo("montoOperacion", att.Value));
                        break;
                    case "montoOperacionMonedaNacional":
                        attrs.Add(new Atributo("montoOperacionMonedaNacional", att.Value));
                        break;
                }
            }

            return attrs;
        }

        private static List<XElement> generarEnterosDIM()
        {
            List<XElement> elementos = new List<XElement>();

            var elementosDIM = from c in declaracion.Descendants("DeclaracionInformativaMensualIDE") select c;

            int consecutivoEnteroPropio = 0;
            int consecutivoEnteroOtrasInstituciones = 0;

            foreach (var reporteRecDiaria in elementosDIM.Descendants("ReporteDeRecaudacionYEnteroDiaria"))
            {

                string fechaCorte = reporteRecDiaria.Attribute("fechaDeCorte").Value;

                var enterosPropios = reporteRecDiaria.Descendants("EnteroPropio");


                foreach (XElement enteroPropio in enterosPropios)
                {
                    consecutivoEnteroPropio++;
                    enteroPropio.Add(new XAttribute("consecutivoEntero", consecutivoEnteroPropio.ToString()));

                    elementos.Add(enteroPropio);
                }

                var enterosOtrasInstituciones = reporteRecDiaria.Descendants("EnteroDeOtrasInstituciones");


                foreach (XElement enteroOtrasInstituciones in enterosOtrasInstituciones)
                {
                    consecutivoEnteroOtrasInstituciones++;
                    enteroOtrasInstituciones.Add(new XAttribute("consecutivoEnteroOtrasInstituciones", consecutivoEnteroOtrasInstituciones.ToString()));
                    enteroOtrasInstituciones.Add(new XAttribute("fechaCorteRecaudacion", fechaCorte));

                    elementos.Add(enteroOtrasInstituciones);
                }
            }

            return elementos;

        }

    }
}
