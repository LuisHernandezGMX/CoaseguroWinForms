using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoaseguroWinForms.DAL.Entities
{
    /// <summary>
    /// El método de pago en el coaseguro.
    /// </summary>
    public enum MetodoPago
    {
        EstadoCuenta,
        Conceptos
    }

    /// <summary>
    /// La forma del pago de comisión al agente.
    /// </summary>
    public enum PagoComisionAgente
    {
        Lider100,
        Participacion
    }
}
