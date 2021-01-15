#ifndef _SGICRIPTO_H_
#define _SGICRIPTO_H_

#include <stddef.h>
#include <string>

#ifdef SGICRIPTO_EXPORTS
#define SGICRIPTO_API __declspec(dllexport)
#else
#define SGICRIPTO_API __declspec(dllimport)
#endif

namespace SgiCripto
{
   // #####################################################################
   typedef enum : char
   {
      CEdoActivo     = 'A',
      CEdoRevocado   = 'R',
      CEdoCaduco     = 'C',
      CEdoTodos      = 'X'  // ocupado para la solicitud de listas de CDs
   } tCertEdo;

   typedef enum : unsigned char
   {
      CTipoTodos = 0,   // ocupado para la solicitud de listas de CDs
      CTipoFEA   = 1,
      CTipoCSD   = 2
   } tCertTipo;

   typedef enum : int
   {
	   ADig_MD5     =  0,	/*Algoritmo MD5*/
	   ADig_MD4     =  1,	/*Algoritmo MD4*/
	   ADig_MD2     =  2,	/*Algoritmo MD2*/
	   ADig_SHA     =  3,	/*Algoritmo SHA*/
	   ADig_SHA1    =  4,	/*Algoritmo SHA1*/
	   ADig_RIPE160 =  5,	/*Algoritmo RIPEMD160*/
	   ADig_SHA224  =  6,   /*Algoritmo sha 224 bits*/
	   ADig_SHA256  =  7,   /*Algoritmo sha 256 bits*/
	   ADig_SHA384  =  8,   /*Algoritmo sha 385 bits*/
	   ADig_SHA512  =  9,   /*Algoritmo sha 512 bits*/
	   ADig_MDC2    = 10    /*Algoritmo MDC2 128 bits*/
   } tAlgDig;

