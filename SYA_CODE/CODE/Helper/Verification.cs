namespace SYA.CODE.Helper
{
    public static class Verification
    {
        public static bool validateHUID(string huid1, string huid2)
        {
            if (!string.IsNullOrWhiteSpace(huid1) && huid1.Length != 6 || !string.IsNullOrWhiteSpace(huid2) && huid2.Length != 6)
            {
                MessageBox.Show("HUID1 length must be 6 characters if not null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(huid2) && huid2.Length != 6)
            {
                MessageBox.Show("HUID2 length must be 6 characters if not null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(huid1) && !string.IsNullOrWhiteSpace(huid2))
            {
                MessageBox.Show("Please insert HUID1 at the correct place.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool validateType(string type)
        {
            if (type == null || string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Validation error: Type cannot be null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool validateWeight(string weight)
        {
            if (!decimal.TryParse(weight, out decimal grossWeight) || grossWeight < 0)
            {
                MessageBox.Show("Validation error: Invalid or negative weight value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool validateWeight(string gross, string net)
        {
            if (decimal.Parse(gross) < decimal.Parse(net))
            {
                MessageBox.Show("Validation error: Gross weight cannot be less than net weight.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool validateLabour(string labour)
        {
            if (!decimal.TryParse(labour, out decimal labour1) || labour1 < 0)
            {
                MessageBox.Show("Validation error: Invalid or negative labour value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool validateOther(string other)
        {
            if (other != null && (!decimal.TryParse(other, out decimal other1) || other1 < 0))
            {
                MessageBox.Show("Validation error: Invalid or negative other value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static string correctWeight(object cellValue)
        {
            // Check if the entered value is not null and can be converted to a decimal
            if (cellValue != null && decimal.TryParse(cellValue.ToString(), out decimal weight))
            {
                // Format the entered value to have three decimal places
                return weight.ToString("0.000");
            }
            return (cellValue ?? "").ToString();
        }
        public static bool ValidatePassword()
        {
            const string correctPassword = "pratyush@sya";  // Predefined password
            // Create a form to prompt the user for a password
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Password Required",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 10, Top = 20, Text = "Enter Password" };
            TextBox textBox = new TextBox() { Left = 10, Top = 50, Width = 260, UseSystemPasswordChar = true };  // Mask the input for password
            Button confirmation = new Button() { Text = "OK", Left = 200, Width = 70, Top = 75, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            // Show the prompt dialog and get the entered password
            string inputPassword = prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
            // Validate if the entered password matches the predefined one
            return inputPassword == correctPassword;
        }
    }
}
