using Personal.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using TagClass.ID3.ID3v2F;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using entC = Personal.Entidades;
using rnC = Personal.ReglaNegocio.Canciones;

namespace ManejoCanciones.uControl
{
    public partial class cDatoCancion : UserControl
    {
        private configAplicacion infAplica { get; set; }
        private List<entC.Canciones.entCancion> lstCanMod { get; set; }

        private ManejoCanciones.FrmCanciones frmPadre
        {
            get
            {
                ManejoCanciones.FrmCanciones frmResult = null;

                if (this.Parent != null) frmResult = this.Parent.Parent.Parent.Parent.Parent as ManejoCanciones.FrmCanciones;

                return frmResult;
            }
        }

        public string rutaCancion
        {
            get { return txtRuta.Text.Trim(); }
            set
            {
                txtRuta.Text = string.Empty;
                if (value == null) { txtRuta.Text = "<null>"; } else { txtRuta.Text = value.ToString().Trim(); }
            }
        }
        public Nullable<int> Mod_numeroPista
        {
            get
            {
                Nullable<int> valor = null;
                if (txtPista.Text.Trim() != "" && txtPista.Text != "<null>") valor = int.Parse(txtPista.Text.Trim());
                return valor;
            }
            set
            {
                txtPista.Text = string.Empty;
                if (value == null) { txtPista.Text = "<null>"; } else { txtPista.Text = value.ToString().Trim(); }
            }
        }
        public Nullable<int> Mod_Año
        {
            get
            {
                Nullable<int> result = null;
                if (txtAño.Text.Trim() != "<null>") result = int.Parse(txtAño.Text.Trim());
                return result;
            }
            set
            {
                txtAño.Text = string.Empty;
                if (value == null) { txtAño.Text = "<null>"; } else { txtAño.Text = value.ToString().Trim(); }
            }
        }
        public Nullable<int> Mod_idGenero
        {
            get { return int.Parse((cmbGenero.SelectedItem as dynamic).Key.ToString().Trim()); }
            set
            {
                if (value == null) { cmbGenero.SelectedIndex = 0; }
                else
                {
                    int i = 0;
                    for (i = 0; i <= cmbGenero.Items.Count - 1; i++)
                    {
                        if ((cmbGenero.Items[i] as dynamic).Key == value.ToString()) break;
                    }
                    cmbGenero.SelectedIndex = i;
                }
            }
        }
        public string Mod_Genero
        {
            get
            {
                string sResult = "";
                if (infAplica.lstGeneros != null)
                {
                    if (infAplica.lstGeneros.Find(item => item.nId == Mod_idGenero) != null) sResult = infAplica.lstGeneros.Find(item => item.nId == Mod_idGenero).sAcronimo.Trim();
                }
                else sResult = (cmbGenero.SelectedItem as dynamic).Value.Trim();

                return sResult;
            }
        }
        public string Mod_Nombre
        {
            get { return txtNombre.Text.Trim(); }
            set
            {
                txtNombre.Text = string.Empty;
                if (value == null) { txtNombre.Text = "<null>"; } else { txtNombre.Text = value.ToString().Trim(); }
            }
        }
        public string Mod_Artista
        {
            get { return txtArtista.Text.Trim(); }
            set
            {
                txtArtista.Text = string.Empty;
                if (value == null) { txtArtista.Text = "<null>"; } else { txtArtista.Text = value.ToString().Trim(); }
            }
        }
        public string Mod_Album
        {
            get { return txtAlbum.Text.Trim(); }
            set
            {
                txtAlbum.Text = string.Empty;
                if (value == null) { txtAlbum.Text = "<null>"; } else { txtAlbum.Text = value.ToString().Trim(); }
            }
        }
        public string Mod_Comentario
        {
            get { return txtComentario.Text.Trim(); }
            set
            {
                txtComentario.Text = string.Empty;
                if (value == null) { txtComentario.Text = "<null>"; } else { txtComentario.Text = value.ToString().Trim(); }
            }
        }

