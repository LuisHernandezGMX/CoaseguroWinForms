using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoaseguroWinForms
{
    static class Program
    {
        /// <summary>
        /// Debe ejecutarse este programa con los siguientes argumentos:
        /// > CoaseguroWinForms.exe [SCommand] [idPv] [esLider]
        /// 
        /// Donde:
        /// 
        /// [SCommand] => Cadena de conexión del SII.
        /// [idPv] => El Id de la póliza a editar.
        /// [esLider] => Booleano que indica si GMX es coaseguradora líder o seguidora
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var sCommand = args[0];
            var idPv = int.Parse(args[1]);
            var esLider = bool.Parse(args[2]);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (esLider) {
                Application.Run(new LiderForm(sCommand, idPv));
            } else {

            }
        }
    }
}
