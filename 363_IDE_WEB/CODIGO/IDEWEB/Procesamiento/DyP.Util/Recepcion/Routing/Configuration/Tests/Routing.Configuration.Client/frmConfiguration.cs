//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.Client:frmConfiguration:0:21/May/2008[SAT.DyP.Routing.Configuration.Client:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SAT.DyP.Routing.Configuration.Client.ConfigurationService;

namespace SAT.DyP.Routing.Configuration.Client {
    public partial class frmConfiguration : Form {

        private string urlRegEx = @"(?:(?:(?:http)://)(?:w{3}\.)?(?:[a-zA-Z0-9/;\?&=:\-_\$\+!\*'\(\|\\~\[\]#%\.])+)";
        private bool bEditing = false;
        private RouteMessage editMessage = null;

        public frmConfiguration() {
            InitializeComponent();
        }

        private void frmConfiguration_Load(object sender, EventArgs e) {
            try {
                this.Cursor = System.Windows.Forms.Cursors.AppStarting;
                LoadRoutingTable();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            catch (Exception ex) {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadRoutingTable() {
            ConfigurationServiceClient client = null;
            try {
                client = new ConfigurationServiceClient();
                ConfigurationService.RouteMessage[] messages = client.GetAllRoutes();
                this.gridRuteo.DataSource = messages;
                this.gridRuteo.Refresh();               
            }
            catch (Exception ex) {
                throw (ex);
            }
            finally {
                if (client != null && client.State == System.ServiceModel.CommunicationState.Opened) client.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            string accion = this.txtSoapAction.Text.Trim();
            string destino = this.txtDestino.Text.Trim();
            if (accion != string.Empty && destino != string.Empty) {
                Regex reg = new Regex(urlRegEx);
                Match match = Match.Empty;
                match = reg.Match(destino);
                if (match.Success) {
                    if (!bEditing)
                        AddRoute(accion,destino,txtDescripcion.Text.Trim());
                    else
                        SaveRoute(accion, destino, txtDescripcion.Text.Trim());
                }
                else {
                    MessageBox.Show("Error, verifique que la acción y el URL de destino sean válidos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else {
                MessageBox.Show("La acción y el destino son campos requeridos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SaveRoute(string accion, string destino, string descripcion) {
            if (DialogResult.Yes == MessageBox.Show("¿Guardar los cambios a la ruta? Recuerde que los servicios se reiniciarán.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                this.Cursor = Cursors.WaitCursor;
                ConfigurationServiceClient client = null;
                try {
                    editMessage.SoapAction = accion;
                    editMessage.Destination = destino;
                    editMessage.Description = descripcion;
                    client = new ConfigurationServiceClient();
                    client.SaveRoute(editMessage);
                    this.txtDescripcion.Text = "";
                    this.txtDestino.Text = "";
                    this.txtSoapAction.Text = "";
                    this.bEditing = false;
                    this.editMessage = null;
                    this.tabPage2.Text = "Agregar";
                    this.LoadRoutingTable();
                    this.tabControl1.SelectedIndex = 0;
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex) {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally {
                    if (client != null && client.State == System.ServiceModel.CommunicationState.Opened) client.Close();
                }
            }
        }

        private void AddRoute(string accion, string destino, string descripcion) {
            if (DialogResult.Yes == MessageBox.Show("¿Guardar la nueva ruta? Recuerde que los servicios se reiniciarán.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                this.Cursor = Cursors.WaitCursor;
                ConfigurationServiceClient client = null;
                try {
                    RouteMessage message = new RouteMessage();
                    message.SoapAction = accion;
                    message.Destination = destino;
                    message.Description = descripcion;
                    client = new ConfigurationServiceClient();
                    client.AddRoute(message);
                    this.LoadRoutingTable();
                    this.txtDescripcion.Text = "";
                    this.txtDestino.Text = "";
                    this.txtSoapAction.Text = "";
                    this.tabControl1.SelectedIndex = 0;
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex) {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally {
                    if (client != null && client.State == System.ServiceModel.CommunicationState.Opened) client.Close();
                }
            }
        }

        private void deleteRowItem_Click(object sender, EventArgs e) {
            int rowIndex = this.gridRuteo.SelectedRows[0].Index;
            RouteMessage message = ((RouteMessage)this.gridRuteo.Rows[rowIndex].DataBoundItem);                
            if (DialogResult.Yes == MessageBox.Show(string.Format("¿Eliminar la configuración de la ruta {0}? El servicio de ruteo será afectado",message.SoapAction), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                ConfigurationServiceClient client = null;
                try {
                    this.Cursor = Cursors.WaitCursor;
                    client = new ConfigurationServiceClient();
                    client.DeleteRoute(message.IDRoute);
                    LoadRoutingTable();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex) {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally {
                    if (client != null && client.State == System.ServiceModel.CommunicationState.Opened) client.Close();
                }
            }
        }

        private void frmConfiguration_FormClosing(object sender, FormClosingEventArgs e) {
        }

        private void editRowItem_Click(object sender, EventArgs e) {
            int rowIndex = this.gridRuteo.SelectedRows[0].Index;
            editMessage = ((RouteMessage)this.gridRuteo.Rows[rowIndex].DataBoundItem);
            this.txtSoapAction.Text = editMessage.SoapAction;
            this.txtDestino.Text = editMessage.Destination;
            this.txtDescripcion.Text = editMessage.Description;            
            this.tabControl1.SelectedIndex = 1;
            this.tabPage2.Text = "Edición";
            bEditing = true;
        }

         private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e) {
            if (bEditing && e.Action == TabControlAction.Selecting && e.TabPageIndex == 0) {
                if (MessageBox.Show("¿Cancelar los cambios?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                    this.txtSoapAction.Text = "";
                    this.txtDestino.Text = "";
                    this.txtDescripcion.Text = "";
                    bEditing = false;
                    this.tabPage2.Text = "Agregar";
                    e.Cancel = false;
                }
                else {
                    e.Cancel = true;
                }
            }
        }
    }
}