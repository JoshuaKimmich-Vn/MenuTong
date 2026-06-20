namespace MenuTong
{
    partial class FormMenuTong
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenuTong));
            panelMenu = new Panel();
            buttonLogOut = new Button();
            labelEmail = new Label();
            labelUserName = new Label();
            buttonHealthcare = new Button();
            buttonFinance = new Button();
            buttonSystem = new Button();
            buttonCalendar = new Button();
            panelTitle = new Panel();
            labelAppTitle = new Label();
            pictureBoxLogo = new PictureBox();
            panelHienThi = new Panel();
            panelMenu.SuspendLayout();
            panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(30, 30, 30);
            panelMenu.Controls.Add(buttonLogOut);
    
            panelMenu.Controls.Add(labelUserName);
            panelMenu.Controls.Add(buttonHealthcare);
            panelMenu.Controls.Add(buttonFinance);
            panelMenu.Controls.Add(buttonSystem);
            panelMenu.Controls.Add(buttonCalendar);
            panelMenu.Controls.Add(panelTitle);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(220, 600);
            panelMenu.TabIndex = 0;
            // 
            // panelTitle
            // 
            panelTitle.BackColor = Color.FromArgb(0, 120, 215);
            panelTitle.Controls.Add(labelAppTitle);
            panelTitle.Controls.Add(pictureBoxLogo);
            panelTitle.Dock = DockStyle.Top;
            panelTitle.Location = new Point(0, 0);
            panelTitle.Name = "panelTitle";
            panelTitle.Size = new Size(220, 80);
            panelTitle.TabIndex = 0;
            // 
            // labelAppTitle
            // 
            labelAppTitle.AutoSize = true;
            labelAppTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAppTitle.ForeColor = Color.White;
            labelAppTitle.Location = new Point(70, 28);
            labelAppTitle.Name = "labelAppTitle";
            labelAppTitle.Size = new Size(135, 37);
            labelAppTitle.TabIndex = 1;
            labelAppTitle.Text = "MenuTong";
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Image = (Image)resources.GetObject("pictureBoxLogo.Image");
            pictureBoxLogo.Location = new Point(15, 12);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(50, 50);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // buttonCalendar
            // 
            buttonCalendar.BackColor = Color.FromArgb(45, 45, 48);
            buttonCalendar.FlatAppearance.BorderSize = 0;
            buttonCalendar.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 120, 215);
            buttonCalendar.FlatStyle = FlatStyle.Flat;
            buttonCalendar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCalendar.ForeColor = Color.White;
            buttonCalendar.Image = (Image)resources.GetObject("buttonCalendar.Image");
            buttonCalendar.ImageAlign = ContentAlignment.MiddleLeft;
            buttonCalendar.Location = new Point(0, 95);
            buttonCalendar.Name = "buttonCalendar";
            buttonCalendar.Padding = new Padding(15, 0, 0, 0);
            buttonCalendar.Size = new Size(220, 50);
            buttonCalendar.TabIndex = 1;
            buttonCalendar.Text = "  📅  Lịch trình";
            buttonCalendar.TextAlign = ContentAlignment.MiddleLeft;
            buttonCalendar.UseVisualStyleBackColor = false;
            buttonCalendar.Click += buttonCalendar_Click;
            // 
            // buttonSystem
            // 
            buttonSystem.BackColor = Color.FromArgb(45, 45, 48);
            buttonSystem.FlatAppearance.BorderSize = 0;
            buttonSystem.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 120, 215);
            buttonSystem.FlatStyle = FlatStyle.Flat;
            buttonSystem.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSystem.ForeColor = Color.White;
            buttonSystem.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSystem.Location = new Point(0, 145);
            buttonSystem.Name = "buttonSystem";
            buttonSystem.Padding = new Padding(15, 0, 0, 0);
            buttonSystem.Size = new Size(220, 50);
            buttonSystem.TabIndex = 4;
            buttonSystem.Text = "  ⚙️  Hệ thống";
            buttonSystem.TextAlign = ContentAlignment.MiddleLeft;
            buttonSystem.UseVisualStyleBackColor = false;
            buttonSystem.Click += buttonSystem_Click;
            // 
            // buttonFinance
            // 
            buttonFinance.BackColor = Color.FromArgb(45, 45, 48);
            buttonFinance.FlatAppearance.BorderSize = 0;
            buttonFinance.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 120, 215);
            buttonFinance.FlatStyle = FlatStyle.Flat;
            buttonFinance.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonFinance.ForeColor = Color.White;
            buttonFinance.ImageAlign = ContentAlignment.MiddleLeft;
            buttonFinance.Location = new Point(0, 195);
            buttonFinance.Name = "buttonFinance";
            buttonFinance.Padding = new Padding(15, 0, 0, 0);
            buttonFinance.Size = new Size(220, 50);
            buttonFinance.TabIndex = 3;
            buttonFinance.Text = "  💰  Tài chính";
            buttonFinance.TextAlign = ContentAlignment.MiddleLeft;
            buttonFinance.UseVisualStyleBackColor = false;
            buttonFinance.Click += buttonFinance_Click;
            // 
            // buttonHealthcare
            // 
            buttonHealthcare.BackColor = Color.FromArgb(45, 45, 48);
            buttonHealthcare.FlatAppearance.BorderSize = 0;
            buttonHealthcare.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 120, 215);
            buttonHealthcare.FlatStyle = FlatStyle.Flat;
            buttonHealthcare.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonHealthcare.ForeColor = Color.White;
            buttonHealthcare.ImageAlign = ContentAlignment.MiddleLeft;
            buttonHealthcare.Location = new Point(0, 245);
            buttonHealthcare.Name = "buttonHealthcare";
            buttonHealthcare.Padding = new Padding(15, 0, 0, 0);
            buttonHealthcare.Size = new Size(220, 50);
            buttonHealthcare.TabIndex = 2;
            buttonHealthcare.Text = "  🏥  Sức khỏe";
            buttonHealthcare.TextAlign = ContentAlignment.MiddleLeft;
            buttonHealthcare.UseVisualStyleBackColor = false;
            buttonHealthcare.Click += buttonHealthcare_Click;
            // 
            // labelUserName
            // 
            labelUserName.AutoSize = true;
            labelUserName.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelUserName.ForeColor = Color.White;
            labelUserName.Location = new Point(15, 470);
            labelUserName.Name = "labelUserName";
            labelUserName.Size = new Size(104, 23);
            labelUserName.TabIndex = 5;
            labelUserName.Text = "👤 Tên: ĐịnhOX";
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelEmail.ForeColor = Color.White;
            labelEmail.Location = new Point(15, 500);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(167, 23);
            labelEmail.TabIndex = 6;
            labelEmail.Text = "✉️ Email: DinhCC@gmail.com";
            // 
            // buttonLogOut
            // 
            buttonLogOut.BackColor = Color.FromArgb(180, 40, 40);
            buttonLogOut.FlatAppearance.BorderSize = 0;
            buttonLogOut.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 50, 50);
            buttonLogOut.FlatStyle = FlatStyle.Flat;
            buttonLogOut.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonLogOut.ForeColor = Color.White;
            buttonLogOut.ImageAlign = ContentAlignment.MiddleLeft;
            buttonLogOut.Location = new Point(0, 540);
            buttonLogOut.Name = "buttonLogOut";
            buttonLogOut.Padding = new Padding(15, 0, 0, 0);
            buttonLogOut.Size = new Size(220, 50);
            buttonLogOut.TabIndex = 7;
            buttonLogOut.Text = "  🚪  Đăng xuất";
            buttonLogOut.TextAlign = ContentAlignment.MiddleLeft;
            buttonLogOut.UseVisualStyleBackColor = false;
            buttonLogOut.Click += buttonLogOut_Click;
            // 
            // panelHienThi
            // 
            panelHienThi.BackColor = Color.White;
            panelHienThi.Dock = DockStyle.Fill;
            panelHienThi.Location = new Point(220, 0);
            panelHienThi.Name = "panelHienThi";
            panelHienThi.Size = new Size(780, 600);
            panelHienThi.TabIndex = 1;
            // 
            // FormMenuTong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(panelHienThi);
            Controls.Add(panelMenu);
            MinimumSize = new Size(800, 500);
            Name = "FormMenuTong";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuTong - Quản lý cá nhân";
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            panelTitle.ResumeLayout(false);
            panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // Panel và các control
        private Panel panelMenu;
        private Panel panelTitle;
        private Label labelAppTitle;
        private PictureBox pictureBoxLogo;
        private Button buttonCalendar;
        private Button buttonSystem;
        private Button buttonFinance;
        private Button buttonHealthcare;
        private Label labelUserName;
        private Label labelEmail;
        private Button buttonLogOut;
        private Panel panelHienThi;
    }
}