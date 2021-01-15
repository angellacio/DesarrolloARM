using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.Caching;
using SAT.DyP.Util.Security;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Configuration;
using SgiCriptoNet = mx.gob.sat.sgi.SgiCripto;
using SAT.DyP.Negocio.Comun.Procesos.Helper;
using SAT.DyP.Util.Security.Interop.SgiHelpers;

namespace SAT.DyP.Negocio.Comun.Procesos.SgiHelper
{
  public class NetSgiHelper : ISgiHelperProvider
  {
    protected SgiCriptoNet.SgiARA ara;
    protected bool blnConectado = false;
    /// Mejoras MS. DAC MAR 2011 
    private CertificateInfo _privCertInfo;
    /// FIN-Mejoras MS. DAC MAR 2011 

     
    
    public void Dispose()
    {
      if (ara != null)
      {
        try { ara.desconecta(); }
        catch { ;}
        try { ara.Dispose(); }
        catch { ;}
        ara = null;
      }
    }

    protected void ConectaAra()
    {
      try
      {
        if (!blnConectado)
        {
          int ret = 0;
          ara = new SgiCriptoNet.SgiARA("");

          ret = ara.inicia();
          if (ret != 0) throw new PlatformException("Error al iniciar el objeto de la ARA : " + ret, ret);

          ret = ara.conecta();
          if (ret != 0) throw new PlatformException("Error al conectar con ARA : " + ret, ret);

          blnConectado = true;
        }
      }
      catch
      {
        blnConectado = false;
        throw;
      }
    }

    /// <summary>
    /// Extrae el RFC del certificado
    /// </summary>
    /// <param name="CertificateBytes">Certificado</param>
    /// <returns>RFC</returns>
    public string GetCertificateTaxPayerID(byte[] CertificateBytes)
    {
      string RFC = string.Empty;

      SgiCriptoNet.SgiIO ioCert = null;
      SgiCriptoNet.SgiCertificado Cert = new SgiCriptoNet.SgiCertificado();
      try
      {
        int ret = 0;
        int buffLenght = CertificateBytes.Length;

        ioCert = new SgiCriptoNet.SgiIO();
        ret = ioCert.inicia(0, ref CertificateBytes, ref buffLenght);
        if (ret != 0) throw new Exception("Error al invocar el metodo SgiIO.inicia : " + ret);

        ret = Cert.inicia(0, ioCert);
        if (ret != 0) throw new Exception("Error al invocar el metodo SgiCertificado.inicia : " + ret);

        ioCert.Dispose();

        ret = Cert.getTitular(1, ref RFC);
        if (ret != 0) throw new Exception("Error al invocar el metodo SgiCertificado.getTitular : " + ret);
      }
      catch(Exception ex)
      { throw new PlatformException(ex); }
      finally
      {
        if (ioCert != null) ioCert.Dispose();
        if (Cert != null) Cert.Dispose();
      }

      return RFC;
    }

