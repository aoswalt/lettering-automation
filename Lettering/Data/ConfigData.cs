using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lettering.Data {
    internal class ConfigData {
        internal enum ExportType { NONE, PLT, EPS };

        private delegate string PathBuilderDelegate(OrderData order);
        private delegate bool ExceptionCheckDelegate(OrderData order, ExceptionData exception);

        internal string rootPath;
        private Dictionary<int, string> types = new Dictionary<int, string>();
        private List<string> prefixes = new List<string>();
        private Dictionary<string, PathData> paths = new Dictionary<string, PathData>();
        private Dictionary<string, PathBuilderDelegate> pathBuilders = new Dictionary<string, PathBuilderDelegate>();
        private Dictionary<string, ExportType> exports = new Dictionary<string, ExportType>();
        private Dictionary<string, List<ExceptionData>> exceptions = new Dictionary<string, List<ExceptionData>>();
        private Dictionary<string, ExceptionCheckDelegate> exceptionChecks = new Dictionary<string, ExceptionCheckDelegate>();
        private List<string> trims = new List<string>();

        internal ConfigData() {
            //NOTE(adam): add path building functions
            pathBuilders.Add("!style", (order) => {
                //NOTE(adam): example: "TTstyle" becomes "TT STYLES\TT style
                foreach(string pre in prefixes) {
                    if(order.itemCode.StartsWith(pre)) {
                        if(types[paths[order.itemCode].type] == "mirror") order.itemCode = paths[order.itemCode].mirrorStyle;
                        if(types[paths[order.itemCode].type] == "names") order.itemCode = paths[order.itemCode].mirrorStyle;

                        return pre + " STYLES\\" + pre + " " + Regex.Replace(order.itemCode, pre, "");
                    }
                }
                return "";  //NOTE(adam): ensures failure if no match
            });
            pathBuilders.Add("!size", (order) => { return (int)order.size + "INCH"; });
            pathBuilders.Add("!spec", (order) => { return String.Format("{0:0.#}", order.spec); });
            pathBuilders.Add("!ya", (order) => { return order.spec < 10 ? "YOUTH" : "ADULT"; });
            pathBuilders.Add("!cd", (order) => {
                //NOTE(adam): using last entered word
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

            //NOTE(adam): add exception checking functions
            exceptionChecks.Add("size", (order, exception) => { return order.size == exception.value; });
            exceptionChecks.Add("spec", (order, exception) => { return order.spec == exception.value; });
        }

        internal bool pathDataExists(string style) {
            return paths.ContainsKey(style);
        }

        internal string trimStyleCode(string style) {
            style = style.Replace(" ", String.Empty);
            style = Regex.Replace(style, @"^CF", "TT");     //NOTE(adam): treat CF as TT

            if(pathDataExists(style)) return style;     //NOTE(adam): path data exists, no trimming needed

            foreach(string pattern in trims) {
                style = Regex.Replace(style, pattern, "");
                if(pathDataExists(style)) return style;     //NOTE(adam): path data found
            }

            return "";
        }

        internal string getTemplatePath(OrderData order) {
            string dir = rootPath + '\\' + pathBuilders["!style"](order);
            string[] pathTokens = dir.Split('\\');
            string file = pathTokens[pathTokens.Length - 1] + " TEMPLATE.cdr";

            return dir + '\\' + file;
        }

        internal void insertType(int i, string desc) {
            types.Add(i, desc);
        }

        internal bool parseType(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) {
                return false;
            } else {
                insertType(int.Parse(tokens[0]), tokens[1]);
                return true;
            }
        }

        internal bool parsePrefix(string line) {
            prefixes.Add(line);
            return true;
        }

        internal void insertPath(string style, int type, int[] wordOrder = null, string mirrorStyle = "") {
            paths.Add(style.Replace(" ", String.Empty),
                      new PathData(type, wordOrder != null ? wordOrder : new int[] { 1, 2, 3, 4 }, mirrorStyle.Replace(" ", String.Empty)));
        }

        internal bool parsePath(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) return false;     //NOTE(adam): need at least style and type

            string style = tokens[0];
            int type = int.Parse(tokens[1]);

            //NOTE(adam): if potential custom word order and mirror style
            if(tokens.Length >= 3) {
                int[] wordOrder;

                //NOTE(adam): if there is a custom word order, set the array
                if(tokens[2] != String.Empty) {
                    wordOrder = new int[4];     //NOTE(adam): no words since using custom
                    int[] customWordOrder = Array.ConvertAll(tokens[2].Split(new char[] { ',', ' ' }, StringSplitOptions.None), s => int.Parse(s));

                    for(int i = 0; i != customWordOrder.Length; ++i) {
                        wordOrder[i] = customWordOrder[i];
                    }
                } else {
                    wordOrder = new int[] { 1, 2, 3, 4 };     //NOTE(adam): default all words
                }

                //NOTE(adam): if mirror style
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

        internal void insertException(string style, string path, string tag, double value) {
            //NOTE(adam): if there is an entry for the style, add to it; otherwise, create new entry
            List<ExceptionData> exceptionList;
            if(exceptions.TryGetValue(style.Replace(" ", String.Empty), out exceptionList)) {
                exceptionList.Add(new ExceptionData(path, tag, value));
            } else {
                exceptions.Add(style.Replace(" ", String.Empty), new List<ExceptionData> { new ExceptionData(path, tag, value) });
            }
        }

        internal bool parseExport(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) return false;     //NOTE(adam): improper line formatting

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

        internal bool parseException(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 3) return false;     //NOTE(adam): improper line formatting

            string[] exception = tokens[2].Split('=');
            insertException(tokens[0], tokens[1], exception[0], double.Parse(exception[1]));
            return true;
        }

        internal bool parseTrim(string line) {
            trims.Add(line);
            return true;
        }

        internal string makeFileName(OrderData order) {
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

        internal string constructPath(OrderData order) {
            return rootPath + constructPartialPath(order) + makeFileName(order);
        }

        internal string constructPartialPath(OrderData order) {      //NOTE(adam): operating on copy to preserve original data
            //NOTE(adam): replace item code if mirror style
            if(types[paths[order.itemCode].type] == "mirror") order.itemCode = paths[order.itemCode].mirrorStyle;

            string startPath = "";
            //NOTE(adam): test for exceptions
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
                //NOTE(adam): fallthrough if no exception match
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

        internal bool isNameStyle(OrderData order) {
            return types[paths[trimStyleCode(order.itemCode)].type] == "names";
        }

        internal ExportType getExportType(OrderData order) {
            foreach(string pattern in exports.Keys) {
                if(Regex.IsMatch(trimStyleCode(order.itemCode), pattern)) {
                    return exports[pattern];
                }
            }
            return ExportType.NONE;
        }

        internal bool isIgnoredStyle(OrderData order) {
            return types[paths[trimStyleCode(order.itemCode)].type] == "ignore" ||
                   (paths[trimStyleCode(order.itemCode)].mirrorStyle != "" &&
                    types[paths[paths[trimStyleCode(order.itemCode)].mirrorStyle].type] == "ignore");
        }
    }
}
