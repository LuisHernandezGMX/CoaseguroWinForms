namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoaseguradorasParticipantesWkf")]
    public partial class CoaseguradorasParticipantesWkf
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

        public virtual CoaseguroPrincipalWkf CoaseguroPrincipalWkf { get; set; }

        public virtual mcia mcia { get; set; }
    }
}
