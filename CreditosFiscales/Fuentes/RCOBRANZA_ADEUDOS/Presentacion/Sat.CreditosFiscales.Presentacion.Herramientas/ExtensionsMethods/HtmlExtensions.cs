
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods.HtmlExtensions:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    /// <summary>
    /// Métodos de extensión para los HtmlHelper
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Regresa el Id html de una propiedad del modelo 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString HtmlIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string htmlFieldId = htmlFieldName.Replace('.', '_');

            return MvcHtmlString.Create(htmlFieldId);
        }

        /// <summary>
        /// Regresa el Nombre html de una propiedad del modelo 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString HtmlNameFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            return MvcHtmlString.Create(htmlFieldName);
        }
        

    }
}
