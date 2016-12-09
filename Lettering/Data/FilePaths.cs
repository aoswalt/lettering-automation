using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lettering.Errors;

namespace Lettering.Data {
    public class FilePaths {
        private static readonly string networkAutomationFolderPath = @"\\vsc-fs01\Lettering\Corel\WORK FOLDERS\Automation\";
        public static readonly string configFileName = "lettering.json";
        public static readonly string adjacentConfigFolderPath = @".\configs\";
        public static readonly string networkConfigFolderPath = networkAutomationFolderPath + @"application\configs\";
        public static readonly string adjacentHolidaysFilePath = adjacentConfigFolderPath + "holidays.txt";
        public static readonly string networkHolidaysFilePath = networkConfigFolderPath + "holidays.txt";
        public static readonly string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\';
        public static readonly string desktopSaveFolderPath = desktopFolderPath + @"INLINE\";
        public static readonly string tempFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TemporaryAutomationFiles\";
        public static readonly string installedFontsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + '\\';
        public static readonly string errorLogFilePath = tempFolderPath + "errors.log";

        private static Dictionary<string, Func<OrderData, LetteringType, string>> pathBuilders = new Dictionary<string, Func<OrderData, LetteringType, string>>() {
            {"!type", (order, type) => {
                //NOTE(adam): example: "TTstyle" becomes "TT STYLES"
                foreach(string stylePrefix in Lettering.Config.Setup.StylePrefixes) {
                    OrderData tempOrder = order.Clone();
                    //NOTE(adam): at least "names" is needed here, "mirror" may not be
                    if(Lettering.IsMirroredStyle(tempOrder.itemCode, type)) tempOrder.itemCode = Lettering.GetStyleData(tempOrder.itemCode, type).MirroredStyle;
                    if(Lettering.IsNameStyle(tempOrder.itemCode, type)) tempOrder.itemCode = Lettering.GetStyleData(tempOrder.itemCode, type).MirroredStyle;

                    if(tempOrder.itemCode.StartsWith(stylePrefix)) {
                        return stylePrefix + " STYLES";
                    }
                }
                ErrorHandler.HandleError(ErrorType.Log, "No style prefix found for !style path builder.");
                return "";
            } },
            {"!style", (order, type) => {
                //NOTE(adam): example: "TTstyle" becomes "TT style"
                foreach(string stylePrefix in Lettering.Config.Setup.StylePrefixes) {
                    OrderData tempOrder = order.Clone();
                    //NOTE(adam): at least "names" is needed here, "mirror" may not be
                    if(Lettering.IsMirroredStyle(tempOrder.itemCode, type)) tempOrder.itemCode = Lettering.GetStyleData(tempOrder.itemCode, type).MirroredStyle;
                    if(Lettering.IsNameStyle(tempOrder.itemCode, type)) tempOrder.itemCode = Lettering.GetStyleData(tempOrder.itemCode, type).MirroredStyle;

                    if(tempOrder.itemCode.StartsWith(stylePrefix)) {
                        return stylePrefix + " " + Regex.Replace(tempOrder.itemCode, stylePrefix, "");
                    }
                }
                ErrorHandler.HandleError(ErrorType.Log, "No style prefix found for !style path builder.");
                return "";
            } },
            {"!size", (order, type) => {
                    //NOTE(adam): if int size, force as int; otherwise, allow decimal part
                    if(Math.Ceiling(order.size) == Math.Floor(order.size)) {
                        return (int)order.size + "INCH";
                    } else {
                        return order.size + "INCH";
                    }
                } },
            {"!spec", (order, type) => { return String.Format("{0:0.#}", order.spec); } },
            {"!ya", (order, type) => { return order.spec< 10 ? "YOUTH" : "ADULT"; } },
            {"!cd", (order, type) => {
                //NOTE(adam): using last entered word as cheer/dance
                if(order.word4 != "") {
                    return order.word4.ToUpper();
                } else if(order.word3 != "") {
                    return order.word3.ToUpper();
                } else if(order.word2 != "") {
                    return order.word2.ToUpper();
                } else if(order.word1 != "") {
                    return order.word1.ToUpper();
                } else {
                    ErrorHandler.HandleError(ErrorType.Log, "No words for !cd path builder.");
                    return "";
                }
            } },
            {"!word1", (order, type) => {
                if(order.word1 != "") {
                    return order.word1.ToUpper();
                } else if(order.name != "") {
                    return order.name.ToUpper();
                } else {
                    return "";
                }
            } },
            {"!word2", (order, type) => { return order.word2.ToUpper(); } },
            {"!word3", (order, type) => { return order.word3.ToUpper(); } },
            {"!word4", (order, type) => { return order.word4.ToUpper(); } },
            {"!ecm1", (order, type) => { return ToEcm(order.word1); } },
            {"!ecm2", (order, type) => { return ToEcm(order.word2); } },
            {"!ecm3", (order, type) => { return ToEcm(order.word3); } },
            {"!ecm4", (order, type) => { return ToEcm(order.word4); } },
            {"!name", (order, type) => { return order.name.ToUpper(); } }
        };

