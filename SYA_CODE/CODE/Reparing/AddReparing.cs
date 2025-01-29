using System;
using System.Data;
using System.Windows.Forms;

namespace SYA
{
    public partial class AddReparing : Form
    {
        private AutoCompleteStringCollection nameCollection = new AutoCompleteStringCollection();
        private HashSet<string> nameSet = new HashSet<string>(); // To quickly check if a name exists

        public AddReparing()
        {
            InitializeComponent();
            LoadAutoCompleteNames();
            txtName.TextChanged += TxtName_TextChanged; // Detect changes in txtName
        }
        private void AddReparing_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true; // Ensures the form captures key events

            // Attach KeyDown event to all textboxes

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // Check if Shift is pressed (to go backward like Shift+Tab)
                bool forward = !ModifierKeys.HasFlag(Keys.Shift);

                // Move to the next control based on TabIndex order
                this.SelectNextControl(this.ActiveControl, forward, true, true, true);
                return true; // Prevent default Enter key behavior (like beeping)
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void LoadAutoCompleteNames()
        {
            try
            {
                string query = "SELECT DISTINCT AC_NAME FROM AC_MAST";
                DataTable dt = helper.FetchDataTableFromDataCareDataBase(query);

                if (dt.Rows.Count > 0)
                {
                    nameCollection.Clear();
                    nameSet.Clear(); // Clear existing data

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["AC_NAME"].ToString();
                        nameCollection.Add(name);
                        nameSet.Add(name); // Store in HashSet for quick lookup
                    }

                    txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtName.AutoCompleteCustomSource = nameCollection;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading names: " + ex.Message);
            }
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            if (nameSet.Contains(txtName.Text)) // Only fetch if the name is in the database
            {
                FillMobileNumber();
            }
            else
            {
                txtNumber.Text = ""; // Clear if not a valid selection
            }
        }

        private void FillMobileNumber()
        {
            try
            {
                string query = $"SELECT AC_MOBILE FROM AC_MAST WHERE AC_NAME = '{txtName.Text}'";
                DataTable dt = helper.FetchDataTableFromDataCareDataBase(query);

                if (dt.Rows.Count > 0)
                {
                    txtNumber.Text = dt.Rows[0]["AC_MOBILE"].ToString();
                }
                else
                {
                    txtNumber.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching mobile number: " + ex.Message);
            }
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
