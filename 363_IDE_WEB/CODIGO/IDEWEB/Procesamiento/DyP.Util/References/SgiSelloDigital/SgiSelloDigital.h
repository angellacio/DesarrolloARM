#ifndef __SGISELLODIGITAL_H_
#define __SGISELLODIGITAL_H_

#ifdef SGISELLODIGITAL_EXPORTS
   #define SGISELLODIGITAL_API __declspec(dllexport)
#else
   #define SGISELLODIGITAL_API __declspec(dllimport)
#endif

#include <string>

namespace SgiSelloDigital
{
   /////////////////////////////////////////////////////////////////////////////
   enum 
   {
      Error_WsaStartup     =  100000,
      Error_GetHostByName  =  200000,
      Error_GetServByName  =  300000,
      Error_Socket         =  400000,
      Error_Bind           =  500000,
      Error_Connect        =  600000,
      Error_Send           =  700000,
      Error_Recv           =  800000,
      Error_Shutdown       =  900000,
      Error_RecvFin        = 1000000,
      Error_CloseSocket    = 1100000,
      Error_WsaCleanup     = 1200000,
      Error_SetSockOpt     = 1300000,

      // Errores del programa 
      Error_Apl_NoIni      =      1,
      Error_Apl_DatosPend  =      2,
      Error_Apl_MalRespSrv =      3,
      Error_Apl_Mem        =     99,

      // Errores del programa (ASI)
      Error_Apl_Oper_Inv   =     -1
   };

   /////////////////////////////////////////////////////////////////////////////


   typedef void*  PtrSelloDigital;

   // ################## creación y liberación de objetos ####################
   SGISELLODIGITAL_API PtrSelloDigital new_SelloDigital();
   SGISELLODIGITAL_API void            delete_SelloDigital(PtrSelloDigital sello);

   // ################## SelloDigital ####################
   SGISELLODIGITAL_API int Inicia   (PtrSelloDigital sello, const char* host, const char* servicio);
   SGISELLODIGITAL_API int GetNSerie(PtrSelloDigital sello, std::string* nserie);
   SGISELLODIGITAL_API int GetFirma (PtrSelloDigital sello, const char* cadenaOriginal, std::string* firma);
   SGISELLODIGITAL_API int Termina  (PtrSelloDigital sello);
};

#endif //__SGISELLODIGITAL_H_
