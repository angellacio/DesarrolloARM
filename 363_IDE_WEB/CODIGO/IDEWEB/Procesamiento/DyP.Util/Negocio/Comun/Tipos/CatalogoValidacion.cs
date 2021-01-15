
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:CatalogoValidacion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.ExceptionHandling;

namespace SAT.DyP.Negocio.Comun.Tipos
{
  /// <summary>
  /// Clase que permite describir los catalogos de versiones de cliente y formulario
  /// que se utilizan en las validaciones.
  /// </summary>
  public class CatalogoValidacion
  {
    private string _versionesCliente = string.Empty;
    private string _versionesFormulario = string.Empty;
    private string _xml = "";

    public string VersionesCliente
    {
      get { return _versionesCliente; }
      set { _versionesCliente = value; }
    }

    public string VersionesFormulario
    {
      get { return _versionesFormulario; }
      set { _versionesFormulario = value; }
    }

 
     
    public CatalogoValidacion(string xml)
    {
 
      _xml = xml;
      LoadFromXml(_xml);
    }

    /// <summary>
    /// Realiza el parseo de una cadena para obtener una lista clave-valor de los catalogos
    /// </summary>
    /// <param name="valores">Cadena con los valores</param>
    /// <returns></returns>
    public static Dictionary<string, string> ParseCatalogo(string valores)
    {
      Dictionary<string, string> resultado = ParseCatalogo(valores, '|');

      return resultado;
    }

    public static Dictionary<string, string> ParseCatalogo(string valores, char separador)
    {
      Dictionary<string, string> resultado = new Dictionary<string, string>();

      if (!string.IsNullOrEmpty(valores))
      {
        string[] items = valores.Split(separador);
        foreach (string item in items)
        {
          if (!string.IsNullOrEmpty(item.Trim()))
          {
            string[] par = item.Split('-');
            resultado.Add(par[0], par[1]);
          }
        }
      }

      return resultado;
    }

    public static IList<string> ParseToList(string valores)
    {
      return ParseToList(valores, '|');
    }

    public static IList<string> ParseToList(string valores, char separador)
    {
      List<string> resultado = new List<string>();
      if (!string.IsNullOrEmpty(valores))
      {
        string[] items = valores.Split(separador);
        foreach (string item in items)
        {
          if (item.Trim().Length > 0)
          {
            string version = item;
            version = version.Replace("-", "','");

            resultado.Add(version);
          }
        }
      }

      return resultado;
    }

    /// <summary>
    /// Crea una nueva instancia en base a los valores de una llave de la tabla
    /// de configuración
    /// </summary>
    /// <param name="keyConfiguration">Clave de configuración</param>
    /// <returns></returns>
    public static CatalogoValidacion CreateFromSettings(string keyConfiguration)
    {
      string settingValue = ConfigurationManager.ApplicationSettings.ReadSetting(keyConfiguration);

      if (String.IsNullOrEmpty(settingValue))
      {
        throw new PlatformException(String.Format("No se encontró el valor del parámetro '{0}' en la tabla de valores de configuración.", keyConfiguration));
      }

      return new CatalogoValidacion(settingValue);
    }


    private void LoadFromXml(string xml)
    {
      TextReader txtReader = new StringReader(xml);
      XmlReader reader = XmlReader.Create(txtReader);
      string nombrePrevio = string.Empty;

      while (reader.Read())
      {
        switch (reader.NodeType)
        {
          case XmlNodeType.Element:
            nombrePrevio = reader.Name;
            break;
          case XmlNodeType.Text:
            if (nombrePrevio.Equals("VersionCliente"))
              _versionesCliente = reader.Value;

            if (nombrePrevio.Equals("VersionFormulario"))
              _versionesFormulario = reader.Value;

            break;
        }

      }

      reader.Close();
      txtReader.Close();
    }
  }
}