using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManejoCanciones.uControl
{
    public partial class frmReproducirCancion : Form
    {
        public string sMediaFile { get; set; }
        public frmReproducirCancion(string sArchivo)
        {
            InitializeComponent();

            sMediaFile = sArchivo;

            txtCancion.Text = sMediaFile;

            ReproducirCancion();
        }

        private void frmReproducirCancion_Load(object sender, EventArgs e)
        {
            
        }

        private void ReproducirCancion()
        {
            try
            {
                wmpMusica.URL = @"" + sMediaFile;
                tmrMusicaProgres.Start();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void mtbVolumen_ValueChanged(object sender, decimal value)
        {
            wmpMusica.settings.volume = mtbVolumen.Value;
            txtNivelVolumen.Text = mtbVolumen.Value.ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (wmpMusica != null)
            {
                wmpMusica.close();
                wmpMusica.Dispose();
            }
            this.Close();
        }

        private void ProgresoMtbCancion()
        {
            if (wmpMusica.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                mtbCancion.Maximum = (int) wmpMusica.Ctlcontrols.currentItem.duration;
                tmrMusicaProgres.Start();
            }
            if (wmpMusica.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                tmrMusicaProgres.Stop();
            }
            if (wmpMusica.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                tmrMusicaProgres.Stop();
                mtbCancion.Value = 0;
            }
        }

        private void tmrMusicaProgres_Tick(object sender, EventArgs e)
        {
            ProgresoMtbCancion();
            mtbCancion.Value = (int)wmpMusica.Ctlcontrols.currentPosition;
            mtbVolumen.Value = wmpMusica.settings.volume;
            txtCancionMinutos.Text = wmpMusica.Ctlcontrols.currentPositionString;
            txtCancionMinutosDuracion.Text = wmpMusica.currentMedia.durationString;
        }

        private void mtbCancion_ValueChanged(object sender, decimal value)
        {
            if (mtbCancion.Value != (int)wmpMusica.Ctlcontrols.currentPosition)
            {
                wmpMusica.Ctlcontrols.currentPosition = mtbCancion.Value;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlay.Text == ";")
            {
                wmpMusica.Ctlcontrols.pause();
                btnPlay.Text = "4";
                tmrMusicaProgres.Stop();
            }
            else if (btnPlay.Text == "4")
            {
                wmpMusica.Ctlcontrols.play();
                btnPlay.Text = ";";
                tmrMusicaProgres.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            wmpMusica.Ctlcontrols.stop();
            btnPlay.Text = "4";
            tmrMusicaProgres.Stop();
            txtCancionMinutos.Text = "__:__";
            mtbCancion.Value = 0;
        }

        private void btnAdelantar_Click(object sender, EventArgs e)
        {
            mtbCancion.Value = mtbCancion.Value + 5;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            mtbCancion.Value = mtbCancion.Value - 5;
        }

        private void btnRepetir_Click(object sender, EventArgs e)
        {
            if (btnRepetir.BackColor == Color.Transparent)
            {
                btnRepetir.BackColor = btnRepetir.FlatAppearance.MouseOverBackColor;
                wmpMusica.settings.setMode("loop", true);
            }
            else if (btnRepetir.BackColor == btnRepetir.FlatAppearance.MouseOverBackColor)
            {
                btnRepetir.BackColor = Color.Transparent;
                wmpMusica.settings.setMode("loop", false);
            }
        }
    }
}
