using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEWebCargaDeclaracionesDWH
{
    class DatosGenerales
    {        
        private int? ejercicio;

        public int? Ejercicio
        {
            get { return ejercicio; }
            set { ejercicio = value; }
        }
        private int? cvePeriodo;

        public int? CvePeriodo
        {
            get { return cvePeriodo; }
            set { cvePeriodo = value; }
        }
        private int? cveTipoDeclaracion;

        public int? CveTipoDeclaracion
        {
            get { return cveTipoDeclaracion; }
            set { cveTipoDeclaracion = value; }
        }
        private string cveVersion;

        public string CveVersion
        {
            get { return cveVersion; }
            set { cveVersion = value; }
        }
        private DateTime? fechaPresOpAnt;

        public DateTime? FechaPresOpAnt
        {
            get { return fechaPresOpAnt; }
            set { fechaPresOpAnt = value; }
        }
        private string numOpAnt;

        public string NumOpAnt
        {
            get { return numOpAnt; }
            set { numOpAnt = value; }
        }
        private string denominacion;

        public string Denominacion
        {
            get { return denominacion; }
            set { denominacion = value; }
        }
        private int? cveTipoInstitucion;

        public int? CveTipoInstitucion
        {
            get { return cveTipoInstitucion; }
            set { cveTipoInstitucion = value; }
        }
        private string rfcRepresentanteLegal;

        public string RfcRepresentanteLegal
        {
            get { return rfcRepresentanteLegal; }
            set { rfcRepresentanteLegal = value; }
        }
        private string curpRepresentanteLegal;

        public string CurpRepresentanteLegal
        {
            get { return curpRepresentanteLegal; }
            set { curpRepresentanteLegal = value; }
        }
        private string nomCompRL;

        public string NomCompRL
        {
            get { return nomCompRL; }
            set { nomCompRL = value; }
        }
        private string nomRL;

        public string NomRL
        {
            get { return nomRL; }
            set { nomRL = value; }
        }
        private string apPatRL;

        public string ApPatRL
        {
            get { return apPatRL; }
            set { apPatRL = value; }
        }
        private string apMatRL;

        public string ApMatRL
        {
            get { return apMatRL; }
            set { apMatRL = value; }
        }
        private double? opRelacionadas;

        public double? OpRelacionadas
        {
            get { return opRelacionadas; }
            set { opRelacionadas = value; }
        }
        private double? depCausantesImpuesto;

        public double? DepCausantesImpuesto
        {
            get { return depCausantesImpuesto; }
            set { depCausantesImpuesto = value; }
        }
        private double? impDeterminadoPeriodo;

        public double? ImpDeterminadoPeriodo
        {
            get { return impDeterminadoPeriodo; }
            set { impDeterminadoPeriodo = value; }
        }
        private double? impRecaudado;

        public double? ImpRecaudado
        {
            get { return impRecaudado; }
            set { impRecaudado = value; }
        }
        private double? impPendienteRecaudar;

        public double? ImpPendienteRecaudar
        {
            get { return impPendienteRecaudar; }
            set { impPendienteRecaudar = value; }
        }
        private double? remanenteRecaudadoPeriodos;

        public double? RemanenteRecaudadoPeriodos
        {
            get { return remanenteRecaudadoPeriodos; }
            set { remanenteRecaudadoPeriodos = value; }
        }
        private double? impEnteradoPeriodo;

        public double? ImpEnteradoPeriodo
        {
            get { return impEnteradoPeriodo; }
            set { impEnteradoPeriodo = value; }
        }
        private double? recaudadoChequesCaja;

        public double? RecaudadoChequesCaja
        {
            get { return recaudadoChequesCaja; }
            set { recaudadoChequesCaja = value; }
        }

        private double? impEnteradoOtrasInstituciones;

        public double? ImpEnteradoOtrasInstituciones
        {
            get { return impEnteradoOtrasInstituciones; }
            set { impEnteradoOtrasInstituciones = value; }
        }


        private string rfcDeclarante;

        public string RfcDeclarante
        {
            get { return rfcDeclarante; }
            set { rfcDeclarante = value; }
        }



    }
}
