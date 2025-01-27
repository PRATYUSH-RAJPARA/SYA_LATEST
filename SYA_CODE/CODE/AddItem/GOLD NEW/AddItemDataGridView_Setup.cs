using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
namespace SYA
{
    public class AddItemDataGridView_Setup
    {
        public void InitializeDataGridView(DataGridView dataGridView1)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(CreateTextBoxColumn("TAG_NO", "Tag No"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("ITEM_TYPE", "Item Type"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("PURITY", "Purity"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("GW", "Gross Weight"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("NW", "Net Weight"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("LBR_RATE", "Labour"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("LBR_AMT", "Labour Amount"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("OTH_AMT", "Other"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("HUID1", "HUID1"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("HUID2", "HUID2"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("HUID3", "HUID3"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("SIZE", "Size"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("PRICE", "Price"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("COMMENT", "Comment"));
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewTextBoxColumn CreateTextBoxColumn(string name, string headerText)
            {
                return new DataGridViewTextBoxColumn
                {
                    Name = name,
                    HeaderText = headerText
                };
            }
            CustomizeDataGridView(dataGridView1);
        }
        public void InitializeAutoCompleteCollections(AutoCompleteStringCollection itemTypeCollection, AutoCompleteStringCollection purityCollection,String GOLD_SILVER)
        {
            if (GOLD_SILVER == "GOLD")
            {
                LoadAutoCompleteValues("G", "IT_NAME", "IT_NAME", itemTypeCollection);
                LoadAutoCompleteValues("GQ", "IT_NAME", "IT_NAME", purityCollection);
            }
            else if (GOLD_SILVER == "SILVER")
            {
                LoadAutoCompleteValues("S", "IT_NAME", "IT_NAME", itemTypeCollection);
                LoadAutoCompleteValues("SQ", "IT_NAME", "IT_NAME", purityCollection);
            }
           
            void LoadAutoCompleteValues(string itemType, string columnName, string displayMember, AutoCompleteStringCollection collection)
            {
                using (SQLiteDataReader reader = helper.FetchDataFromSYADataBase($"SELECT DISTINCT {columnName} FROM ITEM_MASTER WHERE IT_TYPE = '{itemType}'"))
                {
                    while (reader.Read())
                    {
                        collection.Add(reader[displayMember].ToString());
                    }
                }
            }
        }
        public void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e, DataGridView dataGridView1)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                DataGridViewRow gridViewRow = dataGridView1.Rows[i];
                gridViewRow.Height = 35;
            }
        }
        public void CustomizeDataGridView(DataGridView dataGridView1)
        {
            SetColumnStyles(dataGridView1);
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#E9CAC3");
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, (float)15, FontStyle.Bold);
            dataGridView1.RowHeadersVisible = false;
            AdjustColumnHeaderStyles(dataGridView1);
        }
        public void SetColumnStyles(DataGridView dataGridView1)
        {
            dataGridView1.DefaultCellStyle.BackColor=ColorTranslator.FromHtml("#F4E1D9");
           dataGridView1.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#9E2B25");
        }
        public void AdjustColumnHeaderStyles(DataGridView dataGridView1)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 13, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#F4E1D9"); 
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#A9423C");
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 50;
        }
        public void AdjustColumnWidths(DataGridView dataGridView)
        {
            if (dataGridView == null || dataGridView.Columns.Count == 0)
                return;
            int totalWidth = dataGridView.ClientSize.Width;
            dataGridView.Columns["TAG_NO"].Width = (int)(totalWidth * 0.125);        // 5%
            dataGridView.Columns["ITEM_TYPE"].Width = (int)(totalWidth * 0.135);     // 10%
            dataGridView.Columns["PURITY"].Width = (int)(totalWidth * 0.06);        // 10%
            dataGridView.Columns["GW"].Width = (int)(totalWidth * 0.06);            // 8%
            dataGridView.Columns["NW"].Width = (int)(totalWidth * 0.06);            // 8%
            dataGridView.Columns["LBR_RATE"].Width = (int)(totalWidth * 0.06);      // 8%
            dataGridView.Columns["LBR_AMT"].Width = (int)(totalWidth * 0.06);       // 8%
            dataGridView.Columns["OTH_AMT"].Width = (int)(totalWidth * 0.05);       // 7%
            dataGridView.Columns["HUID1"].Width = (int)(totalWidth * 0.07);         // 5%
            dataGridView.Columns["HUID2"].Width = (int)(totalWidth * 0.07);         // 5%
            dataGridView.Columns["HUID3"].Width = (int)(totalWidth * 0.07);         // 5%
            dataGridView.Columns["SIZE"].Width = (int)(totalWidth * 0.04);          // 5%
            dataGridView.Columns["PRICE"].Width = (int)(totalWidth * 0.05);         // 10%
            dataGridView.Columns["COMMENT"].Width = (int)(totalWidth * 0.09);
        }
    }
}
