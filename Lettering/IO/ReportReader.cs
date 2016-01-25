using System;
using System.Data;
using System.Collections.Generic;
using Lettering.IO;
using Lettering.Errors;

namespace Lettering {
    internal class ReportReader {
        internal static DataTable RunReport(DateTime? startDate, DateTime? endDate, ReportType reportType) {
            string dateClause;
            if(startDate.HasValue && endDate.HasValue) {
                //NOTE(adam): start and end could be reversed or same
                DateTime start;
                DateTime end;
                if(startDate.Value <= endDate.Value) {
                    start = startDate.Value;
                    end = endDate.Value;
                } else {
                    start = endDate.Value;
                    end = startDate.Value;
                }

                dateClause = string.Format("((d.dorcy = {0}) AND (d.doryr = {1}) AND (d.dormo = {2}) AND (d.dorda = {3}))",
                                           start.Year / 100, start.Year % 100, start.Month, start.Day);
                start = start.AddDays(1);
                while(start <= end) {
                    dateClause += string.Format(" OR ((d.dorcy = {0}) AND (d.doryr = {1}) AND (d.dormo = {2}) AND (d.dorda = {3}))",
                                               start.Year / 100, start.Year % 100, start.Month, start.Day);
                    start = start.AddDays(1);
                }
            } else if(startDate.HasValue || endDate.HasValue) {
                DateTime date = (startDate.HasValue ? startDate.Value : endDate.Value);
                dateClause = string.Format("((d.dorcy = {0}) AND (d.doryr = {1}) AND (d.dormo = {2}) AND (d.dorda = {3}))",
                                           date.Year / 100, date.Year % 100, date.Month, date.Day);
            } else { 
                //NOTE(adam): default is yesterday
                List<DateTime> holidays = ConfigReader.ReadHolidays();   //TODO(adam): move reading of holidays to config
                DateTime date = DateTime.Today.AddDays(-1);

                dateClause = string.Format("((d.dorcy = {0}) AND (d.doryr = {1}) AND (d.dormo = {2}) AND (d.dorda = {3}))",
                                           date.Year / 100, date.Year % 100, date.Month, date.Day);

                //NOTE(adam): adding days to search for to cover non work days (weekends, holidays, etc)
                while(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(date)) {
                    date = date.AddDays(-1);
                    dateClause += string.Format(" OR ((d.dorcy = {0}) AND (d.doryr = {1}) AND (d.dormo = {2}) AND (d.dorda = {3}))",
                                           date.Year / 100, date.Year % 100, date.Month, date.Day);
                }
            }

            string styleClause = "";
            switch(reportType) {
                case ReportType.Cut:
                    styleClause = @"
                        ((d.dclas IN ('041', '049', '04C', '04D', '04Y', 'F09', 'PS3', 'L02', 'L05', 'L10', 'S03', 'SKL', 'VTT', '04G')) OR
                            (d.ditem LIKE 'SIGN%')) AND 
                        (d.ditem NOT LIKE 'OZ%') AND (d.ditem NOT LIKE 'COZ%') AND 
                        (d.ditem NOT LIKE 'SP%') AND 
                        (d.ditem NOT LIKE 'IDC%')";
                    break;
                case ReportType.Sew:
                    break;
                case ReportType.Stone:
                    break;
                default:
                    ErrorHandler.HandleError(ErrorType.Critical, "Invalid report type in RunReport.");
                    break;
            }

            DataTable reportTable = SqlConnector.RunQuery(dateClause, styleClause);
            UnifyHeaders(reportTable);

            return reportTable;
        }

        //TODO(adam): move UnivyHeaders to a more general location
        internal static void UnifyHeaders(DataTable table) {
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
    }
}
