using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace QuanLyChiTieu_GUI
{
    public class GiaoDichControl : UserControl
    {
        private DateTimePicker dtpNgay;
        private ComboBox cboLoai;
        private ComboBox cboDanhMuc;
        private ComboBox cboLocLoai;
        private TextBox txtSoTien;
        private TextBox txtGhiChu;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvGiaoDich;
        private string[] danhMucThu;
        private string[] danhMucChi;
        private bool dangDinhDangSoTien;

        public event EventHandler DataChanged;

        public double TongThu
        {
            get
            {
                double sum = 0;
                foreach (DataGridViewRow row in dgvGiaoDich.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells[2].Value?.ToString() == "Thu")
                        sum += Utils.LaySoTienTuText(row.Cells[4].Value?.ToString());
                }
                return sum;
            }
        }

        public double TongChi
        {
            get
            {
                double sum = 0;
                foreach (DataGridViewRow row in dgvGiaoDich.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells[2].Value?.ToString() == "Chi")
                        sum += Utils.LaySoTienTuText(row.Cells[4].Value?.ToString());
                }
                return sum;
            }
        }

        public GiaoDichControl()
        {
            dangDinhDangSoTien = false;
            InitializeComponent();

            danhMucThu = new[] { "Lương", "Trợ cấp", "Thưởng", "Thu nhập phụ", "Đầu tư", "Thu nhập tạm" };
            danhMucChi = new[] { "Ăn uống", "Di chuyển", "Học tập", "Mua sắm", "Sức khỏe", "Tiền nhà", "Tiền điện", "Phí liên lạc", "Quần áo", "Mỹ phẩm", "Phí giao lưu", "Y tế" };

            CapNhatDanhMucTheoLoai();
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

        private void DocDuLieuTuFile()
        {
            dgvGiaoDich.Rows.Clear();
            string duongDan = LayDuongDanFileChiTieu();

            if (!File.Exists(duongDan))
            {
                try
                {
                    using (var writer = new StreamWriter(duongDan, false, new UTF8Encoding(true)))
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tạo file chitieu.csv: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            try
            {
                using (var reader = new StreamReader(duongDan, Encoding.UTF8))
                {
                    int stt = 1;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        if (line.Length > 0 && line[0] == '\uFEFF')
                            line = line.Substring(1);

                        string[] parts = line.Split(',');
                        if (parts.Length < 5) continue;

                        string loai = parts[0].Trim();
                        if (loai.Equals("Loai", StringComparison.OrdinalIgnoreCase) ||
                            loai.Equals("Loại", StringComparison.OrdinalIgnoreCase))
                            continue;

                        string danhMuc = parts[1].Trim();
                        string soTienText = parts[2].Trim();
                        string ghiChu = parts[3].Trim();
                        string ngay = parts[4].Trim();
                        double soTien = Utils.LaySoTienTuText(soTienText);

                        dgvGiaoDich.Rows.Add(
                            stt.ToString(),
                            ngay,
                            loai,
                            danhMuc,
                            Utils.DinhDangTienHienThi(soTien),
                            ghiChu
                        );
                        stt++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LuuDuLieuVaoFile()
        {
            string duongDan = LayDuongDanFileChiTieu();

            try
            {
                using (var writer = new StreamWriter(duongDan, false, new UTF8Encoding(true)))
                {
                    foreach (DataGridViewRow row in dgvGiaoDich.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Cells[1].Value == null || row.Cells[2].Value == null ||
                            row.Cells[3].Value == null || row.Cells[4].Value == null ||
                            row.Cells[5].Value == null) continue;

                        string ngay = row.Cells[1].Value.ToString();
                        string loai = row.Cells[2].Value.ToString();
                        string danhMuc = row.Cells[3].Value.ToString();
                        string soTienText = row.Cells[4].Value.ToString();
                        string ghiChu = row.Cells[5].Value.ToString();
                        double soTien = Utils.LaySoTienTuText(soTienText);
                        long soTienLuu = Convert.ToInt64(soTien);

                        string line = Utils.LamSachGiaTriCSV(loai) + "," +
                                      Utils.LamSachGiaTriCSV(danhMuc) + "," +
                                      soTienLuu + "," +
                                      Utils.LamSachGiaTriCSV(ghiChu) + "," +
                                      Utils.LamSachGiaTriCSV(ngay);
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatSTT()
        {
            for (int i = 0; i < dgvGiaoDich.Rows.Count; i++)
            {
                dgvGiaoDich.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
        }

        private void CapNhatSTTSauLoc()
        {
            int stt = 1;
            foreach (DataGridViewRow row in dgvGiaoDich.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Visible)
                {
                    row.Cells[0].Value = stt.ToString();
                    stt++;
                }
            }
        }

        private void LocTheoLoai(string loaiCanLoc)
        {
            bool tatCa = loaiCanLoc == "Tất cả";
            foreach (DataGridViewRow row in dgvGiaoDich.Rows)
            {
                if (row.IsNewRow) continue;
                if (tatCa)
                {
                    row.Visible = true;
                }
                else
                {
                    string loaiRow = row.Cells[2].Value?.ToString();
                    row.Visible = loaiRow == loaiCanLoc;
                }
            }
            CapNhatSTTSauLoc();
        }

        private void LamMoiNhapLieu()
        {
            txtSoTien.Clear();
            txtGhiChu.Clear();
            cboLoai.SelectedIndex = 0;
            cboDanhMuc.SelectedIndex = 0;
            dtpNgay.Value = DateTime.Now;
            dgvGiaoDich.ClearSelection();
        }

        private void ChonGiaTriComboBox(ComboBox comboBox, string giaTri)
        {
            if (string.IsNullOrEmpty(giaTri)) return;
            int index = comboBox.FindStringExact(giaTri);
            if (index >= 0)
                comboBox.SelectedIndex = index;
            else
            {
                comboBox.Items.Add(giaTri);
                comboBox.SelectedItem = giaTri;
            }
        }

        private void txtSoTien_TextChanged(object sender, EventArgs e)
        {
            if (dangDinhDangSoTien) return;
            dangDinhDangSoTien = true;

            string textCu = txtSoTien.Text;
            string textMoi = Utils.DinhDangSoTienNhap(textCu);
            if (textCu != textMoi)
            {
                txtSoTien.Text = textMoi;
                txtSoTien.SelectionStart = txtSoTien.Text.Length;
            }

            dangDinhDangSoTien = false;
        }

        private void dgvGiaoDich_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvGiaoDich.Rows[e.RowIndex];
            if (row == null || row.IsNewRow) return;
            if (row.Cells[1].Value == null || row.Cells[2].Value == null ||
                row.Cells[3].Value == null || row.Cells[4].Value == null ||
                row.Cells[5].Value == null) return;

            string ngayText = row.Cells[1].Value.ToString();
            string loai = row.Cells[2].Value.ToString();
            string danhMuc = row.Cells[3].Value.ToString();
            string soTienText = row.Cells[4].Value.ToString();
            string ghiChu = row.Cells[5].Value.ToString();

            if (DateTime.TryParseExact(ngayText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay))
                dtpNgay.Value = ngay;
            else
                dtpNgay.Value = DateTime.Now;

            ChonGiaTriComboBox(cboLoai, loai);
            ChonGiaTriComboBox(cboDanhMuc, danhMuc);
            txtSoTien.Text = Utils.DinhDangSoTienNhap(soTienText);
            txtGhiChu.Text = ghiChu;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            double soTien = Utils.LaySoTienTuText(txtSoTien.Text);
            if (soTien <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboLoai.SelectedItem == null || cboDanhMuc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại giao dịch và danh mục.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stt = dgvGiaoDich.Rows.Count + 1;
            string ngay = dtpNgay.Value.ToString("dd/MM/yyyy");
            string loai = cboLoai.SelectedItem.ToString();
            string danhMuc = cboDanhMuc.SelectedItem.ToString();
            string ghiChu = txtGhiChu.Text;

            dgvGiaoDich.Rows.Add(
                stt.ToString(),
                ngay,
                loai,
                danhMuc,
                Utils.DinhDangTienHienThi(soTien),
                ghiChu
            );

            CapNhatSTT();
            LuuDuLieuVaoFile();
            LamMoiNhapLieu();
            DataChanged?.Invoke(this, EventArgs.Empty);
            MessageBox.Show("Đã thêm giao dịch thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double soTien = Utils.LaySoTienTuText(txtSoTien.Text);
            if (soTien <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboLoai.SelectedItem == null || cboDanhMuc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại giao dịch và danh mục.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvGiaoDich.SelectedRows[0];
            row.Cells[1].Value = dtpNgay.Value.ToString("dd/MM/yyyy");
            row.Cells[2].Value = cboLoai.SelectedItem.ToString();
            row.Cells[3].Value = cboDanhMuc.SelectedItem.ToString();
            row.Cells[4].Value = Utils.DinhDangTienHienThi(soTien);
            row.Cells[5].Value = txtGhiChu.Text;

            LuuDuLieuVaoFile();
            LamMoiNhapLieu();
            DataChanged?.Invoke(this, EventArgs.Empty);
            MessageBox.Show("Đã sửa giao dịch thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvGiaoDich.SelectedRows[0];
            string ngay = row.Cells[1].Value?.ToString();
            string loai = row.Cells[2].Value?.ToString();
            string danhMuc = row.Cells[3].Value?.ToString();
            string soTien = row.Cells[4].Value?.ToString();
            string thongBao = string.Format(
                "Bạn có chắc muốn xóa giao dịch này?\n\nNgày: {0}\nLoại: {1}\nDanh mục: {2}\nSố tiền: {3}",
                ngay, loai, danhMuc, soTien);

            if (MessageBox.Show(thongBao, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvGiaoDich.Rows.Remove(row);
                CapNhatSTT();
                LuuDuLieuVaoFile();
                LamMoiNhapLieu();
                DataChanged?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Đã xóa giao dịch thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiNhapLieu();
        }

        private void cboLocLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLocLoai.SelectedItem == null) return;
            LocTheoLoai(cboLocLoai.SelectedItem.ToString());
        }

        private void GiaoDichControl_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void CapNhatDanhMucTheoLoai()
        {
            if (cboLoai.SelectedItem == null) return;
            if (danhMucThu == null || danhMucChi == null) return;

            string loai = cboLoai.SelectedItem.ToString();
            cboDanhMuc.Items.Clear();

            string[] danhMucHienTai = loai == "Thu" ? danhMucThu : danhMucChi;
            foreach (string item in danhMucHienTai)
                cboDanhMuc.Items.Add(item);

            if (cboDanhMuc.Items.Count > 0)
                cboDanhMuc.SelectedIndex = 0;
        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatDanhMucTheoLoai();
        }

        private void InitializeComponent()
        {
            Size = new Size(1020, 480);
            MinimumSize = new Size(800, 400);
            Resize += GiaoDichControl_Resize;

            var mainTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            Controls.Add(mainTable);

            var groupNhap = new GroupBox
            {
                Text = "Thông tin giao dịch",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Fill
            };
            mainTable.Controls.Add(groupNhap, 0, 0);

            var nhapTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                ColumnCount = 6,
                RowCount = 2
            };
            nhapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            nhapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            nhapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            nhapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            nhapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            nhapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            nhapTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            nhapTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            groupNhap.Controls.Add(nhapTable);

            var lblNgay = new Label { Text = "Ngày:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            nhapTable.Controls.Add(lblNgay, 0, 0);

            dtpNgay = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };
            nhapTable.Controls.Add(dtpNgay, 1, 0);

            var lblLoai = new Label { Text = "Loại:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            nhapTable.Controls.Add(lblLoai, 2, 0);

            cboLoai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            cboLoai.Items.Add("Thu");
            cboLoai.Items.Add("Chi");
            cboLoai.SelectedIndex = 0;
            cboLoai.SelectedIndexChanged += cboLoai_SelectedIndexChanged;
            nhapTable.Controls.Add(cboLoai, 3, 0);

            var lblDanhMuc = new Label { Text = "Danh mục:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            nhapTable.Controls.Add(lblDanhMuc, 4, 0);

            cboDanhMuc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            nhapTable.Controls.Add(cboDanhMuc, 5, 0);

            var lblSoTien = new Label { Text = "Số tiền:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            nhapTable.Controls.Add(lblSoTien, 0, 1);

            txtSoTien = new TextBox { Dock = DockStyle.Fill };
            txtSoTien.TextChanged += txtSoTien_TextChanged;
            nhapTable.Controls.Add(txtSoTien, 1, 1);

            var lblGhiChu = new Label { Text = "Ghi chú:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            nhapTable.Controls.Add(lblGhiChu, 2, 1);

            txtGhiChu = new TextBox { Dock = DockStyle.Fill };
            nhapTable.Controls.Add(txtGhiChu, 3, 1);

            var buttonPanel = new Panel { Dock = DockStyle.Fill };
            nhapTable.Controls.Add(buttonPanel, 4, 1);
            nhapTable.SetColumnSpan(buttonPanel, 2);

            btnThem = TaoNut("Thêm", 20, btnThem_Click);
            buttonPanel.Controls.Add(btnThem);

            btnSua = TaoNut("Sửa", 115, btnSua_Click);
            buttonPanel.Controls.Add(btnSua);

            btnXoa = TaoNut("Xóa", 210, btnXoa_Click);
            buttonPanel.Controls.Add(btnXoa);

            btnLamMoi = TaoNut("Làm mới", 305, btnLamMoi_Click);
            buttonPanel.Controls.Add(btnLamMoi);

            dgvGiaoDich = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            dgvGiaoDich.CellClick += dgvGiaoDich_CellClick;
            dgvGiaoDich.Columns.Add("STT", "STT");
            dgvGiaoDich.Columns.Add("Ngay", "Ngày");
            dgvGiaoDich.Columns.Add("Loai", "Loại");
            dgvGiaoDich.Columns.Add("DanhMuc", "Danh mục");
            dgvGiaoDich.Columns.Add("SoTien", "Số tiền");
            dgvGiaoDich.Columns.Add("GhiChu", "Ghi chú");
            mainTable.Controls.Add(dgvGiaoDich, 0, 1);

            var panelLoc = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            mainTable.Controls.Add(panelLoc, 0, 2);

            var lblLoc = new Label
            {
                Text = "Lọc:",
                Location = new Point(10, 6),
                Size = new Size(40, 25)
            };
            panelLoc.Controls.Add(lblLoc);

            cboLocLoai = new ComboBox
            {
                Location = new Point(55, 3),
                Size = new Size(120, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboLocLoai.Items.Add("Tất cả");
            cboLocLoai.Items.Add("Thu");
            cboLocLoai.Items.Add("Chi");
            cboLocLoai.SelectedIndex = 0;
            cboLocLoai.SelectedIndexChanged += cboLocLoai_SelectedIndexChanged;
            panelLoc.Controls.Add(cboLocLoai);

            DocDuLieuTuFile();
        }

        private Button TaoNut(string text, int x, EventHandler clickHandler)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(85, 30),
                Location = new Point(x, 5),
                BackColor = Color.FromArgb(0, 150, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            button.Click += clickHandler;
            return button;
        }
    }
}
