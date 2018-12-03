using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoaseguroWinForms
{
    public partial class LiderForm : Form
    {
        #region Variables Privadas

        /// <summary>
        /// Cadena de conexión del SII.
        /// </summary>
        private string sCommand;

        /// <summary>
        /// El Id de la póliza a editar.
        /// </summary>
        private int idPv;

        /// <summary>
        /// Indica si la función de retrollamada para el
        /// cálculo del Fee ya fue añadida al DataGridView
        /// correspondiente.
        /// </summary>
        private bool eventoFeeActivado = false;

        #endregion

        /// <summary>
        /// Crea un nuevo formulario con GMX como coaseguradora líder.
        /// </summary>
        /// <param name="sCommand">Cadena de conexión para el SII.</param>
        /// <param name="idPv">El Id de la póliza a editar.</param>
        public LiderForm(string sCommand, int idPv)
        {
            this.sCommand = sCommand;
            this.idPv = idPv;

            InitializeComponent();

            // Valores de Prueba
            gridCoaseguradoras.Rows.Add("CHUBB DE MÉXICO, COMPAÑÍA DE SEGUROS", "10%", "$ 100,000.00");
            gridCoaseguradoras.Rows.Add("EL ÁGUILA COMPAÑÍA DE SEGUROS, S.A.", "5%", "$ 50,000.00");
            gridCoaseguradoras.Rows.Add("ACE SEGUROS, S.A.", "10%", "$ 100,000.00");
            gridCoaseguradoras.Rows.Add("GENERAL DE SEGUROS, S.A.B.", "10%", "$ 100,000.00");


            gridFee.Rows.Add("CHUBB DE MÉXICO, COMPAÑÍA DE SEGUROS", "", "$ 0.00");
            gridFee.Rows.Add("EL ÁGUILA COMPAÑÍA DE SEGUROS, S.A.", "", "$ 0.00");
            gridFee.Rows.Add("ACE SEGUROS, S.A.", "", "$ 0.00");
            gridFee.Rows.Add("GENERAL DE SEGUROS, S.A.B.", "", "$ 0.00");
        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el porcentaje de Fee de cada
        /// coaseguradora. Solamente deja entrar números decimales en la caja de texto, y al
        /// mismo tiempo calcula el monto de Fee correspondiente.
        /// </summary>
        /// <param name="_">No utilizado.</param>
        /// <param name="e">El objeto editable al cuál se le agregará su función de retrollamada.</param>
        private void gridFee_EditingControlShowing(object _, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!eventoFeeActivado) {
                eventoFeeActivado = true;
                var control = e.Control as DataGridViewTextBoxEditingControl;

                control.KeyPress += (send, ev) => {
                    var textBox = send as DataGridViewTextBoxEditingControl;

                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && (ev.KeyChar != '.')) {
                        ev.Handled = true;
                    }

                    if ((ev.KeyChar == '.') && textBox.Text.Contains(".")) {
                        ev.Handled = true;
                    }
                };
            }
        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el porcentaje del monto
        /// máximo para pago automático de siniestro. Solamente deja entrar números
        /// decimales en la caja de texto, y al mismo tiempo calcula el monto
        /// de siniestro correspondiente.
        /// </summary>
        /// <param name="sender">La caja de texto donde se escribe el monto máximo.</param>
        /// <param name="e">Contiene el valor de la tecla que fue presionada.</param>
        private void txtMontoSiniestro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.Contains(".")) {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Activa y descativa la caja de texto del monto máximo de siniestro y reinicia el
        /// valor que contiene su etiqueta correspondiente.
        /// </summary>
        /// <param name="sender">El RadioButton que originó el evento.</param>
        /// <param name="e">No es utilizado.</param>
        private void rdbSiniestroParticipacion_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            if (radio.Checked) {
                txtMontoSiniestro.Text = string.Empty;
                txtMontoSiniestro.Enabled = false;
                lblMontoSiniestro.Text = "$ 0.00";
            } else {
                txtMontoSiniestro.Enabled = true;
            }
        }

        private void rdbContratoSeguro_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;
            cmbGarantiaPago.Enabled = !radio.Checked;
        }
    }
}
