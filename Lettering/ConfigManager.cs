using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Lettering {
    public class ConfigManager {
        private const string configPath = @"./configs/automation.cfg";
        private enum Sections { Void, Root, Types, Prefixes, Paths, Exports, Exceptions, Trims };

        public static ConfigData getConfig() {
            ConfigData config = new ConfigData();
            Sections curSection = Sections.Void;

            using(StreamReader sr = new StreamReader(configPath)) {
                int lineNumber = 0;
                string line;
                while(sr.Peek() > -1) {
                    line = sr.ReadLine().Trim();
                    ++lineNumber;

                    // do nothing if blank line or comment
                    if(invalidLine(line)) {
                        continue;
                    }

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
                            Lettering.errors += "config " + lineNumber + ": Invalid section header\n";
                        }
                    } else {
                        switch(curSection) {
                            case Sections.Root:
                                config.rootPath = line;
                                break;
                            case Sections.Types:
                                if(!config.parseType(line)) Lettering.errors += "config " + lineNumber + ": Type parse error\n";
                                break;
                            case Sections.Prefixes:
                                if(!config.parsePrefix(line)) Lettering.errors += "config " + lineNumber + ": Prefix parse error\n";
                                break;
                            case Sections.Paths:
                                if(!config.parsePath(line)) Lettering.errors += "config " + lineNumber + ": Path parse error\n";
                                break;
                            case Sections.Exports:
                                if(!config.parseExport(line)) Lettering.errors += "config " + lineNumber + ": Export parse error\n";
                                break;
                            case Sections.Exceptions:
                                if(!config.parseException(line)) Lettering.errors += "config " + lineNumber + ": Exception parse error\n";
                                break;
                            case Sections.Trims:
                                if(!config.parseTrim(line)) Lettering.errors += "config " + lineNumber + ": Trim parse error\n";
                                break;
                            default:
                                Lettering.errors += "config " + lineNumber + ": Unspecified section\n";
                                break;
                        }
                    }
                }
            }

            return config;
        }

        private static bool invalidLine(string line) {
            return !(line.Length > 0 && line[0] != '#');
        }
    }

    public class ConfigData {
        public enum ExportType { NONE, PLT, EPS };

        private delegate string PathBuilderDelegate(OrderData order);
        private delegate bool ExceptionCheckDelegate(OrderData order, ExceptionData exception);

        public string rootPath;
        private Dictionary<int, string> types = new Dictionary<int,string>();
        private List<string> prefixes = new List<string>();
        private Dictionary<string, PathData> paths = new Dictionary<string, PathData>();
        private Dictionary<string, PathBuilderDelegate> pathBuilders = new Dictionary<string, PathBuilderDelegate>();
        private Dictionary<string, ExportType> exports = new Dictionary<string, ExportType>();
        private Dictionary<string, List<ExceptionData>> exceptions = new Dictionary<string, List<ExceptionData>>();
        private Dictionary<string, ExceptionCheckDelegate> exceptionChecks = new Dictionary<string, ExceptionCheckDelegate>();
        private List<string> trims = new List<string>();

        public ConfigData() {
            // add path building functions
            pathBuilders.Add("!style", (order) => {
                // "TTstyle" becomes "TT STYLES\TT style
                foreach(string pre in prefixes) {
                    if(order.itemCode.StartsWith(pre)) {
                        if(types[paths[order.itemCode].type] == "mirror") order.itemCode = paths[order.itemCode].mirrorStyle;
                        if(types[paths[order.itemCode].type] == "names") order.itemCode = paths[order.itemCode].mirrorStyle;

                        return pre + " STYLES\\" + pre + " " + Regex.Replace(order.itemCode, pre, "");
                    }
                }
                return "";  // ensures failure if no match
            });
            pathBuilders.Add("!size", (order) => { return (int)order.size + "INCH"; });
            pathBuilders.Add("!spec", (order) => { return String.Format("{0:0.#}", order.spec); });
            pathBuilders.Add("!ya", (order) => { return order.spec < 10 ? "YOUTH" : "ADULT"; });
            pathBuilders.Add("!cd", (order) => {
                // using last entered word
                if(order.word4 != "") {
                    return order.word4.ToUpper();
                } else if(order.word3 != "") {
                    return order.word3.ToUpper();
                } else if(order.word2 != "") {
                    return order.word2.ToUpper();
                } else if(order.word1 != "") {
                    return order.word1.ToUpper();
                } else {
                    return "";
                }
            });

            // add exception checking functions
            exceptionChecks.Add("size", (order, exception) => { return order.size == exception.value; });
            exceptionChecks.Add("spec", (order, exception) => { return order.spec == exception.value; });
        }

        public bool pathDataExists(string style) {
            return paths.ContainsKey(style);
        }

        public string trimStyleCode(string style) {
            style = style.Replace(" ", String.Empty);
            style = Regex.Replace(style, @"^CF", "TT");     // treat CF as TT

            if(pathDataExists(style)) return style;     // path data exists, no trimming needed

            foreach(string pattern in trims) {
                style = Regex.Replace(style, pattern, "");
                if(pathDataExists(style)) return style;     // path data found
            }

            return "";
        }

        public string getTemplatePath(OrderData order) {
            string dir = rootPath + '\\' + pathBuilders["!style"](order);
            string[] pathTokens = dir.Split('\\');
            string file = pathTokens[pathTokens.Length - 1] + " TEMPLATE.cdr";

            return dir + '\\' + file;
        }

        public void insertType(int i, string desc) {
            types.Add(i, desc);
        }

        public bool parseType(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) {
                return false;
            } else {
                insertType(int.Parse(tokens[0]), tokens[1]);
                return true;
            }
        }

        public bool parsePrefix(string line) {
            prefixes.Add(line);
            return true;
        }

        public void insertPath(string style, int type, int[] wordOrder = null, string mirrorStyle = "") {
            paths.Add(style.Replace(" ", String.Empty), 
                      new PathData(type, wordOrder != null ? wordOrder : new int[] {1, 2, 3, 4}, mirrorStyle.Replace(" ", String.Empty)));
        }

        public bool parsePath(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) return false;     // need at least style and type

            string style = tokens[0];
            int type = int.Parse(tokens[1]);

            // if potential custom word order and mirror style
            if(tokens.Length >= 3) {
                int[] wordOrder;

                // if there is a custom word order, set the array
                if(tokens[2] != String.Empty) {
                    wordOrder = new int[4];     // no words since using custom
                    int[] customWordOrder = Array.ConvertAll(tokens[2].Split(new char[] {',', ' '}, StringSplitOptions.None), s=>int.Parse(s));

                    for(int i = 0; i != customWordOrder.Length; ++i) {
                        wordOrder[i] = customWordOrder[i];
                    }
                } else {
                    wordOrder = new int[] {1, 2, 3, 4};     // default all words
                }

                // if mirror style
                if(tokens.Length >= 4) {
                    string mirrorStyle = tokens[3];
                    insertPath(style, type, wordOrder, mirrorStyle);
                    return true;
                } else {
                    insertPath(style, type, wordOrder);
                    return true;
                }
            } else {
                insertPath(style, type);
                return true;
            }
        }

        public void insertException(string style, string path, string tag, double value) {
            // if there is an entry for the style, add to it; otherwise, create new entry
            List<ExceptionData> exceptionList;
            if(exceptions.TryGetValue(style.Replace(" ", String.Empty), out exceptionList)) {
                exceptionList.Add(new ExceptionData(path, tag, value));
            } else {
                exceptions.Add(style.Replace(" ", String.Empty), new List<ExceptionData> {new ExceptionData(path, tag, value)});
            }
        }

        public bool parseExport(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) return false;     // improper line formatting

            for(int i = 0; i != tokens.Length; ++i) {
                tokens[i] = tokens[i].Replace(" ", String.Empty).ToUpper();
            }

            ExportType e;
            switch(tokens[1]) {
                case "PLT":
                    e = ExportType.PLT;
                    break;
                case "EPS":
                    e = ExportType.EPS;
                    break;
                default:
                    return false;
                    //break;
            }

            String pattern = "^" + tokens[0];
            pattern.Replace('?', '.');
            pattern.Replace("*", ".+");

            exports.Add(pattern, e);
            return true;
        }

        public bool parseException(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 3) return false;     // improper line formatting

            string[] exception = tokens[2].Split('=');
            insertException(tokens[0], tokens[1], exception[0], double.Parse(exception[1]));
            return true;
        }

        public bool parseTrim(string line) {
            trims.Add(line);
            return true;
        }

        public string makeFileName(OrderData order) {
            PathData pathData = ((paths[order.itemCode].mirrorStyle == "") ? 
                paths[order.itemCode] : paths[paths[order.itemCode].mirrorStyle]);
            string fileName = "";

            for(int i = 0; i != pathData.wordOrder.Length; ++i) {
                if(pathData.wordOrder[i] > 0) {
                    switch(pathData.wordOrder[i]) {
                        case 1:
                            fileName += order.word1 + '-';
                            break;
                        case 2:
                            fileName += order.word2 + '-';
                            break;
                        case 3:
                            fileName += order.word3 + '-';
                            break;
                        case 4:
                            fileName += order.word4 + '-';
                            break;
                    }
                }
            }

            fileName = fileName.TrimEnd('-');
            return (fileName != "" ? fileName.ToUpper() : order.name.ToUpper()) + ".cdr";
        }

        public string constructPath(OrderData order) {
            return rootPath + constructPartialPath(order) + makeFileName(order);
        }

        public string constructPartialPath(OrderData order) {      // operating on copy to preserve original data
            // replace item code if mirror style
            if(types[paths[order.itemCode].type] == "mirror") order.itemCode = paths[order.itemCode].mirrorStyle;
            
            string startPath = "";
            // test for exceptions
            List<ExceptionData> possibleExceptions;
            if(exceptions.TryGetValue(order.itemCode, out possibleExceptions)) {
                bool noException = true;
                foreach(ExceptionData ex in possibleExceptions) {
                    if(exceptionChecks[ex.tag.ToLower()](order, ex)) {
                        startPath = '\\' + ex.path;
                        noException = false;
                        break;
                    }
                }
                // fallthrough if no exception match
                if(noException) {
                    startPath = '\\' + types[paths[order.itemCode].type];
                }
            } else {
                startPath = '\\' + types[paths[order.itemCode].type];
            }

            string[] tokens = startPath.Split('\\');
            string finalPath = "";

            foreach(string token in tokens) {
                if(token.StartsWith("!") && pathBuilders.ContainsKey(token)) {
                    finalPath += pathBuilders[token](order) + '\\';
                } else {
                    finalPath += token + '\\';
                }
            }

            //finalPath += makeFileName(order);

            //MessageBox.Show(" item: " + order.itemCode + "\nstart: " + startPath + "\n  end: " + finalPath);

            return finalPath;
        }

        public bool isNameStyle(OrderData order) {
            return types[paths[trimStyleCode(order.itemCode)].type] == "names";
        }

        public ExportType getExportType(OrderData order) {
            foreach(string pattern in exports.Keys) {
                if(Regex.IsMatch(trimStyleCode(order.itemCode), pattern)) {
                    return exports[pattern];
                }
            }
            return ExportType.NONE;
        }

        public bool isIgnoredStyle(OrderData order) {
            return types[paths[trimStyleCode(order.itemCode)].type] == "ignore" ||
                   (paths[trimStyleCode(order.itemCode)].mirrorStyle != "" &&
                    types[paths[paths[trimStyleCode(order.itemCode)].mirrorStyle].type] == "ignore");
        }
    }

    // data structs
    struct PathData {
        public PathData(int type, int[] wordOrder, string mirrorStyle) {
            this.type = type;
            this.wordOrder = (int[])wordOrder.Clone();
            this.mirrorStyle = mirrorStyle;
        }

        public readonly int type;
        public readonly int[] wordOrder;
        public readonly string mirrorStyle;
    }

    struct ExceptionData {
        public ExceptionData(string path, string tag, double value) {
            this.path = path;
            this.tag = tag;
            this.value = value;
        }

        public readonly string path;
        public readonly string tag;
        public readonly double value;
    }

    struct TrimData {
        public TrimData(int length, string pattern) {
            this.length = length;
            this.pattern = pattern;
        }

        public readonly int length;
        public readonly string pattern;
    }
}