   typedef enum : int
   {
/*PKCS7*/	AEnc_DES_CBC       = 100,	/*Algoritmo de cifrado DES-CCB (Cipher Block Chaining)*/
/*PKCS7*/	AEnc_DES_EDE       = 101,	/*Algoritmo de cifrado DES-EDE*/
            AEnc_DES_EDE3      = 102,	/*Algoritmo de cifrado triple DES-EDE3*/
/*PKCS7*/	AEnc_DES_CFB       = 103,	/*Algoritmo DES en cipher feedback*/
            AEnc_DES_EDE_CFB   = 104,	/*Algoritmo triple DES con dos llaves en cipher feedback*/
            AEnc_DES_EDE3_CFB  = 105,	/*Algoritmo triple DES con tres llaves en cipher feed back*/
/*PKCS7*/	AEnc_DES_OFB       = 106,	/*Algoritmo DES en output feedback*/
            AEnc_DES_EDE_OFB   = 107,	/*Algoritmo triple DES con dos llaves en output feedback*/
            AEnc_DES_EDE3_OFB  = 108,	/*Algoritmo triple DES con tres llaves en output feedback*/
            AEnc_DES_EDE_CBC   = 109,	/*Algoritmo triple DES con dos llaves en cipher block chaining*/
/*PKCS7*/	AEnc_DES_EDE3_CBC  = 110,	/*Algoritmo triple DES con tres llaves en cipher block chaining*/
            AEnc_DESX_CBC      = 111,	/*Algoritmo DES en cipher block chaining (variacion que soporta ataque de fuerza bruta)*/
/*PKCS7*/	AEnc_RC2_CBC       = 112,	/*Algoritmo de cifrado RC2 en cipher block chaining*/
            AEnc_RC2_CFB       = 113,	/*Algoritmo de cifrado RC2 en cipher feedback*/
            AEnc_RC2_OFB       = 114,	/*Algoritmo de cifrado RC2 en output feedback*/
            AEnc_RC4           = 115,	/*Algoritmo de cifrado RC4*/
            AEnc_RC4_40        = 116,	/*Algoritmo de cifrado RC4*/
/*PKCS7*/	AEnc_IDEA_CBC      = 117,	/*Algoritmo de cifrado IDEA en cipher block chaining*/
            AEnc_IDEA_CFB      = 118,	/*Algoritmo de cifrado IDEA en cipher feedback*/
            AEnc_IDEA_OFB      = 119,	/*Algoritmo de cifrado IDEA en output feedback*/
/*PKCS7*/	AEnc_BF_CBC        = 120,	/*Algoritmo de cifrado Blow fish en cipher block chaining*/
            AEnc_BF_CFB        = 121,	/*Algoritmo de cifrado Blow fish en cipher feedcack*/
            AEnc_BF_OFB        = 122,	/*Algoritmo de cifrado Blow fish en output feedback*/
/*PKCS7*/	AEnc_CAST5_CBC     = 123,	/*Algoritmo de cifrado CAST5 en cipher block chaining*/
            AEnc_CAST5_CFB     = 124,	/*Algoritmo de cifrado CAST5 en cipher feedback*/
            AEnc_CAST5_OFB     = 125,	/*Algoritmo de cifrado CAST5 en output feedback*/
            AEnc_RC5_CBC       = 126,	/*Algoritmo de cifrado RC5 en cipher block cahining*/
            AEnc_RC5_CFB       = 127,	/*Algoritmo de cifrado RC5 en cipher feedback*/
            AEnc_RC5_OFB       = 128,	/*Algoritmo de cifrado RC5 en output feedback*/
/*PKCS7*/	AEnc_AES_128_CBC   = 129,	/*Algoritmo de cifrado AES 128 en cipher block chaining*/
/*PKCS7*/	AEnc_AES_128_CFB   = 130,	/*Algoritmo de cifrado AES 128 en cipher feedback*/
/*PKCS7*/	AEnc_AES_128_OFB   = 131,	/*Algoritmo de cifrado AES 128 en output feedback*/
/*PKCS7*/	AEnc_AES_192_CBC   = 132,	/*Algoritmo de cifrado AES 192 en cipher block chaining*/
/*PKCS7*/	AEnc_AES_192_CFB   = 133,	/*Algoritmo de cifrado AES 192 en cipher feedback*/
/*PKCS7*/	AEnc_AES_192_OFB   = 134,	/*Algoritmo de cifrado AES 192 en output feedback*/
/*PKCS7*/	AEnc_AES_256_CBC   = 135,	/*Algoritmo de cifrado AES 256 en cipher block chaining*/
/*PKCS7*/	AEnc_AES_256_CFB   = 136,	/*Algoritmo de cifrado AES 256 en cipher feedback*/
/*PKCS7*/	AEnc_AES_256_OFB   = 137	/*Algoritmo de cifrado AES 256 en output feedback*/
   } tAlgEnc;

   typedef enum : int
   {
      TPKCS7_Firmado             = 1,
      TPKCS7_Ensobretado         = 2,
      TPKCS7_FirmadoYEnsobretado = 3      
   } tTipoPKCS7;

