
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos.Catalogo:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos
{
    public static class Catalogo
    {
        public static List<ALR> obtenerCatalogoALR()
        {
            return new DalCatalogo().obtenerCatalogoALR();
        }

        public static List<Autoridad> obtenerCatalogoAutoridad()
        {
            return new DalCatalogo().obtenerCatalogoAutoridad();
        }

        //public static List<CatMarca> ObtenerCatalogoMarca()
        //{
        //    return DalCatalogo.ObtenerCatalogoMarca();
        //}

        public static ALR ObtenerAlrXCveCorta(string CveCorta)
        {
            return DalCatalogo.ObtenerAlrXCveCorta(CveCorta);
        }

        public static List<DiaInhabil> ObtenerCatalogoDiaInhabil()
        {
            DalCatalogo dalCatalogo = new DalCatalogo();
            return dalCatalogo.ObtenerCatalogoDiaInhabil();
        }

        public static List<FechaINPC> ObtenerCatalogoFechaINPC()
        {
            DalCatalogo dalCatalogo = new DalCatalogo();
            return dalCatalogo.ObtenerCatalogoFechaINPC();
        }

        public static DescripcionConceptoPeriodicidad ObtenerDescripcionConceptoPeriodicidad(int idConceptoLey,int idPeriodo, int idPeriodicidad)
        {
            return DalCatalogo.ObtenerDescripcionConceptoPeriodicidad(idConceptoLey, idPeriodo,idPeriodicidad);
        }
    }
}
