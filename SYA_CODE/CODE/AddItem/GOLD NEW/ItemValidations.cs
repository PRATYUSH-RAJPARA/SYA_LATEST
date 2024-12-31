﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SYA
{
    public class ItemValidations
    {
        public bool Validate(string metalType, int columnIndex, int rowIndex, Label l, DataGridView dg, AutoCompleteStringCollection itemTypeCollection, AutoCompleteStringCollection purityCollection)
        {
            dg.Refresh();
            string cellValue = dg[columnIndex, rowIndex].Value?.ToString() ?? "";
            string columnName = dg.Columns[columnIndex].Name.ToString();
            if (columnName == "ITEM_TYPE")
            {
                return ITEM_TYPE();
            }
            else if (columnName == "PURITY")
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
            else if (columnName == "PRICE")
            {
                return PRICE();
            }
            bool ITEM_TYPE()
            {
                if (!itemTypeCollection.Contains(cellValue))
                {
                    return false;
                }
                return true;
            }
            bool PURITY()
            {
                if (!purityCollection.Contains(cellValue))
                {
                    return false; // Set flag to false if value is not in the collection
                }
                else
                {
                    set_CellValue(rowIndex, "GW", "0");
                    return true;
                }
            }
            bool GW()
            {
                string NW = get_CellValue(rowIndex, "NW");
                string GW = get_CellValue(rowIndex, "GW");
                if (!string.IsNullOrEmpty(GW) && GW != "0")
                {
                    set_CellValue(rowIndex, "GW", ConvertToDecimal_3digit(GW));
                    GW = get_CellValue(rowIndex, "GW");
                    if (!string.IsNullOrEmpty(NW) && NW != "0")
                    {
                        if (ConvertToDecimal(NW) > ConvertToDecimal(GW))
                        {
                            set_CellValue(rowIndex, "NW", GW);
                        }
                    }
                    else
                    {
                        set_CellValue(rowIndex, "NW", GW);
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
                string NW = get_CellValue(rowIndex, "NW");
                string GW = get_CellValue(rowIndex, "GW");
                string LBR_RATE = get_CellValue(rowIndex, "LBR_RATE");
                if (!string.IsNullOrEmpty(NW) && NW != "0")
                {
                    set_CellValue(rowIndex, "NW", ConvertToDecimal_3digit(NW));
                    NW = get_CellValue(rowIndex, "NW");
                    if (string.IsNullOrEmpty(GW))
                    {
                        set_CellValue(rowIndex, "GW", NW);
                    }
                    if (ConvertToDecimal(NW) > ConvertToDecimal(GW))
                    {
                        l.Text = "Net Weight is Greater Then Gross Weight !";
                        return false;
                    }
                }
                l.Text = "";
                set_CellValue(rowIndex, "LBR_RATE", "0");
                return true;
            }
            bool LABOUR()
            {
                Set_0_If_Null(rowIndex, "LBR_RATE");
                string NW = get_CellValue(rowIndex, "NW");
                string LBR_RATE = get_CellValue(rowIndex, "LBR_RATE");
                if (!string.IsNullOrEmpty(LBR_RATE) && ConvertToDecimal(LBR_RATE) != 0)
                {
                    set_CellValue(rowIndex, "LBR_AMT", CustomRound(((ConvertToDecimal(NW)) * (ConvertToDecimal(LBR_RATE))), 1).ToString());
                }
                return true;
            }
            bool OTHER()
            {
                decimal price = 7250;
                string NW = (get_CellValue(rowIndex, "NW"));
                string LBR_AMT = (get_CellValue(rowIndex, "LBR_AMT"));
                string OTH_AMT = (get_CellValue(rowIndex, "OTH_AMT"));
                decimal p = CustomRound((ConvertToDecimal(NW) * price) + ConvertToDecimal(LBR_AMT) + ConvertToDecimal(OTH_AMT),1);

                Set_0_If_Null(rowIndex, "OTH_AMT");
                set_CellValue(rowIndex, "PRICE", p.ToString());
                return true;
            }
            bool LABOUR_AMOUNT()
            {
                Set_0_If_Null(rowIndex, "LBR_AMT");
                string NW = (get_CellValue(rowIndex, "NW"));
                string LBR_RATE = (get_CellValue(rowIndex, "LBR_RATE"));
                string LBR_AMT = (get_CellValue(rowIndex, "LBR_AMT"));
                decimal amount = (ConvertToDecimal(LBR_RATE) * ConvertToDecimal(NW));
                if (!string.IsNullOrEmpty(LBR_AMT))
                {
                    if (ConvertToDecimal(LBR_AMT) < (ConvertToDecimal(LBR_RATE) * ConvertToDecimal(NW)))
                    {
                        l.Text = "GG\n";
                        set_CellValue(rowIndex, "LBR_AMT", amount.ToString());
                    }
                    else if (ConvertToDecimal(LBR_AMT) > (ConvertToDecimal(LBR_RATE) * ConvertToDecimal(NW)))
                    {
                        l.Text = "FG \n";
                        set_CellValue(rowIndex, "LBR_RATE", "0");
                    }
                }
                else
                {
                    set_CellValue(rowIndex, "LBR_AMT", amount.ToString());
                }
                set_CellValue(rowIndex, "OTH_AMT", "0");
                return true;
            }
            bool HUID1()
            {
                string huid1 = get_CellValue(rowIndex, "HUID1");
                if (string.IsNullOrEmpty(huid1)) { return true; }
                else if (huid1.Length == 6) { return true; }
                else { return false; }
            }
            bool HUID2()
            {
                string huid2 = get_CellValue(rowIndex, "HUID2");
                if (string.IsNullOrEmpty(huid2)) { return true; }
                else if (huid2.Length == 6) { return true; }
                else { return false; }
            }
            bool HUID3()
            {
                string huid3 = get_CellValue(rowIndex, "HUID3");
                if (string.IsNullOrEmpty(huid3)) { return true; }
                else if (huid3.Length == 6) { return true; }
                else { return false; }
            }
            bool PRICE() {

                return true;
            }
            void set_CellValue(int row_Index, string column, string value)
            {
                dg.Rows[row_Index].Cells[column].Value = value;
                dg.Refresh();
            }
            string get_CellValue(int row_Index, string column)
            {
                return dg.Rows[rowIndex].Cells[column].Value?.ToString() ?? string.Empty;
            }
            string ConvertToDecimal_3digit(string str)
            {
                if (str != null && decimal.TryParse(str.ToString(), out decimal weight))
                {
                    // Format the entered value to have three decimal places
                    return weight.ToString("0.000");
                }
                return (cellValue ?? "").ToString();
            }
            decimal ConvertToDecimal(string str)
            {
                if (decimal.TryParse(str, out decimal result))
                {
                    // Ensure the value is rounded to 3 decimal places
                    return Math.Round(result, 3);
                }
                else
                {
                    // Return 3.000 if parsing fails
                    return 3.000m;
                }
            }
            int CustomRound(decimal value, decimal step)
            {
                decimal remainder = value % step;
                if (remainder == 0)
                {
                    return (int)value;
                }
                else
                {
                    return (int)(value + (step - remainder));
                }
            }
            void Set_0_If_Null(int row_index, string column)
            {
                if (string.IsNullOrEmpty(get_CellValue(row_index, column)))
                {
                    set_CellValue(row_index, column, "0");
                }
            }
            return true;
        }
    }
}
