namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ttipo_mov
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ttipo_mov()
        {
            CoaseguroPrincipal = new HashSet<CoaseguroPrincipal>();
            CoaseguroPrincipalWkf = new HashSet<CoaseguroPrincipalWkf>();
        }

        [Key]
        [Column(TypeName = "numeric")]
        public decimal cod_tipo_mov { get; set; }

        [Required]
        [StringLength(30)]
        public string txt_desc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_iva { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_gasto_emision { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguroPrincipal> CoaseguroPrincipal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguroPrincipalWkf> CoaseguroPrincipalWkf { get; set; }
    }
}
