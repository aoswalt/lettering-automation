using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Lettering.Data;
using Lettering.Errors;

namespace Lettering.IO {
    internal class CsvReader {
        internal static DataTable Read(string chosenCsvFilePath) {
            //NOTE(adam): operating on copy to prevent locking network file
            string fileName = Path.GetFileName(chosenCsvFilePath);
            Directory.CreateDirectory(FilePaths.tempFolderPath);
            File.Copy(chosenCsvFilePath, FilePaths.tempFolderPath + fileName, true);

            string query = @"SELECT * FROM [" + fileName + "]";

            using(OdbcConnection conn = new OdbcConnection("Driver={Microsoft Text Driver (*.txt; *.csv)};DBQ=" + FilePaths.tempFolderPath)) {
                OdbcCommand command = new OdbcCommand(query, conn);
                OdbcDataAdapter adapter = new OdbcDataAdapter(command);

                DataTable dataTable = new DataTable();
                DataColumn[] cols = {
                        new DataColumn("HOUSE", typeof(string)),
                        new DataColumn("ORDER_NO", typeof(int)),
                        new DataColumn("ORDER_VOUCH", typeof(int)),
                        new DataColumn("PARENT_VOUCH", typeof(int)),
                        new DataColumn("ITEM_NO", typeof(string)),
                        new DataColumn("LETTER_SIZE", typeof(double)),
                        new DataColumn("LETTER_SPEC", typeof(double)),
                        new DataColumn("DRAWING_LETTER_WORD1", typeof(string)),
                        new DataColumn("DRAWING_LETTER_WORD2", typeof(string)),
                        new DataColumn("DRAWING_LETTER_WORD3", typeof(string)),
                        new DataColumn("DRAWING_LETTER_WORD4", typeof(string)),
                        new DataColumn("SCHEDULE_DATE_CCYYMMDD", typeof(int)),
                        new DataColumn("SCHEDULE_DATE_MMDDCCYY", typeof(DateTime)),
                        new DataColumn("NAME", typeof(string))
                    };
                dataTable.Columns.AddRange(cols);

                try {
                    adapter.Fill(dataTable);
                } catch(InvalidCastException) {

                    DataTable dtClone = dataTable.Clone();
                    dtClone.Columns["HOUSE"].DataType = typeof(string);
                    dtClone.Columns["SCHEDULE_DATE_MMDDCCYY"].DataType = typeof(DateTime);
                    dtClone.Columns["ORDER_NO"].DataType = typeof(int);
                    dtClone.Columns["ORDER_VOUCH"].DataType = typeof(int);
                    dtClone.Columns["ITEM_NO"].DataType = typeof(string);
                    dtClone.Columns["LETTER_SIZE"].DataType = typeof(double);
                    dtClone.Columns["LETTER_SPEC"].DataType = typeof(double);
                    dtClone.Columns["DRAWING_LETTER_WORD1"].DataType = typeof(string);
                    dtClone.Columns["DRAWING_LETTER_WORD2"].DataType = typeof(string);
                    dtClone.Columns["DRAWING_LETTER_WORD3"].DataType = typeof(string);
                    dtClone.Columns["DRAWING_LETTER_WORD4"].DataType = typeof(string);
                    dtClone.Columns["NAME"].DataType = typeof(string);

                    foreach(DataRow row in dataTable.Rows) {
                        object[] vals = row.ItemArray;

                        vals[0] = ((string)vals[0]).Replace("\"", "");                      // HOUSE
                        vals[1] = int.Parse(((string)vals[1]).Replace("\"", ""));           // ORDER_NO
                        vals[2] = int.Parse(((string)vals[2]).Replace("\"", ""));           // ORDER_VOUCH
                        vals[3] = int.Parse(((string)vals[3]).Replace("\"", ""));           // PARENT_VOUCH
                        vals[4] = ((string)vals[4]).Replace("\"", "");                      // ITEM_NO
                        vals[5] = double.Parse(((string)vals[5]).Replace("\"", ""));        // LETTER_SIZE
                        vals[6] = double.Parse(((string)vals[6]).Replace("\"", ""));        // LETTER_SPEC
                        vals[7] = ((string)vals[7]).Replace("\"", "");                      // DRAWING_LETTER_WORD1
                        vals[8] = ((string)vals[8]).Replace("\"", "");                      // DRAWING_LETTER_WORD2
                        vals[9] = ((string)vals[9]).Replace("\"", "");                      // DRAWING_LETTER_WORD3
                        vals[10] = ((string)vals[10]).Replace("\"", "");                    // DRAWING_LETTER_WORD4
                        vals[11] = int.Parse(((string)vals[11]).Replace("\"", ""));         // SCHEDULE_DATE_CCYYMMDD
                        vals[12] = DateTime.Parse(((string)vals[12]).Replace("\"", ""));    // SCHEDULE_DATE_MMDDCCYY
                        vals[13] = ((string)vals[13]).Replace("\"", "");                    // NAME

                        dtClone.NewRow().ItemArray = vals;
                    }


                    if(!TryRenameHeaders(dataTable)) {
                        ErrorHandler.HandleError(ErrorType.Alert, "Problem with determining headers.");
                        return null;
                    }
                    UnifyHeaders(dtClone);
                    return dtClone;
                }

                File.Delete(FilePaths.tempFolderPath + fileName);


                if(!TryRenameHeaders(dataTable)) {
                    ErrorHandler.HandleError(ErrorType.Alert, "Problem with determining headers.");
                    return null;
                }
                UnifyHeaders(dataTable);
                return dataTable;
            }
        }
        
