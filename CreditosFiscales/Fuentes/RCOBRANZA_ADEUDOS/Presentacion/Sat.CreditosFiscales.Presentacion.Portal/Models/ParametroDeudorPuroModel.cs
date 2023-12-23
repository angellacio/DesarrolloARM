
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.Models:Sat.CreditosFiscales.Presentacion.Portal.Models.ParametroDeudorPuroModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.Models
{
    public class ParametroDeudorPuroModel
    {
        // SARR 25/mar/2015 [DisplayName("Registro federal de contribuyentes")]
        [DisplayName("Registro federal de contribuyentes (opcional)")]
        // SARR 25/mar/2015 [Required(ErrorMessage = "El registro federal de contribuyentes es requerido.")]
        // SARR 25/mar/2015 [Required( AllowEmptyStrings = true)]
        public string Rfc { get; set; }
        
        [DisplayName("Número de resolución")]
        [Required(ErrorMessage = "El número de resolución es requerido.")]
        public string NumeroDocumento { get; set; }
       
        [DisplayName("Autoridad determinante")]
        [Required(ErrorMessage="La autoridad determinante es requerida.")]        
        public string IdAutoridad { get; set; }

        [DisplayName("ADR que controla el adeudo")]
        [Required(ErrorMessage="La ADR que controla el adeudo es requerida.")]        
        public string IdAlr { get; set; }

        [DisplayName("Ingrese los valores que aparecen en la imagen")]
        [Required(ErrorMessage="Los valores que aparacen en la imagen son requeridos.")]
        public string Captcha { get; set; }
        
        [DisplayName("Fecha de resolución determinante")]
        [Required(ErrorMessage="La fecha de resolución determinante es requerida.")]
        public string Fecha { get; set; }        
    }
}