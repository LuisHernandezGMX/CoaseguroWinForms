using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoaseguroWinForms.Business.Observers
{
    /// <summary>
    /// Contiene los métodos que necesita un objeto Sujeto
    /// para poder ser observado apropiadamente por los objetos Observadores.
    /// </summary>
    /// <typeparam name="T">El tipo de dato del estado que almacena este sujeto.</typeparam>
    public interface ISujeto<T>
    {
        /// <summary>
        /// Agrega el <see cref="IObservador"/> indicado a este sujeto.
        /// </summary>
        /// <param name="observador">El observador a ser agregado a la lista de este sujeto.</param>
        void RegistrarObservador(IObservador<T> observador);

        /// <summary>
        /// Elimina el <see cref="IObservador"/> indicado de este sujeto.
        /// </summary>
        /// <param name="observador">El observador que será eliminado de este sujeto.</param>
        void EliminarObservador(IObservador<T> observador);

        /// <summary>
        /// Notifica a todos los observadores de este sujeto acerca de un cambio de estado.
        /// </summary>
        /// <param name="nuevoEstado">El nuevo valor del estado de este sujeto.</param>
        void Notificar(T nuevoEstado);
    }
}
