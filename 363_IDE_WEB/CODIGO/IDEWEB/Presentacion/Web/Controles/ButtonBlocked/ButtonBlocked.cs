using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing.Design;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using Sat.Scade.Net.IDE.Presentacion.Web;

[assembly: System.Web.UI.WebResource(Utility.Resource.RecursosBloqueo, Utility.mimeType.javascript)]
[assembly: System.Web.UI.WebResource(Utility.Resource.ButtonSending, Utility.mimeType.imagegif)]
[assembly: System.Web.UI.WebResource(Utility.Resource.RecursosValidacion, Utility.mimeType.javascript)]

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    [System.Drawing.ToolboxBitmap(typeof(resfinder), Utility.Resource.ButtonSending)]    
    [Themeable(true), ToolboxData(Utility.ToolBoxData.ButtonBlocked)]
    public class ButtonBlocked:Button
    {
        [Category("Utility.Category.EDS"), Description("Url de la imagen que sera mostrada cuando el archivo se esta enviando."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ImagenEnviando
        {
            get
            {
                String s = (String)ViewState["imagenEnviando"];
                return ((s == null) ? Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.ButtonSending): s);
            }

            set
            {
                ViewState["imagenEnviando"] = value;
            }
        }

        public string MensajeEnviando
        {
            get
            {
                String s = (String)ViewState["mensajeEnviando"];
                return ((s == null) ? "Su declaraci&oacute;n esta en proceso de recepci&oacute;n en el <u>SAT</u>, por favor espere un momento." : s);
            }

            set
            {
                ViewState["mensajeEnviando"] = value;
            }
        }

        public bool HabilitarBloqueo
        {
            get
            {
                bool result;
                String s = (String)ViewState["HabilitarBloqueo"];
                if (s == null)
                    return true;
                bool.TryParse(s, out result);
                return result;                
            }

            set
            {
                ViewState["HabilitarBloqueo"] = value;
            }
        }        

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string script = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.RecursosBloqueo);
            this.Page.ClientScript.RegisterClientScriptInclude(Utility.Resource.ScriptBloqueo, script);
            string scriptValidacion = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.RecursosValidacion); 
            this.Page.ClientScript.RegisterClientScriptInclude(Utility.Resource.RecursosValidacion, scriptValidacion);

            if (HabilitarBloqueo)
            {
                this.OnClientClick = String.Format("javascript:validaStrArchivo();return Enviar('{0}', '{1}')", Mensajes.RutaRequerida, Mensajes.RutaInvalida);
            }
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderContents(writer);


            writer.WriteLine();
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
                writer.RenderEndTag();                    

                    writer.AddAttribute(HtmlTextWriterAttribute.Align, Sat.Scade.Net.IDE.Presentacion.Web.Utility.Style.center);
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "LyrUploading");
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);                
            
                    writer.RenderBeginTag(HtmlTextWriterTag.Strong);
                    writer.Write(this.MensajeEnviando);        
                    writer.RenderEndTag();

                    writer.RenderBeginTag(HtmlTextWriterTag.Br);
                    writer.RenderEndTag();                    

                        writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagenEnviando);
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);                    
                    writer.RenderEndTag();
                writer.RenderEndTag();
            
        }
    }
}
