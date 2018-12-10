using System;
using System.Collections.Generic;
using CoaseguroWinForms.DAL.Entities;

namespace CoaseguroWinForms.DAL.ViewModels.Lider
{
    /// <summary>
    /// Almacena todo el estado que se maneja en el formulario <see cref="LiderForm"/>.
    /// </summary>
    public class LiderViewModel
    {
        /// <summary>
        /// El límite máximo de responsabilidad. Este valor
        /// se obtiene de la base de datos.
        /// </summary>
        public decimal LimiteMaxResponsabilidad { get; set; }

        /// <summary>
        /// La prima neta. Este valor se obtiene de la base
        /// de datos.
        /// </summary>
        public decimal PrimaNeta { get; set; }

        /// <summary>
        /// El porcentaje de participación de GMX. Este valor
        /// se obtiene de la base de datos.
        /// </summary>
        public decimal PorcentajeGMX { get; set; }

        /// <summary>
        /// El monto de participación de GMX. Este valor es
        /// calculado.
        /// </summary>
        public decimal MontoGMX { get; set; }

        /// <summary>
        /// Las coaseguradoras seguidoras de GMX.
        /// </summary>
        public List<CoaseguradoraViewModel> Coaseguradoras { get; set; }

        /// <summary>
        /// El método de pago.
        /// </summary>
        public MetodoPago MetodoPago { get; set; }

        /// <summary>
        /// La forma del pago de comisión al agente.
        /// </summary>
        public PagoComisionAgente PagoComisionAgente { get; set; }

        /// <summary>
        /// Porcentaje del monto máximo para pago automático de siniestro.
        /// Este campo contiene el porcentaje SOLO si se eligió la opción
        /// correspondiente. Si el pago del siniestro se hace a participación,
        /// este campo deberá ser NULO.
        /// </summary>
        public decimal? PorcentajePagoSiniestro { get; set; }

        /// <summary>
        /// Garantía de pago del siniestro. Si la garantía se hace de acuerdo
        /// a la Ley sobre el Contrato de Seguro este campo deberá ser NULO. De
        /// lo contrario, contiene el valor correspondiente de la enumeración, la
        /// cual equivale a los días que se tienen para realizar el pago del siniestro.
        /// </summary>
        public DiasGarantiaPago? GarantiaPago { get; set; }
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