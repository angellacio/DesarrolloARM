using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SAT.DyP.Presentacion.Seguridad.Autenticacion.Componentes
{
    [Serializable]
    public class FIELCertificateInfo
    {
        public FIELCertificateInfo()
        {

        }

        public FIELCertificateInfo(string serialNumber, DateTime expirationDate)
        {
            this.serialNumber = serialNumber;
            this.expirationDate = expirationDate;
        }

        public FIELCertificateInfo(string serialNumber, DateTime expirationDate, bool isActive,bool isFielCertificate, bool isAutCertificate):this(serialNumber,expirationDate)
        {
            this._IsActive = isActive;
            this._isFielCertificate = isFielCertificate;
            this._isAutCertificate = isAutCertificate;
        }

        private string serialNumber;
        public string SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }

        private DateTime expirationDate;
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        private bool _isFielCertificate;
        public bool IsFielCertificate
        {
            get { return _isFielCertificate; }
            set { _isFielCertificate = value; }
        }

        private bool _isAutCertificate;
        public bool IsAutCertificate
        {
            get { return _isAutCertificate; }
            set { _isAutCertificate = value; }
        }

    }
}
