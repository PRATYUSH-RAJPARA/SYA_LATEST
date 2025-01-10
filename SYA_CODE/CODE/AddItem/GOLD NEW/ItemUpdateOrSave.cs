using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;
using System.Data;
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
        public void update_or_save(int rowIndex, int columnIndex, DataGridView dg)
        {
            loadData();
            decideUpdateOrDelete();
            void loadData()
            {
                ID = "";
                CO_YEAR = GetFinancialYear();
                CO_BOOK = "";
                VCH_NO = "";
                VCH_DATE = DateTime.Now.ToString("yyyy-MM-dd"); ;
                PURITY = dg.Rows[rowIndex].Cells["PURITY"].Value?.ToString() ?? string.Empty;
                METAL_TYPE = "";
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
                string query = $"SELECT * FROM MAIN_DATA_NEW WHERE CO_YEAR = '{CO_YEAR}' AND TAG_NO = '{TAG_NO}'";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                if (dt.Rows.Count == 1)
                {
                    update();
                }
                else if (dt.Rows.Count == 0)
                {
                    insert();
                }
            }
            void update()
            {
                string updateQuery = $@"
                    UPDATE MAIN_DATA_NEW
                    SET CO_YEAR = '{CO_YEAR}', CO_BOOK = '{CO_BOOK}', VCH_NO = '{VCH_NO}', VCH_DATE = '{VCH_DATE}', PURITY = '{PURITY}', METAL_TYPE = '{METAL_TYPE}', TAG_NO = '{TAG_NO}', DESIGN = '{DESIGN}', HUID1 = '{HUID1}', HUID2 = '{HUID2}', HUID3 = '{HUID3}', ITM_SIZE = '{ITM_SIZE}', ITEM_TYPE = '{ITEM_TYPE}', ITM_PCS = '{ITM_PCS}', GW = '{GW}', NW = '{NW}', LBR_RATE = '{LBR_RATE}', OTH_AMT = '{OTH_AMT}', LBR_AMT = '{LBR_AMT}', SIZE = '{SIZE}', PRICE = '{PRICE}', COMMENT = '{COMMENT}'
                    WHERE CO_YEAR = '{CO_YEAR}' AND TAG_NO = '{TAG_NO}';
                    ";
                helper.RunQueryWithoutParametersSYADataBase(updateQuery, "ExecuteScalar");
            }
            void insert()
            {

                string insertQuery = $@"
                INSERT INTO MAIN_DATA_NEW (ID, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, HUID3, ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, COMMENT)
                VALUES ('{ID}', '{CO_YEAR}', '{CO_BOOK}', '{VCH_NO}', '{VCH_DATE}', '{PURITY}', '{METAL_TYPE}', '{TAG_NO}', '{DESIGN}', '{HUID1}', '{HUID2}', '{HUID3}', '{ITM_SIZE}', '{ITEM_TYPE}', '{ITM_PCS}', '{GW}', '{NW}', '{LBR_RATE}', '{OTH_AMT}', '{LBR_AMT}', '{SIZE}', '{PRICE}', '{COMMENT}');
                ";
                helper.RunQueryWithoutParametersSYADataBase(insertQuery, "ExecuteScalar");

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
