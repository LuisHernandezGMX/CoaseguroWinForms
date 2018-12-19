using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Sistran.Data;
using CoaseguroWinForms.DAL.Entities;
using CoaseguroWinForms.Business.Observers;
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

        /// <summary>
        /// Subjeto principal, el cual propagará todos los cambios en el formulario.
        /// </summary>
        private LimiteMaximoResponsabilidadSubject sujetoLimiteMax;

        /// <summary>
        /// Sujeto-Observador de la participación de GMX.
        /// </summary>
        private GMXSubjectObserver gmxSubObs;

        /// <summary>
        /// Sujeto-Observador del monto de participación de la coaseguradora líder.
        /// </summary>
        private LiderMontoParticipacionSubjectObserver liderMontoSubObs;

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
            InicializarSujetosYObservadores();

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
                PrimaNeta = 200000,
                GMX = new DAL.ViewModels.GMXViewModel(),
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100,
                PorcentajePagoSiniestro = null,
                GarantiaPago = DiasGarantiaPago.TreintaDias,
                Lider = new CoaseguradoraLiderViewModel {
                    Nombre = "SEGUROS BANORTE GENERALI, S.A. DE C.V., GRUPO FINANCIERO BANORTE",
                    PorcentajeParticipacion = 80,
                    MontoParticipacion = 0
                }
            };

            model.Lider.MontoPrimaNeta = decimal.Round(model.PrimaNeta * model.Lider.PorcentajeParticipacion / 100M, 2);
            string primaNetaLider = $"$ {model.Lider.MontoPrimaNeta.ToString("N2")}";
            string porcentajeLider = $"{model.Lider.PorcentajeParticipacion.ToString("N2")} %";

            lblPrimaNeta.Text = $"$ {model.PrimaNeta.ToString("N2")}";

            lblCoaseguradoraLider.Text = model.Lider.Nombre;
            lblMontoPrimaNetaCoaseguradoraLider.Text = primaNetaLider;
            lblParticipacionCoaseguradoraLider.Text = porcentajeLider;

            lblPrimaNetaTotalParticipacion.Text = primaNetaLider;
            lblPorcentajeTotalParticipacion.Text = porcentajeLider;
        }

        /// <summary>
        /// Inicializa los sujetos y sujetos-observadores de este formulario.
        /// </summary>
        private void InicializarSujetosYObservadores()
        {
            sujetoLimiteMax = new LimiteMaximoResponsabilidadSubject();
            gmxSubObs = new GMXSubjectObserver(model, lblMontoGMX);
            liderMontoSubObs = new LiderMontoParticipacionSubjectObserver(model, lblMontoCoaseguradoraLider);
            var totalMontoSubObs = new TotalMontoParticipacionObserver(model, lblMontoTotalParticipacion, lblPorcentajeTotalParticipacion);

            sujetoLimiteMax.RegistrarObservador(gmxSubObs);
            sujetoLimiteMax.RegistrarObservador(liderMontoSubObs);
            gmxSubObs.RegistrarObservador(new SiniestroObserver(model, lblMontoSiniestro));
            gmxSubObs.RegistrarObservador(totalMontoSubObs);
            liderMontoSubObs.RegistrarObservador(totalMontoSubObs);
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
            decimal monto;
            var textBox = sender as TextBox;
            decimal.TryParse(textBox.Text, out monto);

            if (monto < 0M) {
                MessageBox.Show(this, "El monto debe ser mayor a $ 0.00", "Monto Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                model.LimiteMaxResponsabilidad = 0M;
            } else {
                model.LimiteMaxResponsabilidad = monto;
            }

            sujetoLimiteMax.Notificar(model.LimiteMaxResponsabilidad);
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
            decimal porcentaje;
            var textBox = sender as TextBox;
            decimal.TryParse(textBox.Text, out porcentaje);


            if (porcentaje < 0M || porcentaje > 100M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                model.GMX.MontoParticipacion = model.GMX.Porcentaje = model.GMX.MontoPrimaNeta = 0M;
                lblMontoGMX.Text = lblMontoPrimaNetaGMX.Text = "$ 0.00";
                textBox.Clear();
            } else if (porcentaje + model.Lider.PorcentajeParticipacion > 100M) {
                MessageBox.Show(this, "El porcentaje de participación total excede el 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                model.GMX.MontoParticipacion = model.GMX.Porcentaje = model.GMX.MontoPrimaNeta = 0M;
                lblMontoGMX.Text = lblMontoPrimaNetaGMX.Text = "$ 0.00";
                textBox.Clear();
            } else {
                model.GMX.Porcentaje = porcentaje;
                model.GMX.MontoPrimaNeta = decimal.Round(porcentaje * model.PrimaNeta / 100M, 2);
                model.GMX.MontoParticipacion = decimal.Round(porcentaje * model.LimiteMaxResponsabilidad / 100M, 2);

                lblMontoGMX.Text = $"$ {model.GMX.MontoParticipacion.ToString("N2")}";
                lblMontoPrimaNetaGMX.Text = $"$ {model.GMX.MontoPrimaNeta.ToString("N2")}";
            }

            gmxSubObs.Notificar(model.GMX.MontoParticipacion);
            model.MontoPrimaNetaTotalParticipacion = model.GMX.MontoPrimaNeta + model.Lider.MontoPrimaNeta;
            lblPrimaNetaTotalParticipacion.Text = $"$ {model.MontoPrimaNetaTotalParticipacion.ToString("N2")}";
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
