
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:Formulario:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SAT.DyP.Negocio.Comun.Tipos
{
  /// <summary>
  /// Clase contenedora de clase FormularioRelacionDetalle
  /// </summary>
  [Serializable]
  public class Formulario
  {
    private Dictionary<int, char> _diccionarioRelacionDetalle = new Dictionary<int, char>();

 

    /// <summary>
    /// Clase estática de clase Formulario
    /// </summary>
    /// <param name="formularioXml">Documento Xml de las claves de todos los formularios</param>
    /// <returns>Clase Formulario lleno de claves por formulario</returns>
    //public static Formulario LoadFromXML(XmlDocument formularioXml )
    //{
    //    //Formulario formulario = new Formulario();
    //    //FormularioRelacionDetalle relacionDetalle = new FormularioRelacionDetalle();

    //    //formulario
    //    // instanciar el metodo formulario
    //    //mandar metodo add
    //    return new Formulario();
    //}
    /// <summary>
    /// Añade al diccionario un elemento de RelaciónDetalle
    /// </summary>
    /// <param name="relacionDetalle"></param>
    public void Add(FormularioRelacionDetalle relacionDetalle)
    {
      _diccionarioRelacionDetalle[relacionDetalle.Clave] = relacionDetalle.TipoDato;
    }
    /// <summary>
    /// Diccionario de Relación-Detalle de las claves de Formulario
    /// </summary>
    public Dictionary<int, char> DiccionarioRelacionDetalle
    {
      get { return _diccionarioRelacionDetalle; }
    }
  }
}