using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lettering {
    internal class Program {
        private MainWindow launcher = new MainWindow();

        [STAThread]
        static void Main(string[] args) {
            //NOTE(adam): check setup will close corel as necessary & prevents continuing if necessary
            if(SetupManager.CheckSetup()) {
                launcher.ShowDialog();
            }
        }
    }
}
