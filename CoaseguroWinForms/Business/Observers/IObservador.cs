using System;

namespace CoaseguroWinForms.Business.Observers
{
    /// <summary>
    /// Contiene los métodos que necesita un objeto Observador para
    /// reaccionar de acuerdo a los cambios de estado del Sujeto que
    /// está observando.
    /// </summary>
    /// <typeparam name="T">El tipo de dato del estado del sujeto que está siendo observado.</typeparam>
    public interface IObservador<T>
    {
        /// <summary>
        /// Id único de este observador.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Actualiza el estado de este observador de acuerdo al cambio
        /// generado por el sujeto al cual observa.
        /// </summary>
        /// <param name="nuevoEstado">El nuevo estado del sujeto que ha sido propagado a este observador.</param>
        void ActualizarEstado(T nuevoEstado);
    }
}
