using System.Data;
namespace SYA
{
    public partial class silverStockDetailedSummary : Form
    {
        public silverStockDetailedSummary()
        {
            InitializeComponent();
        }
        private void silverStocksDetailedSummary_Load(object sender, EventArgs e)
        {
            SilverDetailSummary();
        }
        public void SilverDetailSummary()
        {
            string query = "SELECT * FROM MAIN_DATA WHERE IT_TYPE = 'S'";
            DataTable MAIN_DATA = helper.FetchDataTableFromSYADataBase(query);
            DataTable Table_SLO = MAIN_DATA.Clone();
            PopulateTable(Table_SLO, "SLO", "ITEM_PURITY");
            StockHelper.LoadDataGridView(Table_SLO, dataGridView1, "S");
            DataTable Table_925 = MAIN_DATA.Clone();
            PopulateTable(Table_925, "925", "ITEM_PURITY");
            StockHelper.LoadDataGridView(Table_925, dataGridView2, "S");
            DataTable Frame = MAIN_DATA.Clone();
            PopulateFrameTable(Frame, "FRAMES & GIFTS", "ITEM_DESC");
            StockHelper.LoadFrameDataGridView(Frame, dataGridView3, "S");
            dataGridView1.Sort(dataGridView1.Columns["ITEM_NAME"], System.ComponentModel.ListSortDirection.Ascending);
            dataGridView2.Sort(dataGridView2.Columns["ITEM_NAME"], System.ComponentModel.ListSortDirection.Ascending);
            dataGridView3.Sort(dataGridView3.Columns["ITEM_NAME"], System.ComponentModel.ListSortDirection.Ascending);
            void PopulateFrameTable(DataTable dt, string purity, string columnName)
            {
                foreach (System.Data.DataRow row in MAIN_DATA.Rows)
                {
                    if (row[columnName].ToString() == purity)
                    {
                        dt.ImportRow(row);
                    }
                }
            }
            void PopulateTable(DataTable dt, string purity,string columnName)
            {
                foreach (System.Data.DataRow row in MAIN_DATA.Rows)
                {
                    if (row[columnName].ToString() == purity)
                    {
                        dt.ImportRow(row);
                    }
                }
            }
        }
        public void loadDG(DataTable dt, DataGridView dg)
        {
            StockHelper.LoadDataGridView(dt, dg, "S");
        }
        private void StyleDataGridView(DataGridView dgv)
        {
            StockHelper.StyleDataGridView(dgv);
        }
    }
}
