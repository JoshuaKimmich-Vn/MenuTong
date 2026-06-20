namespace carlender
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelTopBar = new Panel();
            lblAppName = new Label();
            btnPreviousMonth = new Button();
            btnNextMonth = new Button();
            lblMonth = new Label();
            panelLeftSidebar = new Panel();
            btnAddEvent = new Button();
            lblMyCalendarTitle = new Label();
            lblCalendarStudy = new Label();
            lblCalendarWork = new Label();
            lblCalendarPersonal = new Label();
            lblCalendarOther = new Label();
            panelMainContent = new Panel();
            panelCalendarCard = new Panel();
            tabelCalendar = new TableLayoutPanel();
            panelRightSidebar = new Panel();
            lblSelectedDate = new Label();
            lstEvents = new ListBox();
            btnEditEvent = new Button();
            btnDeleteEvent = new Button();
            btnDeleteAllEvents = new Button();
            panelTopBar.SuspendLayout();
            panelLeftSidebar.SuspendLayout();
            panelMainContent.SuspendLayout();
            panelCalendarCard.SuspendLayout();
            panelRightSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // panelTopBar
            // 
            panelTopBar.BackColor = Color.White;
            panelTopBar.Controls.Add(lblAppName);
            panelTopBar.Controls.Add(btnPreviousMonth);
            panelTopBar.Controls.Add(btnNextMonth);
            panelTopBar.Controls.Add(lblMonth);
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Location = new Point(0, 0);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Size = new Size(1280, 76);
            panelTopBar.TabIndex = 0;
            // 
            // lblAppName
            // 
            lblAppName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblAppName.ForeColor = Color.FromArgb(60, 64, 67);
            lblAppName.Location = new Point(32, 18);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(190, 40);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "📅 Lịch";
            lblAppName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnPreviousMonth
            // 
            btnPreviousMonth.BackColor = Color.White;
            btnPreviousMonth.Cursor = Cursors.Hand;
            btnPreviousMonth.FlatAppearance.BorderSize = 0;
            btnPreviousMonth.FlatStyle = FlatStyle.Flat;
            btnPreviousMonth.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnPreviousMonth.ForeColor = Color.FromArgb(95, 99, 104);
            btnPreviousMonth.Location = new Point(380, 16);
            btnPreviousMonth.Name = "btnPreviousMonth";
            btnPreviousMonth.Size = new Size(42, 42);
            btnPreviousMonth.TabIndex = 3;
            btnPreviousMonth.Text = "‹";
            btnPreviousMonth.UseVisualStyleBackColor = false;
            btnPreviousMonth.Click += btnPreviousMonth_Click;
            // 
            // btnNextMonth
            // 
            btnNextMonth.BackColor = Color.White;
            btnNextMonth.Cursor = Cursors.Hand;
            btnNextMonth.FlatAppearance.BorderSize = 0;
            btnNextMonth.FlatStyle = FlatStyle.Flat;
            btnNextMonth.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnNextMonth.ForeColor = Color.FromArgb(95, 99, 104);
            btnNextMonth.Location = new Point(428, 16);
            btnNextMonth.Name = "btnNextMonth";
            btnNextMonth.Size = new Size(42, 42);
            btnNextMonth.TabIndex = 4;
            btnNextMonth.Text = "›";
            btnNextMonth.UseVisualStyleBackColor = false;
            btnNextMonth.Click += btnNextMonth_Click;
            // 
            // lblMonth
            // 
            lblMonth.Font = new Font("Segoe UI", 17F);
            lblMonth.ForeColor = Color.FromArgb(32, 33, 36);
            lblMonth.Location = new Point(490, 15);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(520, 45);
            lblMonth.TabIndex = 5;
            lblMonth.Text = "Tháng 6, 2026";
            lblMonth.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelLeftSidebar
            // 
            panelLeftSidebar.BackColor = Color.FromArgb(248, 250, 253);
            panelLeftSidebar.Controls.Add(btnAddEvent);
            panelLeftSidebar.Controls.Add(lblMyCalendarTitle);
            panelLeftSidebar.Controls.Add(lblCalendarStudy);
            panelLeftSidebar.Controls.Add(lblCalendarWork);
            panelLeftSidebar.Controls.Add(lblCalendarPersonal);
            panelLeftSidebar.Controls.Add(lblCalendarOther);
            panelLeftSidebar.Dock = DockStyle.Left;
            panelLeftSidebar.Location = new Point(0, 76);
            panelLeftSidebar.Name = "panelLeftSidebar";
            panelLeftSidebar.Size = new Size(235, 684);
            panelLeftSidebar.TabIndex = 1;
            // 
            // btnAddEvent
            // 
            btnAddEvent.BackColor = Color.White;
            btnAddEvent.Cursor = Cursors.Hand;
            btnAddEvent.FlatAppearance.BorderColor = Color.FromArgb(218, 220, 224);
            btnAddEvent.FlatStyle = FlatStyle.Flat;
            btnAddEvent.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAddEvent.ForeColor = Color.FromArgb(32, 33, 36);
            btnAddEvent.Location = new Point(24, 22);
            btnAddEvent.Name = "btnAddEvent";
            btnAddEvent.Size = new Size(145, 52);
            btnAddEvent.TabIndex = 9;
            btnAddEvent.Text = "+  Tạo";
            btnAddEvent.UseVisualStyleBackColor = false;
            btnAddEvent.Click += btnAddEvent_Click;
            // 
            // lblMyCalendarTitle
            // 
            lblMyCalendarTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMyCalendarTitle.ForeColor = Color.FromArgb(32, 33, 36);
            lblMyCalendarTitle.Location = new Point(24, 330);
            lblMyCalendarTitle.Name = "lblMyCalendarTitle";
            lblMyCalendarTitle.Size = new Size(180, 30);
            lblMyCalendarTitle.TabIndex = 12;
            lblMyCalendarTitle.Text = "Lịch của tôi";
            lblMyCalendarTitle.Click += lblMyCalendarTitle_Click;
            // 
            // lblCalendarStudy
            // 
            lblCalendarStudy.Font = new Font("Segoe UI", 10F);
            lblCalendarStudy.ForeColor = Color.FromArgb(0, 122, 255);
            lblCalendarStudy.Location = new Point(32, 370);
            lblCalendarStudy.Name = "lblCalendarStudy";
            lblCalendarStudy.Size = new Size(180, 30);
            lblCalendarStudy.TabIndex = 13;
            lblCalendarStudy.Text = "■  Học tập";
            // 
            // lblCalendarWork
            // 
            lblCalendarWork.Font = new Font("Segoe UI", 10F);
            lblCalendarWork.ForeColor = Color.FromArgb(255, 149, 0);
            lblCalendarWork.Location = new Point(32, 405);
            lblCalendarWork.Name = "lblCalendarWork";
            lblCalendarWork.Size = new Size(180, 30);
            lblCalendarWork.TabIndex = 14;
            lblCalendarWork.Text = "■  Công việc";
            // 
            // lblCalendarPersonal
            // 
            lblCalendarPersonal.Font = new Font("Segoe UI", 10F);
            lblCalendarPersonal.ForeColor = Color.FromArgb(175, 82, 222);
            lblCalendarPersonal.Location = new Point(32, 440);
            lblCalendarPersonal.Name = "lblCalendarPersonal";
            lblCalendarPersonal.Size = new Size(180, 30);
            lblCalendarPersonal.TabIndex = 15;
            lblCalendarPersonal.Text = "■  Cá nhân";
            // 
            // lblCalendarOther
            // 
            lblCalendarOther.Font = new Font("Segoe UI", 10F);
            lblCalendarOther.ForeColor = Color.FromArgb(142, 142, 147);
            lblCalendarOther.Location = new Point(32, 475);
            lblCalendarOther.Name = "lblCalendarOther";
            lblCalendarOther.Size = new Size(180, 30);
            lblCalendarOther.TabIndex = 16;
            lblCalendarOther.Text = "■  Khác";
            // 
            // panelMainContent
            // 
            panelMainContent.BackColor = Color.FromArgb(248, 250, 253);
            panelMainContent.Controls.Add(panelCalendarCard);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(235, 76);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Padding = new Padding(18, 24, 18, 24);
            panelMainContent.Size = new Size(720, 684);
            panelMainContent.TabIndex = 2;
            // 
            // panelCalendarCard
            // 
            panelCalendarCard.BackColor = Color.White;
            panelCalendarCard.Controls.Add(tabelCalendar);
            panelCalendarCard.Dock = DockStyle.Fill;
            panelCalendarCard.Location = new Point(18, 24);
            panelCalendarCard.Name = "panelCalendarCard";
            panelCalendarCard.Padding = new Padding(14);
            panelCalendarCard.Size = new Size(684, 636);
            panelCalendarCard.TabIndex = 3;
            // 
            // tabelCalendar
            // 
            tabelCalendar.BackColor = Color.White;
            tabelCalendar.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tabelCalendar.ColumnCount = 7;
            tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.Dock = DockStyle.Fill;
            tabelCalendar.Location = new Point(14, 14);
            tabelCalendar.Name = "tabelCalendar";
            tabelCalendar.RowCount = 7;
            tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            tabelCalendar.Size = new Size(656, 608);
            tabelCalendar.TabIndex = 17;
            // 
            // panelRightSidebar
            // 
            panelRightSidebar.BackColor = Color.White;
            panelRightSidebar.Controls.Add(lblSelectedDate);
            panelRightSidebar.Controls.Add(lstEvents);
            panelRightSidebar.Controls.Add(btnEditEvent);
            panelRightSidebar.Controls.Add(btnDeleteEvent);
            panelRightSidebar.Dock = DockStyle.Right;
            panelRightSidebar.Location = new Point(955, 76);
            panelRightSidebar.Name = "panelRightSidebar";
            panelRightSidebar.Padding = new Padding(20);
            panelRightSidebar.Size = new Size(325, 684);
            panelRightSidebar.TabIndex = 4;
            // 
            // lblSelectedDate
            // 
            lblSelectedDate.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblSelectedDate.ForeColor = Color.FromArgb(32, 33, 36);
            lblSelectedDate.Location = new Point(20, 25);
            lblSelectedDate.Name = "lblSelectedDate";
            lblSelectedDate.Size = new Size(285, 45);
            lblSelectedDate.TabIndex = 18;
            lblSelectedDate.Text = "Sự kiện ngày";
            lblSelectedDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstEvents
            // 
            lstEvents.BackColor = Color.White;
            lstEvents.BorderStyle = BorderStyle.None;
            lstEvents.Font = new Font("Segoe UI", 10.5F);
            lstEvents.ForeColor = Color.FromArgb(32, 33, 36);
            lstEvents.FormattingEnabled = true;
            lstEvents.HorizontalScrollbar = true;
            lstEvents.IntegralHeight = false;
            lstEvents.Location = new Point(20, 80);
            lstEvents.Name = "lstEvents";
            lstEvents.Size = new Size(285, 420);
            lstEvents.TabIndex = 19;
            lstEvents.SelectedIndexChanged += lstEvents_SelectedIndexChanged;
            // 
            // btnEditEvent
            // 
            btnEditEvent.BackColor = Color.FromArgb(251, 188, 5);
            btnEditEvent.Cursor = Cursors.Hand;
            btnEditEvent.FlatAppearance.BorderSize = 0;
            btnEditEvent.FlatStyle = FlatStyle.Flat;
            btnEditEvent.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnEditEvent.ForeColor = Color.Black;
            btnEditEvent.Location = new Point(20, 525);
            btnEditEvent.Name = "btnEditEvent";
            btnEditEvent.Size = new Size(285, 48);
            btnEditEvent.TabIndex = 20;
            btnEditEvent.Text = "Sửa sự kiện";
            btnEditEvent.UseVisualStyleBackColor = false;
            btnEditEvent.Click += btnEditEvent_Click;
            // 
            // btnDeleteEvent
            // 
            btnDeleteEvent.BackColor = Color.FromArgb(234, 67, 53);
            btnDeleteEvent.Cursor = Cursors.Hand;
            btnDeleteEvent.FlatAppearance.BorderSize = 0;
            btnDeleteEvent.FlatStyle = FlatStyle.Flat;
            btnDeleteEvent.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnDeleteEvent.ForeColor = Color.White;
            btnDeleteEvent.Location = new Point(20, 585);
            btnDeleteEvent.Name = "btnDeleteEvent";
            btnDeleteEvent.Size = new Size(285, 48);
            btnDeleteEvent.TabIndex = 21;
            btnDeleteEvent.Text = "Xóa sự kiện";
            btnDeleteEvent.UseVisualStyleBackColor = false;
            btnDeleteEvent.Click += btnDeleteEvent_Click;
            // 
            // btnDeleteAllEvents
            // 
            btnDeleteAllEvents.BackColor = Color.FromArgb(95, 99, 104);
            btnDeleteAllEvents.Cursor = Cursors.Hand;
            btnDeleteAllEvents.FlatAppearance.BorderSize = 0;
            btnDeleteAllEvents.FlatStyle = FlatStyle.Flat;
            btnDeleteAllEvents.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnDeleteAllEvents.ForeColor = Color.White;
            btnDeleteAllEvents.Location = new Point(20, 640);
            btnDeleteAllEvents.Name = "btnDeleteAllEvents";
            btnDeleteAllEvents.Size = new Size(285, 44);
            btnDeleteAllEvents.TabIndex = 22;
            btnDeleteAllEvents.Text = "Xóa tất cả";
            btnDeleteAllEvents.UseVisualStyleBackColor = false;
            btnDeleteAllEvents.Click += btnDeleteAllEvents_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 253);
            ClientSize = new Size(1280, 760);
            Controls.Add(panelMainContent);
            Controls.Add(panelRightSidebar);
            Controls.Add(panelLeftSidebar);
            Controls.Add(panelTopBar);
            MinimumSize = new Size(1200, 720);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calendar Schedule App";
            Load += MainForm_Load;
            panelTopBar.ResumeLayout(false);
            panelLeftSidebar.ResumeLayout(false);
            panelMainContent.ResumeLayout(false);
            panelCalendarCard.ResumeLayout(false);
            panelRightSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTopBar;
        private Label lblAppName;

        private Panel panelLeftSidebar;
        private Label lblMyCalendarTitle;
        private Label lblCalendarStudy;
        private Label lblCalendarWork;
        private Label lblCalendarPersonal;
        private Label lblCalendarOther;

        private Panel panelMainContent;
        private Panel panelCalendarCard;
        private Panel panelRightSidebar;

        private Label lblMonth;
        private Button btnPreviousMonth;
        private Button btnNextMonth;
        private TableLayoutPanel tabelCalendar;
        private Label lblSelectedDate;
        private ListBox lstEvents;
        private Button btnAddEvent;
        private Button btnDeleteEvent;
        private Button btnEditEvent;
        private Button btnDeleteAllEvents;
    }
}