
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:Bitacora:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase que representa un registro en la Bitacora
    /// </summary>
    [Serializable]
    public class Bitacora
    {
        private long _IdEvento;
        public long IdEvento
        {
            get { return _IdEvento; }
            set { _IdEvento = value; }
        }

        private long _IdProceso;
        public long IdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }

        private int _IdMensaje;
        public int IdMensaje
        {
            get { return _IdMensaje; }
            set { _IdMensaje = value; }
        }

        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private DateTime _Fecha;
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }
    }
}