        private void LlenaComboGenero()
        {
            Dictionary<string, string> lstCatM = new Dictionary<string, string>();
            lstCatM.Add("-1", "No modificar.");

            infAplica.lstGeneros.ForEach(itemC =>
            {
                if (itemC.bEstado)
                    lstCatM.Add(itemC.nId.ToString().Trim(), string.Format("{0} - {1}", itemC.nId, itemC.sDescripcion.Trim()));
            });

            cmbGenero.DataSource = new BindingSource(lstCatM, null);
            cmbGenero.DisplayMember = "Value";
            cmbGenero.ValueMember = "Key";

            //cmbID3v1_Genero.DataSource = new BindingSource(lstCatM, null);
            //cmbID3v1_Genero.DisplayMember = "Value";
            //cmbID3v1_Genero.ValueMember = "Key";

            //cmbID3v2_Genero.DataSource = new BindingSource(lstCatM, null);
            //cmbID3v2_Genero.DisplayMember = "Value";
            //cmbID3v2_Genero.ValueMember = "Key";

            //cmbID3v2_Genero.Items.Clear();
            //foreach (var item in lstCatM)
            //{
            //    cmbID3v2_Genero.Items.Add(item);
            //}
        }

        public cDatoCancion(configAplicacion _infConfig)
        {
            infAplica = _infConfig;

            InitializeComponent();
            LlenaComboGenero();
            LimpiarComponentes();
        }

