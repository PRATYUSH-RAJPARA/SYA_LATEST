using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SYA
{
    public partial class SearchNew : Form
    {
        searchHelper searchHelper = new searchHelper();
        private const int PageSize = 50; // Number of rows to load per page
        private int currentOffset = 0; // Tracks the current offset for lazy loading
        private bool isLoading = false; // Prevents multiple fetches during loading
        private DataTable loadedTable = new DataTable(); // Table to hold all loaded rows

        public SearchNew()
        {
            InitializeComponent();
        }

        private void SearchNew_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadInitialData();
            AttachEventHandlers();
            dataGridView1.RowsAdded += DataGridView1_RowsAdded; // Attach event handler for row addition
            CustomizeDataGridView();
        }

        private void CustomizeDataGridView()
        {
            // Hide specific columns
            string[] columnsToHide = { "ID", "DESIGN", "ITM_SIZE", "ITM_PCS" };
            foreach (string columnName in columnsToHide)
            {
                if (dataGridView1.Columns.Contains(columnName))
                {
                    dataGridView1.Columns[columnName].Visible = false;
                }
            }

            // Set row height
            dataGridView1.RowTemplate.Height = 50;

            // Configure text alignment and font size
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, 15);

            // Adjust column widths to fit content
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Manually adjust widths of specific columns if needed
            AdjustColumnWidths();

            // Additional row styling (if needed for preloaded rows)
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is DataRowView dataRowView)
                {
                    DataRow dataRow = dataRowView.Row;

                    // Styling logic based on conditions
                    if (dataRow["CO_BOOK"] != DBNull.Value)
                    {
                        string coBook = dataRow["CO_BOOK"].ToString();
                        if (coBook == "026" || coBook == "027")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (coBook == "015")
                        {
                            if (dataRow["METAL_TYPE"] != DBNull.Value)
                            {
                                string metalType = dataRow["METAL_TYPE"].ToString();
                                if (metalType == "G")
                                {
                                    row.DefaultCellStyle.BackColor = Color.Gold;
                                }
                                else if (metalType == "S")
                                {
                                    row.DefaultCellStyle.BackColor = Color.Silver;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AdjustColumnWidths()
        {
            // Adjust the width of METAL_TYPE column based on its content
            if (dataGridView1.Columns.Contains("METAL_TYPE"))
            {
                int maxWidth = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string value = row.Cells["METAL_TYPE"].Value?.ToString() ?? string.Empty;
                    int width = TextRenderer.MeasureText(value, dataGridView1.Font).Width;
                    maxWidth = Math.Max(maxWidth, width);
                }
                // Set the width with a margin (you can adjust the margin based on your preference)
                dataGridView1.Columns["METAL_TYPE"].Width = maxWidth + 10; // 10px margin
            }
        }

        private void InitializeDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Scroll += DataGridView1_Scroll;
        }

        private void AttachEventHandlers()
        {
            // Attach event handlers
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.CellEnter += DataGridView1_CellEnter;
            dataGridView1.KeyDown += DataGridView1_KeyDown;
            // Initialize the Timer for Enter key navigation
            searchHelper.EnterKeyHandle_EventHandler(dataGridView1);
        }

        private void LoadInitialData()
        {
            loadedTable = CreateHardcodedDataTable();
            LoadNextPage();
            dataGridView1.DataSource = loadedTable;
        }

        private DataTable CreateHardcodedDataTable()
        {
            DataTable table = new DataTable();
            var columnDefinitions = new (string ColumnName, Type DataType)[] {
                ("ID", typeof(int)),
                ("CO_YEAR", typeof(string)),
                ("CO_BOOK", typeof(string)),
                ("VCH_NO", typeof(string)),
                ("VCH_DATE", typeof(string)),
                ("PURITY", typeof(string)),
                ("METAL_TYPE", typeof(string)),
                ("TAG_NO", typeof(string)),
                ("DESIGN", typeof(string)),
                ("HUID1", typeof(string)),
                ("HUID2", typeof(string)),
                ("HUID3", typeof(string)),
                ("ITM_SIZE", typeof(string)),
                ("ITEM_TYPE", typeof(string)),
                ("ITM_PCS", typeof(int)),
                ("GW", typeof(int)),
                ("NW", typeof(int)),
                ("LBR_RATE", typeof(int)),
                ("OTH_AMT", typeof(int)),
                ("LBR_AMT", typeof(int)),
                ("SIZE", typeof(string)),
                ("PRICE", typeof(int)),
                ("COMMENT", typeof(string)),
                ("ITM_RAT", typeof(int)),
                ("ITM_AMT", typeof(int)),
                ("AC_CODE", typeof(int))
            };
            foreach (var (columnName, dataType) in columnDefinitions)
            {
                table.Columns.Add(columnName, dataType);
            }
            return table;
        }

        private void LoadNextPage()
        {
            isLoading = true;
            string query = $@"
                SELECT 
                    ID, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, HUID3, 
                    ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, COMMENT, ITM_RAT, 
                    ITM_AMT, AC_CODE
                FROM (
                    SELECT * FROM SALE_DATA_NEW
                    UNION
                    SELECT ID, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, 
                           HUID3, ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, 
                           COMMENT, NULL AS ITM_RAT, NULL AS ITM_AMT, NULL AS AC_CODE
                    FROM MAIN_DATA_NEW
                ) AS combined_data
                ORDER BY CO_YEAR DESC, VCH_DATE DESC
                LIMIT {PageSize} OFFSET {currentOffset};";
            DataTable originalTable = helper.FetchDataTableFromSYADataBase(query);
            MapRowsToDataTable(originalTable, loadedTable);
            currentOffset += PageSize;
            isLoading = false;
        }

        private void MapRowsToDataTable(DataTable source, DataTable destination)
        {
            foreach (DataRow row in source.Rows)
            {
                DataRow newRow = destination.NewRow();
                foreach (DataColumn column in source.Columns)
                {
                    if (column.ColumnName == "VCH_DATE" && row[column] != DBNull.Value)
                    {
                        if (row[column] is string dateString)
                        {
                            // Try to parse the string to a DateTime
                            if (DateTime.TryParse(dateString, out DateTime parsedDate))
                            {
                                newRow[column.ColumnName] = parsedDate.ToString("dd-MM-yy");
                            }
                            else
                            {
                                // If parsing fails, keep the original value
                                newRow[column.ColumnName] = dateString;
                            }
                        }
                        else if (row[column] is DateTime dateValue)
                        {
                            // If the value is already a DateTime, format it directly
                            newRow[column.ColumnName] = dateValue.ToString("dd-MM-yy");
                        }
                        else
                        {
                            // If it's neither string nor DateTime, keep the original value
                            newRow[column.ColumnName] = row[column];
                        }
                    }
                    else
                    {
                        newRow[column.ColumnName] = row[column];
                    }
                }
                destination.Rows.Add(newRow);
            }
        }

        // Handle row styling as they are added
        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                DataGridViewRow gridViewRow = dataGridView1.Rows[i];

                // Set row height to 50px
                gridViewRow.Height = 50;

                if (gridViewRow.DataBoundItem is DataRowView dataRowView)
                {
                    DataRow dataRow = dataRowView.Row;

                    // Styling for CO_BOOK values
                    if (dataRow["CO_BOOK"] != DBNull.Value)
                    {
                        string coBookValue = dataRow["CO_BOOK"].ToString();
                        if (coBookValue == "026" || coBookValue == "027")
                        {
                            gridViewRow.DefaultCellStyle.BackColor = Color.LightSkyBlue; // Lighter blue background
                            gridViewRow.DefaultCellStyle.ForeColor = Color.White;       // White font
                        }
                        else if (coBookValue == "015")
                        {
                            // Styling for CO_BOOK = 015 and Metal Type
                            if (dataRow["METAL_TYPE"] != DBNull.Value)
                            {
                                string metalType = dataRow["METAL_TYPE"].ToString().ToUpper();
                                if (metalType == "G") // Gold
                                {
                                    gridViewRow.DefaultCellStyle.BackColor = Color.Gold; // Gold background
                                }
                                else if (metalType == "S") // Silver
                                {
                                    gridViewRow.DefaultCellStyle.BackColor = Color.Silver; // Silver background
                                }
                            }
                        }
                    }

                    // Example: Alternating row colors for unstyled rows (Optional for better readability)
                    if (gridViewRow.DefaultCellStyle.BackColor == Color.Empty) // If no specific style applied
                    {
                        gridViewRow.DefaultCellStyle.BackColor = i % 2 == 0 ? Color.LightGray : Color.White; // Alternating colors
                    }
                }
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            searchHelper.DataGridView1_CellEndEdit_ForEnterKeyHandle(sender, e);
        }

        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            searchHelper.DataGridView1_CellEnter_ForEnterKeyHandle();
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            searchHelper.DataGridView1_KeyDown_ForEnterKeyHandle(dataGridView1, e);
        }

        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                int visibleRowCount = dataGridView1.DisplayedRowCount(false);
                int firstVisibleRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
                if (firstVisibleRowIndex >= 0)
                {
                    int lastVisibleRowIndex = firstVisibleRowIndex + visibleRowCount - 1;
                    if (lastVisibleRowIndex == loadedTable.Rows.Count - 1 && !isLoading)
                    {
                        LoadNextPage();
                    }
                }
            }
        }
    }
}
