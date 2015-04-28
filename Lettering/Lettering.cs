using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using VGCore;
using System.IO;

namespace Lettering {
    public enum ReportType { CSV, SQL };

    public struct OrderData {
        public OrderData(DataRow row) {
            cutHouse = (row[Headers.CUT_HOUSE].ToString()).Trim();
            scheduleDate = row[Headers.SCHEDULE_DATE] != System.DBNull.Value ? ((System.DateTime)row[Headers.SCHEDULE_DATE]).ToString("d") : "";
            enterDate = row[Headers.ENTER_DATE] != System.DBNull.Value ? ((System.DateTime)row[Headers.ENTER_DATE]).ToString("d") : "";
            orderNumber = row[Headers.ORDER_NUMBER] != System.DBNull.Value ? Convert.ToInt32(row[Headers.ORDER_NUMBER]) : 0;
            voucherNumber = row[Headers.VOUCHER] != System.DBNull.Value ? Convert.ToInt32(row[Headers.VOUCHER]) : 0;
            itemCode = (row[Headers.ITEM].ToString()).Trim();
            size = row[Headers.SIZE] != System.DBNull.Value ? Convert.ToDouble(row[Headers.SIZE]) : 0;
            spec = row[Headers.SPEC] != System.DBNull.Value ? Convert.ToDouble(row[Headers.SPEC]) : 0;
            name = (row[Headers.NAME].ToString()).Trim();
            word1 = (row[Headers.WORD1].ToString()).Trim();
            word2 = (row[Headers.WORD2].ToString()).Trim();
            word3 = (row[Headers.WORD3].ToString()).Trim();
            word4 = (row[Headers.WORD4].ToString()).Trim();
            color1 = (row[Headers.COLOR1].ToString()).Trim();
            color2 = (row[Headers.COLOR2].ToString()).Trim();
            color3 = (row[Headers.COLOR3].ToString()).Trim();
            color4 = (row[Headers.COLOR4].ToString()).Trim();
            rushDate = row[Headers.RUSH_DATE] != System.DBNull.Value ? ((System.DateTime)row[Headers.RUSH_DATE]).ToString("d") : "";
            comment = "";
            nameList = new List<string>();
        }

        public string cutHouse;
        public string scheduleDate;
        public string enterDate;
        public int orderNumber;
        public int voucherNumber;
        public string itemCode;
        public double size;
        public double spec;
        public string name;
        public string word1;
        public string word2;
        public string word3;
        public string word4;
        public string color1;
        public string color2;
        public string color3;
        public string color4;
        public string rushDate;
        public string comment;
        public List<string> nameList;
    }

    public class Lettering {
        public static string errors = "";
        public static string tempFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TemporaryAutomationFiles\\";
        private static string destPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\1 CUT FILES";
        public static CorelDRAW.Application corel = new CorelDRAW.Application();
        private static LauncherWindow launcher = new LauncherWindow();
        private static ConfigData config;

        [STAThread]
        static void Main(string[] args) {
            // check setup will close corel as necessary & prevents continuing if necessary
            if(SetupManager.CheckSetup()) {
                launcher.ShowDialog();
            }
        }

