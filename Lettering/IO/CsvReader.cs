using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using Lettering.Data;

namespace Lettering.IO {
    internal class CsvReader {
        internal static DataTable Read(string chosenCsvFilePath) {
            //NOTE(adam): operating on copy to prevent locking network file
            string fileName = Path.GetFileName(chosenCsvFilePath);
            Directory.CreateDirectory(FilePaths.tempFolderPath);
            string tempCsvFilePath = FilePaths.tempFolderPath + fileName;
            File.Copy(chosenCsvFilePath, tempCsvFilePath, true);

            //NOTE(adam): modifies first line of csv to have proper headers
            string[] lines = File.ReadAllLines(tempCsvFilePath);
            lines[0] = RenameHeaders(lines[0]);
            File.WriteAllLines(tempCsvFilePath, lines);

            using(OdbcConnection conn = new OdbcConnection("Driver={Microsoft Text Driver (*.txt; *.csv)};DBQ=" + FilePaths.tempFolderPath)) {
                string query = @"SELECT * FROM [" + fileName + "]";
                OdbcCommand command = new OdbcCommand(query, conn);
                OdbcDataAdapter adapter = new OdbcDataAdapter(command);

                //NOTE(adam): pre-setting fields to enforce types
                DataTable dataTable = new DataTable();
                DataColumn[] cols = new DataColumn[FieldData.List.Count];
                for(int i = 0; i != cols.Length; ++i){
                    FieldData field = FieldData.List[i];
                    cols[i] = new DataColumn(field.DbName, field.FieldType);
                }
                dataTable.Columns.AddRange(cols);

                adapter.Fill(dataTable);
                File.Delete(tempCsvFilePath);

                return dataTable;
            }
        }

        private static string RenameHeaders(string headerLine) {
            string[] headers = headerLine.Split(',');
            for(int i = 0; i != headers.Length; ++i) {
                headers[i] = TryRenameHeader(headers[i]);
            }
            return String.Join(",", headers);
        }

        private static string TryRenameHeader(string header) {
            foreach(FieldData fieldData in FieldData.List) {
                if(fieldData.IsMatch(header)) {
                    return fieldData.DbName;
                }
            }
            return header;
        }
    }
}
