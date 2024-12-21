using Serilog;
using System.Data;
using System.Data.SQLite;
using System.Drawing.Printing;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;
namespace SYA
{
    public partial class addgold1 : Form
    {
        addGoldHelper addGoldHelper = new addGoldHelper();
        private const int ItemNameColumnIndex = 2;
        bool quickSave = false;
        bool quickSaveAndPrint = true;
        public Labour objLabour = new Labour();
        private DataGridViewNavigationHelper navigationHelper;
        public addgold1()
        {
            InitializeComponent();
            navigationHelper = new DataGridViewNavigationHelper(dataGridView1);
            dataGridView1.AutoGenerateColumns = false;
            gridviewstyle();
            DataGridViewTextBoxColumn textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.HeaderText = "PR_CODE";
            textBoxColumn.Name = "prcode";
            dataGridView1.Columns.Add(textBoxColumn);
            dataGridView1.Columns["prcode"].Visible = false;
            InitializeComboBoxColumns();
            dataGridView1.DataSource = GetEmptyDataTable();
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.RowsAdded  += (s, args) => UpdateRowNumbers();
            dataGridView1.RowsRemoved += (s, args) => UpdateRowNumbers();
        }
        private void addgold_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;


            UpdateRowNumbers();
        }
        private void addgold_SizeChanged(object sender, EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int ww = (int)(formWidth * 0.008);
            dataGridView1.Columns["select"].Width = 0; dataGridView1.Columns["tagno"].Width = (int)(ww * 16); dataGridView1.Columns["itemName"].Width = (int)(ww * 18); dataGridView1.Columns["purity"].Width = (int)(ww * 6); dataGridView1.Columns["gross"].Width = (int)(ww * 8); dataGridView1.Columns["net"].Width = (int)(ww * 8); dataGridView1.Columns["labour"].Width = (int)(ww * 10); dataGridView1.Columns["labourAmount"].Width = (int)(ww * 10); dataGridView1.Columns["other"].Width = (int)(ww * 10); dataGridView1.Columns["huid1"].Width = (int)(ww * 10); dataGridView1.Columns["huid2"].Width = (int)(ww * 10); dataGridView1.Columns["size"].Width = (int)(ww * 6); dataGridView1.Columns["comment"].Width = (int)(ww * 8);
            if ((float)(formWidth * 0.0066) > 12.5)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle.Font = new Font("Arial", (float)(formWidth * 0.0066), FontStyle.Regular);
                }
            }
            else
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.DefaultCellStyle.Font = new Font("Arial", (float)12.5, FontStyle.Regular);
                }
            }
        }
        private void InitializeComboBoxColumns()
        {
            LoadComboBoxValues("G", "IT_NAME", "IT_NAME", (DataGridViewComboBoxColumn)dataGridView1.Columns["itemName"]);
            LoadComboBoxValues("GQ", "IT_NAME", "IT_NAME", (DataGridViewComboBoxColumn)dataGridView1.Columns["purity"]);
        }
        private void messageBoxTimer_Tick(object sender, EventArgs e)
        {
            txtMessageBox.Text = string.Empty;
            messageBoxTimer.Stop();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Check if the current cell is in edit mode
            if (e.Control is TextBox textBox)
            {
                // Remove existing event handlers to avoid duplication
                textBox.KeyPress -= TextBox_KeyPress;

                // Attach the KeyPress event for live validation
                textBox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Get the current cell
            var currentCell = dataGridView1.CurrentCell;

            // Check if the cell belongs to one of the numeric columns
            if (currentCell != null &&
                (currentCell.OwningColumn.Name == "gross" ||
                 currentCell.OwningColumn.Name == "net" ||
                 currentCell.OwningColumn.Name == "labour" ||
                 currentCell.OwningColumn.Name == "labourAmount" ||
                 currentCell.OwningColumn.Name == "other"))
            {
                // Allow digits, control characters (e.g., backspace), and one decimal point
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true; // Discard the keypress
                }
                else
                {
                    // Prevent multiple decimal points
                    var textBox = sender as TextBox;
                    if (e.KeyChar == '.' && textBox != null && textBox.Text.Contains("."))
                    {
                        e.Handled = true;
                    }
                }
            }
        }



        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Get the edited cell value
            var editedValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            // Display the value in a message box
            MessageBox.Show($"Edited value is: {editedValue}", "Edit Committed");

            // (Optional) Change another cell value in the same row
            // For example, update the cell in column 1 with some new value
            if (e.ColumnIndex != 1) // Avoid editing the cell itself
            {
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = $"Updated based on {editedValue}";
            }
        }
        
        private void SelectCell(DataGridView dataGridView, int rowIndex, string columnName)
        {
            dataGridView.CurrentCell = dataGridView.Rows[rowIndex].Cells[columnName];
            dataGridView.BeginEdit(true);
        }
        private void gridviewstyle()
        {
            dataGridView1.Columns["select"].Visible = false;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(114, 131, 89); dataGridView1.Columns["select"].HeaderCell.Style.BackColor = Color.FromArgb(151, 169, 124);
            dataGridView1.Columns["tagno"].HeaderCell.Style.BackColor = Color.FromArgb(166, 185, 139);
            dataGridView1.Columns["itemName"].HeaderCell.Style.BackColor = Color.FromArgb(181, 201, 154);
            dataGridView1.Columns["purity"].HeaderCell.Style.BackColor = Color.FromArgb(194, 213, 170);
            dataGridView1.Columns["gross"].HeaderCell.Style.BackColor = Color.FromArgb(207, 225, 185);
            dataGridView1.Columns["net"].HeaderCell.Style.BackColor = Color.FromArgb(220, 235, 202);
            dataGridView1.Columns["labour"].HeaderCell.Style.BackColor = Color.FromArgb(233, 245, 219);
            dataGridView1.Columns["labourAmount"].HeaderCell.Style.BackColor = Color.FromArgb(220, 235, 202);
            dataGridView1.Columns["other"].HeaderCell.Style.BackColor = Color.FromArgb(207, 225, 185);
            dataGridView1.Columns["huid1"].HeaderCell.Style.BackColor = Color.FromArgb(194, 213, 170);
            dataGridView1.Columns["huid2"].HeaderCell.Style.BackColor = Color.FromArgb(181, 201, 154);
            dataGridView1.Columns["size"].HeaderCell.Style.BackColor = Color.FromArgb(166, 185, 139);
            dataGridView1.Columns["comment"].HeaderCell.Style.BackColor = Color.FromArgb(151, 169, 124); foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Font = new Font("Arial", (float)12.5); if (column.Name == "select")
                {
                    column.Width = 40;
                }
                else if (column.Name == "itemName")
                {
                    column.Width = 225;
                }
                else if (column.Name == "tagno")
                {
                    column.Width = 200;
                }
                else if (column.Name == "purity")
                {
                    column.Width = 75;
                }
                else if (column.Name == "gross")
                {
                    column.Width = 100;
                }
                else if (column.Name == "net")
                {
                    column.Width = 100;
                }
                else if (column.Name == "size")
                {
                    column.Width = 80;
                }
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Font = new Font("Arial", (float)12.5, FontStyle.Bold);
            }
        }
        private void UpdateRowNumbers()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = $"{row.Index + 1}";
            }
        }
        private void LoadComboBoxValues(string itemType, string columnName, string displayMember, DataGridViewComboBoxColumn comboBoxColumn)
        {
            using (SQLiteDataReader reader = helper.FetchDataFromSYADataBase($"SELECT DISTINCT {columnName} FROM ITEM_MASTER WHERE IT_TYPE = '{itemType}'"))
            {
                while (reader.Read())
                {
                    comboBoxColumn.Items.Add(reader[displayMember].ToString());
                }
            }
        }
        private DataTable GetEmptyDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("select", typeof(bool));
            dataTable.Columns.Add("tagno", typeof(string));
            dataTable.Columns.Add("itemName", typeof(string));
            dataTable.Columns.Add("purity", typeof(string));
            dataTable.Columns.Add("gross", typeof(decimal));
            dataTable.Columns.Add("net", typeof(decimal));
            dataTable.Columns.Add("labour", typeof(decimal));
            dataTable.Columns.Add("other", typeof(decimal));
            dataTable.Columns.Add("huid1", typeof(string));
            dataTable.Columns.Add("huid2", typeof(string));
            dataTable.Columns.Add("size", typeof(string));
            dataTable.Columns.Add("comment", typeof(string));
            dataTable.Columns.Add("prcode", typeof(string));
            return dataTable;
        }
        private bool SaveData(DataGridViewRow selectedRow, int check)
        {
            if (check == 0)
            {
                try
                {
                    if (dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("DataGridView is empty. Check your data population logic.");
                        return false;
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if (row.Cells["tagno"].Value != null && !string.IsNullOrEmpty(row.Cells["tagno"].Value.ToString()) && row.Cells["tagno"].Value.ToString() != "0")
                            {
                                if (UpdateData(row))
                                {
                                    row.Cells["itemName"].ReadOnly = true;
                                    row.Cells["purity"].ReadOnly = true;
                                    txtMessageBox.Text = "Data Updated Successfully for " + row.Cells["tagno"].Value.ToString() + ".";
                                    messageBoxTimer.Start();
                                }
                            }
                            else
                            {
                                if (InsertData(row))
                                {
                                    row.Cells["itemName"].ReadOnly = true;
                                    row.Cells["purity"].ReadOnly = true;
                                    txtMessageBox.Text = "Data Added Successfully for " + row.Cells["tagno"].Value.ToString() + ".";
                                    messageBoxTimer.Start();
                                }
                            }
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else if (check == 1)
            {
                try
                {
                    if (selectedRow.Cells["tagno"].Value != null && !string.IsNullOrEmpty(selectedRow.Cells["tagno"].Value.ToString()) && selectedRow.Cells["tagno"].Value.ToString() != "0")
                    {
                        if (UpdateData(selectedRow))
                        {
                            selectedRow.Cells["itemName"].ReadOnly = true;
                            selectedRow.Cells["purity"].ReadOnly = true;
                            txtMessageBox.Text = "Data Updated Successfully for " + selectedRow.Cells["tagno"].Value.ToString() + ".";
                            messageBoxTimer.Start();
                        }
                    }
                    else
                    {
                        if (InsertData(selectedRow))
                        {
                            selectedRow.Cells["itemName"].ReadOnly = true;
                            selectedRow.Cells["purity"].ReadOnly = true;
                            txtMessageBox.Text = "Data Added Successfully for " + selectedRow.Cells["tagno"].Value.ToString() + ".";
                            messageBoxTimer.Start();
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }
        private void UpdateTagNo(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
            {
                if (dataGridView1.Columns.Count > ItemNameColumnIndex)
                {
                    object itemNameObject = dataGridView1.Rows[rowIndex].Cells[ItemNameColumnIndex].Value;
                    if (itemNameObject != null)
                    {
                        string itemName = itemNameObject.ToString();
                        string caret = dataGridView1.Rows[rowIndex].Cells["purity"].Value?.ToString();
                        string prCode = addGoldHelper.GetPRCode(itemName);
                        string prefix = "SYA";
                        int newSequenceNumber = addGoldHelper.GetNextSequenceNumber(prefix, prCode, caret);
                        if (!string.IsNullOrEmpty(caret) && !string.IsNullOrEmpty(prCode))
                        {
                            string newTagNo = $"{prefix}{caret}{prCode}{newSequenceNumber:D5}";
                            dataGridView1.Rows[rowIndex].Cells["tagno"].Value = newTagNo;
                            dataGridView1.Rows[rowIndex].Cells["prcode"].Value = prCode;
                        }
                        else
                        {
                            MessageBox.Show("CARET or PR_CODE is empty. Cannot generate Tag No.");
                        }
                    }
                }
            }
        }
      
        private bool UpdateData(DataGridViewRow row)
        {
            //if (!ValidateData(row))
            //{
            //    return false;
            //}
            string updateQuery = "UPDATE MAIN_DATA SET ITEM_DESC = @type, ITEM_PURITY = @caret, GW = @gross, NW = @net, " +
                     "LABOUR_AMT = @labour, WHOLE_LABOUR_AMT = @wholeLabour, OTHER_AMT = @other, HUID1 = @huid1, HUID2 = @huid2, SIZE = @size, " +
                     "\"COMMENT\" = @comment, ITEM_CODE = @prCode WHERE TAG_NO = @tagNo";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
        new SQLiteParameter("@tagNo", row.Cells["tagno"].Value?.ToString()),
        new SQLiteParameter("@type", row.Cells["itemName"].Value?.ToString()),
        new SQLiteParameter("@caret", row.Cells["purity"].Value?.ToString()),
        new SQLiteParameter("@gross", Convert.IsDBNull(row.Cells["gross"].Value) ? 0 : Convert.ToDecimal(row.Cells["gross"].Value)),
        new SQLiteParameter("@net", Convert.IsDBNull(row.Cells["net"].Value) ? 0 : Convert.ToDecimal(row.Cells["net"].Value)),
        new SQLiteParameter("@labour", Convert.IsDBNull(row.Cells["labour"].Value) ? 0 : Convert.ToDecimal(row.Cells["labour"].Value)),
        new SQLiteParameter("@wholeLabour", Convert.IsDBNull(row.Cells["labourAmount"].Value) ? 0 : Convert.ToDecimal(row.Cells["labourAmount"].Value)),
        new SQLiteParameter("@other", Convert.IsDBNull(row.Cells["other"].Value) ? 0 : Convert.ToDecimal(row.Cells["other"].Value)),
        new SQLiteParameter("@huid1", row.Cells["huid1"].Value?.ToString()),
        new SQLiteParameter("@huid2", row.Cells["huid2"].Value?.ToString()),
        new SQLiteParameter("@size", row.Cells["size"].Value?.ToString()),
        new SQLiteParameter("@comment", row.Cells["comment"].Value?.ToString()),
        new SQLiteParameter("@prCode", row.Cells["prcode"].Value?.ToString())
            };
            if (helper.RunQueryWithParametersSYADataBase(updateQuery, parameters))
            {
                return true;
            }
            return false;
        }
        private bool InsertData(DataGridViewRow row)
        {
            try
            {
                string InsertQuery = "INSERT INTO MAIN_DATA ( TAG_NO, ITEM_DESC, ITEM_PURITY, GW, NW, LABOUR_AMT,WHOLE_LABOUR_AMT, OTHER_AMT, HUID1, HUID2, SIZE, \"COMMENT\",IT_TYPE, ITEM_CODE, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PRICE, STATUS, AC_CODE, AC_NAME) VALUES ( @tagNo, @type, @caret, @gross, @net, @labour,@wholeLabour, @other, @huid1, @huid2, @size, @comment,@ittype, @prCode, @coYear, @coBook, @vchNo, @vchDate, @price, @status, @acCode, @acName)";
                {
                    UpdateTagNo(row.Index);
                    //if (!ValidateData(row))
                    //{
                    //    row.Cells["tagno"].Value = null;
                    //    return false;
                    //}
                    int currentYear = DateTime.Now.Year;
                    SQLiteParameter[] parameters = new SQLiteParameter[]
{
                new SQLiteParameter("@tagNo", row.Cells["tagno"].Value?.ToString()),
                new SQLiteParameter("@type", row.Cells["itemName"].Value?.ToString()),
                new SQLiteParameter("@caret", row.Cells["purity"].Value?.ToString()),
                new SQLiteParameter("@gross", Convert.IsDBNull(row.Cells["gross"].Value) ? 0 : Convert.ToDecimal(row.Cells["gross"].Value)),
                new SQLiteParameter("@net", Convert.IsDBNull(row.Cells["net"].Value) ? 0 : Convert.ToDecimal(row.Cells["net"].Value)),
                new SQLiteParameter("@labour", Convert.IsDBNull(row.Cells["labour"].Value) ? 0 : Convert.ToDecimal(row.Cells["labour"].Value)),
                new SQLiteParameter("@wholeLabour", Convert.IsDBNull(row.Cells["labourAmount"].Value) ? 0 : Convert.ToDecimal(row.Cells["labourAmount"].Value)),
                new SQLiteParameter("@other", Convert.IsDBNull(row.Cells["other"].Value) ? 0 : Convert.ToDecimal(row.Cells["other"].Value)),
                new SQLiteParameter("@huid1", row.Cells["huid1"].Value?.ToString()),
                new SQLiteParameter("@huid2", row.Cells["huid2"].Value?.ToString()),
                new SQLiteParameter("@size", row.Cells["size"].Value?.ToString()),
                new SQLiteParameter("@comment", row.Cells["comment"].Value?.ToString()),
                new SQLiteParameter("@prCode", row.Cells["prcode"].Value?.ToString()),
                new SQLiteParameter("@ittype", "G"),
                new SQLiteParameter("@coYear", $"{currentYear}-{currentYear + 1}"),
                new SQLiteParameter("@coBook", "015"),
                new SQLiteParameter("@vchNo", "SYA01"),
                new SQLiteParameter("@vchDate", DateTime.Now),
                new SQLiteParameter("@price", 0),
                new SQLiteParameter("@status", "INSTOCK"),
                new SQLiteParameter("@acCode", null),
                new SQLiteParameter("@acName", null)
};
                    if (helper.RunQueryWithParametersSYADataBase(InsertQuery, parameters))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void PrintLabels()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = helper.TagPrinterName;
                pd.PrintPage += new PrintPageEventHandler(Print);
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing labels: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Print(object sender, PrintPageEventArgs e)
        {
            PrintHelper.PrintPageAddGold(sender, e, dataGridView1.CurrentRow);
        }
        private void btnQuickSaveAndPrint_Click(object sender, EventArgs e)
        {
            if (btnQuickSaveAndPrint.Text == "Enable Quick Save & Print")
            {
                btnQuickSaveAndPrint.Text = "Disable Quick Save & Print";
                txtMessageBox.Text = "Quick Save & Print Enabled.";
                messageBoxTimer.Start();
                quickSaveAndPrint = true;
            }
            else if (btnQuickSaveAndPrint.Text == "Disable Quick Save & Print")
            {
                btnQuickSaveAndPrint.Text = "Enable Quick Save & Print";
                txtMessageBox.Text = "Quick Save & Print Disabled.";
                messageBoxTimer.Start();
                quickSaveAndPrint = false;
            }
        }
        private void buttonquicksave_Click(object sender, EventArgs e)
        {
            if (buttonquicksave.Text == "Enable Quick Save")
            {
                buttonquicksave.Text = "Disable Quick Save";
                txtMessageBox.Text = "Quick Save Enabled.";
                messageBoxTimer.Start();
                quickSave = true;
            }
            else if (buttonquicksave.Text == "Disable Quick Save")
            {
                buttonquicksave.Text = "Enable Quick Save";
                txtMessageBox.Text = "Quick Save Disabled.";
                messageBoxTimer.Start();
                quickSave = false;
            }
        }
    

       
    }
}
