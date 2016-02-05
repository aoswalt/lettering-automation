using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Lettering.Data;
using Lettering.Errors;
using Lettering.Forms;
using Lettering.IO;
using VGCore;

namespace Lettering {
    internal enum ReportType { Cut, Sew, Stone };
    internal enum ExportType { None, Plt, Eps };

    //TODO(adam): rethink all of the static usage

    internal class Lettering {
        internal static CorelDRAW.Application corel = new CorelDRAW.Application();

        private static MainWindow mainWindow;
        private static Dictionary<ReportType, ConfigData> configs = new Dictionary<ReportType, ConfigData>();
        private static bool hasCheckedSetup = false;
        private static bool isSetupOk = false;

        internal static void SetMainWindow(MainWindow mainWindow) {
            Lettering.mainWindow = mainWindow;
        }

        internal static void MoveWindowToTop() {
            mainWindow.MoveToTop();
        }

        internal static void LoadAllConfigs() {
            //NOTE(adam): try multiple locations for config files
            string[] configFiles = null;
            if(Directory.Exists(FilePaths.adjacentConfigFolderPath)) {
                configFiles = Directory.GetFiles(FilePaths.adjacentConfigFolderPath, "*.cfg");
            } else if(Directory.Exists(FilePaths.networkConfigFolderPath)) {
                configFiles = Directory.GetFiles(FilePaths.networkConfigFolderPath, "*.cfg");
            }

            if(configFiles == null || configFiles.Length == 0) {
                ErrorHandler.HandleError(ErrorType.Critical, "Could not find config files.");
                return;
            }
            
            ConfigLoadingWindow configLoadingWindow = new ConfigLoadingWindow();
            configLoadingWindow.Show();
            configLoadingWindow.Location = new System.Drawing.Point(mainWindow.Location.X + (mainWindow.Width - configLoadingWindow.Width) / 2,
                                                                    mainWindow.Location.Y + (mainWindow.Height - configLoadingWindow.Height) / 2);
            for(int i = 0; i != configFiles.Length; ++i) {
                string fileName = Path.GetFileName(configFiles[i]);
                ReportType type;
                if(Enum.TryParse(fileName.Split('.')[0], true, out type)) {
                    ConfigData config = new ConfigData();
                    configLoadingWindow.SetFilesProgress(fileName, i + 1, configFiles.Length);
                    ConfigReader.ReadFile(configFiles[i], config, configLoadingWindow);

                    configs.Add(type, config);
                } else {
                    ErrorHandler.HandleError(ErrorType.Alert, $"Could not find type for config file: {fileName}");
                }
            }
            configLoadingWindow.Hide();
            //TODO(adam): investigate program freeze after reading config file
        }

