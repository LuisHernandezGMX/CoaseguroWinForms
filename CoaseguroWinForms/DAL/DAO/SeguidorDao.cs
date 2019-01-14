using System.Linq;
using CoaseguroWinForms.DAL.Entities;
using CoaseguroWinForms.DAL.ViewModels;
using CoaseguroWinForms.DAL.ViewModels.Seguidor;

namespace CoaseguroWinForms.DAL.DAO.Seguidor
{
    /// <summary>
    /// Incluye las operaciones necesarias en la capa de persistencia
    /// para el formulario <see cref="SeguidorForm"/>.
    /// </summary>
    public class SeguidorDao
    {
        /// <summary>
        /// Rellena el VistaModelo del Seguidor con la información necesaria para
        /// trabajarlo.
        /// </summary>
        /// <param name="idPv">El ID de la póliza a editar.</param>
        /// <param name="cadenaConexion">La cadena de conexión descifrada del SII.</param>
        /// <returns>Una nueva instancia de <see cref="SeguidorViewModel"/>.</returns>
        public static SeguidorViewModel RellenarModelo(int idPv, string cadenaConexion)
        {
            var model = new SeguidorViewModel {
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100,
                PagoSiniestro = PagoSiniestro.Participacion,
                PorcentajePagoSiniestro = null,
                MontoSiniestro = null,
                GarantiaPago = DiasGarantiaPago.TreintaDias
            };

            using (var db = new CoaseguroContext(cadenaConexion)) {
                // Moneda del coaseguro en cuestión
                model.Moneda = db
                    .pv_header
                    .Where(header => header.id_pv == idPv)
                    .Select(header => new TipoMonedaViewModel {
                        Id = header.tmoneda.cod_moneda,
                        Simbolo = header.tmoneda.txt_desc_redu,
                        Descripcion = header.tmoneda.txt_desc
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
    }
}
