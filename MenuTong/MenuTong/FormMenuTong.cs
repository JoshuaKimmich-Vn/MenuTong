using System;
using System.Drawing;
using System.Windows.Forms;
using carlender; // Calendar
using health; // Healthcare
using QuanLyChiTieu_GUI; // Finance

using CalendarMainForm = carlender.MainForm;
using FinanceMainForm = QuanLyChiTieu_GUI.MainForm;

namespace MenuTong
{
    public partial class FormMenuTong : Form
    {
        private Form? currentChildForm = null;

        private CalendarMainForm? calendarForm = null;

        private Form1? healthcareForm = null;

        private FinanceMainForm? financeForm = null;

        private string userName = "";
        private string userEmail = "";

        public FormMenuTong(string username = "", string email = "")
        {
            InitializeComponent();

            this.userName = username;
            this.userEmail = email;

            UpdateUserInfo();
            SetupMenuStyle();
            LoadCalendar();
        }

        private void UpdateUserInfo()
        {
            if (!string.IsNullOrEmpty(userName))
            {
                labelUserName.Text = $"👤 {userName}";
            }
            labelEmail.Visible = false;
        }

        // ============ THIẾT LẬP GIAO DIỆN MENU ============
        private void SetupMenuStyle()
        {
            Button[] menuButtons = { buttonCalendar, buttonSystem, buttonFinance, buttonHealthcare };

            foreach (Button btn in menuButtons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 120, 215);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 80, 150);
                btn.BackColor = Color.FromArgb(45, 45, 48);
                btn.ForeColor = Color.White;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(20, 0, 0, 0);
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                btn.Cursor = Cursors.Hand;
            }

            buttonLogOut.FlatStyle = FlatStyle.Flat;
            buttonLogOut.FlatAppearance.BorderSize = 0;
            buttonLogOut.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 50, 50);
            buttonLogOut.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 30, 30);
            buttonLogOut.BackColor = Color.FromArgb(180, 40, 40);
            buttonLogOut.ForeColor = Color.White;
            buttonLogOut.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonLogOut.Cursor = Cursors.Hand;

            buttonCalendar.Text = "  📅  Lịch trình";
            buttonSystem.Text = "  ⚙️  Hệ thống";
            buttonFinance.Text = "  💰  Tài chính";
            buttonHealthcare.Text = "  🏥  Sức khỏe";
            buttonLogOut.Text = "  🚪  Đăng xuất";
        }

        // ============ HÀM MỞ FORM CON ============
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.BackColor = Color.White;

            panelHienThi.Controls.Clear();
            panelHienThi.Controls.Add(childForm);
            panelHienThi.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();

            currentChildForm = childForm;
        }

        // ============ CALENDAR ============
        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private void LoadCalendar()
        {
            try
            {
                if (calendarForm == null || calendarForm.IsDisposed)
                {
                    calendarForm = new CalendarMainForm();
                }
                OpenChildForm(calendarForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Lịch trình:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============ HEALTHCARE ============
        private void buttonHealthcare_Click(object sender, EventArgs e)
        {
            LoadHealthcare();
        }

        private void LoadHealthcare()
        {
            try
            {
                if (healthcareForm == null || healthcareForm.IsDisposed)
                {
                    healthcareForm = new Form1();
                }
                OpenChildForm(healthcareForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Sức khỏe:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============ FINANCE ============
        private void buttonFinance_Click(object sender, EventArgs e)
        {
            LoadFinance();
        }

        private void LoadFinance()
        {
            try
            {
                if (financeForm == null || financeForm.IsDisposed)
                {
                    financeForm = new FinanceMainForm();
                }
                OpenChildForm(financeForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Tài chính:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============ SYSTEM ============
        private void buttonSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Hệ thống đang được phát triển!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ============ LOGOUT ============
        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                }
                Application.Restart();
            }
        }

        // ============ ĐÓNG FORM ============
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn thoát ứng dụng?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            base.OnFormClosing(e);
        }
    }
}