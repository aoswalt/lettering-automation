using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lettering.Forms {
    public partial class AddExportWindow : Form {
        public string Pattern = "";
        public ExportType Type = ExportType.None;
        public AddExportWindow() {
            InitializeComponent();
            cboxType.DataSource = Enum.GetValues(typeof(ExportType));
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            Pattern = textPattern.Text;
            Type = (ExportType)cboxType.SelectedItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