    /// <summary>
    /// Obtiene el certificado del ARA por el No. de Serie
    /// </summary>
    /// <param name="SerialNumber">No. de Serie del Certificado</param>
    /// <param name="CertInfo">Información del certificado</param>
    /// <returns>Certificado</returns>
      [Obsolete("Debe utilizar el método: 'SAT.DyP.Negocio.Comun.Procesos.SgiHelper.GetCertificateBySerialNumber(string SerialNumber)' y recuperar CertificateInfo de la propiedad NetSgiHelper.CertInfo")]
    public byte[] GetCertificateBySerialNumber(string SerialNumber, out CertificateInfo CertInfo)
    {
      byte[] bCert = null;
      string mensajeError = String.Format("Error al consultar el certificado por el numero de serie '{0}'", SerialNumber);
      try
      {
        int ret = 0;

        //Crea y conecta el objeto con ARA.
        ConectaAra();

        int nEstado = 0; int nTipo = 0; int nLCert = 0;
        string sVig_Ini = ""; string sVig_Fin = "";

        //Solicita el Certificado x Numero de Serie
        ret = ara.solCDxNS(SerialNumber, ref nEstado, ref nTipo, ref sVig_Ini, ref sVig_Fin, ref bCert, ref nLCert);
        if (ret != 0) throw new Exception("Error al invocar el metodo SgiAra.solCDxNS: " + ret);

        DateTime dVig_Ini; DateTime dVig_Fin;

        // Parseo de fechas en formato UTC
        string strUTCMask = (sVig_Ini.Length == 13) ? "yyMMddHHmmssK" : "yyyyMMddHHmmssK";
        if (!DateTime.TryParseExact(sVig_Ini, strUTCMask, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dVig_Ini))
          throw new InvalidOperationException("An invalid timestamp string was returned from SgiARA.solCDxNS()");

        strUTCMask = (sVig_Fin.Length == 13) ? "yyMMddHHmmssK" : "yyyyMMddHHmmssK";
        if (!DateTime.TryParseExact(sVig_Fin, strUTCMask, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dVig_Fin))
          throw new InvalidOperationException("An invalid timestamp string was returned from SgiARA.solCDxNS()");

        CertInfo = new CertificateInfo();
        CertInfo.Type = (CertificateType)nTipo;
        CertInfo.State = (CertificateState)nEstado;
        CertInfo.SerialNumber = SerialNumber;
        CertInfo.ValidityStart = dVig_Ini;
        CertInfo.ValidityEnd = dVig_Fin;
      }
      catch (InvalidOperationException ex)
      { throw new BusinessException(ex.Message, ex); }
      catch (Exception ex)
      { throw new PlatformException(mensajeError, ex); }

      return bCert;
    }
    /// <summary>
    /// Obtiene el certificado del ARA por el No. de Serie
    /// </summary>
    /// <param name="SerialNumber">No. de Serie del Certificado</param>
    /// <param name="CertInfo">Información del certificado</param>
    /// <returns>Certificado</returns>
    /// Mejoras MS. DAC MAR 2011 
    public byte[] GetCertificateBySerialNumber(string SerialNumber)
    {
      byte[] bCert = null;
      string mensajeError = String.Format("Error al consultar el certificado por el numero de serie '{0}'", SerialNumber);
      try
      {
        int ret = 0;

        //Crea y conecta el objeto con ARA.
        ConectaAra();

        int nEstado = 0; int nTipo = 0; int nLCert = 0;
        string sVig_Ini = ""; string sVig_Fin = "";

        //Solicita el Certificado x Numero de Serie
        ret = ara.solCDxNS(SerialNumber, ref nEstado, ref nTipo, ref sVig_Ini, ref sVig_Fin, ref bCert, ref nLCert);
        if (ret != 0) throw new Exception("Error al invocar el metodo SgiAra.solCDxNS: " + ret);

        DateTime dVig_Ini; DateTime dVig_Fin;

        // Parseo de fechas en formato UTC
        string strUTCMask = (sVig_Ini.Length == 13) ? "yyMMddHHmmssK" : "yyyyMMddHHmmssK";
        if (!DateTime.TryParseExact(sVig_Ini, strUTCMask, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dVig_Ini))
          throw new InvalidOperationException("An invalid timestamp string was returned from SgiARA.solCDxNS()");

        strUTCMask = (sVig_Fin.Length == 13) ? "yyMMddHHmmssK" : "yyyyMMddHHmmssK";
        if (!DateTime.TryParseExact(sVig_Fin, strUTCMask, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dVig_Fin))
          throw new InvalidOperationException("An invalid timestamp string was returned from SgiARA.solCDxNS()");

        /// Mejoras MS. DAC MAR 2011 
        _privCertInfo = new CertificateInfo();
        _privCertInfo.Type = (CertificateType)nTipo;
        _privCertInfo.State = (CertificateState)nEstado;
        _privCertInfo.SerialNumber = SerialNumber;
        _privCertInfo.ValidityStart = dVig_Ini;
        _privCertInfo.ValidityEnd = dVig_Fin;
        /// FIN-Mejoras MS. DAC MAR 2011 
    }
      catch (InvalidOperationException ex)
      { throw new BusinessException(ex.Message, ex); }
      catch (Exception ex)
      { throw new PlatformException(mensajeError, ex); }

      return bCert;
    }
    /// FIN-Mejoras MS. DAC MAR 2011 

    /// <summary>
    /// Obtiene la Información del certificado del ARA por el No. de Serie
    /// </summary>
    /// <param name="NumeroSerie">No. de Serie del Certificado</param>
    /// <returns>Información del certificado</returns>
    public CertificateInfo ObtenerCertificado(string NumeroSerie)
    {
      CertificateInfo retCertInfo;
      /// Mejoras MS. DAC MAR 2011 
      GetCertificateBySerialNumber(NumeroSerie);
      retCertInfo = _privCertInfo;
      /// FIN-Mejoras MS. DAC MAR 2011 
      return retCertInfo;
    }

