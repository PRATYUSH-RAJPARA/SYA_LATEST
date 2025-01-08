namespace SYA
{
    partial class addItemNew
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
            label8 = new Label();
            BUTTON_FETCH_DATA = new Button();
            BUTTON_PRINT_ON_OFF = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            LABEL_MESSAGE = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            label1 = new Label();
            panel2 = new Panel();
            panel1 = new Panel();
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label8
            // 
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Microsoft Sans Serif", 24F);
            label8.Location = new Point(8, 0);
            label8.Name = "label8";
            label8.Size = new Size(1073, 43);
            label8.TabIndex = 5;
            label8.Text = "ADD ITEM";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BUTTON_FETCH_DATA
            // 
            BUTTON_FETCH_DATA.Dock = DockStyle.Fill;
            BUTTON_FETCH_DATA.FlatStyle = FlatStyle.Flat;
            BUTTON_FETCH_DATA.Font = new Font("Microsoft Sans Serif", 14.25F);
            BUTTON_FETCH_DATA.Location = new Point(445, 6);
            BUTTON_FETCH_DATA.Name = "BUTTON_FETCH_DATA";
            BUTTON_FETCH_DATA.Size = new Size(154, 42);
            BUTTON_FETCH_DATA.TabIndex = 0;
            BUTTON_FETCH_DATA.Text = "FETCH DATA";
            BUTTON_FETCH_DATA.UseVisualStyleBackColor = true;
            // 
            // BUTTON_PRINT_ON_OFF
            // 
            BUTTON_PRINT_ON_OFF.Dock = DockStyle.Fill;
            BUTTON_PRINT_ON_OFF.FlatStyle = FlatStyle.Flat;
            BUTTON_PRINT_ON_OFF.Font = new Font("Microsoft Sans Serif", 14.25F);
            BUTTON_PRINT_ON_OFF.Location = new Point(40, 6);
            BUTTON_PRINT_ON_OFF.Name = "BUTTON_PRINT_ON_OFF";
            BUTTON_PRINT_ON_OFF.Size = new Size(58, 42);
            BUTTON_PRINT_ON_OFF.TabIndex = 2;
            BUTTON_PRINT_ON_OFF.Text = "ON";
            BUTTON_PRINT_ON_OFF.UseVisualStyleBackColor = true;
            BUTTON_PRINT_ON_OFF.Click += BUTTON_PRINT_ON_OFF_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = SystemColors.ActiveCaption;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(LABEL_MESSAGE, 0, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 3);
            tableLayoutPanel2.Controls.Add(panel1, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(8, 46);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10.1008358F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 70.50878F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 12.319788F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 7.070585F));
            tableLayoutPanel2.Size = new Size(1073, 811);
            tableLayoutPanel2.TabIndex = 0;
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // LABEL_MESSAGE
            // 
            LABEL_MESSAGE.Dock = DockStyle.Fill;
            LABEL_MESSAGE.Font = new Font("Microsoft Sans Serif", 14.25F);
            LABEL_MESSAGE.Location = new Point(3, 652);
            LABEL_MESSAGE.Name = "LABEL_MESSAGE";
            LABEL_MESSAGE.Size = new Size(1067, 99);
            LABEL_MESSAGE.TabIndex = 4;
            LABEL_MESSAGE.Text = "No Updates !";
            LABEL_MESSAGE.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 9;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.5F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.5F));
            tableLayoutPanel4.Controls.Add(BUTTON_PRINT_ON_OFF, 2, 1);
            tableLayoutPanel4.Controls.Add(label1, 3, 1);
            tableLayoutPanel4.Controls.Add(BUTTON_FETCH_DATA, 6, 1);
            tableLayoutPanel4.Controls.Add(panel2, 1, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 754);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5.55555534F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 88.8888855F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5.55555534F));
            tableLayoutPanel4.Size = new Size(1067, 54);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft Sans Serif", 14.25F);
            label1.Location = new Point(104, 3);
            label1.Name = "label1";
            label1.Size = new Size(314, 48);
            label1.TabIndex = 3;
            label1.Text = "Labour Changes On Price Change";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Location = new Point(35, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 42);
            panel2.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView2);
            panel1.Controls.Add(dataGridView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 84);
            panel1.Name = "panel1";
            panel1.Size = new Size(1067, 565);
            panel1.TabIndex = 5;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(0, 0);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(1067, 565);
            dataGridView2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ActiveCaptionText;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1067, 565);
            dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(1067, 75);
            tableLayoutPanel3.TabIndex = 6;
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
            tableLayoutPanel1.Size = new Size(1090, 870);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // addItemNew
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 870);
            Controls.Add(tableLayoutPanel1);
            Name = "addItemNew";
            Text = "addItemNew";
            Load += addItemNew_Load;
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion
        private Label label8;
        private Button BUTTON_FETCH_DATA;
        private Button BUTTON_PRINT_ON_OFF;
        private TableLayoutPanel tableLayoutPanel2;
        private Label LABEL_MESSAGE;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private Panel panel2;
    }
}