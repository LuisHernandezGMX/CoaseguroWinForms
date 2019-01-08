namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pv_importe
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_pv { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal cod_moneda { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_suma_asegurada { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_prima { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_gasto_emision { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_recargo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_descuento { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_decreto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_prima_total { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_iva { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_base_imponible { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_laa { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? imp_prima_pend_reas { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_laa_deduc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_suma_ind { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_prima_obj { get; set; }

        public virtual pv_header pv_header { get; set; }
    }
}
