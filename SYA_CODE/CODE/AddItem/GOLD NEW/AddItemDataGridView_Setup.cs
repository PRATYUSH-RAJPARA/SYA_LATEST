using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
namespace SYA
{
    public class AddItemDataGridView_Setup
    {
       
        public void InitializeAutoCompleteCollections(AutoCompleteStringCollection itemTypeCollection, AutoCompleteStringCollection purityCollection)
        {
            LoadAutoCompleteValues("G", "IT_NAME", "IT_NAME", itemTypeCollection);
            LoadAutoCompleteValues("GQ", "IT_NAME", "IT_NAME", purityCollection);
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
            // ComboBox column
            DataGridViewComboBoxColumn CreateComboBoxColumn(string name, string headerText)
            {
                return new DataGridViewComboBoxColumn
                {
                    Name = name,
                    HeaderText = headerText
                };
            }
         //   InitializeComboBoxColumns();
            CustomizeDataGridView(dataGridView1);
            void InitializeComboBoxColumns()
            {
                LoadComboBoxValues("G", "IT_NAME", "IT_NAME", (DataGridViewComboBoxColumn)dataGridView1.Columns["ITEM_TYPE"]);
                LoadComboBoxValues("GQ", "IT_NAME", "IT_NAME", (DataGridViewComboBoxColumn)dataGridView1.Columns["PURITY"]);
                void LoadComboBoxValues(string itemType, string columnName, string displayMember, DataGridViewComboBoxColumn comboBoxColumn)
                {
                    using (SQLiteDataReader reader = helper.FetchDataFromSYADataBase($"SELECT DISTINCT {columnName} FROM ITEM_MASTER WHERE IT_TYPE = '{itemType}'"))
                    {
                        while (reader.Read())
                        {
                            comboBoxColumn.Items.Add(reader[displayMember].ToString());
                        }
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
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#96B5FE");
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, (float)15);
            dataGridView1.RowHeadersVisible = false;
            AdjustColumnHeaderStyles(dataGridView1);
        }
        public void SetColumnStyles(DataGridView dataGridView1)
        {
            dataGridView1.Columns["TAG_NO"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#96B5FE"); // Jordy Blue-4
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
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        public void AdjustColumnHeaderStyles(DataGridView dataGridView1)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 13, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
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
