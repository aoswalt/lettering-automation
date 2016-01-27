using System;
using System.Collections.Generic;

namespace Lettering.Data {
    internal class FilePaths {
        internal static string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\';
        internal static string tempFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TemporaryAutomationFiles\\";
        internal static string installedFontsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + '\\';
        internal static string networkFontsFolderPath = @"\\production\Lettering\Corel\WORK FOLDERS\VS Fonts\";
        internal static string installedLibraryFilePath = @"C:\Program Files\Corel\CorelDRAW Graphics Suite X7\Draw\GMS\Automation.gms";
        internal static string networkLibraryFilePath = @"\\production\Lettering\Corel\WORK FOLDERS\Automation\Automation.gms";
        internal static string holidaysFilePath = @".\configs\holidays.txt";

        private readonly ConfigData config;

        //TODO(adam): simplify access to constructing file paths

        //TODO(adam): switch to dictionary with key of path type?
        private string rootCutFolderPath;
        private string rootSewFolderPath;
        private string rootStoneFolderPath;
        private string destFolderPath = FilePaths.desktopFolderPath + @"\1 CUT FILES\";
        
        internal FilePaths(ConfigData config) {
            this.config = config;
        }

        internal void SetCutFolderPath(string rootCutFolderPath) {
            if(rootCutFolderPath[rootCutFolderPath.Length - 1] != '\\') {
                rootCutFolderPath += '\\';
            }

            this.rootCutFolderPath = rootCutFolderPath;
        }

        internal void SetSewFolderPath(string rootSewFolderPath) {
            if(rootSewFolderPath[rootSewFolderPath.Length - 1] != '\\') {
                rootSewFolderPath += '\\';
            }

            this.rootSewFolderPath = rootSewFolderPath;
        }

        internal void SetStoneFolderPath(string rootStoneFolderPath) {
            if(rootStoneFolderPath[rootStoneFolderPath.Length - 1] != '\\') {
                rootStoneFolderPath += '\\';
            }

            this.rootStoneFolderPath = rootStoneFolderPath;
        }

        internal string ConstructTemplateFilePath(OrderData order) {
            string dir = rootCutFolderPath + config.pathBuilders["!style"](order);
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

        internal string ConstructNetworkCutFilePath(OrderData order) {
            return rootCutFolderPath + ConstructStylePathPart(order) + ConstructFileName(order) + ".cdr";
        }

        internal string ConstructSaveFolderPath(OrderData order) {
            return destFolderPath + ConstructStylePathPart(order);
        }

        internal string ConstructSaveFilePath(OrderData order) {
            return ConstructSaveFolderPath(order) + ConstructFileName(order) + ".cdr";
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
