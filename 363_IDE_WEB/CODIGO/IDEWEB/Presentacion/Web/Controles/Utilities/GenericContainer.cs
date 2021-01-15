//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:GenericContainer:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
abstract class GenericContainer<ControlType> : WebControl where ControlType: WebControl
{
    // Fields
    private Table _borderTable;
    private Table _layoutTable;
    private ControlType owner;
    private bool renderDesignerRegion;

    // Methods
    public GenericContainer(ControlType owner)
    {
        this.owner = owner;
    }

    private Control FindControl<RequiredType>(string id, bool required, string errorResourceKey)
    {
        Control control = this.FindControl(id);
        if (control is RequiredType)
        {
            return control;
        }
        if (required && !this.DesignMode) //&& !this.Owner.DesignMode)
        {
            throw new HttpException("errorResourceKey " + this.owner.ID + id );
        }
        return null;
    }

    protected Control FindOptionalControl<RequiredType>(string id)
    {
        return this.FindControl<RequiredType>(id, false, null);
    }

    protected Control FindRequiredControl<RequiredType>(string id, string errorResourceKey)
    {
        return this.FindControl<RequiredType>(id, true, errorResourceKey);
    }

    public sealed override void Focus()
    {
        throw new NotSupportedException("NoFocusSupport" + base.GetType().ToString());
    }

    protected sealed override void Render(HtmlTextWriter writer)
    {
        if (this.UsingDefaultTemplate)
        {
            if (!this.ConvertingToTemplate)
            {
                this.BorderTable.CopyBaseAttributes(this);
                if (base.ControlStyleCreated)
                {
                    this.CopyBorderStyles(this.BorderTable, base.ControlStyle);
                    this.CopyStyleToInnerControl(this.LayoutTable, base.ControlStyle);
                }
            }
            //this.LayoutTable.Height = this.Height;
            //this.LayoutTable.Width = this.Width;
            this.RenderContents(writer);
        }
        else
        {
            this.RenderContentsInUnitTable(writer);
        }
    }

    internal void CopyBorderStyles(WebControl control, Style style)
    {
        if ((style != null) && !style.IsEmpty)
        {
            control.BorderStyle = style.BorderStyle;
            control.BorderColor = style.BorderColor;
            control.BorderWidth = style.BorderWidth;
            control.BackColor = style.BackColor;
            control.CssClass = style.CssClass;
        }
    }

    internal void CopyStyleToInnerControl(WebControl control, Style style)
    {
        if ((style != null) && !style.IsEmpty)
        {
            control.ForeColor = style.ForeColor;
            control.Font.CopyFrom(style.Font);
        }
    }

    private void RenderContentsInUnitTable(HtmlTextWriter writer)
    {
        LayoutTable table = new LayoutTable(1, 1, this.Page);
        if (this.RenderDesignerRegion)
        {
            table[0, 0].Attributes["_designerRegion"] = "0";
        }
        else
        {
            foreach (Control control in this.Controls)
            {
                table[0, 0].Controls.Add(control);
            }
        }
        string iD = this.Parent.ID;
        if ((iD != null) && (iD.Length != 0))
        {
            table.ID = this.Parent.ClientID;
        }
        table.CopyBaseAttributes(this);
        table.ApplyStyle(base.ControlStyle);
        table.CellPadding = 0;
        table.CellSpacing = 0;
        table.RenderControl(writer);
    }

    protected void VerifyControlNotPresent<RequiredType>(string id, string errorResourceKey)
    {
        if ((this.FindOptionalControl<RequiredType>(id) != null) && !this.DesignMode) //&& !this.Owner.DesignMode)
        {
            throw new HttpException("errorResourceKey " + this.owner.ID + id);
        }
    }

    // Properties
    internal Table BorderTable
    {
        get
        {
            return this._borderTable;
        }
        set
        {
            this._borderTable = value;
        }
    }

    protected abstract bool ConvertingToTemplate { get; }

    internal Table LayoutTable
    {
        get
        {
            return this._layoutTable;
        }
        set
        {
            this._layoutTable = value;
        }
    }

    internal ControlType Owner
    {
        get
        {
            return this.owner;
        }
    }

    internal bool RenderDesignerRegion
    {
        get
        {
            return (base.DesignMode && this.renderDesignerRegion);
        }
        set
        {
            this.renderDesignerRegion = value;
        }
    }

    private bool UsingDefaultTemplate
    {
        get
        {
            return (this.BorderTable != null);
        }
    }
}

}
