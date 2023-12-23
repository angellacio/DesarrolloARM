using FuncionesExtras.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FuncionesExtras
{
    public class ConsDatCunWeb
    {
        public enum TipoDato { Entero, Fecha, SiNo, Texto }
        public string RutaArchivosPagi { get; set; }
        public string RutaArchivosComp { get; set; }
        public string RutaCrea { get; set; }
        public List<string> LstRutaArchivosPagi { get; set; }
        public List<string> LstRutaArchivosComp { get; set; }
        public List<EntDatosExplotacion> LstComponentesAnalizar { get; set; }
        public List<EntDatosExplotacion> LstComponentesBuscar { get; set; }
        public ConsDatCunWeb()
        {
            RutaArchivosPagi = string.Empty;
            RutaArchivosComp = string.Empty;
            RutaCrea = string.Empty;
            LstRutaArchivosPagi = new List<string>();
            LstRutaArchivosComp = new List<string>();
            LstComponentesAnalizar = new List<EntDatosExplotacion>();
            LstComponentesBuscar = new List<EntDatosExplotacion>();
        }

        public void ConsultaDatosRuta()
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("***************************************");
            Console.WriteLine("");
            RutaArchivosPagi = ObtenDatoPantalla("Especificar ruta donde esta el codigo de las paginas: ", TipoDato.Texto).ToString();
            RutaArchivosComp = ObtenDatoPantalla("Especificar ruta donde esta el codigo de procesamiento: ", TipoDato.Texto).ToString();
            RutaCrea = ObtenDatoPantalla("Especificar ruta donde se crea el archivo: ", TipoDato.Texto).ToString();

            if (string.IsNullOrEmpty(RutaArchivosPagi)) RutaArchivosPagi = @"C:\DatosPersonales\SyeSoftware\Repositorios\GitLab_Sat\dyp_pp_cun-web\CODIGO\Intranet\Presentacion";
            if (string.IsNullOrEmpty(RutaArchivosComp)) RutaArchivosComp = @"C:\DatosPersonales\SyeSoftware\Repositorios\GitLab_Sat\dyp_pp_cun-web\CODIGO\Procesamiento\Fuentes";
            if (string.IsNullOrEmpty(RutaCrea)) RutaCrea = @"C:\DatosPersonales\Personales\Repositorio\GitHub\DesarrolloARM\MonitorRMA\Codigo\WinAplicacion\FuncionesExtras\DatosCunWeb.txt";

            Console.WriteLine($@"");
            Console.WriteLine("***************************************");
            Console.WriteLine("***************************************");
            Console.WriteLine($@"");
            Console.WriteLine($@"Archivos buscados en la siguiente ruta: {RutaArchivosPagi}");
            BuscarArchivosRuta(RutaArchivosPagi, "*.asp");
            BuscarArchivosRuta(RutaArchivosComp, "*.cls");

            Console.WriteLine($@"");
            Console.WriteLine("***************************************");
            Console.WriteLine("***************************************");
            Console.WriteLine($@"");

            ValidaArchivosAPS();

            Console.WriteLine($@"");
            Console.WriteLine("***************************************");
            Console.WriteLine("***************************************");
            Console.WriteLine($@"");

            ValidaArchivosMetodo1();

            Console.WriteLine($@"");
            Console.WriteLine("***************************************");
            Console.WriteLine("***************************************");
            Console.WriteLine($@"");

            CreaArchivoRuta();
        }
        private string ObtenDatoPantalla(string SDatoObtener, TipoDato TDato)
        {
            string sResult = "";
            try
            {
                Console.Write(SDatoObtener);
                string? sDatoConsole = Console.ReadLine();

                sDatoConsole ??= "";
                sResult = sDatoConsole;
                switch (TDato)
                {
                    case TipoDato.Entero:
                        int.Parse(sDatoConsole);
                        break;
                    case TipoDato.Fecha:
                        DateTime.Parse(sDatoConsole);
                        break;
                    case TipoDato.SiNo:
                        Boolean.Parse(sDatoConsole);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                Console.WriteLine("////////////// DATO INCORRECTO ///////////////. Favor de reintentar: ");
                ObtenDatoPantalla(SDatoObtener, TDato);
            }
            return sResult;
        }

        private void BuscarArchivosRuta(string sRutaArchivosLocal, string Extencion)
        {
            List<string> listFolder = Directory.EnumerateDirectories(sRutaArchivosLocal).ToList();
            List<string> listFiles = Directory.EnumerateFiles(sRutaArchivosLocal, Extencion).ToList();

            foreach (string Folder in listFolder)
            {
                BuscarArchivosRuta(Folder, Extencion);
            }

            foreach (string Files in listFiles)
            {
                if (Extencion == "*.asp")
                    LstRutaArchivosPagi.Add(Files.Replace(RutaArchivosPagi, ""));
                else if (Extencion == "*.cls")
                    LstRutaArchivosComp.Add(Files.Replace(RutaArchivosComp, ""));
            }

            //Console.WriteLine($@"      Archivos: {listFiles.Count}. En la ruta {sRutaArchivosLocal.Replace(RutaArchivosPagi, "")}");
        }

        private void CreaArchivoRuta()
        {
            try
            {
                if (File.Exists(RutaCrea)) File.Delete(RutaCrea);
                StreamWriter SW = new(RutaCrea);
                LstComponentesAnalizar.ForEach((itemB) =>
                {
                    SW.WriteLine($"{itemB}");
                });
                SW.Close();
                SW.Dispose();
            }
            catch
            { }
            finally
            { }
        }
        private void ValidaArchivosAPS()
        {
            LstRutaArchivosPagi.ForEach(sArchivo =>
            {
                int nLinea = 0;
                string sArchivoCompleto = $@"{RutaArchivosPagi}{sArchivo}";
                List<EntVariableBusqueda> lstDatosBusqueda = new();

                if (sArchivo == "\\DIM\\CCUDIMDetalleAnexosLIE.asp")
                {
                    foreach (string sDatoArchivo in File.ReadAllLines(sArchivoCompleto))
                    {
                        EntDatArchivo entDatoSeparado;
                        nLinea++;
                        string sDat1 = sDatoArchivo.Replace("\t", " ").Replace("	", " ").Replace("	", " ").Trim();

                        if (sDat1.ToLower().Contains(@$"server.createobject("))
                        {
                            entDatoSeparado = DatosValida(1, nLinea, sArchivoCompleto, sArchivo, sDat1, lstDatosBusqueda);

                            LstComponentesAnalizar.Add(new EntDatosExplotacion()
                            {
                                D10_DatosASP = new EntDatArchivo(entDatoSeparado)
                            });
                            lstDatosBusqueda.Add(new EntVariableBusqueda()
                            {
                                Variable = entDatoSeparado.D10_Variable,
                                Procesado = false
                            });
                        }
                        else if (sDat1.ToLower().Contains(@$".asp") && (!sDat1.ToLower().Contains("ccuerror.asp") && !sDat1.ToLower().Contains("ccuimpresion.asp") && !sDat1.ToLower().Contains(".aspx") && !sDat1.Replace(" ", "").ToLower().Contains("pagina=")))
                        {
                            entDatoSeparado = DatosValida(2, nLinea, sArchivoCompleto, sArchivo, sDat1, lstDatosBusqueda);

                            LstComponentesAnalizar.Add(new EntDatosExplotacion()
                            {
                                D10_DatosASP = new EntDatArchivo(entDatoSeparado),
                                NIdTipoComponente = 2
                            });
                        }
                        else if (lstDatosBusqueda.Count > 0)
                        {
                            DatosValida(0, nLinea, sArchivoCompleto, sArchivo, sDat1, lstDatosBusqueda);
                        }
                    }
                }
            });

            LstComponentesBuscar.ForEach((itemB) =>
            {
                LstComponentesAnalizar.Add(new EntDatosExplotacion()
                {
                    D10_DatosASP = new EntDatArchivo(itemB.D10_DatosASP)
                });
            });
            LstComponentesBuscar.Clear();
        }
        private void ValidaArchivosMetodo1()
        {
            try
            {
                LstComponentesAnalizar.ForEach(itemAnalizis =>
                {
                    if (itemAnalizis.NIdTipoComponente == -2)
                    {
                        LstRutaArchivosComp.FindAll(itemACom =>
                        itemACom.ToLower().Contains(itemAnalizis.D10_DatosASP.D12_ComponenteClase.ToLower()) &&
                        itemACom.ToLower().Contains(itemAnalizis.D10_DatosASP.D12_ComponenteDLL.ToLower())
                        ).ToList().ForEach(itemAEnc =>
                        {
                            Boolean ValidaCod = false;
                            int nLinea = 0;
                            string sArchivoCompleto = $@"{RutaArchivosComp}{itemAEnc}";
                            //FileInfo fileI = new(sArchivoCompleto);
                            List<EntVariableBusqueda> lstDatosBusqueda = new();

                            foreach (string sDatoArchivo in File.ReadAllLines(sArchivoCompleto))
                            {
                                nLinea++;
                                if (sDatoArchivo.ToLower().Contains($"function {itemAnalizis.D10_DatosASP.D13_MetodoFuncion.ToLower()}")) ValidaCod = true;
                                if (ValidaCod && sDatoArchivo.ToLower().Contains($"end function")) ValidaCod = false;
                                if (ValidaCod)
                                {
                                    if (!sDatoArchivo.ToLower().Contains("logerror") && !sDatoArchivo.ToLower().Contains("SharedPropertyGroupManager"))
                                    {
                                        if (sDatoArchivo.ToLower().Contains(" = createobject(") || (sDatoArchivo.ToLower().Contains("= new") && !sDatoArchivo.ToLower().Contains("= new adodb")))
                                        {
                                            EntDatArchivo itemValidado = DatosValida_ASP(1, nLinea, sArchivoCompleto, itemAEnc, sDatoArchivo, lstDatosBusqueda);

                                            if (itemAnalizis.D20_Proxy.D01_RenglonDec == -1)
                                            {
                                                itemAnalizis.D20_Proxy = new EntDatArchivo(itemValidado);
                                            }
                                            else
                                            {
                                                if (LstComponentesBuscar.FindAll(itemCB =>
                                                {
                                                    return itemCB.D20_Proxy.D00_RutaCompleta == itemValidado.D00_RutaCompleta &&
                                                           itemCB.D20_Proxy.D12_ComponenteComp == itemValidado.D12_ComponenteComp;
                                                }).ToList().Count < 0)
                                                {
                                                    LstComponentesBuscar.Add(new EntDatosExplotacion()
                                                    {
                                                        D10_DatosASP = itemAnalizis.D10_DatosASP,
                                                        D20_Proxy = new EntDatArchivo(itemValidado)
                                                    });
                                                }
                                            }
                                            lstDatosBusqueda.Add(new EntVariableBusqueda()
                                            {
                                                Variable = itemValidado.D10_Variable,
                                                Procesado = false
                                            });
                                        }
                                        else if (sDatoArchivo.ToLower().Contains(".commandtext") && sDatoArchivo.Contains(".parameters.append"))
                                        {
                                            if (itemAnalizis.D20_Proxy.D01_MetodoRenglon == -1)
                                            {
                                                itemAnalizis.D20_Proxy.D01_MetodoRenglon = nLinea;
                                                itemAnalizis.D20_Proxy.D00_RutaCompleta = sArchivoCompleto;
                                                itemAnalizis.D20_Proxy.D00_RutaNombre = itemAEnc;
                                                itemAnalizis.D20_Proxy.D02_TextArchivo = sDatoArchivo;
                                            }
                                            else
                                            {
                                                LstComponentesBuscar.Add(new EntDatosExplotacion()
                                                {
                                                    D10_DatosASP = itemAnalizis.D10_DatosASP,
                                                    D20_Proxy = new EntDatArchivo()
                                                    {
                                                        D01_MetodoRenglon = nLinea,
                                                        D00_RutaCompleta = sArchivoCompleto,
                                                        D00_RutaNombre = itemAEnc,
                                                        D02_TextArchivo = sDatoArchivo
                                                    }
                                                });
                                            }
                                            //Console.WriteLine($"**{sDatoArchivo}");
                                        }
                                        else
                                        {
                                            DatosValida_ASP(3, nLinea, sArchivoCompleto, itemAEnc, sDatoArchivo, lstDatosBusqueda);
                                        }
                                    }
                                }
                            };

                        });
                    }
                });

                LstComponentesBuscar.ForEach((itemB) =>
                {
                    LstComponentesAnalizar.Add(new EntDatosExplotacion()
                    {
                        D10_DatosASP = new EntDatArchivo(itemB.D10_DatosASP),
                        D20_Proxy = new EntDatArchivo(itemB.D20_Proxy)
                    });
                });
                LstComponentesBuscar.Clear();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private EntDatArchivo DatosValida(int nOpion, int nLinea, string sArchivoCompleto, string sArchivoNombre, string sDatoOrigen, List<EntVariableBusqueda> lstDatosBusqueda)
        {
            string sDat2 = sDatoOrigen, sDatM = "";
            string[] sDat3;
            EntDatArchivo Result = new()
            {
                D01_RenglonDec = -1,
                D00_RutaCompleta = sArchivoCompleto,
                D00_RutaNombre = sArchivoNombre,
                D02_TextArchivo = sDat2,
                D10_Variable = "",
                D11_Condiciones = "",
                D12_ComponenteComp = "",
                D01_MetodoRenglon = -1,
                D13_MetodoComp = ""
            };

            switch (nOpion)
            {
                case 1: // Igualdad
                    sDat2 = RecortaDatoCadena(sDat2, "SET", "");
                    sDat2 = RecortaDatoCadena(sDat2, "Server.CreateObject(\"", "");
                    sDat2 = RecortaDatoCadena(sDat2, "\")", "");
                    sDat2 = sDat2.Replace(" ", "");
                    sDat3 = sDat2.Split('=');

                    Result.D01_RenglonDec = nLinea;
                    Result.D10_Variable = sDat3[0];
                    Result.D12_ComponenteComp = sDat3[1];
                    break;
                case 2: // ASP relacionados
                    sDat2 = sDat2[..(sDat2.IndexOf(".asp") + 4)];
                    sDat3 = sDat2.Split("'");
                    if (sDat3.Length < 2)
                    {
                        sDat3 = sDatoOrigen.Split('"');
                    }
                    Result.D01_RenglonDec = nLinea;
                    Result.D12_ComponenteComp = sDat3[^1];
                    break;
                default: // Variables referenciados
                    LstComponentesAnalizar.ForEach(itemCAF =>
                    {
                        lstDatosBusqueda.ForEach(itemB =>
                        {
                            if (sDat2.IndexOf($"{itemB.Variable}.") > -1 && !itemB.Procesado)
                            {
                                if (itemCAF.NIdTipoComponente == -2)
                                {
                                    sDatM = sDat2[(sDat2.IndexOf($"{itemB.Variable}.") + itemB.Variable.Length + 1)..];
                                    if (itemCAF.D10_DatosASP.D00_RutaCompleta == sArchivoCompleto && itemCAF.D10_DatosASP.D10_Variable == itemB.Variable && itemCAF.D10_DatosASP.D01_MetodoRenglon == -1)
                                    {
                                        itemCAF.D10_DatosASP.D01_MetodoRenglon = nLinea;
                                        itemCAF.D10_DatosASP.D13_MetodoComp = sDatM;
                                        itemB.Procesado = true;
                                    }
                                    else if (itemCAF.D10_DatosASP.D00_RutaCompleta == sArchivoCompleto && itemCAF.D10_DatosASP.D10_Variable == itemB.Variable && itemCAF.D10_DatosASP.D01_MetodoRenglon != -1)
                                    {
                                        if (LstComponentesBuscar.FindAll(itemCI =>
                                        {
                                            return itemCAF.D10_DatosASP.D00_RutaCompleta == sArchivoCompleto &&
                                                   itemCAF.D10_DatosASP.D10_Variable == itemB.Variable &&
                                                   itemCAF.D10_DatosASP.D01_MetodoRenglon == nLinea;
                                        }).Count == 0)
                                        {
                                            LstComponentesBuscar.Add(new EntDatosExplotacion()
                                            {
                                                D10_DatosASP = new EntDatArchivo()
                                                {
                                                    D01_RenglonDec = itemCAF.D10_DatosASP.D01_RenglonDec,
                                                    D00_RutaCompleta = sArchivoCompleto,
                                                    D00_RutaNombre = sArchivoNombre,
                                                    D02_TextArchivo = sDat2,
                                                    D10_Variable = itemB.Variable,
                                                    D11_Condiciones = itemCAF.D10_DatosASP.D11_Condiciones,
                                                    D12_ComponenteComp = itemCAF.D10_DatosASP.D12_ComponenteComp,
                                                    D01_MetodoRenglon = nLinea,
                                                    D13_MetodoComp = sDatM
                                                },
                                                NIdTipoComponente = -2
                                            });
                                            //itemB.Procesado = true;
                                        }
                                    }
                                }
                            }
                        });
                    });

                    break;
            }

            return Result;
        }
        private EntDatArchivo DatosValida_ASP(int nOpion, int nLinea, string sArchivoCompleto, string sArchivoNombre, string sDatoOrigen, List<EntVariableBusqueda> lstDatosBusqueda)
        {
            string sDat2 = sDatoOrigen.Replace("\t", "").Trim(), sDatM = "";
            string[] sDat3;
            EntDatArchivo Result = new()
            {
                D01_RenglonDec = -1,
                D00_RutaCompleta = sArchivoCompleto,
                D00_RutaNombre = sArchivoNombre,
                D02_TextArchivo = sDat2,
                D10_Variable = "",
                D11_Condiciones = "",
                D12_ComponenteComp = "",
                D01_MetodoRenglon = -1,
                D13_MetodoComp = ""
            };

            switch (nOpion)
            {
                case 1: // Igualdad
                    sDat2 = RecortaDatoCadena(sDat2, "SET", "");
                    sDat2 = RecortaDatoCadena(sDat2, "Server.CreateObject(\"", "");
                    sDat2 = RecortaDatoCadena(sDat2, "\")", "");
                    sDat2 = RecortaDatoCadena(sDat2, "createobject(\"", "").Trim();
                    sDat2 = RecortaDatoCadena(sDat2, ")", "");
                    sDat2 = RecortaDatoCadena(sDat2, "new", "").Trim();

                    sDat2 = sDat2.Replace(" ", "");

                    sDat3 = sDat2.Split('=');

                    Result.D01_RenglonDec = nLinea;
                    Result.D10_Variable = sDat3[0];
                    Result.D12_ComponenteComp = sDat3[1];
                    break;
                case 2: // ASP relacionados
                    sDat2 = sDat2[..(sDat2.IndexOf(".asp") + 4)];
                    sDat3 = sDat2.Split("'");
                    if (sDat3.Length < 2)
                    {
                        sDat3 = sDatoOrigen.Split('"');
                    }
                    Result.D01_RenglonDec = nLinea;
                    Result.D12_ComponenteComp = sDat3[^1];
                    break;
                default: // Variables referenciados
                    lstDatosBusqueda.ForEach(itemB =>
                    {

                        LstComponentesAnalizar.ForEach(itemCAF =>
                        {
                            if (sDat2.IndexOf($"{itemB.Variable}.") > -1 && !itemB.Procesado)
                            {
                                sDatM = sDat2[(sDat2.IndexOf($"{itemB.Variable}.") + itemB.Variable.Length + 1)..];

                                if (itemCAF.D20_Proxy.D00_RutaCompleta == sArchivoCompleto && itemCAF.D20_Proxy.D10_Variable == itemB.Variable && itemCAF.D20_Proxy.D13_MetodoComp == "")
                                {
                                    itemCAF.D20_Proxy.D01_MetodoRenglon = nLinea;
                                    itemCAF.D20_Proxy.D13_MetodoComp = sDatM;
                                    itemB.Procesado = false;
                                }
                                else if (itemCAF.D20_Proxy.D00_RutaCompleta == sArchivoCompleto && itemCAF.D20_Proxy.D10_Variable == itemB.Variable && itemCAF.D20_Proxy.D13_MetodoComp != "")
                                {
                                    if (LstComponentesBuscar.FindAll(itemCI =>
                                    {
                                        return itemCAF.D20_Proxy.D00_RutaCompleta == sArchivoCompleto &&
                                               itemCAF.D20_Proxy.D10_Variable == itemB.Variable &&
                                               itemCAF.D20_Proxy.D01_MetodoRenglon == nLinea;
                                    }).Count == 0)
                                    {
                                        LstComponentesBuscar.Add(new EntDatosExplotacion()
                                        {
                                            D10_DatosASP = itemCAF.D10_DatosASP,
                                            D20_Proxy = new EntDatArchivo()
                                            {
                                                D01_RenglonDec = itemCAF.D20_Proxy.D01_RenglonDec,
                                                D00_RutaCompleta = sArchivoCompleto,
                                                D00_RutaNombre = sArchivoNombre,
                                                D02_TextArchivo = sDat2,
                                                D10_Variable = itemB.Variable,
                                                D11_Condiciones = itemCAF.D20_Proxy.D11_Condiciones,
                                                D12_ComponenteComp = itemCAF.D20_Proxy.D12_ComponenteComp,
                                                D01_MetodoRenglon = nLinea,
                                                D13_MetodoComp = sDatM
                                            },
                                            NIdTipoComponente = -2
                                        });
                                        itemB.Procesado = false;
                                    }
                                }
                            }
                        });
                    });
                    break;
            }

            return Result;
        }

        private static string RecortaDatoCadena(string sCadena, string sDatoOri, string sDatoNuevo)
        {
            string sResult = sCadena, sCadenaMini = sCadena.ToLower();
            int nNumCarDatOri = sDatoOri.Length;
            try
            {
                sDatoOri = sDatoOri.ToLower();
                int nBusquedaPI = sCadenaMini.IndexOf(sDatoOri);
                if (nBusquedaPI == 0)
                {
                    sResult = sCadena.Substring(nBusquedaPI + nNumCarDatOri, sCadena.Length - nNumCarDatOri);
                    sResult = $"{sDatoNuevo}{sResult}";
                }
                else if (nBusquedaPI > 0)
                {
                    sResult = sCadena[..nBusquedaPI];
                    sResult = $"{sResult}{sDatoNuevo}";
                    sResult = $"{sResult}{sCadena[(nBusquedaPI + nNumCarDatOri)..]}";
                }
            }
            catch
            {

            }
            return sResult;
        }



    }
}
