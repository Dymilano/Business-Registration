
# DoanhNghiepPortal - Hệ thống Đăng ký Doanh nghiệp

## 📋 Mô tả dự án

DoanhNghiepPortal là một hệ thống web ASP.NET Core MVC được thiết kế để quản lý và xử lý các thủ tục đăng ký doanh nghiệp, cấp phép kinh doanh và quản lý thông tin doanh nghiệp. Hệ thống cung cấp giao diện thân thiện cho cả người dùng doanh nghiệp và cán bộ quản lý.

### 🎯 Mục tiêu dự án
- **Số hóa thủ tục hành chính**: Chuyển đổi từ quy trình thủ công sang quy trình điện tử
- **Tăng hiệu quả xử lý**: Giảm thời gian chờ đợi và xử lý hồ sơ
- **Minh bạch hóa**: Công khai quy trình và trạng thái xử lý
- **Tích hợp hệ thống**: Kết nối với các hệ thống quản lý nhà nước khác

### 🏛️ Đối tượng sử dụng
- **Doanh nghiệp**: Đăng ký kinh doanh, cập nhật thông tin
- **Cán bộ quản lý**: Xử lý hồ sơ, phê duyệt đăng ký
- **Quản trị viên**: Quản lý hệ thống, người dùng, nội dung
- **Công chúng**: Tìm kiếm thông tin doanh nghiệp, tin tức

## ✨ Tính năng chi tiết

### 🔐 Quản lý người dùng và xác thực

#### Đăng ký tài khoản
- **Form đăng ký**: Thu thập thông tin cá nhân cơ bản
- **Xác thực email**: Gửi link xác nhận qua email
- **Kiểm tra trùng lặp**: Ngăn chặn tài khoản trùng lặp
- **Mã hóa mật khẩu**: Bảo mật thông tin đăng nhập

#### Đăng nhập và bảo mật
- **Xác thực đa yếu tố**: Hỗ trợ SMS/Email OTP
- **Quản lý phiên đăng nhập**: Theo dõi hoạt động người dùng
- **Khóa tài khoản**: Tự động khóa sau nhiều lần đăng nhập sai
- **Đăng xuất an toàn**: Xóa session và cookie

#### Phân quyền người dùng
- **Role-based Access Control (RBAC)**:
  - **Super Admin**: Quản lý toàn bộ hệ thống
  - **Admin**: Quản lý người dùng và nội dung
  - **Moderator**: Duyệt bài viết và bình luận
  - **User**: Sử dụng các dịch vụ cơ bản
  - **Guest**: Xem thông tin công khai

### 🏢 Đăng ký và quản lý doanh nghiệp

#### Đăng ký doanh nghiệp mới
- **Thông tin cơ bản**:
  - Tên doanh nghiệp (tiếng Việt và tiếng Anh)
  - Mã số thuế
  - Địa chỉ trụ sở chính
  - Số điện thoại, email liên hệ
  - Người đại diện pháp luật

- **Thông tin kinh doanh**:
  - Ngành nghề kinh doanh (theo mã ngành)
  - Vốn điều lệ
  - Loại hình doanh nghiệp (TNHH, Cổ phần, Hộ kinh doanh)
  - Thời gian hoạt động dự kiến

#### Quản lý giấy phép kinh doanh
- **Cấp phép ban đầu**: Xem xét hồ sơ, phê duyệt, cấp mã số
- **Gia hạn giấy phép**: Thông báo hạn, quy trình gia hạn tự động
- **Thu hồi giấy phép**: Vi phạm quy định, không hoạt động

#### Theo dõi trạng thái đăng ký
- **Trạng thái hồ sơ**: Chờ xử lý, đang xem xét, yêu cầu bổ sung, chờ phê duyệt, đã phê duyệt, từ chối
- **Thông báo tự động**: Email, SMS, Push notification

### 📰 Trung tâm thông tin và truyền thông

#### Tin tức và thông báo
- **Tin tức chính thức**: Thông báo từ cơ quan quản lý, thay đổi quy định
- **Quản lý nội dung**: Hệ thống phân loại, tìm kiếm, đánh dấu tin nổi bật

#### Hướng dẫn thủ tục
- **Hướng dẫn chi tiết**: Quy trình đăng ký, danh sách giấy tờ, mẫu đơn
- **Tài liệu pháp lý**: Luật Doanh nghiệp, nghị định, thông tư hướng dẫn

