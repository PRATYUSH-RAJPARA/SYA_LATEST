using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYA
{
    public class SearchHelper
    {
        //ComboBox CB_YEAR1;
        //ComboBox CB_NAME1;
        //ComboBox CB_HUID1;
        //ComboBox CB_TAGNO1;
       
        //private void BindACNameComboBox(ComboBox CB_NAME)
        //{
        //    string query = @"
        //SELECT DISTINCT AC_NAME
        //FROM SALE_DATA_NEW 
        //ORDER BY AC_NAME ASC;";
        //    DataTable AC_NAME_TABLE = helper.FetchDataTableFromSYADataBase(query);
        //    // Create a new row for "All" and add it at the top
        //    DataRow allRow = AC_NAME_TABLE.NewRow();
        //    allRow["AC_NAME"] = "All";
        //    AC_NAME_TABLE.Rows.InsertAt(allRow, 0);
        //    // Bind the fetched values to the combo box
        //    CB_NAME.DataSource = AC_NAME_TABLE;
        //    CB_NAME.DisplayMember = "AC_NAME";
        //    CB_NAME.ValueMember = "AC_NAME";
        //    CB_NAME.SelectedIndex = 0;
        //    // Enable autocomplete functionality
        //    CB_NAME.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    CB_NAME.AutoCompleteSource = AutoCompleteSource.ListItems;
        //    CB_NAME1 = CB_NAME;
        //    CB_NAME.TextChanged += CB_NAME_TextChanged;

        //    CB_NAME.SelectedIndexChanged += CB_NAME_SelectedIndexChanged;
        //}
        //private void BindCOYearComboBox(ComboBox CB_YEAR)
        //{
        //    string query = @"
        //SELECT DISTINCT CO_YEAR
        //FROM (
        //    SELECT CO_YEAR FROM SALE_DATA_NEW
        //    UNION
        //    SELECT CO_YEAR FROM MAIN_DATA_NEW
        //) AS combined_data
        //ORDER BY CO_YEAR DESC;";
        //    // Fetch distinct CO_YEAR values from both tables
        //    DataTable coYearTable = helper.FetchDataTableFromSYADataBase(query);
        //    // Create a new row for "All" and add it at the top
        //    DataRow allRow = coYearTable.NewRow();
        //    allRow["CO_YEAR"] = "All";
        //    coYearTable.Rows.InsertAt(allRow, 0); // Insert "All" as the first row
        //    // Bind the fetched values to the combo box
        //    CB_YEAR.DataSource = coYearTable;
        //    CB_YEAR.DisplayMember = "CO_YEAR"; // Column to display in the combo box
        //    CB_YEAR.ValueMember = "CO_YEAR";   // Value to bind in the combo box
        //    // Set the default selected item to "All"
        //    CB_YEAR.SelectedIndex = 0;
        //    CB_YEAR.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    CB_YEAR.AutoCompleteSource = AutoCompleteSource.ListItems;
        //    CB_YEAR1 = CB_YEAR;
        //    CB_YEAR.TextChanged += CB_YEAR_TextChanged;
        //    CB_YEAR.SelectedIndexChanged += CB_YEAR_SelectedIndexChanged;
        //}

        //private void BindTAGNOComboBox(ComboBox CB_TAGNO)
        //{
        //    string query = @"
        //SELECT DISTINCT TAG_NO
        //FROM (
        //    SELECT TAG_NO FROM SALE_DATA_NEW
        //    UNION
        //    SELECT TAG_NO FROM MAIN_DATA_NEW
        //) AS combined_data
        //ORDER BY TAG_NO ASC;";
        //    // Fetch distinct CO_YEAR values from both tables
        //    DataTable TAGNOTable = helper.FetchDataTableFromSYADataBase(query);
        //    // Create a new row for "All" and add it at the top
        //    DataRow allRow = TAGNOTable.NewRow();
        //    allRow["TAG_NO"] = "All";
        //    TAGNOTable.Rows.InsertAt(allRow, 0); // Insert "All" as the first row
        //    // Bind the fetched values to the combo box
        //    CB_TAGNO.DataSource = TAGNOTable;
        //    CB_TAGNO.DisplayMember = "TAG_NO"; // Column to display in the combo box
        //    CB_TAGNO.ValueMember = "TAG_NO";   // Value to bind in the combo box
        //    // Set the default selected item to "All"
        //    CB_TAGNO.SelectedIndex = 0;
        //    CB_TAGNO.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    CB_TAGNO.AutoCompleteSource = AutoCompleteSource.ListItems;
        //    CB_TAGNO1 = CB_TAGNO;
        //    CB_TAGNO.TextChanged += CB_TAGNO_TextChanged;
        //    CB_TAGNO.SelectedIndexChanged += CB_TAGNO_SelectedIndexChanged;
        //}
        //private void BindHUIDComboBox(ComboBox CB_HUID)
        //{
        //    string query = @"
        //SELECT DISTINCT HUID1
        //FROM (
        //    SELECT HUID1 FROM SALE_DATA_NEW
        //    UNION
        //    SELECT HUID1 FROM MAIN_DATA_NEW
        //) AS combined_data
        //ORDER BY HUID1 ASC;";
        //    // Fetch distinct HUID values from the database
        //    DataTable HUIDTable = helper.FetchDataTableFromSYADataBase(query);
        //    // Add "All" as the first row
        //    DataRow allRow = HUIDTable.NewRow();
        //    allRow["HUID1"] = "All";
        //    HUIDTable.Rows.InsertAt(allRow, 0);
        //    // Bind the fetched data to the combo box
        //    CB_HUID.DataSource = HUIDTable;
        //    CB_HUID.DisplayMember = "HUID1";
        //    CB_HUID.ValueMember = "HUID1";
        //    // Set default properties
        //    CB_HUID.AutoCompleteMode = AutoCompleteMode.None;
        //    CB_HUID.AutoCompleteSource = AutoCompleteSource.ListItems;
        //    // Attach event for filtering
        //    CB_HUID1 = CB_HUID;
        //    CB_HUID.TextChanged += CB_HUID_TextChanged;
        //    CB_HUID.SelectedIndexChanged += CB_HUID_SelectedIndexChanged;

        //}
        //private void CB_NAME_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_NAME1, "AC_NAME");

        //}
        //private void CB_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_YEAR1, "CO_YEAR");

        //}
        //private void CB_TAGNO_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_TAGNO1, "TAG_NO");

        //}
        //private void CB_HUID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_HUID1, "HUID1");

        //}

        //private void CB_HUID_TextChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_HUID1, "HUID1");
        //}
        //private void CB_TAGNO_TextChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_TAGNO1, "TAG_NO");
        //}
        //private void CB_NAME_TextChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_NAME1, "AC_NAME");
        //}
        //private void CB_YEAR_TextChanged(object sender, EventArgs e)
        //{
        //    LoadDataBasedOnComboBoxValue(CB_YEAR1, "CO_YEAR");
        //}

        //private void LoadDataBasedOnComboBoxValue(ComboBox CB, string columnName)
        //{
        //    string typedText = CB.Text.ToString();
        //    // Reset DataGridView
        //    dataGridView1.DataSource = null;
        //    dataGridView1.Rows.Clear();
        //    // Reset the current offset
        //    // Build the query directly based on selected CO_YEAR
        //    if (typedText != "All")
        //    {
        //        // Query with WHERE clause for a specific CO_YEAR
        //        WHERE_SALE = $" WHERE {columnName}  LIKE '%{typedText}%'";
        //        WHERE_MD = $" WHERE {columnName}  LIKE '%{typedText}%'";
        //    }
        //    else
        //    {
        //        // Query without WHERE clause for "All" CO_YEAR
        //        WHERE_SALE = "";
        //        WHERE_MD = "";
        //    }
        //    // Fetch the filtered data from the database
        //    SearchPaginationHelper.setCurrentOffset(0);
        //    loadData();
        //}
    }
}
