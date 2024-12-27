using System.Data;
using System.Windows.Forms;
namespace SYA
{
    public  class SearchStyling
    {
        public void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e,DataGridView dataGridView1)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                DataGridViewRow gridViewRow = dataGridView1.Rows[i];
                gridViewRow.Height = 50;
                if (gridViewRow.DataBoundItem is DataRowView dataRowView)
                {
                    DataRow dataRow = dataRowView.Row;
                    if (dataRow["CO_BOOK"] != DBNull.Value)
                    {
                        string coBookValue = dataRow["CO_BOOK"].ToString();
                        if (coBookValue == "026" || coBookValue == "027")
                        {
                            gridViewRow.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                            gridViewRow.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (coBookValue == "015")
                        {
                            gridViewRow.Cells["CO_YEAR"].Style.BackColor = ColorTranslator.FromHtml("#212529");    // Eerie Black
                            gridViewRow.Cells["CO_BOOK"].Style.BackColor = ColorTranslator.FromHtml("#25292D");    // Gunmetal
                            gridViewRow.Cells["VCH_NO"].Style.BackColor = ColorTranslator.FromHtml("#2A2E32");     // Gunmetal-2
                            gridViewRow.Cells["VCH_DATE"].Style.BackColor = ColorTranslator.FromHtml("#2E3236");    // Onyx
                            gridViewRow.Cells["PURITY"].Style.BackColor = ColorTranslator.FromHtml("#33373A");       // Onyx-2
                            gridViewRow.Cells["METAL_TYPE"].Style.BackColor = ColorTranslator.FromHtml("#373B3F");    // Onyx-3
                            gridViewRow.Cells["TAG_NO"].Style.BackColor = ColorTranslator.FromHtml("#3C3F43");       // Onyx-4
                            gridViewRow.Cells["HUID1"].Style.BackColor = ColorTranslator.FromHtml("#404448");        // Outer Space
                            gridViewRow.Cells["HUID2"].Style.BackColor = ColorTranslator.FromHtml("#45484C");        // Outer Space-2
                            gridViewRow.Cells["HUID3"].Style.BackColor = ColorTranslator.FromHtml("#494D50");        // Outer Space-3
                            gridViewRow.Cells["ITEM_TYPE"].Style.BackColor = ColorTranslator.FromHtml("#4E5155");     // Davy's Gray
                            gridViewRow.Cells["GW"].Style.BackColor = ColorTranslator.FromHtml("#525659");           // Davy's Gray-2
                            gridViewRow.Cells["NW"].Style.BackColor = ColorTranslator.FromHtml("#565A5D");           // Davy's Gray-3
                            gridViewRow.Cells["LBR_RATE"].Style.BackColor = ColorTranslator.FromHtml("#5B5E62");      // Davy's Gray-4
                            gridViewRow.Cells["OTH_AMT"].Style.BackColor = ColorTranslator.FromHtml("#5F6366");       // Dim Gray
                            gridViewRow.Cells["LBR_AMT"].Style.BackColor = ColorTranslator.FromHtml("#64676A");       // Dim Gray-2
                            gridViewRow.Cells["SIZE"].Style.BackColor = ColorTranslator.FromHtml("#686C6F");          // Dim Gray-3
                            gridViewRow.Cells["PRICE"].Style.BackColor = ColorTranslator.FromHtml("#6D7073");         // Dim Gray-4
                            gridViewRow.Cells["COMMENT"].Style.BackColor = ColorTranslator.FromHtml("#717478");        // Dim Gray-5
                            gridViewRow.Cells["ITM_RAT"].Style.BackColor = ColorTranslator.FromHtml("#76797C");        // Gray
                            gridViewRow.Cells["ITM_AMT"].Style.BackColor = ColorTranslator.FromHtml("#7A7D80");        // Gray-2
                            gridViewRow.Cells["AC_CODE"].Style.BackColor = ColorTranslator.FromHtml("#7F8285");        // Gray-3
                            gridViewRow.Cells["AC_NAME"].Style.BackColor = ColorTranslator.FromHtml("#838689");        // Gray-4

                            // Apply white font color for all cells in the row
                            foreach (DataGridViewCell cell in gridViewRow.Cells)
                            {
                                cell.Style.ForeColor = Color.White;
                            }
                            //if (dataRow["METAL_TYPE"] != DBNull.Value)
                            //{
                            //    string metalType = dataRow["METAL_TYPE"].ToString().ToUpper();
                            //    if (metalType == "G")
                            //    {
                            //        gridViewRow.DefaultCellStyle.BackColor = Color.Gold;
                            //    }
                            //    else if (metalType == "S")
                            //    {
                            //        gridViewRow.DefaultCellStyle.BackColor = Color.Silver;
                            //    }
                            //}
                        }
                    }
                    if (gridViewRow.DefaultCellStyle.BackColor == Color.Empty)
                    {
                        gridViewRow.DefaultCellStyle.BackColor = i % 2 == 0 ? Color.LightGray : Color.White;
                    }
                }
            }
        }
        public void CustomizeDataGridView(DataGridView dataGridView1)
        {
            // Hide specific columns
            string[] columnsToHide = { "ID", "DESIGN", "ITM_SIZE", "ITM_PCS" };
            foreach (string columnName in columnsToHide)
            {
                if (dataGridView1.Columns.Contains(columnName))
                {
                    dataGridView1.Columns[columnName].Visible = false;
                }
            }
            // Set row height
            dataGridView1.RowTemplate.Height = 50;
            // Configure text alignment and font size
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, 15);
            // Adjust column widths
            AdjustColumnWidths(dataGridView1);
        }
        public void AdjustColumnWidths(DataGridView dataGridView1)
        {
            // Set auto-sizing for all columns initially
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // Manually override specific column widths
            if (dataGridView1.Columns.Contains("METAL_TYPE"))
            {
                dataGridView1.Columns["METAL_TYPE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["METAL_TYPE"].Width = 50;
            }
            if (dataGridView1.Columns.Contains("PURITY"))
            {
                dataGridView1.Columns["PURITY"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["PURITY"].Width = 50;
            }
            if (dataGridView1.Columns.Contains("ITEM_TYPE"))
            {
                dataGridView1.Columns["ITEM_TYPE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["ITEM_TYPE"].Width = 50;
            }
        }
    }
}
