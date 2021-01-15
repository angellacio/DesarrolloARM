//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:HeaderContainer:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    internal sealed class HeaderContainer: WebControl 
    {
        private Image centerImage;
        private Image leftImage;
        private Image rigthImage;
        private Label titleLabel;

        public Label TitleLabel
        {
            get { return titleLabel; }
            set { titleLabel = value; }
        }
        
        public Image RighImage
        {
            get { return rigthImage; }
            set { rigthImage = value; }
        }

        public Image LeftImage
        {
            get { return leftImage; }
            set { leftImage = value; }
        }

        public Image CenterImage
        {
            get { return centerImage; }
            set { centerImage = value; }
        }
        
    }
}
