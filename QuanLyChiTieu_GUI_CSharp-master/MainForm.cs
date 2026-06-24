using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChiTieu_GUI
{
    public class MainForm : Form
    {
        private Label lblTitle;
        private Label lblTongThu;
        private Label lblTongChi;
        private Label lblSoDu;
        private TabControl tabMain;
        private GiaoDichControl giaoDichCtrl;
        private BaoCaoControl baoCaoCtrl;

        public MainForm()
        {
            InitializeComponent();
        }

        private Label TaoTheTongKet(string text, int x, int y, Color mauNen)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(240, 70),
                BackColor = mauNen,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private void CapNhatTongKet()
        {
            if (giaoDichCtrl == null) return;

            double tongThu = giaoDichCtrl.TongThu;
            double tongChi = giaoDichCtrl.TongChi;
            double soDu = tongThu - tongChi;

            lblTongThu.Text = "Tổng thu\n" + Utils.DinhDangTienHienThi(tongThu);
            lblTongChi.Text = "Tổng chi\n" + Utils.DinhDangTienHienThi(tongChi);
            lblSoDu.Text = "Số dư\n" + Utils.DinhDangTienHienThi(soDu);
        }

        private void giaoDich_DataChanged(object sender, EventArgs e)
        {
            CapNhatTongKet();
            baoCaoCtrl?.CapNhatBaoCao();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                giaoDichCtrl?.Refresh();
                Refresh();
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab != null && tabMain.SelectedTab.Text == "Báo cáo")
                baoCaoCtrl?.CapNhatBaoCao();
        }

        private void InitializeComponent()
        {
            Text = "Quản lý chi tiêu cá nhân";
            Size = new Size(1100, 720);
            MinimumSize = new Size(1100, 720);
            StartPosition = FormStartPosition.CenterScreen;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9, FontStyle.Regular);
            Resize += MainForm_Resize;

            lblTitle = new Label
            {
                Text = "QUẢN LÝ CHI TIÊU CÁ NHÂN",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 128, 96),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            Controls.Add(lblTitle);

            lblTongThu = TaoTheTongKet("Tổng thu\n0 VND", 30, 80, Color.FromArgb(230, 255, 240));
            lblTongChi = TaoTheTongKet("Tổng chi\n0 VND", 300, 80, Color.FromArgb(255, 235, 235));
            lblSoDu = TaoTheTongKet("Số dư\n0 VND", 570, 80, Color.FromArgb(235, 245, 255));
            Controls.Add(lblTongThu);
            Controls.Add(lblTongChi);
            Controls.Add(lblSoDu);

            tabMain = new TabControl
            {
                Location = new Point(30, 170),
                Size = new Size(1020, 480),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            tabMain.SelectedIndexChanged += tabMain_SelectedIndexChanged;
            Controls.Add(tabMain);

            var tabGiaoDich = new TabPage("Giao dịch");
            var tabBaoCao = new TabPage("Báo cáo");

            tabMain.TabPages.Add(tabGiaoDich);
            tabMain.TabPages.Add(tabBaoCao);

            giaoDichCtrl = new GiaoDichControl { Dock = DockStyle.Fill };
            giaoDichCtrl.DataChanged += giaoDich_DataChanged;
            tabGiaoDich.Controls.Add(giaoDichCtrl);

            baoCaoCtrl = new BaoCaoControl { Dock = DockStyle.Fill };
            tabBaoCao.Controls.Add(baoCaoCtrl);

            CapNhatTongKet();
        }
    }
}
