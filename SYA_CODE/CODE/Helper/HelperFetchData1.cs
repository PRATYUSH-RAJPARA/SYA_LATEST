using SYA;
using System.Data.SQLite;
using System.Data;
public class HelperFetchData1
{
    DataTable accessData = new DataTable();
    public static NotifyForm notifyForm;
    // Initialize NotifyForm
    private void InitializeNotifyForm()
    {
        notifyForm = new NotifyForm();
        notifyForm.Show();
        Application.DoEvents();
    }
    public void processSaleData()
    {
        accessData.Clear();
        fetchAccessData("026");
        foreach (DataRow row in accessData.Rows)
        {
            if (RecordExists(row, "SALE_DATA_NEW"))
            {
                ExecuteUpdateQueryForSales(row);
            }
            else
            {
                ExecuteInsertQueryForSales(row);
            }
        }
    }
    // Process data with real-time count updates
    public void ProcessData()
    {
        fetchAccessData("015");
        string CO_YEAR_CHECK = (accessData.Rows[3]["CO_YEAR"] ?? "").ToString();
        bool IsFinancialYearGreaterOrEqual(string financialYear, string targetYear = "2024-2025")
        {
            // Check if input is null or empty
            if (string.IsNullOrWhiteSpace(financialYear))
                return false;
            try
            {
                // Split the years into parts
                var financialYearParts = financialYear.Split('-');
                var targetYearParts = targetYear.Split('-');
                // Ensure the format is correct
                if (financialYearParts.Length != 2 || targetYearParts.Length != 2)
                    return false;
                // Parse the years to integers
                int startYear = int.Parse(financialYearParts[0]);
                int endYear = int.Parse(financialYearParts[1]);
                int targetStartYear = int.Parse(targetYearParts[0]);
                int targetEndYear = int.Parse(targetYearParts[1]);
                // Compare the financial years
                if (startYear > targetStartYear || (startYear == targetStartYear && endYear >= targetEndYear))
                    return true;
            }
            catch
            {
                // Handle parsing errors gracefully
                return false;
            }
            return false;
        }
        bool doOrNot = false;
        if (IsFinancialYearGreaterOrEqual(CO_YEAR_CHECK))
        {
            doOrNot = true;
        }
        else
        {
            if (Verification.ValidatePassword())
            {
                doOrNot = true;
            }
            else
            {
                doOrNot = false;
            }
        }
        if (doOrNot)
        {
            InitializeNotifyForm();
            int totalRows = accessData.Rows.Count;
            int updatedRows = 0;
            int insertedRows = 0;
            int skippedRows = 0;
            // Notify total rows in Access
            notifyForm.ShowNotification1($"Total Rows in Access: {totalRows}");
            Application.DoEvents();
            int currentRow = 0;
            foreach (DataRow row in accessData.Rows)
            {
                string tagNo = row["TAG_NO"]?.ToString() ?? "";
                string coYear = row["CO_YEAR"]?.ToString() ?? "";
                DataTable dt = CheckIfTagExistsInAccess(tagNo, coYear);
                if (dt.Rows.Count > 0)
                {
                    // Handle existing tag
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (RecordExists(dr, "SALE_DATA_NEW"))
                        {
                            ExecuteUpdateQueryForSales(dr);
                            updatedRows++;
                        }
                        else
                        {
                            ExecuteInsertQueryForSales(dr);
                            insertedRows++;
                        }
                    }
                    DeleteFromSQLite(tagNo, coYear);
                }
                else
                {
                    // Handle new data
                    if (RecordExists(row, "MAIN_DATA_NEW"))
                    {
                        ExecuteUpdateQuery(row);
                        updatedRows++;
                    }
                    else
                    {
                        ExecuteInsertQuery(row);
                        insertedRows++;
                    }
                }
                currentRow++;
                notifyForm.ShowNotification2($"Processed: {currentRow}/{totalRows} | Updated: {updatedRows} | Inserted: {insertedRows} | Skipped: {skippedRows}");
                Application.DoEvents();
            }
            // Final update after processing all rows
            notifyForm.ShowNotification1($"Access Rows: {totalRows}");
            notifyForm.ShowNotification2($"Processing Complete! Updated: {updatedRows}, Inserted: {insertedRows}, Skipped: {skippedRows}");
            Application.DoEvents();
            notifyForm.CloseNotifyFormAfterDelay(3000);
        }
    }
    public void fetchAccessData(string cobook)
    {
        string query = "SELECT CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, IT_CODE, PR_CODE, IT_TYPE, TAG_NO, DESIGN, ITM_SIZE, ITM_PCS, ITM_GWT, ITM_NWT, LBR_RATE, OTH_AMT, LBR_AMT, ITM_RAT, ITM_AMT, AC_CODE, IT_DESC FROM MAIN_TAG_DATA WHERE CO_BOOK = '" + cobook + "'";
        accessData.Clear();
        accessData = helper.FetchDataTableFromDataCareDataBase(query);
    }
    public bool RecordExists(DataRow accessDataRow, string tableName)
    {
        string tagNo = accessDataRow["TAG_NO"]?.ToString() ?? "";
        string coYear = accessDataRow["CO_YEAR"]?.ToString() ?? "";
        string query = "SELECT 1 FROM " + tableName + " WHERE TAG_NO = '" + tagNo + "' AND CO_YEAR = '" + coYear + "'";
        DataTable dt = helper.FetchDataTableFromSYADataBase(query);
        return dt.Rows.Count > 0;
    }
    public DataTable CheckIfTagExistsInAccess(string tagNo, string coYear)
    {
        string query = "SELECT * FROM MAIN_TAG_DATA WHERE TAG_NO = '" + tagNo + "' AND CO_YEAR = '" + coYear + "' AND CO_BOOK IN ('026', '027')";
        return helper.FetchDataTableFromDataCareDataBase(query);
    }
    public void DeleteFromSQLite(string tagNo, string coYear)
    {
        string query = "DELETE FROM MAIN_DATA_NEW WHERE TAG_NO = '" + tagNo + "' AND CO_YEAR = '" + coYear + "'";
        helper.RunQueryWithParametersSYADataBase(query);
    }
    // Helper method for mapping parameters to SQLite parameters
    public List<SQLiteParameter> MapData_Stock(DataRow accessDataRow)
    {
        var sqliteParameters = new List<SQLiteParameter>
        {
            new SQLiteParameter("@CO_YEAR", accessDataRow["CO_YEAR"] ?? ""),
            new SQLiteParameter("@CO_BOOK", accessDataRow["CO_BOOK"] ?? ""),
            new SQLiteParameter("@VCH_NO", accessDataRow["VCH_NO"] ?? ""),
            new SQLiteParameter("@VCH_DATE", accessDataRow["VCH_DATE"] ?? "")
        };
        string itCode = accessDataRow["IT_CODE"]?.ToString() ?? "";
        string prCode = accessDataRow["PR_CODE"]?.ToString() ?? "";
        string purity = itCode.Replace(prCode, "");
        sqliteParameters.Add(new SQLiteParameter("@PURITY", purity));
        sqliteParameters.Add(new SQLiteParameter("@METAL_TYPE", accessDataRow["IT_TYPE"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@TAG_NO", accessDataRow["TAG_NO"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@DESIGN", accessDataRow["DESIGN"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@ITM_SIZE", accessDataRow["ITM_SIZE"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@ITM_PCS", accessDataRow["ITM_PCS"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@GW", accessDataRow["ITM_GWT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@NW", accessDataRow["ITM_NWT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@LBR_RATE", accessDataRow["LBR_RATE"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@OTH_AMT", accessDataRow["OTH_AMT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@LBR_AMT", accessDataRow["LBR_AMT"] ?? 0));
        string design = accessDataRow["DESIGN"]?.ToString() ?? "";
        string[] designParts = design.Split(',');
        sqliteParameters.Add(new SQLiteParameter("@HUID1", designParts.Length > 0 ? designParts[0] : ""));
        sqliteParameters.Add(new SQLiteParameter("@HUID2", designParts.Length > 1 ? designParts[1] : ""));
        sqliteParameters.Add(new SQLiteParameter("@HUID3", designParts.Length > 2 ? designParts[2] : ""));
        sqliteParameters.Add(new SQLiteParameter("@COMMENT", designParts.Length > 3 ? string.Join(",", designParts.Skip(3)) : ""));
        if (accessDataRow["ITM_SIZE"].ToString().Length == 0)
        {
            sqliteParameters.Add(new SQLiteParameter("@ITEM_TYPE", accessDataRow["PR_CODE"].ToString()));
        }
        else
        {
            sqliteParameters.Add(new SQLiteParameter("@ITEM_TYPE", accessDataRow["ITM_SIZE"] ?? ""));
        }
        sqliteParameters.Add(new SQLiteParameter("@SIZE", ""));
        sqliteParameters.Add(new SQLiteParameter("@PRICE", 0));
        return sqliteParameters;
    }
    // Helper method for mapping parameters for sales data
    public List<SQLiteParameter> MapData_Sales(DataRow accessDataRow)
    {
        var sqliteParameters = new List<SQLiteParameter>
        {
            new SQLiteParameter("@CO_YEAR", accessDataRow["CO_YEAR"] ?? ""),
            new SQLiteParameter("@CO_BOOK", accessDataRow["CO_BOOK"] ?? ""),
            new SQLiteParameter("@VCH_NO", accessDataRow["VCH_NO"] ?? ""),
            new SQLiteParameter("@VCH_DATE", accessDataRow["VCH_DATE"] ?? "")
        };
        string itCode = accessDataRow["IT_CODE"]?.ToString() ?? "";
        string prCode = accessDataRow["PR_CODE"]?.ToString() ?? "";
        string purity = " ";
        try
        {
            purity = itCode.Replace(prCode, "");
        }
        catch { }
        sqliteParameters.Add(new SQLiteParameter("@PURITY", purity));
        sqliteParameters.Add(new SQLiteParameter("@METAL_TYPE", accessDataRow["IT_TYPE"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@TAG_NO", accessDataRow["TAG_NO"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@DESIGN", accessDataRow["DESIGN"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@ITM_SIZE", accessDataRow["ITM_SIZE"] ?? ""));
        sqliteParameters.Add(new SQLiteParameter("@ITM_PCS", accessDataRow["ITM_PCS"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@GW", accessDataRow["ITM_GWT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@NW", accessDataRow["ITM_NWT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@LBR_RATE", accessDataRow["LBR_RATE"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@OTH_AMT", accessDataRow["OTH_AMT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@LBR_AMT", accessDataRow["LBR_AMT"] ?? 0));
        string design = accessDataRow["DESIGN"]?.ToString() ?? "";
        string[] designParts = design.Split(',');
        sqliteParameters.Add(new SQLiteParameter("@HUID1", designParts.Length > 0 ? designParts[0] : ""));
        sqliteParameters.Add(new SQLiteParameter("@HUID2", designParts.Length > 1 ? designParts[1] : ""));
        sqliteParameters.Add(new SQLiteParameter("@HUID3", designParts.Length > 2 ? designParts[2] : ""));
        sqliteParameters.Add(new SQLiteParameter("@COMMENT", designParts.Length > 3 ? string.Join(",", designParts.Skip(3)) : ""));
        if (accessDataRow["ITM_SIZE"].ToString().Length == 0)
        {
            sqliteParameters.Add(new SQLiteParameter("@ITEM_TYPE", accessDataRow["PR_CODE"].ToString()));
        }
        else
        {
            sqliteParameters.Add(new SQLiteParameter("@ITEM_TYPE", accessDataRow["ITM_SIZE"] ?? ""));
        }
        sqliteParameters.Add(new SQLiteParameter("@SIZE", ""));
        sqliteParameters.Add(new SQLiteParameter("@PRICE", 0));
        sqliteParameters.Add(new SQLiteParameter("@ITM_RAT", accessDataRow["ITM_RAT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@ITM_AMT", accessDataRow["ITM_AMT"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@AC_CODE", accessDataRow["AC_CODE"] ?? 0));
        sqliteParameters.Add(new SQLiteParameter("@AC_NAME", FetchACName(accessDataRow["AC_CODE"].ToString() ?? "")));
        string comment = (accessDataRow["IT_DESC"]?.ToString() ?? "") + " " + (designParts.Length > 3 ? string.Join(",", designParts.Skip(3)) : "");
        sqliteParameters.Add(new SQLiteParameter("@COMMENT", comment));
        return sqliteParameters;
    }
    public string FetchACName(string acCode)
    {
        string query = "SELECT AC_NAME FROM AC_MAST WHERE AC_CODE = '" + acCode + "'"; // Assuming your account table is named `ACCOUNT_TABLE`
        DataTable dt = helper.FetchDataTableFromDataCareDataBase(query);
        return dt.Rows.Count > 0 ? dt.Rows[0]["AC_NAME"].ToString() : "";
    }
    // Execute query for insert operation
    public void ExecuteInsertQuery(DataRow accessDataRow)
    {
        string query = "INSERT INTO MAIN_DATA_NEW (CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, ITM_SIZE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, HUID1, HUID2, HUID3, ITEM_TYPE, SIZE, PRICE, COMMENT) VALUES (@CO_YEAR, @CO_BOOK, @VCH_NO, @VCH_DATE, @PURITY, @METAL_TYPE, @TAG_NO, @DESIGN, @ITM_SIZE, @ITM_PCS, @GW, @NW, @LBR_RATE, @OTH_AMT, @LBR_AMT, @HUID1, @HUID2, @HUID3, @ITEM_TYPE, @SIZE, @PRICE, @COMMENT)";
        helper.RunQueryWithParametersSYADataBase(query, MapData_Stock(accessDataRow).ToArray());
    }
    // Execute update query
    public void ExecuteUpdateQuery(DataRow accessDataRow)
    {
        string query = "UPDATE MAIN_DATA_NEW SET CO_BOOK = @CO_BOOK, VCH_NO = @VCH_NO, VCH_DATE = @VCH_DATE, PURITY = @PURITY, METAL_TYPE = @METAL_TYPE, DESIGN = @DESIGN, ITM_SIZE = @ITM_SIZE, ITM_PCS = @ITM_PCS, GW = @GW, NW = @NW, LBR_RATE = @LBR_RATE, OTH_AMT = @OTH_AMT, LBR_AMT = @LBR_AMT, HUID1 = @HUID1, HUID2 = @HUID2, HUID3 = @HUID3, ITEM_TYPE = @ITEM_TYPE, SIZE = @SIZE, PRICE = @PRICE, COMMENT = @COMMENT WHERE TAG_NO = @TAG_NO AND CO_YEAR = @CO_YEAR";
        helper.RunQueryWithParametersSYADataBase(query, MapData_Stock(accessDataRow).ToArray());
    }
    // Execute query for insert operation in sales data
    public void ExecuteInsertQueryForSales(DataRow accessDataRow)
    {
        string query = "INSERT INTO SALE_DATA_NEW (CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, ITM_SIZE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, HUID1, HUID2, HUID3, ITEM_TYPE, SIZE, PRICE, ITM_RAT, ITM_AMT, COMMENT, AC_CODE,AC_NAME) VALUES (@CO_YEAR, @CO_BOOK, @VCH_NO, @VCH_DATE, @PURITY, @METAL_TYPE, @TAG_NO, @DESIGN, @ITM_SIZE, @ITM_PCS, @GW, @NW, @LBR_RATE, @OTH_AMT, @LBR_AMT, @HUID1, @HUID2, @HUID3, @ITEM_TYPE, @SIZE, @PRICE, @ITM_RAT, @ITM_AMT, @COMMENT, @AC_CODE,@AC_NAME)";
        helper.RunQueryWithParametersSYADataBase(query, MapData_Sales(accessDataRow).ToArray());
    }
    // Execute update query for sales data
    public void ExecuteUpdateQueryForSales(DataRow accessDataRow)
    {
        string query = "UPDATE SALE_DATA_NEW SET CO_BOOK = @CO_BOOK, VCH_NO = @VCH_NO, VCH_DATE = @VCH_DATE, PURITY = @PURITY, METAL_TYPE = @METAL_TYPE, DESIGN = @DESIGN, ITM_SIZE = @ITM_SIZE, ITM_PCS = @ITM_PCS, GW = @GW, NW = @NW, LBR_RATE = @LBR_RATE, OTH_AMT = @OTH_AMT, LBR_AMT = @LBR_AMT, HUID1 = @HUID1, HUID2 = @HUID2, HUID3 = @HUID3, ITEM_TYPE = @ITEM_TYPE, SIZE = @SIZE, PRICE = @PRICE, ITM_RAT = @ITM_RAT, ITM_AMT = @ITM_AMT, COMMENT = @COMMENT, AC_CODE = @AC_CODE,AC_NAME = @AC_NAME WHERE TAG_NO = @TAG_NO AND CO_YEAR = @CO_YEAR";
        helper.RunQueryWithParametersSYADataBase(query, MapData_Sales(accessDataRow).ToArray());
    }
}
