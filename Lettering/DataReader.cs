using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using System.IO;

namespace Lettering {
    class DataReader {
        public static DataTable getCsvData() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "csv file (*.csv)|*.csv|txt file (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            try {
                if(openFileDialog.ShowDialog() == DialogResult.OK) {
                    string pathOnly = Path.GetDirectoryName(openFileDialog.FileName);
                    string fileName = Path.GetFileName(openFileDialog.FileName);

                    string query = @"SELECT * FROM [" + fileName + "]";

                    using(OdbcConnection conn = new OdbcConnection("Driver={Microsoft Text Driver (*.txt; *.csv)};DBQ=" + pathOnly)) {
                        OdbcCommand command = new OdbcCommand(query, conn);
                        OdbcDataAdapter adapter = new OdbcDataAdapter(command);

                        DataTable dataTable = new DataTable();

                        DataColumn[] cols = {
                                                new DataColumn("HOUSE", typeof(String)), 
                                                new DataColumn("ORDER_NO", typeof(int)), 
                                                new DataColumn("ORDER_VOUCH", typeof(int)), 
                                                new DataColumn("PARENT_VOUCH", typeof(int)), 
                                                new DataColumn("ITEM_NO", typeof(String)), 
                                                new DataColumn("LETTER_SIZE", typeof(double)), 
                                                new DataColumn("LETTER_SPEC", typeof(double)), 
                                                new DataColumn("DRAWING_LETTER_WORD1", typeof(String)), 
                                                new DataColumn("DRAWING_LETTER_WORD2", typeof(String)), 
                                                new DataColumn("DRAWING_LETTER_WORD3", typeof(String)), 
                                                new DataColumn("DRAWING_LETTER_WORD4", typeof(String)), 
                                                new DataColumn("SCHEDULE_DATE_CCYYMMDD", typeof(int)),
                                                new DataColumn("SCHEDULE_DATE_MMDDCCYY", typeof(DateTime))
                                            };
                        dataTable.Columns.AddRange(cols);

                        try {
                            adapter.Fill(dataTable);
                        } catch(InvalidCastException) {
                            DataTable dtClone = dataTable.Clone();
                            dtClone.Columns["HOUSE"].DataType = typeof(String);
                            dtClone.Columns["SCHEDULE_DATE_MMDDCCYY"].DataType = typeof(DateTime);
                            dtClone.Columns["ORDER_NO"].DataType = typeof(Int32);
                            dtClone.Columns["ORDER_VOUCH"].DataType = typeof(Int32);
                            dtClone.Columns["ITEM_NO"].DataType = typeof(String);
                            dtClone.Columns["LETTER_SIZE"].DataType = typeof(Double);
                            dtClone.Columns["LETTER_SPEC"].DataType = typeof(Double);
                            dtClone.Columns["DRAWING_LETTER_WORD1"].DataType = typeof(String);
                            dtClone.Columns["DRAWING_LETTER_WORD2"].DataType = typeof(String);
                            dtClone.Columns["DRAWING_LETTER_WORD3"].DataType = typeof(String);
                            dtClone.Columns["DRAWING_LETTER_WORD4"].DataType = typeof(String);

                            foreach(DataRow row in dataTable.Rows) {
                                object[] vals = row.ItemArray;

                                vals[0] = ((String)vals[0]).Replace("\"", "");                      // HOUSE
                                vals[1] = int.Parse(((String)vals[1]).Replace("\"", ""));           // ORDER_NO
                                vals[2] = int.Parse(((String)vals[2]).Replace("\"", ""));           // ORDER_VOUCH
                                vals[3] = int.Parse(((String)vals[3]).Replace("\"", ""));           // PARENT_VOUCH
                                vals[4] = ((String)vals[4]).Replace("\"", "");                      // ITEM_NO
                                vals[5] = double.Parse(((String)vals[5]).Replace("\"", ""));        // LETTER_SIZE
                                vals[6] = double.Parse(((String)vals[6]).Replace("\"", ""));        // LETTER_SPEC
                                vals[7] = ((String)vals[7]).Replace("\"", "");                      // DRAWING_LETTER_WORD1
                                vals[8] = ((String)vals[8]).Replace("\"", "");                      // DRAWING_LETTER_WORD2
                                vals[9] = ((String)vals[9]).Replace("\"", "");                      // DRAWING_LETTER_WORD3
                                vals[10] = ((String)vals[10]).Replace("\"", "");                    // DRAWING_LETTER_WORD4
                                vals[11] = int.Parse(((String)vals[11]).Replace("\"", ""));         // SCHEDULE_DATE_CCYYMMDD
                                vals[12] = DateTime.Parse(((String)vals[12]).Replace("\"", ""));    // SCHEDULE_DATE_MMDDCCYY

                                dtClone.NewRow().ItemArray = vals;
                            }

                            unifyHeaders(dtClone);
                            return dtClone;
                        }

                        unifyHeaders(dataTable);
                        return dataTable;
                    }
                } else {
                    //MessageBox.Show("No file chosen.");

                    return new DataTable();
                }
            } catch(Exception ex) {     // should use specific exceptions
                MessageBox.Show("Error: Could not read file.\n\n" + ex.Message);

                return null;
            }
        }

        private static void unifyHeaders(DataTable data) {
            data.Columns["HOUSE"].ColumnName = Headers.CUT_HOUSE;
            data.Columns["SCHEDULE_DATE_MMDDCCYY"].ColumnName = Headers.SCHEDULE_DATE;
            data.Columns.Add(Headers.ENTER_DATE);
            data.Columns["ORDER_NO"].ColumnName = Headers.ORDER_NUMBER;
            data.Columns["ORDER_VOUCH"].ColumnName = Headers.VOUCHER;
            data.Columns["ITEM_NO"].ColumnName = Headers.ITEM;
            data.Columns["LETTER_SIZE"].ColumnName = Headers.SIZE;
            data.Columns["LETTER_SPEC"].ColumnName = Headers.SPEC;
            data.Columns.Add(Headers.NAME);
            data.Columns["DRAWING_LETTER_WORD1"].ColumnName = Headers.WORD1;
            data.Columns["DRAWING_LETTER_WORD2"].ColumnName = Headers.WORD2;
            data.Columns["DRAWING_LETTER_WORD3"].ColumnName = Headers.WORD3;
            data.Columns["DRAWING_LETTER_WORD4"].ColumnName = Headers.WORD4;
            data.Columns.Add(Headers.COLOR1);
            data.Columns.Add(Headers.COLOR2);
            data.Columns.Add(Headers.COLOR3);
            data.Columns.Add(Headers.COLOR4);
            data.Columns.Add(Headers.RUSH_DATE);

            //data.Columns["PARENT_VOUCH"].ColumnName = "";
            //data.Columns["SCHEDULE_DATE_CCYYMMDD"].ColumnName = "";
        }
    }
}
