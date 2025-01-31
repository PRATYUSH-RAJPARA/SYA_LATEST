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
                    tableLayoutPanel6.BackColor = ColorTranslator.FromHtml("#90e0ef");
                    break;
                case "In Progress":
                    tableLayoutPanel6.BackColor = ColorTranslator.FromHtml("#ffe94e");
                    break;
                case "Completed":
                    tableLayoutPanel6.BackColor = ColorTranslator.FromHtml("#a1ef7a");
                    break;
                case "Unable to Complete":
                    tableLayoutPanel6.BackColor = ColorTranslator.FromHtml("#ec8385");
                    break;
            }
        }


    }
}
