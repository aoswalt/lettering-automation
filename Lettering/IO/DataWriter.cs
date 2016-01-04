using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lettering {
    class DataWriter {
        internal static void writeLog(List<OrderData> orders, string fileName) {
            string outFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '/' + fileName + ".csv";

            // delete file if exists
            // SHOULD PROMPT
            if(System.IO.File.Exists(outFilePath)) {
                System.IO.File.Delete(outFilePath);
            }

            using(System.IO.StreamWriter writer = new System.IO.StreamWriter(outFilePath)) {
                writer.WriteLine(getHeaderString());

                foreach(OrderData order in orders) {
                    writer.WriteLine(assembleLine(order));
                }

                writer.Flush();
                writer.Close();
            }
        }

        private static string getHeaderString() {
            string ret = "";
            ret += QueryHeaders.CUT_HOUSE + ",";
            ret += QueryHeaders.SCHEDULE_DATE + ",";
            ret += QueryHeaders.ENTER_DATE + ",";
            ret += QueryHeaders.ORDER_NUMBER + ",";
            ret += QueryHeaders.VOUCHER + ",";
            ret += QueryHeaders.ITEM + ",";
            ret += QueryHeaders.SIZE + ",";
            ret += QueryHeaders.SPEC + ",";
            ret += QueryHeaders.NAME + ",";
            ret += QueryHeaders.WORD1 + ",";
            ret += QueryHeaders.WORD2 + ",";
            ret += QueryHeaders.WORD3 + ",";
            ret += QueryHeaders.WORD4 + ",";
            ret += QueryHeaders.COLOR1 + ",";
            ret += QueryHeaders.COLOR2 + ",";
            ret += QueryHeaders.COLOR3 + ",";
            ret += QueryHeaders.COLOR4 + ",";
            ret += QueryHeaders.RUSH_DATE + ",";
            ret += QueryHeaders.COMMENTS;

            return ret;
        }

        private static string assembleLine(OrderData order) {
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