        public void LlenaDatosMusica(List<entC.Canciones.entCancion> lstCancioinesMod)
        {
            string sRuta = "";
            Boolean bModCanciones = false, bID3v1 = false, bID3v2 = false;
            Nullable<int> Mod_nPista = -2, ID3v1_nPista = -2, ID3v2_nPista = -2, Mod_nAño = -2, ID3v1_nAño = -2, ID3v2_nAño = -2, Mod_nIdGenero = -2, ID3v1_nIdGenero = -2, ID3v2_nIdGenero = -2;
            String Mod_sGenero = "-2", ID3v1_sGenero = "-2", ID3v2_sGenero = "-2", Mod_sNombre = "-2", ID3v1_sNombre = "-2", ID3v2_sNombre = "-2", Mod_sArtista = null, ID3v1_sArtista = null, ID3v2_sArtista = null, Mod_sAlbum = null, ID3v1_sAlbum = null, ID3v2_sAlbum = null, Mod_sComentario = null, ID3v1_sComentario = null, ID3v2_sComentario = null;

            lstCanMod = lstCancioinesMod;
            LimpiarComponentes();

            gbModificado.Text = string.Format(@"{0} ** {1}\{2}", lstCancioinesMod[0].RutaID, lstCancioinesMod[0].Ruta, lstCancioinesMod[0].NombreArchivo);
            sRuta = lstCancioinesMod[0].Ruta.Trim();

            //if (lstCancioinesMod.Count > 1)
            //{
            lstCancioinesMod.ForEach(itemC =>
            {
                if (gbModificado.Text != "<< Varias Canciones >>")
                {
                    if (gbModificado.Text != string.Format(@"{0} ** {1}\{2}", itemC.RutaID, itemC.Ruta, itemC.NombreArchivo)) gbModificado.Text = "<< Varias Canciones >>"; else gbModificado.Text = string.Format(@"{0} ** {1}\{2}", itemC.RutaID, itemC.Ruta, itemC.NombreArchivo);
                }
                if (sRuta != "<< Varias Rutas >>")
                {
                    if (sRuta != itemC.Ruta.Trim()) sRuta = "<< Varias Rutas >>"; else sRuta = itemC.Ruta.Trim();
                }

                if (itemC.bDatCan_Mod) bModCanciones = true;

                Mod_nPista = ValidaDatosMostrar(itemC.DatCan_Mod.sMusica_NumPista, Mod_nPista);
                Mod_nAño = ValidaDatosMostrar(itemC.DatCan_Mod.sMusica_Año, Mod_nAño);

                Mod_nIdGenero = itemC.DatCan_Mod.sMusica_IdGenero;
                Mod_sGenero = itemC.DatCan_Mod.sMusica_Genero;

                Mod_sNombre = ValidaDatosMostrar(itemC.DatCan_Mod.sMusica_Nombre, Mod_sNombre, "<< Varios Títulos >>");
                Mod_sArtista = ValidaDatosMostrar(itemC.DatCan_Mod.sMusica_Artista, Mod_sArtista, "<< Varios Artistas >>");
                Mod_sAlbum = ValidaDatosMostrar(itemC.DatCan_Mod.sMusica_Album, Mod_sAlbum, "<< Varios Album >>");
                Mod_sComentario = ValidaDatosMostrar(itemC.DatCan_Mod.sMusica_Comentario, Mod_sComentario, "<< Varios Comentarios >>");

                if (itemC.bEstadoID3v1) bID3v1 = true;

                ID3v1_nPista = ValidaDatosMostrar(itemC.ID3v1.sMusica_NumPista, ID3v1_nPista);
                ID3v1_nAño = ValidaDatosMostrar(itemC.ID3v1.sMusica_Año, ID3v1_nAño);

                ID3v1_nIdGenero = itemC.ID3v1.sMusica_IdGenero;
                ID3v1_sGenero = itemC.ID3v1.sMusica_Genero;

                ID3v1_sNombre = ValidaDatosMostrar(itemC.ID3v1.sMusica_Nombre, ID3v1_sNombre, "<< Varios Títulos >>");
                ID3v1_sArtista = ValidaDatosMostrar(itemC.ID3v1.sMusica_Artista, ID3v1_sArtista, "<< Varios Artistas >>");
                ID3v1_sAlbum = ValidaDatosMostrar(itemC.ID3v1.sMusica_Album, ID3v1_sAlbum, "<< Varios Album >>");
                ID3v1_sComentario = ValidaDatosMostrar(itemC.ID3v1.sMusica_Comentario, ID3v1_sComentario, "<< Varios Comentarios >>");

                if (itemC.bEstadoID3v2) bID3v2 = true;

                ID3v2_nPista = ValidaDatosMostrar(itemC.ID3v2.sMusica_NumPista, ID3v2_nPista);
                ID3v2_nAño = ValidaDatosMostrar(itemC.ID3v2.sMusica_Año, ID3v2_nAño);

                ID3v2_nIdGenero = itemC.ID3v2.sMusica_IdGenero;
                ID3v2_sGenero = itemC.ID3v2.sMusica_Genero;

                ID3v2_sNombre = ValidaDatosMostrar(itemC.ID3v2.sMusica_Nombre, ID3v2_sNombre, "<< Varios Títulos >>");
                ID3v2_sArtista = ValidaDatosMostrar(itemC.ID3v2.sMusica_Artista, ID3v2_sArtista, "<< Varios Artistas >>");
                ID3v2_sAlbum = ValidaDatosMostrar(itemC.ID3v2.sMusica_Album, ID3v2_sAlbum, "<< Varios Album >>");
                ID3v2_sComentario = ValidaDatosMostrar(itemC.ID3v2.sMusica_Comentario, ID3v2_sComentario, "<< Varios Comentarios >>");
            });
            //}
            //else
            //{
            //bModCanciones = lstCancioinesMod[0].bDatCan_Mod;
            //bID3v1 = lstCancioinesMod[0].bEstadoID3v1;
            //bID3v2 = lstCancioinesMod[0].bEstadoID3v2;

            //if (bModCanciones)
            //{
            //    Mod_nPista = lstCancioinesMod[0].DatCan_Mod.sMusica_NumPista;
            //    Mod_nAño = lstCancioinesMod[0].DatCan_Mod.sMusica_Año;
            //    Mod_nIdGenero = lstCancioinesMod[0].DatCan_Mod.sMusica_IdGenero;
            //    Mod_sGenero = lstCancioinesMod[0].DatCan_Mod.sMusica_Genero;
            //    Mod_sNombre = lstCancioinesMod[0].DatCan_Mod.sMusica_Nombre;
            //    Mod_sArtista = lstCancioinesMod[0].DatCan_Mod.sMusica_Artista;
            //    Mod_sAlbum = lstCancioinesMod[0].DatCan_Mod.sMusica_Album;
            //    Mod_sComentario = lstCancioinesMod[0].DatCan_Mod.sMusica_Comentario;
            //}
            //if (bID3v1)
            //{
            //    ID3v1_nPista = lstCancioinesMod[0].ID3v1.sMusica_NumPista;
            //    ID3v1_nAño = lstCancioinesMod[0].ID3v1.sMusica_Año;
            //    ID3v1_nIdGenero = lstCancioinesMod[0].ID3v1.sMusica_IdGenero;
            //    ID3v1_sGenero = lstCancioinesMod[0].ID3v1.sMusica_Genero;
            //    ID3v1_sNombre = lstCancioinesMod[0].ID3v1.sMusica_Nombre;
            //    ID3v1_sArtista = lstCancioinesMod[0].ID3v1.sMusica_Artista;
            //    ID3v1_sAlbum = lstCancioinesMod[0].ID3v1.sMusica_Album;
            //    ID3v1_sComentario = lstCancioinesMod[0].ID3v1.sMusica_Comentario;
            //}
            //if (bID3v2)
            //{
            //    ID3v2_nPista = lstCancioinesMod[0].ID3v2.sMusica_NumPista;
            //    ID3v2_nAño = lstCancioinesMod[0].ID3v2.sMusica_Año;
            //    ID3v2_nIdGenero = lstCancioinesMod[0].ID3v2.sMusica_IdGenero;
            //    ID3v2_sGenero = lstCancioinesMod[0].ID3v2.sMusica_Genero;
            //    ID3v2_sNombre = lstCancioinesMod[0].ID3v2.sMusica_Nombre;
            //    ID3v2_sArtista = lstCancioinesMod[0].ID3v2.sMusica_Artista;
            //    ID3v2_sAlbum = lstCancioinesMod[0].ID3v2.sMusica_Album;
            //    ID3v2_sComentario = lstCancioinesMod[0].ID3v2.sMusica_Comentario;
            //}
            //}

            rutaCancion = sRuta;
            if (bModCanciones)
            {
                Mod_numeroPista = Mod_nPista;
                Mod_Año = Mod_nAño;
                Mod_idGenero = Mod_nIdGenero;
                //Mod_Genero = Mod_sGenero;
                Mod_Nombre = Mod_sNombre;
                Mod_Artista = Mod_sArtista;
                Mod_Album = Mod_sAlbum;
                Mod_Comentario = Mod_sComentario;
            }
            if (bID3v1)
            {
                txtID3v1_NumPista.Text = ID3v1_nPista.ToString().Trim();
                txtID3v1_Año.Text = ID3v1_nAño.ToString().Trim();
                txtID3v1_Genero.Text = ID3v1_sGenero;
                txtID3v1_Nombre.Text = ID3v1_sNombre;
                txtID3v1_Artista.Text = ID3v1_sArtista;
                txtID3v1_Album.Text = ID3v1_sAlbum;
                txtID3v1_Comentario.Text = ID3v1_sComentario;
            }
            if (bID3v2)
            {
                txtID3v2_NumPista.Text = ID3v2_nPista.ToString().Trim();
                txtID3v2_Año.Text = ID3v2_nAño.ToString().Trim();
                txtID3v2_Genero.Text = ID3v2_sGenero;
                txtID3v2_Nombre.Text = ID3v2_sNombre;
                txtID3v2_Artista.Text = ID3v2_sArtista;
                txtID3v2_Album.Text = ID3v2_sAlbum;
                txtID3v2_Comentario.Text = ID3v2_sComentario;
            }
        }
        private int? ValidaDatosMostrar(int? sDatoOrigen, int? sDatoModificado)
        {
            int? nResult = null;

            switch (sDatoModificado)
            {
                case -999:
                    nResult = -999;
                    break;
                case -2:
                    nResult = sDatoOrigen;
                    break;
                default:
                    if (sDatoModificado != null)
                    {
                        if (sDatoModificado != sDatoOrigen) nResult = -999; else nResult = sDatoModificado;
                    }
                    else nResult = null;
                    break;
            }
            return nResult;
        }
        private string ValidaDatosMostrar(string sDatoOrigen, string sDatoModificado, string sMenajeVarios)
        {
            string sResult = "";

            if (sDatoModificado == sMenajeVarios) sResult = sMenajeVarios;
            else if (sDatoModificado == "-2") sResult = sDatoOrigen;
            else
            {
                if (sDatoOrigen != null && sDatoOrigen != "")
                {
                    if (sDatoModificado == null) sDatoModificado = sDatoOrigen;
                    if (sDatoModificado.ToLower().Trim() != sDatoOrigen.ToLower().Trim()) sResult = sMenajeVarios; else sResult = sDatoOrigen.Trim();
                }
            }

            return sResult;
        }

