using System;
using System.Collections.Generic;

namespace Lettering.Data {
    internal class FilePaths {
        private readonly ConfigData config;

        internal string rootPath;

        internal FilePaths(ConfigData config) {
            this.config = config;
        }

        internal string GetTemplatePath(OrderData order) {
            string dir = rootPath + '\\' + config.pathBuilders["!style"](order);
            string[] pathTokens = dir.Split('\\');
            string file = pathTokens[pathTokens.Length - 1] + " TEMPLATE.cdr";

            return dir + '\\' + file;
        }

        internal string MakeFileName(OrderData order) {
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
            return (fileName != "" ? fileName.ToUpper() : order.name.ToUpper()) + ".cdr";
        }

        internal string ConstructPath(OrderData order) {
            return rootPath + ConstructPartialPath(order) + MakeFileName(order);
        }

        internal string ConstructPartialPath(OrderData order) {      //NOTE(adam): operating on copy to preserve original data
            //NOTE(adam): replace item code if mirror style
            if(config.pathTypes[config.paths[order.itemCode].type] == "mirror") order.itemCode = config.paths[order.itemCode].mirrorStyle;

            string startPath = "";
            //NOTE(adam): test for exceptions
            List<ExceptionData> possibleExceptions;
            if(config.exceptions.TryGetValue(order.itemCode, out possibleExceptions)) {
                bool noException = true;
                foreach(ExceptionData ex in possibleExceptions) {
                    if(config.exceptionChecks[ex.tag.ToLower()](order, ex)) {
                        startPath = '\\' + ex.path;
                        noException = false;
                        break;
                    }
                }
                //NOTE(adam): fallthrough if no exception match
                if(noException) {
                    startPath = '\\' + config.pathTypes[config.paths[order.itemCode].type];
                }
            } else {
                startPath = '\\' + config.pathTypes[config.paths[order.itemCode].type];
            }

            string[] tokens = startPath.Split('\\');
            string finalPath = "";

            foreach(string token in tokens) {
                if(token.StartsWith("!") && config.pathBuilders.ContainsKey(token)) {
                    finalPath += config.pathBuilders[token](order) + '\\';
                } else {
                    finalPath += token + '\\';
                }
            }

            //finalPath += makeFileName(order);

            //MessageBox.Show(" item: " + order.itemCode + "\nstart: " + startPath + "\n  end: " + finalPath);

            return finalPath;
        }
    }
}
