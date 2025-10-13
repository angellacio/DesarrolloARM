using Personal.Entidades;
using Personal.Entidades.Canciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using entC = Personal.Entidades.Canciones;
using rnC = Personal.ReglaNegocio.Canciones;

namespace ManejoCanciones
{
    public partial class FrmCanciones : Form
    {
        public List<entC.entCancion> lstCancionesAll { get; set; }
        public List<entC.entCancion> lstCancionesFill { get; set; }
        List<entC.entFolder> lstFolderAll { get; set; }
        //private entC.datFolder DatFolder { get; set; }
        private Thread hiloObtenCanciones;
        //private Thread hiloListMusicTree;
        private string SRutaOriginal { get; set; }
        public FrmCanciones()
        {
            InitializeComponent();
            lstCancionesAll = new List<entCancion>();
            lstFolderAll = new List<entFolder>();
            dtgMusica.AutoGenerateColumns = false;
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {

            SRutaOriginal = string.Empty;
            DialogResult drOrigen = DialogResult.Cancel;

            drOrigen = fbdUbicacion.ShowDialog();

            if (drOrigen == DialogResult.OK)
            {
                SRutaOriginal = fbdUbicacion.SelectedPath.Trim();

                ThreadStart delegadoPS = new ThreadStart(ObtenCanciones);
                hiloObtenCanciones = new Thread(delegadoPS);
                hiloObtenCanciones.Start();
            }
            txtRutaOrigen.Text = SRutaOriginal;

            //pnlCanciones.AutoScroll = true;
            //pnlCanciones.AutoScrollPosition = new Point(30, 30);
            //dtgMusica.DataSource = lstCanciones;
        }

        private delegate void SetTextDelegate(string prValue);
        private delegate void SetValueDelegate(double prValue);
        private delegate void SetVisibleDelegate(Boolean bolEstado);
        private delegate void SetDataSourceDelegate(List<entC.entCancion> lstLlenar);

        private void SetText_gbListaCanciones(string sValor)
        {
            if (gbListaCanciones.InvokeRequired)
            {
                SetTextDelegate delegado = new SetTextDelegate(SetText_gbListaCanciones);
                gbListaCanciones.Invoke(delegado, new object[] { sValor });
            }
            else
            {
                gbListaCanciones.Text = sValor;
            }
        }
        private void SetValue_bgProgreso(double hecho)
        {
            if (pgbCargando.InvokeRequired)
            {
                SetValueDelegate delegado = new SetValueDelegate(SetValue_bgProgreso);
                pgbCargando.Invoke(delegado, new object[] { (int)hecho });
            }
            else
            {
                pgbCargando.Value = (int)hecho;
            }
        }
        private void SetVisible_bgProgreso(Boolean bolEstado)
        {
            if (pgbCargando.InvokeRequired)
            {
                SetVisibleDelegate delegado = new SetVisibleDelegate(SetVisible_bgProgreso);
                pgbCargando.Invoke(delegado, new object[] { bolEstado });
            }
            else
            {
                pgbCargando.Visible = bolEstado;
            }
        }
        private void SetVisible_btnBuscar(Boolean bolEstado)
        {
            if (btnBuscar.InvokeRequired)
            {
                SetVisibleDelegate delegado = new SetVisibleDelegate(SetVisible_btnBuscar);
                btnBuscar.Invoke(delegado, new object[] { bolEstado });
            }
            else
            {
                btnBuscar.Visible = bolEstado;
            }
        }
        private void SetDatSource_dtgMusica(List<entC.entCancion> lstLlenar)
        {
            if (dtgMusica.InvokeRequired)
            {
                SetDataSourceDelegate delegado = new SetDataSourceDelegate(SetDatSource_dtgMusica);
                dtgMusica.Invoke(delegado, new object[] { lstLlenar });
            }
            else
            {
                dtgMusica.DataSource = lstLlenar;
            }
        }

        private void ObtenCanciones()
        {
            rnC.ManejoCancion manejoCancion = null;
            List<entC.entCancion> lstCancionesExiste = null;
            double dbPorcentaje = 1.0, dbPorcenIncrementa = 0.0;
            try
            {
                SetValue_bgProgreso(dbPorcentaje);

                manejoCancion = new rnC.ManejoCancion(rnC.ManejoCancion.ConsultaCatalogo());

                lstCancionesExiste = manejoCancion.CargaDatosCarpeta(SRutaOriginal);

                if (lstCancionesExiste == null)
                {
                    lstCancionesAll = manejoCancion.BuscarCanciones(SRutaOriginal);

                    dbPorcentaje = 3.0;
                    SetValue_bgProgreso(dbPorcentaje);

                    for (int nR = 0; nR < lstCancionesAll.Count; nR++)
                    {
                        lstCancionesAll[nR].RutaID = nR + 1;
                    }

                    dbPorcentaje = 10.0;
                    SetValue_bgProgreso(dbPorcentaje);

                    MethodInvoker delegado;
                    delegado = new MethodInvoker(setValor_Arbol);
                    tvCanciones.Invoke(delegado);

                    dbPorcenIncrementa = 90.0 / lstCancionesAll.Count;

                    lstCancionesAll.ForEach(itemCancion =>
                    {
                        entC.entCancion itemMod = manejoCancion.completaInformacion(itemCancion);

                        itemCancion.bEstadoID3v1 = itemMod.bEstadoID3v1;
                        itemCancion.bEstadoID3v2 = itemMod.bEstadoID3v2;

                        itemCancion.ID3v1 = new entPropiedadesCancion(itemMod.ID3v1);
                        itemCancion.ID3v2 = new entPropiedadesCancion(itemMod.ID3v2);

                        dbPorcentaje += dbPorcenIncrementa;
                        SetValue_bgProgreso(dbPorcentaje);
                    });

                    manejoCancion.GuardarDatosCarpeta(SRutaOriginal, lstCancionesAll);
                }
                else
                {
                    //DatFolder = DatFolderExist;
                    lstCancionesAll = lstCancionesExiste;
                    dbPorcentaje = 10.0;
                    SetValue_bgProgreso(dbPorcentaje);

                    MethodInvoker delegado;
                    delegado = new MethodInvoker(setValor_Arbol);
                    tvCanciones.Invoke(delegado);

                }
                SetValue_bgProgreso(100);

                lstCancionesFill = new List<entCancion>();

                lstCancionesAll.ForEach(entCancion =>
                {
                    lstCancionesFill.Add(new entCancion(entCancion));
                });

                LlenaGridCanciones();
            }
            catch (ApplicationException)
            {

            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        private void setValor_Arbol()
        {
            setValor_ArbolLista(lstCancionesAll);
        }
        private void setValor_ArbolLista(List<entCancion> lstCancionesFolder)
        {
            int numCarpetaMax = 0;
            List<entC.entFolder> LstFolder = new List<entC.entFolder>();
            List<entCatSensillo> lstFolders = new List<entCatSensillo>();
            lstCancionesFolder.Select(item => string.Format("{0}", item.Ruta.Replace(string.Format(@"{0}\", SRutaOriginal), "")).Trim())
                .Distinct().OrderBy(itemOB => itemOB.Trim()).ToList().ForEach(item =>
                {
                    string[] sArrayCarp = item.Split('\\');
                    string sRutaFin = "", sRutaAnt = "";
                    int nNivel = 0;

                    nNivel = sArrayCarp.Length;
                    sRutaFin = sArrayCarp[sArrayCarp.Length - 1];

                    if (nNivel > 1)
                    {
                        foreach (string sCarpeta in sArrayCarp)
                        {
                            if (sRutaAnt != "") sRutaAnt = string.Format("{0}\\{1}", sRutaAnt, sCarpeta);
                            else sRutaAnt = string.Format("{0}", sCarpeta);
                        }

                        sRutaAnt = sRutaAnt.Replace(string.Format("\\{0}", sRutaFin), "");
                    }
                    lstFolders.Add(new entCatSensillo() { nId = nNivel, sAcronimo = sRutaAnt, sDescripcion = sRutaFin });
                });

            numCarpetaMax = lstFolders.Max(itemMaxCarp => itemMaxCarp.nId);

            foreach (entCatSensillo item in lstFolders)
            {
                string[] sAraySC = item.sAcronimo.Split('\\');

                List<entFolder> lstFolderRecorre = lstFolderAll;

                for (int nR = 0; nR < sAraySC.Length; nR++)
                {
                    if (sAraySC[nR] != string.Empty)
                    {
                        ValidaCarpeta(lstFolderRecorre, sAraySC[nR], nR);

                        entFolder itemFolderRecorre = lstFolderRecorre.Find(itemFRB => itemFRB.sFolder == sAraySC[nR]);
                        if (itemFolderRecorre != null)
                        {
                            lstFolderRecorre = lstFolderRecorre.Find(itemFRB => itemFRB.sFolder == sAraySC[nR]).lstSubFolder;
                        }
                    }
                }

                ValidaCarpeta(lstFolderRecorre, item.sDescripcion, sAraySC.Length + 1);
            }
            tvCanciones.Nodes.Clear();

            lstFolderAll.ForEach(itemF =>
            {
                TreeNode[] tViewSubF1 = AgregarRamasTreeView(itemF);
                if (tViewSubF1 == null) tvCanciones.Nodes.Add(new TreeNode(itemF.sFolder));
                else tvCanciones.Nodes.Add(new TreeNode(itemF.sFolder, tViewSubF1));
            });

            //for (int nR = 0; nR < tvCanciones.Nodes.Count; nR++)
            //{
            //    TreeNode tnAgregar = tvCanciones.Nodes[nR];
            //    AgregarCancionesTreeView("", tnAgregar);
            //}
        }

        public void ValidaCarpeta(List<entFolder> lstBusqueda, string sCarpetaNueva, int nNivel)
        {
            if (nNivel != 0)
            {
                if (lstBusqueda.FindAll(itemB => itemB.sFolder == sCarpetaNueva).Count <= 0)
                {
                    lstBusqueda.Add(new entFolder(sCarpetaNueva, nNivel));
                }
            }
        }

        public entC.entFolder consCreaItemFolder(entC.entFolder itemFolder, entC.entFolder itemFolderPadre)
        {
            entC.entFolder result = null;
            List<entC.entFolder> lstSF = null;

            if (itemFolderPadre != null)
            {
                result = itemFolderPadre;
            }
            if (itemFolder.lstSubFolder.Count > 0)
            {
                lstSF = new List<entC.entFolder>();

                itemFolder.lstSubFolder.ForEach(itemFRep =>
                {
                    Boolean bolExistePadre = false;
                    entC.entFolder itemSubFolder = null;

                    if (itemFolderPadre != null && itemFolderPadre.lstSubFolder.FindAll(itemFind => itemFind.sFolder == itemFRep.sFolder).Count > 0)
                    {
                        bolExistePadre = true;
                    }

                    if (bolExistePadre)
                    {
                        itemSubFolder = itemFolderPadre.lstSubFolder.Find(itemFolderBusqueda => itemFolderBusqueda.sFolder == itemFRep.sFolder);
                    }

                    lstSF.Add(consCreaItemFolder(itemFRep, itemSubFolder));

                    if (bolExistePadre)
                    {
                        lstSF.Find(itemFolderBusqueda => itemFolderBusqueda.sFolder == itemFRep.sFolder).lstSubFolder = itemFolder.lstSubFolder;
                    }
                    //else
                    //{
                    //    lstSF.Add(itemFolder);
                    //}
                });
            }

            if (result == null) result = new entC.entFolder(itemFolder);

            if (lstSF != null && lstSF.Count > 0)
            {
                lstSF.ForEach(item =>
                {
                    result.lstSubFolder.Add(item);
                });
            }

            return result;
        }

        private TreeNode[] AgregarRamasTreeView(entC.entFolder itemFolder)
        {
            TreeNode[] result = null;
            TreeNode[] datSubFolder = null;

            if (itemFolder.lstSubFolder != null && itemFolder.lstSubFolder.Count > 0)
            {
                int nR = 0;
                result = new TreeNode[itemFolder.lstSubFolder.Count];

                itemFolder.lstSubFolder.ForEach(itemSF1 =>
                {
                    datSubFolder = null;
                    if (itemSF1.lstSubFolder.Count > 0) datSubFolder = AgregarRamasTreeView(itemSF1);

                    if (datSubFolder == null) result[nR] = new TreeNode(itemSF1.sFolder);
                    else result[nR] = new TreeNode(itemSF1.sFolder, datSubFolder);

                    nR += 1;
                });
            }

            return result;
        }

        public void LlenaGridCanciones()
        {
            SetText_gbListaCanciones(SRutaOriginal);
            //SetDatSource_dtgMusica(lstCanc);
            SetDatSource_dtgMusica(lstCancionesFill);
            SetVisible_bgProgreso(false);
            SetVisible_btnBuscar(true);
        }

        private void btnEtiquetas_Click(object sender, EventArgs e)
        {

        }

        private void tvCanciones_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            List<entC.entCancion> lstCancionesBusqueda = null;
            string DatosRuta = "";

            DatosRuta = string.Format(@"{0}\{1}", SRutaOriginal, e.Node.FullPath);

            lstCancionesBusqueda = new List<entCancion>();

            lstCancionesFill.ForEach(entCan =>
            {
                if (entCan.Ruta.Contains(DatosRuta))
                {
                    lstCancionesBusqueda.Add(new entCancion(entCan));
                }
            });

            dtgMusica.DataSource = lstCancionesBusqueda;
            gbListaCanciones.Text = DatosRuta;
        }

        private void dtgMusica_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //// Obtener el nombre de la columna clicada
            //string columnName = dtgMusica.Columns[e.ColumnIndex].Name;

            //// Determinar la dirección de ordenación
            //if (sortColumn == columnName)
            //{
            //    // Cambiar la dirección de ordenación si se hace clic en la misma columna
            //    sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            //}
            //else
            //{
            //    // Si se hace clic en una columna diferente, ordenar ascendente por defecto
            //    sortColumn = columnName;
            //    sortOrder = SortOrder.Ascending;
            //}

            //// Llamar al método Sort del GridView
            //SortGridView(columnName, sortOrder);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            uControl.frmBusquedaCanciones frmBusca = new uControl.frmBusquedaCanciones();

            frmBusca.lstCancionesAll = this.lstCancionesAll;

            frmBusca.ShowDialog();

            if (frmBusca.lstCancionesBusqueda != null && frmBusca.lstCancionesBusqueda.Count > 0)
            {
                lstCancionesFill.Clear();
                frmBusca.lstCancionesBusqueda.ForEach(itemCF =>
                {
                    lstCancionesFill.Add(itemCF);
                });
            }

            LlenaGridCanciones();
            setValor_ArbolLista(lstCancionesFill);
        }

        private void dtgMusica_SelectionChanged(object sender, EventArgs e)
        {
            List<entC.entCancion> lstSeleccionC = null;
            uControl.cDatoCancion cDatoCancion1 = null;


            lstSeleccionC = new List<entCancion>();

            for (int nR = 0; nR < ((System.Windows.Forms.DataGridView)sender).SelectedRows.Count; nR++)
            {
                lstSeleccionC.Add(new entCancion(((DataGridViewRow)((System.Windows.Forms.DataGridView)sender).SelectedRows[nR]).DataBoundItem as entCancion));
            }
            
            scCanciones.Panel2.Controls.Clear();
            cDatoCancion1 = new uControl.cDatoCancion();
            cDatoCancion1.Dock = System.Windows.Forms.DockStyle.Fill;
            cDatoCancion1.AutoSize = true;
            cDatoCancion1.LlenaDatosMusica(lstSeleccionC);
            scCanciones.Panel2.Controls.Add(cDatoCancion1);

        }
    }
}
