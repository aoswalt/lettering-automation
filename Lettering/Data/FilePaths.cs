using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lettering.Errors;

namespace Lettering.Data {
    public class FilePaths {
        private static readonly string networkAutomationFolderPath = @"\\production\Lettering\Corel\WORK FOLDERS\Automation\";
        public static readonly string adjacentConfigFolderPath = @".\configs\";
        public static readonly string networkConfigFolderPath = networkAutomationFolderPath + @"application\configs\";
        public static readonly string adjacentHolidaysFilePath = adjacentConfigFolderPath + "holidays.txt";
        public static readonly string networkHolidaysFilePath = networkConfigFolderPath + "holidays.txt";
        public static readonly string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\';
        public static readonly string tempFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TemporaryAutomationFiles\";
        public static readonly string installedFontsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + '\\';
        public static readonly string errorLogFilePath = tempFolderPath + "errors.log";

        private static Dictionary<string, Func<OrderData, LetteringType, string>> pathBuilders = new Dictionary<string, Func<OrderData, LetteringType, string>>() {
            {"!style", (order, type) => {
                //NOTE(adam): example: "TTstyle" becomes "TT STYLES\TT style
                foreach(string stylePrefix in Lettering.Config.Setup.StylePrefixes) {
                    OrderData tempOrder = order.Clone();
                    //NOTE(adam): at least "names" is needed here, "mirror" may not be
                    if(Lettering.IsMirroredStyle(tempOrder.itemCode, type)) tempOrder.itemCode = Lettering.GetStyleData(tempOrder.itemCode, type).MirroredStyle;
                    if(Lettering.IsNameStyle(tempOrder.itemCode, type)) tempOrder.itemCode = Lettering.GetStyleData(tempOrder.itemCode, type).MirroredStyle;

                    if(tempOrder.itemCode.StartsWith(stylePrefix)) {
                        return stylePrefix + " STYLES\\" + stylePrefix + " " + Regex.Replace(tempOrder.itemCode, stylePrefix, "");
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
            {"!word1", (order, type) => { return order.word1.ToUpper(); } },
            {"!word2", (order, type) => { return order.word2.ToUpper(); } },
            {"!word3", (order, type) => { return order.word3.ToUpper(); } },
            {"!word4", (order, type) => { return order.word4.ToUpper(); } },
            {"!ecm1", (order, type) => { return Lettering.ToEcm(order.word1); } },
            {"!ecm2", (order, type) => { return Lettering.ToEcm(order.word2); } },
            {"!ecm3", (order, type) => { return Lettering.ToEcm(order.word3); } },
            {"!ecm4", (order, type) => { return Lettering.ToEcm(order.word4); } }
        };

        internal static string ConstructTemplateFilePath(OrderData order, LetteringType type) {
            string dir = Lettering.Config.Setup.TypeData[type.ToString()].Root + DoPathBuilder(order, type, "!style");
            string[] pathTokens = dir.Split('\\');
            string file = pathTokens[pathTokens.Length - 1] + " TEMPLATE.cdr";

            return dir + '\\' + file;
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
                        switch(styleData.CustomWordOrder[i]) {
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
            } else {
                if(order.word1 != null && order.word1.Length > 0) { fileName += order.word1 + '-'; }
                if(order.word2 != null && order.word2.Length > 0) { fileName += order.word2 + '-'; }
                if(order.word3 != null && order.word3.Length > 0) { fileName += order.word3 + '-'; }
                if(order.word4 != null && order.word4.Length > 0) { fileName += order.word4 + '-'; }
            }

            fileName = fileName.TrimEnd('-');
            return (fileName != "" ? fileName.ToUpper() : order.name.ToUpper());
        }

        internal static string ConstructNetworkOrderFilePath(OrderData order, LetteringType type) {
            return Lettering.Config.Setup.TypeData[type.ToString()].Root + 
                   ConstructStylePathPart(order, type) + 
                   ConstructFileName(order, type) + '.' + 
                   Lettering.Config.Setup.TypeData[type.ToString()].Extension;
        }

        internal static string ConstructSaveFolderPath(OrderData order, LetteringType type) {
            string typeRootPath = Lettering.Config.Setup.TypeData[type.ToString()].Root;
            string[] folders = typeRootPath.Split('\\');
            string lastFolder = folders[folders.Length - 1] + '\\';

            return desktopFolderPath + lastFolder + ConstructStylePathPart(order, type);
        }

        internal static string ConstructSaveFilePath(OrderData order, LetteringType type) {
            return ConstructSaveFolderPath(order, type) + 
                   ConstructFileName(order, type) + '.' + 
                   Lettering.Config.Setup.TypeData[type.ToString()].Extension;
        }

        internal static string ConstructExportFolderPath(OrderData order, LetteringType type, string extension) {
            string typeRootPath = Lettering.Config.Setup.TypeData[type.ToString()].Root;
            string[] folders = typeRootPath.Split('\\');
            string lastFolder = folders[folders.Length - 1] + '\\';

            return desktopFolderPath + lastFolder + ConstructStylePathPart(order, type) + extension.ToUpper() + '\\';
        }

        internal static string ConstructExportFilePath(OrderData order, LetteringType type, string extension) {
            return ConstructExportFolderPath(order, type, extension) + ConstructFileName(order, type) + '.' + extension;
        }

        private static string ConstructStylePathPart(OrderData order, LetteringType type) {
            //NOTE(adam): special path handling
            if(Lettering.GetStylePath(order.itemCode, type) == "cut-sew_files") {
                return DoPathBuilder(order, type, "!style") + @"\SEW FILES\";
            }

            if(Lettering.GetStylePath(order.itemCode, type) == "cut-specific") {
                return FilePaths.ConstructStylePathPart(order, LetteringType.Cut) + @"SEW FILES\";
            }

            //NOTE(adam): replace item code if mirror style
            if(Lettering.IsMirroredStyle(order.itemCode, type)) order.itemCode = Lettering.GetStyleData(order.itemCode, type).MirroredStyle;


            string startPath = "";
            List<Data_Exception> possibleExceptions = Lettering.GetStyleData(order.itemCode, type).Exceptions;
            if(possibleExceptions != null) {
                startPath = Lettering.GetExceptionPath(order, possibleExceptions);
            }
            
            if(startPath == "") {
                startPath = Lettering.GetStylePath(order.itemCode, type);
            }

            return BuildPath(order, type, startPath);
        }

        public static string BuildPath(OrderData order, LetteringType type, string startPath) {
            string[] tokens = startPath.Split('\\');
            string finalPath = "";

            foreach(string token in tokens) {
                if(token.StartsWith("!") && pathBuilders.ContainsKey(token)) {
                    finalPath += DoPathBuilder(order, type, token) + '\\';
                } else {
                    finalPath += token + '\\';
                }
            }

            return finalPath;
        }

        public static string DoPathBuilder(OrderData order, LetteringType type, string token) {
            return pathBuilders[token](order, type);
        }
    }
}