   typedef enum : int
   {
      Error_OK                   =      0,
      // ERRORES DE NEGOCIO

      // Errores de operación
      Error_Cert_Decodifica      =    100,   // Certificado dañado o mal formado
      Error_Cert_AsigLlavePub    =   -101,   // 
      Error_Cert_NoCorrespPvK    =    102,   // Llaves (publica <-> privada) no correspondientes
      Error_Cert_AbrirArchivo    =    103,   // Error al abrir archivo de certificado
      Error_PvK_Decodifica       =    150,   // Llave privada dañada o mal formada
      Error_PvK_AbrirArchivo     =    151,   // Error al abrir archivo de la llave privada
      Error_PvK_PwdIncorrecto    =    152,   // 
      Error_B64_Decodifica       =    200,
      Error_B64_Codifica         =    201,
      Error_Firma_Generacion     =    300,
      Error_Firma_DesencrInv     =    301,
      Error_Firma_NoIniciada     =   -302,
      Error_Firma_NoCorresponde  =    303,
      Error_Firma_EstrIdInv      =    304,

      Error_PKCS7_NoGenera       =    400,
      Error_PKCS7_Decodifica     =    401,
      Error_PKCS7_ExtraeFirmante =    402,
      Error_PKCS7_NoInfoFirmante =    403,
      Error_PKCS7_AbrirArchivo   =    404,
      Error_PKCS7_CrearArchivo   =    405,
      Error_PKCS7_Verificacion   =    406,
      Error_PKCS7_CertFirmaTipo  =    407,
      Error_PKCS7_CertFirmaNoAct =    408,
      Error_PKCS7_ExtraeDest     =    409,
      Error_PKCS7_NoInfoDest     =    410,
      Error_PKCS7_Desensobretado =    411,
      Error_PKCS7_AddDest        =    412,
      Error_PKCS7_AddFirma       =    413,
      Error_PKCS7_IniData        =    414,
      Error_PKCS7_TipoOper       =    415,

      Error_Enc_CargaAlgoritmo   =    500,
      Error_Enc_IniciaContexto   =    501,
      Error_Enc_Proceso          =    502,
      Error_Enc_SetCipher        =    503,

      Error_Dig_AlgNoVal         =    600,

      // ERRORES GRAVES: PROBLEMAS DE PROGRAMACION
      Error_InterfaseNula        = -    1,
      Error_MemInsuficiente      = -    2,
      Error_ParmSinEspacioSuf    = -    3,
      Error_ParmNulo             = -    4,
      Error_Cert_NoIniciado      = -  100,
      Error_ARA_NoReq            = -  200,
      Error_ARA_NoIniciada       = -  201,
      Error_ARA_NoConectada      = -  202,
      Error_ARA_CargaCfg         = -  203,
      Error_ARA_LeeParmsCfg      = -  204,
      Error_ARA_NoSetBitacora    = -  205,
      Error_ARA_NoIniciaMsg      = -  206,
      Error_ARA_NoSSLCli         = -  207, // No se puede conectar a la ARA
      Error_ARA_NoMsgCli         = -  208,
      Error_ARA_NoSetMsg         = -  209,
      Error_ARA_NoGetMsg         = -  210,
      Error_ARA_Transmite        = -  211,
      Error_ARA_Recibe           = -  212,
      Error_ARA_RecibeError      = -  213,
      Error_ARA_MsgNoEsperado    = -  214,
      Error_ARA_NoExisteCert     =    215,
      Error_ARA_CertNoDisponible =    216,
      Error_ARA_NoSerieInv       =    217,
      Error_ARA_RFCInv           =    218,
      Error_ARA_InfoNoDisp       =    219,


      Error_NoIdentificado       = -  999
      //Error_ARA_
      // ERRORES GRAVES

   } tError;
   // #####################################################################

   typedef void*     PtrSgiCripto;
   typedef void*     PtrSgiCert;

   // ################## creación de objetos ####################
   SGICRIPTO_API   PtrSgiCripto   new_SgiCripto();
   SGICRIPTO_API   void           delete_SgiCripto(PtrSgiCripto cripto);

   SGICRIPTO_API   PtrSgiCert     new_SgiCertificado();
   SGICRIPTO_API   void           delete_SgiCertificado(PtrSgiCert cert);

   // ################## Sgi_Cripto ####################
   SGICRIPTO_API   tError   inicia         (PtrSgiCripto cripto, bool reqAra, const char *araCfg = NULL);

   SGICRIPTO_API   tError   conectaARA     (PtrSgiCripto cripto);
   SGICRIPTO_API   void     desconectaARA  (PtrSgiCripto cripto);
   SGICRIPTO_API   tError   keepaliveARA   (PtrSgiCripto cripto);

