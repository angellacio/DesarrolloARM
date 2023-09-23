﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA_Entidades.Empleados;

namespace WA_Entidades.Seguridad
{
    public class EntDatosAutentificacion : EntEmpleado
    {
        public EntDatosAutentificacion()
        {
            IdEmpleado = null;
            IdArea = null;
            IdAplicativo = null;
            Empelado = "";
            Apellido_Uno = "";
            Apellido_Dos = "";
            Usuario = "";
            Orden = null;
            Root = null;
            Estado = null;
            Areas = new List<EntUbicacion>();
            Aplicativos = new List<EntUbicacion>();
        }

        public EntDatosAutentificacion(DataRow DRow)
        {
            IdEmpleado = null;
            IdArea = null;
            IdAplicativo = null;
            Empelado = "";
            Apellido_Uno = "";
            Apellido_Dos = "";
            Usuario = "";
            Orden = null;
            Root = null;
            Estado = null;

            if (DRow["nIdEmpleado"] != DBNull.Value) IdEmpleado = int.Parse(DRow["nIdEmpleado"].ToString());
            if (DRow["nIdArea"] != DBNull.Value) IdArea = int.Parse(DRow["nIdArea"].ToString());
            if (DRow["nIdAplicativo"] != DBNull.Value) IdAplicativo = int.Parse(DRow["nIdAplicativo"].ToString());
            Empelado = DRow["sNombre"].ToString().Trim();
            Apellido_Uno = DRow["sApellido1"].ToString().Trim();
            Apellido_Dos = DRow["sApellido2"].ToString().Trim();
            Usuario = DRow["sUsuario"].ToString().Trim();
            if (DRow["Orden"] != DBNull.Value) Orden = int.Parse(DRow["Orden"].ToString());
            if (DRow["Root"] != DBNull.Value) Root = Boolean.Parse(DRow["Root"].ToString());
            if (DRow["Estado"] != DBNull.Value) Estado = Boolean.Parse(DRow["Estado"].ToString());

            Areas = new List<EntUbicacion>();
            Aplicativos = new List<EntUbicacion>();
        }

        public List<EntUbicacion> Areas { get; set; }
        public List<EntUbicacion> Aplicativos { get; set; }
    }
}