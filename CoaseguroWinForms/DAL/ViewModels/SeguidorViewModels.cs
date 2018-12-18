namespace CoaseguroWinForms.DAL.ViewModels.Seguidor
{
    /// <summary>
    /// Almacena todo el estado que se maneja en el formulario <see cref="SeguidorForm"/>.
    /// </summary>
    public class SeguidorViewModel : FormBaseViewModel
    {
        /// <summary>
        /// La información de la coaseguradora líder.
        /// </summary>
        public CoaseguradoraLiderViewModel Lider { get; set; }

        /// <summary>
        /// El porcentaje de Fee por administración de GMX.
        /// </summary>
        public decimal PorcentajeFeeGMX { get; set; }

        /// <summary>
        /// El monto de Fee por administración de GMX. Se calcula
        /// a partir de PorcentajeFeeGMX.
        /// </summary>
        public decimal MontoFeeGMX { get; set; }
    }

    /// <summary>
    /// Almacena el estado de la coaseguradora líder.
    /// </summary>
    public class CoaseguradoraLiderViewModel
    {
        /// <summary>
        /// Nombre de la coaseguradora.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// El porcentaje de participación de la coaseguradora.
        /// </summary>
        public decimal PorcentajeParticipacion { get; set; }

        /// <summary>
        /// El monto de participación de la coaseguradora.
        /// </summary>
        public decimal MontoParticipacion { get; set; }

        /// <summary>
        /// El monto correspondiente de prima neta de la coaseguradora.
        /// </summary>
        public decimal MontoPrimaNeta { get; set; }
    }
}