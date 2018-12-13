using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using CoaseguroWinForms.DAL.ViewModels.Seguidor;

namespace CoaseguroWinForms.Business.Observers
{
    /// <summary>
    /// Sujeto-Observador que representa a la participación de GMX
    /// como coaseguradora seguidora. Actualiza su propio estado cada
    /// vez que hay un cambio en el límite máximo de responsabilidad al
    /// 100 % (observador de <see cref="LimiteMaximoResponsabilidadSubject"/>).
    /// Y una vez que ocurre este cambio, notifica a todos sus respectivos
    /// observadores con la nueva participación de GMX.
    /// </summary>
    class GMXSubjectObserver : ISujeto<decimal>, IObservador<decimal>
    {
        #region Variables Privadas

        /// <summary>
        /// El vista modelo al cuál está enlazado éste objeto.
        /// </summary>
        private SeguidorViewModel modelo;

        /// <summary>
        /// Etiqueta del formulario que muestra el monto de participación de GMX.
        /// </summary>
        private Label lblMontoGMX;

        /// <summary>
        /// Id único de este observador;
        /// </summary>
        private Guid id;

        /// <summary>
        /// l conjunto de observadores enlazados a este sujeto.
        /// </summary>
        private List<IObservador<decimal>> observadores;

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
        /// Crea un nuevo sujeto-observador enlazado al vista modelo indicado.
        /// </summary>
        /// <param name="modelo">El vista modelo del formulario.</param>
        /// <param name="lblMontoGMX">La etiqueta que muestra el monto de participación de GMX.</param>
        public GMXSubjectObserver(SeguidorViewModel modelo, Label lblMontoGMX)
        {
            id = new Guid();
            this.modelo = modelo;
            this.lblMontoGMX = lblMontoGMX;
            observadores = new List<IObservador<decimal>>();
        }

        /// <summary>
        /// Agrega el <see cref="IObservador"/> indicado a este sujeto.
        /// </summary>
        /// <param name="observador">El observador a ser agregado a la lista de este sujeto.</param>
        public void RegistrarObservador(IObservador<decimal> observador)
        {
            if (!observadores.Any(obs => obs.Id == observador.Id)) {
                observadores.Add(observador);
            }
        }

        /// <summary>
        /// Elimina el <see cref="IObservador"/> indicado de este sujeto.
        /// </summary>
        /// <param name="observador">El observador que será eliminado de este sujeto.</param>
        public void EliminarObservador(IObservador<decimal> observador)
        {
            observadores.RemoveAll(obs => obs.Id == observador.Id);
        }

        /// <summary>
        /// Notifica a todos los observadores de este sujeto acerca de un cambio de estado.
        /// </summary>
        /// <param name="nuevoEstado">El nuevo valor del estado de este sujeto.</param>
        public void Notificar(decimal nuevoEstado)
        {
            foreach (var obs in observadores) {
                obs.ActualizarEstado(nuevoEstado);
            }
        }

        /// <summary>
        /// Actualiza el estado de este observador de acuerdo al cambio
        /// generado por el sujeto al cual observa.
        /// </summary>
        /// <param name="nuevoEstado">El nuevo estado del sujeto que ha sido propagado a este observador.</param>
        public void ActualizarEstado(decimal nuevoEstado)
        {
            modelo.MontoGMX = decimal.Round(nuevoEstado * modelo.PorcentajeGMX / 100M, 2);
            lblMontoGMX.Text = $"$ {modelo.MontoGMX.ToString("N2")}";

            Notificar(modelo.MontoGMX);
        }
    }
}