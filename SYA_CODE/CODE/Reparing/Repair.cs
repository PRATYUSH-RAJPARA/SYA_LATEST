using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SYA
{
    public partial class Repair : Form
    {
        // Stores all cards in memory to use for filtering later.
        private List<RepairCard> allRepairCards = new List<RepairCard>();

        public Repair()
        {
            InitializeComponent();
            // Attach filter event handlers to combo boxes.
            cbName.SelectedIndexChanged += FilterCards;
            cbNumber.SelectedIndexChanged += FilterCards;
            cbType.SelectedIndexChanged += FilterCards;
            cbSubType.SelectedIndexChanged += FilterCards;
            cbPriority.SelectedIndexChanged += FilterCards;
            cbStatus.SelectedIndexChanged += FilterCards;
        }

        private void Repair_Load(object sender, EventArgs e)
        {
            LoadRepairItems();
        }

        private void LoadRepairItems()
        {
            // Clear any existing cards.
            flowLayoutPanel1.Controls.Clear();
            allRepairCards.Clear();

            // Create sets to collect distinct values for filtering.
            HashSet<string> names = new HashSet<string>();
            HashSet<string> numbers = new HashSet<string>();
            HashSet<string> types = new HashSet<string>();
            HashSet<string> subTypes = new HashSet<string>();
            HashSet<string> priorities = new HashSet<string>();
            HashSet<string> statuses = new HashSet<string>();

            // Initialize ComboBoxes with "All"
            cbName.Items.Clear();
            cbNumber.Items.Clear();
            cbType.Items.Clear();
            cbSubType.Items.Clear();
            cbPriority.Items.Clear();
            cbStatus.Items.Clear();

            cbName.Items.Add("All");
            cbNumber.Items.Add("All");
            cbType.Items.Add("All");
            cbSubType.Items.Add("All");
            cbPriority.Items.Add("All");
            cbStatus.Items.Add("All");

            // Set default selected index for all combo boxes to "All"
            cbName.SelectedIndex = 0;
            cbNumber.SelectedIndex = 0;
            cbType.SelectedIndex = 0;
            cbSubType.SelectedIndex = 0;
            cbPriority.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;

            // Fetch repair data from SQLite
            string query = "SELECT ID, NAME, NUMBER, TYPE, SUB_TYPE, PRIORITY, STATUS, BOOK_DATE, IMAGE_PATH FROM RepairingData";
            DataTable dt = helper.FetchDataTableFromSYADataBase(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["ID"]);
                    string name = row["NAME"].ToString();
                    string number = row["NUMBER"].ToString();
                    string type = row["TYPE"].ToString();
                    string subType = row["SUB_TYPE"].ToString();
                    string priority = row["PRIORITY"].ToString();
                    string status = row["STATUS"].ToString();
                    string date = row["BOOK_DATE"].ToString();
                    string imagePath = row["IMAGE_PATH"].ToString();

                    // Create the repair card
                    RepairCard card = new RepairCard();
                    card.SetRepairDetails(id, name, date, status, imagePath);
                    card.Tag = row; // Store the entire row in the card's tag for filtering later

                    // Add the card to the list and FlowLayoutPanel
                    allRepairCards.Add(card);
                    flowLayoutPanel1.Controls.Add(card);

                    // Collect unique values for combo boxes
                    if (!string.IsNullOrWhiteSpace(name)) names.Add(name);
                    if (!string.IsNullOrWhiteSpace(number)) numbers.Add(number);
                    if (!string.IsNullOrWhiteSpace(type)) types.Add(type);
                    if (!string.IsNullOrWhiteSpace(subType)) subTypes.Add(subType);
                    if (!string.IsNullOrWhiteSpace(priority)) priorities.Add(priority);
                    if (!string.IsNullOrWhiteSpace(status)) statuses.Add(status);
                }
            }

            // Populate the combo boxes
            PopulateComboBox(cbName, names);
            PopulateComboBox(cbNumber, numbers);
            PopulateComboBox(cbType, types);
            PopulateComboBox(cbSubType, subTypes);
            PopulateComboBox(cbPriority, priorities);
            PopulateComboBox(cbStatus, statuses);
      //      cbNumber.SelectedItem = "All";

        }







        private void PopulateComboBox(ComboBox comboBox, HashSet<string> values)
        {
            comboBox.Items.Clear();          // Clear previous items
            comboBox.Items.Add("All");       // Add "All" as the default option

            foreach (var value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                    comboBox.Items.Add(value); // Add items to the combo box
            }

            if (comboBox.Items.Count > 0)    // Ensure there are items to select
                comboBox.SelectedIndex = 0;  // Set "All" as the default selection
        }


        private void FilterCards(object sender, EventArgs e)
        {
            // Determine which combo box triggered the filter change
            ComboBox changedComboBox = sender as ComboBox;

            // Get the selected filter value for each field; default to "All" if nothing is selected.
            string selectedName = cbName.SelectedItem?.ToString() ?? "All";
            string selectedNumber = cbNumber.SelectedItem?.ToString() ?? "All";
            string selectedType = cbType.SelectedItem?.ToString() ?? "All";
            string selectedSubType = cbSubType.SelectedItem?.ToString() ?? "All";
            string selectedPriority = cbPriority.SelectedItem?.ToString() ?? "All";
            string selectedStatus = cbStatus.SelectedItem?.ToString() ?? "All";

            // When any combo box is changed, reset the others to "All"
            if (changedComboBox != cbName)
                cbName.SelectedIndex = 0; // Reset to "All"
            if (changedComboBox != cbNumber)
                cbNumber.SelectedIndex = 0; // Reset to "All"
            if (changedComboBox != cbType)
                cbType.SelectedIndex = 0; // Reset to "All"
            if (changedComboBox != cbSubType)
                cbSubType.SelectedIndex = 0; // Reset to "All"
            if (changedComboBox != cbPriority)
                cbPriority.SelectedIndex = 0; // Reset to "All"
            if (changedComboBox != cbStatus)
                cbStatus.SelectedIndex = 0; // Reset to "All"

            // Filter the cards based on the selected values
            foreach (var card in allRepairCards)
            {
                bool isVisible = true;

                // Access the data stored in the card's tag (the row data)
                DataRow row = card.Tag as DataRow;

                if (row != null)
                {
                    // Check if the card matches the selected filter values
                    if (selectedName != "All" && row["NAME"].ToString() != selectedName) isVisible = false;
                    if (selectedNumber != "All" && row["NUMBER"].ToString() != selectedNumber) isVisible = false;
                    if (selectedType != "All" && row["TYPE"].ToString() != selectedType) isVisible = false;
                    if (selectedSubType != "All" && row["SUB_TYPE"].ToString() != selectedSubType) isVisible = false;
                    if (selectedPriority != "All" && row["PRIORITY"].ToString() != selectedPriority) isVisible = false;
                    if (selectedStatus != "All" && row["STATUS"].ToString() != selectedStatus) isVisible = false;
                }

                // Set the card's visibility
                card.Visible = isVisible;
            }
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            // Reset all combo boxes to "All"
            cbName.SelectedIndex = 0;
            cbNumber.SelectedIndex = 0;
            cbType.SelectedIndex = 0;
            cbSubType.SelectedIndex = 0;
            cbPriority.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;

            // Reapply the filters (which will show all cards since "All" is selected)
            FilterCards(null, null);
        }
    }
}
