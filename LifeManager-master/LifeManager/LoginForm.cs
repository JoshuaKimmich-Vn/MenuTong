using System;
using System.Drawing;
using System.Windows.Forms;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace LifeManager
{
    public partial class LoginForm : Form
    {
        // Event để thông báo đăng nhập thành công
        public event EventHandler<LoginEventArgs>? LoginSuccess;

        private TextBox txtUser = null!;
        private TextBox txtPass = null!;
        private Button btnLogin = null!;
        private Button btnRegister = null!;
        private CheckBox chkShow = null!;
        private LinkLabel lnkForgotPass = null!;
        private Panel loginPanel = null!;
        private FirebaseAuthClient _firebaseClient = null!;

        public LoginForm()
        {
            InitializeComponent();
            InitializeUI();
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyCPgL9-XwTYc4CK92GRqStfK2wYo2ygNM4",
                AuthDomain = "project-b45ce.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };

            _firebaseClient = new FirebaseAuthClient(config);
        }

        private void InitializeUI()
        {
            Text = "LifeManager";
            Size = new Size(900, 600);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;

            // Màu nền
            this.BackColor = Color.FromArgb(30, 41, 59);

            //--------------------------------------------------
            // LOGIN PANEL
            //--------------------------------------------------

            loginPanel = new Panel();
            loginPanel.Size = new Size(450, 520);
            loginPanel.BackColor = Color.FromArgb(200, 30, 41, 59);
            Controls.Add(loginPanel);

            //--------------------------------------------------
            // TITLE
            //--------------------------------------------------

            Label title = new Label();
            title.Text = "LifeManager";
            title.ForeColor = Color.White;
            title.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            title.AutoSize = true;
            loginPanel.Controls.Add(title);
            int titleX = (loginPanel.Width - title.Width) / 2;
            title.Location = new Point(titleX, 40);

            //--------------------------------------------------
            // USERNAME
            //--------------------------------------------------

            Label lblUser = new Label();
            lblUser.Text = "Username";
            lblUser.ForeColor = Color.White;
            lblUser.AutoSize = true;
            lblUser.Location = new Point(50, 140);
            loginPanel.Controls.Add(lblUser);

            txtUser = new TextBox();
            txtUser.Size = new Size(350, 30);
            txtUser.Location = new Point(50, 170);
            loginPanel.Controls.Add(txtUser);

            //--------------------------------------------------
            // PASSWORD
            //--------------------------------------------------

            Label lblPass = new Label();
            lblPass.Text = "Password";
            lblPass.ForeColor = Color.White;
            lblPass.AutoSize = true;
            lblPass.Location = new Point(50, 240);
            loginPanel.Controls.Add(lblPass);

            txtPass = new TextBox();
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(350, 30);
            txtPass.Location = new Point(50, 270);
            loginPanel.Controls.Add(txtPass);

            //--------------------------------------------------
            // SHOW PASSWORD
            //--------------------------------------------------

            chkShow = new CheckBox();
            chkShow.Text = "Show Password";
            chkShow.ForeColor = Color.White;
            chkShow.BackColor = Color.Transparent;
            chkShow.AutoSize = true;
            chkShow.Location = new Point(50, 320);
            chkShow.CheckedChanged += (s, e) =>
            {
                txtPass.PasswordChar = chkShow.Checked ? '\0' : '*';
            };
            loginPanel.Controls.Add(chkShow);

            //--------------------------------------------------
            // FORGOT PASSWORD
            //--------------------------------------------------

            lnkForgotPass = new LinkLabel();
            lnkForgotPass.Text = "Forgot Password?";
            lnkForgotPass.LinkColor = Color.LightGray;
            lnkForgotPass.ActiveLinkColor = Color.DodgerBlue;
            lnkForgotPass.BackColor = Color.Transparent;
            lnkForgotPass.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkForgotPass.AutoSize = true;
            loginPanel.Controls.Add(lnkForgotPass);
            lnkForgotPass.Location = new Point(txtPass.Right - lnkForgotPass.Width, 320);
            lnkForgotPass.LinkClicked += ForgotPassword;

            //--------------------------------------------------
            // LOGIN BUTTON
            //--------------------------------------------------

            btnLogin = new Button();
            btnLogin.Text = "SIGN IN";
            btnLogin.Size = new Size(350, 45);
            btnLogin.Location = new Point(50, 370);
            btnLogin.BackColor = Color.DodgerBlue;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += Login;
            loginPanel.Controls.Add(btnLogin);

            //--------------------------------------------------
            // REGISTER BUTTON
            //--------------------------------------------------

            btnRegister = new Button();
            btnRegister.Text = "REGISTER";
            btnRegister.Size = new Size(350, 45);
            btnRegister.Location = new Point(50, 430);
            btnRegister.BackColor = Color.MediumPurple;
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += Register;
            loginPanel.Controls.Add(btnRegister);

            CenterLoginPanel();
            Resize += (s, e) => CenterLoginPanel();
        }

        private void CenterLoginPanel()
        {
            loginPanel.Left = (ClientSize.Width - loginPanel.Width) / 2;
            loginPanel.Top = (ClientSize.Height - loginPanel.Height) / 2;
        }

        private async void Login(object? sender, EventArgs e)
        {
            string email = txtUser.Text.Trim();
            string password = txtPass.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ Username (Email) và Password!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "ĐANG ĐĂNG NHẬP...";
                var userCredential = await _firebaseClient.SignInWithEmailAndPasswordAsync(email, password);

                string userName = email.Split('@')[0];
                string userEmail = email;

                // ✅ Phát event - KHÔNG đóng form
                LoginSuccess?.Invoke(this, new LoginEventArgs(userName, userEmail));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Sai tài khoản hoặc mật khẩu!\nChi tiết lỗi Firebase: " + ex.Message,
                    "Lỗi đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "SIGN IN";
            }
        }

        private void Register(object? sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(_firebaseClient);
            this.Hide();
            registerForm.ShowDialog();
            this.Show();
        }

        private async void ForgotPassword(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            string email = txtUser.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show(
                    "Vui lòng nhập Email của bạn vào ô Username trước khi yêu cầu khôi phục mật khẩu!",
                    "Hướng dẫn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtUser.Focus();
                return;
            }
            try
            {
                lnkForgotPass.Enabled = false;
                lnkForgotPass.Text = "Đang gửi...";

                await _firebaseClient.ResetEmailPasswordAsync(email);

                MessageBox.Show(
                    $"Đã gửi hướng dẫn khôi phục mật khẩu tới email:\n{email}\n\nVui lòng kiểm tra hộp thư (và cả mục Thư Rác/Spam) của bạn.",
                    "Gửi thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra khi gửi yêu cầu khôi phục: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                lnkForgotPass.Enabled = true;
                lnkForgotPass.Text = "Forgot Password?";
                lnkForgotPass.Location = new Point(txtPass.Right - lnkForgotPass.Width, 320);
            }
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public string UserName { get; }
        public string UserEmail { get; }

        public LoginEventArgs(string userName, string userEmail)
        {
            UserName = userName;
            UserEmail = userEmail;
        }
    }
}