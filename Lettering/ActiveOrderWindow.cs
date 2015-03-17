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
    public enum WindowSelection { NEXT, REJECT, CANCEL }

    public partial class ActiveOrderWindow : Form {
        public WindowSelection selection;

        public ActiveOrderWindow() {
            InitializeComponent();
        }

        private void nextButton_Click(object sender, EventArgs e) {
            selection = WindowSelection.NEXT;
            this.Hide();
        }

        private void rejectButton_Click(object sender, EventArgs e) {
            selection = WindowSelection.REJECT;
            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            selection = WindowSelection.CANCEL;
            this.Hide();
        }
    }
}
