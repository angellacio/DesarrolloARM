using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Procesos.Helper
{
    public static class ErroresNegocioHelper
    {
        private static List<int> codigosError = new List<int>(new int[] { 401, 403, 406, 407, 408, 410, 411, 215, 217 });

        public static bool EsErrorNegocio(int codigo)
        {
            return codigosError.Contains(codigo);
        }
    }
}
