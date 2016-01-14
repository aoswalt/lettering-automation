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
            Lettering.AutomateCsv();
        }

        private void runButton_Click(object sender, EventArgs e) {
            DateTime? startDate = null;
            if(datePickerStart.Checked) {
                startDate = datePickerStart.Value;
            }

            DateTime? endDate = null;
            if(datePickerEnd.Checked) {
                endDate = datePickerEnd.Value;
            }

            Lettering.AutomateReport(startDate, endDate);
        }

        private void loadAllConfigsToolStripMenuItem_Click(object sender, EventArgs e) {
            Lettering.LoadAllConfigs();
        }
    }
}
