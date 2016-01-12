using System;
using System.Windows.Forms;

namespace Lettering {
    internal partial class MainWindow : Form {
        internal MainWindow() {
            InitializeComponent();
        }

        private void LauncherWindow_Load(object sender, EventArgs e) {

        }

        private void openButton_Click(object sender, EventArgs e) {
            Lettering.Run(ReportType.Csv);
        }

        private void runButton_Click(object sender, EventArgs e) {
            Lettering.Run(ReportType.Sql);
        }
    }
}
