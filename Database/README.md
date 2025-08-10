# Hướng Dẫn Cài Đặt Database SQL Server 2022

## 📋 Yêu Cầu Hệ Thống

- **SQL Server 2022** (Express, Standard, hoặc Enterprise)
- **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**
- **Windows Authentication** hoặc **SQL Server Authentication**

## 🚀 Các Bước Cài Đặt

### 1. Tạo Database

1. **Mở SQL Server Management Studio**
2. **Kết nối đến SQL Server** với thông tin:
   - Server name: `WINDOWS-PC\MSSQLSERVER01`
   - Authentication: Windows Authentication
3. **Chạy script SQL** từ file `CreateDatabase.sql`

### 2. Cấu Hình Connection String

Cập nhật file `appsettings.json` với connection string phù hợp:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=WINDOWS-PC\\MSSQLSERVER01;Database=DoanhNghiepPortal;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Tạo Migration và Update Database

Chạy các lệnh sau trong terminal:

```bash
# Tạo migration đầu tiên
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update
```

## 📊 Cấu Trúc Database

### Bảng Users
- **Thông tin đăng nhập**: Username, Email, Password
- **Thông tin cá nhân**: FullName, PhoneNumber, Age, Address
- **Thông tin pháp lý**: IdCardNumber, TaxCode
- **Trạng thái**: IsActive, CreatedAt, LastLoginAt

### Bảng Businesses
- **Thông tin doanh nghiệp**: BusinessCode, BusinessName, BusinessType
- **Địa chỉ**: Address, Province, District, Ward
- **Liên hệ**: PhoneNumber, Email, Website
- **Tài chính**: CharterCapital, CapitalUnit
- **Pháp lý**: LicenseNumber, Representative, Position
- **Trạng thái**: Status, CreatedAt, UpdatedAt

### Bảng BusinessRegistrations
- **Đăng ký doanh nghiệp mới**
- **Trạng thái**: Chờ duyệt, Đã duyệt, Từ chối
- **Liên kết với Users**: UserId

### Bảng BusinessLicenses
- **Đăng ký kinh doanh**
- **Trạng thái**: Chờ duyệt, Đã duyệt, Từ chối
- **Liên kết với Users**: UserId

### Bảng UserProfiles
- **Hồ sơ chi tiết**: Avatar, DateOfBirth, Gender
- **Thông tin bổ sung**: Education, Occupation, Skills
- **Liên kết với Users**: UserId

### Bảng AuditLogs
- **Nhật ký hoạt động**: Action, TableName, RecordId
- **Thông tin thay đổi**: OldValues, NewValues
- **Thông tin người dùng**: UserId, IpAddress

## 🔧 Stored Procedures

### sp_SearchBusinesses
Tìm kiếm doanh nghiệp theo nhiều tiêu chí

### sp_GetStatistics
Lấy thống kê tổng quan hệ thống

### sp_UpdateRegistrationStatus
Cập nhật trạng thái đăng ký

## ⚡ Triggers

### tr_BusinessRegistrations_Approved
Tự động tạo BusinessCode khi duyệt đăng ký

### tr_Businesses_Audit
Ghi log thay đổi thông tin doanh nghiệp

## 📈 Indexes

- **Users**: Username, Email, PhoneNumber
- **Businesses**: BusinessCode, BusinessName, Status, Province
- **BusinessRegistrations**: UserId, Status, CreatedAt
- **BusinessLicenses**: UserId, Status, CreatedAt
- **AuditLogs**: UserId, Action, CreatedAt

## 🛠️ Troubleshooting

### Lỗi Kết Nối
```
Error: Cannot connect to SQL Server
```
**Giải pháp:**
1. Kiểm tra SQL Server đã chạy chưa
2. Kiểm tra Server name đúng không
3. Kiểm tra Windows Authentication

### Lỗi Trust Server Certificate
```
Error: The certificate chain was issued by an untrusted authority
```
**Giải pháp:**
Thêm `TrustServerCertificate=true` vào connection string

### Lỗi Database Not Found
```
Error: Cannot open database "DoanhNghiepPortal"
```
**Giải pháp:**
1. Chạy script `CreateDatabase.sql`
2. Kiểm tra tên database đúng không

## 🔐 Bảo Mật

### Windows Authentication (Khuyến nghị)
- Sử dụng tài khoản Windows hiện tại
- Không cần lưu password
- Bảo mật cao

### SQL Server Authentication
- Tạo user riêng cho ứng dụng
- Sử dụng password mạnh
- Chỉ cấp quyền cần thiết

## 📝 Ghi Chú

- **Development**: Sử dụng `TrustServerCertificate=true`
- **Production**: Cấu hình SSL certificate đúng cách
- **Backup**: Tạo backup định kỳ
- **Monitoring**: Theo dõi performance và logs

## 🎯 Kết Quả

Sau khi hoàn thành, bạn sẽ có:
- ✅ Database hoàn chỉnh với 6 bảng
- ✅ Dữ liệu mẫu để test
- ✅ Stored procedures và triggers
- ✅ Indexes tối ưu hiệu suất
- ✅ Hệ thống audit log
- ✅ Kết nối Entity Framework Core 