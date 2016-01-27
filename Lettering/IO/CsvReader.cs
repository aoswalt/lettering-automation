using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;
using Lettering.Data;

namespace Lettering.IO {
    internal class CsvReader {
        //TODO(adam): support varying csv report layouts

        internal static DataTable GetCsvData() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "csv file (*.csv)|*.csv|txt file (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            //try {
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                //string pathOnly = Path.GetDirectoryName(openFileDialog.FileName);
                string fileName = Path.GetFileName(openFileDialog.FileName);

                Directory.CreateDirectory(FilePaths.tempFolderPath);
                File.Copy(openFileDialog.FileName, FilePaths.tempFolderPath + fileName, true);

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

                        ReportReader.UnifyHeaders(dtClone);
                        return dtClone;
                    }

                    Directory.Delete(FilePaths.tempFolderPath, true);

                    ReportReader.UnifyHeaders(dataTable);
                    return dataTable;
                }
            } else {
                //MessageBox.Show("No file chosen.");

                return null;
            }
            /*
            } catch(Exception ex) {     // should use specific exceptions
                MessageBox.Show("Error: Could not read file.\n\n" + ex.Message);

                return null;
            }
             * */
        }
    }
}
