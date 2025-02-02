﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.IO;
using System.Threading.Tasks; // Needed for Task.Run
namespace SYA
{
    public partial class AddReparing : Form
    {
        #region Fields
        private AutoCompleteStringCollection nameCollection = new AutoCompleteStringCollection();
        private HashSet<string> nameSet = new HashSet<string>(); // To quickly check if a name exists
        private FilterInfoCollection videoDevices; // List of available cameras
        private VideoCaptureDevice videoSource; // The selected camera
        private Bitmap capturedImage; // Store captured image
        private string imagePath; // Store image path
        #endregion
        #region Constructor
        public AddReparing()
        {
            InitializeComponent();
            LoadAutoCompleteNames();
            txtName.TextChanged += TxtName_TextChanged; // Detect changes in txtName
        }
        #endregion
        #region Form Initialization & Load
        private void AddReparing_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            //LoadComboBoxData(cbSubType, "TYPE");
            //LoadComboBoxData(cbSubType, "SUB_TYPE");
            //LoadComboBoxData(cbCreatedBy, "USER");
            //LoadComboBoxData(cbPriority, "PRIORITY");
            txtWeight.KeyPress += AllowOnlyNumeric;
            txtNumber.KeyPress += AllowOnlyNumeric;
            txtEstimate.KeyPress += AllowOnlyNumeric;
            txtWeight.Leave += FormatDecimal;
            txtNumber.Leave += FormatDecimal;
            txtEstimate.Leave += FormatDecimal;
            AttachKeyPressEvent(this);
            // Start Camera
            StartCamera();
            LoadTypeRadioButtons();
            LoadUserRadioButtons();
            LoadPriorityRadioButtons();
        }

        private void LoadSubTypeData(string subGroup)
        {
            try
            {
                // Query the RepairingHelper table based on subGroup (REPAIR or KARIGAR)
                string query = $"SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'SUB_TYPE' AND [SUB_GROUP] = '{subGroup}'";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);

                // Create lists of the radio buttons and checkboxes
                List<RadioButton> radioButtons = new List<RadioButton>()
        {
            radioButton15, radioButton16, radioButton17, radioButton18, radioButton19, radioButton20,
            radioButton21, radioButton22, radioButton23, radioButton24, radioButton25, radioButton26,
            radioButton27, radioButton28, radioButton29, radioButton30
        };
                List<CheckBox> checkBoxes = new List<CheckBox>()
        {
            checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6,
            checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12,
            checkBox13, checkBox14, checkBox15, checkBox16
        };

                // First, hide all radio buttons and checkboxes
                foreach (var rb in radioButtons)
                {
                    rb.Visible = false;
                }
                foreach (var cb in checkBoxes)
                {
                    cb.Visible = false;
                }

                // Loop through the fetched data and assign values to radio buttons and checkboxes
                for (int i = 0; i < dt.Rows.Count && i < radioButtons.Count; i++)
                {
                    string name = dt.Rows[i]["NAME"].ToString();

                    // Set radio button text
                    radioButtons[i].Text = name;
                    radioButtons[i].Visible = true;

                    // Set checkbox text
                    checkBoxes[i].Text = name;
                    checkBoxes[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subtypes: {ex.Message}");
            }
        }

        private void TypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Check which type is selected (Repair or Karigar)
            if (rbRepair.Checked) // Assuming you have a radio button for Repair
            {
                tableLayoutPanel15.Visible = true; // Show the table containing subtypes and checkboxes
                tableLayoutPanel16.Visible = true; // Show the table with the radio buttons

                // Load Repair subtypes
                LoadSubTypeData("REPAIR");
            }
            else if (rbKarigar.Checked) // Assuming you have a radio button for Karigar
            {
                tableLayoutPanel15.Visible = true; // Show the table containing subtypes and checkboxes
                tableLayoutPanel16.Visible = true; // Show the table with the radio buttons

                // Load Karigar subtypes
                LoadSubTypeData("KARIGAR");
            }
            else
            {
                tableLayoutPanel15.Visible = false; // Hide if no valid type is selected
                tableLayoutPanel16.Visible = false; // Hide if no valid type is selected
            }
        }


