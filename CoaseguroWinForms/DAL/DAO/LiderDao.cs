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
    public class LiderDao
    {
        /// <summary>
        /// Rellena el VistaModelo del Líder con la información necesaria para
        /// trabajarlo.
        /// </summary>
        /// <param name="idPv">El ID de la póliza a editar.</param>
        /// <param name="cadenaConexion">La cadena de conexión descifrada del SII.</param>
        /// <returns>Una nueva instancia de <see cref="LiderViewModel"/>.</returns>
        public static LiderViewModel RellenarModelo(int idPv, string cadenaConexion)
        {
            var model = new LiderViewModel {
                MetodoPago = MetodoPago.EstadoCuenta,
                PagoComisionAgente = PagoComisionAgente.Lider100,
                PorcentajePagoSiniestro = null,
                GarantiaPago = DiasGarantiaPago.TreintaDias
            };

            using (var db = new CoaseguroContext(cadenaConexion)) {
                // Moneda del coaseguro en cuestión.
                model.Moneda = db
                    .pv_header
                    .Where(header => header.id_pv == idPv)
                    .Select(header => new TipoMonedaViewModel {
                        Id = header.tmoneda.cod_moneda,
                        Simbolo = header.tmoneda.txt_desc_redu,
                        Descripcion = header.tmoneda.txt_desc
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

                // Montos de Participación y Prima Neta de GMX.
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

                foreach (var coas in model.Coaseguradoras) {
                    coas.MontoParticipacion = decimal.Round(model.LimiteMaxResponsabilidad * coas.PorcentajeParticipacion / 100M, 2);
                }

                // Porcentaje de GMX
                model.GMX.Porcentaje = 100M - model
                    .Coaseguradoras
                    .Select(coas => coas.PorcentajeParticipacion)
                    .Sum();
            }

            return model;
        }
    }
}
