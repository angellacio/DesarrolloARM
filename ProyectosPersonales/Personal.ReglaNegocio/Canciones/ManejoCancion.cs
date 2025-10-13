using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TagClass;
using TagClass.ASF;
using TagClass.ID3.ID3v2F;
using entC = Personal.Entidades.Canciones;
using EntCancion = Personal.Entidades.Canciones;

namespace Personal.ReglaNegocio.Canciones
{
    public class ManejoCancion
    {
        entC.datCatalogos datCat = null;

        public static entC.datCatalogos ConsultaCatalogo()
        {
            entC.datCatalogos result = null;
            string fileAll = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, "datCatalogos.lam"), jsonString = "";
            try
            {
                result = new entC.datCatalogos();
                if (File.Exists(fileAll))
                {
                    jsonString = File.ReadAllText(fileAll);
                    result = JsonConvert.DeserializeObject<entC.datCatalogos>(jsonString);
                }
            }
            catch (Exception ex) { }
            finally { }
            return result;
        }

        public ManejoCancion(entC.datCatalogos Catalogos)
        {
            datCat = Catalogos;
        }

        public List<EntCancion.entCancion> BuscarCanciones(string sRutaOrigen)
        {
            List<EntCancion.entCancion> lstResult = null;
            List<string> lstArchivos = null, lstRutas = null;
            try
            {
                lstResult = new List<EntCancion.entCancion>();
                lstRutas = Directory.GetDirectories(sRutaOrigen).ToList();
                lstArchivos = Directory.GetFiles(sRutaOrigen).Where(s => s.EndsWith(".mp3") || s.EndsWith(".wma")).ToList();

                lstRutas.ForEach(itemR =>
                {
                    BuscarCanciones(itemR).ForEach(itemAR =>
                    {
                        lstResult.Add(itemAR);
                    });
                });

                lstArchivos.ForEach(itemArch =>
                {
                    lstResult.Add(new EntCancion.entCancion(itemArch));
                });

                //lstResult.ForEach(itemC => { lstResult.Add(itemC); });
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
            { }
            return lstResult;
        }
        public EntCancion.entCancion completaInformacion(EntCancion.entCancion itemCancion)
        {
            ITagInfo sMusica_iTag = null;
            ID3Info iTID3 = null;
            string NombreCompleto = "";
            int? nTrackID = null, nAño = null, nGenero = null;
            try
            {
                NombreCompleto = string.Format(@"{0}\{1}", itemCancion.Ruta, itemCancion.NombreArchivo);

                if (itemCancion.Extencion == ".mp3") sMusica_iTag = new ID3Info(NombreCompleto, true);
                else if (itemCancion.Extencion == ".wma") sMusica_iTag = new ASFTagInfo(NombreCompleto, true);

                iTID3 = (ID3Info)sMusica_iTag;

                if (iTID3.ID3v1Info.HaveTag)
                {
                    itemCancion.bEstadoID3v1 = true;

                    if (iTID3.ID3v1Info.TrackNumber != 0) nTrackID = int.Parse(iTID3.ID3v1Info.TrackNumber.ToString().Trim());
                    nAño = int.Parse(iTID3.ID3v1Info.Year.Trim());

                    itemCancion.ID3v1.sMusica_NumPista = nTrackID;
                    itemCancion.ID3v1.sMusica_Nombre = iTID3.ID3v1Info.Title.Trim();
                    itemCancion.ID3v1.sMusica_Artista = iTID3.ID3v1Info.Artist.Trim();
                    itemCancion.ID3v1.sMusica_Album = iTID3.ID3v1Info.Album.Trim();
                    itemCancion.ID3v1.sMusica_Año = nAño;
                    itemCancion.ID3v1.sMusica_IdGenero = nGenero;
                    itemCancion.ID3v1.sMusica_Genero = iTID3.ID3v1Info.Genre.ToString().Trim();
                    itemCancion.ID3v1.sMusica_Comentario = iTID3.ID3v1Info.Comment.Trim();
                }

                if (iTID3.ID3v2Info.HaveTag)
                {
                    itemCancion.bEstadoID3v2 = true;
                    if (iTID3.ID3v2Info.GetTextFrame("TRCK").Trim().Length > 0)
                        nTrackID = int.Parse(iTID3.ID3v2Info.GetTextFrame("TRCK").Trim());

                    if (iTID3.ID3v2Info.Version.Minor < 4 && iTID3.ID3v2Info.GetTextFrame("TYER").Trim().Length > 0)
                        nAño = int.Parse(iTID3.ID3v2Info.GetTextFrame("TYER").Trim());

                    itemCancion.ID3v2.sMusica_NumPista = nTrackID;
                    itemCancion.ID3v2.sMusica_Nombre = iTID3.ID3v2Info.GetTextFrame("TIT2").Trim();
                    itemCancion.ID3v2.sMusica_Artista = iTID3.ID3v2Info.GetTextFrame("TPE1").Trim();
                    itemCancion.ID3v2.sMusica_Album = iTID3.ID3v2Info.GetTextFrame("TALB").Trim();
                    itemCancion.ID3v2.sMusica_Año = nAño;
                    itemCancion.ID3v2.sMusica_IdGenero = nGenero;
                    itemCancion.ID3v2.sMusica_Genero = iTID3.ID3v2Info.GetTextFrame("TCON").Trim();
                    itemCancion.ID3v2.sMusica_Comentario = iTID3.ID3v2Info.GetTextFrame("TENC").Trim();
                }
            }
            catch { }
            finally { }
            return itemCancion;
        }

        public void GuardarDatosCarpeta(string sRutaOrigen, List<entC.entCancion> lstCanciones)
        {
            string fileName = "datEncript.lam", fileAll = "", jsonString = "";

            try
            {
                fileAll = string.Format(@"{0}\{1}", sRutaOrigen, fileName);

                //datFolder.listCanciones.ForEach(itemC =>
                //{
                //    itemC.sMusica_iTag = null;
                //});

                //jsonString = JsonConvert.SerializeObject(datFolder.listCanciones, Formatting.Indented, new MemoryStreamJsonConverter());
                jsonString = JsonConvert.SerializeObject(lstCanciones);

                if (File.Exists(fileAll)) File.Delete(fileAll);

                File.WriteAllText(fileAll, jsonString);
            }
            catch
            { }
            finally { }

        }

        public List<entC.entCancion> CargaDatosCarpeta(string sRutaOrigen)
        {
            string fileName = "datEncript.lam", fileAll = "", jsonString = "";
            //entC.datFolder datOrigen = null;
            List<entC.entCancion> datOrigen = null;
            try
            {
                fileAll = string.Format(@"{0}\{1}", sRutaOrigen, fileName);
                if (File.Exists(fileAll))
                {
                    jsonString = File.ReadAllText(fileAll);
                    //datOrigen = JsonConvert.DeserializeObject<entC.datFolder>(jsonString);
                    datOrigen = JsonConvert.DeserializeObject<List<entC.entCancion>>(jsonString);
                }
            }
            catch { }
            finally { }
            return datOrigen;
        }
    }
}