        private static void UnifyHeaders(DataTable table) {
            if(table.Columns.Contains("HOUSE") && !table.Columns.Contains(DbHeaders.CUT_HOUSE)) table.Columns["HOUSE"].ColumnName = DbHeaders.CUT_HOUSE;
            if(table.Columns.Contains("SCHEDULE_DATE_MMDDCCYY") && !table.Columns.Contains(DbHeaders.SCHEDULE_DATE)) table.Columns["SCHEDULE_DATE_MMDDCCYY"].ColumnName = DbHeaders.SCHEDULE_DATE;
            if(!table.Columns.Contains(DbHeaders.ENTER_DATE)) table.Columns.Add(DbHeaders.ENTER_DATE);
            if(table.Columns.Contains("ORDER_NO") && !table.Columns.Contains(DbHeaders.ORDER_NUMBER)) table.Columns["ORDER_NO"].ColumnName = DbHeaders.ORDER_NUMBER;
            if(table.Columns.Contains("ORDER_VOUCH") && !table.Columns.Contains(DbHeaders.VOUCHER)) table.Columns["ORDER_VOUCH"].ColumnName = DbHeaders.VOUCHER;
            if(table.Columns.Contains("ITEM_NO") && !table.Columns.Contains(DbHeaders.ITEM)) table.Columns["ITEM_NO"].ColumnName = DbHeaders.ITEM;
            if(table.Columns.Contains("LETTER_SIZE") && !table.Columns.Contains(DbHeaders.SIZE)) table.Columns["LETTER_SIZE"].ColumnName = DbHeaders.SIZE;
            if(table.Columns.Contains("LETTER_SPEC") && !table.Columns.Contains(DbHeaders.SPEC)) table.Columns["LETTER_SPEC"].ColumnName = DbHeaders.SPEC;
            if(table.Columns.Contains("NAME") && !table.Columns.Contains(DbHeaders.NAME)) table.Columns["NAME"].ColumnName = DbHeaders.NAME;
            if(table.Columns.Contains("DRAWING_LETTER_WORD1") && !table.Columns.Contains(DbHeaders.WORD1)) table.Columns["DRAWING_LETTER_WORD1"].ColumnName = DbHeaders.WORD1;
            if(table.Columns.Contains("DRAWING_LETTER_WORD2") && !table.Columns.Contains(DbHeaders.WORD2)) table.Columns["DRAWING_LETTER_WORD2"].ColumnName = DbHeaders.WORD2;
            if(table.Columns.Contains("DRAWING_LETTER_WORD3") && !table.Columns.Contains(DbHeaders.WORD3)) table.Columns["DRAWING_LETTER_WORD3"].ColumnName = DbHeaders.WORD3;
            if(table.Columns.Contains("DRAWING_LETTER_WORD4") && !table.Columns.Contains(DbHeaders.WORD4)) table.Columns["DRAWING_LETTER_WORD4"].ColumnName = DbHeaders.WORD4;
            if(!table.Columns.Contains(DbHeaders.COLOR1)) table.Columns.Add(DbHeaders.COLOR1);
            if(!table.Columns.Contains(DbHeaders.COLOR2)) table.Columns.Add(DbHeaders.COLOR2);
            if(!table.Columns.Contains(DbHeaders.COLOR3)) table.Columns.Add(DbHeaders.COLOR3);
            if(!table.Columns.Contains(DbHeaders.COLOR4)) table.Columns.Add(DbHeaders.COLOR4);
            if(!table.Columns.Contains(DbHeaders.RUSH_DATE)) table.Columns.Add(DbHeaders.RUSH_DATE);

            //data.Columns["PARENT_VOUCH"].ColumnName = "";
            //data.Columns["SCHEDULE_DATE_CCYYMMDD"].ColumnName = "";
        }

