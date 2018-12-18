﻿using CoaseguroWinForms.DAL.Entities;

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
        /// El porcentaje de participación de GMX.
        /// </summary>
        public decimal PorcentajeGMX { get; set; }

        /// <summary>
        /// El monto de participación de GMX. Este valor se
        /// calcula a partir de PorcentajeGMX.
        /// </summary>
        public decimal MontoGMX { get; set; }

        /// <summary>
        /// El monto de participación de la prima neta de GMX. Este
        /// valor se calcula a partir del PrimaNeta y de PorcentajeGMX.
        /// </summary>
        public decimal MontoPrimaNetaGMX { get; set; }

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
        /// Porcentaje del monto máximo para pago automático de siniestro.
        /// Este campo contiene el porcentaje SOLO si se eligió la opción
        /// correspondiente. Si el pago del siniestro se hace a participación,
        /// este campo deberá ser NULO.
        /// </summary>
        public decimal? PorcentajePagoSiniestro { get; set; }

        /// <summary>
        /// Garantía de pago del siniestro. Contiene el valor correspondiente
        /// de la enumeración, la cual equivale a los días que se tienen para
        /// realizar el pago del siniestro. Si se elige la opción de Ley sobre
        /// el Contrato de Seguro, la garantía de pago deberán de ser 30 días.
        /// </summary>
        public DiasGarantiaPago GarantiaPago { get; set; }
    }
}
