//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:SgiManager:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])

using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Util.Security
{
    /// <summary>
    /// Permite realizar las operaciones básicas de interacción con el ARA
    /// </summary>
    public class SgiManager
    {
        private static ISgiHelperProvider _customProvider=null;

        public static ISgiHelperProvider Provider
        {
            get { return _customProvider; }
            set { _customProvider = value; }
        }
    }
}
