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
    public partial class Fetchalldatafrommsaccess : Form
    {
        public Fetchalldatafrommsaccess()
        {
            InitializeComponent();
        }
        private void Fetchalldatafrommsaccess_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("tableName", "Table Name");
            dataGridView1.Columns.Add("rowCount", "Row Count");
        }
        //private void fetch()
        //{
        //    try
        //    {
        //        // Clear the RichTextBox before writing output
        //        richTextBox1.Clear();
        //        // Fetch table names from the MS Access database
        //        string tableQuery = "SELECT * FROM MSysObjects WHERE Type=1 AND Left(Name, 4) <> 'MSys';";
        //        DataTable tableNames = helper.FetchDataTableFromDataCareDataBase(tableQuery);
        //        // List to store DataTables
        //        List<DataTable> dataTables = new List<DataTable>();
        //        richTextBox1.AppendText("Fetching tables...\n\n");
        //        foreach (DataRow row in tableNames.Rows)
        //        {
        //            string tableName = row["Name"].ToString();
        //            // Fetch data for each table
        //            string dataQuery = $"SELECT * FROM [{tableName}]";
        //            DataTable dataTable = helper.FetchDataTableFromDataCareDataBase(dataQuery);
        //            dataTable.TableName = tableName;
        //            // Add to the list
        //            dataTables.Add(dataTable);
        //            // Write output to RichTextBox
        //            richTextBox1.AppendText($"Fetched table: {tableName}\n");
        //            richTextBox1.AppendText($"Rows: {dataTable.Rows.Count}\n\n");
        //        }
        //        richTextBox1.AppendText($"Total tables fetched: {dataTables.Count}\n");
        //    }
        //    catch (Exception ex)
        //    {
        //        richTextBox1.AppendText($"Error: {ex.Message}\n");
        //    }
        //}
        private void fetch()
        {
            try
            {
                // Clear the DataGridView before adding new data
                dataGridView1.Rows.Clear();
                // Fetch table names from the MS Access database
                string tableQuery = "SELECT * FROM MSysObjects WHERE Type=1 AND Left(Name, 4) <> 'MSys';";
                DataTable tableNames = helper.FetchDataTableFromDataCareDataBase(tableQuery);
                // List to store DataTables
                List<DataTable> dataTables = new List<DataTable>();
                // Iterate through the table names
                foreach (DataRow row in tableNames.Rows)
                {
                    string tableName = row["Name"].ToString();
                    // Fetch data for each table
                    string dataQuery = $"SELECT * FROM [{tableName}]";
                    DataTable dataTable = helper.FetchDataTableFromDataCareDataBase(dataQuery);
                    dataTable.TableName = tableName;
                    // Add to the list
                    dataTables.Add(dataTable);
                    // Only add the row to the DataGridView if the count is non-zero
                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Add(tableName, dataTable.Rows.Count);
                    }
                }
                // Optionally, display the total number of tables fetched
                // Example: statusLabel.Text = $"Total tables fetched: {dataTables.Count}";
            }
            catch (Exception ex)
            {
                // Handle the error appropriately (could be a message box or logging)
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void CompareDatabases()
        {
            try
            {
                // Get connection strings from RichTextBox2 and RichTextBox3
                  string db1ConnectionString = richTextBox3.Text.Trim();
              //  string db1ConnectionString = "Microsoft.ACE.OLEDB.12.0; Data Source = \"D:\\DataCareWh\\DataCare24.mdb\"";
                string db2ConnectionString = richTextBox4.Text.Trim();
                //   string db2ConnectionString = richTextBox3.Text.Trim();
                MessageBox.Show(db1ConnectionString + "      FGFG     " + db2ConnectionString);
                // Fetch table row counts from both databases
                var db1RowCounts = helper.GetTableRowCounts(db1ConnectionString);
                var db2RowCounts = helper.GetTableRowCounts(db2ConnectionString);
                // Compare row counts and log differences
              //  richTextBox1.Clear();
                foreach (var table in db1RowCounts)
                {
                    string tableName = table.Key;
                    int db1RowCount = table.Value;
                    db2RowCounts.TryGetValue(tableName, out int db2RowCount);
                    if (db1RowCount != db2RowCount)
                    {
                 //       richTextBox1.AppendText($"Table: {tableName}, DB1 Rows: {db1RowCount}, DB2 Rows: {db2RowCount}\n");
                    }
                }
                MessageBox.Show("Comparison completed. Check logs in RichTextBox1.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during comparison: {ex.Message}");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            fetch();
        }
    }
}
