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
    public partial class LauncherWindow : Form {
        public LauncherWindow() {
            InitializeComponent();
        }

        private void LauncherWindow_Load(object sender, EventArgs e) {

        }

        private void openButton_Click(object sender, EventArgs e) {
            Lettering.Run();
        }
    }
}
