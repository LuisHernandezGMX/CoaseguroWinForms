namespace CoaseguroWinForms.DAL.ViewModels.Seguidor
{
    /// <summary>
    /// Almacena todo el estado que se maneja en el formulario <see cref="SeguidorForm"/>.
    /// </summary>
    class SeguidorViewModel : FormBaseViewModel
    {
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
}
