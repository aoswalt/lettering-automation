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
    }

    public class Lettering {
        public static string errors = "";
        public static CorelDRAW.Application corel = new CorelDRAW.Application();

        [STAThread]
        static void Main(string[] args) {
            // if setup had to be done, do not open launcher
            if(!SetupManager.CheckSetup()) {
                return;
            }

            LauncherWindow launcher = new LauncherWindow();
            launcher.ShowDialog();
        }

        public static void Run() {
            ConfigData config = ConfigManager.getConfig();

            List<OrderData> missingOrders = new List<OrderData>();

            DataTable data = DataReader.getCsvData();
            foreach(DataRow row in data.Rows) {
                OrderData order = new OrderData(row);
                string trimmedCode = config.trimStyleCode(order.itemCode);

                // if not in config, continue; else, store the trimmed code
                if(trimmedCode.Length == 0) {
                    missingOrders.Add(order);
                    continue;
                } else {
                    order.itemCode = trimmedCode;
                }

                // if built, continue
                string orderPath = config.constructPath(order);

                if(File.Exists(orderPath)) {
                    continue;
                }

                // build
                // MessageBox.Show("To build: " + order.itemCode + "\n Template: " + config.getTemplatePath(order));
                String templatePath = config.getTemplatePath(order);
                if(!File.Exists(templatePath)) {
                    MessageBox.Show("Template not found:\n" + templatePath, "Missing Template", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    missingOrders.Add(order);
                } else {
                    BuildOrder(order);
                }
            }

            DataWriter.writeLog(missingOrders);

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

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Automation {
    class CorelAccess {
        static void Main(string[] args) {

            //corel.Visible = true;
            //corel.InitializeVBA();
            //corel.CreateDocument();
            //corel.ActiveLayer.CreateRectangle(0, 0, 1, 1);
            //corel.ActiveDocument.SaveAs(@"L:\Corel\WORK FOLDERS\Adam\Templates\Macro Templates\Embedded\test.cdr");
            //corel.OpenDocument(@"L:\Corel\WORK FOLDERS\Adam\Templates\Macro Templates\Embedded\TestFile.cdr");

            object[] vars = new object[4];
            vars[0] = "DAR";
            vars[1] = "TIGERS";
            vars[2] = 2;
            vars[3] = 10.5;
            //corel.GMSManager.RunMacro("VBAProject", "ThisDocument.Start", vars);   // Start(style As String, word As String, size As Integer, spec As Double)

            //
            object[] i = new object[1];
            i[0] = 0;
            //corel.GMSManager.RunMacro("Test", "Functions.Alert", i);
            //corel.GMSManager.RunMacro("VBAProject", "ThisDocument.Alert", i);
            //


            /*
            try {
                //corel.GMSManager.RunMacro("Test", "Functions.Alert", i);
                corel.GMSManager.RunMacro("VBAProject", "Module.Alert", i);
                //
            } catch(Exception e) {
                Debug.WriteLine("ERROR: " + e.Message);
                Debug.WriteLine( e.StackTrace);
            }
             * *
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Intro {
    class FileFilter {
        private string pathToRes = "../../res/";        // backup from debug to res folder
        private string inFileName = "lterror.csv";
        private string outFileName = "filtered.csv";

        private void Filter() {
            // delete file if exists
            if(System.IO.File.Exists(pathToRes + outFileName)) {
                System.IO.File.Delete(pathToRes + outFileName);
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(pathToRes + inFileName);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(pathToRes + outFileName);

            // copy header
            String line = sr.ReadLine();
            sw.WriteLine(line);

            while((line = sr.ReadLine()) != null) {
                String[] tokens = line.Split(',');

                /*
                foreach(String s in tokens) {
                    Debug.Write(s + "\t");
                }
                Debug.Write("\n");
                *

                if(tokens[0] == "\"34\"") {     // csv contains quotes around info
                    sw.WriteLine(line);
                }
            }

            sw.Flush();
            sw.Close();
        }

        static void Main(String[] args) {
            FileFilter ff = new FileFilter();
            ff.Filter();
        }
    }
}
*/