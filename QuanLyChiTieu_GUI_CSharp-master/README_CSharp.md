# QuanLyChiTieu_GUI - bản C# WinForms

Đây là bản chuyển từ C++/CLI WinForms sang C# WinForms.

## File chính

- `Program.cs`: điểm chạy chương trình.
- `MainForm.cs`: giao diện chính, tiêu đề, thẻ tổng thu/tổng chi/số dư, tab Giao dịch và Báo cáo.
- `GiaoDichControl.cs`: thêm, sửa, xóa, lọc giao dịch và lưu vào `chitieu.csv`.
- `BaoCaoControl.cs`: đọc `chitieu.csv`, thống kê theo tháng và danh mục.
- `Utils.cs`: hàm định dạng tiền, lấy số tiền, làm sạch dữ liệu CSV.

## Cách chạy

1. Mở Visual Studio.
2. Chọn `Open a project or solution`.
3. Mở file `QuanLyChiTieu_GUI_CSharp.csproj`.
4. Build và Run.

## Dữ liệu

Chương trình dùng file `chitieu.csv` để lưu giao dịch. Nếu chưa có file này, chương trình sẽ tự tạo file trống khi chạy.

Nếu muốn có dữ liệu mẫu để test:

1. Copy file `chitieu_sample.csv`.
2. Đổi tên bản copy thành `chitieu.csv`.
3. Đặt cùng thư mục chạy chương trình hoặc cùng thư mục project.

Không nên upload `chitieu.csv` thật lên GitHub nếu trong đó có dữ liệu chi tiêu cá nhân.
