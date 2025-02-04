using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
namespace SYA
{
    public partial class AddReparing : Form
    {
        // Fields
        private AutoCompleteStringCollection nameCollection = new AutoCompleteStringCollection();
        private HashSet<string> nameSet = new HashSet<string>(); // Quick lookup for names
        private FilterInfoCollection videoDevices;            // List of available cameras
        private VideoCaptureDevice videoSource;                 // The selected camera
        private Bitmap capturedImage;                           // Store captured image
        private string imagePath;                               // Store image path
        // Constructor
        public AddReparing()
        {
            InitializeComponent();
            LoadAutoCompleteNames();
            txtName.TextChanged += TxtName_TextChanged; // Detect changes in txtName
        }
        // Form Load & Initialization
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
            StartCamera();
            // Load initial data
            LoadTypeRadioButtons();
            LoadUserRadioButtons();
            LoadPriorityRadioButtons();
            // Initially hide panels
            tableLayoutPanel15.Visible = false;
            // Attach common events for radio buttons and checkboxes
            AttachSelectionEvents();
        }
        // --- Event Wiring Methods ---
        private void AttachSelectionEvents()
        {
            // Attach CheckedChanged event to all radio buttons
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
            // Attach CheckedChanged event to all checkboxes
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
        // --- RadioButton and Checkbox Event Handlers & Styling ---
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton selectedRadioButton)
            {
                UpdateRadioButtonStyle(selectedRadioButton, selectedRadioButton.Checked);
            }
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox selectedCheckBox)
            {
                StyleRepairCheckBox(selectedCheckBox);
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
                        rb.BackColor = Color.FromArgb(244, 244, 213);
                    }
                }
                radioButton.BackColor = Color.FromArgb(212, 163, 115);
            }
            else
            {
                radioButton.BackColor = Color.Transparent;
            }
        }
        // Dedicated styling function for REPAIR checkboxes
        private void StyleRepairCheckBox(CheckBox cb)
        {
            if (cb.Checked)
            {
                cb.BackColor = Color.FromArgb(212, 163, 115); // Distinct color for checked repair checkboxes
            }
            else
            {
                cb.BackColor = Color.FromArgb(244, 244, 213);
            }
        }
        // --- Data Loading Methods ---
        private void LoadSubTypeData(string subGroup)
        {
            try
            {
                string query = $"SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'SUB_TYPE' AND [SUB_GROUP] = '{subGroup}'";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show($"No data found for {subGroup}");
                    return;
                }
                // Prepare lists for radio buttons and checkboxes.
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
                // Reset and hide all controls
                foreach (var rb in radioButtons)
                {
                    rb.Visible = false;
                    rb.Checked = false;
                    rb.BackColor = Color.FromArgb(244, 244, 213);
                }
                foreach (var cb in checkBoxes)
                {
                    cb.Visible = false;
                    cb.Checked = false;
                    cb.BackColor = Color.FromArgb(244, 244, 213);
                }
                // Populate controls with data
                for (int i = 0; i < dt.Rows.Count && i < radioButtons.Count; i++)
                {
                    string name = dt.Rows[i]["NAME"].ToString();
                    radioButtons[i].Text = name;
                    radioButtons[i].Visible = true;
                    checkBoxes[i].Text = name;
                    checkBoxes[i].Visible = true;
                }
                // For KARIGAR subtypes, you could auto-select and style the first radio button here if desired.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subtypes: {ex.Message}");
            }
        }
        private void LoadTypeRadioButtons()
        {
            try
            {
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'TYPE' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                List<RadioButton> typeRadios = new List<RadioButton>
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
                    typeRadios[i].CheckedChanged += TypeRadioButton_CheckedChanged;
                    // Optionally pre-select the first type if desired.
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
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'USER' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                List<RadioButton> userRadios = new List<RadioButton>
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
                    // Auto-select and style the first user radio button.
                    if (i == 0)
                    {
                        userRadios[i].Checked = true;
                        UpdateRadioButtonStyle(userRadios[i], true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }
        private void LoadPriorityRadioButtons()
        {
            try
            {
                string query = "SELECT NAME FROM RepairingHelper WHERE [GROUP] = 'PRIORITY' ORDER BY ID";
                DataTable dt = helper.FetchDataTableFromSYADataBase(query);
                List<RadioButton> priorityRadios = new List<RadioButton>
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
                    if (i == 0)
                    {
                        priorityRadios[i].Checked = true;
                        UpdateRadioButtonStyle(priorityRadios[i], true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading priorities: " + ex.Message);
            }
        }
        // Event handler for type radio button changes
        private void TypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton selectedRadioButton && selectedRadioButton.Checked)
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
        // Reset visual feedback for sub type controls
        private void ResetSubTypeSelections()
        {
            foreach (RadioButton rb in new List<RadioButton>
            {
                radioButton15, radioButton16, radioButton17, radioButton18, radioButton19, radioButton20,
                radioButton21, radioButton22, radioButton23, radioButton24, radioButton25, radioButton26,
                radioButton27, radioButton28, radioButton29, radioButton30
            })
            {
                rb.Checked = false;
                rb.BackColor = Color.Transparent;
            }
            foreach (CheckBox cb in new List<CheckBox>
            {
                checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6,
                checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12,
                checkBox13, checkBox14, checkBox15, checkBox16
            })
            {
                cb.Checked = false;
                cb.BackColor = Color.Transparent;
            }
        }
        // Highlight the selected radio button (for types)
        private void HighlightRadioButton(RadioButton selectedRadioButton)
        {
            foreach (RadioButton rb in new List<RadioButton> { radioButton11, radioButton12, radioButton13, radioButton14 })
            {
                rb.BackColor = Color.Transparent;
            }
            selectedRadioButton.BackColor = Color.LightBlue;
        }
        // --- Camera Functions ---
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
                    pictureBox1.Image?.Dispose();
                    Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
                    frame.RotateFlip(RotateFlipType.RotateNoneFlipX); // Correct mirror effect
                    pictureBox1.Image = frame;
                }));
            }
            else
            {
                pictureBox1.Image?.Dispose();
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
                frame.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Image = frame;
            }
        }
        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
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
                        videoSource = null;
                    }
                });
            }
        }
        // --- Key & Text Handling ---
        private void AttachKeyPressEvent(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txtBox)
                {
                    txtBox.KeyPress += EditingControl_KeyPress;
                }
                else if (ctrl.HasChildren)
                {
                    AttachKeyPressEvent(ctrl);
                }
            }
        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }
        private void AllowOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
                if (e.KeyChar == '.' && txtBox.Text.Contains("."))
                    e.Handled = true;
            }
        }
        private void FormatDecimal(object sender, EventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                if (decimal.TryParse(txtBox.Text, out decimal result))
                {
                    if (txtBox.Text.Contains("."))
                        txtBox.Text = result.ToString("0.000");
                }
                else
                {
                    txtBox.Text = "0";
                }
            }
        }
        // --- Database & AutoComplete ---
        private void LoadAutoCompleteNames()
        {
            try
            {
                string query = "SELECT DISTINCT AC_NAME FROM AC_MAST";
                DataTable dt = helper.FetchDataTableFromDataCareDataBase(query);
                if (dt.Rows.Count > 0)
                {
                    nameCollection.Clear();
                    nameSet.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["AC_NAME"].ToString();
                        nameCollection.Add(name);
                        nameSet.Add(name);
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
        // --- Other Event Handlers ---
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl == buttonReCapture)
                {
                    buttonReCapture.PerformClick();
                    return true;
                }
                else
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            if (nameSet.Contains(txtName.Text))
            {
                FillMobileNumber();
            }
            else
            {
                txtNumber.Text = "";
            }
            void FillMobileNumber()
            {
                try
                {
                    string query = $"SELECT AC_MOBILE FROM AC_MAST WHERE AC_NAME = '{txtName.Text}'";
                    DataTable dt = helper.FetchDataTableFromDataCareDataBase(query);
                    txtNumber.Text = dt.Rows.Count > 0 ? dt.Rows[0]["AC_MOBILE"].ToString() : "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching mobile number: " + ex.Message);
                }
            }
        }
        private void update_insert()
        {
            // Gather common data from input controls
            string NAME = txtName.Text.Trim();
            string NUMBER = string.IsNullOrWhiteSpace(txtNumber.Text) ? "NULL" : txtNumber.Text.Trim();
            string WEIGHT = string.IsNullOrWhiteSpace(txtWeight.Text) ? "NULL" : txtWeight.Text.Trim();
            string ESTIMATE_COST = string.IsNullOrWhiteSpace(txtEstimate.Text) ? "NULL" : txtEstimate.Text.Trim();
            string BOOK_DATE = DateTime.Today.ToString("yyyy-MM-dd");
            string DELIVERY_DATE = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string BOOK_TIME = DateTime.Now.ToString("HH:mm:ss");
            string UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Get selected values from the panels
            string TYPE = GetSelectedRadioButton(panelType);
            string PRIORITY = GetSelectedRadioButton(panelPriority);
            string CREATED_BY = GetSelectedRadioButton(panelUser);

            // Determine the SUB_TYPE based on TYPE:
            string SUB_TYPE = "";
            if (TYPE == "REPAIR")
            {
                // When TYPE is REPAIR, sub type is selected via checkboxes.
                SUB_TYPE = GetSelectedCheckBoxes(panelCheck);
            }
            else if (TYPE == "KARIGAR")
            {
                // When TYPE is KARIGAR, sub type is selected via radio buttons.
                SUB_TYPE = GetSelectedRadioButton(panelRadio);
            }

            string IMAGE_PATH = string.IsNullOrEmpty(imagePath) ? "" : imagePath;
            string COMMENT = richTextBox1.Text.Trim();
            string STATUS = "New";

            // Build the INSERT query
            string insertQuery = "INSERT INTO RepairingData (NAME, NUMBER, WEIGHT, ESTIMATE_COST, BOOK_DATE, DELIVERY_DATE, BOOK_TIME, UPDATE_TIME, TYPE, SUB_TYPE, CREATED_BY, PRIORITY, IMAGE_PATH, COMMENT, STATUS) " +
                                 "VALUES ('" + NAME + "', " + NUMBER + ", " + WEIGHT + ", " + ESTIMATE_COST + ", '" + BOOK_DATE + "', '" + DELIVERY_DATE + "', '" + BOOK_TIME + "', '" + UPDATE_TIME + "', " +
                                 "'" + TYPE + "', '" + SUB_TYPE + "', '" + CREATED_BY + "', '" + PRIORITY + "', '" + IMAGE_PATH + "', '" + COMMENT + "', '" + STATUS + "')";

            helper.RunQueryWithoutParametersSYADataBase(insertQuery, "ExecuteNonQuery");

            // Optionally, call the print function
            print();
        }

        private void print()
        {
            // Determine the type and sub type accordingly.
            string type = GetSelectedRadioButton(panelType);
            string subType = "";

            if (type == "REPAIR")
            {
                // For REPAIR, sub type comes from checkboxes.
                subType = GetSelectedCheckBoxes(panelCheck);
            }
            else if (type == "KARIGAR")
            {
                // For KARIGAR, sub type comes from radio buttons (assumed to be in panelRadio).
                subType = GetSelectedRadioButton(panelRadio);
            }
            // Otherwise, subType remains an empty string.

            List<string> repairData = new List<string>
    {
        txtName.Text.Trim(),
        string.IsNullOrWhiteSpace(txtNumber.Text) ? "" : txtNumber.Text.Trim(),
        string.IsNullOrWhiteSpace(txtWeight.Text) ? "" : txtWeight.Text.Trim(),
        string.IsNullOrWhiteSpace(txtEstimate.Text) ? "" : txtEstimate.Text.Trim(),
        DateTime.Today.ToString("yyyy-MM-dd"),
        dateTimePicker1.Value.ToString("yyyy-MM-dd"),
        DateTime.Now.ToString("HH:mm:ss"),
        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        type,                                         // TYPE from panelType
        GetSelectedRadioButton(panelPriority),        // PRIORITY
        GetSelectedRadioButton(panelUser),            // CREATED_BY
        subType,                                      // SUB_TYPE (based on TYPE)
        string.IsNullOrEmpty(imagePath) ? "" : imagePath,
        richTextBox1.Text.Trim(),
        "New"
    };

            ThermalPrinter printer = new ThermalPrinter();
            printer.PrintReceipt(repairData);
        }

        private string GetSelectedRadioButton(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is RadioButton rb && rb.Checked)
                {
                    return rb.Text;
                }
                else if (ctrl.HasChildren) // Recursively search inside nested containers
                {
                    string selected = GetSelectedRadioButton(ctrl);
                    if (!string.IsNullOrEmpty(selected))
                    {
                        return selected;
                    }
                }
            }
            return ""; // Return empty if no selection is found
        }


        private string GetSelectedCheckBoxes(Control parent)
        {
            List<string> selectedValues = new List<string>();

            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is CheckBox cb && cb.Checked)
                {
                    selectedValues.Add(cb.Text);
                }
                else if (ctrl.HasChildren) // Recursively check nested controls
                {
                    selectedValues.AddRange(GetSelectedCheckBoxes(ctrl).Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return string.Join(", ", selectedValues); // Return all checked values as a comma-separated string
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
                string folderPath = @"C:\SYA_APP\Images";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileName = "Repair_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
                imagePath = Path.Combine(folderPath, fileName);
                pictureBox1.Image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Image Captured Successfully.");
                StopCamera();
            }
        }
        private void AddReparing_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopCamera();
        }

        private void radioButton17_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void Print(object sender, PrintPageEventArgs e)
        {
            PrintHelper.PrintLabel(sender, e, "hi0", "hi1", "hi3");
        }

    }
}
