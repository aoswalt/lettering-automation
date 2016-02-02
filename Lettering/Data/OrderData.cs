using System;
using System.Collections.Generic;
using System.Data;

namespace Lettering {
    internal class OrderData {
        internal string cutHouse;
        internal string scheduleDate;
        internal string enterDate;
        internal int orderNumber;
        internal int voucherNumber;
        internal string itemCode;
        internal double size;
        internal double spec;
        internal string name;
        internal string word1;
        internal string word2;
        internal string word3;
        internal string word4;
        internal string color1;
        internal string color2;
        internal string color3;
        internal string color4;
        internal string rushDate;
        internal string comment;
        internal List<string> nameList;

        internal OrderData(DataRow row) {
            this.cutHouse = (row[DbHeaders.CUT_HOUSE].ToString()).Trim();
            this.scheduleDate = row[DbHeaders.SCHEDULE_DATE] != System.DBNull.Value ? ((System.DateTime)row[DbHeaders.SCHEDULE_DATE]).ToString("d") : "";
            this.enterDate = row[DbHeaders.ENTER_DATE] != System.DBNull.Value ? ((System.DateTime)row[DbHeaders.ENTER_DATE]).ToString("d") : "";
            this.orderNumber = row[DbHeaders.ORDER_NUMBER] != System.DBNull.Value ? Convert.ToInt32(row[DbHeaders.ORDER_NUMBER]) : 0;
            this.voucherNumber = row[DbHeaders.VOUCHER] != System.DBNull.Value ? Convert.ToInt32(row[DbHeaders.VOUCHER]) : 0;
            this.itemCode = (row[DbHeaders.ITEM].ToString()).Trim();
            this.size = row[DbHeaders.SIZE] != System.DBNull.Value ? Convert.ToDouble(row[DbHeaders.SIZE]) : 0;
            this.spec = row[DbHeaders.SPEC] != System.DBNull.Value ? Convert.ToDouble(row[DbHeaders.SPEC]) : 0;
            this.name = (row[DbHeaders.NAME].ToString()).Trim();
            this.word1 = (row[DbHeaders.WORD1].ToString()).Trim();
            this.word2 = (row[DbHeaders.WORD2].ToString()).Trim();
            this.word3 = (row[DbHeaders.WORD3].ToString()).Trim();
            this.word4 = (row[DbHeaders.WORD4].ToString()).Trim();
            this.color1 = (row[DbHeaders.COLOR1].ToString()).Trim();
            this.color2 = (row[DbHeaders.COLOR2].ToString()).Trim();
            this.color3 = (row[DbHeaders.COLOR3].ToString()).Trim();
            this.color4 = (row[DbHeaders.COLOR4].ToString()).Trim();
            this.rushDate = row[DbHeaders.RUSH_DATE] != System.DBNull.Value ? ((System.DateTime)row[DbHeaders.RUSH_DATE]).ToString("d") : "";
            this.comment = "";
            this.nameList = new List<string>();
        }

        internal OrderData Clone() {
            return (OrderData)this.MemberwiseClone();
        }
    }
}
