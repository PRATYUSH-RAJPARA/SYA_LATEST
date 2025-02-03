using System;
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
        #region Form Initialization & Load
        private void AddReparing_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            txtWeight.KeyPress += AllowOnlyNumeric;
            txtNumber.KeyPress += AllowOnlyNumeric;
            txtEstimate.KeyPress += AllowOnlyNumeric;
            txtWeight.Leave += FormatDecimal;
            txtNumber.Leave += FormatDecimal;
            txtEstimate.Leave += FormatDecimal;
            AttachKeyPressEvent(this);

            // Start Camera
            StartCamera();

            // Load Data
            LoadTypeRadioButtons();
            LoadUserRadioButtons();
            LoadPriorityRadioButtons();

            // Initially Hide Panels
            tableLayoutPanel15.Visible = false;

            // Attach events to all radio buttons and checkboxes
            AttachSelectionEvents();
        }

        // Attach events to radio buttons and checkboxes
        private void AttachSelectionEvents()
        {
            foreach (var rb in new List<RadioButton>
    {
        radioButton11, radioButton12, radioButton13, radioButton14,
        radioButton15, radioButton16, radioButton17, radioButton18,
        radioButton19, radioButton20, radioButton21, radioButton22,
        radioButton23, radioButton24, radioButton25, radioButton26,
        radioButton27, radioButton28, radioButton29, radioButton30,
        rbUser1, rbUser2, rbUser3, rbUser4, rbUser5, rbUser6,
        rbPriority1, rbPriority2, rbPriority3, rbPriority4
    })
            {
                rb.CheckedChanged += RadioButton_CheckedChanged;
            }

            foreach (var cb in new List<CheckBox>
    {
        checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6,
        checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12,
        checkBox13, checkBox14, checkBox15, checkBox16
    })
            {
                cb.CheckedChanged += CheckBox_CheckedChanged;
            }
        }
        #endregion
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton != null)
            {
                UpdateRadioButtonStyle(selectedRadioButton, selectedRadioButton.Checked);
            }
        }

        // **Reset Background Color of All Radio Buttons**
        private void ResetRadioButtonColors()
        {
            foreach (var rb in new List<RadioButton>
    {
        radioButton11, radioButton12, radioButton13, radioButton14,
        radioButton15, radioButton16, radioButton17, radioButton18,
        radioButton19, radioButton20, radioButton21, radioButton22,
        radioButton23, radioButton24, radioButton25, radioButton26,
        radioButton27, radioButton28, radioButton29, radioButton30,
        rbUser1, rbUser2, rbUser3, rbUser4, rbUser5, rbUser6,
        rbPriority1, rbPriority2, rbPriority3, rbPriority4
    })
            {
                rb.BackColor = Color.Transparent; // Reset background color
            }
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectedCheckBox = sender as CheckBox;
            if (selectedCheckBox != null)
            {
                // Highlight checked checkbox
                if (selectedCheckBox.Checked)
                {
                    selectedCheckBox.BackColor = Color.LightGreen; // Highlight checkbox
                }
                else
                {
                    selectedCheckBox.BackColor = Color.Transparent; // Reset color when unchecked
                }
            }
        }

        #region SubType Data Loading
        private void LoadSubTypeData(string subGroup)
        {
            try
            {
                string query = $"SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'SUB_TYPE' AND [SUB_GROUP] = '{subGroup}'";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);

                // Debug: Check if data is available
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show($"No data found for {subGroup}");
                    return;
                }

                // Lists of radio buttons and checkboxes.
                List<RadioButton> radioButtons = new List<RadioButton>
        {
            radioButton15, radioButton16, radioButton17, radioButton18, radioButton19, radioButton20,
            radioButton21, radioButton22, radioButton23, radioButton24, radioButton25, radioButton26,
            radioButton27, radioButton28, radioButton29, radioButton30
        };

                List<CheckBox> checkBoxes = new List<CheckBox>
        {
            checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6,
            checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12,
            checkBox13, checkBox14, checkBox15, checkBox16
        };

                // Hide and reset all controls.
                foreach (var rb in radioButtons)
                {
                    rb.Visible = false;
                    rb.Checked = false;
                    rb.BackColor = Color.Transparent;
                }
                foreach (var cb in checkBoxes)
                {
                    cb.Visible = false;
                    cb.Checked = false;
                    cb.BackColor = Color.Transparent;
                }

                // Assign names from the database to radio buttons and checkboxes.
                for (int i = 0; i < dt.Rows.Count && i < radioButtons.Count; i++)
                {
                    string name = dt.Rows[i]["NAME"].ToString();
                    radioButtons[i].Text = name;
                    radioButtons[i].Visible = true;

                    // Also assign text to checkboxes (used for REPAIR)
                    checkBoxes[i].Text = name;
                    checkBoxes[i].Visible = true;
                }

                // For KARIGAR subtypes, auto-select the first available radio button and style it.
                if (subGroup == "KARIGAR")
                {
                    foreach (var rb in radioButtons)
                    {
                        if (rb.Visible)
                        {
                            rb.Checked = true;  // Automatically select the first visible radio button.
                            UpdateRadioButtonStyle(rb, true); // Manually update its styling.
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subtypes: {ex.Message}");
            }
        }

        #endregion

        #region Type Radio Buttons Handling
        private void TypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton != null && selectedRadioButton.Checked)
            {
                string selectedType = selectedRadioButton.Text;

                ResetSubTypeSelections();
                HighlightRadioButton(selectedRadioButton);

                if (selectedType == "REPAIR")
                {
                    tableLayoutPanel15.Visible = true;
                    panelRadio.Visible = false;
                    panelCheck.Visible = true;

                    panelRadio.Dock = DockStyle.None;
                    panelCheck.Dock = DockStyle.Fill;

                    LoadSubTypeData("REPAIR");

                    // ✅ Force refresh to ensure visibility
                    panelCheck.Refresh();
                    tableLayoutPanel15.Refresh();
                }
                else if (selectedType == "KARIGAR")
                {
                    tableLayoutPanel15.Visible = true;
                    panelRadio.Visible = true;
                    panelCheck.Visible = false;

                    panelCheck.Dock = DockStyle.None;
                    panelRadio.Dock = DockStyle.Fill;

                    LoadSubTypeData("KARIGAR");

                    // ✅ Force refresh
                    panelRadio.Refresh();
                    tableLayoutPanel15.Refresh();
                }
                else
                {
                    tableLayoutPanel15.Visible = false;
                    panelRadio.Visible = false;
                    panelCheck.Visible = false;
                }
            }
        }

        private void ResetSubTypeSelections()
        {
            // Reset the radio buttons and checkboxes visual feedback
            foreach (RadioButton rb in new List<RadioButton> { radioButton15, radioButton16, radioButton17, radioButton18, radioButton19, radioButton20,
                                                             radioButton21, radioButton22, radioButton23, radioButton24, radioButton25, radioButton26,
                                                             radioButton27, radioButton28, radioButton29, radioButton30 })
            {
                rb.Checked = false; // Uncheck the radio buttons
                rb.BackColor = Color.Transparent; // Reset background color
            }

            foreach (CheckBox cb in new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6,
                                                        checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12,
                                                        checkBox13, checkBox14, checkBox15, checkBox16 })
            {
                cb.Checked = false; // Uncheck the checkboxes
                cb.BackColor = Color.Transparent; // Reset background color
            }
        }
        #endregion

        #region Highlighting Visual Feedback for Radio Button
        private void HighlightRadioButton(RadioButton selectedRadioButton)
        {
            // Reset the background color for all radio buttons
            foreach (var rb in new List<RadioButton> { radioButton11, radioButton12, radioButton13, radioButton14 })
            {
                rb.BackColor = Color.Transparent; // Reset background color
            }

            // Highlight the selected radio button
            selectedRadioButton.BackColor = Color.LightBlue; // Change to a highlighted color (e.g., light blue)
        }
        #endregion

        #region Checkbox Highlighting

        // Highlight selected checkbox
        private void HighlightCheckBox(CheckBox selectedCheckBox)
        {
            // Reset the background color for all checkboxes
            foreach (var cb in new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6,
                                                    checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12,
                                                    checkBox13, checkBox14, checkBox15, checkBox16 })
            {
                cb.BackColor = Color.Transparent; // Reset background color
            }

            // Highlight the selected checkbox
            selectedCheckBox.BackColor = Color.LightGreen; // Change to a highlighted color (e.g., light green)
        }
        #endregion

        #region Priority Radio Buttons Handling
        private void LoadPriorityRadioButtons()
        {
            try
            {
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'PRIORITY' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);

                List<RadioButton> priorityRadios = new List<RadioButton>()
        {
            rbPriority1, rbPriority2, rbPriority3, rbPriority4
        };

                foreach (RadioButton rb in priorityRadios)
                {
                    rb.Visible = false;
                }

                for (int i = 0; i < dt.Rows.Count && i < priorityRadios.Count; i++)
                {
                    priorityRadios[i].Text = dt.Rows[i]["NAME"].ToString();
                    priorityRadios[i].Visible = true;

                    // Select the first radio button
                    if (i == 0)
                    {
                        priorityRadios[i].Checked = true;
                        // Manually trigger styling
                        UpdateRadioButtonStyle(priorityRadios[i], true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading priorities: " + ex.Message);
            }
        }
        #endregion        #endregion

        #region Type Radio Buttons Loading
        private void LoadTypeRadioButtons()
        {
            try
            {
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'TYPE' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);

                List<RadioButton> typeRadios = new List<RadioButton>()
        {
            radioButton11, radioButton12, radioButton13, radioButton14
        };

                foreach (RadioButton rb in typeRadios)
                {
                    rb.Visible = false;
                }

                for (int i = 0; i < dt.Rows.Count && i < typeRadios.Count; i++)
                {
                    typeRadios[i].Text = dt.Rows[i]["NAME"].ToString();
                    typeRadios[i].Visible = true;
                    typeRadios[i].CheckedChanged += TypeRadioButton_CheckedChanged; // Attach event

                    // Select the first radio button
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading types: " + ex.Message);
            }
        }
        #endregion        #endregion

       
        private void LoadUserRadioButtons()
        {
            try
            {
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'USER' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);

                List<RadioButton> userRadios = new List<RadioButton>()
        {
            rbUser1, rbUser2, rbUser3, rbUser4, rbUser5, rbUser6
        };

                foreach (RadioButton rb in userRadios)
                {
                    rb.Visible = false;
                }

                for (int i = 0; i < dt.Rows.Count && i < userRadios.Count; i++)
                {
                    userRadios[i].Text = dt.Rows[i]["NAME"].ToString();
                    userRadios[i].Visible = true;

                    // Select the first radio button
                    if (i == 0)
                    {
                        userRadios[i].Checked = true;
                        // Manually trigger styling since CheckedChanged won't fire programmatically
                        UpdateRadioButtonStyle(userRadios[i], true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }
        private void UpdateRadioButtonStyle(RadioButton radioButton, bool isChecked)
        {
            if (isChecked)
            {
                // Reset all radio buttons in the same container
                foreach (Control ctrl in radioButton.Parent.Controls)
                {
                    if (ctrl is RadioButton rb)
                    {
                        rb.BackColor = Color.Transparent;
                    }
                }
                // Highlight the selected radio button
                radioButton.BackColor = Color.LightBlue;
            }
            else
            {
                radioButton.BackColor = Color.Transparent;
            }
        }
        /// <summary>
        /// Applies the user-specific styling to the selected radio button.
        /// It resets the background for all user radio buttons and highlights the selected one.
        /// </summary>
        private void StyleUserRadioButton(RadioButton selectedRadioButton)
        {
            // List all user radio buttons.
            List<RadioButton> userRadios = new List<RadioButton>()
    {
        rbUser1, rbUser2, rbUser3, rbUser4, rbUser5, rbUser6
    };

            // Reset styling for all radio buttons in the user group.
            foreach (RadioButton rb in userRadios)
            {
                rb.BackColor = Color.Transparent;
            }

            // Apply the highlight to the selected radio button.
            selectedRadioButton.BackColor = Color.LightBlue;
        }

        // Optionally, if you want the styling to update when the user selects a different option:
        private void UserRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                StyleUserRadioButton(rb);
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
            string DELIVERY_DATE = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string BOOK_TIME = DateTime.Now.ToString("HH:mm:ss");
            string UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // ✅ Get Selected Radio Button Values
            string TYPE = GetSelectedRadioButton(panelType);
            string PRIORITY = GetSelectedRadioButton(panelPriority);
            string CREATED_BY = GetSelectedRadioButton(panelUser);

            // ✅ Get Selected Checkboxes as a Comma-Separated String
            string SUB_TYPE = GetSelectedCheckBoxes(panelCheck);
            string IMAGE_PATH = string.IsNullOrEmpty(imagePath) ? "" : imagePath; // Assign captured image path
            string COMMENT = richTextBox1.Text.Trim();
            string STATUS = "New";
            string insertQuery = "INSERT INTO RepairingData (NAME, NUMBER, WEIGHT, ESTIMATE_COST, BOOK_DATE, DELIVERY_DATE, BOOK_TIME, UPDATE_TIME, TYPE, SUB_TYPE, CREATED_BY, PRIORITY, IMAGE_PATH, COMMENT, STATUS) " +
                                 "VALUES ('" + NAME + "', " + NUMBER + ", " + WEIGHT + ", " + ESTIMATE_COST + ", '" + BOOK_DATE + "', '" + DELIVERY_DATE + "', '" + BOOK_TIME + "', '" + UPDATE_TIME + "', " +
                                 "'" + TYPE + "', '" + SUB_TYPE + "', '" + CREATED_BY + "', '" + PRIORITY + "', '" + IMAGE_PATH + "', '" + COMMENT + "', '" + STATUS + "')";
            helper.RunQueryWithoutParametersSYADataBase(insertQuery, "ExecuteNonQuery");
        }
        private string GetSelectedRadioButton(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is RadioButton rb && rb.Checked)
                {
                    return rb.Text; // Return the text of the selected radio button
                }
            }
            return ""; // Return empty if no radio button is selected
        }
        private string GetSelectedCheckBoxes(Control parent)
        {
            List<string> selectedValues = new List<string>();

            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is CheckBox cb && cb.Checked)
                {
                    selectedValues.Add(cb.Text); // Add selected checkbox text to the list
                }
            }

            return string.Join(", ", selectedValues); // Combine into a comma-separated string
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
