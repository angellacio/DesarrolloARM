
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Processors.Communication:SecuritySocketGSI:0:21/May/2008[ SAT.DyP.Util.Processors:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace SAT.DyP.Util.Processors.Communication
{

  public enum SocketGSIResult
  {
    Success = 0,
    OutOfMemory = -1,
    NotOpenSecurityClientMatrix = -2,
    NotLocateSecurityClientMatrix = -3,
    InvalidClientMatrix = -4,
    FailtConnectRemoteHost = -7,
    FailtSocketPort = -8,
    FailGetPortAddress = -9,
    FailGetRemoteHost = -10,
    FailOpenTransferService = -20,
    FailtSocketServer = -21,
    FailTransfer = -22,
    IncompleteTransfer = -23,
    MissionBytesTransfer = -24,
    BadTransfer = -25,
    NotExisteLocalFile = -26,
    NotOpenLocalFile = -27,
    FailSendReceiptFile = -30,
    FailReceiptFile = -31,
    NotCreateLocalFile = -32,
    IncompleteReceipt = -33,
    MissingFileSize = -34,
    FailSendExecutionCommand = -40,
    FailExecutionCommand = -41,
    FailServer = -42,
    InvalidUser = -50,
    DisabledUser = -51,
    MissingGrantTransfer = -52,
    MissingGrantReceipt = -53
  }


  /// <summary>
  /// Clase wrapper para implementar la API de comunicaciones
  /// con el socket de seguridad
  /// </summary>
  public class SecuritySocketGSI
  {
    private string _remoteHost;
    private string _remoteLogin;
    //Mejoras MS. DAC MAR 2011 
    private string _message;
    //FIN-Mejoras MS. DAC MAR 2011 

    [DllImport(@"SockGsi.dll", EntryPoint = @"_TransmiteArchivo@20", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    private static extern int TransmiteArchivo(string remoteHost, string remoteLogin, string localFilePath, string remoteFilePath, [MarshalAs(UnmanagedType.VBByRefStr)] ref string message);

    public SecuritySocketGSI(string remoteHost, string remoteLogin)
    {
 
      _remoteHost = remoteHost;
      _remoteLogin = remoteLogin;
    }

    public string RemoteHost
    {
      get { return _remoteHost; }
      set { _remoteHost = value; }
    }

    public string RemoteLogin
    {
      get { return _remoteLogin; }
      set { _remoteLogin = value; }
    }

    //Mejoras MS. DAC MAR 2011 
    public string Message
    {
      get { return _message; }
    }
    //FIN-Mejoras MS. DAC MAR 2011 

    /// <summary>
    /// Realiza el envió de un archivo al DUCTO de procesamiento
    /// </summary>
    /// <param name="localPath">Ubicación local del archivo</param>
    /// <param name="remotePath">Ubicación remota de destino</param>
    /// <param name="message">Mensaje de retorno</param>
    /// <returns></returns>
    [Obsolete("Debe utilizar el método: SAT.DyP.Util.Processors.Communication.SecuritySocketGSI.SendFile(string, string) y recuperar el mensaje de error de la propiedad SecuritySocketGSI.Message")]
    public bool SendFile(string localPath, string remotePath, ref string message)
    {
      bool _result = false;

      if (string.IsNullOrEmpty(_remoteHost) || string.IsNullOrEmpty(_remoteLogin))
        throw new ArgumentException("Missing Remote Host and/or Remote Login");

      try
      {
        message = new string(' ', 512);
        //se transmite el archivo al DUCTO
        int _retVal = TransmiteArchivo(_remoteHost, _remoteLogin, localPath, remotePath, ref message);

        _result = _retVal == 0 ? true : false;

        //se asigna el mensaje de respuesta
        message = string.Format("{0}, SocketGsi return:{1}", message.Trim(), _retVal);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message, ex);
      }

      return _result;
    }

    /// <summary>
    /// Realiza el envío de un archivo al DUCTO de procesamiento
    /// </summary>
    /// <param name="localPath">Ubicación local del archivo</param>
    /// <param name="remotePath">Ubicación remota de destino</param>
    /// <param name="message">Mensaje de retorno</param>
    /// <returns>Devuelve 'True' si el archivo fué enviado a Ducto, de lo contrario 'False'.</returns>
    //Mejoras MS. DAC MAR 2011 
    public bool SendFile(string localPath, string remotePath)
    {
      bool _result = false;

      if (string.IsNullOrEmpty(_remoteHost) || string.IsNullOrEmpty(_remoteLogin))
      {
        throw new ArgumentException("Missing Remote Host and/or Remote Login");
      }

      _message = new string(' ', 512);
      //se transmite el archivo al DUCTO
      int _retVal = TransmiteArchivo(_remoteHost, _remoteLogin, localPath, remotePath, ref _message);

      _result = _retVal == 0 ? true : false;

      //Mejoras MS. DAC MAR 2011 
      //se asigna el mensaje de respuesta
      _message = string.Format("{0}, SocketGsi return:{1}", _message.Trim(), _retVal);
      //FIN-Mejoras MS. DAC MAR 2011 


      return _result;
    }
  }
}