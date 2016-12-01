using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Lettering.Data;
using Lettering.Errors;

namespace Lettering {
    internal class ConfigReader {
        private enum Sections { Void, Root, Extension, Types, Prefixes, Paths, Exports, Exceptions, Trims };

        internal static List<DateTime> ReadHolidays() {
            //TODO(adam): move holidays to json config
            string holidaysFilePath = FilePaths.adjacentHolidaysFilePath;
            if(!File.Exists(holidaysFilePath)) {
                holidaysFilePath = FilePaths.networkHolidaysFilePath;
            }

            if(!File.Exists(holidaysFilePath)) {
                ErrorHandler.HandleError(ErrorType.Alert, "Could not find holidays file.");
                return new List<DateTime>();
            }

            List<DateTime> holidays = new List<DateTime>();
            using(StreamReader sr = new StreamReader(holidaysFilePath)) {
                int lineNumber = 0;
                string line;
                while(sr.Peek() > -1) {
                    line = sr.ReadLine().Trim();
                    ++lineNumber;

                    try {
                        holidays.Add(DateTime.Parse(line));
                    } catch(FormatException) {
                        ErrorHandler.HandleError(ErrorType.Log, $"holidays {lineNumber}: Invalid date.");
                        continue;
                    }
                }
            }

            if(holidays.Count == 0) {
                ErrorHandler.HandleError(ErrorType.Alert, "No holidays loaded!");
            }
            return holidays;
        }

        internal static string ParseExport(string pattern) {
            //NOTE(adam): convert style and generic wildcards to regex pattern
            pattern = "^" + pattern.ToUpper();
            pattern = pattern.Replace('?', '.');
            pattern = pattern.Replace("*", ".+");

            return pattern;
        }

        internal static string[] ParseException(string line) {
            //TODO(adam): check keys and values results
            return Regex.Split(line, "([<>=])");
        }
    }
}
