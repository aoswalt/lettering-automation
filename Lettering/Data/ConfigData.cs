using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lettering.Errors;

namespace Lettering.Data {
    internal class ConfigData {
        internal delegate string PathBuilderDelegate(OrderData order);
        internal delegate bool ExceptionCheckDelegate(OrderData order, ExceptionData exception);

        internal FilePaths filePaths;
        internal Dictionary<int, string> pathTypes = new Dictionary<int, string>();
        internal List<string> stylePrefixes = new List<string>();
        internal Dictionary<string, StylePathData> paths = new Dictionary<string, StylePathData>();
        internal Dictionary<string, PathBuilderDelegate> pathBuilders = new Dictionary<string, PathBuilderDelegate>();
        internal Dictionary<string, ExportType> exports = new Dictionary<string, ExportType>();
        internal Dictionary<string, List<ExceptionData>> exceptions = new Dictionary<string, List<ExceptionData>>();
        internal Dictionary<string, ExceptionCheckDelegate> exceptionChecks = new Dictionary<string, ExceptionCheckDelegate>();
        internal List<string> trims = new List<string>();

        internal ConfigData() {
            filePaths = new FilePaths(this);

            AddPathBuilders();
            AddExceptionChecks();
        }

        internal void AddPathBuilders() {
            pathBuilders.Add("!style", (order) => {
                //NOTE(adam): example: "TTstyle" becomes "TT STYLES\TT style
                foreach(string stylePrefix in stylePrefixes) {
                    if(order.itemCode.StartsWith(stylePrefix)) {
                        //TODO(adam): should these be moved out of the if?
                        if(pathTypes[paths[order.itemCode].type] == "mirror") order.itemCode = paths[order.itemCode].mirrorStyle;
                        if(pathTypes[paths[order.itemCode].type] == "names") order.itemCode = paths[order.itemCode].mirrorStyle;

                        return stylePrefix + " STYLES\\" + stylePrefix + " " + Regex.Replace(order.itemCode, stylePrefix, "");
                    }
                }
                //NOTE(adam): can insert other custom style paths here?
                ErrorHandler.HandleError(ErrorType.Log, "No style prefix found for !style path builder.");
                return "";  //NOTE(adam): ensures failure if no match
            });
            pathBuilders.Add("!size", (order) => { return (int)order.size + "INCH"; });
            pathBuilders.Add("!spec", (order) => { return String.Format("{0:0.#}", order.spec); });
            pathBuilders.Add("!ya", (order) => { return order.spec < 10 ? "YOUTH" : "ADULT"; });
            pathBuilders.Add("!cd", (order) => {
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
            });
        }

        internal void AddExceptionChecks() {
            exceptionChecks.Add("size", (order, exception) => { return order.size == exception.value; });
            exceptionChecks.Add("spec", (order, exception) => { return order.spec == exception.value; });
        }

        internal void InsertPathType(int id, string desc) {
            pathTypes.Add(id, desc);
        }

        internal void InsertStylePrefix(string prefix) {
            stylePrefixes.Add(prefix);
        }

        internal void InsertPath(StylePathData path) {
            path.wordOrder = path.wordOrder != null ? path.wordOrder : new int[] { 1, 2, 3, 4 };
            path.mirrorStyle = path.mirrorStyle.Replace(" ", String.Empty);

            paths.Add(path.style.Replace(" ", String.Empty), path);
        }

        internal void InsertExport(ExportData export) {
            exports.Add(export.styleRegex, export.fileType);
        }

        internal void InsertException(ExceptionData exception) {
            //NOTE(adam): if there is an entry for the style, add to it; otherwise, create new entry
            string style = exception.style.Replace(" ", String.Empty);
            List<ExceptionData> exceptionList;
            if(exceptions.TryGetValue(style, out exceptionList)) {
                exceptionList.Add(exception);
            } else {
                exceptions.Add(style, new List<ExceptionData> { exception });
            }
        }

        internal void InsertTrim(string trim) {
            trims.Add(trim);
        }

        internal string TryTrimStyleCode(string style) {
            style = style.Replace(" ", String.Empty);
            style = Regex.Replace(style, @"^CF", "TT");     //NOTE(adam): always treat CF as TT

            if(paths.ContainsKey(style)) return style;     //NOTE(adam): path data exists, no trimming needed

            //TODO(adam): change this to reuse trims with while & if ?
            foreach(string pattern in trims) {
                style = Regex.Replace(style, pattern, "");
                if(paths.ContainsKey(style)) return style;     //NOTE(adam): path data found
            }
            
            ErrorHandler.HandleError(ErrorType.Log, "No style found in TryTrimStyleCode.");
            return "";
        }

        internal bool IsIgnoredStyle(OrderData order) {
            //TODO(adam): change match to not be string
            return pathTypes[paths[TryTrimStyleCode(order.itemCode)].type] == "ignore" ||
                   (paths[TryTrimStyleCode(order.itemCode)].mirrorStyle != "" &&
                    pathTypes[paths[paths[TryTrimStyleCode(order.itemCode)].mirrorStyle].type] == "ignore");
        }

        internal bool IsNameStyle(string itemCode) {
            //TODO(adam): change match to not be string
            return pathTypes[paths[TryTrimStyleCode(itemCode)].type] == "names";
        }

        internal ExportType GetExportType(string itemCode) {
            foreach(string pattern in exports.Keys) {
                if(Regex.IsMatch(TryTrimStyleCode(itemCode), pattern)) {
                    return exports[pattern];
                }
            }
            return ExportType.None;
        }

        internal void SetRootPath(string rootPath) {
            filePaths.SetCutPath(rootPath);
        }
    }
}
