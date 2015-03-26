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

        public void SetInfoDisplay(OrderData order) {
            lblStyle.Text = order.itemCode;
            lblSize.Text = order.size.ToString();
            lblSpec.Text = order.spec.ToString();

            //lblWord1.Text = (order.word1 != "" ? order.word1 : order.name);
            if(order.word1 != "") {
                lblWord1.Text = order.word1;
            } else {
                if(order.nameList.Count == 1) {
                    lblWord1.Text = order.name;
                } else {
                    lblWord1.Text = "<name list>";
                }
            }
            lblWord2.Text = order.word2;
            lblWord3.Text = order.word3;
            lblWord4.Text = order.word4;
        }
    }
}
