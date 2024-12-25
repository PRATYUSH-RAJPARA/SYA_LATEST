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
                            if (dataRow["METAL_TYPE"] != DBNull.Value)
                            {
                                string metalType = dataRow["METAL_TYPE"].ToString().ToUpper();
                                if (metalType == "G")
                                {
                                    gridViewRow.DefaultCellStyle.BackColor = Color.Gold;
                                }
                                else if (metalType == "S")
                                {
                                    gridViewRow.DefaultCellStyle.BackColor = Color.Silver;
                                }
                            }
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
