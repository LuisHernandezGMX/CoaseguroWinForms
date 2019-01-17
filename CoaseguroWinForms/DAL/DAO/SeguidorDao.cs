using System;
using System.Linq;
using System.Data.Entity;
using CoaseguroWinForms.DAL.Entities;
using CoaseguroWinForms.DAL.ViewModels;
using CoaseguroWinForms.DAL.ViewModels.Seguidor;

namespace CoaseguroWinForms.DAL.DAO.Seguidor
{
    /// <summary>
    /// Incluye las operaciones necesarias en la capa de persistencia
    /// para el formulario <see cref="SeguidorForm"/>.
    /// </summary>
    public class SeguidorDao : BaseDao<SeguidorViewModel>
    {
        /// <summary>
        /// Inicializa una nueva instancia de este DAO.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a trabajar.</param>
        /// <param name="connectionString">Cadena de conexión descifrada del SII.</param>
        public SeguidorDao(int idPv, string connectionString) : base(idPv, connectionString) { }

        /// <summary>
        /// Rellena el VistaModelo con la información necesaria para
        /// trabajarlo por primera vez.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="SeguidorViewModel"/>.</returns>
        public override SeguidorViewModel RellenarNuevoModelo()
        {
            var model = new SeguidorViewModel {
                IdGMX = 1, // GMX siempre es el primer registro de [mcia]
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100,
                PagoSiniestro = PagoSiniestro.Participacion,
                PorcentajePagoSiniestro = null,
                MontoSiniestro = null,
                GarantiaPago = DiasGarantiaPago.TreintaDias
            };

            using (var db = new CoaseguroContext(connectionString)) {
                // Moneda del coaseguro en cuestión
                model.Moneda = db
                    .pv_header
                    .Where(header => header.id_pv == idPv)
                    .Select(header => new TipoMonedaViewModel {
                        Id = header.tmoneda.cod_moneda,
                        Simbolo = header.tmoneda.txt_desc_redu,
                        Descripcion = header.tmoneda.txt_desc,
                        ImporteCambio = header.imp_cambio
                    })
                    .FirstOrDefault();

                // Montos de Participación y Prima de GMX
                model.GMX = db
                    .pv_importe
                    .Where(importe => importe.id_pv == idPv && importe.cod_moneda == model.Moneda.Id)
                    .Select(importe => new GMXViewModel {
                        MontoParticipacion = importe.imp_suma_asegurada,
                        MontoPrimaNeta = importe.imp_prima
                    })
                    .FirstOrDefault();

                // Coaseguradora Líder
                model.Lider = db
                    .pv_cia_lider
                    .Where(lider => lider.id_pv == idPv)
                    .Select(lider => new CoaseguradoraLiderViewModel {
                        Id = lider.cod_cia,
                        Nombre = db.mcia.FirstOrDefault(cia => cia.cod_cia == lider.cod_cia).txt_nom_cia,
                        PorcentajeParticipacion = lider.pje_partic
                    })
                    .FirstOrDefault();
            }

            // Porcentaje de GMX
            model.GMX.Porcentaje = 100M - model.Lider.PorcentajeParticipacion;

            // Límite Máximo de Responsabilidad y Prima Neta
            model.PrimaNeta = decimal.Round(model.GMX.MontoPrimaNeta * 100M / model.GMX.Porcentaje, 2);
            model.LimiteMaxResponsabilidad = decimal.Round(model.GMX.MontoParticipacion * 100M / model.GMX.Porcentaje, 2);

            // Montos de Participación y Prima de Coaseguradora Líder
            model.Lider.MontoPrimaNeta = decimal.Round(model.PrimaNeta * model.Lider.PorcentajeParticipacion / 100M, 2);
            model.Lider.MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * model.Lider.PorcentajeParticipacion / 100M, 2);

            return model;
        }

