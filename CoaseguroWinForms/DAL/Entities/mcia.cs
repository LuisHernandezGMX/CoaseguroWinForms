namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mcia")]
    public partial class mcia
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal cod_cia { get; set; }

        [Required]
        [StringLength(100)]
        public string txt_nom_cia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_tipo_dir { get; set; }

        [Required]
        [StringLength(80)]
        public string txt_direccion { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nro_cod_postal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_zona_dir { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_colonia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_municipio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_dpto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_pais { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_tipo_telef { get; set; }

        [Required]
        [StringLength(15)]
        public string txt_telefono { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_tipo_iva { get; set; }

        [Required]
        [StringLength(20)]
        public string nro_nit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_gasto_pilotaje { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cnt_anos_min_ing_vida { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cnt_anos_max_ing_vida { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cnt_anos_max_perman_vida { get; set; }

        [StringLength(100)]
        public string txt_nom_redu { get; set; }

        public byte? cod_tipo_agente { get; set; }

        public int? cod_agente { get; set; }

        [StringLength(1)]
        public string tipo_cia { get; set; }

        [StringLength(10)]
        public string cod_wins { get; set; }

        public int? cod_ciudad { get; set; }

        [StringLength(10)]
        public string nro_ext { get; set; }

        [StringLength(10)]
        public string nro_int { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_region { get; set; }

        public int? id_persona { get; set; }

        [StringLength(120)]
        public string txt_cheque_a_nom { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_inverfas { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_transferencia { get; set; }
    }
}
