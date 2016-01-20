﻿using System;
using System.Collections.Generic;

namespace Lettering.Data {
    internal class FilePaths {
        //TODO(adam): decide on naming convention for folder vs file path
        internal static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\';
        internal static string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TemporaryAutomationFiles\\";
        internal static string installedFontsPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + '\\';
        internal static string networkFontsPath = @"\\production\Lettering\Corel\WORK FOLDERS\VS Fonts\";
        internal static string installedLibraryFilePath = @"C:\Program Files\Corel\CorelDRAW Graphics Suite X7\Draw\GMS\Automation.gms";
        internal static string networkLibraryFilePath = @"\\production\Lettering\Corel\WORK FOLDERS\Automation\Automation.gms";

        private readonly ConfigData config;

        //TODO(adam): simplify access to constructing file paths

        //TODO(adam): switch to dictionary with key of path type?
        private string rootCutPath;
        private string rootSewPath;
        private string rootStonePath;
        private string destPath = FilePaths.desktopPath + @"\1 CUT FILES\";

        //TODO(adam): ensure paths always end in \ unless file
        internal FilePaths(ConfigData config) {
            this.config = config;
        }

        internal void SetCutPath(string rootCutPath) {
            this.rootCutPath = rootCutPath;
        }

        internal void SetSewPath(string rootSewPath) {
            this.rootSewPath = rootSewPath;
        }

        internal void SetStonePath(string rootStonePath) {
            this.rootStonePath = rootStonePath;
        }

        internal string ConstructTemplatePath(OrderData order) {
            string dir = rootCutPath + '\\' + config.pathBuilders["!style"](order);
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

        internal string ConstructSavePathFolder(OrderData order) {
            return destPath + ConstructStylePathPart(order);
        }

        internal string ConstructSavePath(OrderData order) {
            return ConstructSavePathFolder(order) + ConstructFileName(order) + ".cdr";
        }

        internal string ConstructExportPathFolder(OrderData order, string extension) {
            return destPath + ConstructStylePathPart(order) + extension.ToUpper() + '\\';
        }

        internal string ConstructExportPath(OrderData order, string extension) {
            return ConstructExportPathFolder(order, extension) + ConstructFileName(order) + '.' + extension;
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
