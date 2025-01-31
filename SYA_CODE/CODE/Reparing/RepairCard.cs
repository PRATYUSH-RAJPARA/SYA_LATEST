using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SYA
{
    public partial class RepairCard : UserControl
    {
        private Dictionary<string, Color> statusColors = new Dictionary<string, Color>
    {
        { "New", ColorTranslator.FromHtml("#90e0ef") },
        { "In Progress", ColorTranslator.FromHtml("#ffe94e") },
        { "Completed", ColorTranslator.FromHtml("#a1ef7a") },
        { "Unable to Complete", ColorTranslator.FromHtml("#ec8385") }
    };

        private int repairId; // To store the ID of the repair item

        public RepairCard()
        {
            InitializeComponent();
            AttachEventHandlers(); // Attach button click events
        }

        public void SetRepairDetails(int id, string name, string date, string status)
        {
            repairId = id; // Store the ID
            NAME.Text = name;
            TYPE_DATE.Text = date;
            STATUS.Text = status;
            UpdateStatusUI(status);
        }

        private void UpdateStatusUI(string status)
        {
            if (statusColors.ContainsKey(status))
            {
                tableLayoutPanel6.BackColor = statusColors[status]; // Update panel color
            }

            // Reset all button colors
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
                    btnTypeNew.ForeColor = Color.Black;
                    break;
                case "In Progress":
                    btnTypeInProgress.BackColor = statusColors["In Progress"];
                    btnTypeInProgress.ForeColor = Color.Black;
                    break;
                case "Completed":
                    btnTypeCompleted.BackColor = statusColors["Completed"];
                    btnTypeCompleted.ForeColor = Color.Black;
                    break;
                case "Unable to Complete":
                    btnTypeUnableToComplete.BackColor = statusColors["Unable to Complete"];
                    btnTypeUnableToComplete.ForeColor = Color.Black;
                    break;
            }
        }

        private void AttachEventHandlers()
        {
            btnTypeNew.Click += (s, e) => ChangeStatus("New");
            btnTypeInProgress.Click += (s, e) => ChangeStatus("In Progress");
            btnTypeCompleted.Click += (s, e) => ChangeStatus("Completed");
            btnTypeUnableToComplete.Click += (s, e) => ChangeStatus("Unable to Complete");
            btnDelete.Click += (s, e) => DeleteRepairItem(); // Attach the Delete event
        }

        private void ChangeStatus(string newStatus)
        {
            STATUS.Text = newStatus; // Update label
            UpdateStatusUI(newStatus); // Refresh colors

            // Update the status in the database
            string query = $"UPDATE RepairingData SET STATUS = '{newStatus}' WHERE ID = {repairId}";
            object affectedRows = helper.RunQueryWithoutParametersSYADataBase(query, "ExecuteNonQuery");

            if (affectedRows == null || Convert.ToInt32(affectedRows) <= 0)
            {
                MessageBox.Show("Failed to update the status in the database.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Proceed with any additional logic if the update is successful
            }

        }

        private void DeleteRepairItem()
        {
            // Ask for confirmation
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this repair item?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Delete from database
                string query = $"DELETE FROM RepairingData WHERE ID = {repairId}";
                object affectedRows = helper.RunQueryWithoutParametersSYADataBase(query, "ExecuteNonQuery");

                // Check if the result is valid and the affected rows count is greater than 0
                if (affectedRows != null && Convert.ToInt32(affectedRows) > 0)
                {
                    // If successful (i.e., at least one row was affected), remove the card from the UI
                    this.Parent.Controls.Remove(this);
                }
                else
                {
                    MessageBox.Show("Failed to delete the repair item from the database or no rows were affected.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }


}
