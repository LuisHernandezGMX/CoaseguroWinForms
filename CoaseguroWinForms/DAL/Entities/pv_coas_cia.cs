namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pv_coas_cia
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_pv { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal cod_cia_part { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_part_prima { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_part_premio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_gastos_pil_cedido { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pje_gastos_vida { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ind_cia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_impr_clau_recl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_impr_clau_partic { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fec_imp_clau_recl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fec_imp_clau_partic { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_premio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_prima { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_firma { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_premio_eq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_prima_eq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_admin_com_n { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_admin_com_e { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_impr_total { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_gasto_emision { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_recargo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_descuento { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_decreto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_iva { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_gasto_emision_eq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_recargo_eq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_descuento_eq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_decreto_eq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_iva_eq { get; set; }
    }
}
