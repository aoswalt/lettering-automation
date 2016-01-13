using System;
using System.IO;
using Lettering.Data;
using Lettering.Errors;

namespace Lettering {
    internal class ConfigReader {
        private const string configPath = @"./configs/automation.cfg";
        private enum Sections { Void, Root, Types, Prefixes, Paths, Exports, Exceptions, Trims };

        internal static ConfigData ReadFile() {
            ConfigData config = new ConfigData();
            Sections curSection = Sections.Void;

            using(StreamReader sr = new StreamReader(configPath)) {
                int lineNumber = 0;
                string line;
                while(sr.Peek() > -1) {
                    line = sr.ReadLine().Trim();
                    ++lineNumber;

                    //NOTE(adam): if is blank line or comment, skip
                    if(!(line.Length > 0 && line[0] != '#')) {
                        continue;
                    }

                    //NOTE(adam): set section or parse line based on current section
                    if(line[0] == '>') {
                        if(line.Contains("ROOT")) {
                            curSection = Sections.Root;
                        } else if(line.Contains("TYPES")) {
                            curSection = Sections.Types;
                        } else if(line.Contains("PREFIXES")) {
                            curSection = Sections.Prefixes;
                        } else if(line.Contains("PATHS")) {
                            curSection = Sections.Paths;
                        } else if(line.Contains("EXPORTS")) {
                            curSection = Sections.Exports;
                        } else if(line.Contains("EXCEPTIONS")) {
                            curSection = Sections.Exceptions;
                        } else if(line.Contains("TRIMS")) {
                            curSection = Sections.Trims;
                        } else {
                            ErrorHandler.HandleError(ErrorType.Log, $"config {lineNumber}: Invalid section header.");
                        }
                    } else {
                        switch(curSection) {
                            case Sections.Root:
                                config.SetRootPath(line);
                                break;
                            case Sections.Types:
                                {
                                    string[] tokens = line.Split(':');
                                    //NOTE(adam): expecting line as #:desc
                                    if(tokens.Length < 2) {
                                        ErrorHandler.HandleError(ErrorType.Log, $"config {lineNumber}: Type parse error.");
                                    } else {
                                        config.InsertPathType(int.Parse(tokens[0]), tokens[1]);
                                    }
                                }
                                break;
                            case Sections.Prefixes:
                                config.InsertStylePrefix(line);
                                break;
                            case Sections.Paths:
                                {
                                    StylePathData path = ParsePath(line);
                                    if(path == null) {
                                        ErrorHandler.HandleError(ErrorType.Log, $"config {lineNumber}: Path parse error.");
                                    } else {
                                        config.InsertPath(path);
                                    }
                                }
                                break;
                            case Sections.Exports:
                                {
                                    ExportData export = ParseExport(line);
                                    if(export == null) {
                                        ErrorHandler.HandleError(ErrorType.Log, $"config {lineNumber}: Export parse error.");
                                    } else {
                                        config.InsertExport(export);
                                    }
                                }
                                break;
                            case Sections.Exceptions:
                                {
                                    ExceptionData exception = ParseException(line);
                                    if(exception == null) {
                                        ErrorHandler.HandleError(ErrorType.Log, $"config {lineNumber}: Exception parse error.");
                                    } else {
                                        config.InsertException(exception);
                                    }
                                }
                                break;
                            case Sections.Trims:
                                config.InsertTrim(line);
                                break;
                            default:
                                ErrorHandler.HandleError(ErrorType.Log, $"config {lineNumber}: Unspecified section.");
                                break;
                        }
                    }
                }
            }

            return config;
        }

        internal static StylePathData ParsePath(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) return null;     //NOTE(adam): expecting style:type:(wordOrder):(mirrorStyle)

            StylePathData path = new StylePathData();
            string style = tokens[0];
            int type = int.Parse(tokens[1]);
            int[] wordOrder = null;
            string mirrorStyle = "";

            //NOTE(adam): if potential custom word order
            if(tokens.Length >= 3) {
                //NOTE(adam): if there is a custom word order, set the array
                if(tokens[2] != String.Empty) {
                    wordOrder = new int[4];     //NOTE(adam): default empty since using custom
                    int[] customWordOrder = Array.ConvertAll(tokens[2].Split(new char[] { ',', ' ' }, StringSplitOptions.None), s => int.Parse(s));

                    for(int i = 0; i != customWordOrder.Length; ++i) {
                        wordOrder[i] = customWordOrder[i];
                    }
                } else {
                    wordOrder = new int[] { 1, 2, 3, 4 };     //NOTE(adam): default all words
                }
            }

            //NOTE(adam): if mirror style
            if(tokens.Length >= 4) {
                mirrorStyle = tokens[3];
            }

            path.style = style;
            path.type = type;
            path.wordOrder = wordOrder;
            path.mirrorStyle = mirrorStyle;
            return path;
        }

        internal static ExportData ParseExport(string line) {
            ExportData export = new ExportData();

            string[] tokens = line.Split(':');
            if(tokens.Length != 2) return null;     //NOTE(adam): expecting style:filetype

            //NOTE(adam): convert style and generic wildcards to regex pattern
            String pattern = "^" + tokens[0].ToUpper();
            pattern.Replace('?', '.');
            pattern.Replace("*", ".+");

            ExportType e;
            if(!Enum.TryParse(tokens[1], true, out e)) {
                return null;
            }

            export.styleRegex = pattern;
            export.fileType = e;
            return export;
        }

        internal static ExceptionData ParseException(string line) {
            ExceptionData exception = new ExceptionData();

            string[] tokens = line.Split(':');
            if(tokens.Length != 3) return null;      //NOTE(adam): expecting style:path:detail

            string[] detail = tokens[2].Split('=');
            if(detail.Length != 2) return null;     //NOTE(adam): expecting tag=value

            exception.style = tokens[0];
            exception.path = tokens[1];
            exception.tag = detail[0];
            exception.value = double.Parse(detail[1]);
            return exception;
        }
    }
}