        private void LimpiarComponentes()
        {

            rutaCancion = null;
            Mod_numeroPista = null;
            Mod_Año = null;
            Mod_idGenero = null;
            //Mod_Genero = null;
            Mod_Nombre = null;
            Mod_Artista = null;
            Mod_Album = null;
            Mod_Comentario = null;

            txtID3v1_NumPista.Text = string.Empty;
            txtID3v1_Año.Text = string.Empty;
            txtID3v1_Genero.Text = string.Empty;
            txtID3v1_Nombre.Text = string.Empty;
            txtID3v1_Artista.Text = string.Empty;
            txtID3v1_Album.Text = string.Empty;
            txtID3v1_Comentario.Text = string.Empty;

            txtID3v2_NumPista.Text = string.Empty;
            txtID3v2_Año.Text = string.Empty;
            txtID3v2_Genero.Text = string.Empty;
            txtID3v2_Nombre.Text = string.Empty;
            txtID3v2_Artista.Text = string.Empty;
            txtID3v2_Album.Text = string.Empty;
            txtID3v2_Comentario.Text = string.Empty;
        }

        private void cDatoCancion_Load(object sender, EventArgs e)
        {

        }

        private void txtPista_TextChanged(object sender, EventArgs e)
        {
            ActualizaDatosModificacion(1);
        }
        private void txtAño_TextChanged(object sender, EventArgs e)
        {
            ActualizaDatosModificacion(2);
        }
        private void cmbGenero_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ActualizaDatosModificacion(3);
        }
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ActualizaDatosModificacion(5);
        }
        private void txtArtista_TextChanged(object sender, EventArgs e)
        {
            ActualizaDatosModificacion(6);
        }
        private void txtAlbum_TextChanged(object sender, EventArgs e)
        {
            ActualizaDatosModificacion(7);
        }
        private void txtComentario_TextChanged(object sender, EventArgs e)
        {
            ActualizaDatosModificacion(8);
        }

        private void ActualizaDatosModificacion(int nDatoMod)
        {
            if (lstCanMod != null && frmPadre != null)
            {
                lstCanMod.ForEach(item =>
                {
                    item.bDatCan_Mod = true;

                    frmPadre.lstCancionesAll.FindAll(itemS => itemS.RutaID == item.RutaID).ToList().ForEach(itemSearch =>
                    {
                        itemSearch.bDatCan_Mod = true;

                        if (nDatoMod == 1) itemSearch.DatCan_Mod.sMusica_NumPista = Mod_numeroPista;
                        if (nDatoMod == 2) itemSearch.DatCan_Mod.sMusica_Año = Mod_Año;
                        if (nDatoMod == 3) itemSearch.DatCan_Mod.sMusica_IdGenero = Mod_idGenero;
                        if (nDatoMod == 3) itemSearch.DatCan_Mod.sMusica_Genero = Mod_Genero;
                        if (nDatoMod == 5) itemSearch.DatCan_Mod.sMusica_Nombre = Mod_Nombre;
                        if (nDatoMod == 6) itemSearch.DatCan_Mod.sMusica_Artista = Mod_Artista;
                        if (nDatoMod == 7) itemSearch.DatCan_Mod.sMusica_Album = Mod_Album;
                        if (nDatoMod == 8) itemSearch.DatCan_Mod.sMusica_Comentario = Mod_Comentario;
                    });
                    frmPadre.lstCancionesFill.FindAll(itemS => itemS.RutaID == item.RutaID).ToList().ForEach(itemSearch =>
                    {
                        itemSearch.bDatCan_Mod = true;

                        if (nDatoMod == 1) itemSearch.DatCan_Mod.sMusica_NumPista = Mod_numeroPista;
                        if (nDatoMod == 2) itemSearch.DatCan_Mod.sMusica_Año = Mod_Año;
                        if (nDatoMod == 3) itemSearch.DatCan_Mod.sMusica_IdGenero = Mod_idGenero;
                        if (nDatoMod == 3) itemSearch.DatCan_Mod.sMusica_Genero = Mod_Genero;
                        if (nDatoMod == 5) itemSearch.DatCan_Mod.sMusica_Nombre = Mod_Nombre;
                        if (nDatoMod == 6) itemSearch.DatCan_Mod.sMusica_Artista = Mod_Artista;
                        if (nDatoMod == 7) itemSearch.DatCan_Mod.sMusica_Album = Mod_Album;
                        if (nDatoMod == 8) itemSearch.DatCan_Mod.sMusica_Comentario = Mod_Comentario;
                    });

                    if (nDatoMod == 1) item.DatCan_Mod.sMusica_NumPista = Mod_numeroPista;
                    if (nDatoMod == 2) item.DatCan_Mod.sMusica_Año = Mod_Año;
                    if (nDatoMod == 3) item.DatCan_Mod.sMusica_IdGenero = Mod_idGenero;
                    if (nDatoMod == 3) item.DatCan_Mod.sMusica_Genero = Mod_Genero;
                    if (nDatoMod == 5) item.DatCan_Mod.sMusica_Nombre = Mod_Nombre;
                    if (nDatoMod == 6) item.DatCan_Mod.sMusica_Artista = Mod_Artista;
                    if (nDatoMod == 7) item.DatCan_Mod.sMusica_Album = Mod_Album;
                    if (nDatoMod == 8) item.DatCan_Mod.sMusica_Comentario = Mod_Comentario;


                });
            }

            //LlenaDatosMusica(lstCanMod);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            LlenaDatosMusica(lstCanMod);
        }
    }
}
