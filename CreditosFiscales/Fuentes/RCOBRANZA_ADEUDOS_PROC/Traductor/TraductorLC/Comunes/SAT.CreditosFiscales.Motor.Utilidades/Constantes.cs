
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Utilidades:SAT.CreditosFiscales.Motor.Utilidades.Constantes:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Utilidades
{
    /// <summary>
    /// Esta clase contiene todas las constantes para buscar en catálogo de reglas
    /// </summary>
    public static class Constantes
    {
        #region GeneracionCanonico
        public const string ValidEncondingXML = "utf-8";

        public const string ImporteHistoricoPadre = "ImporteHistoricoPadre";

        public const string ImporteParteActualizadaPadre = "ImporteParteActualizadaPadre";

        public const string ImportePagarPadre = "ImportePagarPadre";

        public const string ImporteParteActualizadaHijo = "ImporteParteActualizadaHijo";

        public const string ImportePagarHijo = "ImportePagarHijo";

        public const string ImporteDescuentoPadre = "ImporteDescuentoPadre";

        public const string ImporteDescuentoHijo = "ImporteDescuentoHijo";

        public const string TotalMontoCargo = "TotalMontoCargo";

        public const string TotalMontoAbono = "TotalMontoAbono";

        public const string TotalACargo = "TotalACargo";

        public const string ImportePagosAnteriores = "ImportePagosAnteriores";

        public const string ImporteSinPagoInicial = "ImporteSinPagoInicial";

        public const string ImportePagoInicial = "ImportePagoInicial";

        #endregion

        #region Constantes de Error

        public const string ErrorMensajeVacio = "MotorCF:MensajeVacio";

        public const string RegistraError = "MotorCF:RegistraError";    
   
        public const string ObtenerFolio = "MotorCF:ObtenerFolio";
                
       public const string RegistraProcesamiento = "MotorCF:RegistraProcesamiento";
        
       public const string ObtenerContrato = "MotorCF:ObtenerContrato";
        
       public const string InicializaEsquema = "MotorCF:InicializaEsquema";
        
       public const string ValidacionEsquema = "MotorCF:ValidacionEsquema";
        
       public const string EsquemaNulo = "MotorCF:EsquemaNulo";
        
       public const string RegistroMensajeEntrada = "MotorCF:RegistroMensajeEntrada";
        
       public const string RegistroMensajeCanonico = "MotorCF:RegistroMensajeCanonico";
        
       public const string EjecutaTransformacion = "MotorCF:EjecutaTransformacion";        

       public const string InicializaXSLT = "MotorCF:InicializaXSLT";
        
       public const string SinContratos = "MotorCF:SinContratos";
        
       public const string SinReglas = "MotorCF:SinReglas";
        
       public const string ObtenerConfiguracion = "MotorCF:ObtenerConfiguracion";
        
       public const string ContratoEntrada = "MotorCF:ContratoEntrada";
        
       public const string ContratoDyP = "MotorCF:ContratoDyP";
        
       public const string EjecucionReglaNet = "MotorCF:EjecucionReglaNet";
        
        public const string NodosReglaNet ="MotorCF:NodosReglaNet";
        
        public const string ServicioWebMotorCreditosFiscales = "MotorCF:ServicioWebMotorCreditosFiscales";
        
        public const string ServicioWebDyP = "MotorCF:ServicioWebDyP";
        
        public const string GeneracionSolicitudDyP = "MotorCF:GeneracionSolicitudDyP";
        
        public const string BusquedaLCGenerada = "MotorCF:BusquedaLCGenerada";
        
        public const string CompletaCanonico = "MotorCF:CompletaCanonico";

        public const string ErrorComunicacionServicioWebDyP = "MotorCF:ServicioWebDyPComunicacion";

        public const string GeneracionPDF = "MotorCF:GeneracionPDF";

        public const string ErrorValidaConceptoPeriodicidadPorTipoPersona = "MotorCF:ValidaConceptoPeriodicidadPorTipoPersona";

        public const string AgrupacionConceptosDyP = "MotorCF:AgrupacionConceptosDyP";

        public const string ConceptoPersonaPeriodicidad = "MotorCF:ConceptoPersonaPeriodicidad";

        public const string RecuperaConceptoPersonaPeriodicidad = "MotorCF:RecuperaConceptoPersonaPeriodicidad";

        public const string FinalConceptoPersonaPeriodicidad = "MotorCF:FinalConceptoPersonaPeriodicidad";

        public const string OrdenamientoTransaccciones = "MotorCF:OrdenamientoTransacciones";

        #endregion

        #region Configuracion

        public const string RFCGenerico = "Generico:Rfc";

        public const string BOIDGenerico = "Generico:BoId";

        public const string ActivaNeteo = "Con:Neteo:Activo";

        #endregion
    }



}
