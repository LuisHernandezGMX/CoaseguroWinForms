namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CoaseguradorasParticipantes
    {
        public int Id { get; set; }

        public int IdCoaseguro { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_cia { get; set; }

        public decimal PorcentajeParticipacion { get; set; }

        public decimal MontoParticipacion { get; set; }

        public decimal MontoParticipacionEquivalente { get; set; }

        public decimal MontoPrima { get; set; }

        public decimal MontoPrimaEquivalente { get; set; }

        public virtual CoaseguroPrincipal CoaseguroPrincipal { get; set; }

        public virtual mcia mcia { get; set; }
    }
}
