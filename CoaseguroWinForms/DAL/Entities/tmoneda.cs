namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tmoneda")]
    public partial class tmoneda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tmoneda()
        {
            CoaseguroPrincipal = new HashSet<CoaseguroPrincipal>();
            CoaseguroPrincipalWkf = new HashSet<CoaseguroPrincipalWkf>();
            pv_header = new HashSet<pv_header>();
        }

        [Key]
        [Column(TypeName = "numeric")]
        public decimal cod_moneda { get; set; }

        [Required]
        [StringLength(3)]
        public string txt_desc_redu { get; set; }

        [Required]
        [StringLength(20)]
        public string txt_desc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_dif_max { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cnt_decimales { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cnt_decimales_cambio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_desvio_cambio_ingreso { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_desvio_cambio_aplicacion { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cnt_decimales_emi { get; set; }

        public byte? cnt_decimales_suma_aseg { get; set; }

        public byte? cnt_decimales_prima { get; set; }

        public byte? cnt_decimales_gastos { get; set; }

        public byte? cnt_decimales_impuestos { get; set; }

        public byte? cnt_decimales_iva { get; set; }

        public byte? cnt_decimales_comis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguroPrincipal> CoaseguroPrincipal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguroPrincipalWkf> CoaseguroPrincipalWkf { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pv_header> pv_header { get; set; }
    }
}