        /// <summary>
        /// Actualiza la información de coaseguro con los datos más recientes y vuelve a rellenar el VistaModelo
        /// para trabajarlo.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="SeguidorViewModel"/>.</returns>
        public override SeguidorViewModel ActualizarYRellenarModelo()
        {
            // Se obtiene la información actualizada para este coaseguro.
            var model = RellenarNuevoModelo();

            using (var db = new CoaseguroContext(connectionString)) {
                var coaseguroPrincipal = db.CoaseguroPrincipal
                    .Include(coas => coas.CoaseguradorasParticipantes)
                    .Include(coas => coas.CoaseguradorasFee)
                    .FirstOrDefault(coas => coas.id_pv == idPv);

                using (var transaction = db.Database.BeginTransaction()) {
                    try {
                        // Se actualizan las tablas de coaseguro correspondientes para esta póliza.
                        var importeCambio = model.Moneda.ImporteCambio;
                        coaseguroPrincipal.LimiteMaximoResponsabilidad = model.LimiteMaxResponsabilidad;
                        coaseguroPrincipal.LimiteMaximoResponsabilidadEquivalente = decimal.Round(model.LimiteMaxResponsabilidad * importeCambio, 2);
                        coaseguroPrincipal.PrimaNeta = model.PrimaNeta;
                        coaseguroPrincipal.PrimaNetaEquivalente = decimal.Round(model.PrimaNeta * importeCambio, 2);
                        coaseguroPrincipal.PorcentajeGMX = model.GMX.Porcentaje;
                        coaseguroPrincipal.MontoParticipacionGMX = model.GMX.MontoParticipacion;
                        coaseguroPrincipal.MontoParticipacionGMXEquivalente = decimal.Round(model.GMX.MontoParticipacion * importeCambio, 2);
                        coaseguroPrincipal.PrimaGMX = model.GMX.MontoPrimaNeta;
                        coaseguroPrincipal.PrimaGMXEquivalente = decimal.Round(model.GMX.MontoPrimaNeta * importeCambio, 2);

                        db.Entry(coaseguroPrincipal).State = EntityState.Modified;
                        db.SaveChanges();

                        var lider = coaseguroPrincipal.CoaseguradorasParticipantes.FirstOrDefault();
                        var gmxFee = coaseguroPrincipal.CoaseguradorasFee.FirstOrDefault();

                        lider.PorcentajeParticipacion = model.Lider.PorcentajeParticipacion;
                        lider.MontoParticipacion = model.Lider.MontoParticipacion;
                        lider.MontoParticipacionEquivalente = decimal.Round(model.Lider.MontoParticipacion * importeCambio, 2);
                        lider.MontoPrima = model.Lider.MontoPrimaNeta;
                        lider.MontoPrimaEquivalente = decimal.Round(model.Lider.MontoPrimaNeta * importeCambio, 2);

                        gmxFee.MontoFee = decimal.Round(gmxFee.PorcentajeFee * model.PrimaNeta / 100M, 2);
                        gmxFee.MontoFeeEquivalente = decimal.Round(gmxFee.MontoFee * importeCambio, 2);

                        model.PorcentajeFeeGMX = gmxFee.PorcentajeFee;
                        model.MontoFeeGMX = gmxFee.MontoFee;

                        db.Entry(lider).State = EntityState.Modified;
                        db.Entry(gmxFee).State = EntityState.Modified;
                        db.SaveChanges();

                        // Se rellena el VistaModelo con la información faltante.
                        model.MetodoPago = (MetodoPago)coaseguroPrincipal.IdMetodoPago;
                        model.PagoComisionAgente = (PagoComisionAgente)coaseguroPrincipal.IdPagoComisionAgente;
                        model.PagoSiniestro = (PagoSiniestro)coaseguroPrincipal.IdPagoSiniestro;
                        model.PorcentajePagoSiniestro = coaseguroPrincipal.PorcentajeSiniestro;
                        model.MontoSiniestro = coaseguroPrincipal.MontoSiniestro;
                        model.GarantiaPago = (DiasGarantiaPago)coaseguroPrincipal.IdGarantiaPago;

                        model.PorcentajeTotalParticipacion = lider.PorcentajeParticipacion + model.GMX.Porcentaje;
                        model.MontoTotalParticipacion = lider.MontoParticipacion + model.GMX.MontoParticipacion;
                        model.MontoPrimaNetaTotalParticipacion = lider.MontoPrima + model.GMX.MontoPrimaNeta;

                        transaction.Commit();
                    } catch {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// Inserta en la base de datos los nuevos registros del VistaModelo indicado.
        /// </summary>
        /// <param name="model">El VistaModelo del formulario.</param>
        public override void GuardarCoaseguro(SeguidorViewModel model)
        {
            if (!ValidarMontos(model)) {
                throw new Exception("Los montos de Porcentaje, Monto y Prima totales de participación no concuerdan con el límite máximo de responsabilidad y la prima neta.");
            }

            using (var db = new CoaseguroContext(connectionString))
            using (var transaction = db.Database.BeginTransaction()) {
                try {
                    var importeCambio = model.Moneda.ImporteCambio;
                    var coaseguroPrincipal = new CoaseguroPrincipal {
                        id_pv = idPv,
                        cod_tipo_mov = (decimal)TipoMovimiento.Lider,
                        cod_moneda = model.Moneda.Id,
                        LimiteMaximoResponsabilidad = model.LimiteMaxResponsabilidad,
                        LimiteMaximoResponsabilidadEquivalente = decimal.Round(model.LimiteMaxResponsabilidad * importeCambio, 2),
                        PrimaNeta = model.PrimaNeta,
                        PrimaNetaEquivalente = decimal.Round(model.PrimaNeta * importeCambio, 2),
                        PorcentajeGMX = model.GMX.Porcentaje,
                        MontoParticipacionGMX = model.GMX.MontoParticipacion,
                        MontoParticipacionGMXEquivalente = decimal.Round(model.GMX.MontoParticipacion * importeCambio, 2),
                        PrimaGMX = model.GMX.MontoPrimaNeta,
                        PrimaGMXEquivalente = decimal.Round(model.GMX.MontoPrimaNeta * importeCambio, 2),
                        IdMetodoPago = (int)model.MetodoPago,
                        IdPagoComisionAgente = (int)model.PagoComisionAgente,
                        IdGarantiaPago = (int)model.GarantiaPago,
                        IdPagoSiniestro = (int)model.PagoSiniestro,
                        PorcentajeSiniestro = model.PorcentajePagoSiniestro,
                        MontoSiniestro = model.MontoSiniestro,
                        MontoSiniestroEquivalente = (model.PagoSiniestro == PagoSiniestro.Participacion)
                            ? null
                            : decimal.Round(model.MontoSiniestro.Value * importeCambio, 2) as decimal?
                    };

                    db.CoaseguroPrincipal.Add(coaseguroPrincipal);
                    db.SaveChanges();

                    var liderParticipante = new CoaseguradorasParticipantes {
                        IdCoaseguro = coaseguroPrincipal.Id,
                        cod_cia = model.Lider.Id,
                        PorcentajeParticipacion = model.Lider.PorcentajeParticipacion,
                        MontoParticipacion = model.Lider.MontoParticipacion,
                        MontoParticipacionEquivalente = decimal.Round(model.Lider.MontoParticipacion * importeCambio, 2),
                        MontoPrima = model.Lider.MontoPrimaNeta,
                        MontoPrimaEquivalente = decimal.Round(model.Lider.MontoPrimaNeta * importeCambio, 2)
                    };

                    db.CoaseguradorasParticipantes.Add(liderParticipante);
                    db.SaveChanges();

                    var liderFee = new CoaseguradorasFee {
                        IdCoaseguro = coaseguroPrincipal.Id,
                        cod_cia = model.IdGMX,
                        PorcentajeFee = model.PorcentajeFeeGMX,
                        MontoFee = model.MontoFeeGMX,
                        MontoFeeEquivalente = decimal.Round(model.MontoFeeGMX * importeCambio, 2)
                    };

                    db.CoaseguradorasFee.Add(liderFee);
                    db.SaveChanges();

                    transaction.Commit();
                } catch {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Actualiza los registros necesarios con la información del VistaModelo indicado.
        /// </summary>
        /// <param name="model">El VistaModelo del formulario.</param>
        public override void ActualizarCoaseguro(SeguidorViewModel model)
        {
            if (!ValidarMontos(model)) {
                throw new Exception("Los montos de Porcentaje, Monto y Prima totales de participación no concuerdan con el límite máximo de responsabilidad y la prima neta.");
            }

            using (var db = new CoaseguroContext(connectionString))
            using (var transaction = db.Database.BeginTransaction()) {
                try {
                    var coaseguroPrincipal = db.CoaseguroPrincipal.FirstOrDefault(principal => principal.id_pv == idPv);

                    // Fee por Administración
                    var gmxFee = db
                        .CoaseguradorasFee
                        .FirstOrDefault(fee => fee.IdCoaseguro == coaseguroPrincipal.Id && fee.cod_cia == model.IdGMX);

                    gmxFee.PorcentajeFee = model.PorcentajeFeeGMX;
                    gmxFee.MontoFee = decimal.Round(model.PorcentajeFeeGMX * model.PrimaNeta / 100M, 2);
                    gmxFee.MontoFeeEquivalente = decimal.Round(gmxFee.MontoFee * model.Moneda.ImporteCambio, 2);

                    db.Entry(gmxFee).State = EntityState.Modified;
                    db.SaveChanges();

                    // Método de Pago, Pago por Comisión, Pago de Siniestro y Garantía de Pago
                    coaseguroPrincipal.IdMetodoPago = (int)model.MetodoPago;
                    coaseguroPrincipal.IdPagoComisionAgente = (int)model.PagoComisionAgente;
                    coaseguroPrincipal.IdGarantiaPago = (int)model.GarantiaPago;
                    coaseguroPrincipal.IdPagoSiniestro = (int)model.PagoSiniestro;
                    coaseguroPrincipal.PorcentajeSiniestro = model.PorcentajePagoSiniestro;
                    coaseguroPrincipal.MontoSiniestro = model.MontoSiniestro;
                    coaseguroPrincipal.MontoSiniestroEquivalente = (model.PagoSiniestro == PagoSiniestro.Participacion)
                        ? null
                        : decimal.Round(model.MontoSiniestro.Value * model.Moneda.ImporteCambio, 2) as decimal?;

                    db.Entry(coaseguroPrincipal).State = EntityState.Modified;
                    db.SaveChanges();

                    transaction.Commit();
                } catch {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
