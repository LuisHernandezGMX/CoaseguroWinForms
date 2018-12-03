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
    }
}
