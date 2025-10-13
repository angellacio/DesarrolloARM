using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;

namespace SAT.CreditosFiscales.Motor.Entidades.ReglasNet
{
    public class ReglaBaseNet
    {        
        public virtual string NombreClase { get { return this.GetType().AssemblyQualifiedName; } }
        
        public virtual RespuestaGenerica Ejecuta(XmlDocument documento)
        {
            return new RespuestaGenerica();
        }

        public virtual RespuestaGenerica EjecutaRegla(XmlDocument documento)
        {
            RespuestaGenerica respuesta = new RespuestaGenerica();

            try
            {
                respuesta = Ejecuta(documento);                
            }
            catch (Exception ex)
            {
                respuesta.AgregaError(Enumeraciones.EnumErrores.EjecucionReglaNet, ex, this.NombreClase);
            }

            return respuesta;
        }
    }
}
