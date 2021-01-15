
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:FirmaDigital:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	

using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Security;
using SAT.DyP.Negocio.Comun.Procesos.SgiHelper;

namespace SAT.DyP.Negocio.Comun.Procesos.Seguridad
{
  /// <summary>
  /// Componente que contiene las operaciones que aplican a la firma digital
  /// </summary>
  public class FirmaDigital
  {
    /// <summary>
    /// Valida una firma con base a los datos de la declaración
    /// </summary>
    /// <param name="rfc">RFC Contribuyente</param>
    /// <param name="numeroSerie">Número de Serie</param>
    /// <param name="cadenaOriginal">Cadena original</param>
    /// <param name="selloDigital">Sello Digital</param>
    /// <returns></returns>
    public bool EsFirmaValida(string rfc, string numeroSerie, string cadenaOriginal, string selloDigital)
    {
      bool _result = false;

      //TODO: INVOCAR WS de SgiHelper
      //SgiManager.Provider = SgiFactory.CreateProvider();
      ISgiHelperProvider Provider = SgiFactory.CreateProvider();
      try
      {
        _result = Provider.EsFirmaValida(numeroSerie, cadenaOriginal, selloDigital);
      }
      finally
      {
        Provider.Dispose();
      }

      return _result;
    }


  }
}