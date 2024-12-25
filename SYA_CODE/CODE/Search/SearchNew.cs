﻿﻿using System;
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

        string SELECT = " SELECT ";
        string CN_SALE = @" ID, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, HUID3,ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, COMMENT, ITM_RAT,  ITM_AMT, AC_CODE,AC_NAME ";
        string FROM_SALE = @" FROM (  SELECT * FROM SALE_DATA_NEW ";
        string WHERE_SALE = @"  ";
        string UNION_SELECT = "  UNION SELECT ";
        string CN_MD = " ID, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, HUID3, ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, COMMENT, NULL AS ITM_RAT, NULL AS ITM_AMT, NULL AS AC_CODE, NULL AS AC_NAME ";
        string FROM_MD = "   FROM MAIN_DATA_NEW ";
        string WHERE_MD =@"  ";
        string COMBINE_DATA = @"  ) AS combined_data  ";
        string WHERE_ALL = " ";
        string orderByQuery = "  ORDER BY CO_YEAR DESC, VCH_DATE DESC ";

        
        public SearchNew()
        {
            string A = SELECT + CN_SALE + FROM_SALE + WHERE_SALE + UNION_SELECT + CN_MD + FROM_MD + WHERE_MD + COMBINE_DATA + orderByQuery;
            InitializeComponent();
        }
        private void SearchNew_Load(object sender, EventArgs e)
        {
            loadData();
            AttachEventHandlers();
            dataGridView1.RowsAdded += DataGridView1_RowsAdded; // Attach event handler for row addition
            BindCOYearComboBox();
            BindACNameComboBox();
        }
        private void loadData() {
            InitializeDataGridView();
            LoadInitialData();
            CustomizeDataGridView();
        }
        private void BindACNameComboBox()
        {
            string query = @"
        SELECT DISTINCT AC_NAME
        FROM SALE_DATA_NEW 
        ORDER BY AC_NAME ASC;";

            DataTable AC_NAME_TABLE = helper.FetchDataTableFromSYADataBase(query);

            // Create a new row for "All" and add it at the top
            DataRow allRow = AC_NAME_TABLE.NewRow();
            allRow["AC_NAME"] = "All";
            AC_NAME_TABLE.Rows.InsertAt(allRow, 0);

            // Bind the fetched values to the combo box
            CB_NAME.DataSource = AC_NAME_TABLE;
            CB_NAME.DisplayMember = "AC_NAME";
            CB_NAME.ValueMember = "AC_NAME";
            CB_NAME.SelectedIndex = 0;

            // Enable autocomplete functionality
            CB_NAME.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CB_NAME.AutoCompleteSource = AutoCompleteSource.ListItems;

            CB_NAME.SelectedIndexChanged += CB_NAME_SelectedIndexChanged;
        }

        private void BindCOYearComboBox()
        {
            string query = @"
        SELECT DISTINCT CO_YEAR
        FROM (
            SELECT CO_YEAR FROM SALE_DATA_NEW
            UNION
            SELECT CO_YEAR FROM MAIN_DATA_NEW
        ) AS combined_data
        ORDER BY CO_YEAR DESC;";
            // Fetch distinct CO_YEAR values from both tables
            DataTable coYearTable = helper.FetchDataTableFromSYADataBase(query);
            // Create a new row for "All" and add it at the top
            DataRow allRow = coYearTable.NewRow();
            allRow["CO_YEAR"] = "All";
            coYearTable.Rows.InsertAt(allRow, 0); // Insert "All" as the first row
            // Bind the fetched values to the combo box
            CB_YEAR.DataSource = coYearTable;
            CB_YEAR.DisplayMember = "CO_YEAR"; // Column to display in the combo box
            CB_YEAR.ValueMember = "CO_YEAR";   // Value to bind in the combo box
            // Set the default selected item to "All"
            CB_YEAR.SelectedIndex = 0;
            CB_YEAR.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CB_YEAR.AutoCompleteSource = AutoCompleteSource.ListItems;
            CB_YEAR.SelectedIndexChanged += CB_YEAR_SelectedIndexChanged;
        }
        private void CB_NAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCoYear = CB_NAME.SelectedValue.ToString();
            // Reload data based on selected CO_YEAR
            LoadDataBasedOnCONAME(selectedCoYear);
        }
        private void CB_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCoYear = CB_YEAR.SelectedValue.ToString();
            // Reload data based on selected CO_YEAR
            LoadDataBasedOnCOYear(selectedCoYear);
        }
        private void LoadDataBasedOnCONAME(string AC_NAME)
        {
            // Reset DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            // Reset the current offset
            currentOffset = 0;
            // Build the query directly based on selected CO_YEAR
            string query = "";
            if (AC_NAME != "All")
            {
                // Query with WHERE clause for a specific CO_YEAR
                WHERE_SALE = $" WHERE AC_NAME = '{AC_NAME}'";
                WHERE_MD = $" WHERE AC_NAME = '{AC_NAME}'";
            }
            else
            {
                // Query without WHERE clause for "All" CO_YEAR
                WHERE_SALE = "";
                WHERE_MD = "";
            }
            // Fetch the filtered data from the database
            currentOffset = 0;
            loadData();
        }
        private void LoadDataBasedOnCOYear(string coYear)
        {
            // Reset DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            // Reset the current offset
            currentOffset = 0;
            // Build the query directly based on selected CO_YEAR
            string query="";
            if (coYear != "All")
            {
                // Query with WHERE clause for a specific CO_YEAR
                WHERE_SALE = $" WHERE CO_YEAR = '{coYear}'";
                WHERE_MD = $" WHERE CO_YEAR = '{coYear}'";
            }
            else
            {
                // Query without WHERE clause for "All" CO_YEAR
                WHERE_SALE = "";
                WHERE_MD = "";
            }
            // Fetch the filtered data from the database
            currentOffset = 0;
            loadData();
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
            // Adjust column widths
            AdjustColumnWidths();
        }
        private void AdjustColumnWidths()
        {
            // Set auto-sizing for all columns initially
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // Manually override specific column widths
            if (dataGridView1.Columns.Contains("METAL_TYPE"))
            {
                dataGridView1.Columns["METAL_TYPE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["METAL_TYPE"].Width = 50;
            }
            if (dataGridView1.Columns.Contains("PURITY"))
            {
                dataGridView1.Columns["PURITY"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["PURITY"].Width = 50;
            }
            if (dataGridView1.Columns.Contains("ITEM_TYPE"))
            {
                dataGridView1.Columns["ITEM_TYPE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["ITEM_TYPE"].Width = 50;
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
                ("GW", typeof(float)),
                ("NW", typeof(float)),
                ("LBR_RATE", typeof(int)),
                ("OTH_AMT", typeof(int)),
                ("LBR_AMT", typeof(int)),
                ("SIZE", typeof(string)),
                ("PRICE", typeof(int)),
                ("COMMENT", typeof(string)),
                ("ITM_RAT", typeof(int)),
                ("ITM_AMT", typeof(int)),
                ("AC_CODE", typeof(string)),
                ("AC_NAME", typeof(string)),
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
            string query = SELECT + CN_SALE + FROM_SALE + WHERE_SALE + UNION_SELECT + CN_MD + FROM_MD + WHERE_MD + COMBINE_DATA + WHERE_ALL+orderByQuery + $" LIMIT {PageSize} OFFSET {currentOffset};";
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
                            if (DateTime.TryParse(dateString, out DateTime parsedDate))
                            {
                                newRow[column.ColumnName] = parsedDate.ToString("dd-MM-yy");
                            }
                            else
                            {
                                newRow[column.ColumnName] = dateString;
                            }
                        }
                        else if (row[column] is DateTime dateValue)
                        {
                            newRow[column.ColumnName] = dateValue.ToString("dd-MM-yy");
                        }
                        else
                        {
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
        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                DataGridViewRow gridViewRow = dataGridView1.Rows[i];
                gridViewRow.Height = 50;
                if (gridViewRow.DataBoundItem is DataRowView dataRowView)
                {
                    DataRow dataRow = dataRowView.Row;
                    if (dataRow["CO_BOOK"] != DBNull.Value)
                    {
                        string coBookValue = dataRow["CO_BOOK"].ToString();
                        if (coBookValue == "026" || coBookValue == "027")
                        {
                            gridViewRow.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                            gridViewRow.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (coBookValue == "015")
                        {
                            if (dataRow["METAL_TYPE"] != DBNull.Value)
                            {
                                string metalType = dataRow["METAL_TYPE"].ToString().ToUpper();
                                if (metalType == "G")
                                {
                                    gridViewRow.DefaultCellStyle.BackColor = Color.Gold;
                                }
                                else if (metalType == "S")
                                {
                                    gridViewRow.DefaultCellStyle.BackColor = Color.Silver;
                                }
                            }
                        }
                    }
                    if (gridViewRow.DefaultCellStyle.BackColor == Color.Empty)
                    {
                        gridViewRow.DefaultCellStyle.BackColor = i % 2 == 0 ? Color.LightGray : Color.White;
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