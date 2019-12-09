using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGroup.SysAmigos.UI.Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Modules.Amigos.frmListar());
            //Application.Run(new Modules.Amigos.frmCadastrar());
           //Application.Run(new Modules.Amigos.Operadores_Consulta.frmListar());
        }
    }
}
