using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
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

            LoadComboBoxData(cbType, "TYPE");
            LoadComboBoxData(cbSubType, "SUB_TYPE");
            LoadComboBoxData(cbCreatedBy, "USER");
            LoadComboBoxData(cbPriority, "PRIORITY");

            txtWeight.KeyPress += AllowOnlyNumeric;
            txtNumber.KeyPress += AllowOnlyNumeric;
            txtEstimate.KeyPress += AllowOnlyNumeric;

            txtWeight.Leave += FormatDecimal;
            txtNumber.Leave += FormatDecimal;
            txtEstimate.Leave += FormatDecimal;

            // Attach KeyPress event to all TextBoxes (excluding RichTextBox)
            AttachKeyPressEvent(this);
        }

        // Recursively attach KeyPress event to all TextBox controls
        private void AttachKeyPressEvent(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txtBox) // Exclude RichTextBox
                {
                    txtBox.KeyPress += EditingControl_KeyPress;
                }
                else if (ctrl.HasChildren) // Check inside panels, group boxes, etc.
                {
                    AttachKeyPressEvent(ctrl);
                }
            }
        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar)) // If it's a letter
            {
                e.KeyChar = char.ToUpper(e.KeyChar); // Convert to uppercase
            }
        }

        private void AllowOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            // Allow only digits, control keys (like backspace), and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Prevent non-numeric and non-decimal characters
            }

            // If the character is a decimal point, ensure there's only one in the text
            if (e.KeyChar == '.' && txtBox.Text.Contains("."))
            {
                e.Handled = true; // Prevent entering more than one decimal point
            }
        }
        private void FormatDecimal(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            // Check if the value is not empty and is a valid decimal
            if (decimal.TryParse(txtBox.Text, out decimal result))
            {
                // If there's a decimal point, format to 3 decimal places
                if (txtBox.Text.Contains("."))
                {
                    txtBox.Text = result.ToString("0.000");
                }
            }
            else
            {
                // If invalid input, reset to zero
                txtBox.Text = "0";
            }
        }



        private void LoadComboBoxData(ComboBox comboBox, string groupType)
        {
            try
            {
                string query = $"SELECT NAME FROM RepairingHelper WHERE [GROUP] = '{groupType}'";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);

                comboBox.Items.Clear(); // Clear existing items
                AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();

                foreach (DataRow row in dt.Rows)
                {
                    string name = row["NAME"].ToString();
                    comboBox.Items.Add(name); // Add to ComboBox
                    autoCompleteCollection.Add(name); // Add to AutoComplete
                }

                // Enable AutoComplete
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCollection;

                comboBox.DropDownStyle = ComboBoxStyle.DropDown; // Allows user input
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {groupType}: {ex.Message}");
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // If the active control is the Save button, trigger save action
                if (this.ActiveControl == buttonSave)
                {
                    buttonSave.PerformClick(); // Simulate button click
                    return true; // Prevent default Enter key behavior
                }
                else
                {
                    // Move to the next control in tab order
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    return true; // Prevent system default behavior
                }
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
            void FillMobileNumber()
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
        }
        private void update_insert()
        {

            string NAME = txtName.Text.Trim();
            string NUMBER = string.IsNullOrWhiteSpace(txtNumber.Text) ? "NULL" : txtNumber.Text.Trim();
            string WEIGHT = string.IsNullOrWhiteSpace(txtWeight.Text) ? "NULL" : txtWeight.Text.Trim();
            string ESTIMATE_COST = string.IsNullOrWhiteSpace(txtEstimate.Text) ? "NULL" : txtEstimate.Text.Trim();
            string BOOK_DATE = DateTime.Today.ToString("yyyy-MM-dd"); // Current date only
            string DELIVERY_DATE = dtDeliveryDate.Value.ToString("yyyy-MM-dd");
            string BOOK_TIME = DateTime.Now.ToString("HH:mm:ss");
            string UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string TYPE = cbType.Text.ToString() ?? "";
            string SUB_TYPE = cbSubType.Text.ToString() ?? "";
            string CREATED_BY = cbCreatedBy.Text.ToString() ?? "";
            string PRIORITY = cbPriority.Text.ToString() ?? "";
            string IMAGE_PATH = "";
            string COMMENT = rtComment.Text.Trim();
            string STATUS = "NEW";
            string insertQuery = "INSERT INTO RepairingData (NAME, NUMBER, WEIGHT, ESTIMATE_COST, BOOK_DATE, DELIVERY_DATE, BOOK_TIME, UPDATE_TIME, TYPE, SUB_TYPE, CREATED_BY, PRIORITY, IMAGE_PATH, COMMENT, STATUS) " +
                     "VALUES ('" + NAME + "', " + NUMBER + ", " + WEIGHT + ", " + ESTIMATE_COST + ", '" + BOOK_DATE + "', '" + DELIVERY_DATE + "', '" + BOOK_TIME + "', '" + UPDATE_TIME + "', " +
                     "'" + TYPE + "', '" + SUB_TYPE + "', '" + CREATED_BY + "', '" + PRIORITY + "', '" + IMAGE_PATH + "', '" + COMMENT + "', '" + STATUS + "')";
            helper.RunQueryWithoutParametersSYADataBase(insertQuery, "ExecuteNonQuery");
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            update_insert();
            this.Close();
        }

        private void dtDeliveryDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
