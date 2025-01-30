namespace SYA
{
    partial class RepairCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepairCard));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            btnDelete = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            btnTypeCompleted = new Button();
            btnTypeUnableToComplete = new Button();
            NAME = new Label();
            TYPE_DATE = new Label();
            STATUS = new Label();
            PICTURE = new PictureBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            btnTypeNew = new Button();
            btnTypeInProgress = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PICTURE).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 94F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 94F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.Size = new Size(400, 400);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel5, 0, 6);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 5);
            tableLayoutPanel2.Controls.Add(NAME, 0, 0);
            tableLayoutPanel2.Controls.Add(TYPE_DATE, 0, 3);
            tableLayoutPanel2.Controls.Add(STATUS, 0, 2);
            tableLayoutPanel2.Controls.Add(PICTURE, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 4);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(15, 15);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.849558F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 46.90266F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.849558F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.849558F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.849558F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.849558F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.849558F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(370, 370);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel5.Controls.Add(btnDelete, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 336);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(364, 31);
            tableLayoutPanel5.TabIndex = 11;
            // 
            // btnDelete
            // 
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Arial Rounded MT Bold", 8.5F);
            btnDelete.Location = new Point(94, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(176, 25);
            btnDelete.TabIndex = 0;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(btnTypeCompleted, 0, 0);
            tableLayoutPanel4.Controls.Add(btnTypeUnableToComplete, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 304);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(364, 26);
            tableLayoutPanel4.TabIndex = 10;
            // 
            // btnTypeCompleted
            // 
            btnTypeCompleted.Dock = DockStyle.Fill;
            btnTypeCompleted.FlatStyle = FlatStyle.Popup;
            btnTypeCompleted.Font = new Font("Arial Rounded MT Bold", 8.5F);
            btnTypeCompleted.Location = new Point(3, 3);
            btnTypeCompleted.Name = "btnTypeCompleted";
            btnTypeCompleted.Size = new Size(176, 20);
            btnTypeCompleted.TabIndex = 0;
            btnTypeCompleted.Text = "COMPLETED";
            btnTypeCompleted.UseVisualStyleBackColor = true;
            // 
            // btnTypeUnableToComplete
            // 
            btnTypeUnableToComplete.Dock = DockStyle.Fill;
            btnTypeUnableToComplete.FlatStyle = FlatStyle.Popup;
            btnTypeUnableToComplete.Font = new Font("Arial Rounded MT Bold", 8.5F);
            btnTypeUnableToComplete.Location = new Point(185, 3);
            btnTypeUnableToComplete.Name = "btnTypeUnableToComplete";
            btnTypeUnableToComplete.Size = new Size(176, 20);
            btnTypeUnableToComplete.TabIndex = 1;
            btnTypeUnableToComplete.Text = "UNABLE TO COMPLET";
            btnTypeUnableToComplete.UseVisualStyleBackColor = true;
            // 
            // NAME
            // 
            NAME.AutoSize = true;
            NAME.Dock = DockStyle.Fill;
            NAME.Font = new Font("Arial Rounded MT Bold", 12F);
            NAME.Location = new Point(3, 0);
            NAME.Name = "NAME";
            NAME.Size = new Size(364, 32);
            NAME.TabIndex = 8;
            NAME.Text = "Name";
            NAME.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TYPE_DATE
            // 
            TYPE_DATE.AutoSize = true;
            TYPE_DATE.Dock = DockStyle.Fill;
            TYPE_DATE.Font = new Font("Arial Rounded MT Bold", 12F);
            TYPE_DATE.Location = new Point(3, 237);
            TYPE_DATE.Name = "TYPE_DATE";
            TYPE_DATE.Size = new Size(364, 32);
            TYPE_DATE.TabIndex = 4;
            TYPE_DATE.Text = "Name";
            TYPE_DATE.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // STATUS
            // 
            STATUS.AutoSize = true;
            STATUS.Dock = DockStyle.Fill;
            STATUS.Font = new Font("Arial Rounded MT Bold", 12F);
            STATUS.Location = new Point(3, 205);
            STATUS.Name = "STATUS";
            STATUS.Size = new Size(364, 32);
            STATUS.TabIndex = 3;
            STATUS.Text = "Name";
            STATUS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PICTURE
            // 
            PICTURE.Dock = DockStyle.Fill;
            PICTURE.Image = (Image)resources.GetObject("PICTURE.Image");
            PICTURE.Location = new Point(3, 35);
            PICTURE.Name = "PICTURE";
            PICTURE.Size = new Size(364, 167);
            PICTURE.SizeMode = PictureBoxSizeMode.Zoom;
            PICTURE.TabIndex = 0;
            PICTURE.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(btnTypeNew, 0, 0);
            tableLayoutPanel3.Controls.Add(btnTypeInProgress, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 272);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(364, 26);
            tableLayoutPanel3.TabIndex = 9;
            // 
            // btnTypeNew
            // 
            btnTypeNew.Dock = DockStyle.Fill;
            btnTypeNew.FlatStyle = FlatStyle.Popup;
            btnTypeNew.Font = new Font("Arial Rounded MT Bold", 8.5F);
            btnTypeNew.Location = new Point(3, 3);
            btnTypeNew.Name = "btnTypeNew";
            btnTypeNew.Size = new Size(176, 20);
            btnTypeNew.TabIndex = 0;
            btnTypeNew.Text = "NEW";
            btnTypeNew.UseVisualStyleBackColor = true;
            // 
            // btnTypeInProgress
            // 
            btnTypeInProgress.Dock = DockStyle.Fill;
            btnTypeInProgress.FlatStyle = FlatStyle.Popup;
            btnTypeInProgress.Font = new Font("Arial Rounded MT Bold", 8.5F);
            btnTypeInProgress.Location = new Point(185, 3);
            btnTypeInProgress.Name = "btnTypeInProgress";
            btnTypeInProgress.Size = new Size(176, 20);
            btnTypeInProgress.TabIndex = 1;
            btnTypeInProgress.Text = "IN PROGRESS";
            btnTypeInProgress.UseVisualStyleBackColor = true;
            // 
            // RepairCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "RepairCard";
            Size = new Size(400, 400);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PICTURE).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox PICTURE;
        private Label NAME;
        private Label TYPE_DATE;
        private Label STATUS;
        private TableLayoutPanel tableLayoutPanel5;
        private Button btnDelete;
        private TableLayoutPanel tableLayoutPanel4;
        private Button btnTypeCompleted;
        private Button btnTypeUnableToComplete;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnTypeNew;
        private Button btnTypeInProgress;
    }
}
