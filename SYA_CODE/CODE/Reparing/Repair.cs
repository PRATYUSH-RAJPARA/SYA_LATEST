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
    public partial class Repair : Form
    {
        public Repair()
        {
            InitializeComponent();
        }

        private void Repair_Load(object sender, EventArgs e)
        {
            LoadRepairItems();
        }

        private void LoadRepairItems()
        {
            // Example data (replace this with your actual data source)
            string[,] repairs =
            {
        { "Ring Repair", "2024-02-01", "500", "New" },
        { "Necklace Polish", "2024-01-25", "750", "Unable to Complete" },
        { "Ring Repair", "2024-02-01", "500", "Completed" },
        { "Necklace Polish", "2024-01-25", "750", "In Progress" },
        { "Ring Repair", "2024-02-01", "500", "New" },
        { "Necklace Polish", "2024-01-25", "750", "In Progress" },
        { "Ring Repair", "2024-02-01", "500", "New" },
        { "Necklace Polish", "2024-01-25", "750", "In Progress" },
        { "Ring Repair", "2024-02-01", "500", "New" },
        { "Necklace Polish", "2024-01-25", "750", "In Progress" },
        { "Bracelet Clasp Fix", "2024-01-18", "600", "Completed" }
    };

            // Clear existing cards
            flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i < repairs.GetLength(0); i++)
            {
                RepairCard card = new RepairCard();
                card.SetRepairDetails(repairs[i, 0], repairs[i, 1], repairs[i, 2], repairs[i, 3]);
                flowLayoutPanel1.Controls.Add(card);
            }
        }

    }
}
