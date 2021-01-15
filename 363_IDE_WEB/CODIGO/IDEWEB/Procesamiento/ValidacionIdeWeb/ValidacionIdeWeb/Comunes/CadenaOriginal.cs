using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace ValidacionIdeWeb
{
    public class CadenaOriginal
    {
        //ANUAL Y MENSUAL
        private string rfcDeclarante;

        public string RfcDeclarante
        {
            get { return rfcDeclarante; }
            set { rfcDeclarante = value; }
        }
        //MENSUAL
        private long importeCheques;

        public long ImporteCheques
        {
            get { return importeCheques; }
            set { importeCheques = value; }
        }
        //ANUAL Y MENSUAL
        private long importeExcedenteDepositos;

        public long ImporteExcedenteDepositos
        {
            get { return importeExcedenteDepositos; }
            set { importeExcedenteDepositos = value; }
        }
        private long importeDeterminadoDepositos;

        public long ImporteDeterminadoDepositos
        {
            get { return importeDeterminadoDepositos; }
            set { importeDeterminadoDepositos = value; }
        }
        private long importeRecaudadoDepositos;

        public long ImporteRecaudadoDepositos
        {
            get { return importeRecaudadoDepositos; }
            set { importeRecaudadoDepositos = value; }
        }
        //ANUAL
        private long importePendienteDepositos;

        public long ImportePendienteDepositos
        {
            get { return importePendienteDepositos; }
            set { importePendienteDepositos = value; }
        }
        //MENSUAL
        private long importeRemanenteDepositos;

        public long ImporteRemanenteDepositos
        {
            get { return importeRemanenteDepositos; }
            set { importeRemanenteDepositos = value; }
        }
        private long importeEnterado;

        public long ImporteEnterado
        {
            get { return importeEnterado; }
            set { importeEnterado = value; }
        }
        //ANUAL Y MENSUAL
        private long operacionesRelacionadas;

        public long OperacionesRelacionadas
        {
            get { return operacionesRelacionadas; }
            set { operacionesRelacionadas = value; }
        }
        private string denominacion;

        public string Denominacion
        {
            get { return denominacion; }
            set { denominacion = value; }
        }
        private DateTime fechaPresentacion = DateTime.MinValue;

        public DateTime FechaPresentacion
        {
            get { return fechaPresentacion; }
            set { fechaPresentacion = value; }
        }
        private string version;

        public string Version
        {
            get { return version; }
            set { version = value; }
        }
        private int ejercicio;

        public int Ejercicio
        {
            get { return ejercicio; }
            set { ejercicio = value; }
        }
        //MENSUAL
        private int periodo;

        public int Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }

        /// Datos para Validar
        public bool EsAnual { get; set; }
        public bool EsNormal { get; set; }
        public string VersionSistema
        {
            get
            {
                string result = "2.0";
                if (EsAnual)
                {
                    if (Ejercicio >= 2014) result = "3.0";
                }
                return result;
            }
        }
        public string NodoPrincipal
        {
            get
            {
                string result = Constantes.ElementoXml.DeclaracionInformativaMensualIDE;
                if (EsAnual)
                {
                    if (VersionSistema == "3.0") result = Constantes.ElementoXml.DeclaracionInformativaAnualISR;
                    else result = Constantes.ElementoXml.DeclaracionInformativaAnualIDE;
                }
                return result;
            }
        }
        public string NodoTipoDeclaracion
        {
            get
            {
                string result = Constantes.ElementoXml.Complementaria;
                if (EsNormal) result = Constantes.ElementoXml.Normal;
                return result;
            }
        }
        public string ArchivoXSD
        {
            get
            {
                string result = Path.Combine(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, Constantes.Validar.directorioEsquemas), AccesoDatos.getValorString(Constantes.Validar.pathXsdValidar));
                if (EsAnual)
                {
                    if (VersionSistema == "3.0") result = Path.Combine(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, Constantes.Validar.directorioEsquemas), AccesoDatos.getValorString(Constantes.Validar.pathXsdValidar3));
                }
                return result;
            }
        }



        public override string ToString()
        {
            string separadorCampos = "|";
            StringBuilder cadena = new StringBuilder();
            cadena.Append(separadorCampos);
            cadena.Append(separadorCampos);
            cadena.Append(string.Format("10001={0}", this.rfcDeclarante));
            cadena.Append(separadorCampos);
            cadena.Append(this.importeCheques);
            cadena.Append(separadorCampos);
            cadena.Append(this.importeExcedenteDepositos);
            cadena.Append(separadorCampos);
            cadena.Append(this.importeDeterminadoDepositos);
            cadena.Append(separadorCampos);
            cadena.Append(this.importeRecaudadoDepositos);
            cadena.Append(separadorCampos);
            cadena.Append(this.importePendienteDepositos);
            cadena.Append(separadorCampos);
            cadena.Append(this.importeRemanenteDepositos);
            cadena.Append(separadorCampos);
            cadena.Append(this.importeEnterado);
            cadena.Append(separadorCampos);
            cadena.Append(this.operacionesRelacionadas);
            cadena.Append(separadorCampos);
            cadena.Append(this.denominacion);
            cadena.Append(separadorCampos);
            cadena.Append(this.fechaPresentacion.ToString());
            cadena.Append(separadorCampos);
            cadena.Append(this.fechaPresentacion.ToShortTimeString());
            cadena.Append(separadorCampos);
            cadena.Append(this.version);
            cadena.Append(separadorCampos);
            cadena.Append(this.ejercicio);
            cadena.Append(separadorCampos);
            cadena.Append(this.periodo);
            cadena.Append(separadorCampos);
            cadena.Append(string.Format("30003=000001000007000112188"));
            cadena.Append(separadorCampos);
            cadena.Append(separadorCampos);

            return cadena.ToString();
        }
    }
}