using System.Linq;
using System.Collections.Generic;

namespace CoaseguroWinForms.Business.Observers
{
    /// <summary>
    /// Sujeto que representa el valor del Límite Máximo de Responsabilidad al 100%.
    /// Cada vez que éste valor cambie, actualizará a todos sus observadores.
    /// </summary>
    class LimiteMaximoResponsabilidadSubject : ISujeto<decimal>
    {
        /// <summary>
        /// El conjunto de observadores enlazados a este sujeto.
        /// </summary>
        private List<IObservador<decimal>> observadores;

        /// <summary>
        /// Crea un nuevo sujeto.
        /// </summary>
        public LimiteMaximoResponsabilidadSubject()
        {
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
    }
}