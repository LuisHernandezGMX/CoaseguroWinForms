using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Sistran.Data;
using CoaseguroWinForms.DAL;
using CoaseguroWinForms.DAL.DAO.Seguidor;
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
        /// Indica si es la primera vez que se registra un coaseguro
        /// en la tabla [CoaseguroPrincipal] relacionada a la póliza
        /// indicada.
        /// </summary>
        private bool esCoaseguroNuevo;

        /// <summary>
        /// Almacena todo el estado del formulario.
        /// </summary>
        private SeguidorViewModel model;

        /// <summary>
        /// Almacena todo el estado del formulario
        /// </summary>
        private SeguidorDao dao;

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

            var connection = new Conecction();
            connection.GetStringConnection(sCommand);

            try {
                dao = new SeguidorDao(idPv, connection.ConecctionSII);
                esCoaseguroNuevo = dao.EsCoaseguroNuevo();

                model = esCoaseguroNuevo
                    ? dao.RellenarNuevoModelo()
                    : dao.ActualizarYRellenarModelo();

                InitializeComponent();
                InicializarCmbGarantiaPago();
                InicializarInformacionFormulario();

                lblNombreUsuario.Text = connection.User;
                lblEntorno.Text = connection.Base;

                if (!esCoaseguroNuevo) {
                    ActualizarFormulario();
                }
            } catch (Exception ex) {
                var mensaje = ex.InnerException?.InnerException?.Message ?? ex.Message;

                MessageBox.Show(this,
                    $"Ocurrió un error al leer la póliza de la base de datos.\n\n\nERROR: {mensaje}\n\nUBICACIÓN: {ex.TargetSite}",
                    "Error al Leer Póliza",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Llena el ComboBox de los días para la garantía de pago con los
        /// valores necesarios.
        /// </summary>
        private void InicializarCmbGarantiaPago()
        {
            // Efecto Secundario.
            // Al asignar la enumeración al DataSource de cmbGarantiaPago se reinicia por alguna razón la propiedad del VistaModelo.
            // Referencia: https://stackoverflow.com/questions/641809/displaymember-getting-reset-on-datasource-null
            var garantiaPago = model.GarantiaPago;

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

            model.GarantiaPago = garantiaPago;
        }

        /// <summary>
        /// Llena el formulario con la información inicial traída de la base de datos.
        /// </summary>
        private void InicializarInformacionFormulario()
        {
            // Etiqueta Monto Siniestro
            lblMontoSiniestro.Text = $"{model.Moneda.Simbolo} 0.00";

            // Límite Máximo y Prima Neta
            lblLimiteMaximoResponsabilidad.Text = $"{model.Moneda.Simbolo} {model.LimiteMaxResponsabilidad.ToString("N2")}";
            lblPrimaNeta.Text = $"{model.Moneda.Simbolo} {model.PrimaNeta.ToString("N2")}";

            // GMX
            var porcentajeGMX = $"{model.GMX.Porcentaje.ToString("N2")} %";
            var montoGMX = $"{model.Moneda.Simbolo} {model.GMX.MontoParticipacion.ToString("N2")}";
            var primaNetaGMX = $"{model.Moneda.Simbolo} {model.GMX.MontoPrimaNeta.ToString("N2")}";

            lblMontoGMX.Text = montoGMX;
            lblPorcentajeGMX.Text = porcentajeGMX;
            lblMontoPrimaNetaGMX.Text = primaNetaGMX;

            // Coaseguradora Líder
            var montoLider = $"{model.Moneda.Simbolo} {model.Lider.MontoParticipacion.ToString("N2")}";
            var primaNetaLider = $"{model.Moneda.Simbolo} {model.Lider.MontoPrimaNeta.ToString("N2")}";
            var porcentajeLider = $"{model.Lider.PorcentajeParticipacion.ToString("N2")} %";

            lblCoaseguradoraLider.Text = model.Lider.Nombre;
            lblMontoCoaseguradoraLider.Text = montoLider;
            lblMontoPrimaNetaCoaseguradoraLider.Text = primaNetaLider;
            lblParticipacionCoaseguradoraLider.Text = porcentajeLider;

            // Total de Participación
            model.MontoTotalParticipacion = model.GMX.MontoParticipacion + model.Lider.MontoParticipacion;
            model.MontoPrimaNetaTotalParticipacion = model.GMX.MontoPrimaNeta + model.Lider.MontoPrimaNeta;
            model.PorcentajeTotalParticipacion = model.GMX.Porcentaje + model.Lider.PorcentajeParticipacion;

            lblPorcentajeTotalParticipacion.Text = $"{model.PorcentajeTotalParticipacion.ToString("N2")} %";
            lblMontoTotalParticipacion.Text = $"{model.Moneda.Simbolo} {model.MontoTotalParticipacion.ToString("N2")}";
            lblPrimaNetaTotalParticipacion.Text = $"{model.Moneda.Simbolo} {model.MontoPrimaNetaTotalParticipacion.ToString("N2")}";
        }

        /// <summary>
        /// Actualiza los campos necesarios del formulario cuando el coaseguro ya existe.
        /// </summary>
        private void ActualizarFormulario()
        {
            // Fee de GMX
            txtPorcentajeFeeGMX.Text = model.PorcentajeFeeGMX.ToString();
            lblMontoFeeGMX.Text = $"{model.Moneda.Simbolo} {model.MontoFeeGMX.ToString("N2")}";

            // Siniestros
            if (model.PagoSiniestro == PagoSiniestro.CienPorCiento) {
                var porcentajeSiniestro = model.PorcentajePagoSiniestro.Value;
                var montoSiniestro = model.MontoSiniestro.Value;

                rdbSiniestro100.Checked = true;
                rdbSiniestroParticipacion.Checked = false;
                txtPorcentajeSiniestro.Text = porcentajeSiniestro.ToString();
                lblMontoSiniestro.Text = $"{model.Moneda.Simbolo} {montoSiniestro.ToString("N2")}";

                model.MontoSiniestro = montoSiniestro;
                model.PorcentajePagoSiniestro = porcentajeSiniestro;
            }

            // Método de Pago, Pago de Comisión a Agente y Garantía de Pago
            if (model.MetodoPago == MetodoPago.Conceptos) {
                rdbPorConceptos.Checked = true;
                rdbEstadoCuenta.Checked = false;
            }

            if (model.PagoComisionAgente == PagoComisionAgente.Participacion) {
                rdbPagoParticipacion.Checked = true;
                rdbLider100.Checked = false;
            }

            if (model.GarantiaPago != DiasGarantiaPago.TreintaDias) {
                cmbGarantiaPago.SelectedValue = model.GarantiaPago;
                rdbGarantiaPagoOtro.Checked = true;
                rdbContratoSeguro.Checked = false;
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
            decimal porcentaje;
            var textBox = sender as TextBox;
            decimal.TryParse(textBox.Text, out porcentaje);

            if (porcentaje < 0M || porcentaje > 100M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                model.PorcentajeFeeGMX = model.MontoFeeGMX = 0M;
                lblMontoFeeGMX.Text = $"{model.Moneda.Simbolo} 0.00";
                textBox.Clear();
            } else {
                model.PorcentajeFeeGMX = porcentaje;
                model.MontoFeeGMX = decimal.Round(porcentaje * model.PrimaNeta / 100M, 2);
                lblMontoFeeGMX.Text = $"{model.Moneda.Simbolo} {model.MontoFeeGMX.ToString("N2")}";
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
                model.PagoSiniestro = PagoSiniestro.Participacion;
                model.PorcentajePagoSiniestro = model.MontoSiniestro = null;
                lblMontoSiniestro.Text = $"{model.Moneda.Simbolo} 0.00";
                txtPorcentajeSiniestro.Clear();
                txtPorcentajeSiniestro.Enabled = false;
            } else {
                model.PagoSiniestro = PagoSiniestro.CienPorCiento;
                model.PorcentajePagoSiniestro = model.MontoSiniestro = 0M;
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

                model.PorcentajePagoSiniestro = model.MontoSiniestro = 0M;
                lblMontoSiniestro.Text = $"{model.Moneda.Simbolo} 0.00";
                textBox.Clear();
            } else {
                model.PorcentajePagoSiniestro = porcentaje;
                model.MontoSiniestro = decimal.Round(porcentaje * model.GMX.MontoParticipacion / 100M, 2);
                lblMontoSiniestro.Text = $"{model.Moneda.Simbolo} {model.MontoSiniestro.Value.ToString("N2")}";
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
            btnSiguiente.Enabled = btnSuspender.Enabled = btnAtras.Enabled = false;

            try {
                if (esCoaseguroNuevo) {
                    dao.GuardarCoaseguro(model);
                } else {
                    dao.ActualizarCoaseguro(model);
                }

                MessageBox.Show(this, "Se ha guardado la información del coaseguro con éxito.", "Siguiente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            } catch (Exception ex) {
                var mensaje = ex.InnerException?.InnerException?.Message ?? ex.Message;

                MessageBox.Show(this,
                    $"Ocurrió un error al guardar el coaseguro.\n\n\nERROR: {mensaje}\n\nUBICACIÓN: {ex.TargetSite}",
                    "Error al Guardar Coaseguro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnSiguiente.Enabled = btnSuspender.Enabled = btnAtras.Enabled = true;
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(this,
                "¿Está seguro de que quiere regresar? Perderá todos los datos registrados en esta pantalla hasta el momento.",
                "Atrás",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK) {
                Close();
            }
        }

        private void btnSuspender_Click(object sender, EventArgs e)
        {
            btnSiguiente.Enabled = btnSuspender.Enabled = btnAtras.Enabled = false;

            try {
                if (esCoaseguroNuevo) {
                    dao.GuardarCoaseguro(model);
                } else {
                    dao.ActualizarCoaseguro(model);
                }

                MessageBox.Show(this, "Se ha guardado la información del coaseguro con éxito.", "Suspender", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            } catch (Exception ex) {
                var mensaje = ex.InnerException?.InnerException?.Message ?? ex.Message;
                MessageBox.Show(this,
                    $"Ocurrió un error al guardar el coaseguro.\n\n\nERROR: {mensaje}\n\nUBICACIÓN: {ex.TargetSite}",
                    "Error al Guardar Coaseguro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnSiguiente.Enabled = btnSuspender.Enabled = btnAtras.Enabled = true;
            }
        }
    }
}