using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace QuanLyChiTieu_GUI
{
    public class BaoCaoControl : UserControl
    {
        private ComboBox cboThang;
        private Button btnCapNhat;
        private DataGridView dgvThongKe;
        private Label lblTongThu;
        private Label lblTongChi;
        private Label lblChenhLech;
        private Label lblSoGiaoDich;
        private Label lblDebug;
        private Label lblDanhMuc;

        private class GiaoDichBaoCao
        {
            public string Loai { get; set; }
            public string DanhMuc { get; set; }
            public double SoTien { get; set; }
            public int Thang { get; set; }
        }

        private class ThongKeDanhMuc
        {
            public double Thu { get; set; }
            public double Chi { get; set; }
        }

        public BaoCaoControl()
        {
            InitializeComponent();
        }

        private string LayDuongDanFileChiTieu()
        {
            string path1 = "chitieu.csv";
            if (File.Exists(path1)) return path1;

            string path2 = Path.Combine(Application.StartupPath, "chitieu.csv");
            if (File.Exists(path2)) return path2;

            string path3 = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "chitieu.csv"));
            if (File.Exists(path3)) return path3;

            string path4 = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "..", "chitieu.csv"));
            if (File.Exists(path4)) return path4;

            return path2;
        }

        private void HienThiBaoCaoRong()
        {
            lblTongThu.Text = "Tổng thu: 0 VND";
            lblTongChi.Text = "Tổng chi: 0 VND";
            lblChenhLech.Text = "Chênh lệch: 0 VND";
            lblSoGiaoDich.Text = "Số giao dịch: 0";

            dgvThongKe.Rows.Clear();
            dgvThongKe.Rows.Add("Chưa có dữ liệu", "", "", "");

            lblDebug.Text = "Chưa có file chitieu.csv";
            lblDanhMuc.Text = "Danh mục: Không có";
        }

        public void CapNhatBaoCao()
        {
            int thang = cboThang.SelectedIndex;
            if (thang < 0) return;

            string path = LayDuongDanFileChiTieu();
            if (!File.Exists(path))
            {
                HienThiBaoCaoRong();
                return;
            }

            var transactions = new List<GiaoDichBaoCao>();
            int dongDoc = 0;

            try
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        dongDoc++;

                        if (line.Length > 0 && line[0] == '\uFEFF')
                            line = line.Substring(1);

                        string[] parts = line.Split(',');
                        if (parts.Length < 5) continue;

                        string loai = parts[0].Trim();
                        if (loai.Equals("Loai", StringComparison.OrdinalIgnoreCase) ||
                            loai.Equals("Loại", StringComparison.OrdinalIgnoreCase))
                            continue;

                        string danhMuc = parts[1].Trim();
                        double soTien = Utils.LaySoTienTuText(parts[2].Trim());
                        string ngayStr = parts[4].Trim();

                        bool parsed = DateTime.TryParseExact(
                            ngayStr,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime ngay);

                        if (!parsed)
                        {
                            parsed = DateTime.TryParseExact(
                                ngayStr,
                                "d/M/yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out ngay);
                        }

                        if (!parsed) continue;

                        transactions.Add(new GiaoDichBaoCao
                        {
                            Loai = loai,
                            DanhMuc = danhMuc,
                            SoTien = soTien,
                            Thang = ngay.Month - 1
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc file báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HienThiBaoCaoRong();
                return;
            }

            var giaoDichThang = new List<GiaoDichBaoCao>();
            foreach (GiaoDichBaoCao item in transactions)
            {
                if (item.Thang == thang)
                    giaoDichThang.Add(item);
            }

            lblDebug.Text = "Đã đọc: " + dongDoc + " dòng, lọc được: " + giaoDichThang.Count + " giao dịch";

            if (giaoDichThang.Count == 0)
            {
                lblTongThu.Text = "Tổng thu: 0 VND";
                lblTongChi.Text = "Tổng chi: 0 VND";
                lblChenhLech.Text = "Chênh lệch: 0 VND";
                lblSoGiaoDich.Text = "Số giao dịch: 0";
                dgvThongKe.Rows.Clear();
                dgvThongKe.Rows.Add("Không có dữ liệu", "", "", "");
                lblDanhMuc.Text = "Danh mục: Không có";
                dgvThongKe.Refresh();
                return;
            }

            var thongKe = new Dictionary<string, ThongKeDanhMuc>();

            foreach (GiaoDichBaoCao item in giaoDichThang)
            {
                if (!thongKe.ContainsKey(item.DanhMuc))
                    thongKe.Add(item.DanhMuc, new ThongKeDanhMuc());

                if (item.Loai == "Thu")
                    thongKe[item.DanhMuc].Thu += item.SoTien;
                else if (item.Loai == "Chi")
                    thongKe[item.DanhMuc].Chi += item.SoTien;
            }

            var dsDanhMuc = new StringBuilder();
            foreach (KeyValuePair<string, ThongKeDanhMuc> kvp in thongKe)
                dsDanhMuc.Append(kvp.Key).Append(", ");
            lblDanhMuc.Text = "Danh mục: " + dsDanhMuc;

            double tongThu = 0;
            double tongChi = 0;
            foreach (KeyValuePair<string, ThongKeDanhMuc> kvp in thongKe)
            {
                tongThu += kvp.Value.Thu;
                tongChi += kvp.Value.Chi;
            }

            lblTongThu.Text = "Tổng thu: " + Utils.DinhDangTienHienThi(tongThu);
            lblTongChi.Text = "Tổng chi: " + Utils.DinhDangTienHienThi(tongChi);
            lblChenhLech.Text = "Chênh lệch: " + Utils.DinhDangTienHienThi(tongThu - tongChi);
            lblSoGiaoDich.Text = "Số giao dịch: " + giaoDichThang.Count;

            dgvThongKe.Rows.Clear();

            foreach (KeyValuePair<string, ThongKeDanhMuc> kvp in thongKe)
            {
                double thu = kvp.Value.Thu;
                double chi = kvp.Value.Chi;
                double chenhLech = thu - chi;

                dgvThongKe.Rows.Add(
                    kvp.Key,
                    Utils.DinhDangTienHienThi(thu),
                    Utils.DinhDangTienHienThi(chi),
                    Utils.DinhDangTienHienThi(chenhLech)
                );
            }

            dgvThongKe.Rows.Add(
                "TỔNG",
                Utils.DinhDangTienHienThi(tongThu),
                Utils.DinhDangTienHienThi(tongChi),
                Utils.DinhDangTienHienThi(tongThu - tongChi)
            );

            int rowCount = dgvThongKe.Rows.Count - 1;
            if (rowCount >= 0)
            {
                dgvThongKe.Rows[rowCount].DefaultCellStyle.BackColor = Color.LightGray;
                dgvThongKe.Rows[rowCount].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }

            if (dgvThongKe.Rows.Count > 0)
                dgvThongKe.FirstDisplayedScrollingRowIndex = 0;

            dgvThongKe.Refresh();
            lblDebug.Text += " | DataGridView có " + dgvThongKe.Rows.Count + " dòng";
        }

        private void InitializeComponent()
        {
            Size = new Size(1020, 480);
            BackColor = Color.White;

            var rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var panelSummary = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0)
            };

            var summaryTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            summaryTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panelSummary.Controls.Add(summaryTable);

            lblTongThu = TaoLabelTongKet("Tổng thu: 0 VND", Color.Green);
            summaryTable.Controls.Add(lblTongThu, 0, 0);

            lblTongChi = TaoLabelTongKet("Tổng chi: 0 VND", Color.Red);
            summaryTable.Controls.Add(lblTongChi, 1, 0);

            lblChenhLech = TaoLabelTongKet("Chênh lệch: 0 VND", Color.Blue);
            summaryTable.Controls.Add(lblChenhLech, 2, 0);

            lblSoGiaoDich = TaoLabelTongKet("Số giao dịch: 0", Color.DarkOrange);
            summaryTable.Controls.Add(lblSoGiaoDich, 3, 0);

            var panelFilter = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 240, 240),
                Margin = new Padding(0)
            };

            var lblThang = new Label
            {
                Text = "Tháng:",
                Location = new Point(20, 20),
                Size = new Size(50, 25)
            };
            panelFilter.Controls.Add(lblThang);

            cboThang = new ComboBox
            {
                Location = new Point(80, 15),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            for (int i = 1; i <= 12; i++)
                cboThang.Items.Add("Tháng " + i);
            cboThang.SelectedIndex = DateTime.Now.Month - 1;
            panelFilter.Controls.Add(cboThang);

            btnCapNhat = new Button
            {
                Text = "Cập nhật",
                Location = new Point(200, 12),
                Size = new Size(100, 32),
                BackColor = Color.FromArgb(0, 150, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCapNhat.Click += btnCapNhat_Click;
            panelFilter.Controls.Add(btnCapNhat);

            lblDebug = new Label
            {
                Location = new Point(350, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.Gray,
                Visible = false
            };
            panelFilter.Controls.Add(lblDebug);

            lblDanhMuc = new Label
            {
                Location = new Point(350, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.DarkBlue,
                Visible = false
            };
            panelFilter.Controls.Add(lblDanhMuc);

            dgvThongKe = new DataGridView
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                AllowUserToAddRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 35,
                ColumnHeadersVisible = true,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvThongKe.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvThongKe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvThongKe.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 110);
            dgvThongKe.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvThongKe.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvThongKe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvThongKe.RowTemplate.Height = 28;

            dgvThongKe.Columns.Clear();

            var colDanhMuc = new DataGridViewTextBoxColumn
            {
                Name = "DanhMuc",
                HeaderText = "Danh mục",
                FillWeight = 30
            };
            colDanhMuc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvThongKe.Columns.Add(colDanhMuc);

            var colThu = new DataGridViewTextBoxColumn
            {
                Name = "Thu",
                HeaderText = "Thu (VND)",
                FillWeight = 25
            };
            colThu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvThongKe.Columns.Add(colThu);

            var colChi = new DataGridViewTextBoxColumn
            {
                Name = "Chi",
                HeaderText = "Chi (VND)",
                FillWeight = 25
            };
            colChi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvThongKe.Columns.Add(colChi);

            var colChenhLech = new DataGridViewTextBoxColumn
            {
                Name = "ChenhLech",
                HeaderText = "Chênh lệch (VND)",
                FillWeight = 20
            };
            colChenhLech.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvThongKe.Columns.Add(colChenhLech);

            rootLayout.Controls.Add(panelSummary, 0, 0);
            rootLayout.Controls.Add(panelFilter, 0, 1);
            rootLayout.Controls.Add(dgvThongKe, 0, 2);
            Controls.Add(rootLayout);

            CapNhatBaoCao();
        }

        private Label TaoLabelTongKet(string text, Color foreColor)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = foreColor,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false,
                Dock = DockStyle.Fill
            };
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            CapNhatBaoCao();
        }
    }
}
