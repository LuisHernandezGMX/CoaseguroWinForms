using System.ComponentModel.DataAnnotations;

namespace CoaseguroWinForms.DAL
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
    /// pago del siniestro. La primera opción (30 días) son
    /// los que exige la Ley sobre el Contrato de Seguro.
    /// TODO: Se inicia la enumeración en 1 por si estos
    /// valores se convierten posteriormente a una tabla
    /// individual en la base de datos.
    /// </summary>
    public enum DiasGarantiaPago
    {
        /// <summary>
        /// Los 30 días estándar de acuerdo a la Ley sobre el Contrato de Seguro.
        /// </summary>
        [Display(Name = "30 días")]
        TreintaDias = 1,

        /// <summary>
        /// Garantía de pago extendida a 45 días.
        /// </summary>
        [Display(Name = "45 días")]
        CuarentaycincoDias,

        /// <summary>
        /// Garantía de pago extendida a 60 días.
        /// </summary>
        [Display(Name = "60 días")]
        SesentaDias,

        /// <summary>
        /// Garantía de pago extendida a 90 días.
        /// </summary>
        [Display(Name = "90 días")]
        NoventaDias,

        /// <summary>
        /// Garantía de pago extendida a 120 días.
        /// </summary>
        [Display(Name = "120 días")]
        CientoyveinteDías
    }
}