using System;
using System.Data;
using System.Data.Odbc;
using Lettering.Errors;

namespace Lettering.IO {
    internal class SqlConnector {
        private static string connectionString = "Driver={iSeries Access ODBC Driver}; System=USC; SignOn=4;";

        internal static DataTable RunQuery(string dateClause, string styleClause) {
            try { 
                using(OdbcConnection conn = new OdbcConnection(connectionString)) {
                    conn.Open();

                    string query = $@"
                        SELECT det.dhous, det.scdat, det.endat, det.ordnr, det.orvch, 
                               det.ditem, det.dlsiz, siz.letwid, nam.letname, 
                               det.dlwr1, det.dlwr2, det.dlwr3, det.dlwr4, 
                               CASE WHEN det.ditem LIKE 'SIGN%' THEN clr.gclr ELSE TRIM(det.dclr1) END AS dclr1, det.dclr2, det.dclr3, det.dclr4, det.rudat
                        FROM (
                              SELECT d.dhous,
                                     CASE WHEN d.dscmo = 0 THEN NULL ELSE DATE(d.dsccy||d.dscyr||'-'||RIGHT('00'||d.dscmo, 2)||'-'||RIGHT('00'||d.dscda, 2)) END AS scdat, 
                                     DATE(d.dorcy||d.doryr||'-'||RIGHT('00'||d.dormo, 2)||'-'||RIGHT('00'||d.dorda, 2)) AS endat, 
                                     d.ordnr, d.orvch, d.dpvch, TRIM(d.ditem) AS ditem, d.dlsiz, 
                                     TRIM(d.dlwr1) as dlwr1, TRIM(d.dlwr2) as dlwr2, TRIM(d.dlwr3) as dlwr3, TRIM(d.dlwr4) as dlwr4, 
                                     TRIM(d.dclr1) AS dclr1, TRIM(d.dclr2) AS dclr2, TRIM(d.dclr3) AS dclr3, TRIM(d.dclr4) AS dclr4, 
                                     CASE d.drumo WHEN 0 THEN NULL ELSE DATE(d.drucy||d.druyr||'-'||RIGHT('00'||d.drumo, 2)||'-'||RIGHT('00'||d.druda, 2)) END AS rudat 

                              FROM VARSITYF.DETAIL AS d
                              WHERE ({dateClause}) AND ({styleClause}) AND (d.dscda > 0)
                        ) AS det

                        LEFT JOIN 
                                    DJLIBR.ORD_NAM_C 
                         AS nam
                        ON det.ordnr = nam.ordnr AND det.orvch = nam.orvch AND nam.letname <> ''

                        LEFT JOIN (
                                    SELECT DISTINCT s.ordnr, s.orvch, s.letwid
                                    FROM VARSITYF.HLDSIZ AS s
                        ) AS siz
                        ON det.ordnr = siz.ordnr AND det.dpvch = siz.orvch

                        LEFT JOIN 
                                    VARSITYF.HLDCLR
                         AS clr
                        ON det.ordnr = clr.ordnr AND det.orvch = clr.orvch AND clr.itseq = 2

                        ORDER BY det.ditem";

                    OdbcCommand command = new OdbcCommand(query);
                    command.Connection = conn;
                    OdbcDataAdapter adapter = new OdbcDataAdapter(command);

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    conn.Close();
                    return table;
                }
            } catch(OdbcException e) {
                if(e.Errors[0].SQLState == "IM002") {
                    ErrorHandler.HandleError(ErrorType.Alert, "Driver not found.\n\nPlease contact the IT Department to install the ODBC Driver for IBM iSeries Access.");
                    return null;
                } else {
                    string odbcErrorLog = "";
                    for(int i = 0; i != e.Errors.Count; ++i) {
                        odbcErrorLog += ("Error " + (i + 1) + " of " + e.Errors.Count + "\n");
                        odbcErrorLog += ("SQLState:  " + e.Errors[i].SQLState + "\n");
                        odbcErrorLog += ("NativErr:  " + e.Errors[i].NativeError + "\n");
                        odbcErrorLog += ("EMessage:  " + e.Errors[i].Message + "\n");
                        odbcErrorLog += ("ESource:   " + e.Errors[i].Source + "\n\n");
                    }
                    ErrorHandler.HandleError(ErrorType.Alert, odbcErrorLog);
                    return null;
                }
            } catch(Exception e) {
                ErrorHandler.HandleError(ErrorType.Alert, "Exception: " + e.Message);
                return null;
            }
        }
    }
}
