using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using VGCore;
using System.IO;
using Lettering.Data;
using Lettering.Errors;
using Lettering.Forms;

namespace Lettering {
    internal enum ReportType { Csv, Sql };
    internal enum ExportType { None, Plt, Eps };
    internal enum ActionType { Cut, Sew, STONE };

    internal class Lettering {
        internal static string errors = "";
        internal static string tempFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TemporaryAutomationFiles\\";
        internal static CorelDRAW.Application corel = new CorelDRAW.Application();
        private static ConfigData config = new ConfigData();

        internal Lettering() {
            string destPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\1 CUT FILES";
            config.filePaths.SetDestPath(destPath);
        }

        internal static void LoadAllConfigs() {
            //TODO(adam): test for folder & files first
            string[] configFiles = Directory.GetFiles(@".\configs\", "*.cfg");
            
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            for(int i = 0; i != configFiles.Length; ++i) {
                loadingWindow.SetFilesProgress(Path.GetFileName(configFiles[i]), i + 1, configFiles.Length);
                ConfigReader.ReadFile(configFiles[i], config, loadingWindow);
            }
            loadingWindow.Hide();
        }

        internal static void AutomateReport(DateTime? startDate, DateTime? endDate) {
            DataTable data = DataReader.RunReport(startDate, endDate);
            if(data == null) {
                ErrorHandler.HandleError(ErrorType.Alert, "No data from report.");
                return;
            }

            ProcessOrders(data);
        }

        internal static void AutomateCsv() {
            DataTable data = DataReader.GetCsvData();
            if(data == null) {
                ErrorHandler.HandleError(ErrorType.Alert, "No data from csv.");
                return;
            }

            ProcessOrders(data);
        }

        private static void ProcessOrders(DataTable data) { 
            bool cancelBuilding = false;

            //config = ConfigManager.getConfig();
            ActiveOrderWindow activeOrderWindow = new ActiveOrderWindow();
            List<string> currentNames = new List<string>();
            List<OrderData> ordersToLog = new List<OrderData>();

            //TODO(adam): messaging
            MessageBox.Show(data.Rows.Count + " entries found");

            //NOTE(adam): convert rows to data entries before loop to allow lookahead
            List<OrderData> orders = new List<OrderData>();
            foreach(DataRow row in data.Rows) {
                OrderData order = new OrderData();
                
                order.cutHouse = (row[DbHeaders.CUT_HOUSE].ToString()).Trim();
                order.scheduleDate = row[DbHeaders.SCHEDULE_DATE] != System.DBNull.Value ? ((System.DateTime)row[DbHeaders.SCHEDULE_DATE]).ToString("d") : "";
                order.enterDate = row[DbHeaders.ENTER_DATE] != System.DBNull.Value ? ((System.DateTime)row[DbHeaders.ENTER_DATE]).ToString("d") : "";
                order.orderNumber = row[DbHeaders.ORDER_NUMBER] != System.DBNull.Value ? Convert.ToInt32(row[DbHeaders.ORDER_NUMBER]) : 0;
                order.voucherNumber = row[DbHeaders.VOUCHER] != System.DBNull.Value ? Convert.ToInt32(row[DbHeaders.VOUCHER]) : 0;
                order.itemCode = (row[DbHeaders.ITEM].ToString()).Trim();
                order.size = row[DbHeaders.SIZE] != System.DBNull.Value ? Convert.ToDouble(row[DbHeaders.SIZE]) : 0;
                order.spec = row[DbHeaders.SPEC] != System.DBNull.Value ? Convert.ToDouble(row[DbHeaders.SPEC]) : 0;
                order.name = (row[DbHeaders.NAME].ToString()).Trim();
                order.word1 = (row[DbHeaders.WORD1].ToString()).Trim();
                order.word2 = (row[DbHeaders.WORD2].ToString()).Trim();
                order.word3 = (row[DbHeaders.WORD3].ToString()).Trim();
                order.word4 = (row[DbHeaders.WORD4].ToString()).Trim();
                order.color1 = (row[DbHeaders.COLOR1].ToString()).Trim();
                order.color2 = (row[DbHeaders.COLOR2].ToString()).Trim();
                order.color3 = (row[DbHeaders.COLOR3].ToString()).Trim();
                order.color4 = (row[DbHeaders.COLOR4].ToString()).Trim();
                order.rushDate = row[DbHeaders.RUSH_DATE] != System.DBNull.Value ? ((System.DateTime)row[DbHeaders.RUSH_DATE]).ToString("d") : "";
                order.comment = "";
                order.nameList = new List<string>();

                orders.Add(order);
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
                string orderPath = config.filePaths.ConstructSavePath(order);
                string newMadePath = config.filePaths.ConstructSavePath(order);

                if(File.Exists(orderPath) || File.Exists(newMadePath)) {
                    order.comment += "Already made";
                    ordersToLog.Add(order);
                    continue;
                }

                if(config.IsIgnoredStyle(order)) {
                    order.comment += "Ignored style";
                    ordersToLog.Add(order);
                    continue;
                }

                if(cancelBuilding) {
                    order.comment += "Cancelled building";
                    ordersToLog.Add(order);
                    continue;
                }

                //NOTE(adam): build point
                // MessageBox.Show("To build: " + order.itemCode + "\n Template: " + config.getTemplatePath(order));
                String templatePath = config.filePaths.ConstructTemplatePath(order);
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
                                System.IO.Directory.CreateDirectory(namesDir);
                                corel.ActiveDocument.SaveAs(namesDir + order.orderNumber + order.voucherNumber.ToString("D3") + ".cdr");
                            } else {
                                System.IO.Directory.CreateDirectory(config.filePaths.ConstructSavePathFolder(order));
                                corel.ActiveDocument.SaveAs(newMadePath);
                                if(config.GetExportType(order.itemCode) != ExportType.None) ExportOrder(order);
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
                    System.Threading.Thread.Sleep(50);      // prevent error on closing templates

                    currentNames.Clear();
                }
            }

            DataWriter.writeLog(ordersToLog, "LetteringLog-" + DateTime.Now.ToString("yyyyMMdd_HHmm"));

            //TODO(adam): messaging
            MessageBox.Show("Done!");
            //TODO(adam): errors display
            if(errors.Length > 0) MessageBox.Show(errors, "Error Log");
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
            string orderWords = config.filePaths.ConstructFileName(order).Replace(".cdr", String.Empty);
            ExportType exportType = config.GetExportType(order.itemCode);


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

            //TODO(adam): condense section since overall format repeated
            switch(exportType) {
                case ExportType.Plt: {
                    if(corel.ActiveSelection.Shapes.Count == 0) {
                        MessageBox.Show("Could no get shapes for exporting. Manual export required.");
                    } else {
                        System.IO.Directory.CreateDirectory(config.filePaths.ConstructExportPathFolder(order, "PLT"));
                        //NOTE(adam): options need to be specified within Corel previously
                        corel.ActiveDocument.Export(config.filePaths.ConstructExportPath(order, "PLT"), cdrFilter.cdrPLT, cdrExportRange.cdrSelection);
                    }
                    } break;
                case ExportType.Eps: {
                    if(corel.ActiveSelection.Shapes.Count == 0) {
                        MessageBox.Show("Could no get shapes for exporting. Manual export required.");
                    } else {
                        System.IO.Directory.CreateDirectory(config.filePaths.ConstructExportPathFolder(order, "EPS"));
                        //NOTE(adam): options need to be specified within Corel previously
                        corel.ActiveDocument.Export(config.filePaths.ConstructExportPath(order, "EPS"), cdrFilter.cdrEPS, cdrExportRange.cdrSelection);
                    }
                    } break;
                default:
                    break;
            }
        }
    }
}