using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEWebCargaDeclaracionesDWH
{
    class Constantes
    {
        
        public const string ErrorIDEWebCargaDeclaracionesDWH = "IdeCargaDeclaracionesDWH Error: ";
        public const int Materia = 251;
        public const string Source = "IdeCargaDeclaracionesDWH";
        public const string Log = "IDE WEB Log";

        public static class SettingNames
        {
            public const string BaseDatos = "Sat.DyP.IDE::BaseDatos";
            public const string DirectorioEnvio = "Sat.DyP.IDE::RutaLocal";            
        }

        public static class NodosXML
        {
            public const string DIM = "DeclaracionInformativaMensualIDE";
            public const string DIA = "DeclaracionInformativaAnualIDE";
            public const string IDDC = "InstitucionDistintaDeCredito";
            public const string IDC = "InstitucionDeCredito";           
        }

        public static class EstatusDWH
        {
            public const int Exitoso = 2;
            public const int Fallido = 3;
        }

        
    }
}