    /// <summary>
    /// Procesa una declaración firmada, validando al existencia de almenos un
    /// certificado 'Activo' y de 'FIEL' dentro de los firmantes
    /// </summary>
    /// <param name="Declaracion">Declaracion</param>
    /// <param name="NoSerie">No. de Serie de la declaración</param>
    /// <returns>Verdadero/Falso si la declaración se procesó con exito.</returns>
    public string ProcesaDeclaracion(byte[] Declaracion)
    {
      string NoSerie = string.Empty;
      SgiCriptoNet.SgiIO ioDecla = null;
      SgiCriptoNet.SgiSobre Decla = new SgiCriptoNet.SgiSobre();
      SgiCriptoNet.SgiCadCert CadCert = new SgiCriptoNet.SgiCadCert();
      try
      {
        int ret = 0;

        //Crea y conecta objeto con ARA
        ConectaAra();

        int buffLenght = Declaracion.Length;
        ioDecla = new SgiCriptoNet.SgiIO();
        ret = ioDecla.inicia(0, ref Declaracion, ref buffLenght);
        if (ret != 0) throw new Exception("Error al iniciar el objeto SgiIO : " + ret);

        ret = Decla.inicia(ioDecla);
        if (ret != 0) throw new Exception("Error al iniciar el objeto SgiSobre : " + ret);
        ioDecla.Dispose();

        int TipoDecla, nCerts, nFirmantes, nDestinatarios;
        TipoDecla = nCerts = nFirmantes = nDestinatarios = 0;
        SgiCriptoNet.SgiCertificado[] Certs = null;
        string[] NoSeriesFirmantes = null;
        string[] NoSeriesDestinatarios = null;

        ret = Decla.getDatosSobre(ref TipoDecla,
                                  ref Certs,
                                  ref nCerts,
                                  ref NoSeriesFirmantes,
                                  ref nFirmantes,
                                  ref NoSeriesDestinatarios,
                                  ref nDestinatarios);
        if (ret != 0) throw new Exception("Error al invocar el metodo SgiSobre.getDatosSobre : " + ret);

        ret = CadCert.inicia("");
        if (ret != 0) throw new Exception("Error al iniciar el objeto SgiCardCert : " + ret);

        // Felipe Silva -> RMA 308746 ; Se modifica el barrido de validacion, usando NoSeriesFirmantes en lugar de Certs
        foreach (string _Serie in NoSeriesFirmantes)
        {
          try
          {
            string _NoSerie, _VigIni, _VigFin;
            int _Estado, _TipoCert;
            _NoSerie = _VigIni = _VigFin = null;
            _Estado = _TipoCert = 0;

            _NoSerie = (_Serie.Substring(0, 2) == "0x") ? HexToString(_Serie) : _Serie;

            if (_NoSerie.Length != 20) throw new ArgumentException("No de certificado inválido.");

            ret = ara.solEdoCDxNS(_NoSerie, ref _Estado, ref _TipoCert, ref _VigIni, ref _VigFin);
            if (ret != 0) throw new BusinessException("Error al invocar el metodo SgiARA.solEdoCDxNS : " + ret, ret);

            if (_Estado == (int)CertificateState.Active && _TipoCert == (int)CertificateType.ElectronicSignature)
              NoSerie = _NoSerie;
          }
          catch (Exception ex)
          {
            if (ex is BusinessException)
              EventLogHelper.WriteErrorEntry(ex, IdentificadorEvento.DECLARASAT_COMUN_EVENT_ID);
          }
        }
      }
      catch (Exception ex) { throw new PlatformException(ex); }
      finally
      {
        if (ioDecla != null) ioDecla.Dispose();
        if (Decla != null) Decla.Dispose();
        if (CadCert != null) CadCert.Dispose();
      }

      return NoSerie;
    }

    private string HexToString(string HexData)
    {
      string Data = HexData, sValue, sHex = "";
      while (Data.Length > 0)
      {
        sValue = Data.Substring(0, 2);
        if (sValue != "0x")
        {
          int iValue = Convert.ToInt32(sValue, 16);
          sHex += char.ConvertFromUtf32(iValue);
        }
        Data = Data.Substring(2, Data.Length - 2);
      }
      return sHex;
    }

