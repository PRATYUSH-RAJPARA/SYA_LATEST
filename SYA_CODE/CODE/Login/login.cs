using System.Data.SQLite;
namespace SYA
{
    public partial class login : Form
    {
        private SQLiteConnection connection;
        public login()
        {
            InitializeComponent();
            connection = new SQLiteConnection(helper.SYAConnectionString);
            //    connection = new SQLiteConnection("Data Source=C:\\Users\\91760\\Desktop\\SYA\\SYADataBase.db;Version=3;");
        }
        private void login_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
        //private void btnlogin_Click(object sender, EventArgs e)
        //{
        //    // Retrieve entered username and password
        //    string username = textBox1.Text;
        //    string password = textBox2.Text;
        //    // Perform authentication logic
        //    //if (AuthenticateUser(username, password))
        //    //{
        //        // Hide the login form
        //        this.Hide();
        //        // Open the next form (replace NextForm with the name of your next form)
        //        main mainform = new main();
        //        mainform.Show();
        //    //}
        //    else
        //    {
        //        MessageBox.Show("Invalid username or password. Please try again.");
        //    }
        //}
        //private bool AuthenticateUser(string username, string password)
        //{
        //    using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Account WHERE username = @username AND password = @password", connection))
        //    {
        //        connection.Open();
        //        cmd.Parameters.AddWithValue("@username", username);
        //        cmd.Parameters.AddWithValue("@password", password);
        //        int count = Convert.ToInt32(cmd.ExecuteScalar());
        //        connection.Close();
        //        return count > 0;
        //    }
        //}
        //private void retrive_Click(object sender, EventArgs e)
        //{
        //    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Account", connection))
        //    {
        //        connection.Open();
        //        using (SQLiteDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                // Access data using reader["column_name"]
        //                string username = reader["username"].ToString();
        //                string password = reader["password"].ToString();
        //                // Do something with the data, e.g., display it in a MessageBox
        //                MessageBox.Show($"Username: {username}, Password: {password}");
        //            }
        //        }
        //        connection.Close();
        //    }
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToUpper() == "SYA" && textBox2.Text.Length == 0)
            {
                this.Hide();
                // Open the next form (replace NextForm with the name of your next form)
                main mainform = new main();
                mainform.Show();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.ToUpper() == "SYA" && textBox2.Text.Length == 0)
                {
                    this.Hide();
                    // Open the next form (replace NextForm with the name of your next form)
                    main mainform = new main();
                    mainform.Show();
                }
            }
        }
        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}