        internal static void CheckFonts() {
            string neededFonts = FontChecker.GetNeededFonts(mainWindow);

            if(neededFonts.Length > 0) {
                //NOTE(adam): open font folder and display message listing needed fonts
                Process.Start(FilePaths.networkFontsFolderPath);
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
            if(configs.Values.Count == 0) {
                LoadAllConfigs();
            }
            
            CheckMacroSetup();
            if(!isSetupOk) { return; }

            DataTable data = ReportReader.RunReport(startDate, endDate, ReportType.Cut);
            if(data == null) {
                ErrorHandler.HandleError(ErrorType.Alert, "No data from report.");
                return;
            }

            ProcessOrders(data);
        }

        internal static void AutomateCsv() {
            if(configs.Values.Count == 0) {
                LoadAllConfigs();
            }

            CheckMacroSetup();
            if(!isSetupOk) { return; }

            DataTable data = CsvReader.GetCsvData();
            if(data == null) {
                ErrorHandler.HandleError(ErrorType.Alert, "No data from csv.");
                return;
            }

            ProcessOrders(data);
        }
        
        internal static void ExportReport(ReportType type, DateTime? startDate, DateTime? endDate) {
            if(configs.Values.Count == 0) {
                LoadAllConfigs();
            }

            if(!configs.ContainsKey(type)) {
                ErrorHandler.HandleError(ErrorType.Critical, $"No config found for {type} orders.");
                return;
            }
            
            DataTable data = ReportReader.RunReport(startDate, endDate, type);
            if(data == null) {
                ErrorHandler.HandleError(ErrorType.Alert, "No data from report.");
                return;
            }

            CheckForDoneOrders(data, type);
        }

        //TODO(adam): combine repeating in CheckForDoneOrders and ProcssOrders

        //TODO(adam): progress bar?
        private static void CheckForDoneOrders(DataTable data, ReportType type) {
            ConfigData config = configs[type];

            List<OrderData> ordersToLog = new List<OrderData>();

            Messenger.Show(data.Rows.Count + " total orders found");

            //NOTE(adam): convert rows to data entries before loop to match processing
            List<OrderData> orders = new List<OrderData>();
            foreach(DataRow row in data.Rows) {
                orders.Add(new OrderData(row));
            }

            for(int i = 0; i != orders.Count; ++i) {
                OrderData order = orders[i];
                string trimmedCode = config.TryTrimStyleCode(order.itemCode);

                //NOTE(adam): if not in config, continue; else, store the trimmed code
                if(trimmedCode.Length == 0) {
                    order.comment += "Not in config";
                    ordersToLog.Add(order);
                    continue;
                } else {
                    order.itemCode = trimmedCode;
                }

                //NOTE(adam): if built, continue
                string orderPath = config.filePaths.ConstructNetworkOrderFilePath(order);
                string newMadePath = config.filePaths.ConstructSaveFilePath(order);

                if(config.IsIgnoredStyle(order)) {
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
            
            string reportFileName = $"{type.ToString()}Report-{DateTime.Now.ToString("yyyyMMdd_HHmm")}";
            CsvWriter.WriteReport(ordersToLog, reportFileName);
            Messenger.Show($"Report saved as {reportFileName}.csv");
        }

        private static void ProcessOrders(DataTable data) {
            bool cancelBuilding = false;
            ConfigData config = configs[ReportType.Cut];

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
                string trimmedCode = config.TryTrimStyleCode(order.itemCode);

                //NOTE(adam): if not in config, continue; else, store the trimmed code
                if(trimmedCode.Length == 0) {
                    order.comment += "Not in config";
                    ordersToLog.Add(order);
                    continue;
                } else {
                    order.itemCode = trimmedCode;
                }

                //NOTE(adam): if built, continue
                string orderPath = config.filePaths.ConstructNetworkOrderFilePath(order);
                string newMadePath = config.filePaths.ConstructSaveFilePath(order);

                if(config.IsIgnoredStyle(order)) {
                    order.comment += "Ignored style";
                    ordersToLog.Add(order);
                    continue;
                }

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
                String templatePath = config.filePaths.ConstructTemplateFilePath(order);
                if(!File.Exists(templatePath)) {
                    ErrorHandler.HandleError(ErrorType.Alert, "Template not found:\n" + templatePath);
                    order.comment += "Template not found";
                    ordersToLog.Add(order);
                } else {
                    currentNames.Add(order.name);

                    if(config.IsNameStyle(order.itemCode)) {
                        //NOTE(adam): if following is name style and same order/voucher, skip processing current list
                        if((i + 1 != orders.Count) && (config.TryTrimStyleCode(orders[i + 1].itemCode).Length > 0) && (config.IsNameStyle(orders[i + 1].itemCode)) && 
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

                            if(config.IsNameStyle(order.itemCode)) {
                                string namesDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Name Styles\" + order.cutHouse + "\\";
                                Directory.CreateDirectory(namesDir);
                                corel.ActiveDocument.SaveAs(namesDir + order.orderNumber + order.voucherNumber.ToString("D3") + ".cdr");
                            } else {
                                Directory.CreateDirectory(config.filePaths.ConstructSaveFolderPath(order));
                                corel.ActiveDocument.SaveAs(newMadePath);
                                if(config.GetExportType(order.itemCode) != ExportType.None) ExportOrder(order, config);
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
                    System.Threading.Thread.Sleep(50);      //NOTE(adamf): delay prevents error on closing templates

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

        private static void ExportOrder(OrderData order, ConfigData config) {
            string orderWords = config.filePaths.ConstructFileName(order).Replace(".cdr", String.Empty);
            ExportType exportType = config.GetExportType(order.itemCode);

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
                Directory.CreateDirectory(config.filePaths.ConstructExportFolderPath(order, exportUpper));
                //NOTE(adam): options need to be specified within Corel previously
                corel.ActiveDocument.Export(config.filePaths.ConstructExportFilePath(order, exportUpper), corelExport, cdrExportRange.cdrSelection);
            }
        }
    }
}