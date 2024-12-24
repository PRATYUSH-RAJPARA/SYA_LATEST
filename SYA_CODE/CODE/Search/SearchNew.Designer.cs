namespace SYA
{
    partial class SearchNew
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            LABEL_MESSAGE = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            BUTTON_PRINT_ON_OFF = new Button();
            BUTTON_FETCH_DATA = new Button();
            BUTTON_RESET_FILTERS = new Button();
            TB_EVERYTHING = new TableLayoutPanel();
            richTextBox8 = new RichTextBox();
            TB_HUID = new RichTextBox();
            TB_BILLNO = new RichTextBox();
            TB_WEIGHT = new RichTextBox();
            TB_TAGNO = new RichTextBox();
            label3 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label1 = new Label();
            label2 = new Label();
            CB_NAME = new ComboBox();
            CB_YEAR = new ComboBox();
            dataGridView1 = new DataGridView();
            label8 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            TB_EVERYTHING.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 99F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 1);
            tableLayoutPanel1.Controls.Add(label8, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 94F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            tableLayoutPanel1.Size = new Size(1616, 843);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(LABEL_MESSAGE, 0, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 3);
            tableLayoutPanel2.Controls.Add(TB_EVERYTHING, 0, 0);
            tableLayoutPanel2.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(11, 45);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 78F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));
            tableLayoutPanel2.Size = new Size(1593, 786);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // LABEL_MESSAGE
            // 
            LABEL_MESSAGE.Dock = DockStyle.Fill;
            LABEL_MESSAGE.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            LABEL_MESSAGE.Location = new Point(3, 691);
            LABEL_MESSAGE.Name = "LABEL_MESSAGE";
            LABEL_MESSAGE.Size = new Size(1587, 39);
            LABEL_MESSAGE.TabIndex = 4;
            LABEL_MESSAGE.Text = "No Updates !";
            LABEL_MESSAGE.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 15;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.Controls.Add(BUTTON_PRINT_ON_OFF, 9, 1);
            tableLayoutPanel4.Controls.Add(BUTTON_FETCH_DATA, 11, 1);
            tableLayoutPanel4.Controls.Add(BUTTON_RESET_FILTERS, 13, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 733);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 5;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel4.Size = new Size(1587, 50);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // BUTTON_PRINT_ON_OFF
            // 
            BUTTON_PRINT_ON_OFF.Dock = DockStyle.Fill;
            BUTTON_PRINT_ON_OFF.FlatStyle = FlatStyle.Flat;
            BUTTON_PRINT_ON_OFF.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            BUTTON_PRINT_ON_OFF.Location = new Point(918, 5);
            BUTTON_PRINT_ON_OFF.Name = "BUTTON_PRINT_ON_OFF";
            BUTTON_PRINT_ON_OFF.Size = new Size(184, 34);
            BUTTON_PRINT_ON_OFF.TabIndex = 2;
            BUTTON_PRINT_ON_OFF.Text = "PRINTING OFF";
            BUTTON_PRINT_ON_OFF.UseVisualStyleBackColor = true;
            // 
            // BUTTON_FETCH_DATA
            // 
            BUTTON_FETCH_DATA.Dock = DockStyle.Fill;
            BUTTON_FETCH_DATA.FlatStyle = FlatStyle.Flat;
            BUTTON_FETCH_DATA.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            BUTTON_FETCH_DATA.Location = new Point(1139, 5);
            BUTTON_FETCH_DATA.Name = "BUTTON_FETCH_DATA";
            BUTTON_FETCH_DATA.Size = new Size(184, 34);
            BUTTON_FETCH_DATA.TabIndex = 0;
            BUTTON_FETCH_DATA.Text = "FETCH DATA";
            BUTTON_FETCH_DATA.UseVisualStyleBackColor = true;
            // 
            // BUTTON_RESET_FILTERS
            // 
            BUTTON_RESET_FILTERS.Dock = DockStyle.Fill;
            BUTTON_RESET_FILTERS.FlatStyle = FlatStyle.Flat;
            BUTTON_RESET_FILTERS.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            BUTTON_RESET_FILTERS.Location = new Point(1360, 5);
            BUTTON_RESET_FILTERS.Name = "BUTTON_RESET_FILTERS";
            BUTTON_RESET_FILTERS.Size = new Size(184, 34);
            BUTTON_RESET_FILTERS.TabIndex = 1;
            BUTTON_RESET_FILTERS.Text = "RESET FILTERS";
            BUTTON_RESET_FILTERS.UseVisualStyleBackColor = true;
            // 
            // TB_EVERYTHING
            // 
            TB_EVERYTHING.ColumnCount = 15;
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            TB_EVERYTHING.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            TB_EVERYTHING.Controls.Add(richTextBox8, 13, 3);
            TB_EVERYTHING.Controls.Add(TB_HUID, 11, 3);
            TB_EVERYTHING.Controls.Add(TB_BILLNO, 9, 3);
            TB_EVERYTHING.Controls.Add(TB_WEIGHT, 7, 3);
            TB_EVERYTHING.Controls.Add(TB_TAGNO, 5, 3);
            TB_EVERYTHING.Controls.Add(label3, 5, 1);
            TB_EVERYTHING.Controls.Add(label7, 13, 1);
            TB_EVERYTHING.Controls.Add(label6, 11, 1);
            TB_EVERYTHING.Controls.Add(label5, 9, 1);
            TB_EVERYTHING.Controls.Add(label4, 7, 1);
            TB_EVERYTHING.Controls.Add(label1, 1, 1);
            TB_EVERYTHING.Controls.Add(label2, 3, 1);
            TB_EVERYTHING.Controls.Add(CB_NAME, 1, 3);
            TB_EVERYTHING.Controls.Add(CB_YEAR, 3, 3);
            TB_EVERYTHING.Dock = DockStyle.Fill;
            TB_EVERYTHING.Location = new Point(3, 3);
            TB_EVERYTHING.Name = "TB_EVERYTHING";
            TB_EVERYTHING.RowCount = 5;
            TB_EVERYTHING.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            TB_EVERYTHING.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            TB_EVERYTHING.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            TB_EVERYTHING.RowStyles.Add(new RowStyle(SizeType.Percent, 54F));
            TB_EVERYTHING.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            TB_EVERYTHING.Size = new Size(1587, 72);
            TB_EVERYTHING.TabIndex = 0;
            // 
            // richTextBox8
            // 
            richTextBox8.BorderStyle = BorderStyle.FixedSingle;
            richTextBox8.Dock = DockStyle.Fill;
            richTextBox8.Font = new Font("Arial Rounded MT Bold", 15.25F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox8.Location = new Point(1360, 31);
            richTextBox8.Multiline = false;
            richTextBox8.Name = "richTextBox8";
            richTextBox8.Size = new Size(184, 32);
            richTextBox8.TabIndex = 4;
            richTextBox8.Text = "";
            // 
            // TB_HUID
            // 
            TB_HUID.BorderStyle = BorderStyle.FixedSingle;
            TB_HUID.Dock = DockStyle.Fill;
            TB_HUID.Font = new Font("Arial Rounded MT Bold", 15.25F, FontStyle.Regular, GraphicsUnit.Point);
            TB_HUID.Location = new Point(1139, 31);
            TB_HUID.Multiline = false;
            TB_HUID.Name = "TB_HUID";
            TB_HUID.Size = new Size(184, 32);
            TB_HUID.TabIndex = 4;
            TB_HUID.Text = "";
            // 
            // TB_BILLNO
            // 
            TB_BILLNO.BorderStyle = BorderStyle.FixedSingle;
            TB_BILLNO.Dock = DockStyle.Fill;
            TB_BILLNO.Font = new Font("Arial Rounded MT Bold", 15.25F, FontStyle.Regular, GraphicsUnit.Point);
            TB_BILLNO.Location = new Point(918, 31);
            TB_BILLNO.Multiline = false;
            TB_BILLNO.Name = "TB_BILLNO";
            TB_BILLNO.Size = new Size(184, 32);
            TB_BILLNO.TabIndex = 4;
            TB_BILLNO.Text = "";
            // 
            // TB_WEIGHT
            // 
            TB_WEIGHT.BorderStyle = BorderStyle.FixedSingle;
            TB_WEIGHT.Dock = DockStyle.Fill;
            TB_WEIGHT.Font = new Font("Arial Rounded MT Bold", 15.25F, FontStyle.Regular, GraphicsUnit.Point);
            TB_WEIGHT.Location = new Point(697, 31);
            TB_WEIGHT.Multiline = false;
            TB_WEIGHT.Name = "TB_WEIGHT";
            TB_WEIGHT.Size = new Size(184, 32);
            TB_WEIGHT.TabIndex = 4;
            TB_WEIGHT.Text = "";
            // 
            // TB_TAGNO
            // 
            TB_TAGNO.BorderStyle = BorderStyle.FixedSingle;
            TB_TAGNO.Dock = DockStyle.Fill;
            TB_TAGNO.Font = new Font("Arial Rounded MT Bold", 15.25F, FontStyle.Regular, GraphicsUnit.Point);
            TB_TAGNO.Location = new Point(476, 31);
            TB_TAGNO.Multiline = false;
            TB_TAGNO.Name = "TB_TAGNO";
            TB_TAGNO.Size = new Size(184, 32);
            TB_TAGNO.TabIndex = 4;
            TB_TAGNO.Text = "";
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(476, 2);
            label3.Name = "label3";
            label3.Size = new Size(184, 24);
            label3.TabIndex = 2;
            label3.Text = "TAG NO";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(1360, 2);
            label7.Name = "label7";
            label7.Size = new Size(184, 24);
            label7.TabIndex = 3;
            label7.Text = "EVERYTHING";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(1139, 2);
            label6.Name = "label6";
            label6.Size = new Size(184, 24);
            label6.TabIndex = 3;
            label6.Text = "HUID";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(918, 2);
            label5.Name = "label5";
            label5.Size = new Size(184, 24);
            label5.TabIndex = 3;
            label5.Text = "BILL NO";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(697, 2);
            label4.Name = "label4";
            label4.Size = new Size(184, 24);
            label4.TabIndex = 3;
            label4.Text = "WEIGHT";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(34, 2);
            label1.Name = "label1";
            label1.Size = new Size(184, 24);
            label1.TabIndex = 0;
            label1.Text = "NAME";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(255, 2);
            label2.Name = "label2";
            label2.Size = new Size(184, 24);
            label2.TabIndex = 1;
            label2.Text = "YEAR";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CB_NAME
            // 
            CB_NAME.Dock = DockStyle.Fill;
            CB_NAME.Font = new Font("Arial Rounded MT Bold", 15.25F, FontStyle.Regular, GraphicsUnit.Point);
            CB_NAME.FormattingEnabled = true;
            CB_NAME.Location = new Point(34, 31);
            CB_NAME.Name = "CB_NAME";
            CB_NAME.Size = new Size(184, 31);
            CB_NAME.TabIndex = 5;
            // 
            // CB_YEAR
            // 
            CB_YEAR.Dock = DockStyle.Fill;
            CB_YEAR.Font = new Font("Arial Rounded MT Bold", 15.25F, FontStyle.Regular, GraphicsUnit.Point);
            CB_YEAR.FormattingEnabled = true;
            CB_YEAR.Location = new Point(255, 31);
            CB_YEAR.Name = "CB_YEAR";
            CB_YEAR.Size = new Size(184, 31);
            CB_YEAR.TabIndex = 6;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 81);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1587, 607);
            dataGridView1.TabIndex = 1;
            // 
            // label8
            // 
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(11, 0);
            label8.Name = "label8";
            label8.Size = new Size(1593, 42);
            label8.TabIndex = 5;
            label8.Text = "SEARCH";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SearchNew
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1616, 843);
            Controls.Add(tableLayoutPanel1);
            Name = "SearchNew";
            Text = "SearchNew";
            Load += SearchNew_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            TB_EVERYTHING.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }
        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel TB_EVERYTHING;
        private Label label3;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label1;
        private Label label2;
        public DataGridView dataGridView1;
        private RichTextBox richTextBox8;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label8;
        private ComboBox CB_YEAR;
        private ComboBox CB_NAME;
        private Button BUTTON_FETCH_DATA;
        private Button BUTTON_RESET_FILTERS;
        private Button BUTTON_PRINT_ON_OFF;
        private Label LABEL_MESSAGE;
        private RichTextBox TB_HUID;
        private RichTextBox TB_BILLNO;
        private RichTextBox TB_WEIGHT;
        private RichTextBox TB_TAGNO;
    }
}