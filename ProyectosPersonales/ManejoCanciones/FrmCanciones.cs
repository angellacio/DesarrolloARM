using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private List<entC.entCancion> LstMusica { get; set; }
        private List<entC.entCancion> LstCanSinFiltros { get; set; }
        List<entC.entFolder> LstFolder { get; set; }
        private Thread hiloObtenCanciones;
        private string SRutaOriginal { get; set; }
        public FrmCanciones()
        {
            InitializeComponent();
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {

            SRutaOriginal = string.Empty;
            DialogResult drOrigen = DialogResult.Cancel;

            drOrigen = fbdUbicacion.ShowDialog();

            if (drOrigen == DialogResult.OK)
            {
                LstMusica = new List<entC.entCancion>();
                LstCanSinFiltros = new List<entC.entCancion>();

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

        private delegate void SetValueDelegate(double prValue);
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

        private void ObtenCanciones()
        {
            rnC.ManejoCancion manejoCancion = null;
            List<string> listString = null;
            double dbPorcentaje = 1.0, dbPorcenIncrementa = 0.0;
            int nRRuta = 0;
            try
            {
                SetValue_bgProgreso(dbPorcentaje);

                manejoCancion = new rnC.ManejoCancion();
                listString = manejoCancion.BuscarCanciones(SRutaOriginal);

                dbPorcentaje = 3.0;
                SetValue_bgProgreso(dbPorcentaje);

                listString.ForEach(itemRutaArch =>
                {
                    string sRutCom = itemRutaArch.Trim();

                    LstCanSinFiltros.Add(new entC.entCancion(sRutCom));
                });

                dbPorcentaje = 7.0;
                SetValue_bgProgreso(dbPorcentaje);

                LstCanSinFiltros.Select(itemS => itemS.Ruta).Distinct().ToList().OrderBy(itemO => itemO).ToList().ForEach(itemF =>
                {
                    if (LstMusica.FindAll(itemC => itemC.Ruta == itemF).ToList().Count == 0) nRRuta += 1;

                    LstCanSinFiltros.FindAll(itemFRuta => itemFRuta.Ruta.Contains(itemF)).ForEach(itemFF =>
                    {
                        itemFF.RutaID = nRRuta;
                        LstMusica.Add(new entC.entCancion(itemFF));
                    });
                });

                dbPorcentaje = 10.0;
                SetValue_bgProgreso(dbPorcentaje);

                dbPorcenIncrementa = 90.0 / LstMusica.Count;

                LstMusica.ForEach(itemCancion =>
                {
                    entC.entCancion itemMod = manejoCancion.completaInformacion(itemCancion);

                    itemCancion.sMusica_iTag = itemMod.sMusica_iTag;
                    itemCancion.sMusica_NumPista = itemMod.sMusica_NumPista;
                    itemCancion.sMusica_Nombre = itemMod.sMusica_Nombre;
                    itemCancion.sMusica_Artista = itemMod.sMusica_Artista;
                    itemCancion.sMusica_ArtistaC = itemMod.sMusica_ArtistaC;
                    itemCancion.sMusica_Album = itemMod.sMusica_Album;
                    itemCancion.sMusica_Año = itemMod.sMusica_Año;
                    itemCancion.sMusica_IdGenero = itemMod.sMusica_IdGenero;
                    itemCancion.sMusica_Genero = itemMod.sMusica_Genero;
                    itemCancion.sMusica_Comentario = itemMod.sMusica_Comentario;

                    dbPorcentaje += dbPorcenIncrementa;
                    SetValue_bgProgreso(dbPorcentaje);
                });

                //lstMusica = lstCan.OrderBy(itemOrden => itemOrden.Ruta).ToList();

                MethodInvoker delegado;

                delegado = new MethodInvoker(setValor_Arbol);
                tvCanciones.Invoke(delegado);
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
            LstFolder = new List<entC.entFolder>();
            List<entC.entFolder> lstFolderRep = new List<entC.entFolder>();
            int nRT = 0;
            List<string> lstRutaAgrupar = LstMusica.Select(item => item.Ruta.Trim()).Distinct().ToList();

            lstRutaAgrupar.ForEach(itemR =>
            {
                string sRutaTV = string.Format("{0}", itemR.Replace(string.Format(@"{0}", SRutaOriginal), ""));
                if (sRutaTV.Length > 0) sRutaTV = sRutaTV.Substring(1, sRutaTV.Length - 1);

                entC.entFolder itemEstructura = consEstructuraRep(sRutaTV, nRT);

                lstFolderRep.Add(itemEstructura);




                //for (nNivelFolder = 0; nNivelFolder < nNivelMax; nNivelFolder++)
                //{
                //    int nCarpetasEF = 0;
                //    string sFolder = lstRutaSepadado[nNivelFolder].Trim();
                //    switch (nNivelFolder)
                //    {
                //        case 0:
                //            nCarpetasEF = lstFolder.FindAll(itemFF => 
                //            itemFF.sFolder.Trim() == lstRutaSepadado[nNivelFolder].Trim() && itemFF.nNivel == nNivelFolder).Count;

                //            if (nCarpetasEF == 0)
                //            {
                //                lstFolder.Add(new entC.entFolder(nNivelFolder, nRT, sFolder, sSubRuta));
                //            }

                //            sSubRuta = sFolder;
                //            break;
                //        case 1:
                //            nCarpetasEF = lstFolder.Find(itemFBS =>
                //            itemFBS.sFolder == sSubRuta && itemFBS.nNivel == (nNivelFolder - 1)
                //            ).lstSubFolder.FindAll(itemFF =>
                //            itemFF.sFolder.Trim() == lstRutaSepadado[nNivelFolder].Trim() && itemFF.nNivel == nNivelFolder).Count;

                //            if (nCarpetasEF == 0)
                //            {
                //                lstFolder.Find(itemFF =>
                //                itemFF.sFolder == sSubRuta && itemFF.nNivel == (nNivelFolder - 1)
                //                ).lstSubFolder.Add(new entC.entFolder(nNivelFolder, nRT, sFolder, sSubRuta));
                //            }

                //            sSubRuta = string.Format(@"{0}\{1}", sSubRuta, sFolder);
                //            break;
                //        case 2:
                //            string[] sRRecortada = sSubRuta.Split('\\');
                //            nCarpetasEF = lstFolder.Find(itemFBS => 
                //            itemFBS.sFolder == sRRecortada[0] && itemFBS.nNivel == (nNivelFolder - 2)).lstSubFolder.Find(itemFBS1 => 
                //            itemFBS1.sFolder == sRRecortada[1] && itemFBS1.nNivel == (nNivelFolder - 1)).lstSubFolder.FindAll(itemFF =>
                //            itemFF.sFolder.Trim() == lstRutaSepadado[nNivelFolder].Trim() && itemFF.nNivel == nNivelFolder).Count;

                //            if (nCarpetasEF == 0)
                //            {
                //                lstFolder.Find(itemFF =>
                //                itemFF.sFolder == sRRecortada[0] && itemFF.nNivel == (nNivelFolder - 2)).lstSubFolder.Find(itemFBS1 =>
                //                itemFBS1.sFolder == sRRecortada[1] && itemFBS1.nNivel == (nNivelFolder - 1)).lstSubFolder
                //                .Add(new entC.entFolder(nNivelFolder, nRT, sFolder, sSubRuta));
                //            }

                //            sSubRuta = string.Format(@"{0}\{1}", sSubRuta, sFolder);
                //            break;
                //        case 3:
                //            break;
                //    }
                //}

                nRT += 1;
            });



            lstFolderRep.ForEach(itemFRep =>
            {
                Boolean bolExistePadre = false;
                entC.entFolder itemFolder = null;
                entC.entFolder itemSubFolder = null;

                if (LstFolder != null && LstFolder.FindAll(itemFolderBusqueda => itemFolderBusqueda.sFolder == itemFRep.sFolder).Count > 0)
                {
                    bolExistePadre = true;
                }

                if (bolExistePadre)
                {
                    itemSubFolder = LstFolder.Find(itemFolderBusqueda => itemFolderBusqueda.sFolder == itemFRep.sFolder);
                }

                itemFolder = consCreaItemFolder(itemFRep, itemSubFolder);

                if (bolExistePadre)
                {
                    LstFolder.Find(itemFolderBusqueda => itemFolderBusqueda.sFolder == itemFRep.sFolder).lstSubFolder = itemFolder.lstSubFolder;
                }
                else
                {
                    LstFolder.Add(itemFolder);
                }
            });


            tvCanciones.Nodes.Clear();

            LstFolder.ForEach(itemF =>
            {
                TreeNode[] tViewSubF1 = AgregarRamasTreeView(itemF);
                if (tViewSubF1 == null) tvCanciones.Nodes.Add(new TreeNode(itemF.sFolder));
                else tvCanciones.Nodes.Add(new TreeNode(itemF.sFolder, tViewSubF1));
            });

            //lstRutaAgrupar.ForEach(itemR =>
            //{
            //    string sRutaTV = string.Format("{0}", itemR.Replace(string.Format(@"{0}", sRutaOriginal), ""));
            //    if (sRutaTV == "")
            //    {
            //        tvCanciones.Nodes.Add(new TreeNode("¡ ¡ ¡  Todos  ! ! !"));
            //    }
            //    else
            //    {
            //        tvCanciones.Nodes.Add(new TreeNode(sRutaTV));
            //    }

            //    //uControl.cDatoCancion itemCanciones = new uControl.cDatoCancion
            //    //{
            //    //    Location = new Point(5, nRT),
            //    //    Size = new Size(770, 90),
            //    //    //Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right),
            //    //    //Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right))),
            //    //    AutoSize = true
            //    //};

            //    ////itemCanciones.Name = string.Format("{0}", itemR.Replace(sRutaOriginal, ""));
            //    //itemCanciones.Name = string.Format("nR_{0}", nR);
            //    //itemCanciones.rutaCancion = string.Format("{0}", itemR.Replace(string.Format(@"{0}\", sRutaOriginal), ""));

            //    ////pnlCanciones.Controls.Add(itemCanciones);

            //    //nRT += 130;
            //    nR++;
            //});
        }

        public entC.entFolder consEstructuraRep(string sRutaCrear, int nRenglon)
        {
            entC.entFolder result = null;
            List<string> lstRutaSepadado = null;
            string sSubCarpeta = "";

            lstRutaSepadado = sRutaCrear.Split('\\').ToList();
            int nNivelFolder = 0, nNivelMax = lstRutaSepadado.Count;
            entC.entFolder consEstruHijo = null;

            for (nNivelFolder = (nNivelMax - 1); nNivelFolder >= 0; nNivelFolder--)
            {
                entC.entFolder consEstruPadre = null;
                if (nNivelFolder == 0)
                    result = new entC.entFolder(nNivelFolder, nRenglon, lstRutaSepadado[nNivelFolder], sSubCarpeta);
                else
                    consEstruPadre = new entC.entFolder(nNivelFolder, nRenglon, lstRutaSepadado[nNivelFolder], sSubCarpeta);

                if (result != null && consEstruHijo != null)
                    result.lstSubFolder.Add(consEstruHijo);
                if (consEstruPadre != null)
                {
                    if (consEstruHijo != null)
                    {
                        consEstruPadre.lstSubFolder.Add(consEstruHijo);
                    }
                    consEstruHijo = consEstruPadre;
                }
            }

            return result;
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
                    if (itemSF1.lstSubFolder.Count > 0) datSubFolder = AgregarRamasTreeView(itemSF1);

                    if (datSubFolder == null) result[nR] = new TreeNode(itemSF1.sFolder);
                    else result[nR] = new TreeNode(itemSF1.sFolder, datSubFolder);

                    nR += 1;
                });
            }

            return result;
        }
    }
}
