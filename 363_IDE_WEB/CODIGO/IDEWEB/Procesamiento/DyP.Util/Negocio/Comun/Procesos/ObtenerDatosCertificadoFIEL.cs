
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ObtenerDatosCertificadoFiel:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Security;
using SAT.DyP.Negocio.Comun.Procesos.SgiHelper;

namespace SAT.DyP.Negocio.Comun.Procesos
{
  public class ObtenerDatosCertificadoFIEL
  {
    public CertificateInfo Execute(string numeroSerie)
    {
      CertificateInfo certificateInfo = new CertificateInfo();

      //TODO: Invocar WS SgiHelper
      ISgiHelperProvider Provider = SgiFactory.CreateProvider();
      try { certificateInfo = Provider.ObtenerCertificado(numeroSerie); }
      catch (Exception ex)
      { throw new SAT.DyP.Util.Types.PlatformException(ex.Message, ex); }
      finally
      { Provider.Dispose(); }

      //SgiManager.Provider = SgiFactory.CreateProvider();
      //try
      //{
      //  certificateInfo = SgiManager.Provider.ObtenerCertificado(numeroSerie);
      //}
      //catch (Exception ex)
      //{
      //  throw new SAT.DyP.Util.Types.PlatformException(ex.Message);
      //}
      //finally
      //{
      //  SgiManager.Provider.Dispose();
      //}

      return certificateInfo;
    }
  }
}