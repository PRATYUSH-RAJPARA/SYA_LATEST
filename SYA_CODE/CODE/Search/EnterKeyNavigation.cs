    using System.Windows.Forms;
    namespace SYA
    {
        public class EnterKeyNavigation
        {
            #region DataGridView Navigation (Enter Key Handling)
            private int savedRowIndex = -1;
            private int savedColumnIndex = -1;
            private System.Windows.Forms.Timer moveCellTimer;
            private DataGridView _dataGridView1;
            public void EnterKeyHandle_EventHandler(DataGridView dataGridView1)
            {
                _dataGridView1 = dataGridView1;
                moveCellTimer = new System.Windows.Forms.Timer();
                moveCellTimer.Interval = 1; // Minimal delay
                moveCellTimer.Tick += MoveCellTimer_Tick_ForEnterKeyHandle;
            }
            public void DataGridView1_CellEndEdit_ForEnterKeyHandle(object sender, DataGridViewCellEventArgs e)
            {
                savedRowIndex = e.RowIndex;
                savedColumnIndex = e.ColumnIndex;
            }
            public void DataGridView1_CellEnter_ForEnterKeyHandle()
            {
                if (savedRowIndex >= 0 && savedColumnIndex >= 0)
                {
                    moveCellTimer.Start();
                }
            }
            public void DataGridView1_KeyDown_ForEnterKeyHandle(DataGridView dataGridView1, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; // Prevent default Enter key behavior
                    if (dataGridView1.CurrentCell != null)
                    {
                        savedRowIndex = dataGridView1.CurrentCell.RowIndex;
                        savedColumnIndex = dataGridView1.CurrentCell.ColumnIndex;
                        moveCellTimer.Start();
                    }
                }
            }
        public void MoveCellTimer_Tick_ForEnterKeyHandle(object sender, EventArgs e)
        {
            moveCellTimer.Stop();
            if (savedRowIndex >= 0 && savedColumnIndex >= 0)
            {
                int nextColumnIndex = savedColumnIndex + 1;
                // Loop to find the next visible column in the current row
                while (nextColumnIndex < _dataGridView1.Columns.Count && !_dataGridView1.Columns[nextColumnIndex].Visible)
                {
                    nextColumnIndex++;
                }
                // If a valid visible column is found, set the current cell
                if (nextColumnIndex < _dataGridView1.Columns.Count)
                {
                    _dataGridView1.CurrentCell = _dataGridView1.Rows[savedRowIndex].Cells[nextColumnIndex];
                }
                else if (savedRowIndex + 1 < _dataGridView1.Rows.Count)
                {
                    // If no visible columns in the current row, move to the next row and set the first visible column
                    savedRowIndex++;
                    nextColumnIndex = 0; // Start from the first column of the next row
                    // Loop to find the first visible column in the next row
                    while (nextColumnIndex < _dataGridView1.Columns.Count && !_dataGridView1.Columns[nextColumnIndex].Visible)
                    {
                        nextColumnIndex++;
                    }
                    // Set the first visible cell in the next row
                    if (nextColumnIndex < _dataGridView1.Columns.Count)
                    {
                        _dataGridView1.CurrentCell = _dataGridView1.Rows[savedRowIndex].Cells[nextColumnIndex];
                    }
                }
                // Reset saved row and column indices
                savedRowIndex = -1;
                savedColumnIndex = -1;
            }
        }
        // Helper method to get the first visible column index
        private int GetFirstVisibleColumnIndex()
        {
            for (int i = 0; i < _dataGridView1.Columns.Count; i++)
            {
                if (_dataGridView1.Columns[i].Visible)
                {
                    return i;
                }
            }
            return -1; // Return -1 if no visible columns exist
        }
        #endregion
    }
}