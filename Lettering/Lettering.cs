using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using VGCore;
using System.IO;

namespace Lettering {
    public struct OrderData {
        public OrderData(DataRow row) {
            cutHouse = row[Headers.CUT_HOUSE] != System.DBNull.Value ? (string)row[Headers.CUT_HOUSE] : "";
            scheduleDate = row[Headers.SCHEDULE_DATE] != System.DBNull.Value ? ((System.DateTime)row[Headers.SCHEDULE_DATE]).ToString("d") : "";
            enterDate = row[Headers.ENTER_DATE] != System.DBNull.Value ? ((System.DateTime)row[Headers.ENTER_DATE]).ToString("d") : "";
            orderNumber = row[Headers.ORDER_NUMBER] != System.DBNull.Value ? (int)row[Headers.ORDER_NUMBER] : 0;
            voucherNumber = row[Headers.VOUCHER] != System.DBNull.Value ? (int)row[Headers.VOUCHER] : 0;
            itemCode = row[Headers.ITEM] != System.DBNull.Value ? (string)row[Headers.ITEM] : "";
            size = row[Headers.SIZE] != System.DBNull.Value ? Convert.ToDouble(row[Headers.SIZE]) : 0;
            spec = row[Headers.SPEC] != System.DBNull.Value ? (double)row[Headers.SPEC] : 0;
            name = row[Headers.NAME] != System.DBNull.Value ? (string)row[Headers.NAME] : "";
            word1 = row[Headers.WORD1] != System.DBNull.Value ? (string)row[Headers.WORD1] : "";
            word2 = row[Headers.WORD2] != System.DBNull.Value ? (string)row[Headers.WORD2] : "";
            word3 = row[Headers.WORD3] != System.DBNull.Value ? (string)row[Headers.WORD3] : "";
            word4 = row[Headers.WORD4] != System.DBNull.Value ? (string)row[Headers.WORD4] : "";
            color1 = row[Headers.COLOR1] != System.DBNull.Value ? (string)row[Headers.COLOR1] : "";
            color2 = row[Headers.COLOR2] != System.DBNull.Value ? (string)row[Headers.COLOR2] : "";
            color3 = row[Headers.COLOR3] != System.DBNull.Value ? (string)row[Headers.COLOR3] : "";
            color4 = row[Headers.COLOR4] != System.DBNull.Value ? (string)row[Headers.COLOR4] : "";
            rushDate = row[Headers.RUSH_DATE] != System.DBNull.Value ? ((System.DateTime)row[Headers.RUSH_DATE]).ToString("d") : "";
            comment = "";
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
    }

    public class Lettering {
        public static string errors = "";
        public static CorelDRAW.Application corel = new CorelDRAW.Application();
        private static LauncherWindow launcher = new LauncherWindow();

        [STAThread]
        static void Main(string[] args) {
            // check setup will close corel as necessary
            SetupManager.CheckSetup();

            launcher.ShowDialog();
        }

        public static void Run() {
            bool cancelBuilding = false;

            ConfigData config = ConfigManager.getConfig();
            ActiveOrderWindow activeOrderWindow = new ActiveOrderWindow();

            List<OrderData> ordersToLog = new List<OrderData>();

            DataTable data = DataReader.getCsvData();
            if(data == null) {
                return;
            } else {
                launcher.Close();
            }

            foreach(DataRow row in data.Rows) {
                OrderData order = new OrderData(row);
                string trimmedCode = config.trimStyleCode(order.itemCode);

                // if not in config, continue; else, store the trimmed code
                if(trimmedCode.Length == 0) {
                    order.comment += "Not in config.";
                    ordersToLog.Add(order);
                    continue;
                } else {
                    order.itemCode = trimmedCode;
                }

                // if built, continue
                string orderPath = config.constructPath(order);

                if(File.Exists(orderPath)) {
                    continue;
                }

                if(cancelBuilding) {
                    order.comment += "Cancelled building.";
                    ordersToLog.Add(order);
                    continue;
                }

                // build
                // MessageBox.Show("To build: " + order.itemCode + "\n Template: " + config.getTemplatePath(order));
                String templatePath = config.getTemplatePath(order);
                if(!File.Exists(templatePath)) {
                    MessageBox.Show("Template not found:\n" + templatePath, "Missing Template", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    order.comment += "Template not found.";
                    ordersToLog.Add(order);
                } else {
                    //BuildOrder(order);
                    activeOrderWindow.ShowDialog();

                    if(activeOrderWindow.selection == WindowSelection.NEXT) {
                        order.comment += "Completed.";
                        ordersToLog.Add(order);
                    } else if(activeOrderWindow.selection == WindowSelection.REJECT) {
                        order.comment += "Manually rejected.";
                        ordersToLog.Add(order);
                    } else if(activeOrderWindow.selection == WindowSelection.CANCEL) {
                        cancelBuilding = true;
                        order.comment += "Cancelled building.";
                        ordersToLog.Add(order);
                    }
                }
            }

            DataWriter.writeLog(ordersToLog, "LetteringLog-" + DateTime.Now.ToString("yyyymmdd_HHmm"));

            if(errors.Length > 0) MessageBox.Show(errors, "Error Log");
        }

        private static void BuildOrder(OrderData order) {
            Shape orderShape = corel.ActiveLayer.CreateRectangle2(0, 0, 0.1, 0.1);
            orderShape.Name = "OrderData";
            orderShape.Properties["order", 1] = order.size;
            orderShape.Properties["order", 2] = order.spec;
            orderShape.Properties["order", 3] = order.word1;
            orderShape.Properties["order", 4] = order.word2;
            orderShape.Properties["order", 5] = order.word3;
            orderShape.Properties["order", 6] = order.word4;
            orderShape.Properties["order", 7] = new string[] { "" };    //TODO: flesh out handling names

            corel.ActivePage.CreateLayer("Automate");
        }
    }
}