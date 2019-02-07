using System;
using System.Data.Entity;
using System.Linq;

using CoaseguroWinForms.DAL.Entities;
using CoaseguroWinForms.DAL.ViewModels;
using CoaseguroWinForms.DAL.ViewModels.Lider;

namespace CoaseguroWinForms.DAL.DAO.Lider
{
    /// <summary>
    /// Incluye las operaciones necesarias en la capa de persistencia
    /// para el formulario <see cref="LiderForm"/>.
    /// </summary>
    public class LiderDao : BaseDao<LiderViewModel>
    {
        /// <summary>
        /// Inicializa una nueva instancia de este DAO.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a trabajar.</param>
        /// <param name="connectionString">Cadena de conexión descifrada del SII.</param>
        public LiderDao(int idPv, string connectionString) : base(idPv, connectionString) {}

        /// <summary>
        /// Rellena el VistaModelo con la información necesaria para
        /// trabajarlo por primera vez.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="LiderViewModel"/>.</returns>
        public override LiderViewModel RellenarNuevoModelo()
        {
            var model = new LiderViewModel {
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100,
                PagoSiniestro = PagoSiniestro.Participacion,
                PorcentajePagoSiniestro = null,
                MontoSiniestro = null,
                FormaIndemnizacion = null,
                GarantiaPago = DiasGarantiaPago.TreintaDias
            };

            using (var db = new CoaseguroContext(connectionString)) {
                // Moneda del coaseguro en cuestión.
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

                // Límite Máximo de Responsabilidad y Prima Neta
                var limiteYPrimaNeta = db
                    .pv_importe_coas
                    .Where(importe => importe.id_pv == idPv && importe.cod_moneda == model.Moneda.Id)
                    .Select(importe => new {
                        LimiteMaximoResponsabilidad = importe.imp_suma_asegurada,
                        PrimaNeta = importe.imp_prima
                    })
                    .FirstOrDefault();

                model.PrimaNeta = limiteYPrimaNeta.PrimaNeta;
                model.LimiteMaxResponsabilidad = limiteYPrimaNeta.LimiteMaximoResponsabilidad;

                // Montos de Participación y Prima de GMX.
                model.GMX = db
                    .pv_importe
                    .Where(importe => importe.id_pv == idPv && importe.cod_moneda == model.Moneda.Id)
                    .Select(importe => new GMXViewModel {
                        MontoParticipacion = importe.imp_suma_asegurada,
                        MontoPrimaNeta = importe.imp_prima
                    })
                    .FirstOrDefault();

                // Coaseguradoras
                model.Coaseguradoras = db
                    .pv_coas_cia
                    .Where(coas => coas.id_pv == idPv)
                    .Select(coas => new CoaseguradoraViewModel {
                        Id = (int)coas.cod_cia_part,
                        Nombre = db.mcia.FirstOrDefault(cia => cia.cod_cia == coas.cod_cia_part).txt_nom_cia,
                        PorcentajeParticipacion = coas.pje_part_prima,
                        MontoPrimaNeta = coas.imp_prima
                    })
                    .ToList();
            }

            // Monto de Participación de Aseguradoras
            foreach (var coas in model.Coaseguradoras) {
                coas.MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * coas.PorcentajeParticipacion / 100M, 2);
            }

            // Porcentaje de GMX
            model.GMX.Porcentaje = 100M - model
                .Coaseguradoras
                .Select(coas => coas.PorcentajeParticipacion)
                .Sum();

            return model;
        }

        /// <summary>
        /// Actualiza la información de coaseguro con los datos más recientes y vuelve a rellenar el VistaModelo
        /// para trabajarlo.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="LiderViewModel"/>.</returns>
        public override LiderViewModel ActualizarYRellenarModelo()
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

                        foreach (var coas in model.Coaseguradoras) {
                            var coaseguradora = coaseguroPrincipal
                                .CoaseguradorasParticipantes
                                .FirstOrDefault(part => part.cod_cia == coas.Id);

                            var coaseguradoraFee = coaseguroPrincipal
                                .CoaseguradorasFee
                                .FirstOrDefault(fee => fee.cod_cia == coas.Id);

                            coaseguradora.PorcentajeParticipacion = coas.PorcentajeParticipacion;
                            coaseguradora.MontoParticipacion = coas.MontoParticipacion;
                            coaseguradora.MontoParticipacionEquivalente = decimal.Round(coas.MontoParticipacion * importeCambio, 2);
                            coaseguradora.MontoPrima = coas.MontoPrimaNeta;
                            coaseguradora.MontoPrimaEquivalente = decimal.Round(coas.MontoPrimaNeta * importeCambio, 2);
                            db.Entry(coaseguradora).State = EntityState.Modified;

                            coaseguradoraFee.MontoFee = decimal.Round(coaseguradoraFee.PorcentajeFee * model.PrimaNeta / 100M, 2);
                            coaseguradoraFee.MontoFeeEquivalente = decimal.Round(coaseguradoraFee.MontoFee * importeCambio, 2);
                            db.Entry(coaseguradoraFee).State = EntityState.Modified;

                            coas.PorcentajeFee = coaseguradoraFee.PorcentajeFee;
                            coas.MontoFee = coaseguradoraFee.MontoFee;
                        }

