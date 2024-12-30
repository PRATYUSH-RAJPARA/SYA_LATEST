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
        private bool isHandlingCellEnter = false; // Flag to prevent reentrant calls
        int C = 0;
        int R = 0;
        bool a = true;
        public addItemNew()
        {
            InitializeComponent();
        }
        private void addItemNew_Load(object sender, EventArgs e)
        {
            Attach_Event_Handlers();
            AddItemDataGridView_Setup.InitializeDataGridView(dataGridView1);
            // Use Invoke to set focus after initialization
            this.BeginInvoke(new Action(() =>
            {
                if (dataGridView1.Rows.Count >= 0 && dataGridView1.Columns.Contains("ITEM_TYPE"))
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["ITEM_TYPE"];
                    dataGridView1.BeginEdit(true); // Start editing if necessary
                }
            }));
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
                dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
                dataGridView1.CellEnter += DataGridView1_CellEnter;
                dataGridView1.KeyDown += DataGridView1_KeyDown;
                dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                dataGridView1.RowsAdded += DataGridView1_RowsAdded;
                EnterKeyNavigation.EnterKeyHandle_EventHandler(dataGridView1);
                void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
                {
                     a = itemValidations.Validate("G",e.ColumnIndex, e.RowIndex, LABEL_MESSAGE, dataGridView1);
                    if (!a)
                    {
                        C = e.ColumnIndex;
                        R = e.RowIndex;
                    }
                   // dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells["NW"];
                    EnterKeyNavigation.DataGridView1_CellEndEdit_ForEnterKeyHandle(sender, e);
                }
                 void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
                {
                    if (!a)
                    {
                        StopMovingFocus();
                    }
                    void StopMovingFocus()
                    {
                        if (isHandlingCellEnter)
                            return;
                        try
                        {
                            isHandlingCellEnter = true;
                                dataGridView1.BeginInvoke(new Action(() =>
                                {
                                    int rowIndex = e.RowIndex;
                                    int itemTypeColumnIndex = dataGridView1.Columns["ITEM_TYPE"].Index;
                                    dataGridView1.CurrentCell = dataGridView1.Rows[R].Cells[C];
                                }));
                        }
                        finally
                        {
                            isHandlingCellEnter = false;
                        }
                    }
                    // Avoid reentrant calls
                    if (isHandlingCellEnter)
                        return;
                    try
                    {
                        isHandlingCellEnter = true;
                        if (dataGridView1.Columns[e.ColumnIndex].Name == "TAG_NO")
                        {
                            dataGridView1.BeginInvoke(new Action(() =>
                            {
                                int rowIndex = e.RowIndex;
                                int itemTypeColumnIndex = dataGridView1.Columns["ITEM_TYPE"].Index;
                                dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[itemTypeColumnIndex];
                            }));
                        }
                    }
                    finally
                    {
                        isHandlingCellEnter = false;
                    }
                    EnterKeyNavigation.DataGridView1_CellEnter_ForEnterKeyHandle();
                }
                void DataGridView1_KeyDown(object sender, KeyEventArgs e)
                {
                    a = itemValidations.Validate("G", dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, LABEL_MESSAGE, dataGridView1);
                    if (!a)
                    {
                        C = dataGridView1.CurrentCell.ColumnIndex;
                        R = dataGridView1.CurrentCell.RowIndex;
                    }
                    EnterKeyNavigation.DataGridView1_KeyDown_ForEnterKeyHandle(dataGridView1, e);
                }
                void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
                {
                    if (dataGridView1.CurrentCell == null)
                    {
                        return; // No cell is currently selected
                    }
                    DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                    if (editingControl != null)
                    {
                        try
                        {
                            editingControl.KeyPress -= EditingControl_KeyPress;
                        }
                        catch (Exception) { }
                        if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["GW"].Index ||
                            dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["NW"].Index ||
                            dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["LBR_AMT"].Index ||
                            dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["LBR_RATE"].Index ||
                            dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["OTH_AMT"].Index)
                        {
                            editingControl.KeyPress += EditingControl_KeyPress;
                        }
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
        }
        private void BUTTON_PRINT_ON_OFF_Click(object sender, EventArgs e)
        {
        }
    }
}
