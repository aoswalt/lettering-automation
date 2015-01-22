using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Lettering {
    public class ConfigManager {
        private const string configPath = @"./configs/automation.cfg";
        private enum Sections {Void, Root, Types, Prefixes, Paths, Exceptions, Trims};

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
        private delegate string PathBuilderDelegate(OrderData order);

        public string rootPath;
        private Dictionary<int, string> types = new Dictionary<int,string>();
        private List<string> prefixes = new List<string>();
        private Dictionary<string, PathData> paths = new Dictionary<string, PathData>();
        private Dictionary<string, PathBuilderDelegate> pathBuilders = new Dictionary<string, PathBuilderDelegate>();
        private Dictionary<string, ExceptionData> exceptions = new Dictionary<string, ExceptionData>();
        private List<string> trims = new List<string>();

        public ConfigData() {
            // add path building functions
            pathBuilders.Add("!style", (order) => {
                foreach(string pre in prefixes) {
                    if(order.itemCode.StartsWith(pre)) {
                        return pre + " STYLES\\" + pre + " " + Regex.Replace(order.itemCode, pre, "");
                    }
                }
                return "";  // ensures failure if no match
            });
            pathBuilders.Add("!size", (order) => { return (int)order.size + "INCH"; });
            pathBuilders.Add("!spec", (order) => { return String.Format("0.#", order.spec); });
            pathBuilders.Add("!ya", (order) => { return order.spec < 10 ? "YOUTH" : "ADULT"; });
            pathBuilders.Add("!cd", (order) => { return order.word2.ToUpper(); });  // current styles only use word 2
        }

        public bool pathDataExists(string style) {
            return paths.ContainsKey(style);
        }

        public string trimStyleCode(string style) {
            style = style.Replace(" ", String.Empty);
            if(pathDataExists(style)) return style;     // path data exists, no trimming needed

            foreach(string pattern in trims) {
                style = Regex.Replace(style, pattern, "");
                if(pathDataExists(style)) return style;     // path data found
            }

            return "";
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

        public void insertPath(string style, int type, bool[] excludes = null, string mirrorStyle = "") {
            paths.Add(style.Replace(" ", String.Empty), 
                      new PathData(type, excludes != null ? excludes : new bool[4], mirrorStyle.Replace(" ", String.Empty)));
        }

        public bool parsePath(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 2) return false;     // need at least style and type

            string style = tokens[0];
            int type = int.Parse(tokens[1]);

            // if potential excludes and mirror style
            if(tokens.Length >= 3) {
                bool[] excludes = new bool[4];

                // if there are exclusions, set the bool array
                if(tokens[2] != String.Empty) {
                    int[] excludeWords = Array.ConvertAll(tokens[2].Split(new char[] {',', ' '}, StringSplitOptions.None), s=>int.Parse(s));

                    foreach(int i in excludeWords) {
                        excludes[i] = true;
                    }
                }

                // if mirror style
                if(tokens.Length >= 4) {
                    string mirrorStyle = tokens[3];
                    insertPath(style, type, excludes, mirrorStyle);
                    return true;
                } else {
                    insertPath(style, type, excludes);
                    return true;
                }
            } else {
                insertPath(style, type);
                return true;
            }
        }

        public void insertException(string style, string path, string tag, double value) {
            exceptions.Add(style.Replace(" ", String.Empty), new ExceptionData(path, tag, value));
        }

        public bool parseException(string line) {
            string[] tokens = line.Split(':');
            if(tokens.Length < 3) return false;     // improper line formatting

            string[] exception = tokens[2].Split('=');
            insertException(tokens[0], tokens[2], exception[0], double.Parse(exception[1]));
            return true;
        }

        public bool parseTrim(string line) {
            trims.Add(line);
            return true;
        }

        private string makeFileName(OrderData order) {
            PathData pathData = paths[order.itemCode];
            string fileName = "";

            if(!pathData.excludes[0]) {
                fileName += order.word1 + '-';
            }

            if(!pathData.excludes[1]) {
                fileName += order.word2 + '-';
            }

            if(!pathData.excludes[2]) {
                fileName += order.word3 + '-';
            }
            
            if(!pathData.excludes[3]) {
                fileName += order.word4 + '-';
            }

            return fileName.TrimEnd('-') + ".cdr";
        }

        public string constructPath(OrderData order) {
            string startPath = rootPath + '\\' + types[paths[order.itemCode].type];
            string[] tokens = startPath.Split('\\');
            string finalPath = "";

            foreach(string token in tokens) {
                if(token.StartsWith("!") && pathBuilders.ContainsKey(token)) {
                    finalPath += pathBuilders[token](order) + '\\';
                } else {
                    finalPath += token + '\\';
                }
            }

            finalPath += makeFileName(order);

            MessageBox.Show("start: " + startPath + "\n  end: " + finalPath);

            return "";
        }
    }

    // data structs
    struct PathData {
        public PathData(int type, bool[] excludes, string mirrorStyle) {
            this.type = type;
            this.excludes = (bool[])excludes.Clone();
            this.mirrorStyle = mirrorStyle;
        }

        public readonly int type;
        public readonly bool[] excludes;
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
