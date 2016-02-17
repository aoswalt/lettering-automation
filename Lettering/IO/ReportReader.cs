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
                List<DateTime> holidays = ConfigReader.ReadHolidays();
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
                    styleClause += @"
                        ((d.ditem LIKE '%MN%') OR (d.ditem LIKE 'PF%') OR (d.dlrea LIKE 'ASW') OR (d.ditem LIKE 'PK%')) AND 
                        ((d.ditem NOT LIKE '%CBSLIMN%') AND (d.ditem NOT LIKE '%SLIMN%')) AND 
                        (d.dclas NOT IN ('010', '045', '04A', '04B', '04M', '04O', '065', '075', '083', '086', '087', '089', 
                                         '0DB', '0P1', '0P2', '112', 'CS2', 'S01', 'S02', 'SSO', 'STL')) AND 
                        ((TRIM(d.ditem) NOT LIKE 'MNB1') AND (TRIM(d.ditem) NOT LIKE 'MNB2') AND
                         (d.ditem NOT LIKE 'MNBN%') AND (d.ditem NOT LIKE 'MNB2N%') AND 
                         (d.ditem NOT LIKE 'MNBLN%') AND (d.ditem NOT LIKE 'MNBL2N%') AND 
                         (d.ditem NOT LIKE 'MNSN%') AND (d.ditem NOT LIKE 'MNS2N%') AND (d.ditem NOT LIKE 'MNS3N%'))";
                    break;
                case ReportType.Stone:
                    styleClause += @"
                        (d.dclas IN ('04U', '04V', '04W', 'L01', 'L03', 'L04', 'L09', 'F09', 'PS3', 'RSC', 'RSO', 'TSO')) AND 
                        ((d.ditem LIKE 'RH%') OR (d.ditem LIKE 'TS%') OR (d.ditem LIKE 'RST%') OR (d.ditem Like 'RVOMMPT%'))";
                    break;
                default:
                    ErrorHandler.HandleError(ErrorType.Critical, "Invalid report type in RunReport.");
                    break;
            }

            DataTable reportTable = SqlConnector.RunQuery(dateClause, styleClause);

            return reportTable;
        }
    }
}
