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
    public partial class InputWindow : Form {
        public string Input = "";
        public InputWindow(string message, string title) {
            InitializeComponent();
            labelText.Text = message;
            Text = title;
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            Input = textBoxInput.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
