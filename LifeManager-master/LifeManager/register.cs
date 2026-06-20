using System;
using System.Drawing;
using System.Windows.Forms;
using Firebase.Auth; // Thư viện Firebase

namespace LifeManager
{
    public class RegisterForm : Form
    {
        private TextBox txtEmail = null!;
        private TextBox txtPass = null!;
        private TextBox txtConfirmPass = null!;
        private Button btnSubmitRegister = null!;
        private LinkLabel lnkBackToLogin = null!;
        private Panel registerPanel = null!;

        // Nhận FirebaseClient từ LoginForm truyền sang để không phải cấu hình lại
        private FirebaseAuthClient _firebaseClient;

        public RegisterForm(FirebaseAuthClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Register - LifeManager";
            Size = new Size(900, 600);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;

            // ✅ DÙNG MÀU NỀN THAY VÌ ẢNH
            this.BackColor = Color.FromArgb(30, 41, 59);

            // Bảng mờ chứa các control
            registerPanel = new Panel();
            registerPanel.Size = new Size(450, 520);
            registerPanel.BackColor = Color.FromArgb(200, 30, 41, 59);
            Controls.Add(registerPanel);

            // Tiêu đề
            Label title = new Label();
            title.Text = "CREATE ACCOUNT";
            title.ForeColor = Color.White;
            title.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            title.AutoSize = true;

            // BẮT BUỘC: Add vào Panel trước rồi mới tính kích thước
            registerPanel.Controls.Add(title);

            // Đã sửa: Để máy tính tự đo chiều rộng chữ rồi mới chia đôi
            int titleX = (registerPanel.Width - title.Width) / 2;
            title.Location = new Point(titleX, 30);

            // 1. Ô Email
            Label lblEmail = new Label() { Text = "Email (Username)", ForeColor = Color.White, AutoSize = true, Location = new Point(50, 100) };
            registerPanel.Controls.Add(lblEmail);
            txtEmail = new TextBox() { Size = new Size(350, 30), Location = new Point(50, 130) };
            registerPanel.Controls.Add(txtEmail);

            // 2. Ô Mật khẩu
            Label lblPass = new Label() { Text = "Password", ForeColor = Color.White, AutoSize = true, Location = new Point(50, 180) };
            registerPanel.Controls.Add(lblPass);
            txtPass = new TextBox() { Size = new Size(350, 30), Location = new Point(50, 210), PasswordChar = '*' };
            registerPanel.Controls.Add(txtPass);

            // 3. Ô Nhập lại mật khẩu
            Label lblConfirmPass = new Label() { Text = "Confirm Password", ForeColor = Color.White, AutoSize = true, Location = new Point(50, 260) };
            registerPanel.Controls.Add(lblConfirmPass);
            txtConfirmPass = new TextBox() { Size = new Size(350, 30), Location = new Point(50, 290), PasswordChar = '*' };
            registerPanel.Controls.Add(txtConfirmPass);

            // Nút bấm ĐĂNG KÝ
            btnSubmitRegister = new Button();
            btnSubmitRegister.Text = "REGISTER NOW";
            btnSubmitRegister.Size = new Size(350, 45);
            btnSubmitRegister.Location = new Point(50, 360);
            btnSubmitRegister.BackColor = Color.MediumPurple; // Màu tím giống thiết kế của bạn
            btnSubmitRegister.ForeColor = Color.White;
            btnSubmitRegister.FlatStyle = FlatStyle.Flat;
            btnSubmitRegister.FlatAppearance.BorderSize = 0;
            btnSubmitRegister.Click += SubmitRegister;
            registerPanel.Controls.Add(btnSubmitRegister);

            // Nút Quay lại Đăng Nhập
            lnkBackToLogin = new LinkLabel();
            lnkBackToLogin.Text = "Already have an account? Sign In";
            lnkBackToLogin.LinkColor = Color.LightGray;
            lnkBackToLogin.ActiveLinkColor = Color.DodgerBlue;
            lnkBackToLogin.BackColor = Color.Transparent;
            lnkBackToLogin.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkBackToLogin.AutoSize = true;
            registerPanel.Controls.Add(lnkBackToLogin);

            // Căn giữa link trở về đăng nhập
            int linkX = (registerPanel.Width - lnkBackToLogin.Width) / 2;
            lnkBackToLogin.Location = new Point(linkX, 430);
            lnkBackToLogin.LinkClicked += (s, e) => { this.Close(); }; // Đóng form này sẽ tự quay lại Login

            // Căn giữa panel
            CenterPanel();
            Resize += (s, e) => CenterPanel();
        }

        private void CenterPanel()
        {
            registerPanel.Left = (ClientSize.Width - registerPanel.Width) / 2;
            registerPanel.Top = (ClientSize.Height - registerPanel.Height) / 2;
        }

        // Xử lý logic Đăng ký với Firebase
        private async void SubmitRegister(object? sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string pass = txtPass.Text;
            string confirmPass = txtConfirmPass.Text;

            // 1. Kiểm tra nhập đủ thông tin
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra Mật khẩu và Nhập lại mật khẩu có khớp nhau không
            if (pass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp! Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPass.Clear();
                txtConfirmPass.Focus();
                return;
            }

            try
            {
                btnSubmitRegister.Enabled = false;
                btnSubmitRegister.Text = "ĐANG TẠO TÀI KHOẢN...";

                // Gọi Firebase tạo User
                var userCredential = await _firebaseClient.CreateUserWithEmailAndPasswordAsync(email, pass);

                MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Đóng form đăng ký, tự động trả về form đăng nhập
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmitRegister.Enabled = true;
                btnSubmitRegister.Text = "REGISTER NOW";
            }
        }
    }
}