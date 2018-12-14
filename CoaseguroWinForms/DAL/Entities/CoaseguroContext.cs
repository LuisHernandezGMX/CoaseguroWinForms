using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace CoaseguroWinForms.DAL.Entities
{
    /// <summary>
    /// Conexión por Entity Framework a la base de datos de acuerdo
    /// a la cadena de conexión del SII.
    /// </summary>
    public class CoaseguroContext : DbContext
    {
        /// <summary>
        /// Genera una nueva conexión a la base de datos indicada en la cadena
        /// de conexión.
        /// </summary>
        /// <param name="connectionString">La cadena de conexión descifrada del SII.</param>
        public CoaseguroContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<CoaseguroContext>(null);
        }
    }
}