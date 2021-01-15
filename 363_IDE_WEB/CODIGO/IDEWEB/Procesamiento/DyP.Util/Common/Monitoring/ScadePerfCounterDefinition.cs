//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Monitoring:ScadePerfCounterDefinition:0:21/May/2008[SAT.DyP.Util.Monitoring:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Util.Monitoring
{
    /// <summary>
    /// Clase auxiliar en donde se definen en código los datos de cada uno de los contadores de desempeño que la aplicación va a usar,
    /// para que puedan ser consultados tanto en tiempo de instalación durante el setup de la aplicación, como en tiempo de ejecución 
    /// para actualizar los valores de estos contadores.
    /// </summary>
    internal class ScadePerfCounterDefinition
    {
        // Nombre de la categoría de performance counters
        public const string CategoryName = "DyP External Calls";
        public const string CategoryHelp = "Contadores de desempeño de las llamadas a otros sistemas DyP";
        public const string DefaultInstanceName = "Default";

        public ScadePerfCounterDefinition(string name, string help, System.Diagnostics.PerformanceCounterType type)
        {
            this.CounterName = name;
            this.CounterHelp = help;
            this.CounterType = type;
        }

        public string CounterName;
        public string CounterHelp;
        public System.Diagnostics.PerformanceCounterType CounterType;

        private static Dictionary<string, ScadePerfCounterDefinition> _counterDefinitions = new Dictionary<string, ScadePerfCounterDefinition>();

        internal static Dictionary<string, ScadePerfCounterDefinition> CounterDefinitions
        {
            get { return ScadePerfCounterDefinition._counterDefinitions; }
            set { ScadePerfCounterDefinition._counterDefinitions = value; }
        }

        static ScadePerfCounterDefinition()
        {
            DefineNew(ScadePerfCounter.Security_inicializa);
            DefineNew(ScadePerfCounter.Security_getNSerie);
            DefineNew(ScadePerfCounter.Security_getRFC);
            DefineNew(ScadePerfCounter.Security_getNom);
            DefineNew(ScadePerfCounter.Security_getVigIni);
            DefineNew(ScadePerfCounter.Security_getVigFin);
            DefineNew(ScadePerfCounter.Security_solCDxNS);
            DefineNew(ScadePerfCounter.Security_solEdoCDxNS);
            DefineNew(ScadePerfCounter.Security_solNSCDFEAxRFC);
            DefineNew(ScadePerfCounter.Security_solListaCDxRFC);
            DefineNew(ScadePerfCounter.Security_generaFirma);
            DefineNew(ScadePerfCounter.Security_verFirma);
            DefineNew(ScadePerfCounter.Security_verificaLlaves);
            DefineNew(ScadePerfCounter.Security_encripta);
            DefineNew(ScadePerfCounter.Security_desencripta);
            DefineNew(ScadePerfCounter.Security_genP7);
            DefineNew(ScadePerfCounter.Security_obtieneNumSerieP7);
            DefineNew(ScadePerfCounter.Security_procP7);
            DefineNew(ScadePerfCounter.Security_conectaARA);
            DefineNew(ScadePerfCounter.Security_desconectaARA);
            DefineNew(ScadePerfCounter.Security_GetNSerie);
            DefineNew(ScadePerfCounter.Security_GetFirma);
            DefineNew(ScadePerfCounter.Security_Inicia);
            DefineNew(ScadePerfCounter.Security_Termina);
            DefineNew(ScadePerfCounter.Backend_EnvioDucto);
            DefineNew(ScadePerfCounter.Directory_ValidateUser);
        }

        private static void DefineNew(string name)
        {
            _counterDefinitions.Add(name, new ScadePerfCounterDefinition(name, "Tiempo de respuesta en milisegundos de la invocación de " + name, System.Diagnostics.PerformanceCounterType.NumberOfItems64));
        }
    }
}
