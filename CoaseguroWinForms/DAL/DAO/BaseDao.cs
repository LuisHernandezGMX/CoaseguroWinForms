using System.Linq;
using CoaseguroWinForms.DAL.Entities;
using CoaseguroWinForms.DAL.ViewModels;

namespace CoaseguroWinForms.DAL.DAO
{
    /// <summary>
    /// DAO que contiene el estado y operaciones básicas para el
    /// acceso a la capa de persistencia de la aplicación.
    /// </summary>
    /// <typeparam name="T">El tipo del VistaModelo con el que trabajará el DAO.</typeparam>
    public abstract class BaseDao<T> where T : FormBaseViewModel
    {
        /// <summary>
        /// El Id de la póliza a editar.
        /// </summary>
        protected int idPv;

        /// <summary>
        /// Cadena de conexión a bases del SII.
        /// </summary>
        protected string connectionString;

        /// <summary>
        /// Inicializa una nueva instancia de este DAO.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a trabajar.</param>
        /// <param name="connectionString">Cadena de conexión descifrada del SII.</param>
        public BaseDao(int idPv, string connectionString)
        {
            this.idPv = idPv;
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Regresa True si este coaseguro es nuevo para la póliza indicada. Si ya existe
        /// un coaseguro con el IdPv indicado, regresa False.
        /// </summary>
        /// <returns>Booleano que indica si existe un registro en [CoaseguroPrincipalWkf] con el Id indicado.</returns>
        public bool EsCoaseguroNuevo()
        {
            using (var db = new CoaseguroContext(connectionString)) {
                return !db.CoaseguroPrincipal.Any(coas => coas.id_pv == idPv);
            }
        }

        /// <summary>
        /// Valida que el porcentaje total de participación, el monto total de participación y
        /// el monto total de prima sean igual respectivamente al 100%, al límite máximo de
        /// responsabilidad y a la prima neta.
        /// </summary>
        /// <param name="model">El VistaModelo del formulario</param>
        /// <returns>True si los montos concuerdan, de lo contrario regresa False.</returns>
        protected bool ValidarMontos(T model)
        {
            return
                model.PorcentajeTotalParticipacion == 100.00M
                && model.MontoTotalParticipacion == model.LimiteMaxResponsabilidad
                && model.MontoPrimaNetaTotalParticipacion == model.PrimaNeta;
        }

        /// <summary>
        /// Rellena el VistaModelo con la información necesaria para
        /// trabajarlo por primera vez.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="LiderViewModel"/>.</returns>
        public abstract T RellenarNuevoModelo();

        /// <summary>
        /// Actualiza la información de coaseguro con los datos más recientes y vuelve a rellenar el VistaModelo
        /// para trabajarlo.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="LiderViewModel"/>.</returns>
        public abstract T ActualizarYRellenarModelo();

        /// <summary>
        /// Inserta en la base de datos los nuevos registros del VistaModelo indicado.
        /// </summary>
        /// <param name="model">El VistaModelo del formulario.</param>
        public abstract void GuardarCoaseguro(T model);

        /// <summary>
        /// Actualiza los registros necesarios con la información del VistaModelo indicado.
        /// </summary>
        /// <param name="model">El VistaModelo del formulario.</param>
        public abstract void ActualizarCoaseguro(T model);
    }
}