    private byte[] ShrinkBuffer(byte[] buffIn, int buffLength)
    {
      //int buffLength = 0;
      //foreach (byte bytX in buffIn)
      //{
      //  if (bytX == '\0')
      //    break;
      //  buffLength++;
      //}
      byte[] ret = new byte[buffLength];
      Array.Copy(buffIn, ret, buffLength);
      return ret;
    }

    /// <summary>
    /// Procesa sobre
    /// </summary>
    /// <param name="DecryptionCertificatePath">Ruta del certificado de desencripción</param>
    /// <param name="DecryptionPrivateKeyPath">Ruta de la llave de desencripción</param>
    /// <param name="DecryptionPassword">Contraseña de desencripción</param>
    /// <param name="EncryptedContentBytes">Contenido del sobre</param>
    /// <param name="CertificateSATPath">Ruta del certificado del SAT</param>
    /// <param name="CertType">Tipo de certificado</param>
    /// <returns>Contenido del sobre</returns>
    public byte[] ProcessPkcs7FileInfo(string DecryptionCertificatePath, string DecryptionPrivateKeyPath, string DecryptionPassword, byte[] EncryptedContentBytes)
    {
      byte[] retValue = new byte[EncryptedContentBytes.Length * 3];

      SgiCriptoNet.SgiLlavePriv privk = new SgiCriptoNet.SgiLlavePriv();
      SgiCriptoNet.SgiCertificado cert = new SgiCriptoNet.SgiCertificado();
      SgiCriptoNet.SgiSobre sobre = new SgiCriptoNet.SgiSobre();
      SgiCriptoNet.SgiIO ioIn = null;
      SgiCriptoNet.SgiIO ioOut = null;

      try
      {
        int ret = 0;
        ioIn = new SgiCriptoNet.SgiIO();
        ret = ioIn.inicia(0, DecryptionCertificatePath);

        if (ret == 0)
        {
          ret = cert.inicia(0, ioIn);
          if (ret != 0)
          { throw new Exception("Error al cargar el certificado de desencripción : " + ret); }
        }
        else
        { throw new Exception("Error al cargar el certificado de desencripción : " + ret); }
        ioIn.Dispose();

        ioIn = new SgiCriptoNet.SgiIO();
        ret = ioIn.inicia(0, DecryptionPrivateKeyPath);
        if (ret == 0)
        {
          ret = privk.inicia(ioIn, DecryptionPassword, DecryptionPassword.Length);
          if (ret != 0)
          { throw new Exception("Error al cargar la llave privada de desencripción : " + ret); }
        }
        else
        { throw new Exception("Error al cargar la llave privada de desencripción : " + ret); }
        ioIn.Dispose();

        ret = privk.verificaLlaves(cert);
        if (ret != 0)
        { throw new Exception("La llave privada del destinatario no corresponde con el certificado :" + ret); }

        int buffLength = EncryptedContentBytes.Length;
        ioIn = new SgiCriptoNet.SgiIO();
        ret = ioIn.inicia(0, ref EncryptedContentBytes, ref buffLength);
        if (ret != 0)
        { throw new Exception("Error al iniciar el buffer de datos encriptados : " + ret); }

        ret = sobre.inicia(ioIn);
        if (ret != 0)
        { throw new Exception("Error al cargar el sobre : " + ret); }
        ioIn.Dispose();

        buffLength = retValue.Length;
        ioOut = new SgiCriptoNet.SgiIO();
        ret = ioOut.inicia(1, ref retValue, ref buffLength);
        if (ret != 0)
        { throw new Exception("Error al iniciar el buffer de datos desencriptados : " + ret); }

        ret = sobre.procesa(cert, privk, ref ioOut);
        if (ret != 0)
        { throw new Exception("Error al procesar el sobre : " + ret); }

        ret = ioOut.getbuffer(ref buffLength, ref retValue);
        if (ret != 0)
        { throw new Exception("Error al llamar al metodo SgiIO.getbuffer : " + ret); }

        ioOut.Dispose();
        retValue = ShrinkBuffer(retValue, buffLength);
      }
      catch (Exception ex)
      { throw new PlatformException(ex); }
      finally
      {
        if (privk != null) privk.Dispose();
        if (cert != null) cert.Dispose();
        if (sobre != null) sobre.Dispose();
        if (ioIn != null) ioIn.Dispose();
        if (ioOut != null) ioOut.Dispose();
      }

      return retValue;
    }

