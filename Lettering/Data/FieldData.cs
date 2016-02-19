using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Lettering.Data {
    class FieldData {
        public static ReadOnlyCollection<FieldData> List { get { return privateList.AsReadOnly(); } }
        private static List<FieldData> privateList = new List<FieldData>();

        public static readonly FieldData CUT_HOUSE =     new FieldData("DHOUS",    "Cut House",     typeof(string),   new string[] { "house", "hse" });
        public static readonly FieldData SCHEDULE_DATE = new FieldData("SCDAT",    "Schedule Date", typeof(DateTime), new string[] { "schedule", "sch*MMDDCCYY", "sch" });
        public static readonly FieldData ENTER_DATE =    new FieldData("ENDAT",    "Enter Date",    typeof(DateTime), new string[] { "enter", "order*date", "order*dt" });
        public static readonly FieldData ORDER_NUMBER =  new FieldData("ORDNR",    "Order Number",  typeof(int),      new string[] { "order" });
        public static readonly FieldData VOUCHER =       new FieldData("ORVCH",    "Voucher",       typeof(int),      new string[] { "voucher", "vch" });
        public static readonly FieldData ITEM =          new FieldData("DITEM",    "Item",          typeof(string),   new string[] { "item", "style", "code" });
        public static readonly FieldData SIZE =          new FieldData("DLSIZ",    "Size",          typeof(double),   new string[] { "size", "sze" });
        public static readonly FieldData SPEC =          new FieldData("LETWID",   "Spec",          typeof(double),   new string[] { "spec", "width" });
        public static readonly FieldData NAME =          new FieldData("LETNAME",  "Name",          typeof(string),   new string[] { "name" });
        public static readonly FieldData WORD2 =         new FieldData("DLWR1",    "Word 1",        typeof(string),   new string[] { "word*1" });
        public static readonly FieldData WORD3 =         new FieldData("DLWR2",    "Word 2",        typeof(string),   new string[] { "word*2" });
        public static readonly FieldData WORD4 =         new FieldData("DLWR3",    "Word 3",        typeof(string),   new string[] { "word*3" });
        public static readonly FieldData WORD1 =         new FieldData("DLWR4",    "Word 4",        typeof(string),   new string[] { "word*4" });
        public static readonly FieldData COLOR1 =        new FieldData("DCLR1",    "Color 1",       typeof(string),   new string[] { "color*1" });
        public static readonly FieldData COLOR2 =        new FieldData("DCLR2",    "Color 2",       typeof(string),   new string[] { "color*2" });
        public static readonly FieldData COLOR3 =        new FieldData("DCLR3",    "Color 3",       typeof(string),   new string[] { "color*3" });
        public static readonly FieldData COLOR4 =        new FieldData("DCLR4",    "Color 4",       typeof(string),   new string[] { "color*4" });
        public static readonly FieldData RUSH_DATE =     new FieldData("RUDAT",    "Rush Date",     typeof(DateTime), new string[] { "rush", "rsh" });
        public static readonly FieldData COMMENTS =      new FieldData("COMMENTS", "Comment",       typeof(DateTime), new string[] { "comment" });

        internal readonly string DbName;
        internal readonly string DisplayName;
        internal readonly Type FieldType;

        private string[] tryMatches;

        internal FieldData(string DbName, string DisplayName, Type FieldType, string[] rawMatches) {
            this.DbName = DbName.ToUpper();
            this.DisplayName = DisplayName;
            this.FieldType = FieldType;

            tryMatches = new string[rawMatches.Length];

            for(int i = 0; i != rawMatches.Length; ++i) {
                //NOTE(adam): convert simple wildcards to regex wildcards and add surrounding matches
                string pattern = rawMatches[i].ToUpper().Replace("?", ".").Replace("*", "(.+)?");
                pattern = $"(.+)?{pattern}(.+)?";
                tryMatches[i] = pattern;
            }

            FieldData.privateList.Add(this);
        }

        internal bool IsMatch(string input) {
            foreach(string lookupString in tryMatches) {
                if(input.ToUpper() == DbName || Regex.IsMatch(input.ToUpper(), lookupString)) {
                    return true;
                }
            }
            return false;
        }
    }
}
