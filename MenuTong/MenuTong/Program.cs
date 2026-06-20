using System;
using System.Windows.Forms;
using LifeManager;

namespace MenuTong
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Tạo form Login
            LoginForm loginForm = new LoginForm();

            // Đăng ký sự kiện đăng nhập thành công
            loginForm.LoginSuccess += (sender, e) =>
            {
                // Ẩn Login
                loginForm.Hide();

                // Mở MenuTong với thông tin user
                FormMenuTong mainForm = new FormMenuTong(e.UserName, e.UserEmail);
                mainForm.Show();

                // Khi MenuTong đóng, thoát ứng dụng
                mainForm.FormClosed += (s, args) => Application.Exit();
            };

            // Chạy form Login
            Application.Run(loginForm);
        }
    }
}