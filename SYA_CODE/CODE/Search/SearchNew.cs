using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace SYA
{
    public partial class SearchNew : Form
    {
        EnterKeyNavigation EnterKeyNavigation = new EnterKeyNavigation();
        SearchPaginationHelper SearchPaginationHelper = new SearchPaginationHelper();
        SearchStyling searchStyling = new SearchStyling();
        SearchHelper searchHelper = new SearchHelper();
        string SELECT = " SELECT ";
        string CN_SALE = @" ID, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, HUID3,ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, COMMENT, ITM_RAT,  ITM_AMT, AC_CODE,AC_NAME ";
        string FROM_SALE = @" FROM (  SELECT * FROM SALE_DATA_NEW ";
        string WHERE_SALE = @"  ";
        string UNION_SELECT = "  UNION SELECT ";
        string CN_MD = " ID, CO_YEAR, CO_BOOK, VCH_NO, VCH_DATE, PURITY, METAL_TYPE, TAG_NO, DESIGN, HUID1, HUID2, HUID3, ITM_SIZE, ITEM_TYPE, ITM_PCS, GW, NW, LBR_RATE, OTH_AMT, LBR_AMT, SIZE, PRICE, COMMENT, NULL AS ITM_RAT, NULL AS ITM_AMT, NULL AS AC_CODE, NULL AS AC_NAME ";
        string FROM_MD = "   FROM MAIN_DATA_NEW ";
        string WHERE_MD = @"  ";
        string COMBINE_DATA = @"  ) AS combined_data  ";
        string WHERE_ALL = " ";
        string orderByQuery = "  ORDER BY CO_YEAR DESC, VCH_DATE DESC ";
        public SearchNew()
        {
            InitializeComponent();
        }
        private void SearchNew_Load(object sender, EventArgs e)
        {
            loadData();
            AttachEventHandlers();
            dataGridView1.RowsAdded += DataGridView1_RowsAdded; // Attach event handler for row addition
            BindCOYearComboBox();
            BindACNameComboBox();
            BindTAGNOComboBox();
            BindHUIDComboBox();
        }
        private void loadData()
        {
            InitializeDataGridView();
            LoadInitialData();
           searchStyling.CustomizeDataGridView(dataGridView1);
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
            CB_NAME.AutoCompleteMode = AutoCompleteMode.Suggest;
            CB_NAME.AutoCompleteSource = AutoCompleteSource.ListItems;
            CB_NAME.TextChanged += CB_NAME_TextChanged;

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
            CB_YEAR.AutoCompleteMode = AutoCompleteMode.Suggest;
            CB_YEAR.AutoCompleteSource = AutoCompleteSource.ListItems;
            CB_YEAR.TextChanged += CB_YEAR_TextChanged;
            CB_YEAR.SelectedIndexChanged += CB_YEAR_SelectedIndexChanged;
        }

        private void BindTAGNOComboBox()
        {
            string query = @"
        SELECT DISTINCT TAG_NO
        FROM (
            SELECT TAG_NO FROM SALE_DATA_NEW
            UNION
            SELECT TAG_NO FROM MAIN_DATA_NEW
        ) AS combined_data
        ORDER BY TAG_NO ASC;";
            // Fetch distinct CO_YEAR values from both tables
            DataTable TAGNOTable = helper.FetchDataTableFromSYADataBase(query);
            // Create a new row for "All" and add it at the top
            DataRow allRow = TAGNOTable.NewRow();
            allRow["TAG_NO"] = "All";
            TAGNOTable.Rows.InsertAt(allRow, 0); // Insert "All" as the first row
            // Bind the fetched values to the combo box
            CB_TAGNO.DataSource = TAGNOTable;
            CB_TAGNO.DisplayMember = "TAG_NO"; // Column to display in the combo box
            CB_TAGNO.ValueMember = "TAG_NO";   // Value to bind in the combo box
            // Set the default selected item to "All"
            CB_TAGNO.SelectedIndex = 0;
            CB_TAGNO.AutoCompleteMode = AutoCompleteMode.Suggest;
            CB_TAGNO.AutoCompleteSource = AutoCompleteSource.ListItems;
            CB_TAGNO.TextChanged += CB_TAGNO_TextChanged;
            CB_TAGNO.SelectedIndexChanged += CB_TAGNO_SelectedIndexChanged;
        }
        private void BindHUIDComboBox()
        {
            string query = @"
        SELECT DISTINCT HUID1
        FROM (
            SELECT HUID1 FROM SALE_DATA_NEW
            UNION
            SELECT HUID1 FROM MAIN_DATA_NEW
        ) AS combined_data
        ORDER BY HUID1 ASC;";
            // Fetch distinct HUID values from the database
            DataTable HUIDTable = helper.FetchDataTableFromSYADataBase(query);
            // Add "All" as the first row
            DataRow allRow = HUIDTable.NewRow();
            allRow["HUID1"] = "All";
            HUIDTable.Rows.InsertAt(allRow, 0);
            // Bind the fetched data to the combo box
            CB_HUID.DataSource = HUIDTable;
            CB_HUID.DisplayMember = "HUID1";
            CB_HUID.ValueMember = "HUID1";
            // Set default properties
            CB_HUID.AutoCompleteMode = AutoCompleteMode.None;
            CB_HUID.AutoCompleteSource = AutoCompleteSource.ListItems;
            // Attach event for filtering
            CB_HUID.TextChanged += CB_HUID_TextChanged;
            CB_HUID.SelectedIndexChanged += CB_HUID_SelectedIndexChanged;

        }
        private void CB_NAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_NAME, "AC_NAME");

        }
        private void CB_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_YEAR, "CO_YEAR");

        }
        private void CB_TAGNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_TAGNO, "TAG_NO");

        }
        private void CB_HUID_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_HUID, "HUID1");

        }

        private void CB_HUID_TextChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_HUID, "HUID1");
        }
        private void CB_TAGNO_TextChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_TAGNO, "TAG_NO");
        }
        private void CB_NAME_TextChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_NAME, "AC_NAME");
        }
        private void CB_YEAR_TextChanged(object sender, EventArgs e)
        {
            LoadDataBasedOnComboBoxValue(CB_YEAR, "CO_YEAR");
        }
        private void LoadDataBasedOnComboBoxValue(ComboBox CB, string columnName)
        {
            string typedText = CB.Text.ToString();
            // Reset DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            // Reset the current offset
            // Build the query directly based on selected CO_YEAR
            if (typedText != "All")
            {
                // Query with WHERE clause for a specific CO_YEAR
                WHERE_SALE = $" WHERE {columnName}  LIKE '%{typedText}%'";
                WHERE_MD = $" WHERE {columnName}  LIKE '%{typedText}%'";
            }
            else
            {
                // Query without WHERE clause for "All" CO_YEAR
                WHERE_SALE = "";
                WHERE_MD = "";
            }
            // Fetch the filtered data from the database
            SearchPaginationHelper.setCurrentOffset(0);
            loadData();
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
            EnterKeyNavigation.EnterKeyHandle_EventHandler(dataGridView1);
        }
        private void LoadInitialData()
        {
           SearchPaginationHelper.loadedTable = CreateHardcodedDataTable();
            dataGridView1.DataSource = LoadNextPage(); 
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
        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
          searchStyling.DataGridView1_RowsAdded(sender, e,dataGridView1);
        }
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            EnterKeyNavigation.DataGridView1_CellEndEdit_ForEnterKeyHandle(sender, e);
        }
        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            EnterKeyNavigation.DataGridView1_CellEnter_ForEnterKeyHandle();
        }
        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            EnterKeyNavigation.DataGridView1_KeyDown_ForEnterKeyHandle(dataGridView1, e);
        }
        public DataTable LoadNextPage()
        {
            string query = SELECT + CN_SALE + FROM_SALE + WHERE_SALE + UNION_SELECT + CN_MD + FROM_MD + WHERE_MD + COMBINE_DATA + WHERE_ALL + orderByQuery;
            return SearchPaginationHelper.LoadNextPage(query);
        }
        public void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            string query = SELECT + CN_SALE + FROM_SALE + WHERE_SALE + UNION_SELECT + CN_MD + FROM_MD + WHERE_MD + COMBINE_DATA + WHERE_ALL + orderByQuery;
            SearchPaginationHelper.DataGridView1_Scroll(sender, e, dataGridView1,query)
;
        }
    }
}