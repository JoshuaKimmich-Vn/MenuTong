namespace carlender
{
    partial class EventForm
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
            label1 = new Label();
            txtTitle = new TextBox();
            label2 = new Label();
            label3 = new Label();
            dtpStartTime = new DateTimePicker();
            dtpEndTime = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            txtDescription = new TextBox();
            cboCategory = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 30);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 0;
            label1.Text = "Tiêu đề:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(140, 30);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(250, 27);
            txtTitle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(178, 76);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 2;
            label2.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 120);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 3;
            label3.Text = "Kết thúc:";
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(140, 75);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(250, 27);
            dtpStartTime.TabIndex = 4;
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(140, 120);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(250, 27);
            dtpEndTime.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 75);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 6;
            label4.Text = "Bắt đầu:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(34, 168);
            label5.Name = "label5";
            label5.Size = new Size(61, 20);
            label5.TabIndex = 7;
            label5.Text = "Ghi chú:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(140, 165);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(250, 80);
            txtDescription.TabIndex = 8;
            // 
            // cboCategory
            // 
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(140, 265);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(250, 28);
            cboCategory.TabIndex = 9;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(140, 320);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 10;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(290, 320);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // EventForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 383);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cboCategory);
            Controls.Add(txtDescription);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dtpEndTime);
            Controls.Add(dtpStartTime);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtTitle);
            Controls.Add(label1);
            Name = "EventForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm sự kiện";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtTitle;
        private Label label2;
        private Label label3;
        private DateTimePicker dtpStartTime;
        private DateTimePicker dtpEndTime;
        private Label label4;
        private Label label5;
        private TextBox txtDescription;
        private ComboBox cboCategory;
        private Button btnSave;
        private Button btnCancel;
    }
}