﻿using System.Data.SQLite;
using System.Windows.Forms;
namespace SYA
{
    public  class addGoldHelper
    {
        private SQLiteConnection connectionToSYADatabase;
        private SQLiteConnection connectionToDatacare1;
        public  string correctWeight(object cellValue)
        {
            // Check if the value is not null, not empty/whitespace, and can be parsed to a decimal
            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString())
                && decimal.TryParse(cellValue.ToString(), out decimal weight))
            {
                // Format the entered value to 3 decimal places
                return weight.ToString("0.000");
            }
            // Return default "0.000" if invalid input or null
            return "0.000";
        }
        public  string getCurrentColumnName(DataGridView dataGridView1)
        {
            return dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
        }
        public  bool validateHUID(string huid1, string huid2, string huid3)
        {
            // Check if huid1, huid2, or huid3 have invalid lengths (not null/whitespace and not 6 characters)
            if (!string.IsNullOrWhiteSpace(huid1) && huid1.Length != 6 ||
                !string.IsNullOrWhiteSpace(huid2) && huid2.Length != 6 ||
                !string.IsNullOrWhiteSpace(huid3) && huid3.Length != 6)
            {
                MessageBox.Show("HUID1, HUID2, and HUID3 lengths must be 6 characters if not null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Ensure HUID1 is provided if HUID2 or HUID3 are entered
            if (string.IsNullOrWhiteSpace(huid1) && (!string.IsNullOrWhiteSpace(huid2) || !string.IsNullOrWhiteSpace(huid3)))
            {
                MessageBox.Show("Please insert HUID1 before HUID2 or HUID3.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Ensure HUID2 is provided if HUID3 is entered
            if (string.IsNullOrWhiteSpace(huid2) && !string.IsNullOrWhiteSpace(huid3))
            {
                MessageBox.Show("Please insert HUID2 before HUID3.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public string GetPRCode(string itemName)
        {
            connectionToSYADatabase = new SQLiteConnection(helper.SYAConnectionString);
            using (SQLiteConnection con = new SQLiteConnection(connectionToSYADatabase.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand($"SELECT PR_CODE FROM ITEM_MASTER WHERE IT_NAME = '{itemName}' AND IT_TYPE = 'G'", con))
                {
                    con.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    return null;
                }
            }
        }
        public int GetNextSequenceNumber(string prefix, string prCode, string caret)
        {
            connectionToSYADatabase = new SQLiteConnection(helper.SYAConnectionString);
            prefix ??= "";
            prCode ??= "";
            caret ??= "";
            int prefixLength = prefix.Length + prCode.Length + caret.Length;
            using (SQLiteConnection con = new SQLiteConnection(connectionToSYADatabase.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand($"SELECT MAX(CAST(SUBSTR(TAG_NO, {prefixLength + 1}) AS INTEGER)) FROM MAIN_DATA WHERE ITEM_CODE = '{prCode}'", con))
                {
                    con.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        return Convert.ToInt32(result) + 1;
                    }
                    return 1;
                }
            }
        }
    }
}
