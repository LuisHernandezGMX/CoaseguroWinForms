namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pv_cia_lider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_pv { get; set; }

        public int cod_cia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_partic { get; set; }

        [StringLength(20)]
        public string txt_poliza_lider { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_comision { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_admin_com_n { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_admin_com_e { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_rec_partic { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_rec_recl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fec_rec_partic { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fec_rec_recl { get; set; }

        [StringLength(20)]
        public string txt_anexo_lider { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pje_gtos { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pje_reserva { get; set; }
    }
}