   SGICRIPTO_API   tError   solCDxNS       (PtrSgiCripto cripto, const char *no_serie, tCertEdo* estado, 
                                            tCertTipo* tipo, std::string *vig_ini, std::string *vig_fin, 
                                            unsigned char *cert, /*[in, out]*/ int *lcert);
   SGICRIPTO_API   tError   solEdoCDxNS    (PtrSgiCripto cripto, const char *no_serie, tCertEdo* estado, 
                                            tCertTipo* tipo, std::string *vig_ini, std::string *vig_fin);
   SGICRIPTO_API   tError   solNSCDFEAxRFC (PtrSgiCripto cripto, const char *rfc, std::string *no_serie);
   SGICRIPTO_API   tError   solListaCDxRFC (PtrSgiCripto cripto, const char *rfc, tCertEdo estado, tCertTipo tipo, 
                                            unsigned __int16 numCerts, std::string *lista);

   SGICRIPTO_API   tError   verificaLlaves (PtrSgiCripto cripto, const char *pathCert, 
                                            const char *pathKey, const char *pwd);
      
   SGICRIPTO_API   tError   generaFirma    (PtrSgiCripto cripto, const char *cadenaOriginal, const char *pathPrivKey, 
                                            const char *pwd, tAlgDig algdig, std::string *firmaB64);

   SGICRIPTO_API   tError   verFirmaIni    (PtrSgiCripto cripto, PtrSgiCert certificado, const char *firmaB64);
   SGICRIPTO_API   tError   verFirmaProceso(PtrSgiCripto cripto, const char *buffParcial, unsigned int lbuff);
   SGICRIPTO_API   tError   verFirmaFin    (PtrSgiCripto cripto);
	   
   SGICRIPTO_API   tError   verFirma       (PtrSgiCripto cripto, PtrSgiCert certificado, const char *firmaB64, 
                                            const char *cadenaOriginal);

   SGICRIPTO_API   tError   genP7          (PtrSgiCripto cripto, const char *certFirmante, const char *pvKFirmante,
                                            const char *pwd, const char *pathArchOrigen, tTipoPKCS7 tipo, 
                                            const char *pathArchDest, tAlgEnc algenc, const char* certDestinatario);

   SGICRIPTO_API   tError   obtieneNumSerieP7(PtrSgiCripto cripto, const char *pathPKCS7, std::string *nseries);

   SGICRIPTO_API   tError   procP7         (PtrSgiCripto cripto, const char *certDestinatario, const char *pvKDestinatario, 
                                            const char *pwd, const char *pathArchPKCS7, const char *pathArchDest, 
                                            const char * CApath, tCertTipo tc);

   SGICRIPTO_API   tError   procP7         (PtrSgiCripto cripto, const char *certDestinatario, const char *pvKDestinatario, 
                                            const char *pwd, const char *bufPKCS7, int lngBufPKCS7, char **bufDest, int* lbufDest,  
                                            const char * CApath, tCertTipo tc);


   SGICRIPTO_API   tError   encripta       (PtrSgiCripto cripto, const unsigned char *bufdatos, unsigned __int16 lbufdatos, 
                                            tAlgEnc algenc, const unsigned char *pwd, int lpwd, 
                                            unsigned char* bufenc, /*[in, out]*/ unsigned __int16 *lbufenc);
   SGICRIPTO_API   tError   desencripta    (PtrSgiCripto cripto, const unsigned char *bufenc, unsigned __int16 lbufenc, 
                                            tAlgEnc algenc, const unsigned char *pwd, int lpwd, 
                                            unsigned char* bufdatos, /*[in, out]*/ unsigned __int16 *lbufdatos);

   // ################## Sgi_Certificado ####################

   SGICRIPTO_API   tError   inicializa(PtrSgiCert cert, const unsigned char* bufcert, int lbufcert);
   SGICRIPTO_API   tError   getNSerie (PtrSgiCert cert, std::string* serie);
   SGICRIPTO_API   tError   getRFC    (PtrSgiCert cert, std::string* rfc);
   SGICRIPTO_API   tError   getNom    (PtrSgiCert cert, std::string* nom);
   SGICRIPTO_API   tError   getVigIni (PtrSgiCert cert, std::string* vig);
   SGICRIPTO_API   tError   getVigFin (PtrSgiCert cert, std::string* vig);

   // ######################################

   SGICRIPTO_API   const char* msgError(tError error);
};


#endif // _SGICRIPTO_H_
