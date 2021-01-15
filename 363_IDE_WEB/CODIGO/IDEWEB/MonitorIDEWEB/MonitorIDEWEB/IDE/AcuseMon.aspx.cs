using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Shared_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["usuario"] == null)
        {

            Response.Redirect("Login.aspx");

        }
        int Folio = int.Parse(Request["Folio"]);
        ServiceMonitor.ServicioMonitorClient SC = new ServiceMonitor.ServicioMonitorClient();
        try
        {
            int cont = 0;
            String et1 = Server.HtmlDecode(SC.TraerAcuse(Folio).Replace("  ", "").Replace("\n", ""));
            string[] separadores = et1.Split('<');
            //LEYENDO CADENA QUITANDO ESPACIOS Y SALTOS DE LINEA, SEPARANDOLO POR CADA QUE LEA "<"//

            foreach (var word in separadores)
            {
                //txt1.Text = txt1.Text+word.ToString()+" "+Busca(word.ToString(), "h4>SERVICIO DE ADMINISTRACIÓN TRIBUTARIA") + " En la posicion " + i +"\n";

                if (SearchText(word.ToString(), "h4>SERVICIO DE ADMINISTRACIÓN TRIBUTARIA"))//BUSCANDO CADENA
                {
                    //LabelTitulo.Text = word.ToString().Substring(3);
                    Label1.Text = word.ToString().Replace("SERVICIO DE ADMINISTRACIÓN TRIBUTARIA", " ");//ELIMINANDO CADENA DEL HTML 
                }
                else
                    if (SearchText(word.ToString(), "h4>ACUSE DE ACEPTACIÓN") || SearchText(word.ToString(), "h4>NOTIFICACIÓN DE RECHAZO")) //BUSCANDO CADENAS EN EL TEXTO HTML
                    {
                        LabelNotificacion.Text = word.ToString().Substring(3);                  //AÑADIENDO CADENA COMO SUBTITUTLO DE LA PAGINA

                        if (SearchText(word.ToString(), "h4>ACUSE DE ACEPTACIÓN"))
                        {
                            Label1.Text = word.ToString().Replace("h4>ACUSE DE ACEPTACIÓN", " ");           //ELIMINANDO CADENA DEL HTML
                        }
                        else
                        {
                            Label1.Text = word.ToString().Replace("h4>NOTIFICACIÓN DE RECHAZO", " ");       //ELIMINANDO CADENA DEL HTML
                        }
                    }
                    else
                        if (SearchText(word.ToString(), "h3>DECLARACIÓN INFORMATIVA ANUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTIVO") || SearchText(word.ToString(), "h3>DECLARACIÓN INFORMATIVA MENSUAL A LOS DEPOSITOS EN EFECTIVO") || SearchText(word.ToString(), "h3>DECLARACIÓN INFORMATIVA MENSUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTIVO") || SearchText(word.ToString(), "h3>DECLARACIÓN INFORMATIVA ANUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO")) //BUSCANDO CADENAS EN EL TEXTO HTML
                        {
                            LabelTitulo.Text = word.ToString().Substring(3);        //AÑADIENDO CADENA DE TEXTO EN EL TITULO DE LA PAGINA
                        }
                        else
                            if (SearchText(word.ToString(), "b>ACUSE DE RECIBO DE ACEPTACIÓN DE LA DECLARACIÓN INFORMATIVA ANUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTIVO") || SearchText(word.ToString(), "b>ACUSE DE RECIBO DE ACEPTACIÓN DE LA DECLARACIÓN INFORMATIVA ANUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO"))
                            {
                                Label1.Text = word.ToString().Replace(word.ToString(), ""); //ELIMINANDO CADENA DEL TEXTO HTML
                            }
                            else
                                if (SearchText(word.ToString(), "hr />"))       //BUSCANDO ETIQUETA HR QUE PRESENTA LAS LINEAS EN LA PAGINA Y EN EL PDF SON LAS LINEAS NEGRAS
                                {
                                    cont++;
                                    if (cont == 1 || cont == 3 && LabelNotificacion.Text.Equals("NOTIFICACIÓN DE RECHAZO"))
                                    {
                                        Label1.Text += word.ToString().Replace("hr />", "");
                                    }
                                    else { Label1.Text += "<" + word.ToString(); }
                                }
                                else { Label1.Text = Label1.Text + "<" + word.ToString(); }
            }//TERMINA FOREACH 
        }
        catch (NullReferenceException se)
        {
            if (se != null)
            {
                LabelTitulo.Text = "En proceso de Validacion.";
                LabelNotificacion.Text = "Folio: " + Folio;
                Label1.Text = "Su Declaracion con el Folio: " + Folio + " Ha sido recibida y se encuentra en proceso de validacion.";
            }
        }
        catch (Exception exc) { Label1.Text += exc.ToString(); }
    }
    //##### METODO QUE RETORNA UN BOLEANO SI EXISTE LA PALABRA########//
    private bool SearchText(String cad, String busqueda)
    {
        bool existe = false;
        String busca = busqueda;
        String str = cad;

        if (str.Equals(busca))
        {
            existe = true;
        }
        else { existe = false; }

        return existe;
    }

    protected void satSesionLoginStatus_LoggedOut(object sender, EventArgs e)
    {
        Session["usuario"] = null;
        Response.Redirect("~/IDE/Login.aspx");
        Session["usuario"] = null;
    }

}