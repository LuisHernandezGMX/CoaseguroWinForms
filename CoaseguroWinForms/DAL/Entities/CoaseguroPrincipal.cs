namespace CoaseguroWinForms.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoaseguroPrincipal")]
    public partial class CoaseguroPrincipal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CoaseguroPrincipal()
        {
            CoaseguradorasFee = new HashSet<CoaseguradorasFee>();
            CoaseguradorasParticipantes = new HashSet<CoaseguradorasParticipantes>();
        }

        public int Id { get; set; }

        public int id_pv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_tipo_mov { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_moneda { get; set; }

        public decimal LimiteMaximoResponsabilidad { get; set; }

        public decimal LimiteMaximoResponsabilidadEquivalente { get; set; }

        public decimal PrimaNeta { get; set; }

        public decimal PrimaNetaEquivalente { get; set; }

        public decimal PorcentajeGMX { get; set; }

        public decimal MontoParticipacionGMX { get; set; }

        public decimal MontoParticipacionGMXEquivalente { get; set; }

        public decimal PrimaGMX { get; set; }

        public decimal PrimaGMXEquivalente { get; set; }

        public int IdMetodoPago { get; set; }

        public int IdPagoComisionAgente { get; set; }

        public int IdPagoSiniestro { get; set; }

        public decimal? PorcentajeSiniestro { get; set; }

        public decimal? MontoSiniestro { get; set; }

        public decimal? MontoSiniestroEquivalente { get; set; }

        public int IdGarantiaPago { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguradorasFee> CoaseguradorasFee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoaseguradorasParticipantes> CoaseguradorasParticipantes { get; set; }

        public virtual GarantiaPagoCoaseguro GarantiaPagoCoaseguro { get; set; }

        public virtual MetodoPagoCoaseguro MetodoPagoCoaseguro { get; set; }

        public virtual PagoComisionAgenteCoaseguro PagoComisionAgenteCoaseguro { get; set; }

        public virtual PagoSiniestroCoaseguro PagoSiniestroCoaseguro { get; set; }

        public virtual pv_header pv_header { get; set; }

        public virtual tmoneda tmoneda { get; set; }

        public virtual ttipo_mov ttipo_mov { get; set; }
    }
}