        private static bool TryRenameHeaders(DataTable dataTable) {
            TryHeaderRename(dataTable, DbHeaders.CUT_HOUSE, new string[] { "house", "hse" });
            TryHeaderRename(dataTable, DbHeaders.SCHEDULE_DATE, new string[] { "schedule", "sch" });
            TryHeaderRename(dataTable, DbHeaders.ENTER_DATE, new string[] { "enter", "order*date", "order*dt" });
            TryHeaderRename(dataTable, DbHeaders.ORDER_NUMBER, new string[] { "order" });
            TryHeaderRename(dataTable, DbHeaders.VOUCHER, new string[] { "voucher", "vch" });
            TryHeaderRename(dataTable, DbHeaders.ITEM, new string[] { "item", "style", "code" });
            TryHeaderRename(dataTable, DbHeaders.SIZE, new string[] { "size", "sze" });
            TryHeaderRename(dataTable, DbHeaders.SPEC, new string[] { "spec", "width" });
            TryHeaderRename(dataTable, DbHeaders.NAME, new string[] { "name" });
            TryHeaderRename(dataTable, DbHeaders.WORD1, new string[] { "word*1" });
            TryHeaderRename(dataTable, DbHeaders.WORD3, new string[] { "word*2" });
            TryHeaderRename(dataTable, DbHeaders.WORD4, new string[] { "word*3" });
            TryHeaderRename(dataTable, DbHeaders.WORD2, new string[] { "word*4" });
            TryHeaderRename(dataTable, DbHeaders.COLOR1, new string[] { "color*1" });
            TryHeaderRename(dataTable, DbHeaders.COLOR2, new string[] { "color*2" });
            TryHeaderRename(dataTable, DbHeaders.COLOR3, new string[] { "color*3" });
            TryHeaderRename(dataTable, DbHeaders.COLOR4, new string[] { "color*4" });
            TryHeaderRename(dataTable, DbHeaders.RUSH_DATE, new string[] { "rush", "rsh" });
            TryHeaderRename(dataTable, DbHeaders.COMMENTS, new string[] { "comment" });

            return true;
        }

        private static void TryHeaderRename(DataTable dataTable, string dbHeader, string[] tryMatches) {
            foreach(string lookupString in tryMatches) {
                //NOTE(adam): convert simple wildcards to regex wildcards and add surrounding matches
                string pattern = lookupString.ToUpper().Replace("?", ".").Replace("*", "(.+)?");
                pattern = $"(.+)?{pattern}(.+)?";

                foreach(DataColumn col in dataTable.Columns) {
                    string colName = col.ColumnName.ToUpper();
                    if(colName == dbHeader) continue;

                    if(Regex.IsMatch(colName, pattern) && !dataTable.Columns.Contains(dbHeader)) {
                        col.ColumnName = dbHeader;
                    }
                }
            }
        }
    }
}
