using System;
using System.Collections.Generic;
using System.Data;
using Lettering.Data;

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
            this.cutHouse = (row[FieldData.CUT_HOUSE.DbName].ToString()).Trim();
            this.scheduleDate = row[FieldData.SCHEDULE_DATE.DbName] != System.DBNull.Value ? ((System.DateTime)row[FieldData.SCHEDULE_DATE.DbName]).ToString("d") : "";
            this.enterDate = row[FieldData.ENTER_DATE.DbName] != System.DBNull.Value ? ((System.DateTime)row[FieldData.ENTER_DATE.DbName]).ToString("d") : "";
            this.orderNumber = row[FieldData.ORDER_NUMBER.DbName] != System.DBNull.Value ? Convert.ToInt32(row[FieldData.ORDER_NUMBER.DbName]) : 0;
            this.voucherNumber = row[FieldData.VOUCHER.DbName] != System.DBNull.Value ? Convert.ToInt32(row[FieldData.VOUCHER.DbName]) : 0;
            this.itemCode = (row[FieldData.ITEM.DbName].ToString()).Trim();
            this.size = row[FieldData.SIZE.DbName] != System.DBNull.Value ? Convert.ToDouble(row[FieldData.SIZE.DbName]) : 0;
            this.spec = row[FieldData.SPEC.DbName] != System.DBNull.Value ? Convert.ToDouble(row[FieldData.SPEC.DbName]) : 0;
            this.name = (row[FieldData.NAME.DbName].ToString()).Trim();
            this.word1 = (row[FieldData.WORD1.DbName].ToString()).Trim();
            this.word2 = (row[FieldData.WORD2.DbName].ToString()).Trim();
            this.word3 = (row[FieldData.WORD3.DbName].ToString()).Trim();
            this.word4 = (row[FieldData.WORD4.DbName].ToString()).Trim();
            this.color1 = (row[FieldData.COLOR1.DbName].ToString()).Trim();
            this.color2 = (row[FieldData.COLOR2.DbName].ToString()).Trim();
            this.color3 = (row[FieldData.COLOR3.DbName].ToString()).Trim();
            this.color4 = (row[FieldData.COLOR4.DbName].ToString()).Trim();
            this.rushDate = row[FieldData.RUSH_DATE.DbName] != System.DBNull.Value ? ((System.DateTime)row[FieldData.RUSH_DATE.DbName]).ToString("d") : "";
            this.comment = "";
            this.nameList = new List<string>();
        }

        internal OrderData Clone() {
            return (OrderData)this.MemberwiseClone();
        }
    }
}
