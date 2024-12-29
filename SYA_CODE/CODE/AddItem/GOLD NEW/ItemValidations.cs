using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SYA
{
    public class ItemValidations
    {
        public bool Validate(string metalType, int columnIndex, int rowIndex, Label l, DataGridView dg)
        {
            //     dg[columnIndex+3, rowIndex].Value = "asas";
            dg.Refresh();
            string cellValue = dg[columnIndex, rowIndex].Value?.ToString() ?? "";
            string columnName = dg.Columns[columnIndex].Name.ToString();
            if (columnName == "PURITY")
            {
                return PURITY();
            }
            else if (columnName == "GW")
            {
                return GW();
            }
            else if (columnName == "NW")
            {
                return NW();
            }
            else if (columnName == "LBR_RATE")
            {
                return LABOUR();
            }
            else if (columnName == "OTH_AMT")
            {
                return OTHER();
            }
            else if (columnName == "LBR_AMT")
            {
                return LABOUR_AMOUNT();
            }
            else if (columnName == "HUID1")
            {
                return HUID1();
            }
            else if (columnName == "HUID2")
            {
                return HUID2();
            }
            else if (columnName == "HUID3")
            {
                return HUID3();
            }
            bool PURITY()
            {
                if (!string.IsNullOrEmpty(get_CellValue(rowIndex, "PURITY")))
                {
                    set_CellValue(rowIndex, "GW", "0");
                    set_CellValue(rowIndex, "NW", "0");
                    set_CellValue(rowIndex, "LBR_RATE", "0");
                    set_CellValue(rowIndex, "LBR_AMT", "0");
                    set_CellValue(rowIndex, "OTH_AMT", "0");
                    if (metalType == "S" && cellValue == "925")
                    {
                        set_CellValue(rowIndex, "LBR_RATE", "380");
                    }
                }
                else { return false; }
                return true;
            }
            bool GW()
            {
                if (!string.IsNullOrEmpty(get_CellValue(rowIndex, "GW")) && get_CellValue(rowIndex, "GW") != "0")
                {
                    if (!string.IsNullOrEmpty(get_CellValue(rowIndex, "NW")) && get_CellValue(rowIndex, "NW") != "0")
                    {
                        if (ConvertToDecimal(get_CellValue(rowIndex, "NW")) > ConvertToDecimal(get_CellValue(rowIndex, "GW")))
                        {
                            set_CellValue(rowIndex, "NW", get_CellValue(rowIndex, "GW"));
                        }
                    }
                    else
                    {
                        set_CellValue(rowIndex, "NW", get_CellValue(rowIndex, "GW"));
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            bool NW()
            {
                if (!string.IsNullOrEmpty(get_CellValue(rowIndex, "GW")) && get_CellValue(rowIndex, "GW") != "0")
                {
                    if (ConvertToDecimal(get_CellValue(rowIndex, "NW")) > ConvertToDecimal(get_CellValue(rowIndex, "GW")))
                    {
                        l.Text = "Net Weight is Greater Then Gross Weight !";
                        return false;
                    }
                }
                else { return false; }
                l.Text = "";
                return true;
            }
            bool LABOUR()
            {
                if (!string.IsNullOrEmpty(get_CellValue(rowIndex, "LBR_RATE")) && ConvertToDecimal(get_CellValue(rowIndex, "LBR_RATE")) != 0)
                {
                    set_CellValue(rowIndex, "LBR_AMT", ((ConvertToDecimal(get_CellValue(rowIndex, "NW"))) * (ConvertToDecimal(get_CellValue(rowIndex, "LBR_RATE")))).ToString());
                }

                return true;
            }
            bool OTHER() { return true; }
            bool LABOUR_AMOUNT()
            {
                decimal nw = ConvertToDecimal(get_CellValue(rowIndex, "NW"));
                decimal lbr_rat = ConvertToDecimal(get_CellValue(rowIndex, "LBR_RATE"));
                decimal lbr_amt = ConvertToDecimal(get_CellValue(rowIndex, "LBR_AMT"));
                if (lbr_amt < (lbr_rat * nw)) {
                    set_CellValue(rowIndex, "LBR_AMT", (lbr_rat * nw).ToString());
                }
                return true;
            }
            bool HUID1() { return true; }
            bool HUID2() { return true; }
            bool HUID3() { return true; }
            void set_CellValue(int row_Index, string column, string value)
            {
                dg.Rows[row_Index].Cells[column].Value = value;
                dg.Refresh();
            }
            string get_CellValue(int row_Index, string column)
            {
                return dg.Rows[rowIndex].Cells[column].Value?.ToString() ?? string.Empty;
            }
            decimal ConvertToDecimal(string str)
            {
                return decimal.TryParse(str, out decimal result) ? result : 0m;
            }
            return true;
        }
    }
}
