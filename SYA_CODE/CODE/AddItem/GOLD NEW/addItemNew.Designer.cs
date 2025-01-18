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
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            BUTTON_WEIGHT_OR_PRICE = new Button();
            LABEL_MESSAGE = new Label();
            panel1 = new Panel();
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
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
            label8.ForeColor = Color.FromArgb(158, 43, 37);
            label8.Location = new Point(7, 10);
            label8.Name = "label8";
            label8.Size = new Size(902, 34);
            label8.TabIndex = 5;
            label8.Text = "ADD ITEM";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            label8.Click += label8_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel2.Controls.Add(LABEL_MESSAGE, 0, 2);
            tableLayoutPanel2.Controls.Add(panel1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(7, 47);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 7.5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 84.5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(902, 623);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Controls.Add(BUTTON_WEIGHT_OR_PRICE, 1, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel4.Size = new Size(896, 40);
            tableLayoutPanel4.TabIndex = 6;
            tableLayoutPanel4.Paint += tableLayoutPanel4_Paint;
            // 
            // BUTTON_WEIGHT_OR_PRICE
            // 
            BUTTON_WEIGHT_OR_PRICE.BackColor = Color.FromArgb(169, 66, 60);
            BUTTON_WEIGHT_OR_PRICE.Dock = DockStyle.Fill;
            BUTTON_WEIGHT_OR_PRICE.FlatStyle = FlatStyle.Flat;
            BUTTON_WEIGHT_OR_PRICE.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BUTTON_WEIGHT_OR_PRICE.ForeColor = Color.FromArgb(255, 248, 240);
            BUTTON_WEIGHT_OR_PRICE.Location = new Point(719, 5);
            BUTTON_WEIGHT_OR_PRICE.Name = "BUTTON_WEIGHT_OR_PRICE";
            BUTTON_WEIGHT_OR_PRICE.Size = new Size(155, 30);
            BUTTON_WEIGHT_OR_PRICE.TabIndex = 0;
            BUTTON_WEIGHT_OR_PRICE.Text = "WEIGHT TAG";
            BUTTON_WEIGHT_OR_PRICE.UseVisualStyleBackColor = false;
            BUTTON_WEIGHT_OR_PRICE.Click += BUTTON_WEIGHT_OR_PRICE_Click;
            // 
            // LABEL_MESSAGE
            // 
            LABEL_MESSAGE.Dock = DockStyle.Fill;
            LABEL_MESSAGE.Font = new Font("Microsoft Sans Serif", 14.25F);
            LABEL_MESSAGE.ForeColor = Color.FromArgb(158, 43, 37);
            LABEL_MESSAGE.Location = new Point(3, 572);
            LABEL_MESSAGE.Name = "LABEL_MESSAGE";
            LABEL_MESSAGE.Size = new Size(896, 51);
            LABEL_MESSAGE.TabIndex = 4;
            LABEL_MESSAGE.Text = "No Updates !";
            LABEL_MESSAGE.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView2);
            panel1.Controls.Add(dataGridView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(896, 520);
            panel1.TabIndex = 5;
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = Color.FromArgb(233, 202, 195);
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.GridColor = Color.FromArgb(158, 43, 37);
            dataGridView2.Location = new Point(0, 0);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(896, 520);
            dataGridView2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ActiveCaptionText;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(896, 520);
            dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 99F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.5F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 2);
            tableLayoutPanel1.Controls.Add(label8, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 1.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 92.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            tableLayoutPanel1.Size = new Size(918, 681);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // addItemNew
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 225, 217);
            ClientSize = new Size(918, 681);
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
        private TableLayoutPanel tableLayoutPanel2;
        private Label LABEL_MESSAGE;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel4;
        private Button BUTTON_WEIGHT_OR_PRICE;
    }
}