using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
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
            ret += DbHeaders.CUT_HOUSE + ",";
            ret += DbHeaders.SCHEDULE_DATE + ",";
            ret += DbHeaders.ENTER_DATE + ",";
            ret += DbHeaders.ORDER_NUMBER + ",";
            ret += DbHeaders.VOUCHER + ",";
            ret += DbHeaders.ITEM + ",";
            ret += DbHeaders.SIZE + ",";
            ret += DbHeaders.SPEC + ",";
            ret += DbHeaders.NAME + ",";
            ret += DbHeaders.WORD1 + ",";
            ret += DbHeaders.WORD2 + ",";
            ret += DbHeaders.WORD3 + ",";
            ret += DbHeaders.WORD4 + ",";
            ret += DbHeaders.COLOR1 + ",";
            ret += DbHeaders.COLOR2 + ",";
            ret += DbHeaders.COLOR3 + ",";
            ret += DbHeaders.COLOR4 + ",";
            ret += DbHeaders.RUSH_DATE + ",";
            ret += DbHeaders.COMMENTS;

            return ret;
        }

        private static string BuildRowString(OrderData order) {
            string ret = "";
            ret += order.cutHouse + ",";
            ret += order.scheduleDate + ",";
            ret += order.enterDate + ",";
            ret += order.orderNumber + ",";
            ret += order.voucherNumber + ",";
            ret += order.itemCode + ",";
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
            ret += order.comment;

            return ret;
        }
    }
}
