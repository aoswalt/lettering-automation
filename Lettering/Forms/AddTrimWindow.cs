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
    public partial class AddTrimWindow : Form {
        public string Pattern = "";
        public string Comment = "";
        public AddTrimWindow() {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            Pattern = textPattern.Text;
            Comment = textComment.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