    /// <summary>
    /// Verifica que la cadena original y el sello digital sean correspondientes a
    /// un certificado en especifico
    /// </summary>
    /// <param name="NumeroSerie">No. de Serie del certificado</param>
    /// <param name="CadenaOriginal">Cadena Original</param>
    /// <param name="SelloDigital">Sello Digital</param>
    /// <returns>Resultado de la validación</returns>
    public bool EsFirmaValida(string NumeroSerie, string CadenaOriginal, string SelloDigital)
    {
      bool _result = false;
      string mensajeError = String.Format("Error al intentar validar la firma con el numero de serie: '{0}'.", NumeroSerie);

      SgiCriptoNet.SgiIO io = null;
      SgiCriptoNet.SgiFirma firma = new SgiCriptoNet.SgiFirma();
      SgiCriptoNet.SgiIO cadOrigIo = null;

      try
      {
        int ret = 0;

        //se inicia el objeto para las consultas a la ARA
        ConectaAra();

        int nEstado = 0; int nTipo = 0; int nLCert = 0;
        string sVig_Ini = ""; string sVig_Fin = "";
        byte[] bCert = null;

        //Solicita el Certificado x Numero de Serie
        ret = ara.solCDxNS(NumeroSerie, ref nEstado, ref nTipo, ref sVig_Ini, ref sVig_Fin, ref bCert, ref nLCert);
        if (ret != 0) throw new Exception("Error al llamar al metodo SgiARA.solCDxNS : " + ret);

        io = new SgiCriptoNet.SgiIO();
        ret = io.inicia(0, ref bCert, ref nLCert);
        if (ret != 0) throw new Exception("Error al iniciar el objeto SgiIO : " + ret);

        SgiCriptoNet.SgiCertificado cert = new SgiCriptoNet.SgiCertificado();
        ret = cert.inicia(0, io);
        if (ret != 0) throw new Exception("Error al iniciar el objeto SgiCertificado : " + ret);
        io.Dispose();

        byte[] buffer = Encoding.ASCII.GetBytes(CadenaOriginal);
        int buffLength = buffer.Length;

        cadOrigIo = new SgiCriptoNet.SgiIO();
        ret = cadOrigIo.inicia(0, ref buffer, ref buffLength);
        if (ret != 0) throw new Exception("Error al iniciar el objeto SgiIO : " + ret);

        SgiCriptoNet.SgiUtils objUtil = new SgiCriptoNet.SgiUtils();
        byte[] buffSelloOri = Encoding.ASCII.GetBytes(SelloDigital);
        byte[] buffSello = new byte[buffSelloOri.Length * 3];
        int buffSelloLength = buffSello.Length;
        ret = objUtil.B64(false, buffSelloOri, buffSelloOri.Length, ref buffSello, ref buffSelloLength);
        if (ret != 0) throw new Exception("Error al llamar al metodo SgiUtils.B64 : " + ret);

        ret = firma.iniciaVer(cert);
        if (ret != 0) throw new Exception("Error al llamar al metodo SgiFirma.iniciaVer : " + ret);

        ret = firma.verFirma(buffSello, buffSello.Length, cadOrigIo);
        if (ret != 0) throw new BusinessException("Error al llamar al metodo SgiFirma.verFirma : " + ret);

//        cadOrigIo.Dispose();
        _result = true;
      }
      catch (BusinessException ex)
      { throw new PlatformException(mensajeError + "\r\n" + ex.Message, ex); }
      catch (Exception ex)
      { throw new PlatformException(mensajeError + "\r\n" + ex.Message, ex); }
      finally
      {
        if (io != null) io.Dispose();
        if (firma != null) firma.Dispose();
        if (cadOrigIo != null) cadOrigIo.Dispose();
      }

      return _result;
    }

