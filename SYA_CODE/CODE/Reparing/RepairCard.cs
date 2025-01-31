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

        public RepairCard()
        {
            InitializeComponent();
            AttachEventHandlers(); // Attach button click events
        }

        public void SetRepairDetails(string name, string date, string cost, string status)
        {
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
                    btnTypeNew.ForeColor = Color.White;
                    break;
                case "In Progress":
                    btnTypeInProgress.BackColor = statusColors["In Progress"];
                    btnTypeInProgress.ForeColor = Color.White;
                    break;
                case "Completed":
                    btnTypeCompleted.BackColor = statusColors["Completed"];
                    btnTypeCompleted.ForeColor = Color.White;
                    break;
                case "Unable to Complete":
                    btnTypeUnableToComplete.BackColor = statusColors["Unable to Complete"];
                    btnTypeUnableToComplete.ForeColor = Color.White;
                    break;
            }
        }

        private void AttachEventHandlers()
        {
            btnTypeNew.Click += (s, e) => ChangeStatus("New");
            btnTypeInProgress.Click += (s, e) => ChangeStatus("In Progress");
            btnTypeCompleted.Click += (s, e) => ChangeStatus("Completed");
            btnTypeUnableToComplete.Click += (s, e) => ChangeStatus("Unable to Complete");
        }

        private void ChangeStatus(string newStatus)
        {
            STATUS.Text = newStatus; // Update label
            UpdateStatusUI(newStatus); // Refresh colors
        }
    }
}