        private void LoadPriorityRadioButtons()
        {
            try
            {
                // Fetch priority names from RepairingHelper where [GROUP] = 'PRIORITY'
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'PRIORITY' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                // Create a list of your radio buttons in the order you want to display them.
                List<RadioButton> priorityRadios = new List<RadioButton>()
        {
            rbPriority1, rbPriority2, rbPriority3, rbPriority4
        };
                // First, hide all the radio buttons.
                foreach (RadioButton rb in priorityRadios)
                {
                    rb.Visible = false;
                }
                // Loop through the fetched rows and assign names to radio buttons.
                for (int i = 0; i < dt.Rows.Count && i < priorityRadios.Count; i++)
                {
                    priorityRadios[i].Text = dt.Rows[i]["NAME"].ToString();
                    priorityRadios[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading priorities: " + ex.Message);
            }
        }
        private void LoadTypeRadioButtons()
        {
            try
            {
                // Fetch type names from RepairingHelper where [GROUP] = 'TYPE'
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'TYPE' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                // Create a list of your type radio buttons in the order you want to display them.
                List<RadioButton> typeRadios = new List<RadioButton>()
        {
                    radioButton11, radioButton12, radioButton13, radioButton14
        };
                // First, hide all the radio buttons.
                foreach (RadioButton rb in typeRadios)
                {
                    rb.Visible = false;
                }
                // Loop through the fetched rows and assign names to radio buttons.
                for (int i = 0; i < dt.Rows.Count && i < typeRadios.Count; i++)
                {
                    typeRadios[i].Text = dt.Rows[i]["NAME"].ToString();
                    typeRadios[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading types: " + ex.Message);
            }
        }
        private void LoadUserRadioButtons()
        {
            try
            {
                // Fetch the user names from RepairingHelper table where [GROUP] = 'USER'
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'USER' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                // Create a list of radio buttons in the desired order.
                List<RadioButton> userRadios = new List<RadioButton>()
        {
            rbUser1, rbUser2, rbUser3, rbUser4, rbUser5, rbUser6
        };
                // First hide all radio buttons.
                foreach (RadioButton rb in userRadios)
                {
                    rb.Visible = false;
                }
                // Loop through the fetched users and assign their names to the radio buttons.
                for (int i = 0; i < dt.Rows.Count && i < userRadios.Count; i++)
                {
                    userRadios[i].Text = dt.Rows[i]["NAME"].ToString();
                    userRadios[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }
        #endregion
        #region Camera Functions
        private void StartCamera()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count > 0)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                    videoSource.Start();
                }
                else
                {
                    MessageBox.Show("No camera found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing camera: " + ex.Message);
            }
        }
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image?.Dispose(); // Dispose of the old image
                    Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
                    // Apply horizontal flip to fix mirror effect
                    frame.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    pictureBox1.Image = frame; // Display the corrected image
                }));
            }
            else
            {
                pictureBox1.Image?.Dispose(); // Dispose of the old image
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
                // Apply horizontal flip to fix mirror effect
                frame.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Image = frame; // Display the corrected image
            }
        }
        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                // Run the stop operation asynchronously on a separate thread
                Task.Run(() =>
                {
                    try
                    {
                        videoSource.SignalToStop();
                        videoSource.WaitForStop();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error stopping camera: " + ex.Message);
                    }
                    finally
                    {
                        videoSource = null; // Release reference after stopping
                    }
                });
            }
        }
        #endregion
        #region Key & Text Handling
        // Recursively attach KeyPress event to all TextBox controls
        private void AttachKeyPressEvent(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txtBox) // Exclude RichTextBox
                {
                    txtBox.KeyPress += EditingControl_KeyPress;
                }
                else if (ctrl.HasChildren) // Check inside panels, group boxes, etc.
                {
                    AttachKeyPressEvent(ctrl);
                }
            }
        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar)) // If it's a letter
            {
                e.KeyChar = char.ToUpper(e.KeyChar); // Convert to uppercase
            }
        }
        private void AllowOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            // Allow only digits, control keys (like backspace), and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Prevent non-numeric and non-decimal characters
            }
            // If the character is a decimal point, ensure there's only one in the text
            if (e.KeyChar == '.' && txtBox.Text.Contains("."))
            {
                e.Handled = true; // Prevent entering more than one decimal point
            }
        }
        private void FormatDecimal(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            // Check if the value is not empty and is a valid decimal
            if (decimal.TryParse(txtBox.Text, out decimal result))
            {
                // If there's a decimal point, format to 3 decimal places
                if (txtBox.Text.Contains("."))
                {
                    txtBox.Text = result.ToString("0.000");
                }
            }
            else
            {
                // If invalid input, reset to zero
                txtBox.Text = "0";
            }
        }
        #endregion
        #region Database & AutoComplete
        private void LoadComboBoxData(ComboBox comboBox, string groupType)
        {
            try
            {
                string query = $"SELECT NAME FROM RepairingHelper WHERE [GROUP] = '{groupType}'";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                comboBox.Items.Clear(); // Clear existing items
                AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();
                List<string> itemsList = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["NAME"].ToString();
                    itemsList.Add(name); // Add to List
                    autoCompleteCollection.Add(name); // Add to AutoComplete
                }
                // Sort items before adding to ComboBox
                itemsList.Sort();
                comboBox.Items.AddRange(itemsList.ToArray());
                // Enable AutoComplete
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCollection;
                comboBox.DropDownStyle = ComboBoxStyle.DropDown; // Allows user input
                // Set the first item as default (if available)
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {groupType}: {ex.Message}");
            }
        }
        private void LoadAutoCompleteNames()
        {
            try
            {
                string query = "SELECT DISTINCT AC_NAME FROM AC_MAST";
                DataTable dt = helper.FetchDataTableFromDataCareDataBase(query);
                if (dt.Rows.Count > 0)
                {
                    nameCollection.Clear();
                    nameSet.Clear(); // Clear existing data
                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["AC_NAME"].ToString();
                        nameCollection.Add(name);
                        nameSet.Add(name); // Store in HashSet for quick lookup
                    }
                    txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtName.AutoCompleteCustomSource = nameCollection;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading names: " + ex.Message);
            }
        }
        #endregion
        #region Event Handlers
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // If the active control is the Save button, trigger save action
                if (this.ActiveControl == buttonReCapture)
                {
                    buttonReCapture.PerformClick(); // Simulate button click
                    return true; // Prevent default Enter key behavior
                }
                else
                {
                    // Move to the next control in tab order
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    return true; // Prevent system default behavior
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            if (nameSet.Contains(txtName.Text)) // Only fetch if the name is in the database
            {
                FillMobileNumber();
            }
            else
            {
                txtNumber.Text = ""; // Clear if not a valid selection
            }
            void FillMobileNumber()
            {
                try
                {
                    string query = $"SELECT AC_MOBILE FROM AC_MAST WHERE AC_NAME = '{txtName.Text}'";
                    DataTable dt = helper.FetchDataTableFromDataCareDataBase(query);
                    if (dt.Rows.Count > 0)
                    {
                        txtNumber.Text = dt.Rows[0]["AC_MOBILE"].ToString();
                    }
                    else
                    {
                        txtNumber.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching mobile number: " + ex.Message);
                }
            }
        }
        private void update_insert()
        {
            string NAME = txtName.Text.Trim();
            string NUMBER = string.IsNullOrWhiteSpace(txtNumber.Text) ? "NULL" : txtNumber.Text.Trim();
            string WEIGHT = string.IsNullOrWhiteSpace(txtWeight.Text) ? "NULL" : txtWeight.Text.Trim();
            string ESTIMATE_COST = string.IsNullOrWhiteSpace(txtEstimate.Text) ? "NULL" : txtEstimate.Text.Trim();
            string BOOK_DATE = DateTime.Today.ToString("yyyy-MM-dd");
            string DELIVERY_DATE = dtDeliveryDate.Value.ToString("yyyy-MM-dd");
            string BOOK_TIME = DateTime.Now.ToString("HH:mm:ss");
            string UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string TYPE = "";
            string SUB_TYPE = "";
            string CREATED_BY = "";
            string PRIORITY = "";
            string IMAGE_PATH = string.IsNullOrEmpty(imagePath) ? "" : imagePath; // Assign captured image path
            string COMMENT = rtComment.Text.Trim();
            string STATUS = "New";
            string insertQuery = "INSERT INTO RepairingData (NAME, NUMBER, WEIGHT, ESTIMATE_COST, BOOK_DATE, DELIVERY_DATE, BOOK_TIME, UPDATE_TIME, TYPE, SUB_TYPE, CREATED_BY, PRIORITY, IMAGE_PATH, COMMENT, STATUS) " +
                                 "VALUES ('" + NAME + "', " + NUMBER + ", " + WEIGHT + ", " + ESTIMATE_COST + ", '" + BOOK_DATE + "', '" + DELIVERY_DATE + "', '" + BOOK_TIME + "', '" + UPDATE_TIME + "', " +
                                 "'" + TYPE + "', '" + SUB_TYPE + "', '" + CREATED_BY + "', '" + PRIORITY + "', '" + IMAGE_PATH + "', '" + COMMENT + "', '" + STATUS + "')";
            helper.RunQueryWithoutParametersSYADataBase(insertQuery, "ExecuteNonQuery");
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            update_insert();
            this.Close();
        }
        private void buttonReCapture_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                string folderPath = @"C:\SYA_APP\Images"; // Adjust as needed
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileName = "Repair_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
                imagePath = Path.Combine(folderPath, fileName);
                pictureBox1.Image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Image Captured Successfully.");
                // Stop the camera after capturing the image
                StopCamera();
            }
        }
        private void change_visiable(object sender, EventArgs e)
        {
            if (tableLayoutPanel16.Visible == true)
            {
                tableLayoutPanel16.Visible = false;
                tableLayoutPanel17.Visible = true;
                tableLayoutPanel17.Dock = DockStyle.Fill;
            }
            else
            {
                tableLayoutPanel16.Visible = true;
                tableLayoutPanel17.Visible = false;
                tableLayoutPanel16.Dock = DockStyle.Fill;
            }
        }
        private void AddReparing_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopCamera();
        }
        #endregion
    }
}
