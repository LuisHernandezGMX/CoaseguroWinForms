using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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

    /// <summary>
    /// Los días que se tienen disponibles para realizar el
    /// pago del siniestro.
    /// </summary>
    public enum DiasGarantiaPago
    {
        [Display(Name = "30 días")]
        TreintaDias,

        [Display(Name = "45 días")]
        CuarentaycincoDias,

        [Display(Name = "60 días")]
        SesentaDias
    }
}