### 👨‍💼 Giao diện quản trị và báo cáo

#### Dashboard quản lý
- **Thống kê tổng quan**: Số lượng doanh nghiệp, tỷ lệ phê duyệt, thời gian xử lý
- **Cảnh báo và nhắc nhở**: Hồ sơ quá hạn, giấy phép sắp hết hạn

#### Quản lý người dùng
- **Danh sách người dùng**: Tìm kiếm, lọc, xuất dữ liệu
- **Quản lý quyền hạn**: Phân quyền chi tiết, tạo nhóm người dùng

#### Xử lý đăng ký doanh nghiệp
- **Quy trình xử lý**: Phân công cán bộ, theo dõi thời gian, ghi chú
- **Báo cáo và thống kê**: Báo cáo theo thời gian, địa bàn, hiệu suất

## 🛠️ Công nghệ sử dụng

### Backend Framework
- **ASP.NET Core 8.0**: Framework web hiện đại, hiệu suất cao
- **Entity Framework Core 9.0**: ORM cho database operations
- **Identity Framework**: Quản lý xác thực và phân quyền
- **SignalR**: Real-time communication

### Database
- **SQL Server**: Database chính của hệ thống
- **Redis**: Cache và session storage
- **Entity Framework Migrations**: Quản lý schema database

### Frontend
- **HTML5**: Semantic markup
- **CSS3**: Styling và animations
- **JavaScript ES6+**: Client-side logic
- **Bootstrap 5**: Responsive UI framework
- **jQuery**: DOM manipulation và AJAX

### Security
- **JWT Tokens**: Stateless authentication
- **HTTPS**: Bảo mật truyền tải
- **CORS**: Cross-origin resource sharing
- **Data Encryption**: Mã hóa dữ liệu nhạy cảm

## 📁 Cấu trúc dự án

```
DoanhNghiepPortal/
├── Controllers/          # Controllers xử lý logic
├── Models/              # Data models
├── Views/               # Razor views
├── Data/                # Database context
├── Migrations/          # Entity Framework migrations
├── wwwroot/             # Static files (CSS, JS, Images)
└── Program.cs           # Entry point
```

## 🚀 Cài đặt và chạy

### Yêu cầu hệ thống
- .NET 8.0 SDK
- SQL Server (LocalDB hoặc SQL Server Express)
- Visual Studio 2022 hoặc Visual Studio Code

### Bước 1: Clone dự án
```bash
git clone <repository-url>
cd DoanhNghiepPortal
```

### Bước 2: Cài đặt dependencies
```bash
dotnet restore
```

### Bước 3: Cấu hình database
1. Cập nhật connection string trong `appsettings.json`
2. Chạy migrations:
```bash
dotnet ef database update
```

### Bước 4: Chạy ứng dụng
```bash
dotnet run
```

Ứng dụng sẽ chạy tại: `http://localhost:5106`

## 🗄️ Cấu hình Database

Connection string mẫu trong `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DoanhNghiepPortal;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

## 👥 Tài khoản mặc định

- **Admin**: Tài khoản quản trị hệ thống
- **User**: Tài khoản người dùng thông thường

## 📱 Giao diện

Hệ thống được thiết kế responsive với Bootstrap, hỗ trợ đầy đủ trên các thiết bị:
- Desktop
- Tablet
- Mobile

## 🔧 Phát triển

### Chạy trong môi trường Development
```bash
dotnet run --environment Development
```

### Tạo migration mới
```bash
dotnet ef migrations add <MigrationName>
```

### Cập nhật database
```bash
dotnet ef database update
```

## 📄 Giấy phép

Dự án này được phát triển cho mục đích giáo dục và thương mại.

## 👨‍💻 Đóng góp

Mọi đóng góp đều được chào đón! Vui lòng:
1. Fork dự án
2. Tạo feature branch
3. Commit thay đổi
4. Push lên branch
5. Tạo Pull Request


Nếu có câu hỏi hoặc góp ý, vui lòng liên hệ qua:
- Email: nguyenduymilano@gmail.com
- GitHub Issues: https://github.com/Dymilano
- Liên hệ : 0349729139

---

**Phiên bản**: 1.0.0  
**Cập nhật lần cuối**: Tháng 8, 2025
