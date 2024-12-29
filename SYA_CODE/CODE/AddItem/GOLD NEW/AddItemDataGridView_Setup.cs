using System.Data;
using System.Windows.Forms;
namespace SYA
{
    public class AddItemDataGridView_Setup
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
                            gridViewRow.Cells["PURITY"].Style.BackColor = ColorTranslator.FromHtml("#96B5FE");     // Jordy Blue-4
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
                            foreach (DataGridViewCell cell in gridViewRow.Cells)
                            {
                                cell.Style.ForeColor = Color.Black;
                            }
                        }
                //        else if (coBookValue == "026" || coBookValue == "027")
                //        {
                //            gridViewRow.Cells["CO_YEAR"].Style.BackColor = ColorTranslator.FromHtml("#212529");    // Eerie Black
                //            gridViewRow.Cells["CO_BOOK"].Style.BackColor = ColorTranslator.FromHtml("#25292D");    // Gunmetal
                //            gridViewRow.Cells["VCH_NO"].Style.BackColor = ColorTranslator.FromHtml("#2A2E32");     // Gunmetal-2
                //            gridViewRow.Cells["VCH_DATE"].Style.BackColor = ColorTranslator.FromHtml("#2E3236");    // Onyx
                //            gridViewRow.Cells["PURITY"].Style.BackColor = ColorTranslator.FromHtml("#33373A");       // Onyx-2
                //            gridViewRow.Cells["METAL_TYPE"].Style.BackColor = ColorTranslator.FromHtml("#373B3F");    // Onyx-3
                //            gridViewRow.Cells["TAG_NO"].Style.BackColor = ColorTranslator.FromHtml("#3C3F43");       // Onyx-4
                //            gridViewRow.Cells["HUID1"].Style.BackColor = ColorTranslator.FromHtml("#404448");        // Outer Space
                //            gridViewRow.Cells["HUID2"].Style.BackColor = ColorTranslator.FromHtml("#45484C");        // Outer Space-2
                //            gridViewRow.Cells["HUID3"].Style.BackColor = ColorTranslator.FromHtml("#494D50");        // Outer Space-3
                //            gridViewRow.Cells["ITEM_TYPE"].Style.BackColor = ColorTranslator.FromHtml("#4E5155");     // Davy's Gray
                //            gridViewRow.Cells["GW"].Style.BackColor = ColorTranslator.FromHtml("#525659");           // Davy's Gray-2
                //            gridViewRow.Cells["NW"].Style.BackColor = ColorTranslator.FromHtml("#565A5D");           // Davy's Gray-3
                //            gridViewRow.Cells["LBR_RATE"].Style.BackColor = ColorTranslator.FromHtml("#5B5E62");      // Davy's Gray-4
                //            gridViewRow.Cells["OTH_AMT"].Style.BackColor = ColorTranslator.FromHtml("#5F6366");       // Dim Gray
                //            gridViewRow.Cells["LBR_AMT"].Style.BackColor = ColorTranslator.FromHtml("#64676A");       // Dim Gray-2
                //            gridViewRow.Cells["SIZE"].Style.BackColor = ColorTranslator.FromHtml("#686C6F");          // Dim Gray-3
                //            gridViewRow.Cells["PRICE"].Style.BackColor = ColorTranslator.FromHtml("#6D7073");         // Dim Gray-4
                //            gridViewRow.Cells["COMMENT"].Style.BackColor = ColorTranslator.FromHtml("#717478");        // Dim Gray-5
                //            gridViewRow.Cells["ITM_RAT"].Style.BackColor = ColorTranslator.FromHtml("#76797C");        // Gray
                //            gridViewRow.Cells["ITM_AMT"].Style.BackColor = ColorTranslator.FromHtml("#7A7D80");        // Gray-2
                //            gridViewRow.Cells["AC_CODE"].Style.BackColor = ColorTranslator.FromHtml("#7F8285");        // Gray-3
                //            gridViewRow.Cells["AC_NAME"].Style.BackColor = ColorTranslator.FromHtml("#838689");        // Gray-4
                //            // Apply white font color for all cells in the row
                //            foreach (DataGridViewCell cell in gridViewRow.Cells)
                //            {
                //                cell.Style.ForeColor = Color.White;
                //            }
                //            //if (dataRow["METAL_TYPE"] != DBNull.Value)
                //            //{
                //            //    string metalType = dataRow["METAL_TYPE"].ToString().ToUpper();
                //            //    if (metalType == "G")
                //            //    {
                //            //        gridViewRow.DefaultCellStyle.BackColor = Color.Gold;
                //            //    }
                //            //    else if (metalType == "S")
                //            //    {
                //            //        gridViewRow.DefaultCellStyle.BackColor = Color.Silver;
                //            //    }
                //            //}
                //    if (gridViewRow.DefaultCellStyle.BackColor == Color.Empty)
                //    {
                //        gridViewRow.DefaultCellStyle.BackColor = i % 2 == 0 ? Color.LightGray : Color.White;
                //    }
                //}
            }
        }
        public void SetColumnStyles(DataGridView dataGridView1)
        {
            // Set styles for each column by column name (for non-ComboBox columns)
            dataGridView1.Columns["TAG_NO"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#96B5FE"); // Jordy Blue-4
            // Set background color for ComboBox columns (non-edited cells) when DataGridView is first initialized
            dataGridView1.Columns["ITEM_TYPE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#9EBBFE"); // Jordy Blue-6
            dataGridView1.Columns["PURITY"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#A3BEFE"); // Jordy Blue-7
            dataGridView1.Columns["GW"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#A7C1FE"); // Jordy Blue-8
            dataGridView1.Columns["NW"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#ABC4FE"); // Jordy Blue-9
            dataGridView1.Columns["LBR_RATE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#AFC7FE"); // Jordy Blue-10
            dataGridView1.Columns["OTH_AMT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#B4CAFE"); // Periwinkle
            dataGridView1.Columns["LBR_AMT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#B8CCFD"); // Periwinkle-2
            dataGridView1.Columns["HUID1"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#BCCFFD"); // Periwinkle-3
            dataGridView1.Columns["HUID2"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C0D2FD"); // Periwinkle-4
            dataGridView1.Columns["HUID3"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C4D5FD"); // Periwinkle-5
            dataGridView1.Columns["SIZE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C9D8FD"); // Periwinkle-6
            dataGridView1.Columns["PRICE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#CDDBFD"); // Periwinkle-7
            dataGridView1.Columns["COMMENT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1DEFD"); // Lavender Web
            // Set ForeColor for all columns
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.ForeColor = Color.Black;
            }
            // Apply background color for ComboBox cells in the first row (if any)
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Apply background color for ITEM_TYPE ComboBox cells
                row.Cells["ITEM_TYPE"].Style.BackColor = ColorTranslator.FromHtml("#9EBBFE"); // Jordy Blue-6
                // Apply background color for PURITY ComboBox cells
                row.Cells["PURITY"].Style.BackColor = ColorTranslator.FromHtml("#A3BEFE"); // Jordy Blue-7
            }
        }
        public void CustomizeDataGridView(DataGridView dataGridView1)
        {
            // Hide specific columns
            //string[] columnsToHide = { "ID", "DESIGN", "ITM_SIZE", "ITM_PCS" };
            //foreach (string columnName in columnsToHide)
            //{
            //    if (dataGridView1.Columns.Contains(columnName))
            //    {
            //        dataGridView1.Columns[columnName].Visible = false;
            //    }
            //}
            // Set row height
            SetColumnStyles(dataGridView1);
            dataGridView1.RowTemplate.Height = 50;
            // Configure text alignment and font size
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, 20);
            dataGridView1.RowHeadersVisible = false;
            // Adjust column widths
            AdjustColumnWidths(dataGridView1);
            AdjustColumnHeaderStyles(dataGridView1);
        }
        public void AdjustColumnHeaderStyles(DataGridView dataGridView1)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 13, FontStyle.Bold); // Set font size and style
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Set alignment to middle-center
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Set font color
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black; // Set background color
            dataGridView1.EnableHeadersVisualStyles = false; // Ensure cust
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Enable manual height resizing
            dataGridView1.ColumnHeadersHeight = 50; // Set header}
        }
        public void AdjustColumnWidths(DataGridView dataGridView1)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            if (dataGridView1.Columns.Contains("COMMENT"))
            {
                dataGridView1.Columns["COMMENT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
