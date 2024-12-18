namespace SYA
{
    public partial class NotifyForm : Form
    {
        public NotifyForm()
        {
            InitializeComponent();
        }
        public void ShowNotification1(string message)
        {
            textBox1.Text = message;
        }
        public void ShowNotification2(string message)
        {
            textBox2.Text = message;
        }
        private void NotifyForm_Load(object sender, EventArgs e)
        {
        }
        public void CloseNotifyFormAfterDelay(int milliseconds)
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = milliseconds;
            timer.Tick += (s, e) =>
            {
                timer.Stop(); // Stop the timer
                this.Close(); // Close the current form instance
            };
            timer.Start(); // Start the timer
        }
    }
}
