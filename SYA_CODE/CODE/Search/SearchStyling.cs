using System.Data;
using System.Windows.Forms;
namespace SYA
{
    public class SearchStyling
    {
        public void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e, DataGridView dataGridView1)
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
                        if (coBookValue == "015")
                        {
                            gridViewRow.Cells["CO_YEAR"].Style.BackColor = ColorTranslator.FromHtml("#85A9FF");    // Vista Blue
                            gridViewRow.Cells["CO_BOOK"].Style.BackColor = ColorTranslator.FromHtml("#89ACFF");    // Jordy Blue
                            gridViewRow.Cells["VCH_NO"].Style.BackColor = ColorTranslator.FromHtml("#8DAFFF");     // Jordy Blue-2
                            gridViewRow.Cells["VCH_DATE"].Style.BackColor = ColorTranslator.FromHtml("#92B2FF");   // Jordy Blue-3
                            gridViewRow.Cells["PURITY"].Style.BackColor = ColorTranslator.FromHtml("#96B5FE");     // Jordy Blue-4
                            gridViewRow.Cells["METAL_TYPE"].Style.BackColor = ColorTranslator.FromHtml("#9AB8FE"); // Jordy Blue-5
                            gridViewRow.Cells["TAG_NO"].Style.BackColor = ColorTranslator.FromHtml("#9EBBFE");     // Jordy Blue-6
                            gridViewRow.Cells["HUID1"].Style.BackColor = ColorTranslator.FromHtml("#A3BEFE");      // Jordy Blue-7
                            gridViewRow.Cells["HUID2"].Style.BackColor = ColorTranslator.FromHtml("#A7C1FE");      // Jordy Blue-8
                            gridViewRow.Cells["HUID3"].Style.BackColor = ColorTranslator.FromHtml("#ABC4FE");      // Jordy Blue-9
                            gridViewRow.Cells["ITEM_TYPE"].Style.BackColor = ColorTranslator.FromHtml("#AFC7FE");  // Jordy Blue-10
                            gridViewRow.Cells["GW"].Style.BackColor = ColorTranslator.FromHtml("#B4CAFE");         // Periwinkle
                            gridViewRow.Cells["NW"].Style.BackColor = ColorTranslator.FromHtml("#B8CCFD");         // Periwinkle-2
                            gridViewRow.Cells["LBR_RATE"].Style.BackColor = ColorTranslator.FromHtml("#BCCFFD");   // Periwinkle-3
                            gridViewRow.Cells["OTH_AMT"].Style.BackColor = ColorTranslator.FromHtml("#C0D2FD");    // Periwinkle-4
                            gridViewRow.Cells["LBR_AMT"].Style.BackColor = ColorTranslator.FromHtml("#C4D5FD");    // Periwinkle-5
                            gridViewRow.Cells["SIZE"].Style.BackColor = ColorTranslator.FromHtml("#C9D8FD");       // Periwinkle-6
                            gridViewRow.Cells["PRICE"].Style.BackColor = ColorTranslator.FromHtml("#CDDBFD");      // Periwinkle-7
                            gridViewRow.Cells["COMMENT"].Style.BackColor = ColorTranslator.FromHtml("#D1DEFD");    // Lavender Web
                            gridViewRow.Cells["ITM_RAT"].Style.BackColor = ColorTranslator.FromHtml("#D5E1FC");    // Lavender Web-2
                            gridViewRow.Cells["ITM_AMT"].Style.BackColor = ColorTranslator.FromHtml("#DAE4FC");    // Lavender Web-3
                            gridViewRow.Cells["AC_CODE"].Style.BackColor = ColorTranslator.FromHtml("#DEE7FC");    // Lavender Web-4
                            gridViewRow.Cells["AC_NAME"].Style.BackColor = ColorTranslator.FromHtml("#E2EAFC");    // Lavender Web-5
                            foreach (DataGridViewCell cell in gridViewRow.Cells)
                            {
                                cell.Style.ForeColor = Color.Black;
                            }
                        }
                        else if (coBookValue == "026" || coBookValue == "027")
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
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, 12);
            dataGridView1.RowHeadersVisible = false;
            // Adjust column widths
            AdjustColumnWidths(dataGridView1);
            AdjustColumnHeaderStyles(dataGridView1);
        }
        public void AdjustColumnHeaderStyles(DataGridView dataGridView1)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); // Set font size and style
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Set alignment to middle-center
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Set font color
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black; // Set background color
            dataGridView1.EnableHeadersVisualStyles = false; // Ensure cust
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Enable manual height resizing
            dataGridView1.ColumnHeadersHeight = 35; // Set header}
            dataGridView1.Columns["LBR_RATE"].HeaderText = "LBR";
            dataGridView1.Columns["OTH_AMT"].HeaderText = "OTH";
            dataGridView1.Columns["LBR_AMT"].HeaderText = "LBR\nAMT";
            dataGridView1.Columns["TAG_NO"].HeaderText = "TAG\nNO";
            dataGridView1.Columns["VCH_DATE"].HeaderText = "DATE";
            dataGridView1.Columns["VCH_NO"].HeaderText = "VCH\nNO";
            dataGridView1.Columns["CO_YEAR"].HeaderText = "CO\nYEAR";
            dataGridView1.Columns["ITM_RAT"].HeaderText = "ITM\nRAT";
            dataGridView1.Columns["ITM_AMT"].HeaderText = "ITM\nAMT";
            dataGridView1.Columns["AC_CODE"].HeaderText = "AC\nCODE";
            dataGridView1.Columns["CO_BOOK"].HeaderText = "";
            dataGridView1.Columns["METAL_TYPE"].HeaderText = "";
            dataGridView1.Columns["PURITY"].HeaderText = "";
            dataGridView1.Columns["ITEM_TYPE"].HeaderText = "";
        }
        public void AdjustColumnWidths(DataGridView dataGridView1)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                if (dataGridView1.Columns.Contains("COMMENT"))
                {
                    dataGridView1.Columns["COMMENT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  
                }
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); // Set font size and style
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Set font color
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black; // Set background color
            dataGridView1.EnableHeadersVisualStyles = false; // Ensure cust
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Enable manual height resizing
            dataGridView1.ColumnHeadersHeight = 50; // Set header
        }
    }
}
