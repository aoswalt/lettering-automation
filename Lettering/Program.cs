using System;
using System.IO;
using Lettering.Data;

namespace Lettering {
    internal class Program {
        [STAThread]
        internal static void Main(string[] args) {
            Directory.CreateDirectory(FilePaths.tempFolderPath);

            System.Windows.Forms.Application.EnableVisualStyles();
            MainWindow window = new MainWindow();
            Lettering.SetMainWindow(window);
            window.ShowDialog();
        }
    }
}
