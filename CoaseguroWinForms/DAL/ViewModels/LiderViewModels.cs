using System.Collections.Generic;

namespace CoaseguroWinForms.DAL.ViewModels.Lider
{
    /// <summary>
    /// Almacena todo el estado que se maneja en el formulario <see cref="LiderForm"/>.
    /// </summary>
    public class LiderViewModel : FormBaseViewModel
    {
        /// <summary>
        /// Las coaseguradoras seguidoras de GMX.
        /// </summary>
        public List<CoaseguradoraViewModel> Coaseguradoras { get; set; }
    }

    /// <summary>
    /// Representa una coaseguradora en el formulario.
    /// </summary>
    public class CoaseguradoraViewModel
    {
        /// <summary>
        /// El Id de la coaseguradora.
        /// Viene de la base de datos.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// El nombre de la coaseguradora.
        /// Viene de la base de datos.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// El porcentaje de participación de la coaseguradora.
        /// Viene de la base de datos.
        /// </summary>
        public decimal PorcentajeParticipacion { get; set; }

        /// <summary>
        /// El monto de participación de la coaseguradora.
        /// Este valor es calculado.
        /// </summary>
        public decimal MontoParticipacion { get; set; }

        /// <summary>
        /// El monto de prima neta de la coaseguradora de acuerdo
        /// a su porcentaje de participación.
        /// </summary>
        public decimal MontoPrimaNeta { get; set; }

        /// <summary>
        /// El porcentaje de Fee de la coaseguradora.
        /// Este valor es ingresado por el usuario.
        /// </summary>
        public decimal PorcentajeFee { get; set; }

        /// <summary>
        /// El monto de Fee de la coaseguradora.
        /// Este valor es calculado.
        /// </summary>
        public decimal MontoFee { get; set; }
    }
}