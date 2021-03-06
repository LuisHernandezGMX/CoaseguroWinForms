﻿using System.Data.Entity;

namespace CoaseguroWinForms.DAL.Entities
{
    /// <summary>
    /// Conexión por Entity Framework a la base de datos de acuerdo
    /// a la cadena de conexión del SII.
    /// </summary>
    public class CoaseguroContext : DbContext
    {
        public virtual DbSet<CoaseguradorasFee> CoaseguradorasFee { get; set; }
        public virtual DbSet<CoaseguradorasFeeWkf> CoaseguradorasFeeWkf { get; set; }
        public virtual DbSet<CoaseguradorasParticipantes> CoaseguradorasParticipantes { get; set; }
        public virtual DbSet<CoaseguradorasParticipantesWkf> CoaseguradorasParticipantesWkf { get; set; }
        public virtual DbSet<CoaseguroPrincipal> CoaseguroPrincipal { get; set; }
        public virtual DbSet<CoaseguroPrincipalWkf> CoaseguroPrincipalWkf { get; set; }
        public virtual DbSet<GarantiaPagoCoaseguro> GarantiaPagoCoaseguro { get; set; }
        public virtual DbSet<mcia> mcia { get; set; }
        public virtual DbSet<MetodoPagoCoaseguro> MetodoPagoCoaseguro { get; set; }
        public virtual DbSet<PagoComisionAgenteCoaseguro> PagoComisionAgenteCoaseguro { get; set; }
        public virtual DbSet<PagoSiniestroCoaseguro> PagoSiniestroCoaseguro { get; set; }
        public virtual DbSet<pv_cia_lider> pv_cia_lider { get; set; }
        public virtual DbSet<pv_cia_lider_wkf> pv_cia_lider_wkf { get; set; }
        public virtual DbSet<pv_coas_cia> pv_coas_cia { get; set; }
        public virtual DbSet<pv_coas_cia_wkf> pv_coas_cia_wkf { get; set; }
        public virtual DbSet<pv_header> pv_header { get; set; }
        public virtual DbSet<pv_header_wkf> pv_header_wkf { get; set; }
        public virtual DbSet<pv_importe> pv_importe { get; set; }
        public virtual DbSet<pv_importe_coas> pv_importe_coas { get; set; }
        public virtual DbSet<pv_importe_coas_wkf> pv_importe_coas_wkf { get; set; }
        public virtual DbSet<pv_importe_wkf> pv_importe_wkf { get; set; }
        public virtual DbSet<tmoneda> tmoneda { get; set; }
        public virtual DbSet<ttipo_mov> ttipo_mov { get; set; }

        /// <summary>
        /// Genera una nueva conexión a la base de datos indicada en la cadena
        /// de conexión.
        /// </summary>
        /// <param name="connectionString">La cadena de conexión descifrada del SII.</param>
        public CoaseguroContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<CoaseguroContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoaseguradorasFee>()
                .Property(e => e.cod_cia)
                .HasPrecision(3, 0);

            modelBuilder.Entity<CoaseguradorasFeeWkf>()
                .Property(e => e.cod_cia)
                .HasPrecision(3, 0);

            modelBuilder.Entity<CoaseguradorasParticipantes>()
                .Property(e => e.cod_cia)
                .HasPrecision(3, 0);

            modelBuilder.Entity<CoaseguradorasParticipantesWkf>()
                .Property(e => e.cod_cia)
                .HasPrecision(3, 0);

            modelBuilder.Entity<CoaseguroPrincipal>()
                .Property(e => e.cod_tipo_mov)
                .HasPrecision(2, 0);

            modelBuilder.Entity<CoaseguroPrincipal>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<CoaseguroPrincipal>()
                .HasMany(e => e.CoaseguradorasFee)
                .WithRequired(e => e.CoaseguroPrincipal)
                .HasForeignKey(e => e.IdCoaseguro);

            modelBuilder.Entity<CoaseguroPrincipal>()
                .HasMany(e => e.CoaseguradorasParticipantes)
                .WithRequired(e => e.CoaseguroPrincipal)
                .HasForeignKey(e => e.IdCoaseguro);

            modelBuilder.Entity<CoaseguroPrincipalWkf>()
                .Property(e => e.cod_tipo_mov)
                .HasPrecision(2, 0);

