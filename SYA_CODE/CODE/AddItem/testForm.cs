using System;
using System.Windows.Forms;

namespace SYA
{
    public partial class testForm : Form
    {
        private int savedRowIndex = -1;
        private int savedColumnIndex = -1;
        private System.Windows.Forms.Timer moveCellTimer;

        public testForm()
        {
            InitializeComponent();

            // Attach event handlers
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.CellEnter += DataGridView1_CellEnter;
            dataGridView1.KeyDown += DataGridView1_KeyDown;

            // Initialize the Timer
            moveCellTimer = new System.Windows.Forms.Timer();
            moveCellTimer.Interval = 1; // Minimal delay
            moveCellTimer.Tick += MoveCellTimer_Tick;
        }

        private void testForm_Load(object sender, EventArgs e)
        {
            // Add 7 columns to the DataGridView
            dataGridView1.Columns.Add("Column1", "Column 1");
            dataGridView1.Columns.Add("Column2", "Column 2");
            dataGridView1.Columns.Add("Column3", "Column 3");
            dataGridView1.Columns.Add("Column4", "Column 4");
            dataGridView1.Columns.Add("Column5", "Column 5");
            dataGridView1.Columns.Add("Column6", "Column 6");
            dataGridView1.Columns.Add("Column7", "Column 7");

            // Add sample data
            dataGridView1.Rows.Add("Data1", "Data2", "Data3", "Data4", "Data5", "Data6", "Data7");
            dataGridView1.Rows.Add("Data8", "Data9", "Data10", "Data11", "Data12", "Data13", "Data14");
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Save the current cell's coordinates
            savedRowIndex = e.RowIndex;
            savedColumnIndex = e.ColumnIndex;
        }

        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Start the Timer to change focus after the current event
            if (savedRowIndex >= 0 && savedColumnIndex >= 0)
            {
                moveCellTimer.Start();
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Prevent default Enter key behavior

                if (dataGridView1.CurrentCell != null)
                {
                    // Save the current cell's coordinates
                    savedRowIndex = dataGridView1.CurrentCell.RowIndex;
                    savedColumnIndex = dataGridView1.CurrentCell.ColumnIndex;

                    // Start the Timer to handle navigation
                    moveCellTimer.Start();
                }
            }
        }

        private void MoveCellTimer_Tick(object sender, EventArgs e)
        {
            // Stop the Timer to prevent repeated execution
            moveCellTimer.Stop();

            // Move focus to the next cell
            if (savedRowIndex >= 0 && savedColumnIndex >= 0)
            {
                int nextColumnIndex = savedColumnIndex + 1;

                if (nextColumnIndex < dataGridView1.Columns.Count)
                {
                    // Move to the next column in the same row
                    dataGridView1.CurrentCell = dataGridView1.Rows[savedRowIndex].Cells[nextColumnIndex];
                }
                else if (savedRowIndex + 1 < dataGridView1.Rows.Count)
                {
                    // Move to the first column of the next row
                    dataGridView1.CurrentCell = dataGridView1.Rows[savedRowIndex + 1].Cells[0];
                }

                // Reset saved indices after moving
                savedRowIndex = -1;
                savedColumnIndex = -1;
            }
        }
    }
}
