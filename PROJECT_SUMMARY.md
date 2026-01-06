# TỔNG HỢP CHI TIẾT ĐỒ ÁN: HỆ THỐNG QUẢN LÝ NÔNG TRẠI THÔNG MINH

## 1. Thông tin chung
- **Tên dự án**: Hệ thống Quản lý Nông nghiệp Thông minh
- **Mục tiêu**: Hiện đại hóa quy trình quản lý nông trại, tối ưu hóa nguồn lực và tăng tính minh bạch trong truy xuất nguồn gốc sản phẩm.
- **Đối tượng sử dụng**: Chủ nông trại, quản lý kho, nhân viên kỹ thuật.

## 2. Kiến trúc hệ thống (3-Layer Architecture)
Dự án được xây dựng theo mô hình 3 lớp chuẩn để đảm bảo tính bảo trì và mở rộng:
1. **GUI (Presentation Layer)**: Giao diện người dùng WinForms hiện đại.
2. **BUS (Business Logic Layer)**: Xử lý các nghiệp vụ, tính toán và logic ứng dụng.
3. **DAL (Data Access Layer)**: Tương tác với cơ sở dữ liệu SQL Server thông qua Entity Framework.

## 3. Các chức năng chính (Full Features)

### 🔑 Hệ thống Đăng nhập & Phân quyền
- **Giao diện không viền (Frameless)**: Tạo trải nghiệm hiện đại như ứng dụng di động.
- **Đăng nhập an toàn**: Lưu thông tin phiên làm việc (Session), hỗ trợ phân quyền Admin/Quản lý/Nhân viên.
- **Hỗ trợ người dùng**: Nút thoát nhanh, kéo thả cửa sổ tự do, liên kết quên mật khẩu/đăng ký.
- **Đăng nhập bằng mã QR**: Cho phép chuyển nhanh sang trình quét thông tin không cần login.

### 📈 Dashboard & Quản lý Trung tâm
- **Thống kê tổng quan**: Số lượng cây trồng, vật nuôi hiện có, diện tích trồng trọt.
- **Biểu đồ tài chính**: Trực quan hóa doanh thu và chi phí theo thời gian.
- **Widgets**: Hiển thị thời tiết thời gian thực và các nhắc nhở công việc quan trọng.

### 🔍 Hệ thống Quét mã QR (QR Integration)
- **Tốc độ cao**: Sử dụng thư viện `ZXing` kết hợp `AForge` để xử lý hình ảnh camera mượt mà.
- **Tra cứu thời gian thực**: Quét mã nhận diện ngay thông tin chi tiết về vòng đời cây trồng (Crop) hoặc tình trạng sức khỏe vật nuôi (Animal).
- **Lịch sử quét**: Lưu lại các lần quét gần nhất để tiện theo dõi.

### 📦 Quân lý Sản xuất
- **Cây trồng (Crops)**: Quản lý giống, ngày trồng, dự kiến thu hoạch, tình trạng sâu bệnh.
- **Vật nuôi (Animals)**: Quản lý giống, tuổi, cân nặng, lịch tiêm chủng, số lượng.
- **Kho bãi & Nhân sự**: Theo dõi tồn kho vật tư nông nghiệp và lịch làm việc của công nhân.

## 4. Công nghệ & Thư viện (Tech Stack)
- **Framework**: .NET Framework 4.7.2
- **Lập trình**: C# (C-Sharp)
- **Cơ sở dữ liệu**: SQL Server + Entity Framework (Model-First/Code-First).
- **Giao diện**: Giao diện WinForms tùy chỉnh, Win32 API cho hiệu ứng kéo thả (Draggability).
- **Thư viện xử lý QR**: `ZXing.Net`, `AForge.Video`, `AForge.Video.DirectShow`.

## 5. Đặc điểm nổi bậy về UI/UX
- **Premium Design**: Sử dụng bảng màu hiện đại, font chữ Segoe UI sang trọng thay vì font mặc định hệ thống.
- **Transparency**: Các nhãn chữ trên màn hình đăng nhập được làm trong suốt, tích hợp hoàn hảo vào ảnh nền nông trại.
- **Responsive Navigation**: Thanh Sidebar linh hoạt, tự động làm nổi bật menu đang hoạt động.
- **Custom Window Management**: Tích hợp các nút điều khiển (Đóng, Thu nhỏ) tùy chỉnh phù hợp với phong cách ứng dụng.

## 6. Hướng dẫn cài đặt & Chạy ứng dụng
1. **Cơ sở dữ liệu**: Đảm bảo đã chạy file script SQL hoặc đính kèm Database trong SQL Management Studio.
2. **Cấu hình**: Kiểm tra chuỗi kết nối (Connection String) trong file `App.config` ở project GUI.
3. **Thư viện**: Restore các gói NuGet (AForge, ZXing).
4. **Build**: Mở file `.sln` bằng Visual Studio và nhấn F5 để khởi chạy.

## 7. Định hướng phát triển tương lai
- Tích hợp đăng nhập mạng xã hội (Google, Microsoft).
- Kết nối với các cảm biến IoT để tự động cập nhật dữ liệu môi trường (độ ẩm, nhiệt độ).
- Phát triển thêm ứng dụng di động để quản lý nông trại từ xa.
