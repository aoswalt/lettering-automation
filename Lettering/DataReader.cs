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
                        adapter.Fill(dataTable);

                        unifyHeaders(dataTable);

                        return dataTable;
                    }
                } else {
                    MessageBox.Show("No file chosen.");

                    return null;
                }
            } catch(Exception ex) {
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
