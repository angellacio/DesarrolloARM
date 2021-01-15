
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ValidadorCertificado:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Security;

namespace SAT.DyP.Negocio.Comun.Procesos.Seguridad
{
    public class ValidadorCertificado
    {
        CertificateInfo _currentInfo;

        public ValidadorCertificado(CertificateInfo datosCertificado)
        {
            _currentInfo = datosCertificado;
        }

        public bool EsCertificadoActivo
        {
            get
            {
                return _currentInfo.State != CertificateState.Active? false:true;
            }
        }

        public bool EsTipoFIEL
        {
            get
            {
                return _currentInfo.Type == CertificateType.ElectronicSignature ? true : false;
                   
            }
        }

        public bool EsTipoAutenticacion
        {
            get
            {
                return _currentInfo.Type == CertificateType.ElectronicSignature ? true : false;
            }
        }
    }
}
