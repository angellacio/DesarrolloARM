using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_Entidades.Empleados
{
    public class EntEmpleado
    {
        public EntEmpleado() 
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
        }
        public EntEmpleado(DataRow DRow) 
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
        }
        public int? IdEmpleado { get; set; }
        public int? IdArea { get; set; }
        public int? IdAplicativo { get; set; }
        public string Empelado { get; set; }
        public string Apellido_Uno { get; set; }
        public string Apellido_Dos { get; set; }
        public string Usuario { get; set; }
        public int? Orden { get; set; }
        public Boolean? Root { get; set; }
        public Boolean? Estado { get; set; }
    }
}
