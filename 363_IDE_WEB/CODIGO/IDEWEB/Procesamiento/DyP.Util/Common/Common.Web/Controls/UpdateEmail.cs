//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Web.Controls:UpdateEmail:0:21/May/2008[SAT.DyP.Util.Web:1.0:21/May/2008])
using System;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Diagnostics;
using System.Web.Security;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using SAT.DyP.Util.Logging;
using System.Text.RegularExpressions;
using SAT.DyP.Presentacion.Seguridad.Membresia;

namespace SAT.DyP.Util.Web.Controls
{
  [DefaultProperty("Text")]
  [ToolboxData("<{0}:UpdateEmail runat=server></{0}:UpdateEmail>")]
  public class UpdateEmail : WebControl
  {
    [Bindable(false)]
    [Category("Appearance")]
    [DefaultValue("Invalid email")]
    [Localizable(true)]
    public string EmailValidationErrorText
    {
      get
      {
        return GetFromViewState("MsgErrorEmail");
      }

      set
      {
        ViewState["MsgErrorEmail"] = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("User ID")]
    [Localizable(true)]
    public string UserID
    {
      get
      {
        return GetFromViewState("UserID");
      }

      set
      {
        ViewState["UserID"] = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("User Fullname")]
    [Localizable(true)]
    public string UserFullName
    {
      get
      {
        return GetFromViewState("UserFullName");
      }

      set
      {
        ViewState["UserFullName"] = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("Correo Electrónico")]
    [Localizable(true)]
    public string EmailLabelText
    {
      get
      {
        return GetFromViewState("EmailLabelText");
      }

      set
      {
        ViewState["EmailLabelText"] = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("RFC")]
    [Localizable(true)]
    public string UserLabelText
    {
      get
      {
        return GetFromViewState("UserLabelText");
      }

      set
      {
        ViewState["UserLabelText"] = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("username@server.com")]
    [Localizable(true)]
    public string DefaultEmailValue
    {
      get
      {
        return GetFromViewState("DefaultEmailValue");
      }

      set
      {
        ViewState["DefaultEmailValue"] = value;
      }
    }

    /// <summary>
    /// Devuelve el dato enviado por la forma
    /// </summary>
    public string PostedEmailValue
    {
      get { return Page.Request.Form["UsrEmail"]; }
    }

    private string GetFromViewState(string key)
    {
      String s = (String)ViewState[key];
      return ((s == null) ? String.Empty : s);
    }

    private bool CheckEmail(string email)
    {
      string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
      Regex regexEmail = new Regex(pattern);

      return regexEmail.IsMatch(email);
    }

    private string CreateJsValidator()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("<script type='text/javascript'>");

      string content = @"
function set_error_message(content){   
      var _msg=document.getElementById('updateEmailValidatorMsg');
      _msg.innerHTML='<p><strong><font color=red>' + content + '</font></strong></p>';
}";
      sb.AppendLine(content);
      sb.AppendLine("</script>");

      return sb.ToString();
    }

    /// <summary>
    /// Valida el contenido del correo electrónico
    /// </summary>
    /// <returns></returns>
    public bool ValidateContent()
    {
      bool result = CheckEmail(this.PostedEmailValue);
      if (!result)
      {
        ShowMessageText(this.EmailValidationErrorText);
      }
      return result;
    }

    /// <summary>
    /// Permite mostrar un mensaje de texto
    /// </summary>
    /// <param name="text"></param>
    public void ShowMessageText(string text)
    {
      Page.ClientScript.RegisterStartupScript(typeof(string), "MSG_ERROR", "<script>set_error_message('" + text + "');</script>");
    }

    /// <summary>
    /// Actualiza la información del email usando el membership
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
      bool result = false;
      try
      {
        IDCMembershipUser userInfo = (IDCMembershipUser)Membership.GetUser(this.UserID);
        userInfo.Email = this.PostedEmailValue;
        Membership.UpdateUser(userInfo);
        result = true;
      }
      catch (Exception ex)
      {
        throw new SAT.DyP.Util.Types.PlatformException(ex.Message, ex);
      }
      return result;
    }

    protected override void OnPreRender(EventArgs e)
    {
      if (!Page.ClientScript.IsClientScriptBlockRegistered("UPDATER_EMAIL_CONTROL"))
      {
        Page.ClientScript.RegisterClientScriptBlock(typeof(string), "UPDATER_EMAIL_CONTROL", CreateJsValidator());
      }

      base.OnPreRender(e);
    }



    protected override void RenderContents(HtmlTextWriter output)
    {
      try
      {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td align='center'><strong>" + UserLabelText + "&nbsp;:&nbsp;" + this.UserID + "</strong><td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td>&nbsp;</td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td align='center'>" + "<strong>" + this.UserFullName + "</strong>" + "</td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td>&nbsp;</td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td>" + "<strong>" + EmailLabelText + "</strong>&nbsp;<input type='text' id='UsrEmail' name='UsrEmail' width='350' value='" + this.DefaultEmailValue + "'>" + "<font color='red'>(*)</font></td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td>&nbsp;</td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td><div style='width:100%;' id='updateEmailValidatorMsg' name='updateEmailValidatorMsg'></div></td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("</table>");

        output.Write(sb.ToString());
      }
      catch (Exception ex)
      {
        EventLogHelper.WriteEntry(ex.StackTrace, EventLogEntryType.Warning, CoreLogEventIdentifier.PROCESSOR_MAIL_EVENT_ID);
        output.Write("<h1>Error:" + ex.Message + "</h1>");
        //output.Write("<h1>Error:" + ex.Message + "<br>" + ex.StackTrace + "</h1>");
      }
    }
  }
}

