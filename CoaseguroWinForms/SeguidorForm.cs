using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Sistran.Data;
using CoaseguroWinForms.DAL.Entities;
using CoaseguroWinForms.DAL.ViewModels.Seguidor;

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
        private int idPv;

        /// <summary>
        /// Almacena todo el estado del formulario.
        /// </summary>
        private SeguidorViewModel model;

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
            InicializarInformacionFormulario();
            InicializarCmbGarantiaPago();

            // Conexión SII
            var connection = new Conecction();
            connection.GetStringConnection(sCommand);

            lblNombreUsuario.Text = connection.User;
            lblEntorno.Text = connection.Base;
        }

        /// <summary>
        /// Llena el ComboBox de los días para la garantía de pago con los
        /// valores necesarios.
        /// </summary>
        private void InicializarCmbGarantiaPago()
        {
            cmbGarantiaPago.DisplayMember = "Name";
            cmbGarantiaPago.ValueMember = "ValorEnum";
            cmbGarantiaPago.DataSource = Enum
                .GetValues(typeof(DiasGarantiaPago))
                .Cast<DiasGarantiaPago>()
                .Where(valor => valor != DiasGarantiaPago.TreintaDias)
                .Select(valor => new {
                    (Attribute.GetCustomAttribute(valor.GetType().GetField(valor.ToString()), typeof(DisplayAttribute)) as DisplayAttribute).Name,
                    ValorEnum = valor
                })
                .OrderBy(v => v.ValorEnum)
                .ToList();
        }

        /// <summary>
        /// Llena el formulario con la información inicial traída de la base de datos.
        /// </summary>
        private void InicializarInformacionFormulario()
        {
            // ViewModel de Prueba (el real se llena con los datos de la base de datos.)
            model = new SeguidorViewModel {
                LimiteMaxResponsabilidad = 1000000,
                PrimaNeta = 200000,
                GMX = new DAL.ViewModels.GMXViewModel(),
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100,
                PorcentajePagoSiniestro = null,
                GarantiaPago = DiasGarantiaPago.TreintaDias,
                Lider = new CoaseguradoraLiderViewModel {
                    Nombre = "SEGUROS BANORTE GENERALI, S.A. DE C.V., GRUPO FINANCIERO BANORTE",
                    PorcentajeParticipacion = 80M,
                }
            };

            // Límite Máximo y Prima Neta
            lblLimiteMaximoResponsabilidad.Text = $"$ {model.LimiteMaxResponsabilidad.ToString("N2")}";
            lblPrimaNeta.Text = $"$ {model.PrimaNeta.ToString("N2")}";

            // GMX
            model.GMX.Porcentaje = 100M - model.Lider.PorcentajeParticipacion;
            model.GMX.MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * model.GMX.Porcentaje / 100M, 2);
            model.GMX.MontoPrimaNeta = decimal.Round(model.PrimaNeta * model.GMX.Porcentaje / 100M, 2);

            var porcentajeGMX = $"{model.GMX.Porcentaje.ToString("N2")} %";
            var montoGMX = $"$ {model.GMX.MontoParticipacion.ToString("N2")}";
            var primaNetaGMX = $"$ {model.GMX.MontoPrimaNeta.ToString("N2")}";

            lblMontoGMX.Text = montoGMX;
            lblPorcentajeGMX.Text = porcentajeGMX;
            lblMontoPrimaNetaGMX.Text = primaNetaGMX;

            // Coaseguradora Líder
            model.Lider.MontoPrimaNeta = decimal.Round(model.PrimaNeta * model.Lider.PorcentajeParticipacion / 100M, 2);
            model.Lider.MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * model.Lider.PorcentajeParticipacion / 100M, 2);

            var montoLider = $"$ {model.Lider.MontoParticipacion.ToString("N2")}";
            var primaNetaLider = $"$ {model.Lider.MontoPrimaNeta.ToString("N2")}";
            var porcentajeLider = $"{model.Lider.PorcentajeParticipacion.ToString("N2")} %";

            lblCoaseguradoraLider.Text = model.Lider.Nombre;
            lblMontoCoaseguradoraLider.Text = montoLider;
            lblMontoPrimaNetaCoaseguradoraLider.Text = primaNetaLider;
            lblParticipacionCoaseguradoraLider.Text = porcentajeLider;

            // Total de Participación
            var porcentajeTotal = model.GMX.Porcentaje + model.Lider.PorcentajeParticipacion;
            var montoTotal = model.GMX.MontoParticipacion + model.Lider.MontoParticipacion;
            var primaNetaTotal = model.GMX.MontoPrimaNeta + model.Lider.MontoPrimaNeta;

            lblMontoTotalParticipacion.Text = $"$ {montoTotal.ToString("N2")}";
            lblPrimaNetaTotalParticipacion.Text = $"$ {primaNetaTotal.ToString("N2")}";
            lblPorcentajeTotalParticipacion.Text = $"{porcentajeTotal.ToString("N2")} %";
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
            decimal porcentaje;
            var textBox = sender as TextBox;
            decimal.TryParse(textBox.Text, out porcentaje);

            if (porcentaje < 0M || porcentaje > 100M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                model.PorcentajeFeeGMX = model.MontoFeeGMX = 0M;
                lblMontoFeeGMX.Text = "$0.00";
                textBox.Clear();
            } else {
                model.PorcentajeFeeGMX = porcentaje;
                model.MontoFeeGMX = decimal.Round(porcentaje * model.PrimaNeta / 100M, 2);
                lblMontoFeeGMX.Text = $"$ {model.MontoFeeGMX.ToString("N2")}";
            }
        }

        /// <summary>
        /// Asigna el nuevo valor del Método de Pago al vista modelo
        /// cada vez que se selecciona un RadioButton.
        /// </summary>
        /// <param name="sender">El RadioButton del estado de cuenta en la sección Método de Pago.</param>
        /// <param name="e">No se utiliza.</param>
        private void rdbEstadoCuenta_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            model.MetodoPago = radio.Checked
                ? MetodoPago.EstadoCuenta
                : MetodoPago.Conceptos;
        }

        /// <summary>
        /// Asigna el nuevo valor del Pago de Comisión al Agente al vista modelo
        /// cada vez que se selecciona un RadioButton.
        /// </summary>
        /// <param name="sender">El RadioButton del líder al 100% en la sección Pago de Comisión al Agente.</param>
        /// <param name="e">No se utiliza.</param>
        private void rdbLider100_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            model.PagoComisionAgente = radio.Checked
                ? PagoComisionAgente.Lider100
                : PagoComisionAgente.Participacion;
        }

        private void rdbSiniestroParticipacion_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            if (radio.Checked) {
                model.PorcentajePagoSiniestro = null;
                lblMontoSiniestro.Text = "$ 0.00";
                txtPorcentajeSiniestro.Clear();
                txtPorcentajeSiniestro.Enabled = false;
            } else {
                model.PorcentajePagoSiniestro = 0M;
                txtPorcentajeSiniestro.Enabled = true;
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
            decimal porcentaje;
            var textBox = sender as TextBox;
            decimal.TryParse(textBox.Text, out porcentaje);

            if (porcentaje < 0M || porcentaje > 100M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                model.PorcentajePagoSiniestro = 0M;
                lblMontoSiniestro.Text = "$ 0.00";
                textBox.Clear();
            } else {
                model.PorcentajePagoSiniestro = porcentaje;
                var monto = decimal.Round(porcentaje * model.GMX.MontoParticipacion / 100M, 2);
                lblMontoSiniestro.Text = $"$ {monto.ToString("N2")}";
            }
        }

        /// <summary>
        /// Activa y desactiva el ComboBox para la Garantía de Pago y asigna el valor correspondiente
        /// al VistaModelo del formulario.
        /// </summary>
        /// <param name="sender">El RadioButton que originó el evento.</param>
        /// <param name="e">No es utilizado.</param>
        private void rdbContratoSeguro_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            if (radio.Checked) {
                model.GarantiaPago = DiasGarantiaPago.TreintaDias;
                cmbGarantiaPago.Enabled = false;
            } else {
                model.GarantiaPago = (DiasGarantiaPago)cmbGarantiaPago.SelectedValue;
                cmbGarantiaPago.Enabled = true;
            }
        }

        /// <summary>
        /// Asigna el nuevo valor de los días de Garantía de Pago al vista modelo
        /// cada vez que se selecciona una opción del ComboBox.
        /// </summary>
        /// <param name="sender">El ComboBox de los días en la sección Garantía de Pago.</param>
        /// <param name="e">No se utiliza.</param>
        private void cmbGarantiaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            model.GarantiaPago = (DiasGarantiaPago)combo.SelectedValue;
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
    }
}