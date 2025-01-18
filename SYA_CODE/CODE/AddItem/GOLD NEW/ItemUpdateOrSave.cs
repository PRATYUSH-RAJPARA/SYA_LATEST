using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
namespace SYA
{
    public class ItemUpdateOrSave
    {
        string ID = "";
        string CO_YEAR = "";
        string CO_BOOK = "";
        string VCH_NO = "";
        string VCH_DATE = "";
        string PURITY = "";
        string METAL_TYPE = "";
        string TAG_NO = "";
        string DESIGN = "";
        string HUID1 = "";
        string HUID2 = "";
        string HUID3 = "";
        string ITM_SIZE = "";
        string ITEM_TYPE = "";
        string ITM_PCS = "";
        string GW = "";
        string NW = "";
        string LBR_RATE = "";
        string OTH_AMT = "";
        string LBR_AMT = "";
        string SIZE = "";
        string PRICE = "";
        string COMMENT = "";
        string message = "";
        public string update_or_save(int rowIndex, int columnIndex, DataGridView dg, Label L)
        {
            try
            {
              //  L.Text = $"{rowIndex} + {columnIndex} ";
                loadData();
                decideUpdateOrDelete();
                return message;
            }
            catch (Exception ex)
            {
                L.Text += $"\nError: {ex.Message}";
                return "Error";
            }
            void loadData()
            {
                ID = "";
                CO_YEAR = GetFinancialYear();
                CO_BOOK = "115";
                VCH_NO = "0";
                VCH_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                PURITY = dg.Rows[rowIndex].Cells["PURITY"].Value?.ToString() ?? string.Empty;
                METAL_TYPE = "G";
                TAG_NO = dg.Rows[rowIndex].Cells["TAG_NO"].Value?.ToString() ?? string.Empty;
                DESIGN = "";
                HUID1 = dg.Rows[rowIndex].Cells["HUID1"].Value?.ToString() ?? string.Empty;
                HUID2 = dg.Rows[rowIndex].Cells["HUID2"].Value?.ToString() ?? string.Empty;
                HUID3 = dg.Rows[rowIndex].Cells["HUID3"].Value?.ToString() ?? string.Empty;
                ITM_SIZE = "";
                ITEM_TYPE = "";
                ITM_PCS = "0";
                GW = dg.Rows[rowIndex].Cells["GW"].Value?.ToString() ?? string.Empty;
                NW = dg.Rows[rowIndex].Cells["NW"].Value?.ToString() ?? string.Empty;
                LBR_RATE = dg.Rows[rowIndex].Cells["LBR_RATE"].Value?.ToString() ?? string.Empty;
                OTH_AMT = dg.Rows[rowIndex].Cells["OTH_AMT"].Value?.ToString() ?? string.Empty;
                LBR_AMT = dg.Rows[rowIndex].Cells["LBR_AMT"].Value?.ToString() ?? string.Empty;
                SIZE = dg.Rows[rowIndex].Cells["SIZE"].Value?.ToString() ?? string.Empty;
                PRICE = dg.Rows[rowIndex].Cells["PRICE"].Value?.ToString() ?? string.Empty;
                COMMENT = dg.Rows[rowIndex].Cells["COMMENT"].Value?.ToString() ?? string.Empty;
            }
            void decideUpdateOrDelete()
            {
                if (string.IsNullOrWhiteSpace(TAG_NO))
                {
                  //  L.Text += "\nInsert";
                    insert();
                }
                else
                {
                   // L.Text += " Else";
                    string query = $"SELECT * FROM MAIN_DATA_NEW WHERE CO_YEAR = '{CO_YEAR}' AND TAG_NO = '{TAG_NO}'";
                    DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                    if (dt.Rows.Count == 1)
                    {
                   //     L.Text += "\nUpdate 1";
                        update();
                    }
                    else if (dt.Rows.Count == 0)
                    {
                      //  L.Text += "\nInsert 1";
                        insert();
                    }
                }
            }
            void update()
            {
                try
                {
                    string updateQuery = $@"
                        UPDATE MAIN_DATA_NEW
                        SET    HUID1 = '{HUID1}', HUID2 = '{HUID2}', HUID3 = '{HUID3}', GW = '{GW}', NW = '{NW}', LBR_RATE = '{LBR_RATE}', OTH_AMT = '{OTH_AMT}', LBR_AMT = '{LBR_AMT}', SIZE = '{SIZE}', PRICE = '{PRICE}', COMMENT = '{COMMENT}'
                        WHERE CO_YEAR = '{CO_YEAR}' AND TAG_NO = '{TAG_NO}';
                    ";
                    object result = helper.RunQueryWithoutParametersSYADataBase(updateQuery, "ExecuteNonQuery");
                    int affectedRows = Convert.ToInt32(result);
                    message = "update";
                    L.Text = ($"Updated successfully for TAG_NO : {TAG_NO} : ({affectedRows.ToString()}).");
                }
                catch (Exception ex)
                {
                    L.Text += $"\nUpdate Error: {ex.Message}";
                }
            }
            void insert()
            {
                try
                {
                    ITEM_TYPE = GetPRCode(dg.Rows[rowIndex].Cells["ITEM_TYPE"].Value?.ToString() ?? string.Empty);
                    ITM_SIZE = ITEM_TYPE;
                    GetTagNo();
                    insert_now();
                    dg.Rows[rowIndex].Cells["TAG_NO"].ReadOnly = true;
                    dg.Rows[rowIndex].Cells["ITEM_TYPE"].ReadOnly = true;
                    dg.Rows[rowIndex].Cells["PURITY"].ReadOnly = true;
                }
                catch (Exception ex)
                {
                    L.Text += $"\nInsert Error: {ex.Message}";
                }
                string GetPRCode(string itemName)
                {
                    try
                    {
                        string query = $"SELECT PR_CODE FROM ITEM_MASTER WHERE IT_NAME = '{itemName}' AND IT_TYPE = 'G'";
                        DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                        return dt.Rows.Count > 0 ? dt.Rows[0]["PR_CODE"].ToString() : string.Empty;
                    }
                    catch (Exception ex)
                    {
                        L.Text += $"\nPRCode Error: {ex.Message}";
                        return string.Empty;
                    }
                }
                void GetTagNo()
                {
                    try
                    {
                        string prCode = GetPRCode(dg.Rows[rowIndex].Cells["ITEM_TYPE"].Value?.ToString() ?? string.Empty);
                        string prefix = "SYA";
                        int newSequenceNumber = GetNextSequenceNumber();
                        if (!string.IsNullOrEmpty(PURITY) && !string.IsNullOrEmpty(prCode))
                        {
                            string newTagNo = $"{prefix}{PURITY}{prCode}{newSequenceNumber:D5}";
                            dg.Rows[rowIndex].Cells["TAG_NO"].Value = newTagNo;
                            TAG_NO = newTagNo;
                        }
                        else
                        {
                            throw new Exception("CARET or PR_CODE is empty. Cannot generate Tag No.");
                        }
                        int GetNextSequenceNumber()
                        {
                            string query = $"SELECT CAST(SUBSTR(TAG_NO, {prefix.Length + prCode.Length + PURITY.Length + 1}) AS INTEGER) FROM MAIN_DATA_NEW WHERE ITEM_TYPE = '{prCode}' AND PURITY = '{PURITY}'";
                            DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                            List<int> soldTagNumbers = new List<int>();
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[0] != DBNull.Value)
                                {
                                    soldTagNumbers.Add(Convert.ToInt32(row[0]));
                                }
                            }
                            soldTagNumbers.Sort();
                            int nextSequenceNumber = 1;
                            foreach (int tagNumber in soldTagNumbers)
                            {
                                if (tagNumber > nextSequenceNumber)
                                {
                                    return nextSequenceNumber;
                                }
                                nextSequenceNumber = tagNumber + 1;
                            }
                            return nextSequenceNumber;
                        }
                    }
                    catch (Exception ex)
                    {
                        L.Text += $"\nGetTagNo Error: {ex.Message}";
                    }
                }
                void insert_now()
                {
                    try
                    {
                        string insertQuery = $@"
                            INSERT INTO MAIN_DATA_NEW (CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, HUID3, ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, COMMENT)
                            VALUES ('{CO_YEAR}', '{CO_BOOK}', '{VCH_NO}', '{VCH_DATE}', '{PURITY}', '{METAL_TYPE}', '{TAG_NO}', '{DESIGN}', '{HUID1}', '{HUID2}', '{HUID3}', '{ITM_SIZE}', '{ITEM_TYPE}', {ITM_PCS}, {GW}, {NW}, '{LBR_RATE}', {OTH_AMT}, {LBR_AMT}, '{SIZE}', {PRICE}, '{COMMENT}');
                        ";
                        object result = helper.RunQueryWithoutParametersSYADataBase(insertQuery, "ExecuteNonQuery");
                        int affectedRows = Convert.ToInt32(result);
                        message = "insert";
                        L.Text = ($"Inserted successfully for TAG_NO : {TAG_NO} : ({affectedRows.ToString()}).");
                    }
                    catch (Exception ex)
                    {
                        L.Text += $"\nInsertNow Error: {ex.Message}";
                    }
                }
            }
        }
        public static string GetFinancialYear()
        {
            DateTime now = DateTime.Now;
            int startYear = now.Month >= 4 ? now.Year : now.Year - 1;
            int endYear = startYear + 1;
            return $"{startYear}-{endYear}";
        }
    }
}
