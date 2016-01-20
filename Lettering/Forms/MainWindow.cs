using System;
using System.Windows.Forms;

namespace Lettering {
    internal partial class MainWindow : Form {
        internal MainWindow() {
            InitializeComponent();
        }

        private void btnCsvAutomation_Click(object sender, EventArgs e) {
            Lettering.AutomateCsv();
        }

        private void btnCutAutomation_Click(object sender, EventArgs e) {
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

        private void btnCheckSetup_Click(object sender, EventArgs e) {
            Lettering.CheckSetup();
        }

        private void loadAllConfigsToolStripMenuItem_Click(object sender, EventArgs e) {
            Lettering.LoadAllConfigs();
        }

        private void checkFontsToolStripMenuItem_Click(object sender, EventArgs e) {
            Lettering.CheckFonts();
        }
    }
}