        public static void Run(ReportType reportType) {
            bool cancelBuilding = false;

            config = ConfigManager.getConfig();
            ActiveOrderWindow activeOrderWindow = new ActiveOrderWindow();
            List<string> currentNames = new List<string>();
            List<OrderData> ordersToLog = new List<OrderData>();

            DataTable data = ((reportType == ReportType.CSV) ? DataReader.getCsvData() : DataReader.runReport());
            if(data == null) {
                return;
            } else {
                launcher.Hide();
            }

            MessageBox.Show(data.Rows.Count + " entries found");

            // convert rows to data entries before loop to allow lookahead
            List<OrderData> orders = new List<OrderData>();
            foreach(DataRow row in data.Rows) {
                orders.Add(new OrderData(row));
            }

            for(int i = 0; i != orders.Count; ++i) {
                OrderData order = orders[i];
                string trimmedCode = config.trimStyleCode(order.itemCode);

                // if not in config, continue; else, store the trimmed code
                if(trimmedCode.Length == 0) {
                    order.comment += "Not in config";
                    ordersToLog.Add(order);
                    continue;
                } else {
                    order.itemCode = trimmedCode;
                }

                // if built, continue
                string orderPath = config.constructPath(order);
                string newMadePath = destPath + config.constructPartialPath(order) + config.makeFileName(order);

                if(File.Exists(orderPath) || File.Exists(newMadePath)) {
                    order.comment += "Already made";
                    ordersToLog.Add(order);
                    continue;
                }

                if(config.isIgnoredStyle(order)) {
                    order.comment += "Ignored style";
                    ordersToLog.Add(order);
                    continue;
                }

                if(cancelBuilding) {
                    order.comment += "Cancelled building";
                    ordersToLog.Add(order);
                    continue;
                }

                // build
                // MessageBox.Show("To build: " + order.itemCode + "\n Template: " + config.getTemplatePath(order));
                String templatePath = config.getTemplatePath(order);
                if(!File.Exists(templatePath)) {
                    MessageBox.Show("Template not found:\n" + templatePath, "Missing Template", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    order.comment += "Template not found";
                    ordersToLog.Add(order);
                } else {
                    currentNames.Add(order.name);

                    if(config.isNameStyle(order)) {
                        // if following is name style and same order/voucher, skip processing current list
                        if(i + 1 != orders.Count && config.isNameStyle(orders[i + 1]) && 
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

                            if(config.isNameStyle(order)) {
                                string namesDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Name Styles\" + order.cutHouse + "\\";
                                System.IO.Directory.CreateDirectory(namesDir);
                                corel.ActiveDocument.SaveAs(namesDir + order.orderNumber + order.voucherNumber.ToString("D3") + ".cdr");
                            } else {
                                System.IO.Directory.CreateDirectory(destPath + config.constructPartialPath(order));
                                corel.ActiveDocument.SaveAs(newMadePath);
                                if(config.getExportType(order) != ConfigData.ExportType.NONE) ExportOrder(order);
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

            MessageBox.Show("Done!");
            if(errors.Length > 0) MessageBox.Show(errors, "Error Log");
        }

        private static void BuildOrder(string templatePath, OrderData order) {
            corel.Visible = true;
            corel.OpenDocument(templatePath);

            if(corel.Documents.Count < 1) {
                MessageBox.Show("No documents open.");
                return;
            }

            if(order.nameList.Count == 0) {
                order.nameList.Add("");
            }

            // work around 1-based array in VBA
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
            string orderWords = config.makeFileName(order).Replace(".cdr", String.Empty);
            ConfigData.ExportType exportType = config.getExportType(order);


            corel.ActiveDocument.ClearSelection();

            // try selecting specific sew shape
            if(exportType == ConfigData.ExportType.PLT) {
                Shape sewShape = corel.ActivePage.FindShape(orderWords + "_sew");
                if(sewShape != null) sewShape.AddToSelection();
            }

            // if no shapes found, try selecting by order words
            if(corel.ActiveSelection.Shapes.Count == 0) {
                Shape found = corel.ActivePage.FindShape(orderWords);
                if(found != null) found.AddToSelection();
            }

            // if no shapes found, select bottom right
            if(corel.ActiveSelection.Shapes.Count == 0) {
                corel.ActiveDocument.ReferencePoint = cdrReferencePoint.cdrTopLeft;
                corel.ActivePage.SelectShapesFromRectangle(corel.ActivePage.SizeWidth / 2,
                                                           corel.ActivePage.SizeHeight / 2,
                                                           corel.ActivePage.SizeWidth,
                                                           corel.ActivePage.SizeHeight,
                                                           false);
            }

            // if no shapes found, select on whole page
            if(corel.ActiveSelection.Shapes.Count == 0) {
                corel.ActiveDocument.ReferencePoint = cdrReferencePoint.cdrTopLeft;
                corel.ActivePage.SelectShapesFromRectangle(0,
                                                           0,
                                                           corel.ActivePage.SizeWidth,
                                                           corel.ActivePage.SizeHeight,
                                                           false);
            }

            // if no shapes found, select all
            if(corel.ActiveSelection.Shapes.Count == 0) {
                corel.ActivePage.SelectableShapes.All().AddToSelection();
            }

            switch(exportType) {
                case ConfigData.ExportType.PLT: {
                    if(corel.ActiveSelection.Shapes.Count == 0) {
                        MessageBox.Show("Could no get shapes for exporting. Manual export required.");
                    } else {
                        string exportPath = destPath + config.constructPartialPath(order) + "PLT\\";
                        System.IO.Directory.CreateDirectory(exportPath);
                        // options need to be specified within Corel previously
                        corel.ActiveDocument.Export(exportPath + orderWords + ".plt", cdrFilter.cdrPLT, cdrExportRange.cdrSelection);
                    }
                    } break;
                case ConfigData.ExportType.EPS: {
                    if(corel.ActiveSelection.Shapes.Count == 0) {
                        MessageBox.Show("Could no get shapes for exporting. Manual export required.");
                    } else {
                        string exportPath = destPath + config.constructPartialPath(order) + "EPS\\";
                        System.IO.Directory.CreateDirectory(exportPath);
                        // options need to be specified within Corel previously
                        corel.ActiveDocument.Export(exportPath + orderWords + ".eps", cdrFilter.cdrEPS, cdrExportRange.cdrSelection);
                    }
                    } break;
                default:
                    break;
            }
        }
    }
}