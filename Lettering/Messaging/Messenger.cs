using System.Windows.Forms;
using Lettering.Errors;
using Lettering.Forms;

namespace Lettering {
    internal enum MessageButtons { Ok, OkCancel, YesNo }
    
    internal class Messenger {
        private static MainWindow parentWindow;

        internal static void SetParentWindow(MainWindow parentWindow) {
            Messenger.parentWindow = parentWindow;
        }

        internal static void Show(string message) {
            Show(message, "Lettering");
        }

        internal static void Show(string message, string title) {
            Show(message, title, MessageButtons.Ok);
        }

        internal static bool Show(string message, string title, MessageButtons buttons) {
            MessageBoxButtons messsageBoxButtons;
            switch(buttons) {
                case MessageButtons.OkCancel:
                    messsageBoxButtons = MessageBoxButtons.OKCancel;
                    break;
                case MessageButtons.YesNo:
                    messsageBoxButtons = MessageBoxButtons.YesNo;
                    break;
                default:
                    messsageBoxButtons = MessageBoxButtons.OK;
                    break;
            }
            
            Lettering.MoveWindowToTop();
            //TODO(adam): add custom message window too allow centering on parent
            DialogResult result = MessageBox.Show(parentWindow, message, title, messsageBoxButtons);
            return result == DialogResult.OK || result == DialogResult.Yes;
        }

        internal static string Prompt(string message, string title) {
            InputWindow inputWindow = new InputWindow(message, title);
            inputWindow.ShowDialog(parentWindow);
            return inputWindow.Input;
        }

        internal static void ShowErrorLog() {
            string log = "";
            foreach(string line in ErrorHandler.GetErrorLog()) {
                log += $"{line}\n";
            }

            MessageBox.Show(parentWindow, log);
        }
    }
}
