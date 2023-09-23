﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using rnSeg = WA_RN.Seguridad;
using entSeg = WA_Entidades.Seguridad;
using exM = UtileriasComunes.ManejoErrores;

namespace WA_Formas
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            entSeg.EntDatosAutentificacion datSeguridad;
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                rnSeg.IManejoSeguridad ManejoSeg = new rnSeg.ManejoSeguridad();

                datSeguridad = ManejoSeg.ValidaUsuario();
                Application.Run(new FrmInicio(datSeguridad));
            }
            catch (exM.ErroresAplicacion ex)
            {
                if (ex.IdError == -999) Application.Run(new FrmLogin());
                else MessageBox.Show(ex.MensajeUsuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}