using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYA
{
    public partial class RepairDetailsForm : Form
    {
        private int repairId;  // Store the repair ID

        public RepairDetailsForm(int repairId)
        {
            InitializeComponent();
            this.repairId = repairId;
        }

        // Default constructor (if needed)
        public RepairDetailsForm()
        {
            InitializeComponent();
        }

        private void RepairDetailsForm_Load(object sender, EventArgs e)
        {
            // Load record details (as shown earlier)
            LoadDetails();

            // Initialize ComboBox data
            LoadComboBoxData(cbType, "TYPE");
            LoadComboBoxData(cbSubType, "SUB_TYPE");
            LoadComboBoxData(cbCreatedBy, "USER");
            LoadComboBoxData(cbPriority, "PRIORITY");

            // Attach numeric validations
            txtWeight.KeyPress += AllowOnlyNumeric;
            txtNumber.KeyPress += AllowOnlyNumeric;
            txtCost.KeyPress += AllowOnlyNumeric; // Assuming txtCost is similar to txtEstimate in AddReparing

            txtWeight.Leave += FormatDecimal;
            txtNumber.Leave += FormatDecimal;
            txtCost.Leave += FormatDecimal;

            // Attach key press for automatic uppercasing (if needed)
            AttachKeyPressEvent(this);
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

        private void AllowOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && txtBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void FormatDecimal(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (decimal.TryParse(txtBox.Text, out decimal result))
            {
                if (txtBox.Text.Contains("."))
                {
                    txtBox.Text = result.ToString("0.000");
                }
            }
            else
            {
                txtBox.Text = "0";
            }
        }

        private void AttachKeyPressEvent(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txtBox)
                {
                    txtBox.KeyPress += EditingControl_KeyPress;
                }
                else if (ctrl.HasChildren)
                {
                    AttachKeyPressEvent(ctrl);
                }
            }
        }

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void LoadDetails()
        {
            // Build your query. Adjust the column names as needed.
            string query = $"SELECT * FROM RepairingData WHERE ID = {repairId}";

            // Fetch the data using your helper function
            DataTable dt = helper.FetchDataTableFromSYADataBase(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // Populate textboxes and other controls with data from the database
                txtName.Text = row["NAME"].ToString();
                txtNumber.Text = row["NUMBER"].ToString();
                txtWeight.Text = row["WEIGHT"].ToString();
                txtCost.Text = row["ESTIMATE_COST"].ToString();
                rtxtComment.Text = row["COMMENT"].ToString();

                // For ComboBoxes, you might need to ensure the item exists or set the Text property:
                cbType.Text = row["TYPE"].ToString();
                cbSubType.Text = row["SUB_TYPE"].ToString();
                cbCreatedBy.Text = row["CREATED_BY"].ToString();
                cbPriority.Text = row["PRIORITY"].ToString();

                // Set date controls – assuming your database stores dates in a valid format
                lblBookingDate.Text = row["BOOK_DATE"].ToString();
                lblUpdateDate.Text = row["UPDATE_TIME"].ToString();

                // Set the DateTimePicker for the delivery date if available
                DateTime deliveryDate;
                if (DateTime.TryParse(row["DELIVERY_DATE"].ToString(), out deliveryDate))
                {
                    lblDeliveryDate.Value = deliveryDate;
                }

                // Optionally load an image into the PictureBox if you have an IMAGE_PATH column
                if (row.Table.Columns.Contains("IMAGE_PATH"))
                {
                    string imagePath = row["IMAGE_PATH"].ToString();
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        // Optionally, set a default image or clear the picture box
                        pictureBox1.Image = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("No details found for the selected repair item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateRepairRecord();
        }

        private void UpdateRepairRecord()
        {
            // Gather values from the form fields. Use appropriate conversion and validations as needed.
            string NAME = txtName.Text.Trim();
            string NUMBER = string.IsNullOrWhiteSpace(txtNumber.Text) ? "NULL" : $"'{txtNumber.Text.Trim()}'";
            string WEIGHT = string.IsNullOrWhiteSpace(txtWeight.Text) ? "NULL" : txtWeight.Text.Trim();
            string ESTIMATE_COST = string.IsNullOrWhiteSpace(txtCost.Text) ? "NULL" : txtCost.Text.Trim();
            // Assume BOOK_DATE is not updated – it remains the original booking date, which you may have stored or displayed.
            string DELIVERY_DATE = lblDeliveryDate.Value.ToString("yyyy-MM-dd");
            string UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string TYPE = string.IsNullOrEmpty(cbType.Text) ? "''" : $"'{cbType.Text.Trim()}'";
            string SUB_TYPE = string.IsNullOrEmpty(cbSubType.Text) ? "''" : $"'{cbSubType.Text.Trim()}'";
            string CREATED_BY = string.IsNullOrEmpty(cbCreatedBy.Text) ? "''" : $"'{cbCreatedBy.Text.Trim()}'";
            string PRIORITY = string.IsNullOrEmpty(cbPriority.Text) ? "''" : $"'{cbPriority.Text.Trim()}'";
            string COMMENT = $"'{rtxtComment.Text.Trim()}'"; // Enclose in quotes

            // Build your UPDATE query. Adjust field names as required.
            string updateQuery = $@"
        UPDATE RepairingData SET 
            NAME = '{NAME}',
            NUMBER = {NUMBER},
            WEIGHT = {WEIGHT},
            ESTIMATE_COST = {ESTIMATE_COST},
            DELIVERY_DATE = '{DELIVERY_DATE}',
            UPDATE_TIME = '{UPDATE_TIME}',
            TYPE = {TYPE},
            SUB_TYPE = {SUB_TYPE},
            CREATED_BY = {CREATED_BY},
            PRIORITY = {PRIORITY},
            COMMENT = {COMMENT}
        WHERE ID = {repairId}";  // repairId should have been set when the form was constructed

            // Execute the query using your helper method and check affected rows.
            object affectedRows = helper.RunQueryWithoutParametersSYADataBase(updateQuery, "ExecuteNonQuery");
            if (affectedRows != null && Convert.ToInt32(affectedRows) > 0)
            {
                MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
