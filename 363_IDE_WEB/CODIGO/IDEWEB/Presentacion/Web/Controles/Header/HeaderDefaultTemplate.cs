//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:HeaderDefaultTemplate:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public class HeaderDefaultTemplate:ITemplate
    {
        public bool modoFiel;        
        private string imageUrlLeft;
        private string imageUrlCenter;
        private string imageUrlRight;
        private Table container;        
        private MedioPresentacion medioPresentacion;

        public string ImageUrlRight
        {
            get { return imageUrlRight; }
            set { imageUrlRight = value; }
        }
        
        public string ImageUrlCenter
        {
            get { return imageUrlCenter; }
            set { imageUrlCenter = value; }
        }

        public string ImageUrlLeft
        {
            get { return imageUrlLeft; }
            set { imageUrlLeft = value; }
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
        
        public HeaderDefaultTemplate()
        {
            this.container = new Table();
        }

        void CreateControls(HeaderContainer headerContainer)
        {
            headerContainer.LeftImage = new Image();
            headerContainer.LeftImage.ID = Utility.ID.LeftHeaderImage;
            headerContainer.LeftImage.Width = new Unit(155,UnitType.Pixel);
            headerContainer.LeftImage.Height = new Unit(68,UnitType.Pixel);
            //headerContainer.LeftImage.ImageUrl = this.ImageUrlLeft;
            headerContainer.LeftImage.ImageUrl = "~/App_Themes/Default/Images/ImageSat.jpg";

            headerContainer.CenterImage = new Image();
            headerContainer.CenterImage.ID = Utility.ID.CenterHeaderImage;
            //headerContainer.CenterImage.ImageUrl = this.ImageUrlCenter;
            headerContainer.CenterImage.ImageUrl = "~/App_Themes/Default/Images/HeaderHomolagado.jpg";

            headerContainer.RighImage = new Image();
            headerContainer.RighImage.ID = Utility.ID.RightHeaderImage;
            headerContainer.RighImage.Width = new Unit(80, UnitType.Pixel);
            headerContainer.RighImage.Height = new Unit(98, UnitType.Pixel);
            //headerContainer.RighImage.ImageUrl = this.ImageUrlRight;
            headerContainer.RighImage.ImageUrl = "~/App_Themes/Default/Images/ImageScade.jpg";

            headerContainer.TitleLabel = new Label();
            headerContainer.TitleLabel.ID = Utility.ID.TitleHeaderLabel;
            headerContainer.TitleLabel.Text = Utility.Text.Title;            
            headerContainer.TitleLabel.Font.Name = Utility.Style.font_name;
            headerContainer.TitleLabel.Font.Bold = true;
            headerContainer.TitleLabel.Font.Size = new FontUnit(2, UnitType.Ex);            
        }

        private void LayoutControls(HeaderContainer headerContainer)
        {

            if (this.MedioPresentacion == MedioPresentacion.Internet && !this.ModoFiel)
            {
                this.LayoutInternet(headerContainer);
            }
            else if (this.MedioPresentacion != MedioPresentacion.Internet && !this.ModoFiel)
            {
                this.LayoutModulo(headerContainer);                
            }
            else if(this.ModoFiel)
            {
                this.LayoutInternetWithOutTitle(headerContainer);
            }            
            else            
            {
                this.LayoutInternetWithOutTitle(headerContainer);   
            }                                    
        }
        /// <summary>
        /// Muestra el encabezado con una sola imagen al centro y sin titulo.
        /// </summary>
        /// <param name="headerContainer">Contenedor de controles.</param>
        private void LayoutInternetWithOutTitle(HeaderContainer headerContainer)
        {            
            TableCell imageCell = new TableCell();
            imageCell.ColumnSpan = 1;
            imageCell.HorizontalAlign = HorizontalAlign.Center;            
            imageCell.Controls.Add(headerContainer.CenterImage);
            TableRow headerImageRow = new TableRow();
            headerImageRow.Cells.Add(imageCell);            

            this.container.ID = Utility.ID.Container;
            this.container.Width = new Unit(100, UnitType.Percentage);
            this.container.Rows.Add(headerImageRow);            
            headerContainer.Controls.Add(this.container);
        }

        /// <summary>
        /// Muestra el encabezado con una sola imagen y al centro un titulo.
        /// </summary>
        /// <param name="headerContainer">Contenedor de controles.</param>
        private void LayoutInternet(HeaderContainer headerContainer)
        {
            TableCell titleCell = new TableCell();
            titleCell.ColumnSpan = 1;
            titleCell.HorizontalAlign = HorizontalAlign.Center; ;
            titleCell.Controls.Add(headerContainer.CenterImage);
            TableRow headerImageRow = new TableRow();
            headerImageRow.Cells.Add(titleCell);

            TableCell imageCell = new TableCell();
            imageCell.ColumnSpan = 1;
            imageCell.HorizontalAlign = HorizontalAlign.Center;
            imageCell.Controls.Add(new LiteralControl("<br/>"));
            imageCell.Controls.Add(headerContainer.TitleLabel);
            TableRow headerTitleRow = new TableRow();
            headerTitleRow.Cells.Add(imageCell);

            this.container.ID = Utility.ID.Container;
            this.container.Width = new Unit(100, UnitType.Percentage);
            this.container.Rows.Add(headerImageRow);
            this.container.Rows.Add(headerTitleRow);

            headerContainer.Controls.Add(this.container);
        }

        /// <summary>
        /// Muestra el encabezado con una imagen a la izquierda y otra a la derecha con el titulo al centro.
        /// </summary>
        /// <param name="headerContainer">Contenedor de controles.</param>
        private void LayoutModulo(HeaderContainer headerContainer)
        {
            TableCell leftImageCell = new TableCell();
            leftImageCell.ColumnSpan = 1;
            leftImageCell.Width = new Unit(15, UnitType.Percentage);
            leftImageCell.HorizontalAlign = HorizontalAlign.Left;
            leftImageCell.Controls.Add(headerContainer.LeftImage);

            TableCell titleCell = new TableCell();
            titleCell.ColumnSpan = 1;
            titleCell.Width = new Unit(70, UnitType.Percentage);
            titleCell.HorizontalAlign = HorizontalAlign.Center;
            titleCell.Controls.Add(headerContainer.TitleLabel);

            TableCell rightImageCell = new TableCell();
            rightImageCell.ColumnSpan = 1;
            rightImageCell.Width = new Unit(15, UnitType.Percentage);
            rightImageCell.HorizontalAlign = HorizontalAlign.Right;
            rightImageCell.Controls.Add(headerContainer.RighImage);

            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(leftImageCell);
            headerRow.Cells.Add(titleCell);
            headerRow.Cells.Add(rightImageCell);

            TableCell spaceCell = new TableCell();
            spaceCell.ColumnSpan = 3;
            spaceCell.HorizontalAlign = HorizontalAlign.Center;
            spaceCell.Controls.Add(new LiteralControl("<br/><br/>"));
            TableRow headerSpaceRow = new TableRow();
            headerSpaceRow.Cells.Add(spaceCell);

            this.container.ID = Utility.ID.Container;
            this.container.Width = new Unit(100, UnitType.Percentage);
            this.container.Rows.Add(headerRow);
            this.container.Rows.Add(headerSpaceRow);

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
