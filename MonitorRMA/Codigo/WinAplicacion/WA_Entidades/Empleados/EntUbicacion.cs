using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilEnumText = UtileriasComunes.ManejoEnumTextos.Empelados;

namespace WA_Entidades.Empleados
{
    public class EntUbicacion : EntCatalogo
    {
        public EntUbicacion(utilEnumText.TC_Ubicacion tipoCatalogo)
        {
            TipoCatalogo = tipoCatalogo;
            CatPapa = null;
            IdTabla = null;
            Tabla = null;
            EstadoTabla = null;
            IdCatalogo = null;
            Acronimo = null;
            Orden = null;
            Observaciones = null;
            Catalogo = null;
            EstadaCatalogo = null;
            Seleccion = false;
        }

        public EntUbicacion(utilEnumText.TC_Ubicacion tipoCatalogo, DataRow DRow, int? DatoDefault)
        {
            TipoCatalogo = tipoCatalogo;
            CatPapa = null;
            IdTabla = null;
            Tabla = null;
            EstadoTabla = null;
            IdCatalogo = null;
            Acronimo = null;
            Orden = null;
            Observaciones = null;
            Catalogo = null;
            EstadaCatalogo = null;
            Seleccion = false;

            if (DRow["nIdArea"] != DBNull.Value)  CatPapa = int.Parse(DRow["nIdArea"].ToString());
            if (DRow["nIdTabla"] != DBNull.Value) IdTabla = int.Parse(DRow["nIdTabla"].ToString());
            Tabla = DRow["Tabla"].ToString().Trim();
            if (DRow["Est_Tabla"] != DBNull.Value) EstadoTabla = Boolean.Parse(DRow["Est_Tabla"].ToString());
            if (DRow["nIdTablaDetalle"] != DBNull.Value) IdCatalogo = int.Parse(DRow["nIdTablaDetalle"].ToString());
            Acronimo = DRow["Acronimo"].ToString().Trim();
            if (DRow["Orden"] != DBNull.Value) Orden = int.Parse(DRow["Orden"].ToString());
            Observaciones = DRow["Observaciones"].ToString().Trim();
            Catalogo = DRow["TablaDetalle"].ToString().Trim();
            if (DRow["Est_Detalle"] != DBNull.Value) EstadaCatalogo = Boolean.Parse(DRow["Est_Detalle"].ToString());

            if (DatoDefault == null) Seleccion = true;
            else Seleccion = IdCatalogo == DatoDefault.Value; 
        }

        public int? CatPapa { get; set; }
        public utilEnumText.TC_Ubicacion TipoCatalogo { get; set; }
        public Boolean Seleccion { get; set; }
    }
}
