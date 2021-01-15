
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ActualizaMailContribuyente:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Negocio.Comun.Procesos.Datos;
using SAT.DyP.Util.ExceptionHandling;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos
{
  /// <summary>
  /// Clase encargada de la obtención de los datos del Contribuyente
  /// </summary>
  [Serializable]
  public class ActualizaMailContribuyente
  {
    public static bool Execute(string rfc, string email)
    {
      DALContribuyente dal = new DALContribuyente();

      try
      {
        /* FSM @ PPMC 65622 */
        string[] eMails = email.Split(';');
        bool retValue = dal.ActualizaMailContribuyente(rfc, eMails[0]);

        if (eMails.Length > 1)
          retValue = dal.ActualizaMailAdicionalContribuyente(rfc, eMails[1]);
        else
          retValue = dal.EliminaMailAdicionalContribuyente(rfc);

        dal.RegistraLogCambios(rfc, eMails[0], (eMails.Length > 1) ? eMails[1] : string.Empty);

        return retValue;
        /* FSM @ PPMC 65622 */
      }
      catch (PlatformException ex)
      {
        bool rethrow = ExceptionHandler.HandleException(ex);
        if (rethrow)
          throw;

        return false;
      }
      catch (Exception ex)
      {
        bool rethrow = ExceptionHandler.HandleException(ex);
        if (rethrow)
          throw new PlatformException(string.Format("Error al intentar actualizar el Correo Electrónico '{0}' del RFC '{1}'", email, rfc), ex);

        return false;
      }
    }
  }
}