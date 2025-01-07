using SYA;
using Timer = System.Windows.Forms.Timer;
namespace SYA
{
    public partial class main : Form
    {
        private Timer _timer;
        public main()
        {
            InitializeComponent();
            InitializeDataGridView();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(main_KeyDown);
            _timer = new Timer();
            _timer.Interval = 10000; // 10 seconds (in milliseconds)
            _timer.Tick += Timer_Tick;
            // Start the timer
            _timer.Start();
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            await FetchAndDisplayData();
        }
        // Method to fetch and display data
        private async Task FetchAndDisplayData()
        {
            string url = "https://bcast.aaravbullion.in/VOTSBroadcastStreaming/Services/xml/GetLiveRateByTemplateID/aarav";
            try
            {
                //   string response = await GetApiData(url);
                //    DisplayData(response);
            }
            catch (Exception ex)
            {
                //  MessageBox.Show($"Error: {ex.Message}");
            }
        }
        // Function to get data from the API
        private async Task<string> GetApiData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
        // Function to display the data in DataGridView
        private void InitializeDataGridView()
        {
            //   dataGridView1.Columns.Clear();
            //   dataGridView1.Columns.Add("ID", "ID");
            //   dataGridView1.Columns.Add("ItemName", "Item Name");
            //   dataGridView1.Columns.Add("Price1", "Price 1");
            //   dataGridView1.Columns.Add("Price2", "Price 2");
            //   dataGridView1.Columns.Add("Price3", "Price 3");
            //   dataGridView1.Columns.Add("Price4", "Price 4");
        }
        private void DisplayData(string data)
        {
            // Clear the previous data
            //   dataGridView1.Rows.Clear();
            // Split the data into rows and columns
            var rows = data.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in rows)
            {
                var columns = row.Split(new[] { '\t' }, StringSplitOptions.None);
                //     dataGridView1.Rows.Add(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5]);
                // Ensure we have the right number of columns (adjust based on your data)
                // Check if the row matches "GOLD 999.(AMD) WITH TDS"
                if (columns[2].Equals("GOLD 999.(AMD) WITH TDS", StringComparison.OrdinalIgnoreCase))
                {
                    // Retrieve the price (which is in the third column, index 2)
                    string price = columns[2];
                    // Optionally display the price in a label or message box
                    MessageBox.Show($"Price of GOLD 999.(AMD) WITH TDS: {price}");
                    // Add this row to the DataGridView (if you still want to display it)
                    //         dataGridView1.Rows.Add(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5]);
                }
            }
        }
        private void main_Load(object sender, EventArgs e)
        {
            btnStocks.Visible = true;
            //btnRtgs.Visible = false;
            btnImportData.Visible = true;
            btnPrintTags.Visible = false;
            btnCustomer.Visible = false;
            panelsecond.Visible = false;
            button9.Visible = false;
            button8.Visible = false;
            btnDisplayPrice.Visible = false;
            //  btnSortContact.Visible = false;
            //panelchild.visible = false;
            btnHideAllSecondPanelButtons();
            helper.loadSettingsValues();
            helper.loadLabourTable();
        }
        // Loads form by name in panelchild
        public void LoadForm(Form form)
        {
            // Close the currently displayed form (if any)
            if (panelChild.Controls.Count > 0)
            {
                Form currentForm = panelChild.Controls[0] as Form;
                if (currentForm != null)
                {
                    currentForm.Close();
                    currentForm.Dispose();
                }
            }
            // Set the properties of the new form
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            // Add the new form to the panel
            panelChild.Controls.Add(form);
            // Sho the Panel Child
            panelChild.Visible = true;
            // Show the new form
            form.Show();
        }
        // main left panel buttons
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            btnHideAllSecondPanelButtons();
            panelsecond.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
        }
        private void btnSellItem_Click(object sender, EventArgs e)
        {
            btnHideAllSecondPanelButtons();
            panelsecond.Visible = true;
            button6.Visible = true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new Search());
          //  LoadForm(new SearchNew());
        }
        private void btnSales_Click(object sender, EventArgs e)
        {
            btnHideAllSecondPanelButtons();
            panelsecond.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
        }
        private void btnStocks_Click(object sender, EventArgs e)
        {
            btnHideAllSecondPanelButtons();
            panelsecond.Visible = true;
            button13.Visible = true;
            button12.Visible = true;
            button23.Visible = true;
            button7.Visible = true;
        }
        private void btnPrintTags_Click(object sender, EventArgs e)
        {
            btnHideAllSecondPanelButtons();
            panelsecond.Visible = true;
        }
        private void btnRtgs_Click(object sender, EventArgs e)
        {
            btnHideAllSecondPanelButtons();
            panelsecond.Visible = true;
            button17.Visible = true;
            btnEditRTGS.Visible = true;
        }
        private void btnImportData_Click(object sender, EventArgs e)
        {
            btnHideAllSecondPanelButtons();
            panelsecond.Visible = true;
            button20.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button9.Visible = true;
            button8.Visible = true;
        }
        private void btnHideAllSecondPanelButtons()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button9.Visible = false;
            button8.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button13.Visible = false;
            button12.Visible = false;
            button17.Visible = false;
            btnEditRTGS.Visible = false;
            button20.Visible = false;
            button23.Visible = false;
            button22.Visible = false;
            button7.Visible = false;
        }
        // button add gold
        private void button1_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
          //  LoadForm(new addgold_WORKING());
            LoadForm(new addItemNew());
        }
        // BUTTON DATACARE ADD
        private void button2_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            // LoadForm(new Form1()); 
            LoadForm(new addSilver());
        }
        private void button10_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new saleReport());
            //  LoadForm(new Form1());
        }
        private void button6_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new sell());
        }
        private void button13_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new VerifyStock());
        }
        private void btnSortContact_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new ContactDisplay());
            //RichTextBox r = new RichTextBox();
            //Contact.SortContactData(r, "datacare");
        }
        private void button17_Click(object sender, EventArgs e)
        {
            //RichTextBox r = new RichTextBox();
            //Contact contact = new Contact();
            //contact.SortContactData(r, "datacare");
            panelsecond.Visible = false;
            LoadForm(new PrintRTGS());
        }
        private void button11_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new SaleSummary());
        }
        private void button12_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new STOCKSummary());
        }
        private void main_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl+S is pressed
            if (e.Control && e.KeyCode == Keys.S)
            {
                // Open the form
                LoadForm(new settings());
            }
            if (e.Control && e.KeyCode == Keys.L)
            {
                // Open the form
                LoadForm(new Labour());
            }
        }
        private void button22_Click_1(object sender, EventArgs e)
        {
            LoadForm(new PrintLabel());
        }
        private void button23_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new goldStockDetailedSummary());
        }
        private void button7_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new silverStockDetailedSummary());
        }
        private void button20_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            FetchSaleDataHelper.fetchSaleData();
            string queryToFetchFromMSAccess = "SELECT * FROM MAIN_TAG_DATA WHERE CO_BOOK = '015' OR CO_BOOK = '15'";
            HelperFetchData.InsertInStockDataIntoSQLite(queryToFetchFromMSAccess);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            string queryToFetchFromMSAccess = "SELECT * FROM MAIN_TAG_DATA WHERE CO_BOOK = '015' OR CO_BOOK = '15'";
            HelperFetchData.InsertInStockDataIntoSQLite(queryToFetchFromMSAccess);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            FetchSaleDataHelper.fetchSaleData();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LoadForm(new SaleAuto());
        }
        private void button8_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            RichTextBox r = new RichTextBox();
            Contact contact = new Contact();
            contact.SortContactData(r, "datacare");
        }
        private void btnEditRTGS_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new ADDEDITRTGS());
        }
        private void button9_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            RichTextBox r = new RichTextBox();
            Contact contact = new Contact();
            contact.SortContactData(r, "all");
        }
        private void btnDisplayPrice_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new ShowOnDisplayForm());
        }
        private void btnDataCareData_Click(object sender, EventArgs e)
        {
            panelsecond.Visible = false;
            LoadForm(new Fetchalldatafrommsaccess());
        }
        private void btnProcessData_Click(object sender, EventArgs e)
        {
            //            string query = @"
            //    UPDATE MAIN_TAG_DATA
            //    SET TAG_NO = 'OLD' + TAG_NO
            //    WHERE CO_BOOK = '027';
            //";
            //    helper.RunQueryWithoutParametersDataCareDataBase(query, "ExecuteNonQuery");
            HelperFetchData1 obj = new HelperFetchData1();
            obj.ProcessData();
        }
    }
    public class ApiResponseItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CurrentValue { get; set; }
        public string OpenValue { get; set; }
        public string HighValue { get; set; }
        public string LowValue { get; set; }
    }
}
