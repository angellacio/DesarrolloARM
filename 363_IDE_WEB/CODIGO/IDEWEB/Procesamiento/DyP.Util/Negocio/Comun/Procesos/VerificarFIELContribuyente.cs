
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ValidarFielContribuyente:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.Types;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Security;
using SAT.DyP.Negocio.Comun.Procesos.SgiHelper;

namespace SAT.DyP.Negocio.Comun.Procesos
{
  /// <summary>
  /// Componente que permite verificar si un contribuyente cuenta con FIEL
  /// </summary>
  public class VerificarFIELContribuyente
  {
    public bool Execute(string rfc)
    {
      bool result = false;
      //TODO: Invocar WS Sgi Helper
      ISgiHelperProvider Provider = SgiFactory.CreateProvider();
      try { result = Provider.VerificarFIELContribuyente(rfc.ToString()); }
      catch
      { throw; }
      finally { Provider.Dispose(); }
      return result;
    }
  }
}