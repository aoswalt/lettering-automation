using System;

namespace Lettering {
    internal class Program {
        [STAThread]
        internal static void Main(string[] args) {
            System.Windows.Forms.Application.EnableVisualStyles();
            MainWindow window = new MainWindow();
            Lettering.mainWindow = window;
            window.ShowDialog();
        }
    }
}