                        db.SaveChanges();

                        // Se rellena el VistaModelo con la información faltante.
                        model.MetodoPago = (MetodoPago)coaseguroPrincipal.IdMetodoPago;
                        model.PagoComisionAgente = (PagoComisionAgente)coaseguroPrincipal.IdPagoComisionAgente;
                        model.PagoSiniestro = (PagoSiniestro)coaseguroPrincipal.IdPagoSiniestro;
                        model.PorcentajePagoSiniestro = coaseguroPrincipal.PorcentajeSiniestro;
                        model.MontoSiniestro = coaseguroPrincipal.MontoSiniestro;
                        model.FormaIndemnizacion = (IndemnizacionSiniestro?)coaseguroPrincipal.FormaIndemnizacion;
                        model.GarantiaPago = (DiasGarantiaPago)coaseguroPrincipal.IdGarantiaPago;

                        var totalPorcentaje = model
                            .Coaseguradoras
                            .Select(coas => coas.PorcentajeParticipacion)
                            .Sum();

                        var totalMonto = model
                            .Coaseguradoras
                            .Select(coas => coas.MontoParticipacion)
                            .Sum();

                        var totalPrimaNeta = model
                            .Coaseguradoras
                            .Select(coas => coas.MontoPrimaNeta)
                            .Sum();

                        model.PorcentajeTotalParticipacion = totalPorcentaje + model.GMX.Porcentaje;
                        model.MontoTotalParticipacion = totalMonto + model.GMX.MontoParticipacion;
                        model.MontoPrimaNetaTotalParticipacion = totalPrimaNeta + model.GMX.MontoPrimaNeta;

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
        public override void GuardarCoaseguro(LiderViewModel model)
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
                            : decimal.Round(model.MontoSiniestro.Value * importeCambio, 2) as decimal?,
                        FormaIndemnizacion = (model.PagoSiniestro == PagoSiniestro.Participacion)
                            ? null
                            : (int?)model.FormaIndemnizacion.Value
                    };
                    
                    db.CoaseguroPrincipal.Add(coaseguroPrincipal);
                    db.SaveChanges();

                    var coaseguradorasParticipantes = model
                        .Coaseguradoras
                        .Select(coas => new CoaseguradorasParticipantes {
                            IdCoaseguro = coaseguroPrincipal.Id,
                            cod_cia = coas.Id,
                            PorcentajeParticipacion = coas.PorcentajeParticipacion,
                            MontoParticipacion = coas.MontoParticipacion,
                            MontoParticipacionEquivalente = decimal.Round(coas.MontoParticipacion * importeCambio, 2),
                            MontoPrima = coas.MontoPrimaNeta,
                            MontoPrimaEquivalente = decimal.Round(coas.MontoPrimaNeta * importeCambio, 2)
                        });

                    db.CoaseguradorasParticipantes.AddRange(coaseguradorasParticipantes);
                    db.SaveChanges();

                    var coaseguradorasFee = model
                        .Coaseguradoras
                        .Select(coas => new CoaseguradorasFee {
                            IdCoaseguro = coaseguroPrincipal.Id,
                            cod_cia = coas.Id,
                            PorcentajeFee = coas.PorcentajeFee,
                            MontoFee = coas.MontoFee,
                            MontoFeeEquivalente = decimal.Round(coas.MontoFee * importeCambio, 2)
                        });

                    db.CoaseguradorasFee.AddRange(coaseguradorasFee);
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
        public override void ActualizarCoaseguro(LiderViewModel model)
        {
            if (!ValidarMontos(model)) {
                throw new Exception("Los montos de Porcentaje, Monto y Prima totales de participación no concuerdan con el límite máximo de responsabilidad y la prima neta.");
            }

            using (var db = new CoaseguroContext(connectionString))
            using (var transaction = db.Database.BeginTransaction()) {
                try {
                    var coaseguroPrincipal = db.CoaseguroPrincipal.FirstOrDefault(principal => principal.id_pv == idPv);

                    // Fee por Administración
                    foreach (var coas in model.Coaseguradoras) {
                        var coasFee = db
                            .CoaseguradorasFee
                            .FirstOrDefault(fee => fee.IdCoaseguro == coaseguroPrincipal.Id && fee.cod_cia == coas.Id);

                        coasFee.PorcentajeFee = coas.PorcentajeFee;
                        coasFee.MontoFee = decimal.Round(coas.PorcentajeFee * model.PrimaNeta / 100M, 2);
                        coasFee.MontoFeeEquivalente = decimal.Round(coasFee.MontoFee * model.Moneda.ImporteCambio, 2);

                        db.Entry(coasFee).State = EntityState.Modified;
                    }

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
                    coaseguroPrincipal.FormaIndemnizacion = (model.PagoSiniestro == PagoSiniestro.Participacion)
                        ? null
                        : (int?)model.FormaIndemnizacion;

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
