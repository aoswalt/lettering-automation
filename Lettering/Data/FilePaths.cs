using System;
using System.Collections.Generic;
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
        public static readonly string networkFontsFolderPath = @"\\production\Lettering\Corel\WORK FOLDERS\VS Fonts\";
        public static readonly string installedLibraryFilePath = @"C:\Program Files\Corel\CorelDRAW Graphics Suite X7\Draw\GMS\Automation.gms";
        public static readonly string networkLibraryFilePath = networkAutomationFolderPath + "Automation.gms";
        public static readonly string errorLogFilePath = tempFolderPath + "errors.log";

        private string rootFolderPath;
        private string destFolderPath = FilePaths.desktopFolderPath + @"\1 CUT FILES\";

        private readonly StyleConfigData config;

        //TODO(adam): simplify access to constructing file paths

        public FilePaths(StyleConfigData config) {
            this.config = config;
        }

        internal void SetRootFolderPath(string rootFolderPath) {
            if(rootFolderPath[rootFolderPath.Length - 1] != '\\') {
                rootFolderPath += '\\';
            }

            this.rootFolderPath = rootFolderPath;
        }

        internal string ConstructTemplateFilePath(OrderData order) {
            string dir = rootFolderPath + config.pathBuilders["!style"](order);
            string[] pathTokens = dir.Split('\\');
            string file = pathTokens[pathTokens.Length - 1] + " TEMPLATE.cdr";

            return dir + '\\' + file;
        }

        internal string ConstructFileName(OrderData order) {
            StylePathData pathData = ((config.paths[order.itemCode].mirrorStyle == "") ?
                config.paths[order.itemCode] : config.paths[config.paths[order.itemCode].mirrorStyle]);
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
            return (fileName != "" ? fileName.ToUpper() : order.name.ToUpper());
        }

        internal string ConstructNetworkOrderFilePath(OrderData order) {
            return rootFolderPath + ConstructStylePathPart(order) + ConstructFileName(order) + config.fileExtension;
        }

        internal string ConstructSaveFolderPath(OrderData order) {
            return destFolderPath + ConstructStylePathPart(order);
        }

        internal string ConstructSaveFilePath(OrderData order) {
            return ConstructSaveFolderPath(order) + ConstructFileName(order) + config.fileExtension;
        }

        internal string ConstructExportFolderPath(OrderData order, string extension) {
            return destFolderPath + ConstructStylePathPart(order) + extension.ToUpper() + '\\';
        }

        internal string ConstructExportFilePath(OrderData order, string extension) {
            return ConstructExportFolderPath(order, extension) + ConstructFileName(order) + '.' + extension;
        }

        private string ConstructStylePathPart(OrderData order) {
            //NOTE(adam): operating on copy to preserve original data
            OrderData tempOrder = order;

            //TODO(adam): this feels really messy. can I access other configs in a better way?
            if(config.GetStyleLookupType(tempOrder.itemCode) != config.styleType) {
                if(config.pathTypes[config.paths[tempOrder.itemCode].type] == "cut-sew_files") {
                    StyleConfigData cutConfig = Lettering.configs[ReportType.Cut];
                    return cutConfig.pathBuilders["!style"](tempOrder) + @"\SEW FILES\";
                }

                if(config.pathTypes[config.paths[tempOrder.itemCode].type] == "cut-specific") {
                    StyleConfigData cutConfig = Lettering.configs[ReportType.Cut];
                    return cutConfig.filePaths.ConstructStylePathPart(tempOrder) + @"SEW FILES\";
                }

                ErrorHandler.HandleError(ErrorType.Alert, "Mismatched style and config with no match found.");
            }

            //NOTE(adam): replace item code if mirror style
            if(config.pathTypes[config.paths[tempOrder.itemCode].type] == "mirror") tempOrder.itemCode = config.paths[tempOrder.itemCode].mirrorStyle;

            string startPath = "";
            //NOTE(adam): test for exceptions
            List<ExceptionData> possibleExceptions;
            if(config.exceptions.TryGetValue(tempOrder.itemCode, out possibleExceptions)) {
                bool noException = true;
                foreach(ExceptionData ex in possibleExceptions) {
                    if(config.exceptionChecks[ex.tag.ToLower()](tempOrder, ex)) {
                        startPath = ex.path;
                        noException = false;
                        break;
                    }
                }
                //NOTE(adam): fallthrough if no exception match
                if(noException) {
                    startPath = config.pathTypes[config.paths[tempOrder.itemCode].type];
                }
            } else {
                startPath = config.pathTypes[config.paths[tempOrder.itemCode].type];
            }

            string[] tokens = startPath.Split('\\');
            string finalPath = "";

            foreach(string token in tokens) {
                if(token.StartsWith("!") && config.pathBuilders.ContainsKey(token)) {
                    finalPath += config.pathBuilders[token](tempOrder) + '\\';
                } else {
                    finalPath += token + '\\';
                }
            }

            return finalPath;
        }
    }
}
