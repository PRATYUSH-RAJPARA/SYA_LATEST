using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace SYA
{
    public class ItemValidations
    {
        public bool change_Labour_On_Price_Change = false;
        public decimal RATE = 92;
        public void ExportDataGridViewToExcel(DataGridView dgv)
        {
            // Generate file name with timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string filePath = @$"F:/ExportedDataGridView_{timestamp}.xlsx";
            // Enable EPPlus license
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");
                // Export header
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    var headerCell = dgv.Columns[col].HeaderCell;
                    var excelCell = worksheet.Cells[1, col + 1];
                    // Set header value
                    excelCell.Value = headerCell.Value ?? dgv.Columns[col].HeaderText;
                    // Apply header styles
                    excelCell.Style.Font.Bold = true;
                    excelCell.Style.Font.Size = 12;
                    excelCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    excelCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    excelCell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    excelCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                // Export data rows
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        var gridCell = dgv.Rows[row].Cells[col];
                        var excelCell = worksheet.Cells[row + 2, col + 1];
                        // Force the cell's value to string (as text)
                        excelCell.Value = gridCell.Value?.ToString() ?? string.Empty;
                        excelCell.Style.Numberformat.Format = "@"; // Force Excel to treat as text
                        // Apply font styles
                        excelCell.Style.Font.Size = 11;
                        excelCell.Style.Font.Color.SetColor(gridCell.Style.ForeColor != Color.Empty ? gridCell.Style.ForeColor : Color.Black);
                        // Apply text alignment (Left, Center, Right)
                        DataGridViewContentAlignment align = gridCell.Style.Alignment;
                        excelCell.Style.HorizontalAlignment = align switch
                        {
                            DataGridViewContentAlignment.MiddleRight => ExcelHorizontalAlignment.Right,
                            DataGridViewContentAlignment.MiddleCenter => ExcelHorizontalAlignment.Center,
                            _ => ExcelHorizontalAlignment.Left
                        };
                        // Apply background color (default to White if not set)
                        Color bgColor = gridCell.Style.BackColor != Color.Empty ? gridCell.Style.BackColor : Color.White;
                        excelCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        excelCell.Style.Fill.BackgroundColor.SetColor(bgColor);
                        // Apply borders
                        excelCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }
                // Auto-fit columns (taking into account column width scaling)
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    worksheet.Column(col + 1).AutoFit();
                }
                // Save file
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
                // Confirm export
                MessageBox.Show($"Excel file generated successfully at:\n{filePath}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public bool Validate(string metalType, int columnIndex, int rowIndex, Label l, DataGridView dg, AutoCompleteStringCollection itemTypeCollection, AutoCompleteStringCollection purityCollection, String GOLD_SILVER)
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
            else if (columnName == "COMMENT")
            {
                bool VALUE1 = ITEM_TYPE();
                bool VALUE2 = PURITY();
                bool VALUE3 = GW();
                bool VALUE4 = NW();
                bool VALUE5 = LABOUR();
                bool VALUE6 = OTHER();
                bool VALUE12 = OTHER_LBR_CHECK();
                bool OTHER_LBR_CHECK()
                {
                    string LBR_AMT = (get_CellValue(rowIndex, "LBR_AMT"));
                    string OTH_AMT = (get_CellValue(rowIndex, "OTH_AMT"));
                    string LBR_RATE = (get_CellValue(rowIndex, "LBR_RATE"));
                    bool CHECK_IF_NULL_OR_0(string n)
                    {
                        return !(string.IsNullOrEmpty(n) || n == "0");
                    }

                    if (CHECK_IF_NULL_OR_0(LBR_AMT) || CHECK_IF_NULL_OR_0(OTH_AMT) || CHECK_IF_NULL_OR_0(LBR_RATE))
                    {
                        return true;  // At least one has a non-zero, non-null value
                    }
                    else
                    {
                        return false; // All are either null or "0"
                    }

                }
                bool VALUE7 = LABOUR_AMOUNT();
                if (GOLD_SILVER == "GOLD")
                {
                    bool VALUE8 = HUID1();
                    bool VALUE9 = HUID2();
                    bool VALUE10 = HUID3();
                    if (!VALUE8) { dg.CurrentCell = dg.Rows[rowIndex].Cells["HUID1"]; l.Text = "Error in HUID1 !"; return false; }
                    if (!VALUE9) { dg.CurrentCell = dg.Rows[rowIndex].Cells["HUID2"]; l.Text = "Error in HUID2 !"; return false; }
                    if (!VALUE10) { dg.CurrentCell = dg.Rows[rowIndex].Cells["HUID3"]; l.Text = "Error in HUID3 !"; return false; }
                }
                bool VALUE11 = PRICE();
                if (!VALUE1) { dg.CurrentCell = dg.Rows[rowIndex].Cells["ITEM_TYPE"]; l.Text = "Error in ITEM_TYPE !"; return false; }
                if (!VALUE2) { dg.CurrentCell = dg.Rows[rowIndex].Cells["PURITY"]; l.Text = "Error in PURITY !"; return false; }
                if (!VALUE3) { dg.CurrentCell = dg.Rows[rowIndex].Cells["GW"]; l.Text = "Error in GW !"; return false; }
                if (!VALUE4) { dg.CurrentCell = dg.Rows[rowIndex].Cells["NW"]; l.Text = "Error in NW !"; return false; }
                if (!VALUE5) { dg.CurrentCell = dg.Rows[rowIndex].Cells["LABOUR"]; l.Text = "Error in LABOUR !"; return false; }
                if (!VALUE6) { dg.CurrentCell = dg.Rows[rowIndex].Cells["OTHER"]; l.Text = "Error in OTHER !"; return false; }
                if (!VALUE12) { dg.CurrentCell = dg.Rows[rowIndex].Cells["LBR_RATE"]; l.Text = "Error in LABOUR !"; return false; }
                if (!VALUE7) { dg.CurrentCell = dg.Rows[rowIndex].Cells["LBR_AMT"]; l.Text = "Error in LABOUR_AMOUNT !"; return false; }
                if (!VALUE11) { dg.CurrentCell = dg.Rows[rowIndex].Cells["PRICE"]; l.Text = "Error in PRICE !"; return false; }
                l.Text = "";
                return true;
            }
            bool ITEM_TYPE()
            {
                if (!itemTypeCollection.Contains(get_CellValue(rowIndex, "ITEM_TYPE")))
                {
                    l.Text = "Error in Item Type !";
                    return false;
                }
                l.Text = "";
                return true;
            }
            bool PURITY()
            {
                string GW = get_CellValue(rowIndex, "GW");
                if (!purityCollection.Contains(get_CellValue(rowIndex, "PURITY")))
                {
                    l.Text = "Error in Purity !";
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(GW))
                    {
                        set_CellValue(rowIndex, "GW", "0");
                    }
                    l.Text = "";
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
                    l.Text = "Error in Gross Weight";
                    return false;
                }
                l.Text = "";
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
                Set_0_If_Null(rowIndex, "LBR_RATE");
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
                l.Text = "";
                return true;
            }
            bool OTHER()
            {
                Set_0_If_Null(rowIndex, "OTH_AMT");
                string NW = (get_CellValue(rowIndex, "NW"));
                string LBR_AMT = (get_CellValue(rowIndex, "LBR_AMT"));
                string OTH_AMT = (get_CellValue(rowIndex, "OTH_AMT"));
                string LBR_RATE = (get_CellValue(rowIndex, "LBR_RATE"));
                decimal NEW_PRICE = CustomRound((ConvertToDecimal(NW) * RATE) + ConvertToDecimal(LBR_AMT) + ConvertToDecimal(OTH_AMT), 1);
                string PRICE = get_CellValue(rowIndex, "PRICE");
                if (string.IsNullOrEmpty(get_CellValue(rowIndex, "PRICE")))
                {
                    set_CellValue(rowIndex, "PRICE", NEW_PRICE.ToString());
                }
                else if (ConvertToDecimal(PRICE) != (ConvertToDecimal(NW) * ConvertToDecimal(get_CellValue(rowIndex, "LBR_RATE"))))
                {
                    set_CellValue(rowIndex, "PRICE", NEW_PRICE.ToString());
                }
                l.Text = "";
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
                Set_0_If_Null(rowIndex, "OTH_AMT");
                l.Text = "";
                return true;
            }
            bool HUID1()
            {
                string huid1 = get_CellValue(rowIndex, "HUID1");
                string huid2 = get_CellValue(rowIndex, "HUID2");
                if (string.IsNullOrEmpty(huid1))
                {
                    if (!string.IsNullOrEmpty(huid2))
                    {
                        set_CellValue(rowIndex, "HUID1", huid2);
                        set_CellValue(rowIndex, "HUID2", "");
                    }
                    l.Text = "";
                    return true;
                }
                else if (huid1.Length == 6) { l.Text = ""; return true; }
                else { l.Text = "Error in HUID1 !"; return false; }
            }
            bool HUID2()
            {
                string huid1 = get_CellValue(rowIndex, "HUID1");
                string huid2 = get_CellValue(rowIndex, "HUID2");
                string huid3 = get_CellValue(rowIndex, "HUID3");
                if (string.IsNullOrEmpty(huid2))
                {
                    if (!string.IsNullOrEmpty(huid3))
                    {
                        if (string.IsNullOrEmpty(huid1))
                        {
                            set_CellValue(rowIndex, "HUID1", huid3);
                            set_CellValue(rowIndex, "HUID3", "");
                        }
                        else
                        {
                            set_CellValue(rowIndex, "HUID2", huid3);
                            set_CellValue(rowIndex, "HUID3", "");
                        }
                    }
                    l.Text = "";
                    return true;
                }
                else if (huid2.Length == 6)
                {
                    if (string.IsNullOrEmpty(huid1))
                    {
                        set_CellValue(rowIndex, "HUID1", huid2);
                        set_CellValue(rowIndex, "HUID2", "");
                    }
                    l.Text = "";
                    return true;
                }
                else { l.Text = "Error in HUID2 !"; return false; }
            }
            bool HUID3()
            {
                string huid1 = get_CellValue(rowIndex, "HUID1");
                string huid2 = get_CellValue(rowIndex, "HUID2");
                string huid3 = get_CellValue(rowIndex, "HUID3");
                if (string.IsNullOrEmpty(huid3))
                {
                    l.Text = "";
                    return true;
                }
                else if (huid3.Length == 6)
                {
                    if (string.IsNullOrEmpty(huid1))
                    {
                        set_CellValue(rowIndex, "HUID1", huid3);
                        set_CellValue(rowIndex, "HUID3", "");
                    }
                    else if (string.IsNullOrEmpty(huid2))
                    {
                        set_CellValue(rowIndex, "HUID2", huid3);
                        set_CellValue(rowIndex, "HUID3", "");
                    }
                    l.Text = "";
                    return true;
                }
                else { l.Text = "Error in HUID3 !"; return false; }
            }
            bool PRICE()
            {
                decimal PRICE = ConvertToDecimal(get_CellValue(rowIndex, "PRICE"));
                decimal NW = ConvertToDecimal(get_CellValue(rowIndex, "NW"));
                string LBR_RATE = get_CellValue(rowIndex, "LBR_RATE");
                string LBR_AMT = (get_CellValue(rowIndex, "LBR_AMT"));
                string OTH_AMT = (get_CellValue(rowIndex, "OTH_AMT"));
                decimal NEW_PRICE = CustomRound(((NW) * RATE) + ConvertToDecimal(LBR_AMT) + ConvertToDecimal(OTH_AMT), 1);
                if (PRICE != NEW_PRICE && change_Labour_On_Price_Change)
                {
                    int LBR_RATE_NEW = CustomRound(((PRICE - (RATE * NW)) / NW), 1);
                    set_CellValue(rowIndex, "LBR_RATE", LBR_RATE_NEW.ToString());
                    set_CellValue(rowIndex, "LBR_AMT", CustomRound((((NW)) * (ConvertToDecimal(LBR_RATE_NEW.ToString()))), 1).ToString());
                    set_CellValue(rowIndex, "OTH_AMT", "0");
                }
                l.Text = "";
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
                    return weight.ToString("0.000");
                }
                return (cellValue ?? "").ToString();
            }
            decimal ConvertToDecimal(string str)
            {
                if (decimal.TryParse(str, out decimal result))
                {
                    return Math.Round(result, 3);
                }
                else
                {
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
            l.Text = "";
            return true;
        }
    }
}
