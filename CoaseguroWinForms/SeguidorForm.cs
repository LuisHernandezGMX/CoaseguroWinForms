using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistran.Data;

namespace CoaseguroWinForms
{
    public partial class SeguidorForm : Form
    {
        #region Variables Privadas

        /// <summary>
        /// Cadena de conexión del SII.
        /// </summary>
        private string sCommand;

        /// <summary>
        /// El Id de la póliza a editar.
        /// </summary>
        int idPv;
        #endregion

        /// <summary>
        /// Crea un nuevo formulario con GMX como coaseguradora seguidora.
        /// </summary>
        /// <param name="sCommand">Cadena de conexión para el SII.</param>
        /// <param name="idPv">El Id de la póliza a editar.</param>
        public SeguidorForm(string sCommand, int idPv)
        {
            this.sCommand = sCommand;
            this.idPv = idPv;

            InitializeComponent();

            // Conexión SII
            var connection = new Conecction();
            connection.GetStringConnection(sCommand);

            lblNombreUsuario.Text = connection.User;
            lblEntorno.Text = connection.Base;
        }

        /// <summary>
        /// Llena el formulario con la información inicial traída de la base de datos.
        /// </summary>
        private void InicializarInformacionFormulario()
        {

        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el monto del límite máximo de
        /// responsabilidad al 100%. Solamente deja entrar números decimales en la caja de texto,
        /// y al mismo tiempo calcula todos los montos del formulario que tomen como base este valor.
        /// </summary>
        /// <param name="sender">La caja de texto donde se escribe el monto.</param>
        /// <param name="e">>Contiene el valor de la tecla que fue presionada.</param>
        private void txtLimiteMaxResponsabilidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.Contains(".")) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && string.IsNullOrWhiteSpace((sender as TextBox).Text)) {
                e.Handled = true;
            }
        }

        private void txtLimiteMaxResponsabilidad_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            var monto = string.IsNullOrWhiteSpace(textBox.Text)
                ? 0M
                : decimal.Parse(textBox.Text);

            if (monto < 0M) {
                MessageBox.Show(this, "El monto debe ser mayor a $ 0.00", "Monto Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // TODO: Actualización de todos los observadores y vista modelo a 0.
            } else {
                // TODO: Actualización de los obervadores y del vista modelo con sus nuevos valores.
            }
        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el porcentaje del Fee por administración
        /// de GMX. Solamente deja entrar números decimales y al mismo tiempo calcula el monto de Fee de
        /// GMX.
        /// </summary>
        /// <param name="sender">La caja de texto donde se escribe el porcentaje del Fee.</param>
        /// <param name="e">La tecla que fue presionada.</param>
        private void txtPorcentajeFeeGMX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.Contains(".")) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && string.IsNullOrWhiteSpace((sender as TextBox).Text)) {
                e.Handled = true;
            }
        }

        private void txtPorcentajeFeeGMX_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            var porcentaje = string.IsNullOrWhiteSpace(textBox.Text)
                ? 0M
                : decimal.Parse(textBox.Text);

            if (porcentaje < 0M || porcentaje > 100M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // TODO: Actualización del vista modelo
                lblMontoFeeGMX.Text = "$0.00";
                textBox.Clear();
            } else {
                // TODO: Cálculo del monto de Fee y actualización del vista modelo.
                lblMontoFeeGMX.Text = $"$ {porcentaje.ToString("N2")}";
            }
        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el porcentaje de participación de GMX. Solamente
        /// deja entrar números decimales y al mismo tiempo calcula el monto de participación de GMX y actualiza
        /// los montos del formulario que tomen como base este valor.
        /// </summary>
        /// <param name="sender">La caja de texto donde se escribe el porcentaje de participación.</param>
        /// <param name="e">La tecla que fue presionada.</param>
        private void txtParticipacionGMX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.Contains(".")) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && string.IsNullOrWhiteSpace((sender as TextBox).Text)) {
                e.Handled = true;
            }
        }

        private void txtParticipacionGMX_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            var porcentaje = string.IsNullOrWhiteSpace(textBox.Text)
                ? 0M
                : decimal.Parse(textBox.Text);

            if (porcentaje < 0M || porcentaje > 100M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // TODO: Actualización del vista modelo y observadores
                lblMontoGMX.Text = "$ 0.00";
                textBox.Clear();
            } else {
                // TODO: Cálculo del monto de Fee y actualización del vista modelo y observadores.
                lblMontoGMX.Text = $"$ {porcentaje.ToString("N2")}";
            }
        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el porcentaje para el monto máximo de pago
        /// de siniestro. Solamente deja entrar números decimales y al mismo tiempo calculo el monto correspondiente.
        /// </summary>
        /// <param name="sender">La caja de texto donde se escribe el porcentaje del monto del siniestro a pagar.</param>
        /// <param name="e">La tecla que fue presionada.</param>
        private void txtPorcentajeSiniestro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.Contains(".")) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && string.IsNullOrWhiteSpace((sender as TextBox).Text)) {
                e.Handled = true;
            }
        }

        private void txtPorcentajeSiniestro_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            var porcentaje = string.IsNullOrWhiteSpace(textBox.Text)
                ? 0M
                : decimal.Parse(textBox.Text);

            if (porcentaje < 0M || porcentaje > 100M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // TODO: Actualización del vista modelo
                lblMontoSiniestro.Text = "$ 0.00";
                textBox.Clear();
            } else {
                // TODO: Cálculo del monto de siniestro y actualización del vista modelo.
                lblMontoSiniestro.Text = $"$ {porcentaje.ToString("N2")}";
            }
        }

        private void rdbSiniestroParticipacion_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            // TODO: Actualizar vista modelo
            if (radio.Checked) {
                lblMontoSiniestro.Text = "$ 0.00";
                txtPorcentajeSiniestro.Clear();
                txtPorcentajeSiniestro.Enabled = false;
            } else {
                txtPorcentajeSiniestro.Enabled = true;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSuspender_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rdbContratoSeguro_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            // TODO: Actualizar vista modelo.
            if (radio.Checked) {
                cmbGarantiaPago.Enabled = false;
            } else {
                cmbGarantiaPago.Enabled = true;
            }
        }
    }
}
