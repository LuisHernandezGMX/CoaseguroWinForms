namespace CoaseguroWinForms.DAL.ViewModels
{
    /// <summary>
    /// Vista modelo base para almacenar el estado que se maneja en los formularios.
    /// </summary>
    public class FormBaseViewModel
    {
        /// <summary>
        /// El límite máximo de responsabilidad al 100%.
        /// </summary>
        public decimal LimiteMaxResponsabilidad { get; set; }

        /// <summary>
        /// La prima neta.
        /// </summary>
        public decimal PrimaNeta { get; set; }

        /// <summary>
        /// El tipo de moneda a manejar en este formulario.
        /// </summary>
        public TipoMonedaViewModel Moneda { get; set; }

        /// <summary>
        /// Los montos propios de GMX en el formulario.
        /// </summary>
        public GMXViewModel GMX { get; set; }

        /// <summary>
        /// Porcentaje total de la participación de las coaseguradoras,
        /// incluyendo a GMX.
        /// </summary>
        public decimal PorcentajeTotalParticipacion { get; set; }

        /// <summary>
        /// Monto total de la participación de todas las coaseguradoras,
        /// incluyendo a GMX.
        /// </summary>
        public decimal MontoTotalParticipacion { get; set; }

        /// <summary>
        /// Monto total de la prima neta de todas las coaseguradoras, incluyendo
        /// a GMX.
        /// </summary>
        public decimal MontoPrimaNetaTotalParticipacion { get; set; }

        /// <summary>
        /// El método de pago.
        /// </summary>
        public MetodoPago MetodoPago { get; set; }

        /// <summary>
        /// La forma del pago de comisión al agente.
        /// </summary>
        public PagoComisionAgente PagoComisionAgente { get; set; }

        /// <summary>
        /// La forma de pago del siniestro.
        /// </summary>
        public PagoSiniestro PagoSiniestro { get; set; }

        /// <summary>
        /// Porcentaje del monto máximo para pago automático de siniestro.
        /// Este campo contiene el porcentaje SOLO si se eligió la opción
        /// correspondiente. Si el pago del siniestro se hace a participación,
        /// este campo deberá ser NULO.
        /// </summary>
        public decimal? PorcentajePagoSiniestro { get; set; }

        /// <summary>
        /// Monto máximo para pago automático de siniestro. Este campo contiene
        /// el monto SOLO si se eligió la opción correspondiente. Si el pago del
        /// siniestro se hace a participación, este campo deberá ser NULO.
        /// </summary>
        public decimal? MontoSiniestro { get; set; }

        /// <summary>
        /// La forma de indemnización del siniestro. Puede utilizarse un porcentaje
        /// sobre el Límite Máximo de Responsabilidad o un monto específico. Si el
        /// pago del siniestro se hace a participación, este campo deberá ser NULO.
        /// </summary>
        public IndemnizacionSiniestro? FormaIndemnizacion { get; set; }

        /// <summary>
        /// Garantía de pago del siniestro. Contiene el valor correspondiente
        /// de la enumeración, la cual equivale a los días que se tienen para
        /// realizar el pago del siniestro. Si se elige la opción de Ley sobre
        /// el Contrato de Seguro, la garantía de pago deberán de ser 30 días.
        /// </summary>
        public DiasGarantiaPago GarantiaPago { get; set; }
    }

    /// <summary>
    /// Los montos propios de GMX en el formulario.
    /// </summary>
    public class GMXViewModel
    {
        /// <summary>
        /// El porcentaje de participación de GMX.
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// El monto de participación de GMX. Este valor se
        /// calcula a partir de PorcentajeGMX.
        /// </summary>
        public decimal MontoParticipacion { get; set; }

        /// <summary>
        /// El monto de participación de la prima neta de GMX. Este
        /// valor se calcula a partir del PrimaNeta y de PorcentajeGMX.
        /// </summary>
        public decimal MontoPrimaNeta { get; set; }
    }

    /// <summary>
    /// Representa el tipo de moneda a manejar para este coaseguro. Está
    /// definido por la tabla [tmoneda].
    /// </summary>
    public class TipoMonedaViewModel
    {
        /// <summary>
        /// El Id de esta moneda.
        /// </summary>
        public decimal Id { get; set; }

        /// <summary>
        /// El símbolo a utilizar, por ejemplo, [$]
        /// para MXN y [US$] para dólar estadounidense.
        /// </summary>
        public string Simbolo { get; set; }

        /// <summary>
        /// La descripción de esta moneda.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// El importe de cambio en moneda equivalente de la póliza.
        /// </summary>
        public decimal ImporteCambio { get; set; }
    }
}
