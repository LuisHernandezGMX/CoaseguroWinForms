using System.ComponentModel.DataAnnotations;

namespace CoaseguroWinForms.DAL
{
    /// <summary>
    /// El tipo de coaseguro a manejar.
    /// </summary>
    public enum TipoMovimiento
    {
        Seguidor = 2,
        Lider
    }

    /// <summary>
    /// El método de pago en el coaseguro.
    /// </summary>
    public enum MetodoPago
    {
        EstadoCuenta = 1,
        Conceptos
    }

    /// <summary>
    /// La forma del pago de comisión al agente.
    /// </summary>
    public enum PagoComisionAgente
    {
        Lider100 = 1,
        Participacion
    }

    /// <summary>
    /// La forma de pago del siniestro.
    /// </summary>
    public enum PagoSiniestro
    {
        Participacion = 1,
        CienPorCiento
    }

    /// <summary>
    /// Indica cómo se calculará el límite máximo
    /// de indemnización sin consultar a las
    /// coaseguradoras.
    /// </summary>
    public enum IndemnizacionSiniestro
    {
        /// <summary>
        /// Indica que el valor a leer para la indemnización
        /// será el porcentaje del límite máximo.
        /// </summary>
        Porcentaje = 1,

        /// <summary>
        /// Indica que el valor a leer para la indemnización
        /// será un monto específico.
        /// </summary>
        Monto
    }

    /// <summary>
    /// Los días que se tienen disponibles para realizar el
    /// pago del siniestro. La primera opción (30 días) son
    /// los que exige la Ley sobre el Contrato de Seguro.
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