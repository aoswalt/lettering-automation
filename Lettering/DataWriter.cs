using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lettering {
    class DataWriter {
        public static void writeLog(List<OrderData> orders, string fileName = "log") {
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
            ret += Headers.CUT_HOUSE + ",";
            ret += Headers.SCHEDULE_DATE + ",";
            ret += Headers.ENTER_DATE + ",";
            ret += Headers.ORDER_NUMBER + ",";
            ret += Headers.VOUCHER + ",";
            ret += Headers.ITEM + ",";
            ret += Headers.SIZE + ",";
            ret += Headers.SPEC + ",";
            ret += Headers.NAME + ",";
            ret += Headers.WORD1 + ",";
            ret += Headers.WORD2 + ",";
            ret += Headers.WORD3 + ",";
            ret += Headers.WORD4 + ",";
            ret += Headers.COLOR1 + ",";
            ret += Headers.COLOR2 + ",";
            ret += Headers.COLOR3 + ",";
            ret += Headers.COLOR4 + ",";
            ret += Headers.RUSH_DATE;

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
            ret += order.rushDate;

            return ret;
        }
    }
}
