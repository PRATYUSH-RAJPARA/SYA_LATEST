using System;
using System.Windows.Forms;
namespace SYA
{
    public class DataGridViewNavigationHelper
    {
        private int savedRowIndex = -1;
        private int savedColumnIndex = -1;
        private System.Windows.Forms.Timer moveCellTimer;
        private DataGridView targetDataGridView;
        public DataGridViewNavigationHelper(DataGridView dataGridView)
        {
            targetDataGridView = dataGridView;
            // Attach event handlers
            targetDataGridView.CellEndEdit += DataGridView_CellEndEdit;
            targetDataGridView.CellEnter += DataGridView_CellEnter;
            targetDataGridView.KeyDown += DataGridView_KeyDown;
            // Initialize the Timer
            moveCellTimer = new System.Windows.Forms.Timer { Interval = 1 };
            moveCellTimer.Tick += MoveCellTimer_Tick;
        }
        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Save the current cell's coordinates
            savedRowIndex = e.RowIndex;
            savedColumnIndex = e.ColumnIndex;
        }
        private void DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Start the Timer to change focus after the current event
            if (savedRowIndex >= 0 && savedColumnIndex >= 0)
            {
                moveCellTimer.Start();
            }
        }
        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Prevent default Enter key behavior
                if (targetDataGridView.CurrentCell != null)
                {
                    // Save the current cell's coordinates
                    savedRowIndex = targetDataGridView.CurrentCell.RowIndex;
                    savedColumnIndex = targetDataGridView.CurrentCell.ColumnIndex;
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
                // Check if the current column is "comment"
                if (targetDataGridView.Columns[savedColumnIndex].Name == "comment")
                {
                    if (savedRowIndex + 1 < targetDataGridView.Rows.Count)
                    {
                        // Move to the first column of the next row
                        targetDataGridView.CurrentCell = targetDataGridView.Rows[savedRowIndex + 1].Cells[0];
                    }
                    else
                    {
                        // Add a new row and move to the first column of the new row
                        int newRowIndex = targetDataGridView.Rows.Add();
                        targetDataGridView.CurrentCell = targetDataGridView.Rows[newRowIndex].Cells[0];
                    }
                }
                else
                {
                    // Move to the next column in the same row
                    targetDataGridView.CurrentCell = targetDataGridView.Rows[savedRowIndex].Cells[savedColumnIndex + 1];
                }

                // Reset saved indices after moving
                savedRowIndex = -1;
                savedColumnIndex = -1;
            }
        }
    }
}
