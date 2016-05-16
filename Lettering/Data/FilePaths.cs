using System;
using System.Collections.Generic;

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

        internal static string ConstructTemplateFilePath(OrderData order, LetteringType type) {
            string dir = Lettering.Config.Setup.TypeData[type.ToString()].Root + Lettering.DoPathBuilder(order, type, "!style");
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
            return Lettering.Config.Setup.TypeData[type.ToString()].Root + ConstructStylePathPart(order, type) + ConstructFileName(order, type) + '.' + Lettering.Config.Setup.TypeData[type.ToString()].Extension;
        }

        internal static string ConstructSaveFolderPath(OrderData order, LetteringType type) {
            string typeRootPath = Lettering.Config.Setup.TypeData[type.ToString()].Root;
            string[] folders = typeRootPath.Split('\\');
            string lastFolder = folders[folders.Length - 1] + '\\';

            return desktopFolderPath + lastFolder + ConstructStylePathPart(order, type);
        }

        internal static string ConstructSaveFilePath(OrderData order, LetteringType type) {
            return ConstructSaveFolderPath(order, type) + ConstructFileName(order, type) + '.' + Lettering.Config.Setup.TypeData[type.ToString()].Extension;
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
                return Lettering.DoPathBuilder(order, type, "!style") + @"\SEW FILES\";
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

            return Lettering.BuildPath(order, type, startPath);
        }
    }
}
