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
    public partial class RepairCard : UserControl
    {
        public RepairCard()
        {
            InitializeComponent();
        }
        public void SetRepairDetails(string name, string date, string cost, string status)
        {
            NAME.Text = name;
            TYPE_DATE.Text = date;
         //   lblCost.Text = "₹" + cost;
            STATUS.Text = status;
         //   picItem.Image = image;

            // Change status label color
            switch (status)
            {
                case "New":
                    STATUS.ForeColor = Color.Blue;
                    break;
                case "In Progress":
                    STATUS.ForeColor = Color.Orange;
                    break;
                case "Completed":
                    STATUS.ForeColor = Color.Green;
                    break;
                case "Unable to Complete":
                    STATUS.ForeColor = Color.Red;
                    break;
            }
        }


    }
}
