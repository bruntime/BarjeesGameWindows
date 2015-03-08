using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barjees.Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the game application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Start a new game instance
            GameApp.Instance.Start(new BarjeesForm());
        }
    }
}
