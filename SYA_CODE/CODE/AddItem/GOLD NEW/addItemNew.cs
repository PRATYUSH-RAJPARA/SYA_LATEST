using System.Drawing.Drawing2D;
namespace SYA
{
    public partial class addItemNew : Form
    {
        ItemValidations itemValidations = new ItemValidations();
        AddItemDataGridView_Setup AddItemDataGridView_Setup = new AddItemDataGridView_Setup();
        ItemUpdateOrSave ItemUpdateOrSave = new ItemUpdateOrSave();
        bool is_Valid = true;
        string WEIGHT_OR_PRICE = "WEIGHT TAG";
        public AutoCompleteStringCollection itemTypeCollection = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection purityCollection = new AutoCompleteStringCollection();
        public addItemNew()
        {
            InitializeComponent();
        }
        private void SetButtonGradient(Button button, string state)
        {
            // Determine colors based on the state
            Color startColor = state == "ON" ? Color.LightGreen : Color.Gray;
            Color endColor = state == "ON" ? Color.Green : Color.DarkGray;
            // Remove existing Paint event handler to prevent multiple handlers
            button.Paint -= Button_Paint;
            // Attach the updated Paint event handler
            button.Paint += Button_Paint;
            // Invalidate the button to trigger a repaint
            button.Invalidate();
            // Local function for handling the Paint event
            void Button_Paint(object sender, PaintEventArgs e)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(button.ClientRectangle, startColor, endColor, LinearGradientMode.ForwardDiagonal))
                {
                    // Fill gradient background
                    e.Graphics.FillRectangle(brush, button.ClientRectangle);
                    // Draw button text
                    TextRenderer.DrawText(e.Graphics, state, button.Font, button.ClientRectangle, button.ForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
        }
        private void addItemNew_Load(object sender, EventArgs e)
        {
            label1.Text = "Labour Changes On\nPrice Change";
            dataGridView2.Visible = false;
            BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE.Text = "ON";
            SetButtonGradient(BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE, BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE.Text);
            itemValidations.change_Labour_On_Price_Change = true;
            Attach_Event_Handlers();
            AddItemDataGridView_Setup.InitializeDataGridView(dataGridView1);
            dataGridView1.Rows.Add();
            AddItemDataGridView_Setup.InitializeAutoCompleteCollections(itemTypeCollection, purityCollection);
            this.BeginInvoke(new Action(() =>
            {
                if (dataGridView1.Rows.Count >= 0 && dataGridView1.Columns.Contains("ITEM_TYPE"))
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["ITEM_TYPE"];
                }
            }));
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                var editingControl = dataGridView1.EditingControl as TextBox;
                string uncommittedValue = "";
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                string cellValue = dataGridView1.Rows[rowIndex].Cells[columnIndex].Value?.ToString() ?? "";
                if (editingControl != null)
                {
                    uncommittedValue = editingControl.Text;
                    if (cellValue.Length > 0)
                    {
                        if (cellValue != uncommittedValue)
                        {
                            dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = uncommittedValue;
                        }
                    }
                    dataGridView1.EndEdit();
                }
                is_Valid = itemValidations.Validate("G", columnIndex, rowIndex, LABEL_MESSAGE, dataGridView1, itemTypeCollection, purityCollection);
                if (is_Valid)
                {
                    if (dataGridView1.Columns[columnIndex].Name.ToString() == "COMMENT")
                    {
                        string RESULT =  ItemUpdateOrSave.update_or_save(rowIndex, columnIndex,dataGridView1,LABEL_MESSAGE);
                        //if (RESULT == "0") { LABEL_MESSAGE.Text = "Changes Not Saved."; }
                        //else if (RESULT == "1") { LABEL_MESSAGE.Text = $"{dataGridView1.Rows[rowIndex].Cells["TAG_NO"].Value.ToString()} Saved."; }
                        //else { LABEL_MESSAGE.Text = $"Something went wrong with tag No : {dataGridView1.Rows[rowIndex].Cells["TAG_NO"].Value.ToString()} Saved."; }
                    }
                    if (columnIndex < dataGridView1.ColumnCount - 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[columnIndex + 1];
                    }
                    else
                    {
                        dataGridView1.Rows.Add();
                        if (rowIndex < dataGridView1.RowCount - 1)
                        {
                            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex + 1].Cells[0];
                        }
                    }
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Attach_Event_Handlers()
        {
            others();
            datagridview();
            void others()
            {
                this.SizeChanged += Form1_SizeChanged;
                void Form1_SizeChanged(object sender, EventArgs e)
                {
                    AddItemDataGridView_Setup.AdjustColumnWidths(dataGridView1);
                }
            }
            void datagridview()
            {
                dataGridView1.RowsAdded += DataGridView1_RowsAdded;
                dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
                {
                    AddItemDataGridView_Setup.DataGridView1_RowsAdded(sender, e, dataGridView1);
                }
                void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
                {
                    if (e.Control is TextBox textBox)
                    {
                        textBox.Enter -= TextBox_Enter;
                        textBox.Enter += TextBox_Enter;
                        if (dataGridView1.CurrentCell.OwningColumn.Name == "ITEM_TYPE")
                        {
                            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            textBox.AutoCompleteCustomSource = itemTypeCollection;
                        }
                        else if (dataGridView1.CurrentCell.OwningColumn.Name == "PURITY")
                        {
                            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            textBox.AutoCompleteCustomSource = purityCollection;
                        }
                        else
                        {
                            textBox.AutoCompleteMode = AutoCompleteMode.None;
                        }
                    }
                    if (dataGridView1.CurrentCell == null)
                    {
                        return;
                    }
                    DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                    if (editingControl != null)
                    {
                        editingControl.KeyPress -= EditingControl_KeyPress;
                        editingControl.KeyPress += EditingControl_KeyPress;
                    }
                    void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
                    {
                        TextBox textBox = sender as TextBox;
                        if (textBox != null)
                        {
                            if (char.IsLetter(e.KeyChar))
                            {
                                e.KeyChar = char.ToUpper(e.KeyChar); // Convert to uppercase
                            }
                            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["GW"].Index ||
                                dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["NW"].Index ||
                                dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["LBR_AMT"].Index ||
                                dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["LBR_RATE"].Index ||
                                dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["OTH_AMT"].Index)
                            {
                                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8)
                                {
                                    e.Handled = true; // Block invalid characters
                                }
                                else
                                {
                                    if (e.KeyChar == '.' && textBox.Text.Contains("."))
                                    {
                                        e.Handled = true; // Block multiple decimal points
                                    }
                                }
                            }
                        }
                    }
                    void TextBox_Enter(object sender, EventArgs e)
                    {
                        if (sender is TextBox textBox)
                        {
                            textBox.SelectAll();
                        }
                    }
                }
            }
        }
        private void BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE_Click(object sender, EventArgs e)
        {
            if (BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE.Text == "ON")
            {
                BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE.Text = "OFF";
                itemValidations.change_Labour_On_Price_Change = false;
            }
            else
            {
                BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE.Text = "ON";
                itemValidations.change_Labour_On_Price_Change = true;
            }
            SetButtonGradient(BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE, BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE.Text);
            SetButtonGradient(BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE, BUTTON_LABOUR_CHANGE_ON_PRICE_CHANGE.Text);
        }
        private void BUTTON_WEIGHT_OR_PRICE_Click(object sender, EventArgs e)
        {
            if (BUTTON_WEIGHT_OR_PRICE.Text == "WEIGHT TAG")
            {
                BUTTON_WEIGHT_OR_PRICE.Text = "PRICE TAG";
            }
            else
            {
                BUTTON_WEIGHT_OR_PRICE.Text = "WEIGHT TAG";
            }
        }
    }
}
