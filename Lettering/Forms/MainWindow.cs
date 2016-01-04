using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lettering {
    internal partial class MainWindow : Form {
        internal MainWindow() {
            InitializeComponent();
        }

        private void LauncherWindow_Load(object sender, EventArgs e) {

        }

        private void openButton_Click(object sender, EventArgs e) {
            Lettering.Run(ReportType.CSV);
        }

        private void runButton_Click(object sender, EventArgs e) {
            Lettering.Run(ReportType.SQL);
        }
    }
}
