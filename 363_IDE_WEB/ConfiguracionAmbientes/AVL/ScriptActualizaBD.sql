USE DyPUtil
GO
UPDATE ApplicationSettings SET SettingValue = 'Server=10.229.132.12\10.229.132.13;Database=IDEWEB;User Id=usrIDEWebBD;password=/*PaswordCorrecto*/;' WHERE SettingName = 'SAT.DyP.IDE::BaseDatos';
UPDATE ApplicationSettings SET SettingValue = 'P:\DELCARACIONES\IDE\Envio' WHERE SettingName = 'SAT.DyP.IDE::RutaLocal';
UPDATE ApplicationSettings SET SettingValue = 'P:\DELCARACIONES\IDE\UnZip' WHERE SettingName = 'Sat.DyP.Ide::Validar.directorioArchivosUnZip';
UPDATE ApplicationSettings SET SettingValue = 'P:\DELCARACIONES\IDE\Acuses' WHERE SettingName = 'Sat.DyP.Ide::Validar.pathAcuses';
UPDATE ApplicationSettings SET SettingValue = 'Server=10.229.132.12\10.229.132.13;Database=DyPUtil;User Id=usrIDEWebBD;password=/*PaswordCorrecto*/;' WHERE SettingName = 'SAT.DyP.Negocio.Common.EnvioAMA::TransmitControl';
UPDATE ApplicationSettings SET SettingValue = 'Server=10.229.136.217\TQIDCUATSQLI02,1433;Database=sped;User Id=admscade;password=/*PaswordCorrecto*/;' WHERE SettingName = 'SAT.DyP.Negocio.Comun::BD_SPED';
UPDATE ApplicationSettings SET SettingValue = 'Server=10.229.132.12\10.229.132.13;Database=DyPUtil;User Id=usrIDEWebBD;password=/*PaswordCorrecto*/;' WHERE SettingName = 'SAT.DyP.Util::SCADE_NET';
UPDATE ApplicationSettings SET SettingValue = 'C:\SAT\Certificados\Tradicional\SAT_AUT_CERT\AC_SAT_DESA.cer' WHERE SettingName = 'SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::SATCertificateAuthority';
UPDATE ApplicationSettings SET SettingValue = 'C:\SAT\Certificados\Tradicional\De_srv.key' WHERE SettingName = 'SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionCertificatePath';