            modelBuilder.Entity<CoaseguroPrincipalWkf>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<CoaseguroPrincipalWkf>()
                .HasMany(e => e.CoaseguradorasFeeWkf)
                .WithRequired(e => e.CoaseguroPrincipalWkf)
                .HasForeignKey(e => e.IdCoaseguro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoaseguroPrincipalWkf>()
                .HasMany(e => e.CoaseguradorasParticipantesWkf)
                .WithRequired(e => e.CoaseguroPrincipalWkf)
                .HasForeignKey(e => e.IdCoaseguro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GarantiaPagoCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipal)
                .WithRequired(e => e.GarantiaPagoCoaseguro)
                .HasForeignKey(e => e.IdGarantiaPago);

            modelBuilder.Entity<GarantiaPagoCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipalWkf)
                .WithRequired(e => e.GarantiaPagoCoaseguro)
                .HasForeignKey(e => e.IdGarantiaPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_cia)
                .HasPrecision(3, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.txt_nom_cia)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_tipo_dir)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.txt_direccion)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.nro_cod_postal)
                .HasPrecision(5, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_zona_dir)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_colonia)
                .HasPrecision(5, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_municipio)
                .HasPrecision(3, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_dpto)
                .HasPrecision(3, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_pais)
                .HasPrecision(3, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_tipo_telef)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.txt_telefono)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_tipo_iva)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.nro_nit)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cnt_anos_min_ing_vida)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cnt_anos_max_ing_vida)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cnt_anos_max_perman_vida)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.txt_nom_redu)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.tipo_cia)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_wins)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.nro_ext)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.nro_int)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.cod_region)
                .HasPrecision(2, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.txt_cheque_a_nom)
                .IsUnicode(false);

            modelBuilder.Entity<mcia>()
                .Property(e => e.sn_inverfas)
                .HasPrecision(1, 0);

            modelBuilder.Entity<mcia>()
                .Property(e => e.sn_transferencia)
                .HasPrecision(1, 0);

            modelBuilder.Entity<mcia>()
                .HasMany(e => e.CoaseguradorasFeeWkf)
                .WithRequired(e => e.mcia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<mcia>()
                .HasMany(e => e.CoaseguradorasParticipantesWkf)
                .WithRequired(e => e.mcia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MetodoPagoCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipal)
                .WithRequired(e => e.MetodoPagoCoaseguro)
                .HasForeignKey(e => e.IdMetodoPago);

            modelBuilder.Entity<MetodoPagoCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipalWkf)
                .WithRequired(e => e.MetodoPagoCoaseguro)
                .HasForeignKey(e => e.IdMetodoPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PagoComisionAgenteCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipal)
                .WithRequired(e => e.PagoComisionAgenteCoaseguro)
                .HasForeignKey(e => e.IdPagoComisionAgente);

            modelBuilder.Entity<PagoComisionAgenteCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipalWkf)
                .WithRequired(e => e.PagoComisionAgenteCoaseguro)
                .HasForeignKey(e => e.IdPagoComisionAgente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PagoSiniestroCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipal)
                .WithRequired(e => e.PagoSiniestroCoaseguro)
                .HasForeignKey(e => e.IdPagoSiniestro);

            modelBuilder.Entity<PagoSiniestroCoaseguro>()
                .HasMany(e => e.CoaseguroPrincipalWkf)
                .WithRequired(e => e.PagoSiniestroCoaseguro)
                .HasForeignKey(e => e.IdPagoSiniestro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.pje_partic)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.txt_poliza_lider)
                .IsUnicode(false);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.pje_comision)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.sn_admin_com_n)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.sn_admin_com_e)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.sn_rec_partic)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.sn_rec_recl)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.txt_anexo_lider)
                .IsUnicode(false);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.pje_gtos)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_cia_lider>()
                .Property(e => e.pje_reserva)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.pje_partic)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.txt_poliza_lider)
                .IsUnicode(false);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.pje_comision)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.sn_admin_com_n)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.sn_admin_com_e)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.sn_rec_partic)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.sn_rec_recl)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.txt_anexo_lider)
                .IsUnicode(false);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.pje_gtos)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_cia_lider_wkf>()
                .Property(e => e.pje_reserva)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.cod_cia_part)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.pje_part_prima)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.pje_part_premio)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.pje_gastos_pil_cedido)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.pje_gastos_vida)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.ind_cia)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.sn_impr_clau_recl)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.sn_impr_clau_partic)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.sn_firma)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.sn_admin_com_n)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.sn_admin_com_e)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia>()
                .Property(e => e.sn_impr_total)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.cod_cia_part)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.pje_part_prima)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.pje_part_premio)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.pje_gastos_pil_cedido)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.pje_gastos_vida)
                .HasPrecision(5, 2);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.ind_cia)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.sn_impr_clau_recl)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.sn_impr_clau_partic)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.sn_firma)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.sn_admin_com_n)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.sn_admin_com_e)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_coas_cia_wkf>()
                .Property(e => e.sn_impr_total)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_suc)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_ramo)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.nro_pol)
                .HasPrecision(9, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.aaaa_endoso)
                .HasPrecision(4, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.nro_endoso)
                .HasPrecision(8, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.imp_cambio)
                .HasPrecision(9, 4);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_operacion)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.nro_solicitud)
                .HasPrecision(9, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.nro_cotizacion)
                .HasPrecision(7, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.nro_flota)
                .HasPrecision(7, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.sn_impresion)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.sn_anula_automatica)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_tipo_agente)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_grupo_endo)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_tipo_endo)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_sistema)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.sn_cob_coas_total)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.sn_bancaseguros)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_periodo_fact)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.nro_pv_cero)
                .HasPrecision(9, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.sn_fronting)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_grupo)
                .HasPrecision(4, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_subramo)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_origen)
                .IsUnicode(false);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.txt_partic_acreedor)
                .IsUnicode(false);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.cod_partic_cias)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header>()
                .Property(e => e.id_sol_cotiz)
                .HasPrecision(7, 0);

            modelBuilder.Entity<pv_header>()
                .HasOptional(e => e.pv_cia_lider)
                .WithRequired(e => e.pv_header);

            modelBuilder.Entity<pv_header>()
                .HasMany(e => e.pv_importe)
                .WithRequired(e => e.pv_header)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_suc)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_ramo)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.nro_pol)
                .HasPrecision(9, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.aaaa_endoso)
                .HasPrecision(4, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.nro_endoso)
                .HasPrecision(6, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.imp_cambio)
                .HasPrecision(9, 4);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_operacion)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.nro_solicitud)
                .HasPrecision(9, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.nro_cotizacion)
                .HasPrecision(7, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.nro_flota)
                .HasPrecision(7, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.sn_impresion)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.sn_anula_automatica)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_tipo_agente)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_grupo_endo)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_tipo_endo)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_sistema)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.sn_cob_coas_total)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.sn_bancaseguros)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_periodo_fact)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.nro_pv_cero)
                .HasPrecision(9, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.sn_fronting)
                .HasPrecision(1, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_grupo)
                .HasPrecision(4, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_subramo)
                .HasPrecision(3, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_origen)
                .IsUnicode(false);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.txt_partic_acreedor)
                .IsUnicode(false);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.cod_partic_cias)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .Property(e => e.id_sol_cotiz)
                .HasPrecision(7, 0);

            modelBuilder.Entity<pv_header_wkf>()
                .HasMany(e => e.CoaseguroPrincipalWkf)
                .WithRequired(e => e.pv_header_wkf)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pv_importe>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_importe_coas>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_importe_coas_wkf>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<pv_importe_wkf>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.cod_moneda)
                .HasPrecision(2, 0);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.txt_desc_redu)
                .IsUnicode(false);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.txt_desc)
                .IsUnicode(false);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.imp_dif_max)
                .HasPrecision(7, 2);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.cnt_decimales)
                .HasPrecision(1, 0);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.cnt_decimales_cambio)
                .HasPrecision(1, 0);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.pje_desvio_cambio_ingreso)
                .HasPrecision(5, 2);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.pje_desvio_cambio_aplicacion)
                .HasPrecision(5, 2);

            modelBuilder.Entity<tmoneda>()
                .Property(e => e.cnt_decimales_emi)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tmoneda>()
                .HasMany(e => e.CoaseguroPrincipalWkf)
                .WithRequired(e => e.tmoneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tmoneda>()
                .HasMany(e => e.pv_header)
                .WithRequired(e => e.tmoneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ttipo_mov>()
                .Property(e => e.cod_tipo_mov)
                .HasPrecision(2, 0);

            modelBuilder.Entity<ttipo_mov>()
                .Property(e => e.txt_desc)
                .IsUnicode(false);

            modelBuilder.Entity<ttipo_mov>()
                .Property(e => e.sn_iva)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ttipo_mov>()
                .Property(e => e.sn_gasto_emision)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ttipo_mov>()
                .Property(e => e.sn_activo)
                .HasPrecision(1, 0);

            modelBuilder.Entity<ttipo_mov>()
                .HasMany(e => e.CoaseguroPrincipalWkf)
                .WithRequired(e => e.ttipo_mov)
                .WillCascadeOnDelete(false);
        }
    }
}