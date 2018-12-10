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
        }
    }
}
