namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PagoSiniestroCoaseguro")]
    public partial class PagoSiniestroCoaseguro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PagoSiniestroCoaseguro()
        {
            CoaseguroPrincipal = new HashSet<CoaseguroPrincipal>();
            CoaseguroPrincipalWkf = new HashSet<CoaseguroPrincipalWkf>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguroPrincipal> CoaseguroPrincipal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguroPrincipalWkf> CoaseguroPrincipalWkf { get; set; }
    }
}
