using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using DataTable = System.Data.DataTable;
namespace SYA.CODE.Helper
{
    public static class helper
    {
        private static IConfigurationRoot _configuration;
        private static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(@"C:\\SYA_NEW\\DATABASE\\SYA_DATA_NEW\\config")
                        .AddJsonFile("appsettings.json")
                        .Build();
                }
                return _configuration;
            }
        }
        public static string SYAContactConnectionString;
        public static string SyaSettingsDataBase => Configuration["ConnectionStrings:SyaSettingsDataBase"];
        public static string SYAConnectionString;
        public static string accessConnectionString;
        public static string excelFile;
        public static string ImageFolder;
        public static string LogsFolder;
        public static string TagPrinterName;
        public static string NormalPrinterName;
        public static string DataVerificationOldTable;
        public static string DataVerificationNewTable;
        public static string GoldPerGramLabour;
        public static string SilverPerGramLabour;
        public static object RunQueryWithoutParameters(string connectionString, string query, string commandType)
        {
            //i am chaning int to object so where we are implementing use proper conversion
            // pratyush delete
            //  MessageBox.Show(query);
            object res = null;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            if (commandType == "ExecuteNonQuery")
                            {
                                res = command.ExecuteNonQuery();
                            }
                            else if (commandType == "ExecuteScalar")
                            {
                                res = command.ExecuteScalar();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception as needed
                        MessageBox.Show($"Error executing query:\n\n{query}\n\nCommand Type: {commandType}\n\nError Message: {ex.Message}\n\n{ex.StackTrace}\n\n{ex.InnerException}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Display the error message in a custom form
                MessageBox.Show($"Outerr Error executing query:\n\n{query}\n\nCommand Type: {commandType}\n\nError Message: {ex.Message}");
                // Re-throw the exception for higher-level handling, if needed
                throw;
            }
            return res;
        }
        public static DataTable dt1 = new DataTable();
        public static void loadSettingsValues()
        {
            string query = "SELECT * FROM Settings";
            dt1 = FetchDataTableFromSYASettingsDataBase(query);
            foreach (DataRow row in dt1.Rows)
            {
                SYAConnectionString = row["SYADatabase"].ToString();
                accessConnectionString = row["DataCareDatabase"].ToString();
                SYAContactConnectionString = row["ContactDataBase"].ToString();
                excelFile = row["ExcelFile"].ToString();
                ImageFolder = row["Images"].ToString();
                LogsFolder = row["Logs"].ToString();
                TagPrinterName = row["TagPrinterName"].ToString();
                NormalPrinterName = row["NormalPrinterName"].ToString();
                DataVerificationOldTable = row["DataVerificationOldTable"].ToString();
                DataVerificationNewTable = row["DataVerificationNewTable"].ToString();
                GoldPerGramLabour = row["GoldPerGramLabour"].ToString();
                SilverPerGramLabour = row["SilverPerGramLabour"].ToString();
            }
        }
        public static DataTable FetchDataTable(string connectionString, string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            dataTable.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception as needed
                        MessageBox.Show($"Error executing query and filling DataTable: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                MessageBox.Show($"Outer Error executing query and filling DataTable: {ex.Message}");
            }
            return dataTable;
        }
        // SYA SETTINGS
        public static DataTable FetchDataTableFromSYASettingsDataBase(string query)
        {
            return FetchDataTable(SyaSettingsDataBase, query);
        }
        public static object RunQueryWithoutParametersSyaSettingsDataBase(string query, string commandType)
        {
            return RunQueryWithoutParameters(SyaSettingsDataBase, query, commandType);
        }
        // DATACARE
        public static DataTable FetchDataTableFromDataCareDataBase(string query)
        {
            DataTable dataCareDataTable = new DataTable();
            try
            {
                using (OleDbConnection accessConnection = new OleDbConnection(accessConnectionString))
                {
                    accessConnection.Open();
                    try
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, accessConnection))
                        {
                            adapter.Fill(dataCareDataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception as needed
                        MessageBox.Show("FetchFromDataCareDataBase Error executing query: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                MessageBox.Show("Outer FetchFromDataCareDataBase Error executing query: " + ex.Message);
            }
            return dataCareDataTable;
        }
        // SYA DATABASE
        public static DataTable FetchDataTableFromSYADataBase(string query)
        {
            return FetchDataTable(SYAConnectionString, query);
        }
        public static SQLiteDataReader FetchDataFromSYADataBase(string query)
        {
            SQLiteDataReader reader = null;
            try
            {
                SQLiteConnection connection = new SQLiteConnection(SYAConnectionString);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                Console.WriteLine($"Error executing reader: {ex.Message}");
            }
            return reader;
        }
        public static bool RunQueryWithParametersSYADataBase(string query, SQLiteParameter[] parameters = null)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(SYAConnectionString))
                {
                    connection.Open();
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            if (parameters != null)
                            {
                                command.Parameters.AddRange(parameters);
                            }
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception as needed
                        MessageBox.Show($"RunQueryWithParametersSYADataBase Error executing query: {ex.Message}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                MessageBox.Show($"Outer RunQueryWithParametersSYADataBase Error executing query: {ex.Message}");
                return false;
            }
        }
        public static object RunQueryWithoutParametersSYADataBase(string query, string commandType)
        {
            return RunQueryWithoutParameters(SYAConnectionString, query, commandType);
        }
        // SYA CONTACT
        public static object RunQueryWithoutParametersSYAContactDataBase(string query, string commandType)
        {
            return RunQueryWithoutParameters(SYAContactConnectionString, query, commandType);
        }
        public static bool RunQueryWithParametersSYAContactDataBase(string query, SQLiteParameter[] parameters = null)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(SYAContactConnectionString))
                {
                    connection.Open();
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            if (parameters != null)
                            {
                                command.Parameters.AddRange(parameters);
                            }
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception as needed
                        MessageBox.Show($"RunQueryWithParametersSYADataBase Error executing query: {ex.Message}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                MessageBox.Show($"Outer RunQueryWithParametersSYADataBase Error executing query: {ex.Message}");
                return false;
            }
        }
        public static DataTable FetchDataTableFromSYAContactDataBase(string query)
        {
            return FetchDataTable(SYAContactConnectionString, query);
        }
        // ADD NEW FOR FETCH ALL ACCESS TABLES  
        public static Dictionary<string, int> GetTableRowCounts(string connectionString)
        {
            Dictionary<string, int> tableRowCounts = new Dictionary<string, int>();
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    // Fetch all user tables from the MS Access database
                    string query = "SELECT Name FROM MSysObjects WHERE Type=1 AND Name NOT LIKE 'MSys%'";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader.GetString(0);
                            // Count rows for each table
                            string rowCountQuery = $"SELECT COUNT(*) FROM [{tableName}]";
                            using (OleDbCommand rowCountCommand = new OleDbCommand(rowCountQuery, connection))
                            {
                                int rowCount = (int)rowCountCommand.ExecuteScalar();
                                tableRowCounts[tableName] = rowCount;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching table row counts: {ex.Message}");
            }
            return tableRowCounts;
        }
        // EXTRA
        public static DataTable tableLabour = new DataTable();
        public static void loadLabourTable()
        {
            string query = "SELECT * FROM Labour";
            tableLabour = FetchDataTableFromSYASettingsDataBase(query);
        }
    }
}
