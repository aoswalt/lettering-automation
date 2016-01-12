using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lettering.Errors {
    internal enum ErrorType { Log, Alert, Critical }

    internal class ErrorHandler {
        internal static void HandleError(Error e) {
            switch(e.Type) {
                case ErrorType.Log:
                    {
                        Debug.WriteLine(e.Message + '\n');
                    }
                    break;
                case ErrorType.Alert:
                    {
                        MessageBox.Show(e.Message);
                    }
                    break;
                case ErrorType.Critical:
                    {
                        MessageBox.Show(e.Message);
                        //TODO(adam): abort from here?
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid ErrorType.");
            }
        }
    }
}
