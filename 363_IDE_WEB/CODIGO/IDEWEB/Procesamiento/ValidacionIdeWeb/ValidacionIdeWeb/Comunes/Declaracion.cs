using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionIdeWeb
{
    public class Declaracion
    {
        private int folio;
        private int idMedioRecepcion;
        private int idEstatusRecepcion;
        private int idEntidadReceptora;
        private string rfcAutenticacion;
        private string rfcContribuyente;
        private string direccionIP;
        private string nombreArchivo;
        private int tamañoArchivo;
        private string formato;
        private int materia;
        private bool recepcionNotificado;
        private bool procesamientoNotificado;
        private DateTime fechaRecepcion;
        private DateTime fechaModificacion;
        private bool esNormal;
        private bool esAnual;       
        private CadenaOriginal cadenaOriginal;
        private string numeroOperacion;
        private string selloDigital;
        private string numeroSerie;
        private string archivoFisico;

        public string ArchivoFisico
        {
          get { return archivoFisico; }
          set { archivoFisico = value; }
        }

        public string NumeroSerie
        {
            get { return numeroSerie; }
            set { numeroSerie = value; }
        }


        public string SelloDigital
        {
            get { return selloDigital; }
            set { selloDigital = value; }
        }


        public string NumeroOperacion
        {
            get { return numeroOperacion; }
            set { numeroOperacion = value; }
        }

        public CadenaOriginal CadenaOriginal
        {
            get { return cadenaOriginal; }
            set { cadenaOriginal = value; }
        }      

        public bool EsAnual
        {
            get { return esAnual; }
            set { esAnual = value; }
        }

        public bool EsNormal
        {
            get { return esNormal; }
            set { esNormal = value; }
        }
        
        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }
        
        public DateTime FechaRecepcion
        {
            get { return fechaRecepcion; }
            set { fechaRecepcion = value; }
        }
       
        public bool ProcesamientoNotificado
        {
            get { return procesamientoNotificado; }
            set { procesamientoNotificado = value; }
        }
        
        public bool RecepcionNotificado
        {
            get { return recepcionNotificado; }
            set { recepcionNotificado = value; }
        }
       
        public int Materia
        {
            get { return materia; }
            set { materia = value; }
        }       

        public string Formato
        {
            get { return formato; }
            set { formato = value; }
        }       

        public int TamañoArchivo
        {
            get { return tamañoArchivo; }
            set { tamañoArchivo = value; }
        }
        
        public string NombreArchivo
        {
            get { return nombreArchivo; }
            set { nombreArchivo = value; }
        }
     
        public string DireccionIP
        {
            get { return direccionIP; }
            set { direccionIP = value; }
        }
       
        public string RfcContribuyente
        {
            get { return rfcContribuyente; }
            set { rfcContribuyente = value; }
        }
       
        public string RfcAutenticacion
        {
            get { return rfcAutenticacion; }
            set { rfcAutenticacion = value; }
        }        

        public int IdEntidadReceptora
        {
            get { return idEntidadReceptora; }
            set { idEntidadReceptora = value; }
        }
        
        public int IdEstatusRecepcion
        {
            get { return idEstatusRecepcion; }
            set { idEstatusRecepcion = value; }
        }
        
        public int IdMedioRecepcion
        {
            get { return idMedioRecepcion; }
            set { idMedioRecepcion = value; }
        }        

        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }
        
    }
}