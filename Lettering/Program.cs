using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lettering {
    internal class Program {
        [STAThread]
        internal static void Main(string[] args) {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }
    }
}
