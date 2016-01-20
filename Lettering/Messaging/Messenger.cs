using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lettering {
    internal class Messenger {
        private static MainWindow parentWindow;

        internal static void SetParentWindow(MainWindow parentWindow) {
            Messenger.parentWindow = parentWindow;
        }

        internal static void Show(string message) {
            Show(message, "Lettering");
        }

        internal static void Show(string message, string title) {
            //TODO(adam): add displaying to custom form for auto-sizing, etc
            MessageBox.Show(message, title);
        }
    }
}
