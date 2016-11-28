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
    public partial class AddPathRuleWindow : Form {
        public string Id = "";
        public string Rule = "";
        public AddPathRuleWindow() {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            Id = textId.Text;
            Rule = textRule.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
