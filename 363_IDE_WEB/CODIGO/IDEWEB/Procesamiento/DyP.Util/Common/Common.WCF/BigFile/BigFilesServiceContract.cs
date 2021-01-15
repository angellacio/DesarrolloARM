
//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:IBigFilesService:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO;

namespace SAT.DyP.Util.Service.WCF
{
    [ServiceContract(Name = "BigFilesService", Namespace = "http://www.sat.gob.mx/scade.net/2008/BigFilesService")]
    public interface IBigFilesService
    {
        [OperationContract(Name = "UploadData", Action = "http://www.sat.gob.mx/scade.net/2008/BigFilesService/UploadData", ReplyAction = "http://www.sat.gob.mx/scade.net/2008/BigFilesService/UploadDataResponse")]
        void UploadData(UploadContent data);

        [OperationContract(Action = "http://www.sat.gob.mx/scade.net/2008/BigFilesService/GetTransferInfo")]
        TransferInfo GetTransferInfo(TransferInfo request);

        [OperationContract(Name = "Transfer", IsOneWay = false, Action = "http://www.sat.gob.mx/scade.net/2008/BigFilesService/Transfer")]
        TransferResponse Transfer(TransferRequest request);

        [OperationContract(Name = "UndoTransfer", Action = "http://www.sat.gob.mx/scade.net/2008/BigFilesService/UndoTransfer")]
        void UndoTransfer(string fileName);

    }

    [MessageContract(IsWrapped = true, WrapperNamespace = "http://www.sat.gob.mx/scade.net/2008/UploadContent")]
    public class UploadContent
    {
        [MessageBodyMember(Order = 1)]
        public System.IO.Stream Content;

        [MessageHeader(MustUnderstand = true)]
        public string SendToFile;
    }

    [DataContract(Namespace = "http://www.sat.gob.mx/scade.net/2008/TransferInfo", Name = "TransferInfo")]
    public class TransferInfo
    {
        private string _identifier;
        private string _fileName;
        private TransferStatus _status = TransferStatus.Unkwond;
        private string _message;
        private bool _exists = false;
        private int _totalSize;

        [DataMember(Name = "Id")]
        public string Id
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        [DataMember(Name = "FileName")]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [DataMember(Name = "Status")]
        public TransferStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember(Name = "Message")]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        [DataMember(Name = "Exists")]
        public bool Exists
        {
            get { return _exists; }
            set { _exists = value; }
        }

        [DataMember(Name = "TotalSize")]
        public int TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = value; }
        }
    }

    [DataContract(Namespace = "http://www.sat.gob.mx/scade.net/2008/TransferInfo", Name = "TransferStatus")]
    public enum TransferStatus : int
    {
        /// <summary>
        /// Indica que se desconoce el estado del archivo posiblemente a un error inesperado.
        /// </summary>
        [EnumMember()]
        Unkwond = 0,

        /// <summary>
        /// Indica que el archivo se encuentra en base de datos.
        /// </summary>
        [EnumMember()]
        Uploaded = 1,

        /// <summary>
        /// Indica que el archivo se encuentra en proceso de transferencia.
        /// </summary>
        [EnumMember()]
        InProgress = 2,

        /// <summary>
        /// Indica que el archivo se encuentra en el sistema de archivos.
        /// </summary>
        [EnumMember()]
        Downloaded = 3
    }


    [MessageContract(IsWrapped = true, WrapperNamespace = "http://www.sat.gob.mx/scade.net/2008/TransferRequest")]
    public class TransferRequest
    {
        [MessageBodyMember(Order = 1)]
        public Stream FileContent;

        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public string DatabaseKeyConfig;

        [MessageHeader(MustUnderstand = true)]
        public string StoredProcedureName;

        [MessageHeader(MustUnderstand = true)]
        public string StoredProcedureParameterName;

        [MessageHeader(MustUnderstand = true)]
        public string Message;

    }

    [MessageContract(IsWrapped = true, WrapperNamespace = "http://www.sat.gob.mx/scade.net/2008/TransferResponse")]
    public class TransferResponse
    {
        [MessageHeader(MustUnderstand = true)]
        public TransferStatus Status;
        [MessageHeader(MustUnderstand = true)]
        public string Message;
    }

}
