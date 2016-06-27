using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Lettering.Data;
using Lettering.Errors;
using Lettering.Forms;
using Lettering.IO;
using Newtonsoft.Json;
using VGCore;

namespace Lettering {
    public enum LetteringType { Cut, Sew, Stone };
    public enum ExportType { None, Plt, Eps };

    internal class Lettering {
        public static JsonConfigData Config;

        internal static CorelDRAW.Application corel = new CorelDRAW.Application();
        
        private static Dictionary<string, Func<OrderData, LetteringType, string>> pathBuilders = new Dictionary<string, Func<OrderData, LetteringType, string>>() {
            {"!style", (order, type) => {
                //NOTE(adam): example: "TTstyle" becomes "TT STYLES\TT style
                foreach(string stylePrefix in Config.Setup.StylePrefixes) {
                    OrderData tempOrder = order.Clone();
                    //NOTE(adam): at least "names" is needed here, "mirror" may not be
                    if(IsMirroredStyle(tempOrder.itemCode, type)) tempOrder.itemCode = GetStyleData(tempOrder.itemCode, type).MirroredStyle;
                    if(IsNameStyle(tempOrder.itemCode, type)) tempOrder.itemCode = GetStyleData(tempOrder.itemCode, type).MirroredStyle;

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
            {"!ecm1", (order, type) => { return $"ECM{ToEcm(order.word1)}"; } },
            {"!ecm2", (order, type) => { return $"ECM{ToEcm(order.word2)}"; } },
            {"!ecm3", (order, type) => { return $"ECM{ToEcm(order.word3)}"; } },
            {"!ecm4", (order, type) => { return $"ECM{ToEcm(order.word4)}"; } }
        };

        private static Dictionary<string, Func<object, object, bool>> conditionCheckers = new Dictionary<string, Func<object, object, bool>>() {
            { "=", (object a, object b) => { return a.Equals(b); } },
            { "!=", (object a, object b) => { return !a.Equals(b); } },
            { ">", (object a, object b) => { return (double)a > (double)b; } },
            { "<", (object a, object b) => { return (double)a < (double)b; } },
            { ">=", (object a, object b) => { return (double)a >= (double)b; } },
            { "<=", (object a, object b) => { return (double)a <= (double)b; } }
        };

        private static MainWindow mainWindow;
        private static bool hasCheckedSetup = false;
        private static bool isSetupOk = false;

        internal static void SetMainWindow(MainWindow mainWindow) {
            Lettering.mainWindow = mainWindow;
        }

        internal static void MoveWindowToTop() {
            mainWindow.MoveToTop();
        }

        internal static void LoadAllConfigs() {
            Config = JsonConvert.DeserializeObject<JsonConfigData>(File.ReadAllText(FilePaths.desktopFolderPath + "jsonOutput.json"));
        }
        
        internal static void LaunchConfigEditor() {
            LoadAllConfigs();
            EditorWindow editor = new EditorWindow(Config);
            editor.ShowDialog(mainWindow);
            Config = editor.Config;

            File.WriteAllText(FilePaths.desktopFolderPath + "jsonOutput_result.json", JsonConvert.SerializeObject(Config,
                new JsonSerializerSettings() {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                }));
        }

        internal static void CheckFonts() {
            string neededFonts = FontChecker.GetNeededFonts(mainWindow);

            if(neededFonts.Length > 0) {
                //NOTE(adam): open font folder and display message listing needed fonts
                Process.Start(Config.Setup.FilePaths.NetworkFontsFolderPath);
                System.Threading.Thread.Sleep(200);     //NOTE(adam): delay to ensure dialog on top of folder window
                ErrorHandler.HandleError(ErrorType.Alert, $"Font(s) need to be installed or updated:\n{neededFonts}");
            }
        }

        internal static bool CheckMacroSetup() {
            if(!isSetupOk) {
                isSetupOk = SetupManager.CheckMacroSetup(mainWindow, hasCheckedSetup);
                hasCheckedSetup = true;
            }

            return isSetupOk;
        }

        internal static void AutomateReport(DateTime? startDate, DateTime? endDate) {
            if(Config == null) {
                LoadAllConfigs();
            }

            CheckMacroSetup();
            if(!isSetupOk) { return; }

            DataTable data = ReportReader.RunReport(startDate, endDate, LetteringType.Cut);
            if(data == null) {
                ErrorHandler.HandleError(ErrorType.Alert, "No data from report.");
                return;
            }

            ProcessOrders(data);
        }

        internal static void AutomateCsv() {
            if(Config == null) {
                LoadAllConfigs();
            }

            CheckMacroSetup();
            if(!isSetupOk) { return; }


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "csv file (*.csv)|*.csv|txt file (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                DataTable data = CsvReader.Read(openFileDialog.FileName);
                if(data == null) {
                    ErrorHandler.HandleError(ErrorType.Alert, "No data from csv.");
                    return;
                } 

                ProcessOrders(data);
            }
        }

        internal static void ExportReport(LetteringType type, DateTime? startDate, DateTime? endDate) {
            if(Config == null) {
                LoadAllConfigs();
            }

            DataTable data = ReportReader.RunReport(startDate, endDate, type);
            if(data == null) {
                ErrorHandler.HandleError(ErrorType.Alert, "No data from report.");
                return;
            }

            CheckForDoneOrders(data, type);
        }

        //TODO(adam): combine repeating in CheckForDoneOrders and ProcssOrders

        private static void CheckForDoneOrders(DataTable data, LetteringType type) {
            List<OrderData> ordersToLog = new List<OrderData>();

            Messenger.Show(data.Rows.Count + " total orders found");

            ReportProgressWindow progressWindow = new ReportProgressWindow();
            progressWindow.Show();
            progressWindow.Location = new System.Drawing.Point(mainWindow.Location.X + (mainWindow.Width - progressWindow.Width) / 2,
                                                               mainWindow.Location.Y + (mainWindow.Height - progressWindow.Height) / 2);

            //NOTE(adam): convert rows to data entries before loop to match processing
            List<OrderData> orders = new List<OrderData>();
            foreach(DataRow row in data.Rows) {
                orders.Add(new OrderData(row));
            }

            for(int i = 0; i != orders.Count; ++i) {
                progressWindow.SetReportProgress(type, i, orders.Count);

                OrderData order = orders[i];
                string trimmedCode = TryTrimStyleCode(order.itemCode);

                //NOTE(adam): if not in config, continue; else, store the trimmed code
                if(trimmedCode.Length == 0) {
                    order.comment += "Not in config";
                    ordersToLog.Add(order);
                    continue;
                } else {
                    order.itemCode = trimmedCode;
                }
                
                if(GetStyleData(trimmedCode, type) == null) {
                    ErrorHandler.HandleError(ErrorType.Log, $"No data for {trimmedCode} at Type: {type}");
                    order.comment += "Wrong lettering type";
                    ordersToLog.Add(order);
                    continue;
                }

                string orderPath = FilePaths.ConstructNetworkOrderFilePath(order, type);
                string newMadePath = FilePaths.ConstructSaveFilePath(order, type);
                order.path = orderPath;

                if(IsIgnoredStyle(order.itemCode, type)) {
                    order.comment += "Ignored style";
                    ordersToLog.Add(order);
                    continue;
                }

                if(File.Exists(orderPath) || File.Exists(newMadePath)) {
                    order.comment += "Already made";
                    ordersToLog.Add(order);
                    continue;
                } else {
                    order.comment += "Need to build";
                    ordersToLog.Add(order);
                }
            }

            progressWindow.Close();

            string reportFileName = $"{type.ToString()}Report-{DateTime.Now.ToString("yyyyMMdd_HHmm")}";
            CsvWriter.WriteReport(ordersToLog, reportFileName);
            Messenger.Show($"Report saved as {reportFileName}.csv");
        }

        private static void ProcessOrders(DataTable data) {
            LetteringType type = LetteringType.Cut;

            bool cancelBuilding = false;

            ActiveOrderWindow activeOrderWindow = new ActiveOrderWindow();
            List<string> currentNames = new List<string>();
            List<OrderData> ordersToLog = new List<OrderData>();

            mainWindow.Hide();
            Messenger.Show(data.Rows.Count + " entries found");

            //NOTE(adam): convert rows to data entries before loop to allow lookahead
            List<OrderData> orders = new List<OrderData>();
            foreach(DataRow row in data.Rows) {
                orders.Add(new OrderData(row));
            }

            for(int i = 0; i != orders.Count; ++i) {
                OrderData order = orders[i];
                string trimmedCode = TryTrimStyleCode(order.itemCode);

                //NOTE(adam): if not in config, continue; else, store the trimmed code
                if(trimmedCode.Length == 0) {
                    order.comment += "Not in config";
                    ordersToLog.Add(order);
                    continue;
                } else {
                    order.itemCode = trimmedCode;
                }

                string orderPath = FilePaths.ConstructNetworkOrderFilePath(order, type);
                string newMadePath = FilePaths.ConstructSaveFilePath(order, type);
                order.path = orderPath;

                if(IsIgnoredStyle(order.itemCode, type)) {
                    order.comment += "Ignored style";
                    ordersToLog.Add(order);
                    continue;
                }

                //NOTE(adam): if built, continue
                if(File.Exists(orderPath) || File.Exists(newMadePath)) {
                    order.comment += "Already made";
                    ordersToLog.Add(order);
                    continue;
                }

                if(cancelBuilding) {
                    order.comment += "Cancelled building";
                    ordersToLog.Add(order);
                    continue;
                }

                //NOTE(adam): build point
                string templatePath = FilePaths.ConstructTemplateFilePath(order, type);
                if(!File.Exists(templatePath)) {
                    ErrorHandler.HandleError(ErrorType.Alert, "Template not found:\n" + templatePath);
                    order.comment += "Template not found";
                    ordersToLog.Add(order);
                } else {
                    currentNames.Add(order.name);

                    if(IsNameStyle(order.itemCode, type)) {
                        //NOTE(adam): if following is name style and same order/voucher, skip processing current list
                        if((i + 1 != orders.Count) && 
                           (TryTrimStyleCode(orders[i + 1].itemCode).Length > 0) && 
                           (IsNameStyle(orders[i + 1].itemCode, type)) &&
                           (order.orderNumber == orders[i + 1].orderNumber) &&
                           (order.voucherNumber == orders[i + 1].voucherNumber)) {
                            order.comment += "Name style";
                            ordersToLog.Add(order);
                            continue;
                        }
                    }

                    order.nameList = currentNames;

                    BuildOrder(templatePath, order);
                    activeOrderWindow.SetInfoDisplay(order);
                    activeOrderWindow.ShowDialog();

                    if(activeOrderWindow.selection == WindowSelection.NEXT) {
                        if(corel.Documents.Count > 0 && corel.ActiveDocument.Dirty) {
                            ShapeRange shapes = corel.ActiveDocument.ActivePage.FindShapes(null, cdrShapeType.cdrTextShape);
                            shapes.ConvertToCurves();

                            if(IsNameStyle(order.itemCode, type)) {
                                string namesDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Name Styles\" + order.cutHouse + "\\";
                                Directory.CreateDirectory(namesDir);
                                corel.ActiveDocument.SaveAs(namesDir + order.orderNumber + order.voucherNumber.ToString("D3") + ".cdr");
                            } else {
                                Directory.CreateDirectory(FilePaths.ConstructSaveFolderPath(order, type));
                                corel.ActiveDocument.SaveAs(newMadePath);
                                if(GetExportType(order.itemCode, type) != ExportType.None) ExportOrder(order);
                            }
                        }

                        order.comment += "Completed";
                        ordersToLog.Add(order);
                    } else if(activeOrderWindow.selection == WindowSelection.REJECT) {
                        order.comment += "Manually rejected";
                        ordersToLog.Add(order);
                    } else if(activeOrderWindow.selection == WindowSelection.CANCEL) {
                        cancelBuilding = true;
                        order.comment += "Cancelled building";
                        ordersToLog.Add(order);
                    }

                    while(corel.Documents.Count > 0) {
                        corel.ActiveDocument.Close();
                    }
                    System.Threading.Thread.Sleep(50);      //NOTE(adam): delay prevents error on closing templates

                    currentNames.Clear();
                }
            }

            CsvWriter.WriteReport(ordersToLog, "LetteringLog-" + DateTime.Now.ToString("yyyyMMdd_HHmm"));

            Messenger.Show("Done!");
            mainWindow.Show();
        }

        private static void BuildOrder(string templatePath, OrderData order) {
            corel.Visible = true;
            corel.OpenDocument(templatePath);

            if(corel.Documents.Count < 1) {
                ErrorHandler.HandleError(ErrorType.Alert, "No documents open.");
                return;
            }

            if(order.nameList.Count == 0) {
                order.nameList.Add("");
            }

            //NOTE(adam): work around 1-based array in VBA
            order.nameList.Insert(0, "");

            Shape orderShape = corel.ActivePage.Layers["Layer 1"].CreateRectangle2(0, 0, 0.1, 0.1);
            orderShape.Name = "OrderData";
            orderShape.Properties["order", 1] = order.size;
            orderShape.Properties["order", 2] = order.spec;
            orderShape.Properties["order", 3] = order.word1;
            orderShape.Properties["order", 4] = order.word2;
            orderShape.Properties["order", 5] = order.word3;
            orderShape.Properties["order", 6] = order.word4;
            orderShape.Properties["order", 7] = order.nameList.ToArray();

            corel.ActivePage.CreateLayer("Automate");
        }

        private static void ExportOrder(OrderData order) {
            string orderWords = FilePaths.ConstructFileName(order, LetteringType.Cut).Replace(".cdr", String.Empty);
            ExportType exportType = Config.Setup.Exports.Find(x => Regex.Match(order.itemCode, x.StyleRegex).Success).FileType;

            cdrFilter corelExport;
            switch(exportType) {
                case ExportType.Eps:
                    corelExport = cdrFilter.cdrEPS;
                    break;
                case ExportType.Plt:
                    corelExport = cdrFilter.cdrPLT;
                    break;
                default:
                    ErrorHandler.HandleError(ErrorType.Alert, $"Invalid export type: {exportType}");
                    return;
            }

            corel.ActiveDocument.ClearSelection();

            //NOTE(adam): try selecting specific sew shape
            if(exportType == ExportType.Plt) {
                Shape sewShape = corel.ActivePage.FindShape(orderWords + "_sew");
                if(sewShape != null) sewShape.AddToSelection();
            }

            //NOTE(adam): if no shapes found, try selecting by order words
            if(corel.ActiveSelection.Shapes.Count == 0) {
                Shape found = corel.ActivePage.FindShape(orderWords);
                if(found != null) found.AddToSelection();
            }

            //NOTE(adam): if no shapes found, select bottom right
            if(corel.ActiveSelection.Shapes.Count == 0) {
                corel.ActiveDocument.ReferencePoint = cdrReferencePoint.cdrTopLeft;
                corel.ActivePage.SelectShapesFromRectangle(corel.ActivePage.SizeWidth / 2,
                                                           corel.ActivePage.SizeHeight / 2,
                                                           corel.ActivePage.SizeWidth,
                                                           corel.ActivePage.SizeHeight,
                                                           false);
            }

            //NOTE(adam): if no shapes found, select on whole page
            if(corel.ActiveSelection.Shapes.Count == 0) {
                corel.ActiveDocument.ReferencePoint = cdrReferencePoint.cdrTopLeft;
                corel.ActivePage.SelectShapesFromRectangle(0,
                                                           0,
                                                           corel.ActivePage.SizeWidth,
                                                           corel.ActivePage.SizeHeight,
                                                           false);
            }

            //NOTE(adam): if no shapes found, select all
            if(corel.ActiveSelection.Shapes.Count == 0) {
                corel.ActivePage.SelectableShapes.All().AddToSelection();
            }


            if(corel.ActiveSelection.Shapes.Count == 0) {
                Messenger.Show("Could not get shapes for exporting. Manual export required.");
            } else {
                string exportUpper = exportType.ToString().ToUpper();
                Directory.CreateDirectory(FilePaths.ConstructExportFolderPath(order, LetteringType.Cut, exportUpper));
                //NOTE(adam): options need to be specified within Corel previously
                corel.ActiveDocument.Export(FilePaths.ConstructExportFilePath(order, LetteringType.Cut, exportUpper), corelExport, cdrExportRange.cdrSelection);
            }
        }

        public static Data_StyleData GetStyleData(string styleCode, LetteringType type) {
            return GetStyleData(Config.Styles[styleCode], type);
        }

        public static Data_StyleData GetStyleData(Data_Style style, LetteringType type) {
            switch(type) {
                case LetteringType.Cut:
                    return style.Cut;
                case LetteringType.Sew:
                    return style.Sew;
                case LetteringType.Stone:
                    return style.Stone;
                default:
                    ErrorHandler.HandleError(ErrorType.Critical, "Invalid report type for style data");
                    return null;
            }
        }

        public static bool IsIgnoredStyle(string styleCode, LetteringType type) {
            return Config.Setup.PathRules.Find(x => x.Id == GetStyleData(styleCode, type).Rule).Rule == "ignore";
        }

        public static bool IsNameStyle(string styleCode, LetteringType type) {
            return Config.Setup.PathRules.Find(x => x.Id == GetStyleData(styleCode, type).Rule).Rule == "names";
        }

        public static bool IsMirroredStyle(string styleCode, LetteringType type) {
            return GetStyleData(styleCode, type).MirroredStyle != null;
        }

        public static Data_StyleData GetMirroredStyleData(string styleCode, LetteringType type) {
            return GetStyleData(GetStyleData(styleCode, type).MirroredStyle, type);
        }
        
        public static ExportType GetExportType(string styleCode, LetteringType type) {
            return Config.Setup.Exports.Find(x => Regex.Match(styleCode, x.StyleRegex).Success).FileType;
        }

        public static string GetStylePath(string styleCode, LetteringType type) {
            return Config.Setup.PathRules.Find(x => x.Id == GetStyleData(styleCode, type).Rule).Rule;
        }

        public static string TryTrimStyleCode(string styleCode) {
            styleCode = styleCode.Replace(" ", String.Empty);
            styleCode = Regex.Replace(styleCode, @"^CF", "TT");     //NOTE(adam): always treat CF as TT
            styleCode = Regex.Replace(styleCode, @"^JVT", "TT");    //NOTE(adam): always treat JVT as TT

            if(Config.Styles.ContainsKey(styleCode)) return styleCode;     //NOTE(adam): path data exists, no trimming needed
            
            foreach(string pattern in Config.Setup.Trims.Select(trim => trim.Pattern)) {
                styleCode = Regex.Replace(styleCode, pattern, String.Empty);
                if(Config.Styles.ContainsKey(styleCode)) return styleCode;     //NOTE(adam): path data found
            }

            ErrorHandler.HandleError(ErrorType.Log, $"No style found in TryTrimStyleCode for final style code {styleCode}");
            return "";
        }

        public static string GetExceptionPath(OrderData order, List<Data_Exception> possibleExceptions) {
            if(possibleExceptions != null) {
                foreach(Data_Exception ex in possibleExceptions) {
                    if(ex.Conditions == null) {
                        return ex.Path;
                    }

                    foreach(string condition in ex.Conditions) {
                        if(MatchesCondition(order, condition)) {
                            return ex.Path;
                        }
                    }
                }
            }
            return "";
        }

        private static string ToEcm(string word) {
            return $"ECM{Regex.Replace(word, "\\d+", "$1")}";
        }

        private static bool MatchesCondition(OrderData order, string condition) {
            string[] tokens = Regex.Split(condition, @"([^\w\d\.]+)");
            if(tokens.Length != 3) {
                ErrorHandler.HandleError(ErrorType.Alert, "Invalid condition: " + condition);
                return false;
            }

            object prop = null;
            switch(tokens[0].ToLower()) {
                case "size":
                    prop = order.size;
                    break;
                case "spec":
                    prop = order.spec;
                    break;
                case "word1":
                    prop = order.word1;
                    break;
                case "word2":
                    prop = order.word2;
                    break;
                case "word3":
                    prop = order.word3;
                    break;
                case "word4":
                    prop = order.word4;
                    break;
                default:
                    ErrorHandler.HandleError(ErrorType.Alert, "No matching condition property.");
                    break;
            }

            //TODO(adam): get property for condition key
            double val;
            if(double.TryParse(tokens[2], out val)) {
                return conditionCheckers[tokens[1]]((double)prop, val);
            } else {
                return conditionCheckers[tokens[1]](prop, tokens[2]);
            }
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
