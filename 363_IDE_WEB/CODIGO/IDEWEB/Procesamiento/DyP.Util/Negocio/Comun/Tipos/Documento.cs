
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:Documento:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase abstracta que representa un documento de declaración
    /// </summary>
    [Serializable]
    public class Documento
    {
        public Documento(DocumentRequest request)
        {
            this._rfc = request.RFC;
            this._fechaPresentacion = request.ReceivedDate;
            this._rfcAutenticacion = request.PostedByRFC;
            this.Contenido = request.DocumentData;
            this._nombreArchivo = request.FileName;
            this._medioPresentacion = request.ReceiveChanel;
            this._IPAddress = request.ClientIPAddress;
            this.TipoDePersona = Convert.ToInt32(request.TaxableEntityType);
            this._Materia = request.TaxStatementType;
            this._entidadReceptora = request.BranchId;
            this._NumeroSerie = request.CertificateSerialNumber;
            this._rutaArchivoRecibido = request.DocumentPath;
            this._fileSize= request.FileSize;
            this._idMensaje = request.MessageID;
        }

        public Documento()
        { }

        private string _rfc;
        public string Rfc
        {
            get { return _rfc; }
            set { _rfc = value; }
        }

        private string _rfcAutenticacion;
        public string RfcAutenticacion
        {
            get { return _rfcAutenticacion; }
            set { _rfcAutenticacion = value; }
        }

        private string _contenido;
        public string Contenido
        {
            get { return _contenido; }
            set { _contenido = value; }
        }
        private string _nombreArchivo;
        public string NombreArchivo
        {
            get { return _nombreArchivo; }
            set { _nombreArchivo = value; }
        }

        private int _medioPresentacion;
        public int MedioPresentacion
        {
            get { return _medioPresentacion; }
            set { _medioPresentacion = value; }
        }

        private int _entidadReceptora;
        public int EntidadReceptora
        {
            get { return _entidadReceptora; }
            set { _entidadReceptora = value; }
        }
        private DateTime _fechaPresentacion;
        public DateTime FechaPresentacion
        {
            get { return _fechaPresentacion; }
            set { _fechaPresentacion = value; }
        }

        private long _idProceso;
        public long IdProceso
        {
            get { return _idProceso; }
            set { _idProceso = value; }
        }

        private string _IPAddress;
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        private string _NumeroOperacion;
        public string NumeroOperacion
        {
            get { return _NumeroOperacion; }
            set { _NumeroOperacion = value; }
        }

        private string _NumeroSerie;
        public string NumeroSerie
        {
            get { return _NumeroSerie; }
            set { _NumeroSerie = value; }
        }

        //private string _CadenaOriginal;
        //public string CadenaOriginal
        //{
        //    get { return _CadenaOriginal; }
        //    set { _CadenaOriginal = value; }
        //}

        private string _SelloDigital;
        public string SelloDigital
        {
            get { return _SelloDigital; }
            set { _SelloDigital = value; }
        }

        private int _estatus;
        public int Estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
        }

        private DateTime _FechaUltimaActualizacion;
        public DateTime FechaUltimaActualizacion
        {
            get { return _FechaUltimaActualizacion; }
            set { _FechaUltimaActualizacion = value; }
        }

        private int _FirmadoConFea;
        public int FirmadoConFea
        {
            get { return _FirmadoConFea; }
            set { _FirmadoConFea = value; }
        }
        private TipoDePersona _TipoDePersona;
        
        public int TipoDePersona
        {
            get { return Convert.ToInt32(_TipoDePersona); }
            set { _TipoDePersona = (TipoDePersona) Enum.ToObject(typeof(TipoDePersona),value); }
        }
        public TipoDePersona TipoDePersonaEnum
        {
            get{return this._TipoDePersona;}
            set{this._TipoDePersona = value;}
        }    

        private int _Materia;
        public int Materia
        {
            get { return this._Materia; }
            set { this._Materia = value; }
        }

        private string _rutaArchivoRecibido;

        public string RutaArchivoRecibido
        {
            get { return _rutaArchivoRecibido; }
            set { _rutaArchivoRecibido = value; }
        }

        private int _fileSize;

        public int FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        private string _idMensaje;
        public string IdMensaje
        {
            get { return _idMensaje; }
            set { _idMensaje = value; }
        }

    }
}
