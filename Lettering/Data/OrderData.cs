using System;
using System.Collections.Generic;
using System.Data;
using Lettering.Data;

namespace Lettering {
    public class OrderData {
        public string cutHouse;
        public string scheduleDate;
        public string enterDate;
        public int orderNumber;
        public int voucherNumber;
        public string originalItemCode;
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
        public string path;

        public OrderData(DataRow row) {
            this.cutHouse =         (row[FieldData.CUT_HOUSE.DbName].ToString()).Trim();
            this.scheduleDate =      row[FieldData.SCHEDULE_DATE.DbName] != DBNull.Value ? ((DateTime)row[FieldData.SCHEDULE_DATE.DbName]).ToString("d") : "";
            this.enterDate =         row[FieldData.ENTER_DATE.DbName] != DBNull.Value ? ((DateTime)row[FieldData.ENTER_DATE.DbName]).ToString("d") : "";
            this.orderNumber =       row[FieldData.ORDER_NUMBER.DbName] != DBNull.Value ? Convert.ToInt32(row[FieldData.ORDER_NUMBER.DbName]) : 0;
            this.voucherNumber =     row[FieldData.VOUCHER.DbName] != DBNull.Value ? Convert.ToInt32(row[FieldData.VOUCHER.DbName]) : 0;
            this.originalItemCode = (row[FieldData.ITEM.DbName].ToString()).Trim();
            this.itemCode =         (row[FieldData.ITEM.DbName].ToString()).Trim();
            this.size =              row[FieldData.SIZE.DbName] != DBNull.Value ? Convert.ToDouble(row[FieldData.SIZE.DbName]) : 0;
            this.spec =              row[FieldData.SPEC.DbName] != DBNull.Value ? Convert.ToDouble(row[FieldData.SPEC.DbName]) : 0;
            this.name =             (row[FieldData.NAME.DbName].ToString()).Trim();
            this.word1 =            (row[FieldData.WORD1.DbName].ToString()).Trim();
            this.word2 =            (row[FieldData.WORD2.DbName].ToString()).Trim();
            this.word3 =            (row[FieldData.WORD3.DbName].ToString()).Trim();
            this.word4 =            (row[FieldData.WORD4.DbName].ToString()).Trim();
            this.color1 =           (row[FieldData.COLOR1.DbName].ToString()).Trim();
            this.color2 =           (row[FieldData.COLOR2.DbName].ToString()).Trim();
            this.color3 =           (row[FieldData.COLOR3.DbName].ToString()).Trim();
            this.color4 =           (row[FieldData.COLOR4.DbName].ToString()).Trim();
            this.rushDate =          row[FieldData.RUSH_DATE.DbName] != DBNull.Value ? ((DateTime)row[FieldData.RUSH_DATE.DbName]).ToString("d") : "";
            this.comment = "";
            this.nameList = new List<string>();
            this.path = "";
        }

        public OrderData Clone() {
            return (OrderData)this.MemberwiseClone();
        }
    }
}
