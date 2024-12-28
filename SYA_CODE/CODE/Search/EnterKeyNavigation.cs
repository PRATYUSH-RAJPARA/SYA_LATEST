namespace SYA
{
    public class EnterKeyNavigation
    {
        private int savedRowIndex = -1;
        private int savedColumnIndex = -1;
        private System.Windows.Forms.Timer moveCellTimer;
        private DataGridView _dataGridView1;
        public bool AllowRowAdding { get; set; } = false; // Flag to enable/disable row adding
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
        }public void DataGridView1_CellEndEdit_ForEnterKeyHandle1(object sender, DataGridViewCellEventArgs e,Label l,DataGridView dataGridView1)
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
        public void DataGridView1_KeyDown_ForEnterKeyHandle1(DataGridView dataGridView1, KeyEventArgs e,Label LABEL_MESSAGE)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Prevent default Enter key behavior
                if (dataGridView1.CurrentCell != null)
                {
                    if (dataGridView1.IsCurrentCellInEditMode)
                    {
                        dataGridView1.EndEdit(); // Commit the current cell's edit
                    }
                    // Retrieve the value of the current cell
                    savedRowIndex = dataGridView1.CurrentCell.RowIndex;
                    savedColumnIndex = dataGridView1.CurrentCell.ColumnIndex;
                    var cellValue = dataGridView1[savedColumnIndex, savedRowIndex].Value?.ToString() ?? "Empty";
                    LABEL_MESSAGE.Text = $" Cell Value on Leave: {cellValue} ";
                    moveCellTimer.Start();
                }
            }
        }
        public void MoveCellTimer_Tick_ForEnterKeyHandle(object sender, EventArgs e)
        {
           // MessageBox.Show("5");
            moveCellTimer.Stop();
            if (savedRowIndex >= 0 && savedColumnIndex >= 0)
            {
                int nextColumnIndex = savedColumnIndex + 1;
                // Loop to find the next visible column in the current row
                while (nextColumnIndex < _dataGridView1.Columns.Count && !_dataGridView1.Columns[nextColumnIndex].Visible)
                {
                    nextColumnIndex++;
                }
                if (nextColumnIndex < _dataGridView1.Columns.Count)
                {
                    // If a valid visible column is found, move to it
                    _dataGridView1.CurrentCell = _dataGridView1.Rows[savedRowIndex].Cells[nextColumnIndex];
                }
                else
                {
                    // Move to the next row or add a new row if it's the last row and AllowRowAdding is true
                    if (savedRowIndex + 1 >= _dataGridView1.Rows.Count - 1 && AllowRowAdding)
                    {
                        _dataGridView1.Rows.Add(); // Add a new row
                    }
                    savedRowIndex++;
                    nextColumnIndex = 0;
                    // Loop to find the first visible column in the new row
                    while (nextColumnIndex < _dataGridView1.Columns.Count && !_dataGridView1.Columns[nextColumnIndex].Visible)
                    {
                        nextColumnIndex++;
                    }
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
    }
}
