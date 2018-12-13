using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoaseguroWinForms.DAL.ViewModels.Seguidor;

namespace CoaseguroWinForms.Business.Observers
{
    /// <summary>
    /// Observador que representa el monto máximo para pago automático
    /// de siniestro. Actualiza su propio estado cada vez que hay un cambio
    /// en el monto de participacipon de GMX (observador de <see cref="GMXSubjectObserver"/>).
    /// </summary>
    class SiniestroObserver : IObservador<decimal>
    {
        #region Variables Privadas

        /// <summary>
        /// El vista modelo al cuál está enlazado éste objeto.
        /// </summary>
        private SeguidorViewModel modelo;

        /// <summary>
        /// Etiqueta del formulario que muestra el monto del siniestro a pagar.
        /// </summary>
        private Label lblMontoSiniestro;

        /// <summary>
        /// Id único de este observador;
        /// </summary>
        private Guid id;

        #endregion

        /// <summary>
        /// Id único de este observador.
        /// </summary>
        public Guid Id {
            get {
                return id;
            }
        }

        /// <summary>
        /// Crea un nuevo observador enlazado al vista modelo indicado.
        /// </summary>
        /// <param name="modelo">El vista modelo del formulario.</param>
        /// <param name="lblMontoSiniestro">La etiqueta que muestra el monto de siniestro a pagar.</param>
        public SiniestroObserver(SeguidorViewModel modelo, Label lblMontoSiniestro)
        {
            id = new Guid();
            this.modelo = modelo;
            this.lblMontoSiniestro = lblMontoSiniestro;
        }

        /// <summary>
        /// Actualiza el estado de este observador de acuerdo al cambio
        /// generado por el sujeto al cual observa.
        /// </summary>
        /// <param name="nuevoEstado">El nuevo estado del sujeto que ha sido propagado a este observador.</param>
        public void ActualizarEstado(decimal nuevoEstado)
        {
            if (modelo.PorcentajePagoSiniestro != null) {
                var monto = decimal.Round(nuevoEstado * modelo.PorcentajePagoSiniestro.Value / 100M, 2);
                lblMontoSiniestro.Text = $"$ {monto.ToString("N2")}";
            }
        }
    }
}