
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.DireccionTraductor:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SAT.CreditosFiscales.Motor.Entidades.Enumeraciones
{


    /// <summary>
    /// Dirección de mensaje del motor de traducción.
    /// </summary>
    public enum DireccionTraductor
    {
        /// <summary>
        /// Mensaje que viene de la aplicación origen al motor de traducción.
        /// </summary>
        Entrada = 1,
        /// <summary>
        /// Mensaje que va del motor de traducción a DyP.
        /// </summary>
        Salida        
    }

    /// <summary>
    /// Pasos que ejecuta el traductor.
    /// </summary>
    public enum PasosTraductor
    {
        /// <summary>
        /// El motor de traducción ha recibido un mensaje para procesar.
        /// </summary>
        RecibePeticion = 0,
        /// <summary>
        /// Se han obtenido las reglas que se van a ejecutar sobre el mensaje.
        /// </summary>
        ObtieneReglas,
        /// <summary>
        /// Se han obtenido las reglas que se van a ejecutar sobre el mensaje para SIAT.
        /// </summary>
        ObtieneReglasSIAT,
        /// <summary>
        /// Se ha validado el mensaje de entrada con el XSD.
        /// </summary>
        ValidaXSDEntrada,
        /// <summary>
        /// Se aplicaron las reglas al mensaje.
        /// </summary>
        AplicaReglasAntesCanonicoSIAT,
        /// <summary>
        /// Se aplicaron las reglas al mensaje para SIAT.
        /// </summary>
        AplicaReglasAntesCanonico,    
        /// <summary>
        /// Se completa la transformación canonica
        /// </summary>
        CompletaTransformacionCanonica,
        /// <summary>
        /// Se completa la transformación canonica para SIAT
        /// </summary>
        CompletaTransformacionCanonicaSIAT,
        /// <summary>
        /// Se completa la válidación de que los conceptos tengan una periodicidad válida para el tipo de persona indicada
        /// </summary>
        ValidaConceptoPeriodicidadPorTipoPersona,
        /// <summary>
        /// Agrupacion de conceptos
        /// </summary>
        AgrupacionConceptos,
        /// <summary>
        /// Busqueda de lineas existentes
        /// </summary>
        BuscaLineaExistenteBD,
        /// <summary>
        /// Inserta datos del objeto canónico en DB
        /// </summary>
        InsertaDatosCanonicoDB,
        /// <summary>
        /// Inserta datos del objeto canónico en DB para SIAT
        /// </summary>
        InsertaDatosCanonicoDBSIAT,
        /// <summary>
        /// Aplica las reglas después de insertar canonico
        /// </summary>
        AplicaReglasDespuesCanonico,
        /// <summary>
        /// Completa Esquema DyP
        /// </summary>
        CompletaEsquemaDyP,
        /// <summary>
        /// Valida la salida XSD
        /// </summary>
        ValidaXSDSalida,    
        /// <summary>
        /// Ordenamiento de transacciones DyP
        /// </summary>
        OrdenamientoTransacciones,
        /// <summary>
        /// Se ha enviado el mensaje resultante al servicio de DyP.
        /// </summary>              
        EnviadoDyP,
        /// <summary>
        /// Actualiza los datos en BD
        /// </summary>
        ActualizaDatosDB,
        /// <summary>
        /// Genera documento PDF
        /// </summary>
        GeneraPDF,        
    }
}
