using System;
using System.Diagnostics;

namespace Lettering.Errors {
    internal enum ErrorType { Log, Alert, Critical }

    internal class ErrorHandler {
        internal static void HandleError(ErrorType type, string message) {
            switch(type) {
                case ErrorType.Log:
                    {
                        Debug.WriteLine(message);
                    }
                    break;
                case ErrorType.Alert:
                    {
                        Messenger.Show(message, "Error!");
                    }
                    break;
                case ErrorType.Critical:
                    {
                        Messenger.Show(message, "Critical Error!");
                        //TODO(adam): abort from here?
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid ErrorType.");
            }
        }
    }
}
