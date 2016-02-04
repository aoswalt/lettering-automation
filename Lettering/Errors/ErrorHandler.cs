using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lettering.Data;
using Lettering.IO;

namespace Lettering.Errors {
    internal enum ErrorType { Log, Alert, Critical }

    internal class ErrorHandler {
        private static List<string> errorLog = new List<string>();

        static ErrorHandler() {
            TextWriter.CreateFile(FilePaths.errorLogFilePath);
        }

        internal static void HandleError(ErrorType type, string message) {
            string logLine = $"{type.ToString().ToUpper()}:  {message}";
            errorLog.Add(logLine);
            TextWriter.AppendFile(FilePaths.errorLogFilePath, logLine);

            switch(type) {
                case ErrorType.Log:
                    {
                        Debug.WriteLine(logLine);
                    }
                    break;
                case ErrorType.Alert:
                    {
                        Debug.WriteLine(logLine);
                        Messenger.Show(message, "Error!");
                    }
                    break;
                case ErrorType.Critical:
                    {
                        Debug.WriteLine(logLine);
                        Messenger.Show(message, "Critical Error!");
                        //TODO(adam): abort from here?
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid ErrorType.");
            }
        }

        internal static List<string> GetErrorLog() {
            return errorLog;
        }
    }
}
