using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lettering {
    internal class Program {
        [STAThread]
        internal static void Main(string[] args) {
            MainWindow launcher = new MainWindow();

            //NOTE(adam): check setup will close corel as necessary & prevents continuing if necessary
            if(SetupManager.CheckSetup()) {
                launcher.ShowDialog();
            }
        }
    }
}