    /// <summary>
    /// Verifica en ARA si el contribuyente tiene FIEL
    /// </summary>
    /// <param name="rfc">RFC del contribuyente</param>
    /// <returns>Resultado de la verificación</returns>
    public bool VerificarFIELContribuyente(string rfc)
    {
      string mensajeError = String.Format("Error al obtener FIEL de contribuyente con el RFC '{0}' en la ARA.", rfc);
      bool result = false;

      try
      {
        int ret = 0;

        //se inicia el objeto para las consultas a la ARA
        ConectaAra();

        string Serials = "";
        //Se solicita la lista de certs FIEL y ACTIVOS del RFC.
        ret = ara.solListaCDxRFC(rfc, (int)CertificateState.Active, (int)CertificateType.ElectronicSignature, 1, ref Serials);
        if (ret != 0) throw new Exception("Error al ejecutar SgiARA.solListaCDxRFC :" + ret);

        if (!string.IsNullOrEmpty(Serials))
        {
          String[] SerialNumbers = Serials.Split('|');

          if (SerialNumbers.Length > 0)
            result = true;
        }
      }
      catch (Exception ex)
      { throw new PlatformException(mensajeError + "\r\n\r\n" + ex.Message, ex); }

      return result;
    }

    /// <summary>
    /// Genera el sello digital
    /// </summary>
    /// <param name="CadenaOriginal">Cadena Original</param>
    /// <returns>Sello digital</returns>
    public string GenerarSelloDigital(string CadenaOriginal)
    {
      string CadenaFirmada = string.Empty;
      SgiDigitalSealHelper _helper = null;
      try
      {
        string _hostName = ReadFromSettings(Constantes.SELLO_DIGITAL_HOSTNAME);
        string _serviceName = ReadFromSettings(Constantes.SELLO_DIGITAL_SERVICENAME);

        _helper = new SgiDigitalSealHelper(_hostName, _serviceName);

        CadenaFirmada = _helper.GenerateDigitalSeal(CadenaOriginal);
      }
      catch (Exception ex)
      { throw new PlatformException("Error al generar el sello digital.", ex); }
      finally
      {
        if (_helper != null)
        { _helper.Dispose(); }
      }

      return CadenaFirmada;
    }

    public string DesencriptarDeclaracion(string NombreArchivo, string ContenidoDocumento, int MedioPresentacion, string rfcAutenticacion, string rfc)
    {
      throw new Exception("Not implemented.");
    }

    /// <summary>
    /// Lee el valor de los Settings
    /// </summary>
    /// <param name="SettingKey">Llave</param>
    /// <returns>Valor</returns>
    private string ReadFromSettings(string SettingKey)
    {
      string _Value = string.Empty;

      if (CachingService.Contains(SettingKey))
      {
        _Value = CachingService.GetItem(SettingKey).ToString();
      }
      else
      {
        _Value = ConfigurationManager.ApplicationSettings.ReadSetting(SettingKey);
        CachingService.AddItem(SettingKey, _Value);
      }

      return _Value;
    }

    /// <summary>
    /// Convierte una cadena a un buffer de bytes con 
    /// una codificación en específico
    /// </summary>
    /// <param name="encoding">Codificación</param>
    /// <param name="strIn">Cadena a codificar</param>
    /// <returns>Buffer codificado</returns>
    private byte[] StrToByteArray(Encoding encoding, string strIn)
    {
      return encoding.GetBytes(strIn);
    }

    /// <summary>
    /// Convierte una cadena a un buffer de bytes en codificacíón UTF8
    /// </summary>
    /// <param name="strIn">Cadena a codificar</param>
    /// <returns>Buffer codificado</returns>
    private byte[] StrToByteArray(string strIn)
    {
      return StrToByteArray(new UTF8Encoding(), strIn);
    }

    /// <summary>
    /// Conviete un buffer de bytes a una cadena con
    /// una codificación en específico
    /// </summary>
    /// <param name="encoding">Codificación</param>
    /// <param name="ArrIn">Buffer a codificar</param>
    /// <returns>Cadena codificada</returns>
    private string ByteArrayToStr(Encoding encoding, byte[] ArrIn)
    {
      return encoding.GetString(ArrIn);
    }

    /// <summary>
    /// Convierte un buffer de bytes a una cadena con codificación UTF8
    /// </summary>
    /// <param name="ArrIn">Buffer a codificar</param>
    /// <returns>Cadena Codificada</returns>
    private string ByteArrayToStr(byte[] ArrIn)
    {
      return ByteArrayToStr(new UTF8Encoding(), ArrIn);
    }
    /// Mejoras MS. DAC MAR 2011 
    #region Propiedades
    public CertificateInfo CertInfo
    {
        get { return _privCertInfo; }
    }
    #endregion
    /// FIN-Mejoras MS. DAC MAR 2011 

  }

}
