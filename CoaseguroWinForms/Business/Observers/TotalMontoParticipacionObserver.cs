using System;
using System.Windows.Forms;
using CoaseguroWinForms.DAL.ViewModels.Seguidor;

namespace CoaseguroWinForms.Business.Observers
{
    /// <summary>
    /// Observador que representa al monto total de
    /// participación. Actualiza su propio estado cada
    /// vez que el monto de participación de <see cref="LiderMontoParticipacionSubjectObserver"/>
    /// cambia o que el monto de GMX cambia.
    /// </summary>
    public class TotalMontoParticipacionObserver : IObservador<decimal>
    {
        #region Variables Privadas

        /// <summary>
        /// El vista modelo al cuál está enlazado éste objeto.
        /// </summary>
        private SeguidorViewModel modelo;

        /// <summary>
        /// Etiqueta del formulario que muestra el monto de participación de la coaseguradora Líder.
        /// </summary>
        private Label lblMontoTotalParticipacion;

        /// <summary>
        /// Etiqueta del formulario que muestra el monto de participación total.
        /// </summary>
        Label lblPorcentajeTotalParticipacion;

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
        /// <param name="lblMontoTotalParticipacion">Etiqueta del formulario que muestra el monto de participación total.</param>
        /// <param name="lblPorcentajeTotalParticipacion">Etiqueta del formulario que muestra el monto de participación total.</param>
        public TotalMontoParticipacionObserver(SeguidorViewModel modelo, Label lblMontoTotalParticipacion, Label lblPorcentajeTotalParticipacion)
        {
            id = Guid.NewGuid();
            this.modelo = modelo;
            this.lblMontoTotalParticipacion = lblMontoTotalParticipacion;
            this.lblPorcentajeTotalParticipacion = lblPorcentajeTotalParticipacion;
        }

        /// <summary>
        /// Actualiza el estado de este observador de acuerdo al cambio
        /// generado por el sujeto al cual observa.
        /// </summary>
        /// <param name="nuevoEstado">El nuevo estado del sujeto que ha sido propagado a este observador.</param>
        public void ActualizarEstado(decimal nuevoEstado)
        {
            modelo.PorcentajeTotalParticipacion = modelo.PorcentajeGMX + modelo.Lider.PorcentajeParticipacion;
            modelo.MontoTotalParticipacion = modelo.MontoGMX + modelo.Lider.MontoParticipacion;
            lblMontoTotalParticipacion.Text = $"$ {modelo.MontoTotalParticipacion.ToString("N2")}";
            lblPorcentajeTotalParticipacion.Text = $"{modelo.PorcentajeTotalParticipacion.ToString("N2")} %";
        }
    }
}