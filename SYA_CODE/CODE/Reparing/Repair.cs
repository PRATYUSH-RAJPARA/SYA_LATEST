using System;
using System.Data;
using System.Drawing;
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
            // Clear existing cards
            flowLayoutPanel1.Controls.Clear();

            // Fetch repair data from SQLite
            DataTable dt = helper.FetchDataTableFromSYADataBase("SELECT ID, NAME, BOOK_DATE, STATUS FROM RepairingData");

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["ID"]);  // Get the repair ID
                    string name = row["NAME"].ToString();
                    string date = row["BOOK_DATE"].ToString();
                    string status = row["STATUS"].ToString();

                    RepairCard card = new RepairCard();
                    card.SetRepairDetails(id, name, date, status); // Pass the ID to the card
                    flowLayoutPanel1.Controls.Add(card);
                }
            }
            else
            {
                MessageBox.Show("Error loading repair data.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
