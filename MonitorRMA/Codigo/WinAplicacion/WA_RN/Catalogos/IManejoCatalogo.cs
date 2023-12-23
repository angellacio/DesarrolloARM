using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent = WA_Entidades;

namespace WA_RN.Catalogos
{
    public interface IManejoCatalogo
    {
        List<Ent.EntCatalogo> Consulta_PR_Cat_Aplicativos();
        List<Ent.EntCatalogo> Consulta_PR_Cat_EstadosRMA();
        List<Ent.EntCatalogo> Consulta_PR_Cat_TipoRequerimiento();
    }
}
