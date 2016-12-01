using System.Collections.Generic;
using System.IO;
using Lettering.Data;

namespace Lettering {
    internal class CsvWriter {
        internal static void WriteReport(List<OrderData> orders, string fileName) {
            string reportFile = FilePaths.desktopFolderPath + fileName + ".csv";
            
            if(File.Exists(reportFile)) {
                if(!Messenger.Show($"Report with name {fileName} already exists. Overwrite?", "Overwrite", MessageButtons.YesNo)) {
                    return;
                } else {
                    File.Delete(reportFile);
                }
            }

            using(StreamWriter writer = new StreamWriter(reportFile)) {
                writer.WriteLine(BuildHeaderString());

                foreach(OrderData order in orders) {
                    writer.WriteLine(BuildRowString(order));
                }

                writer.Flush();
                writer.Close();
            }
        }

        private static string BuildHeaderString() {
            string ret = "";
            ret += FieldData.CUT_HOUSE.DisplayName + ",";
            ret += FieldData.SCHEDULE_DATE.DisplayName + ",";
            ret += FieldData.ENTER_DATE.DisplayName + ",";
            ret += FieldData.ORDER_NUMBER.DisplayName + ",";
            ret += FieldData.VOUCHER.DisplayName + ",";
            ret += FieldData.ITEM.DisplayName + ",";
            ret += FieldData.SIZE.DisplayName + ",";
            ret += FieldData.SPEC.DisplayName + ",";
            ret += FieldData.NAME.DisplayName + ",";
            ret += FieldData.WORD1.DisplayName + ",";
            ret += FieldData.WORD2.DisplayName + ",";
            ret += FieldData.WORD3.DisplayName + ",";
            ret += FieldData.WORD4.DisplayName + ",";
            ret += FieldData.COLOR1.DisplayName + ",";
            ret += FieldData.COLOR2.DisplayName + ",";
            ret += FieldData.COLOR3.DisplayName + ",";
            ret += FieldData.COLOR4.DisplayName + ",";
            ret += FieldData.RUSH_DATE.DisplayName + ",";
            ret += FieldData.COMMENTS.DisplayName + ",";
            ret += FieldData.PATH.DisplayName;

            return ret;
        }

        private static string BuildRowString(OrderData order) {
            string ret = "";
            ret += order.cutHouse + ",";
            ret += order.scheduleDate + ",";
            ret += order.enterDate + ",";
            ret += order.orderNumber + ",";
            ret += order.voucherNumber + ",";
            ret += order.originalItemCode + ",";
            ret += order.size + ",";
            ret += order.spec + ",";
            ret += order.name + ",";
            ret += order.word1 + ",";
            ret += order.word2 + ",";
            ret += order.word3 + ",";
            ret += order.word4 + ",";
            ret += order.color1 + ",";
            ret += order.color2 + ",";
            ret += order.color3 + ",";
            ret += order.color4 + ",";
            ret += order.rushDate + ",";
            ret += order.comment + ",";
            ret += order.path;

            return ret;
        }
    }
}
