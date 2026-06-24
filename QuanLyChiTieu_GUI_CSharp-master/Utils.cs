using System;
using System.Text;

namespace QuanLyChiTieu_GUI
{
    public static class Utils
    {
        public static string ChiLayChuSo(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            var ketQua = new StringBuilder();
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                    ketQua.Append(c);
            }
            return ketQua.ToString();
        }

        public static string DinhDangSoTienNhap(string text)
        {
            string so = ChiLayChuSo(text);
            if (string.IsNullOrEmpty(so)) return string.Empty;

            string ketQua = string.Empty;
            int dem = 0;
            for (int i = so.Length - 1; i >= 0; i--)
            {
                ketQua = so[i] + ketQua;
                dem++;
                if (dem == 3 && i != 0)
                {
                    ketQua = "." + ketQua;
                    dem = 0;
                }
            }
            return ketQua;
        }

        public static string DinhDangTienHienThi(double soTien)
        {
            long tien = Convert.ToInt64(Math.Truncate(soTien));
            string s = tien.ToString();
            string ketQua = string.Empty;
            int dem = 0;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                ketQua = s[i] + ketQua;
                dem++;
                if (dem == 3 && i != 0)
                {
                    ketQua = "." + ketQua;
                    dem = 0;
                }
            }

            return ketQua + " VND";
        }

        public static double LaySoTienTuText(string text)
        {
            string so = ChiLayChuSo(text);
            if (string.IsNullOrEmpty(so)) return 0;
            return Convert.ToDouble(so);
        }

        public static string LamSachGiaTriCSV(string text)
        {
            if (text == null) return string.Empty;
            return text.Replace(",", " ").Trim();
        }
    }
}
