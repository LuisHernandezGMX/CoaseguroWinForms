using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        }

        /// <summary>
        /// Llena el formulario con la información inicial traída de la base de datos.
        /// </summary>
        private void InicializarInformacionFormulario()
        {
            // ViewModel de Prueba (el real se llena con los datos de la base de datos.)
            model = new LiderViewModel {
                LimiteMaxResponsabilidad = 1000000,
                PrimaNeta = 1000000,
                PorcentajeGMX = 70,
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100
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
        /// <param name="_">No utilizado.</param>
        /// <param name="e">El objeto editable al cuál se le agregará su función de retrollamada.</param>
        private void gridFee_EditingControlShowing(object _, DataGridViewEditingControlShowingEventArgs e)
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
                    var textBox = send as TextBox;
                    var porcentaje = string.IsNullOrWhiteSpace(textBox.Text)
                        ? 0M
                        : decimal.Parse(textBox.Text);

                    if (porcentaje > 100M || porcentaje < 0M) {
                        MessageBox.Show(this, "El porcentaje debe ser mayor a 0% y menor a 100%", "Porcentaje Fuera de Límite",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);;

                        textBox.Clear();
                        return;
                    }

                    var idCoaseguradora = (int)gridFee.CurrentCell.Tag;
                    var coaseguradora = model.Coaseguradoras.FirstOrDefault(coas => coas.Id == idCoaseguradora);

                    coaseguradora.PorcentajeFee = porcentaje;
                    coaseguradora.MontoFee = decimal.Round(model.PrimaNeta * porcentaje / 100, 2);

                    gridFee.CurrentCell.OwningRow.Cells[2].Value = $"$ {coaseguradora.MontoFee.ToString("N2")}";
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

        private void rdbEstadoCuenta_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            model.MetodoPago = radio.Checked
                ? MetodoPago.EstadoCuenta
                : MetodoPago.Conceptos;
        }

        private void rdbLider100_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;

            model.PagoComisionAgente = radio.Checked
                ? PagoComisionAgente.Lider100
                : PagoComisionAgente.Participacion;
        }
    }
}
