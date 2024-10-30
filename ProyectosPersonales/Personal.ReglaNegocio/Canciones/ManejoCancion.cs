using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EntCancion = Personal.Entidades.Canciones;
using System.Runtime.Remoting.MetadataServices;
using System.Web.UI.MobileControls;
using TagClass;
using TagClass.ID3.ID3v2F;
using TagClass.ASF;

namespace Personal.ReglaNegocio.Canciones
{
    public class ManejoCancion
    {
        public List<string> BuscarCanciones(string sRutaOrigen)
        {
            List<string> lstResult = null, lstRutas = null;
            try
            {
                lstRutas = Directory.GetDirectories(sRutaOrigen).ToList();
                lstResult = Directory.GetFiles(sRutaOrigen).Where(s => s.EndsWith(".mp3") || s.EndsWith(".wma")).ToList();

                lstRutas.ForEach(itemR =>
                {
                    List<string> lstArchSC = BuscarCanciones(itemR);

                    lstArchSC.ForEach(itemAR =>
                    {
                        lstResult.Add(itemAR);
                    });
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
            string sAño = "";
            try
            {
                if (itemCancion.Extencion == ".mp3")
                {
                    itemCancion.sMusica_iTag = new ID3Info(itemCancion.NombreCompleto, true);
                }
                else if (itemCancion.Extencion == ".wma")
                {
                    itemCancion.sMusica_iTag = new ASFTagInfo(itemCancion.NombreCompleto, true);
                }

                itemCancion.sMusica_NumPista = int.Parse(ObtenValores("TRCK", itemCancion.sMusica_iTag).ToString().Trim());
                itemCancion.sMusica_Nombre = ObtenValores("TIT2", itemCancion.sMusica_iTag).ToString().Trim();
                itemCancion.sMusica_Artista = ObtenValores("TPE1", itemCancion.sMusica_iTag).ToString().Trim();
                itemCancion.sMusica_Album = ObtenValores("TALB", itemCancion.sMusica_iTag).ToString().Trim();
                itemCancion.sMusica_IdGenero = int.Parse(ObtenValores("TCON", itemCancion.sMusica_iTag).ToString().Trim());
                itemCancion.sMusica_Genero = ObtenValores("TCON", itemCancion.sMusica_iTag).ToString().Trim();
                itemCancion.sMusica_Comentario = ObtenValores("TENC", itemCancion.sMusica_iTag).ToString().Trim();

                sAño = ObtenValores("TYER", itemCancion.sMusica_iTag).ToString().Trim();
                if (sAño != "") itemCancion.sMusica_Año = int.Parse(sAño);
            }
            catch { }
            finally { }
            return itemCancion;
        }
        string ObtenValores(string sKey, ITagInfo iTag)
        {
            ID3Info iTID3 = (ID3Info) iTag;
            string sResult = "";

            if (iTID3.GetType() == typeof(ID3Info))
            {
                if (!iTID3.ID3v1Info.HaveTag) return "";
                switch (sKey)
                {
                    case "TRCK":
                        sResult = iTID3.ID3v1Info.TrackNumber.ToString().Trim();
                        break;
                    case "TIT2":
                        sResult = iTID3.ID3v1Info.Title.ToString().Trim();
                        break;
                    case "TPE1":
                        sResult = iTID3.ID3v1Info.Artist.ToString().Trim();
                        break;
                    case "TALB":
                        sResult = iTID3.ID3v1Info.Album.ToString().Trim();
                        break;
                    case "TCON":
                        sResult = iTID3.ID3v1Info.Genre.ToString().Trim();
                        break;
                    case "TPOS":
                        sResult = iTID3.ID3v1Info.ToString().Trim();
                        break;
                    case "TENC":
                        sResult = iTID3.ID3v1Info.Comment.ToString().Trim();
                        break;
                    case "TYER":
                        sResult = iTID3.ID3v1Info.Year.ToString().Trim();
                        break;
                }
            }
            else
            {
                if (!iTID3.ID3v2Info.HaveTag) return "";
                sResult = iTID3.ID3v2Info.GetTextFrame(sKey).ToString().Trim();
            }

            return sResult;
        }
        //public List<Cancion.entCancion> BuscaCanciones(string sRutaSCanea)
        //{
        //    List<Cancion.entCancion> result = null;
        //    try
        //    {
        //        result = new List<Cancion.entCancion>();

        //        result = ConsultaMultimedios(sRutaSCanea);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    { }
        //    return result;
        //}

        //private List<Cancion.entCancion> ConsultaMultimedios(string sRutaO)
        //{
        //    List<Cancion.entCancion> result = null;
        //    List<string> lstRutas = null;
        //    List<string> lstArchivos = null;
        //    Cancion.entCancion itemCancionValida = null;
        //    try
        //    {
        //        result = new List<Cancion.entCancion>();
        //        lstRutas = Directory.GetDirectories(sRutaO).ToList();
        //        lstArchivos = Directory.GetFiles(sRutaO).ToList();

        //        lstRutas.ForEach(itemR =>
        //        {
        //            ConsultaMultimedios(itemR).ForEach(itemAR =>
        //            {
        //                result.Add(itemAR);
        //            });
        //        });

        //        lstArchivos.ForEach(itemC =>
        //        {
        //            itemCancionValida = ValidaArchivoMusica(itemC);

        //            if (itemCancionValida != null) result.Add(itemCancionValida);
        //        });
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    { }
        //    return result;
        //}

        //private Cancion.entCancion ValidaArchivoMusica(string sArchivo)
        //{
        //    Cancion.entCancion result = null;
        //    FileInfo fiMusica = null;
        //    try
        //    {
        //        fiMusica = new FileInfo(sArchivo);

        //        if (!(fiMusica.Exists)) throw new ApplicationException(string.Format("No se encntro el Archivo '{0}'", sArchivo));
        //        if (fiMusica.Extension.Trim().ToLower() == ".mp3" || fiMusica.Extension.Trim().ToLower() == ".wma")
        //            result = new Cancion.entCancion(fiMusica);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        fiMusica = null;
        //    }
        //    return result;
        //}
    }
}
