using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using entC = Personal.Entidades.Canciones;

namespace ManejoCanciones.uControl
{
    public partial class frmBusquedaCanciones : Form
    {
        public enum enumTipoFiltro { Ruta, Nombre, Titulo, Artista, Todos }
        public List<entC.entCancion> lstCancionesAll { get; set; }
        public List<entC.entCancion> lstCancionesBusqueda { get; set; }
        public frmBusquedaCanciones()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lstCancionesBusqueda = new List<entC.entCancion>();

            BuscaCanciones(enumTipoFiltro.Todos, txtBusqueda.Text.Trim());

            this.Close();

        }

        private void BuscaCanciones(enumTipoFiltro sCampo, string sValor)
        {
            lstCancionesAll.ForEach(item =>
            {
                Boolean bolEstado = false;
                switch (sCampo)
                {
                    case enumTipoFiltro.Ruta:
                        if (item.Ruta.Contains(sValor))
                        {
                            bolEstado = true;
                        }
                        break;
                    case enumTipoFiltro.Nombre:
                        if (item.NombreArchivo.Contains(sValor))
                        {
                            bolEstado = true;
                        }
                        break;
                    case enumTipoFiltro.Titulo:
                        if ((item.bEstadoID3v1 && item.ID3v1.sMusica_Nombre.Contains(sValor)) ||
                        (item.bEstadoID3v2 && item.ID3v2.sMusica_Nombre.Contains(sValor)))
                        {
                            bolEstado = true;
                        }
                        break;
                    case enumTipoFiltro.Artista:
                        if ((item.bEstadoID3v1 && item.ID3v1.sMusica_Artista.Contains(sValor)) ||
                        (item.bEstadoID3v2 && item.ID3v2.sMusica_Artista.Contains(sValor)))
                        {
                            bolEstado = true;
                        }
                        break;
                    case enumTipoFiltro.Todos:
                        if (item.Ruta.Contains(sValor))
                        {
                            bolEstado = true;
                        }
                        if (item.NombreArchivo.Contains(sValor))
                        {
                            bolEstado = true;
                        }
                        if ((item.bEstadoID3v1 && item.ID3v1.sMusica_Nombre.Contains(sValor)) ||
                        (item.bEstadoID3v2 && item.ID3v2.sMusica_Nombre.Contains(sValor)))
                        {
                            bolEstado = true;
                        }
                        if ((item.bEstadoID3v1 && item.ID3v1.sMusica_Artista.Contains(sValor)) ||
                        (item.bEstadoID3v2 && item.ID3v2.sMusica_Artista.Contains(sValor)))
                        {
                            bolEstado = true;
                        }
                        break;
                }
                if (bolEstado)
                {
                    lstCancionesBusqueda.Add(new entC.entCancion(item));
                }
            });

        }

    }
}