        private static Dictionary<string, Func<object, object, bool>> comparisons = new Dictionary<string, Func<object, object, bool>>() {
            { "=", (object a, object b) => { return a.Equals(b); } },
            { "!=", (object a, object b) => { return !a.Equals(b); } },
            { ">", (object a, object b) => { return (double)a > (double)b; } },
            { "<", (object a, object b) => { return (double)a < (double)b; } },
            { ">=", (object a, object b) => { return (double)a >= (double)b; } },
            { "<=", (object a, object b) => { return (double)a <= (double)b; } }
        };

        internal static string ConstructTemplateFilePath(OrderData order, LetteringType type) {
            return BuildPath(order, type, Lettering.Config.Setup.TypeData[type.ToString()].Root + @"!type\!style\!style TEMPLATE.cdr");
        }

        internal static string ConstructFileName(OrderData order, LetteringType type) {
            Data_StyleData styleData;
            if(Lettering.IsMirroredStyle(order.itemCode, type)) {
                styleData = Lettering.GetMirroredStyleData(order.itemCode, type);
            } else {
                styleData = Lettering.GetStyleData(order.itemCode, type);
            }

            string fileName = "";
            if(styleData.CustomWordOrder != null) {
                for(int i = 0; i != styleData.CustomWordOrder.Count; ++i) {
                    if(styleData.CustomWordOrder[i] > 0) {
                        //NOTE(adam): "!word1-" etc
                        fileName += $"!word{styleData.CustomWordOrder[i]}-";
                    }
                }
            } else {
                fileName += "!word1-!word2-!word3-!word4";
            }
            return BuildPath(order, type, fileName);
        }

        private static string ToEcm(string word) {
            return $"ECM{Regex.Replace(word, "[^\\d]+(\\d+)", "$1")}";
        }

        private static string GetRootLast(OrderData order, LetteringType type) {
            string typeRootPath = Lettering.Config.Setup.TypeData[type.ToString()].Root;
            string[] folders = typeRootPath.Split('\\');
            return folders[folders.Length - 1] + '\\';
        }

        private static List<string> getKnownExtensions() {
            List<string> extensions = new List<string>();

            List<Data_TypeData> types = new List<Data_TypeData>(Lettering.Config.Setup.TypeData.Values);
            List<string> typeExtensions = types.ConvertAll(t => t.Extension.ToLower());
            extensions.AddRange(typeExtensions);
            

            List<ExportType> rawExportExtensions = new List<ExportType>((ExportType[])Enum.GetValues(typeof(ExportType)));
            List<string> exportExtensions = rawExportExtensions.ConvertAll(ext => ext.ToString());
            exportExtensions.Remove(ExportType.None.ToString());
            extensions.AddRange(exportExtensions);

            return extensions;
        }

        internal static string ConstructNetworkOrderFilePath(OrderData order, LetteringType type) {
            string path = Lettering.Config.Setup.TypeData[type.ToString()].Root +
                          ConstructStylePathPart(order, type);
            string filename = '\\' + ConstructFileName(order, type) + '.' + 
                              Lettering.Config.Setup.TypeData[type.ToString()].Extension;

            List<string> extensions = getKnownExtensions();
            bool noMatch = extensions.TrueForAll(ext => !path.Contains("." + ext));
            if(noMatch) { path += filename; }

            return BuildPath(order, type, path);
        }

