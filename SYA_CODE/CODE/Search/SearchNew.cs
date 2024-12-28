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
            bindComboBoxes();
        }
        private void bindComboBoxes()
        {
            BindCOYearComboBox();
            BindACNameComboBox();
            BindTAGNOComboBox();
            BindHUIDComboBox();
            BindBillNoComboBox();
            BindWEIGHTComboBox();
            void BindACNameComboBox()
            {
                string query = @"
                SELECT DISTINCT AC_NAME
                FROM SALE_DATA_NEW 
                ORDER BY AC_NAME ASC;";
                BindComboBox(CB_NAME, query, "AC_NAME", "AC_NAME", "All", CB_NAME_TextChanged, CB_NAME_SelectedIndexChanged);
            }
            void BindCOYearComboBox()
            {
                string query = @"
                SELECT DISTINCT CO_YEAR
                FROM (
                    SELECT CO_YEAR FROM SALE_DATA_NEW
                    UNION
                    SELECT CO_YEAR FROM MAIN_DATA_NEW
                ) AS combined_data
                ORDER BY CO_YEAR DESC;";
                BindComboBox(CB_YEAR, query, "CO_YEAR", "CO_YEAR", "All", CB_YEAR_TextChanged, CB_YEAR_SelectedIndexChanged);
            }
            void BindTAGNOComboBox()
            {
                string query = @"
                SELECT DISTINCT TAG_NO
                FROM (
                    SELECT TAG_NO FROM SALE_DATA_NEW
                    UNION
                    SELECT TAG_NO FROM MAIN_DATA_NEW
                ) AS combined_data
                ORDER BY TAG_NO ASC;";
                BindComboBox(CB_TAGNO, query, "TAG_NO", "TAG_NO", "All", CB_TAGNO_TextChanged, CB_TAGNO_SelectedIndexChanged);
            }
            void BindHUIDComboBox()
            {
                string query = @"
                SELECT DISTINCT HUID1,HUID2,HUID3
                FROM (
                    SELECT HUID1,HUID2,HUID3 FROM SALE_DATA_NEW
                    UNION
                    SELECT HUID1,HUID2,HUID3 FROM MAIN_DATA_NEW
                ) AS combined_data
                ORDER BY HUID1 ASC;";
                BindComboBox(CB_HUID, query, "HUID1", "HUID1", "All", CB_HUID_TextChanged, CB_HUID_SelectedIndexChanged, AutoCompleteMode.None);
            }
            void BindWEIGHTComboBox()
            {
                string query = @"
                SELECT DISTINCT GW,NW
                FROM (
                    SELECT GW,NW FROM SALE_DATA_NEW
                    UNION
                    SELECT GW,NW FROM MAIN_DATA_NEW
                ) AS combined_data
                ORDER BY GW ASC;";
                BindComboBox(CB_WEIGHT, query, "GW", "GW", "All", CB_WEIGHT_TextChanged, CB_WEIGHT_SelectedIndexChanged, AutoCompleteMode.None);
            }
            void BindBillNoComboBox()
            {
                string query = @"
                SELECT DISTINCT VCH_NO
                FROM (
                    SELECT VCH_NO FROM SALE_DATA_NEW
                    UNION
                    SELECT VCH_NO FROM MAIN_DATA_NEW
                ) AS combined_data
                ORDER BY VCH_NO ASC;";
                BindComboBox(CB_BILLNO, query, "VCH_NO", "VCH_NO", "All", CB_BILLNO_TextChanged, CB_BILLNO_SelectedIndexChanged, AutoCompleteMode.None);
            }
            void BindComboBox(ComboBox comboBox, string query, string displayMember, string valueMember, string allText, EventHandler textChangedHandler, EventHandler selectedIndexChangedHandler, AutoCompleteMode autoCompleteMode = AutoCompleteMode.Suggest)
            {
                // Fetch data using the query
                DataTable dataTable = helper.FetchDataTableFromSYADataBase(query);
                // Add "All" as the first row
                if (displayMember != "GW")
                {
                    DataRow allRow = dataTable.NewRow();
                    allRow[displayMember] = allText;
                    dataTable.Rows.InsertAt(allRow, 0);
                }
                if (displayMember == "CO_YEAR")
                {
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = 0;
                    comboBox.AutoCompleteMode = autoCompleteMode;
                    comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                if (textChangedHandler != null)
                    comboBox.TextChanged += textChangedHandler;
                //if (selectedIndexChangedHandler != null)
                //    comboBox.SelectedIndexChanged += selectedIndexChangedHandler;
            }
             void CB_NAME_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_NAME, "AC_NAME");
            }
             void CB_YEAR_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_YEAR, "CO_YEAR");
            }
             void CB_TAGNO_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_TAGNO, "TAG_NO");
            }
             void CB_HUID_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_HUID, "HUID1");
            }
             void CB_WEIGHT_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_WEIGHT, "GW");
            }
             void CB_BILLNO_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_BILLNO, "VCH_NO");
            }
             void CB_HUID_TextChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_HUID, "HUID1");
            }
             void CB_WEIGHT_TextChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_WEIGHT, "GW");
            }
             void CB_BILLNO_TextChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_BILLNO, "VCH_NO");
            }
             void CB_TAGNO_TextChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_TAGNO, "TAG_NO");
            }
             void CB_NAME_TextChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_NAME, "AC_NAME");
            }
             void CB_YEAR_TextChanged(object sender, EventArgs e)
            {
                LoadDataBasedOnComboBoxValue(CB_YEAR, "CO_YEAR");
            }
             void LoadDataBasedOnComboBoxValue(ComboBox CB, string columnName)
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
                    if (columnName == "HUID1")
                    {
                        WHERE_SALE = $" WHERE HUID1  LIKE '%{typedText}%' OR HUID2 LIKE '%{typedText}%' OR HUID3 LIKE '%{typedText}%' ";
                        WHERE_MD = WHERE_SALE;
                    }
                    else if (columnName == "GW")
                    {
                        WHERE_SALE = $" WHERE GW  LIKE '%{typedText}%' OR NW LIKE '%{typedText}%' ";
                        WHERE_MD = WHERE_SALE;
                    }
                    else
                    {
                        WHERE_SALE = $" WHERE {columnName}  LIKE '%{typedText}%'";
                        WHERE_MD = WHERE_SALE;
                    }
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
        }
        private void loadData()
        {
            InitializeDataGridView();
            LoadInitialData();
            searchStyling.CustomizeDataGridView(dataGridView1);
            //  ApplyColorsForSalesData(dataGridView1);
        }
        private void ApplyColorsForSalesData(DataGridView dataGridView)
        {
            dataGridView.Columns["CO_YEAR"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#212529");    // Eerie Black
            dataGridView.Columns["CO_BOOK"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#25292D");    // Gunmetal
            dataGridView.Columns["VCH_NO"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2A2E32");     // Gunmetal-2
            dataGridView.Columns["VCH_DATE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2E3236");    // Onyx
            dataGridView.Columns["PURITY"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#33373A");       // Onyx-2
            dataGridView.Columns["METAL_TYPE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#373B3F");    // Onyx-3
            dataGridView.Columns["TAG_NO"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#3C3F43");       // Onyx-4
            dataGridView.Columns["HUID1"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#404448");        // Outer Space
            dataGridView.Columns["HUID2"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#45484C");        // Outer Space-2
            dataGridView.Columns["HUID3"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#494D50");        // Outer Space-3
            dataGridView.Columns["ITEM_TYPE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4E5155");     // Davy's Gray
            dataGridView.Columns["GW"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#525659");           // Davy's Gray-2
            dataGridView.Columns["NW"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#565A5D");           // Davy's Gray-3
            dataGridView.Columns["LBR_RATE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#5B5E62");      // Davy's Gray-4
            dataGridView.Columns["OTH_AMT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#5F6366");       // Dim Gray
            dataGridView.Columns["LBR_AMT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#64676A");       // Dim Gray-2
            dataGridView.Columns["SIZE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#686C6F");          // Dim Gray-3
            dataGridView.Columns["PRICE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#6D7073");         // Dim Gray-4
            dataGridView.Columns["COMMENT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#717478");        // Dim Gray-5
            dataGridView.Columns["ITM_RAT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#76797C");        // Gray
            dataGridView.Columns["ITM_AMT"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7A7D80");        // Gray-2
            dataGridView.Columns["AC_CODE"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7F8285");        // Gray-3
            dataGridView.Columns["AC_NAME"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#838689");        // Gray-4
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.DefaultCellStyle.ForeColor = Color.White;
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
            EnterKeyNavigation.EnterKeyHandle_EventHandler(dataGridView1);
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
            searchStyling.DataGridView1_RowsAdded(sender, e, dataGridView1);
        }
        public DataTable LoadNextPage()
        {
            string query = SELECT + CN_SALE + FROM_SALE + WHERE_SALE + UNION_SELECT + CN_MD + FROM_MD + WHERE_MD + COMBINE_DATA + WHERE_ALL + orderByQuery;
            return SearchPaginationHelper.LoadNextPage(query);
        }
        public void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            string query = SELECT + CN_SALE + FROM_SALE + WHERE_SALE + UNION_SELECT + CN_MD + FROM_MD + WHERE_MD + COMBINE_DATA + WHERE_ALL + orderByQuery;
            SearchPaginationHelper.DataGridView1_Scroll(sender, e, dataGridView1, query)
;
        }
    }
}