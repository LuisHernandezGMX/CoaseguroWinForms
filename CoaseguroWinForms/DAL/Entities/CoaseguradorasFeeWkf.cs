namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoaseguradorasFeeWkf")]
    public partial class CoaseguradorasFeeWkf
    {
        public int Id { get; set; }

        public int IdCoaseguro { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_cia { get; set; }

        public decimal PorcentajeFee { get; set; }

        public decimal MontoFee { get; set; }

        public decimal MontoFeeEquivalente { get; set; }

        public virtual CoaseguroPrincipalWkf CoaseguroPrincipalWkf { get; set; }

        public virtual mcia mcia { get; set; }
    }
}
