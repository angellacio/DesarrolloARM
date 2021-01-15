//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:HeaderNewTemplate:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public class HeaderNewTemplate : ITemplate
    {
        public bool modoFiel;        
        private string imageUrlCenter;
        private Table container;        
        private MedioPresentacion medioPresentacion;

        public string ImageUrlCenter
        {
            get { return imageUrlCenter; }
            set { imageUrlCenter = value; }
        }

        public MedioPresentacion MedioPresentacion
        {
            get { return medioPresentacion; }
            set { medioPresentacion = value; }
        }

        public bool ModoFiel
        {
            get { return modoFiel; }
            set { modoFiel = value; }
        }

        public HeaderNewTemplate()
        {
            this.container = new Table();
        }

        void CreateControls(HeaderContainer headerContainer)
        {
            headerContainer.LeftImage = new Image();
            headerContainer.LeftImage.ID = Utility.ID.LeftHeaderImage;

            headerContainer.CenterImage = new Image();
            headerContainer.CenterImage.ID = Utility.ID.CenterHeaderImage;
            headerContainer.CenterImage.ImageUrl = this.ImageUrlCenter;
            headerContainer.CenterImage.Width = new Unit(800,UnitType.Pixel);

            headerContainer.RighImage = new Image();
            headerContainer.RighImage.ID = Utility.ID.RightHeaderImage;

            headerContainer.TitleLabel = new Label();
            headerContainer.TitleLabel.ID = Utility.ID.TitleHeaderLabel;
            headerContainer.TitleLabel.Text = Utility.Text.Title;
            headerContainer.TitleLabel.ForeColor = System.Drawing.Color.White;
            headerContainer.TitleLabel.Font.Size = new FontUnit(3, UnitType.Ex);

        }

        private void LayoutControls(HeaderContainer headerContainer)
        {
            TableCell titleCell1 = new TableCell();
            titleCell1.ColumnSpan = 1;

            TableCell titleCell = new TableCell();
            titleCell.ColumnSpan = 2;
            titleCell.Width = new Unit(66, UnitType.Percentage);
            titleCell.HorizontalAlign = HorizontalAlign.Left;
            titleCell.Controls.Add(headerContainer.TitleLabel);
            TableRow headerImageRow = new TableRow();
            headerImageRow.Cells.Add(titleCell1);
            headerImageRow.Cells.Add(titleCell);                       

            this.container.ID = Utility.ID.Container;
            this.container.Width = new Unit(800, UnitType.Pixel);
            this.container.Height = new Unit(130, UnitType.Pixel);
            this.container.Rows.Add(headerImageRow);            
            this.container.BackImageUrl = headerContainer.CenterImage.ImageUrl;
            
            headerContainer.Controls.Add(this.container);
        }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            HeaderContainer headerContainer = (HeaderContainer)container;
            this.CreateControls(headerContainer);
            this.LayoutControls(headerContainer);
        }

        #endregion
    }
}
