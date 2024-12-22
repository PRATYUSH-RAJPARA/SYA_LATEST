using System;
using System.Data;
using System.Windows.Forms;

namespace SYA.CODE.Search
{
    public partial class SearchNew : Form
    {
        searchHelper searchHelper = new searchHelper();
        private const int PageSize = 200; // Number of rows to load per page
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
                    newRow[column.ColumnName] = row[column];
                }
                destination.Rows.Add(newRow);
            }
        }



        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           searchHelper.DataGridView1_CellEndEdit_ForEnterKeyHandle(sender,e);
        }

        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            searchHelper.DataGridView1_CellEnter_ForEnterKeyHandle();
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            searchHelper.DataGridView1_KeyDown_ForEnterKeyHandle(dataGridView1,e);
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
