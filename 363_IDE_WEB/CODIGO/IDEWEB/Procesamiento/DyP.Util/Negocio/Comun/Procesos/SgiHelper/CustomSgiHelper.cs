
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:CustomSgiHelper:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Security;
//using SAT.DyP.Util.Security.Interop.SgiHelpers;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.Caching;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Types;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Negocio.Comun.Procesos.Helper;

namespace SAT.DyP.Negocio.Comun.Procesos.SgiHelper
{
    /// <summary>
    /// Proveedor de SgiHelpers en modo InProc
    /// </summary>
    [Obsolete("Clase obsoleta, usar NetSgiHelper", true)]
    public class CustomSgiHelper:ISgiHelperProvider
    {
        #region ISgiHelperProvider Members

        public bool EsFirmaValida(string numeroSerie, string cadenaOriginal, string selloDigital)
        {
            bool _result = false;
            string mensajeError = String.Format("Error al intentar validar la firma con el numero de serie: '{0}'.", numeroSerie);

            //SgiCryptoHelper _helper = new SgiCryptoHelper(true, null);
            //CertificateInfo _certificateInfo = new CertificateInfo();

            try
            {
                //byte[] _certificate = _helper.GetCertificateBySerialNumber(numeroSerie, ref _certificateInfo);
                //SgiCertificateHelper _certificateHelper = new SgiCertificateHelper(_certificate);
                //_result = _helper.IsSignatureValid(_certificateHelper, cadenaOriginal, selloDigital);
            }
            catch (BusinessException ex)
            {
                if (ErroresNegocioHelper.EsErrorNegocio(ex.LegacyErrorCode))
                {
                    throw;
                }
                else
                {
                    throw new PlatformException(mensajeError, ex);
                }
            }
            catch (PlatformException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlatformException(mensajeError, ex);
            }
            finally
            {
                //if (_helper != null)
                //{
                //    _helper.Dispose();
                //}
            }

            return _result;
        }

        public string GenerarSelloDigital(string cadenaOriginal)
        {
            string cadenaFirmada = string.Empty;
            //SgiDigitalSealHelper _helper = null;

            try
            {
                string _hostName = ReadFromSettings(Constantes.SELLO_DIGITAL_HOSTNAME);
                string _serviceName = ReadFromSettings(Constantes.SELLO_DIGITAL_SERVICENAME);

                //_helper = new SgiDigitalSealHelper(_hostName, _serviceName);

                //cadenaFirmada = _helper.GenerateDigitalSeal(cadenaOriginal);
            }
            catch (PlatformException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlatformException("Error al generar el sello digital.", ex);
            }
            finally
            {
                //if (_helper != null)
                //{
                //    _helper.Dispose();
                //}
            }

            return cadenaFirmada;            
        }

        public bool VerificarFIELContribuyente(string rfc)
        {
            string mensajeError = String.Format("Error al obtener FIEL de contribuyente con el RFC '{0}' en la ARA.", rfc);
            bool result = false;
            //SgiCryptoHelper helper = null;

            try
            {
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
                if (ErroresNegocioHelper.EsErrorNegocio(ex.LegacyErrorCode))
                {
                    EventLogHelper.WriteWarningEntry(mensajeError, IdentificadorEvento.NEGOCIO_COMUN_EVENT_ID);
                }
                else
                {
                    throw new PlatformException(mensajeError, ex);
                }

                result = false;
            }
            catch (PlatformException)
            {
                throw;
            }
            catch
            {
                throw new PlatformException(mensajeError);
            }
            finally
            {
                //if (helper != null)
                //{
                //    helper.Dispose();
                //}
            }

            return result;
        }

        public CertificateInfo ObtenerCertificado(string numeroSerie)
        {
            string mensajeError = String.Format("Error al consultar el certificado por el numero de serie '{0}'", numeroSerie);
            //SgiCryptoHelper helper = null;
            CertificateInfo certificateInfo = new CertificateInfo();

            try
            {
                //helper = new SgiCryptoHelper(true, null);
                //certificateInfo = (CertificateInfo)helper.GetCertificateInfoBySerialNumber(numeroSerie);
            }
            catch (BusinessException ex)
            {
                if (ErroresNegocioHelper.EsErrorNegocio(ex.LegacyErrorCode))
                {
                    throw;
                }
                else
                {
                    throw new PlatformException(mensajeError, ex);
                }
            }
            catch (PlatformException)
            {
                throw;
            }
            catch
            {
                throw new PlatformException(mensajeError);
            }
            finally
            {
                //if (helper != null)
                //{
                //    helper.Dispose();
                //}
            }

            return certificateInfo;
        }

        public string DesencriptarDeclaracion(string nombreArchivo, string contenidoDocumento, int medioPresentacion, string rfcAutenticacion, string rfc)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Dispose()
        {
            
        }

        private string ReadFromSettings(string key)
        {
            string _value = string.Empty;

            if (CachingService.Contains(key))
            {
                _value = CachingService.GetItem(key).ToString();
            }
            else
            {
                _value = ConfigurationManager.ApplicationSettings.ReadSetting(key);
                CachingService.AddItem(key, _value);
            }

            return _value;
        }

        #endregion
    }
}
