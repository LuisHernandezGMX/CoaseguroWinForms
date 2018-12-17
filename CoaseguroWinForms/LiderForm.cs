using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sistran.Data;
using CoaseguroWinForms.DAL.Entities;
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
        /// Almacena todo el estado del formulario.
        /// </summary>
        private LiderViewModel model;

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
            model = new LiderViewModel {
                LimiteMaxResponsabilidad = 1000000,
                PrimaNeta = 200000,
                PorcentajeGMX = 70,
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100,
                PorcentajePagoSiniestro = null,
                GarantiaPago = DiasGarantiaPago.TreintaDias
            };
            
            model.Coaseguradoras = new List<CoaseguradoraViewModel> {
                new CoaseguradoraViewModel {
                    Id = 1,
                    Nombre = "CHUBB DE MÉXICO, COMPAÑÍA DE SEGUROS",
                    PorcentajeParticipacion = 10,
                    MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * 10 / 100, 2)
                },
                new CoaseguradoraViewModel {
                    Id = 2,
                    Nombre = "EL ÁGUILA COMPAÑÍA DE SEGUROS, S.A.",
                    PorcentajeParticipacion = 5,
                    MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * 5 / 100, 2)
                },
                new CoaseguradoraViewModel {
                    Id = 3,
                    Nombre = "ACE SEGUROS, S.A.",
                    PorcentajeParticipacion = 5,
                    MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * 5 / 100, 2)
                },
                new CoaseguradoraViewModel {
                    Id = 4,
                    Nombre = "GENERAL DE SEGUROS, S.A.B.",
                    PorcentajeParticipacion = 10,
                    MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * 10 / 100, 2)
                }
            };

            // Características del Coaseguro
            lblLimiteMaximoResponsabilidad.Text = $"$ {model.LimiteMaxResponsabilidad.ToString("N2")}";
            lblPrimaNeta.Text = $"$ {model.PrimaNeta.ToString("N2")}";
            lblPorcentajeGMX.Text = $"{model.PorcentajeGMX.ToString("N2")}%";

            // Participación de GMX
            model.MontoGMX = decimal.Round(model.LimiteMaxResponsabilidad * model.PorcentajeGMX / 100, 2);
            lblMontoGMX.Text = $"$ {model.MontoGMX.ToString("N2")}";

            model.Coaseguradoras.ForEach(coas => {
                // Grid Coaseguradoras
                gridCoaseguradoras
                    .Rows
                    .Add(coas.Nombre, $"{coas.PorcentajeParticipacion.ToString("N2")}%", $"$ {coas.MontoParticipacion.ToString("N2")}");

                // Grid Fee
                gridFee
                    .Rows
                    .Add(coas.Nombre, string.Empty, "$ 0.00");
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

            lblPorcentajeCoaseguradoras.Text = $"{totalPorcentaje.ToString("N2")}%";
            lblMontoCoaseguradoras.Text = $"$ {totalMonto.ToString("N2")}";
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
                        lblMonto.Value = "$ 0.00";
                        textBox.Clear();
                    } else {
                        coaseguradora.PorcentajeFee = porcentaje;
                        coaseguradora.MontoFee = decimal.Round(model.PrimaNeta * porcentaje / 100, 2);
                        lblMonto.Value = $"$ {coaseguradora.MontoFee.ToString("N2")}";
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
                txtPorcentajeSiniestro.Text = string.Empty;
                txtPorcentajeSiniestro.Enabled = false;
                lblMontoSiniestro.Text = "$ 0.00";
                model.PorcentajePagoSiniestro = null;
            } else {
                txtPorcentajeSiniestro.Enabled = true;
                model.PorcentajePagoSiniestro = 0M;
            }
        }

        /// <summary>
        /// Se dispara este evento cuando se empieza a escribir el porcentaje del monto
        /// máximo para pago automático de siniestro. Solamente deja entrar números
        /// decimales en la caja de texto, y al mismo tiempo calcula el monto
        /// de siniestro correspondiente.
        /// </summary>
        /// <param name="sender">La caja de texto donde se escribe el porcentaje.</param>
        /// <param name="e">Contiene el valor de la tecla que fue presionada.</param>
        private void txtPorcentajeSiniestro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.Contains(".")) {
                e.Handled = true;
            }
        }

        private void txtPorcentajeSiniestro_KeyUp(object sender, KeyEventArgs e)
        {
            decimal porcentaje;
            var textBox = sender as TextBox;
            decimal.TryParse(textBox.Text, out porcentaje);

            if (porcentaje > 100M || porcentaje < 0M) {
                MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                lblMontoSiniestro.Text = "$ 0.00";
                model.PorcentajePagoSiniestro = 0M;
                textBox.Clear();
            } else {
                model.PorcentajePagoSiniestro = porcentaje;
                var monto = decimal.Round(model.LimiteMaxResponsabilidad * porcentaje / 100M, 2);
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