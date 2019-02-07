using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Sistran.Data;
using CoaseguroWinForms.DAL;
using CoaseguroWinForms.DAL.DAO.Lider;
using CoaseguroWinForms.DAL.ViewModels.Lider;

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

        /// <summary>
        /// Indica si es la primera vez que se registra un coaseguro
        /// en la tabla [CoaseguroPrincipal] relacionada a la póliza
        /// indicada.
        /// </summary>
        private bool esCoaseguroNuevo;

        /// <summary>
        /// Almacena todo el estado del formulario.
        /// </summary>
        private LiderViewModel model;

        /// <summary>
        /// Acceso a operaciones en capa de persistencia para este formulario.
        /// </summary>
        private LiderDao dao;

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

            var connection = new Conecction();
            connection.GetStringConnection(sCommand);

            try {
                dao = new LiderDao(idPv, connection.ConecctionSII);
                esCoaseguroNuevo = dao.EsCoaseguroNuevo();

                model = esCoaseguroNuevo
                    ? dao.RellenarNuevoModelo()
                    : dao.ActualizarYRellenarModelo();

                InitializeComponent();
                InicializarCmbGarantiaPago();
                InicializarInformacionFormulario();

                lblEntorno.Text = connection.Base;
                lblNombreUsuario.Text = connection.User;

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
            lblMontoIndemnizacion.Text = $"{model.Moneda.Simbolo} 0.00";

            // Características del Coaseguro
            lblPrimaNeta.Text = $"{model.Moneda.Simbolo} {model.PrimaNeta.ToString("N2")}";
            lblLimiteMaximoResponsabilidad.Text = $"{model.Moneda.Simbolo} {model.LimiteMaxResponsabilidad.ToString("N2")}";

            // Participación de GMX
            lblPorcentajeGMX.Text = $"{model.GMX.Porcentaje.ToString("N2")} %";
            lblMontoGMX.Text = $"{model.Moneda.Simbolo} {model.GMX.MontoParticipacion.ToString("N2")}";
            lblMontoPrimaNetaGMX.Text = $"{model.Moneda.Simbolo} {model.GMX.MontoPrimaNeta.ToString("N2")}";

            model.Coaseguradoras.ForEach(coas => {
                // Grid Coaseguradoras
                gridCoaseguradoras
                    .Rows
                    .Add(
                        coas.Nombre,
                        $"{coas.PorcentajeParticipacion.ToString("N2")} %",
                        $"{model.Moneda.Simbolo} {coas.MontoParticipacion.ToString("N2")}",
                        $"{model.Moneda.Simbolo} {coas.MontoPrimaNeta.ToString("N2")}");

                // Grid Fee
                gridFee
                    .Rows
                    .Add(coas.Nombre, string.Empty, $"{model.Moneda.Simbolo} 0.00");
            });

            // Se agrega el Id a la celda de edición para identificar más fácilmente a la coaseguradora.
            int i = 0;
            foreach (DataGridViewRow row in gridFee.Rows) {
                var celda = row.Cells[1];
                celda.Tag = model.Coaseguradoras[i++].Id;
            }

            // Total de Participación
            var totalPorcentaje = model
                .Coaseguradoras
                .Select(coas => coas.PorcentajeParticipacion)
                .Sum();

            var totalMonto = model
                .Coaseguradoras
                .Select(coas => coas.MontoParticipacion)
                .Sum();

            var totalPrimaNeta = model
                .Coaseguradoras
                .Select(coas => coas.MontoPrimaNeta)
                .Sum();

            model.PorcentajeTotalParticipacion = totalPorcentaje + model.GMX.Porcentaje;
            model.MontoTotalParticipacion = totalMonto + model.GMX.MontoParticipacion;
            model.MontoPrimaNetaTotalParticipacion = totalPrimaNeta + model.GMX.MontoPrimaNeta;

            lblPorcentajeCoaseguradoras.Text = $" {model.PorcentajeTotalParticipacion.ToString("N2")} %";
            lblMontoCoaseguradoras.Text = $"{model.Moneda.Simbolo} {model.MontoTotalParticipacion.ToString("N2")}";
            lblPrimaNetaTotalParticipacion.Text = $"{model.Moneda.Simbolo} {model.MontoPrimaNetaTotalParticipacion.ToString("N2")}";
        }

        /// <summary>
        /// Actualiza los campos necesarios del formulario cuando el coaseguro ya existe.
        /// </summary>
        private void ActualizarFormulario()
        {
            // Fee de Coaseguradoras
            int i = 0;
            foreach (DataGridViewRow row in gridFee.Rows) {
                var porcentajeFee = row.Cells[1];
                var montoFee = row.Cells[2];

                porcentajeFee.Value = model.Coaseguradoras[i].PorcentajeFee.ToString();
                montoFee.Value = $"{model.Moneda.Simbolo} {model.Coaseguradoras[i].MontoFee.ToString("N2")}";
                i++;
            }

            // Siniestros
            if (model.PagoSiniestro == PagoSiniestro.CienPorCiento) {
                var porcentajeSiniestro = model.PorcentajePagoSiniestro.Value;
                var montoSiniestro = model.MontoSiniestro.Value;
                var formaIndemnizacion = model.FormaIndemnizacion;

                rdbSiniestro100.Checked = true;
                rdbSiniestroParticipacion.Checked = false;

                model.MontoSiniestro = montoSiniestro;
                model.PorcentajePagoSiniestro = porcentajeSiniestro;
                model.FormaIndemnizacion = formaIndemnizacion;

                lblPorcentajeIndemnizacion.Text = $"{model.PorcentajePagoSiniestro?.ToString("N2")} %";
                lblMontoIndemnizacion.Text = $"{model.Moneda.Simbolo} {model.MontoSiniestro?.ToString("N2")}";
                txtMontoIndemnizacion.Text = (model.FormaIndemnizacion == IndemnizacionSiniestro.Porcentaje)
                    ? model.PorcentajePagoSiniestro.ToString()
                    : model.MontoSiniestro.ToString();

                if (model.FormaIndemnizacion == IndemnizacionSiniestro.Monto) {
                    rdbPorcentajeIndemnizacion.Checked = false;
                    rdbMontoIndemnizacion.Checked = true;
                }
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
        /// Se dispara este evento cuando se empieza a escribir el porcentaje de Fee de cada
        /// coaseguradora. Solamente deja entrar números decimales en la caja de texto, y al
        /// mismo tiempo calcula el monto de Fee correspondiente.
        /// </summary>
        /// <param name="sender">No utilizado.</param>
        /// <param name="e">El objeto editable al cuál se le agregará su función de retrollamada.</param>
        private void gridFee_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!eventoFeeActivado) {
                eventoFeeActivado = true;
                var control = e.Control as DataGridViewTextBoxEditingControl;

                // Se impide la entrada de caracteres alfabéticos y/o dobles puntos decimales.
                control.KeyPress += (send, ev) => {
                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && (ev.KeyChar != '.')) {
                        ev.Handled = true;
                    }

                    if ((ev.KeyChar == '.') && (send as TextBox).Text.Contains(".")) {
                        ev.Handled = true;
                    }
                };

                // Una vez que la caja de texto tiene la cantidad completa, se realiza el cálculo del Fee correspondiente.
                control.KeyUp += (send, ev) => {
                    decimal porcentaje;
                    var textBox = send as TextBox;
                    decimal.TryParse(textBox.Text, out porcentaje);

                    var idCoaseguradora = (int)gridFee.CurrentCell.Tag;
                    var coaseguradora = model.Coaseguradoras.FirstOrDefault(coas => coas.Id == idCoaseguradora);
                    var lblMonto = gridFee.CurrentCell.OwningRow.Cells[2];

                    if (porcentaje > 100M || porcentaje < 0M) {
                        MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        coaseguradora.PorcentajeFee = coaseguradora.MontoFee = 0M;
                        lblMonto.Value = $"{model.Moneda.Simbolo} 0.00";
                        textBox.Clear();
                    } else {
                        coaseguradora.PorcentajeFee = porcentaje;
                        coaseguradora.MontoFee = decimal.Round(model.PrimaNeta * porcentaje / 100, 2);
                        lblMonto.Value = $"{model.Moneda.Simbolo} {coaseguradora.MontoFee.ToString("N2")}";
                    }
                };
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
                model.PagoSiniestro = PagoSiniestro.Participacion;
                model.PorcentajePagoSiniestro = model.MontoSiniestro = null;
                model.FormaIndemnizacion = null;

                rdbPorcentajeIndemnizacion.Enabled = false;
                rdbMontoIndemnizacion.Enabled = false;
                txtMontoIndemnizacion.Clear();
                txtMontoIndemnizacion.Enabled = false;
                lblPorcentajeIndemnizacion.Text = $"{model.Moneda.Simbolo} 0.00";
                lblMontoIndemnizacion.Text = $"0.00 %";
            } else {
                model.PagoSiniestro = PagoSiniestro.CienPorCiento;
                model.PorcentajePagoSiniestro = model.MontoSiniestro = 0M;
                model.FormaIndemnizacion = rdbPorcentajeIndemnizacion.Checked
                    ? IndemnizacionSiniestro.Porcentaje
                    : IndemnizacionSiniestro.Monto;

                rdbPorcentajeIndemnizacion.Enabled = true;
                rdbMontoIndemnizacion.Enabled = true;
                txtMontoIndemnizacion.Enabled = true;
            }
        }

        /// <summary>
        /// Cambia la forma de calcular las cantidades de indemnización del siniestro.
        /// </summary>
        /// <param name="sender">El RadioButton que originó el evento.</param>
        /// <param name="e">No se utiliza.</param>
        private void rdbPorcentajeIndemnizacion_CheckedChanged(object sender, EventArgs e)
        {
            model.FormaIndemnizacion = rdbPorcentajeIndemnizacion.Checked
                    ? IndemnizacionSiniestro.Porcentaje
                    : IndemnizacionSiniestro.Monto;

            txtMontoIndemnizacion_KeyUp(txtMontoIndemnizacion, null);
        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el porcentaje del límite
        /// máximo de indemnización. Solamente deja entrar números
        /// decimales en la caja de texto, y al mismo tiempo calcula el monto/porcentaje
        /// de acuerdo a los RadioButton correspondientes.
        /// </summary>
        /// <param name="sender">La caja de texto donde se escribe el límite máximo.</param>
        /// <param name="e">Contiene el valor de la tecla que fue presionada.</param>
        private void txtMontoIndemnizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.Contains(".")) {
                e.Handled = true;
            }
        }

        private void txtMontoIndemnizacion_KeyUp(object sender, KeyEventArgs e)
        {
            decimal monto;
            var textBox = sender as TextBox;
            decimal.TryParse(textBox.Text, out monto);

            if (rdbPorcentajeIndemnizacion.Checked) {
                // Se realiza el cálculo por porcentaje.
                if (monto > 100M || monto < 0M) {
                    MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    lblPorcentajeIndemnizacion.Text = "0.00 %";
                    lblMontoIndemnizacion.Text = $"{model.Moneda.Simbolo} 0.00";
                    model.PorcentajePagoSiniestro = model.MontoSiniestro = 0M;
                    textBox.Clear();
                } else {
                    if (!textBox.Text.EndsWith(".")) {
                        monto = decimal.Round(monto, 2);
                        textBox.Text = (monto == 0M)
                            ? string.Empty
                            : monto.ToString();
                    }
                    
                    model.PorcentajePagoSiniestro = monto;
                    model.MontoSiniestro = decimal.Round(model.LimiteMaxResponsabilidad * monto / 100M, 2);
                    
                    lblPorcentajeIndemnizacion.Text = $"{model.PorcentajePagoSiniestro?.ToString("N2")} %";
                    lblMontoIndemnizacion.Text = $"{model.Moneda.Simbolo} {model.MontoSiniestro?.ToString("N2")}";
                }
            } else {
                // Se realiza el cálculo por monto.
                decimal porcentaje = decimal.Round(monto * 100M / model.LimiteMaxResponsabilidad);

                if (porcentaje > 100M || porcentaje < 0M) {
                    MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    lblPorcentajeIndemnizacion.Text = "0.00 %";
                    lblMontoIndemnizacion.Text = $"{model.Moneda.Simbolo} 0.00";
                    model.PorcentajePagoSiniestro = model.MontoSiniestro = 0M;
                    textBox.Clear();
                } else {
                    model.PorcentajePagoSiniestro = porcentaje;
                    model.MontoSiniestro = monto;

                    lblPorcentajeIndemnizacion.Text = $"{porcentaje.ToString("N2")} %";
                    lblMontoIndemnizacion.Text = $"{model.Moneda.Simbolo} {monto.ToString("N2")}";
                }
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