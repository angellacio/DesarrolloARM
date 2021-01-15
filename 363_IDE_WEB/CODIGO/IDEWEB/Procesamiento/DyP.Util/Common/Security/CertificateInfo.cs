
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:CertificateInfo:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Util.Security
{
    [Serializable]
    public struct CertificateInfo
    {
        private CertificateType _type;
        private CertificateState _state;
        private string _serialNumber;
        private string _taxPayerID;
        private string _taxPayerName;
        DateTime _validityStart;
        DateTime _validityEnd;

        public CertificateInfo(CertificateType type, CertificateState state, string serialNumber, string taxPayerID, string taxPayerName, DateTime validityStart, DateTime validityEnd)
        {
            this._type          = type;
            this._state         = state;
            this._serialNumber  = serialNumber;
            this._taxPayerID    = taxPayerID;
            this._taxPayerName  = taxPayerName;
            this._validityStart = validityStart;
            this._validityEnd   = validityEnd;
        }

        public CertificateInfo(CertificateInfo certificateInfo)
        {
            this._type          = certificateInfo.Type;
            this._state         = certificateInfo.State;
            this._serialNumber  = certificateInfo.SerialNumber;
            this._taxPayerID    = certificateInfo.TaxPayerID;
            this._taxPayerName  = certificateInfo.TaxPayerName;
            this._validityStart = certificateInfo.ValidityStart;
            this._validityEnd   = certificateInfo.ValidityEnd;
        }

        public CertificateType Type
        {
            get { return this._type;  }
            set { this._type = value; }
        }

        public CertificateState State
        {
            get { return this._state;  }
            set { this._state = value; }
        }

        public string SerialNumber
        {
            get { return this._serialNumber;  }
            set { this._serialNumber = value; }
        }

        public string TaxPayerID
        {
            get { return this._taxPayerID;  }
            set { this._taxPayerID = value; }
        }

        public string TaxPayerName
        {
            get { return this._taxPayerName;  }
            set { this._taxPayerName = value; }
        }

        public DateTime ValidityStart
        {
            get { return this._validityStart;  }
            set { this._validityStart = value; }
        }

        public DateTime ValidityEnd
        {
            get { return this._validityEnd;  }
            set { this._validityEnd = value; }
        }
    }
}
