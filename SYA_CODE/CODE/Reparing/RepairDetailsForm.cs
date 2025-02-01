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
        private string currentStatus;
        private Dictionary<string, Color> statusColors = new Dictionary<string, Color>
        {
            { "New", ColorTranslator.FromHtml("#90e0ef") },
            { "In Progress", ColorTranslator.FromHtml("#ffe94e") },
            { "Completed", ColorTranslator.FromHtml("#a1ef7a") },
            { "Unable to Complete", ColorTranslator.FromHtml("#ec8385") }
        };

        // Event to notify that the record has been updated or deleted
        public event EventHandler RecordUpdated;

        // -----------------------------
        // UI update methods
        // -----------------------------
        private void UpdateStatusUI(string status)
        {
            // Reset all status button colors
            btnTypeNew.BackColor = SystemColors.Control;
            btnTypeNew.ForeColor = Color.Black;
            btnTypeInProgress.BackColor = SystemColors.Control;
            btnTypeInProgress.ForeColor = Color.Black;
            btnTypeCompleted.BackColor = SystemColors.Control;
            btnTypeCompleted.ForeColor = Color.Black;
            btnTypeUnableToComplete.BackColor = SystemColors.Control;
            btnTypeUnableToComplete.ForeColor = Color.Black;

            // Highlight the selected status button
            switch (status)
            {
                case "New":
                    btnTypeNew.BackColor = statusColors["New"];
                    break;
                case "In Progress":
                    btnTypeInProgress.BackColor = statusColors["In Progress"];
                    break;
                case "Completed":
                    btnTypeCompleted.BackColor = statusColors["Completed"];
                    break;
                case "Unable to Complete":
                    btnTypeUnableToComplete.BackColor = statusColors["Unable to Complete"];
                    break;
            }
        }

        private void AttachStatusEventHandlers()
        {
            btnTypeNew.Click += (s, e) => OnStatusButtonClicked("New");
            btnTypeInProgress.Click += (s, e) => OnStatusButtonClicked("In Progress");
            btnTypeCompleted.Click += (s, e) => OnStatusButtonClicked("Completed");
            btnTypeUnableToComplete.Click += (s, e) => OnStatusButtonClicked("Unable to Complete");
        }

        private void OnStatusButtonClicked(string newStatus)
        {
            // Update UI and store new status for saving
            UpdateStatusUI(newStatus);
            currentStatus = newStatus;
        }

        // -----------------------------
        // Constructors
        // -----------------------------
        public RepairDetailsForm(int repairId)
        {
            InitializeComponent();
            this.repairId = repairId;
        }

        // Default constructor if needed
        public RepairDetailsForm()
        {
            InitializeComponent();
        }

        // -----------------------------
        // Form Load and Control Initialization
        // -----------------------------
        private void RepairDetailsForm_Load(object sender, EventArgs e)
        {
            // Load record details and initialize controls
            LoadDetails();
            LoadComboBoxData(cbType, "TYPE");
            LoadComboBoxData(cbSubType, "SUB_TYPE");
            LoadComboBoxData(cbCreatedBy, "USER");
            LoadComboBoxData(cbPriority, "PRIORITY");

            txtWeight.KeyPress += AllowOnlyNumeric;
            txtNumber.KeyPress += AllowOnlyNumeric;
            txtCost.KeyPress += AllowOnlyNumeric;
            txtWeight.Leave += FormatDecimal;
            txtNumber.Leave += FormatDecimal;
            txtCost.Leave += FormatDecimal;
            AttachKeyPressEvent(this);

            // Attach status button events
            AttachStatusEventHandlers();

            // Set the initial status based on loaded data (if currentStatus is not set, UpdateStatusUI will use its value)
            UpdateStatusUI(currentStatus);
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
                    comboBox.Items.Add(name);
                    autoCompleteCollection.Add(name);
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

        // -----------------------------
        // Data Loading
        // -----------------------------
        private void LoadDetails()
        {
            string query = $"SELECT * FROM RepairingData WHERE ID = {repairId}";
            DataTable dt = helper.FetchDataTableFromSYADataBase(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                txtName.Text = row["NAME"].ToString();
                txtNumber.Text = row["NUMBER"].ToString();
                txtWeight.Text = row["WEIGHT"].ToString();
                txtCost.Text = row["ESTIMATE_COST"].ToString();
                rtxtComment.Text = row["COMMENT"].ToString();

                cbType.Text = row["TYPE"].ToString();
                cbSubType.Text = row["SUB_TYPE"].ToString();
                cbCreatedBy.Text = row["CREATED_BY"].ToString();
                cbPriority.Text = row["PRIORITY"].ToString();

                lblBookingDate.Text = row["BOOK_DATE"].ToString();
                lblUpdateDate.Text = row["UPDATE_TIME"].ToString();
                if (DateTime.TryParse(row["DELIVERY_DATE"].ToString(), out DateTime deliveryDate))
                {
                    lblDeliveryDate.Value = deliveryDate;
                }

                // Load image if available
                if (row.Table.Columns.Contains("IMAGE_PATH"))
                {
                    string imagePathFromDB = row["IMAGE_PATH"].ToString();
                    if (!string.IsNullOrEmpty(imagePathFromDB) && System.IO.File.Exists(imagePathFromDB))
                    {
                        pictureBox1.Image = Image.FromFile(imagePathFromDB);
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }

                // Load and update status
                string loadedStatus = row["STATUS"].ToString();
                if (string.IsNullOrEmpty(loadedStatus))
                {
                    loadedStatus = "New";
                }
                currentStatus = loadedStatus;
                UpdateStatusUI(currentStatus);
            }
            else
            {
                MessageBox.Show("No details found for the selected repair item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -----------------------------
        // Update Functionality
        // -----------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateRepairRecord();
        }

        private void UpdateRepairRecord()
        {
            string NAME = txtName.Text.Trim();
            string NUMBER = string.IsNullOrWhiteSpace(txtNumber.Text) ? "NULL" : $"'{txtNumber.Text.Trim()}'";
            string WEIGHT = string.IsNullOrWhiteSpace(txtWeight.Text) ? "NULL" : txtWeight.Text.Trim();
            string ESTIMATE_COST = string.IsNullOrWhiteSpace(txtCost.Text) ? "NULL" : txtCost.Text.Trim();
            string DELIVERY_DATE = lblDeliveryDate.Value.ToString("yyyy-MM-dd");
            string UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string TYPE = string.IsNullOrEmpty(cbType.Text) ? "''" : $"'{cbType.Text.Trim()}'";
            string SUB_TYPE = string.IsNullOrEmpty(cbSubType.Text) ? "''" : $"'{cbSubType.Text.Trim()}'";
            string CREATED_BY = string.IsNullOrEmpty(cbCreatedBy.Text) ? "''" : $"'{cbCreatedBy.Text.Trim()}'";
            string PRIORITY = string.IsNullOrEmpty(cbPriority.Text) ? "''" : $"'{cbPriority.Text.Trim()}'";
            string COMMENT = $"'{rtxtComment.Text.Trim()}'";
            string STATUS = string.IsNullOrEmpty(currentStatus) ? "'NEW'" : $"'{currentStatus}'";

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
                    COMMENT = {COMMENT},
                    STATUS = {STATUS}
                WHERE ID = {repairId}";

            object affectedRows = helper.RunQueryWithoutParametersSYADataBase(updateQuery, "ExecuteNonQuery");
            if (affectedRows != null && Convert.ToInt32(affectedRows) > 0)
            {
                // Notify subscribers that the record was updated
                RecordUpdated?.Invoke(this, EventArgs.Empty);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -----------------------------
        // Delete Functionality
        // -----------------------------
        private void DeleteRepairRecord()
        {
            if (MessageBox.Show("Are you sure you want to delete this repair record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string deleteQuery = $"DELETE FROM RepairingData WHERE ID = {repairId}";
                object affectedRows = helper.RunQueryWithoutParametersSYADataBase(deleteQuery, "ExecuteNonQuery");
                if (affectedRows != null && Convert.ToInt32(affectedRows) > 0)
                {
                    // Notify subscribers (e.g., the main form) that a record has been deleted.
                    RecordUpdated?.Invoke(this, EventArgs.Empty);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Delete failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRepairRecord();
        }
    }
}
