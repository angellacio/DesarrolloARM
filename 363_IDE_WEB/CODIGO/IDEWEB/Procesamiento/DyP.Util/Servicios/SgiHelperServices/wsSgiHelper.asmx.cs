
//@(#)SCADE2(W:SKDN10211CO5:SAT.DyP.Negocio.Comun.Servicios:wsSgiHelper:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Servicios:1.0:21/Mayo/2008])

using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using SAT.DyP.Util.Security;
using SAT.DyP.Util.Logging;
using SAT.DyP.Negocio.Comun.Procesos;
using SAT.DyP.Util.Caching;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Types;
using SAT.DyP.Negocio.Comun.Tipos;
using SgiHelper = SAT.DyP.Negocio.Comun.Procesos.SgiHelper;

namespace SgiHelperServices
{
    /// <summary>
    /// Summary description for wsSgiHelper
    /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [ToolboxItem(false)]
  public class wsSgiHelper : System.Web.Services.WebService
  {
    [WebMethod(Description = "Valida una firma digital")]
    public bool EsFirmaValida(string NumeroSerie, string CadenaOriginal, string SelloDigital)
    {
      bool _result = false;

      SgiHelper.NetSgiHelper _helper = new SgiHelper.NetSgiHelper();
      try
      {
        _result = _helper.EsFirmaValida(NumeroSerie, CadenaOriginal, SelloDigital);
      }
      catch (Exception ex)
      {
        EventLogHelper.WriteErrorEntry(ex.Message, CoreLogEventIdentifier.SECURITY_EVENT_ID);
      }
      finally
      {
        if (_helper != null)
          _helper.Dispose();
      }

      return _result;
    }

    [WebMethod(Description = "Generación de sello digital")]
    public string GenerarSelloDigital(string CadenaOriginal)
    {
      string CadenaFirmada = string.Empty;
      SgiHelper.NetSgiHelper _helper = new SgiHelper.NetSgiHelper();
      try
      {
        CadenaFirmada = _helper.GenerarSelloDigital(CadenaOriginal);
        //string _hostName = ReadFromSettings(Constantes.SELLO_DIGITAL_HOSTNAME);
        //string _serviceName = ReadFromSettings(Constantes.SELLO_DIGITAL_SERVICENAME);
        //_helper = new SgiDigitalSealHelper(_hostName, _serviceName);
        //cadenaFirmada = _helper.GenerateDigitalSeal(cadenaOriginal);
      }
      catch (Exception ex)
      {
        EventLogHelper.WriteErrorEntry(ex.Message, CoreLogEventIdentifier.SECURITY_EVENT_ID);
      }
      finally
      {
        if (_helper != null)
          _helper.Dispose();
      }

      return CadenaFirmada;
    }

    [WebMethod(Description = "Verificar certificados de la FIEL del contribuyente")]
    public bool VerificarFIELContribuyente(string rfc)
    {
      bool _result = false;
      SgiHelper.NetSgiHelper _helper = new SgiHelper.NetSgiHelper();

      try
      {
        _result = _helper.VerificarFIELContribuyente(rfc);
        //helper = new SgiCryptoHelper(true, null);
        //List<string> serialNumbers = helper.GetCertificateSerialNumbersByTaxPayerID(rfc,
        //    SAT.DyP.Util.Security.CertificateType.ElectronicSignature,
        //    SAT.DyP.Util.Security.CertificateState.Active, 1);
        //if (serialNumbers.Count > 0)
        //{
        //    result = true;
        //}
      }
      catch (BusinessException ex)
      {
        string errorString = String.Format("Error al obtener FIEL de contribuyente en la ARA: {0}", ex);
        EventLogHelper.WriteWarningEntry(errorString, IdentificadorEvento.NEGOCIO_COMUN_EVENT_ID);
        _result = false;
      }
      catch (PlatformException ex)
      {
        EventLogHelper.WriteErrorEntry(ex.Message, CoreLogEventIdentifier.SGIHELPER_EVENT_ID);
        throw new PlatformException(ex.Message);
      }
      catch (Exception ex)
      {
        EventLogHelper.WriteErrorEntry(ex.Message, CoreLogEventIdentifier.SGIHELPER_EVENT_ID);
        throw new PlatformException(ex.Message);
      }
      finally
      {
        if (_helper != null)
          _helper.Dispose();
      }

      return _result;
    }

    [WebMethod(Description = "Obtiene los datos del certificado")]
    public CertificateInfo ObtenerCertificado(string NumeroSerie)
    {
      SgiHelper.NetSgiHelper _helper = new SgiHelper.NetSgiHelper();

      //SgiCryptoHelper certAuthority = null;
      CertificateInfo CertInfo;
      try
      {
          /// Mejoras MS. DAC MAR 2011 
        _helper.GetCertificateBySerialNumber(NumeroSerie);
        CertInfo = _helper.CertInfo;
        /// FIN-Mejoras MS. DAC MAR 2011 
        //certAuthority = new SgiCryptoHelper(true, null);
        //certificateInfo = (CertificateInfo)certAuthority.GetCertificateInfoBySerialNumber(numeroSerie);
      }
      catch (Exception ex)
      {
        throw new SAT.DyP.Util.Types.PlatformException(ex.Message);
      }
      finally
      {
        if (_helper != null)
          _helper.Dispose();
      }

      return CertInfo;
    }

    [WebMethod(Description = "Permite obtener el texto legible de una declaración")]
    public string DesencriptarDeclaracion(string NombreArchivo, string ContenidoDocumento, int MedioPresentacion, string rfcAutenticacion, string rfc)
    {
      DesencriptarHelper _helper = new DesencriptarHelper();
      string ContenidoResultado = string.Empty;
      try
      {
        ContenidoResultado = _helper.DesencriptarDeclaracion(NombreArchivo, ContenidoDocumento, MedioPresentacion, rfcAutenticacion, rfc);
      }
      catch (PlatformException oPEx)
      {
        throw oPEx;
      }
      catch (BusinessException oEx)
      {
        throw oEx;
      }
      catch (Exception ex)
      {
        throw ex;
      }

      return ContenidoResultado;
    }

  }
}