        internal static string ConstructSaveFolderPath(OrderData order, LetteringType type) {
            string lastFolder = GetRootLast(order, type);
            string path = desktopSaveFolderPath + lastFolder + ConstructStylePathPart(order, type);
            return BuildPath(order, type, path);
        }

        internal static string ConstructSaveFilePath(OrderData order, LetteringType type) {
            string path = ConstructSaveFolderPath(order, type);
            string filename = '\\' + ConstructFileName(order, type) + '.' + 
                              Lettering.Config.Setup.TypeData[type.ToString()].Extension;

            List<string> extensions = getKnownExtensions();
            bool noMatch = extensions.TrueForAll(ext => !path.Contains("." + ext));
            if(noMatch) { path += filename; }

            return BuildPath(order, type, path);
        }

        internal static string ConstructExportFolderPath(OrderData order, LetteringType type, string extension) {
            string lastFolder = GetRootLast(order, type);
            string path = desktopSaveFolderPath + lastFolder + ConstructStylePathPart(order, type) + "\\" + extension.ToUpper();
            return BuildPath(order, type, path);
        }

        internal static string ConstructExportFilePath(OrderData order, LetteringType type, string extension) {
            string path = ConstructExportFolderPath(order, type, extension);
            string filename = '\\' + ConstructFileName(order, type) + '.' + extension;

            List<string> extensions = getKnownExtensions();
            bool noMatch = extensions.TrueForAll(ext => !path.Contains("." + ext));
            if(noMatch) { path += filename; }

            return BuildPath(order, type, path);
        }

        private static string ConstructStylePathPart(OrderData order, LetteringType type) {
            //NOTE(adam): special path handling
            if(Lettering.GetStylePath(order.itemCode, type) == "cut-sew_files") {
                return  @"!type\\!style\SEW FILES\";
            }

            if(Lettering.GetStylePath(order.itemCode, type) == "cut-specific") {
                return FilePaths.ConstructStylePathPart(order, LetteringType.Cut) + @"SEW FILES\";
            }

            //NOTE(adam): replace item code if mirror style
            if(Lettering.IsMirroredStyle(order.itemCode, type)) order.itemCode = Lettering.GetStyleData(order.itemCode, type).MirroredStyle;


            string path = "";
            List<Data_Exception> possibleExceptions = Lettering.GetStyleData(order.itemCode, type).Exceptions;
            if(possibleExceptions != null) {
                path = GetExceptionPath(order, type, possibleExceptions);
            }
            
            if(path == "") {
                path = Lettering.GetStylePath(order.itemCode, type);
            }

            return path;
        }

        private static string BuildPath(OrderData order, LetteringType type, string path) {
            foreach(string key in pathBuilders.Keys) {
                if(path.Contains(key)) {
                    path = path.Replace(key, pathBuilders[key](order, type));
                }
            }
            path = Regex.Replace(path, @"-+\B", "");
            return path;
        }

        private static string GetExceptionPath(OrderData order, LetteringType type, List<Data_Exception> possibleExceptions) {
            if(possibleExceptions != null) {
                foreach(Data_Exception ex in possibleExceptions) {
                    if(ex.Conditions == null) {
                        return ex.Path;
                    }

                    foreach(string condition in ex.Conditions) {
                        if(MatchesCondition(order, type, condition)) {
                            return ex.Path;
                        }
                    }
                }
            }
            return "";
        }

        private static bool MatchesCondition(OrderData order, LetteringType type, string condition) {
            string[] tokens = Regex.Split(condition, @"([^\w\d\.]+)");
            if(tokens.Length != 3) {
                ErrorHandler.HandleError(ErrorType.Alert, "Invalid condition: " + condition);
                return false;
            }
            
            string prop = pathBuilders["!" + tokens[0].ToLower()](order, type);
            
            double val;
            if(double.TryParse(tokens[2], out val)) {
                return comparisons[tokens[1]](double.Parse(prop), val);
            } else {
                return comparisons[tokens[1]](prop, tokens[2]);
            }
        }
    }
}
