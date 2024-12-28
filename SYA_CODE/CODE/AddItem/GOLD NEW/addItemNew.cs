using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SYA
{
    public partial class addItemNew : Form
    {
        EnterKeyNavigation EnterKeyNavigation = new EnterKeyNavigation();
        ItemValidations itemValidations = new ItemValidations();
        AddItemDataGridView_Setup AddItemDataGridView_Setup = new AddItemDataGridView_Setup();
        public addItemNew()
        {
            InitializeComponent();
        }
        private void addItemNew_Load(object sender, EventArgs e)
        {
            AttachEventHandlers();
            InitializeDataGridView();
        }
        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(CreateTextBoxColumn("TAG_NO", "Tag No"));
            dataGridView1.Columns.Add(CreateComboBoxColumn("ITEM_TYPE", "Item Type"));
            dataGridView1.Columns.Add(CreateComboBoxColumn("PURITY", "Purity"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("GW", "Gross Weight"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("NW", "Net Weight"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("LBR_RATE", "Labour"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("LBR_AMT", "Labour Amount"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("OTH_AMT", "Other"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("HUID1", "HUID1"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("HUID2", "HUID2"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("HUID3", "HUID3"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("SIZE", "Size"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("PRICE", "PRICE"));
            dataGridView1.Columns.Add(CreateTextBoxColumn("COMMENT", "Comment"));
            dataGridView1.AllowUserToAddRows = true;
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
            AddItemDataGridView_Setup.CustomizeDataGridView(dataGridView1);

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
        private void AttachEventHandlers()
        {
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.CellEnter += DataGridView1_CellEnter;
            dataGridView1.KeyDown += DataGridView1_KeyDown;
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;

            EnterKeyNavigation.EnterKeyHandle_EventHandler(dataGridView1);
             void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
                var cellValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? "Empty";
                DataGridViewCell dg_cell = dataGridView1[e.ColumnIndex, e.RowIndex];
                var columnName = dataGridView1.Columns[e.ColumnIndex].Name;

              //  itemValidations.Validate(e.ColumnIndex, e.RowIndex, LABEL_MESSAGE);
                EnterKeyNavigation.DataGridView1_CellEndEdit_ForEnterKeyHandle(sender, e);
            }
             void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
            {
                EnterKeyNavigation.DataGridView1_CellEnter_ForEnterKeyHandle();
            }
             void DataGridView1_KeyDown(object sender, KeyEventArgs e)
            {
                var cellValue = dataGridView1.CurrentCell.Value?.ToString() ?? "Empty";
                var columnName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
                DataGridViewCell dg_cell = dataGridView1.CurrentCell;
                dg_cell.Value.ToString();

             //   itemValidations.Validate(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, LABEL_MESSAGE);
                EnterKeyNavigation.DataGridView1_KeyDown_ForEnterKeyHandle(dataGridView1, e);
            }
             void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
            {
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                try
                {
                    editingControl.KeyPress -= EditingControl_KeyPress;
                }
                catch(Exception ){ }
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["GW"].Index ||
                    dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["NW"].Index ||
                    dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["LBR_AMT"].Index ||
                    dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["LBR_RATE"].Index ||
                    dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["OTH_AMT"].Index)
                {
                    editingControl.KeyPress += EditingControl_KeyPress;
                }
                void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8)
                    {
                        e.Handled = true; // Block invalid characters
                    }
                    else
                    {
                        // Ensure only one decimal point is allowed
                        if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                        {
                            e.Handled = true; // Block multiple decimal points
                        }
                    }
                }
            }
             void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
            {
                AddItemDataGridView_Setup.DataGridView1_RowsAdded(sender, e, dataGridView1);
            }

        }
        private void BUTTON_PRINT_ON_OFF_Click(object sender, EventArgs e)
        {
        }
    }
}
