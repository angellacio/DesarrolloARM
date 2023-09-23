using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = WA_Entidades;

namespace WA_RN.Seguridad
{
    public interface IManejoSeguridad
    {
        ent.Seguridad.EntDatosAutentificacion ValidaUsuario(string sUsuario, string sPasword, Boolean bolEcripta);
        ent.Seguridad.EntDatosAutentificacion ValidaUsuario();
    }
}
