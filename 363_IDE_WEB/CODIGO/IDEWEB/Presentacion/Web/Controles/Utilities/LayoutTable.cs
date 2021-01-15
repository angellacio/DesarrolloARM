//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:LayoutTable:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{

    [SupportsEventValidation]
    internal sealed class LayoutTable : Table
    {
        // Methods
        public LayoutTable(int rows, int columns, Page page)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException("rows");
            }
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException("columns");
            }
            if (page != null)
            {
                this.Page = page;
            }
            for (int i = 0; i < rows; i++)
            {
                TableRow row = new TableRow();
                this.Rows.Add(row);
                for (int j = 0; j < columns; j++)
                {
                    TableCell cell = new LayoutTableCell();
                    row.Cells.Add(cell);
                }
            }
        }

        // Properties
        public TableCell this[int row, int column]
        {
            get
            {
                return this.Rows[row].Cells[column];
            }
        }
    }

    sealed class LayoutTableCell : TableCell
    {
        // Methods
        protected override void AddedControl(Control control, int index)
        {
            if (control.Page == null)
            {
                control.Page = this.Page;
            }
        }

        protected override void RemovedControl(Control control)
        {
        }
    }

 


}

