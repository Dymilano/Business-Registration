-- =============================================
-- Tạo Database DoanhNghiepPortal
-- =============================================
USE master;
GO

-- Kiểm tra và xóa database nếu tồn tại
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'DoanhNghiepPortal')
BEGIN
    ALTER DATABASE DoanhNghiepPortal SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DoanhNghiepPortal;
END
GO

-- Tạo database mới
CREATE DATABASE DoanhNghiepPortal
ON PRIMARY (
    NAME = DoanhNghiepPortal_Data,
    FILENAME = 'C:\SQLServer\Data\DoanhNghiepPortal.mdf',
    SIZE = 100MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10MB
)
LOG ON (
    NAME = DoanhNghiepPortal_Log,
    FILENAME = 'C:\SQLServer\Logs\DoanhNghiepPortal.ldf',
    SIZE = 50MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 5MB
);
GO

USE DoanhNghiepPortal;
GO

-- =============================================
-- Tạo bảng Users (Người dùng)
-- =============================================
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
    Age INT,
    CurrentAddress NVARCHAR(500),
    PermanentAddress NVARCHAR(500),
    IdCardNumber NVARCHAR(12),
    Hometown NVARCHAR(100),
    TaxCode NVARCHAR(13),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2,
    LastLoginAt DATETIME2
);
GO

-- =============================================
-- Tạo bảng BusinessRegistrations (Đăng ký doanh nghiệp)
-- =============================================
CREATE TABLE BusinessRegistrations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    BusinessName NVARCHAR(200) NOT NULL,
    BusinessType NVARCHAR(50) NOT NULL,
    BusinessLine NVARCHAR(200),
    Address NVARCHAR(500) NOT NULL,
    Province NVARCHAR(50),
    District NVARCHAR(50),
    Ward NVARCHAR(50),
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(100),
    Website NVARCHAR(200),
    CharterCapital DECIMAL(18,2),
    CapitalUnit NVARCHAR(10) DEFAULT 'VND',
    Representative NVARCHAR(100),
    Position NVARCHAR(50),
    IdNumber NVARCHAR(12),
    EstablishmentDate DATE,
    Status NVARCHAR(20) DEFAULT 'Chờ duyệt',
    Notes NVARCHAR(1000),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2,
    ApprovedAt DATETIME2,
    ApprovedBy NVARCHAR(50),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
GO

-- =============================================
-- Tạo bảng BusinessLicenses (Đăng ký kinh doanh)
-- =============================================
CREATE TABLE BusinessLicenses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    BusinessName NVARCHAR(200) NOT NULL,
    BusinessType NVARCHAR(50) NOT NULL,
    BusinessLine NVARCHAR(200),
    Address NVARCHAR(500) NOT NULL,
    Province NVARCHAR(50),
    District NVARCHAR(50),
    Ward NVARCHAR(50),
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(100),
    Website NVARCHAR(200),
    Representative NVARCHAR(100),
    Position NVARCHAR(50),
    IdNumber NVARCHAR(12),
    EstablishmentDate DATE,
    Status NVARCHAR(20) DEFAULT 'Chờ duyệt',
    Notes NVARCHAR(1000),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2,
    ApprovedAt DATETIME2,
    ApprovedBy NVARCHAR(50),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
GO

-- =============================================
-- Tạo bảng Businesses (Doanh nghiệp đã được duyệt)
-- =============================================
CREATE TABLE Businesses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BusinessCode NVARCHAR(13) NOT NULL UNIQUE,
    BusinessName NVARCHAR(200) NOT NULL,
    EnglishName NVARCHAR(200),
    ShortName NVARCHAR(50),
    BusinessType NVARCHAR(50) NOT NULL,
    BusinessLine NVARCHAR(200),
    Address NVARCHAR(500) NOT NULL,
    Province NVARCHAR(50),
    District NVARCHAR(50),
    Ward NVARCHAR(50),
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(100),
    Website NVARCHAR(200),
    CharterCapital DECIMAL(18,2),
    CapitalUnit NVARCHAR(10) DEFAULT 'VND',
    EstablishmentDate DATE,
    LicenseDate DATE,
    LicenseNumber NVARCHAR(50),
    Representative NVARCHAR(100),
    Position NVARCHAR(50),
    Status NVARCHAR(20) DEFAULT 'Đang hoạt động',
    Notes NVARCHAR(1000),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2,
    CreatedBy NVARCHAR(50),
    UpdatedBy NVARCHAR(50)
);
GO

-- =============================================
-- Tạo bảng UserProfiles (Hồ sơ cá nhân)
-- =============================================
CREATE TABLE UserProfiles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL UNIQUE,
    Avatar NVARCHAR(500),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    MaritalStatus NVARCHAR(20),
    Education NVARCHAR(100),
    Occupation NVARCHAR(100),
    Company NVARCHAR(200),
    EmergencyContact NVARCHAR(100),
    EmergencyPhone NVARCHAR(20),
    BloodType NVARCHAR(5),
    Allergies NVARCHAR(500),
    MedicalConditions NVARCHAR(500),
    Interests NVARCHAR(500),
    Skills NVARCHAR(500),
    Languages NVARCHAR(200),
    SocialMedia NVARCHAR(500),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
GO

-- =============================================
-- Tạo bảng AuditLogs (Nhật ký hoạt động)
-- =============================================
CREATE TABLE AuditLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    Action NVARCHAR(50) NOT NULL,
    TableName NVARCHAR(50) NOT NULL,
    RecordId INT,
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX),
    IpAddress NVARCHAR(45),
    UserAgent NVARCHAR(500),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
GO

-- =============================================
-- Tạo Indexes để tối ưu hiệu suất
-- =============================================

-- Index cho Users
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_PhoneNumber ON Users(PhoneNumber);

-- Index cho BusinessRegistrations
CREATE INDEX IX_BusinessRegistrations_UserId ON BusinessRegistrations(UserId);
CREATE INDEX IX_BusinessRegistrations_Status ON BusinessRegistrations(Status);
CREATE INDEX IX_BusinessRegistrations_CreatedAt ON BusinessRegistrations(CreatedAt);

-- Index cho BusinessLicenses
CREATE INDEX IX_BusinessLicenses_UserId ON BusinessLicenses(UserId);
CREATE INDEX IX_BusinessLicenses_Status ON BusinessLicenses(Status);
CREATE INDEX IX_BusinessLicenses_CreatedAt ON BusinessLicenses(CreatedAt);

-- Index cho Businesses
CREATE INDEX IX_Businesses_BusinessCode ON Businesses(BusinessCode);
CREATE INDEX IX_Businesses_BusinessName ON Businesses(BusinessName);
CREATE INDEX IX_Businesses_Status ON Businesses(Status);
CREATE INDEX IX_Businesses_Province ON Businesses(Province);

-- Index cho UserProfiles
CREATE INDEX IX_UserProfiles_UserId ON UserProfiles(UserId);

-- Index cho AuditLogs
CREATE INDEX IX_AuditLogs_UserId ON AuditLogs(UserId);
CREATE INDEX IX_AuditLogs_Action ON AuditLogs(Action);
CREATE INDEX IX_AuditLogs_CreatedAt ON AuditLogs(CreatedAt);

-- =============================================
-- Tạo Stored Procedures
-- =============================================

-- SP để tìm kiếm doanh nghiệp
CREATE PROCEDURE sp_SearchBusinesses
    @BusinessCode NVARCHAR(13) = NULL,
    @BusinessName NVARCHAR(200) = NULL,
    @Address NVARCHAR(500) = NULL,
    @Province NVARCHAR(50) = NULL,
    @Status NVARCHAR(20) = NULL
AS
BEGIN
    SELECT * FROM Businesses 
    WHERE (@BusinessCode IS NULL OR BusinessCode LIKE '%' + @BusinessCode + '%')
        AND (@BusinessName IS NULL OR BusinessName LIKE '%' + @BusinessName + '%')
        AND (@Address IS NULL OR Address LIKE '%' + @Address + '%')
        AND (@Province IS NULL OR Province LIKE '%' + @Province + '%')
        AND (@Status IS NULL OR Status = @Status)
    ORDER BY CreatedAt DESC;
END
GO

-- SP để lấy thống kê
CREATE PROCEDURE sp_GetStatistics
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS TotalUsers,
        (SELECT COUNT(*) FROM Businesses WHERE Status = 'Đang hoạt động') AS ActiveBusinesses,
        (SELECT COUNT(*) FROM BusinessRegistrations WHERE Status = 'Chờ duyệt') AS PendingRegistrations,
        (SELECT COUNT(*) FROM BusinessLicenses WHERE Status = 'Chờ duyệt') AS PendingLicenses;
END
GO

-- SP để cập nhật trạng thái đăng ký
CREATE PROCEDURE sp_UpdateRegistrationStatus
    @RegistrationId INT,
    @Status NVARCHAR(20),
    @ApprovedBy NVARCHAR(50),
    @TableName NVARCHAR(20) -- 'BusinessRegistrations' hoặc 'BusinessLicenses'
AS
BEGIN
    IF @TableName = 'BusinessRegistrations'
    BEGIN
        UPDATE BusinessRegistrations 
        SET Status = @Status, 
            ApprovedAt = CASE WHEN @Status = 'Đã duyệt' THEN GETDATE() ELSE NULL END,
            ApprovedBy = @ApprovedBy,
            UpdatedAt = GETDATE()
        WHERE Id = @RegistrationId;
    END
    ELSE IF @TableName = 'BusinessLicenses'
    BEGIN
        UPDATE BusinessLicenses 
        SET Status = @Status, 
            ApprovedAt = CASE WHEN @Status = 'Đã duyệt' THEN GETDATE() ELSE NULL END,
            ApprovedBy = @ApprovedBy,
            UpdatedAt = GETDATE()
        WHERE Id = @RegistrationId;
    END
END
GO

-- =============================================
-- Tạo Triggers
-- =============================================

-- Trigger để tự động tạo BusinessCode khi duyệt đăng ký doanh nghiệp
CREATE TRIGGER tr_BusinessRegistrations_Approved
ON BusinessRegistrations
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Status)
    BEGIN
        INSERT INTO Businesses (
            BusinessCode, BusinessName, BusinessType, BusinessLine, Address,
            Province, District, Ward, PhoneNumber, Email, Website,
            CharterCapital, CapitalUnit, Representative, Position,
            EstablishmentDate, LicenseDate, LicenseNumber, Status,
            CreatedAt, CreatedBy
        )
        SELECT 
            'DN' + RIGHT('000000000' + CAST(i.Id AS VARCHAR(9)), 9),
            i.BusinessName, i.BusinessType, i.BusinessLine, i.Address,
            i.Province, i.District, i.Ward, i.PhoneNumber, i.Email, i.Website,
            i.CharterCapital, i.CapitalUnit, i.Representative, i.Position,
            i.EstablishmentDate, GETDATE(), 'GP' + RIGHT('000000' + CAST(i.Id AS VARCHAR(6)), 6),
            'Đang hoạt động', GETDATE(), i.ApprovedBy
        FROM inserted i
        INNER JOIN deleted d ON i.Id = d.Id
        WHERE i.Status = 'Đã duyệt' AND d.Status != 'Đã duyệt';
    END
END
GO

-- Trigger để ghi log thay đổi
CREATE TRIGGER tr_Businesses_Audit
ON Businesses
AFTER UPDATE
AS
BEGIN
    INSERT INTO AuditLogs (UserId, Action, TableName, RecordId, OldValues, NewValues)
    SELECT 
        NULL, -- Có thể lấy từ context
        'UPDATE',
        'Businesses',
        i.Id,
        (SELECT * FROM deleted WHERE Id = i.Id FOR JSON PATH),
        (SELECT * FROM inserted WHERE Id = i.Id FOR JSON PATH)
    FROM inserted i;
END
GO

-- =============================================
-- Tạo dữ liệu mẫu
-- =============================================

-- Thêm user admin
INSERT INTO Users (Username, Email, Password, FullName, PhoneNumber, Age, CurrentAddress, PermanentAddress, IdCardNumber, Hometown, TaxCode, IsActive)
VALUES ('admin', 'admin@example.com', '123456', N'Nguyễn Văn Admin', '0123456789', 30, 
        N'123 Đường ABC, Quận 1, TP.HCM', N'456 Đường XYZ, Quận 3, TP.HCM', 
        '123456789012', N'TP.HCM', '1234567890', 1);

-- Thêm doanh nghiệp mẫu
INSERT INTO Businesses (BusinessCode, BusinessName, EnglishName, ShortName, BusinessType, BusinessLine, Address, Province, District, Ward, PhoneNumber, Email, Website, CharterCapital, CapitalUnit, EstablishmentDate, LicenseDate, LicenseNumber, Representative, Position, Status, CreatedAt, CreatedBy)
VALUES 
('DN000000001', N'CÔNG TY TNHH ABC', 'ABC COMPANY LIMITED', 'ABC', N'Công ty TNHH', N'Thương mại điện tử', N'123 Đường Nguyễn Huệ, Quận 1', N'TP.HCM', N'Quận 1', N'Phường Bến Nghé', '028-12345678', 'info@abc.com.vn', 'www.abc.com.vn', 10000000000, 'VND', '2020-01-15', '2020-01-20', 'GP123456', N'Nguyễn Văn A', N'Giám đốc', N'Đang hoạt động', GETDATE(), 'admin'),
('DN000000002', N'CÔNG TY CỔ PHẦN XYZ', 'XYZ JOINT STOCK COMPANY', 'XYZ', N'Công ty cổ phần', N'Công nghệ thông tin', N'456 Đường Lê Lợi, Quận 3', N'TP.HCM', N'Quận 3', N'Phường Bến Thành', '028-87654321', 'contact@xyz.com.vn', 'www.xyz.com.vn', 50000000000, 'VND', '2019-06-10', '2019-06-15', 'GP654321', N'Trần Thị B', N'Chủ tịch HĐQT', N'Đang hoạt động', GETDATE(), 'admin'),
('DN000000003', N'DOANH NGHIỆP TƯ NHÂN DEF', 'DEF PRIVATE ENTERPRISE', 'DEF', N'Doanh nghiệp tư nhân', N'Dịch vụ tài chính', N'789 Đường Trần Hưng Đạo, Quận 5', N'TP.HCM', N'Quận 5', N'Phường 1', '028-11223344', 'info@def.com.vn', 'www.def.com.vn', 20000000000, 'VND', '2021-03-20', '2021-03-25', 'GP112233', N'Lê Văn C', N'Chủ doanh nghiệp', N'Tạm ngừng hoạt động', GETDATE(), 'admin');

-- Thêm đăng ký doanh nghiệp mẫu
INSERT INTO BusinessRegistrations (UserId, BusinessName, BusinessType, BusinessLine, Address, Province, District, Ward, PhoneNumber, Email, Website, CharterCapital, CapitalUnit, Representative, Position, IdNumber, EstablishmentDate, Status, Notes)
VALUES 
(1, N'CÔNG TY TNHH GHI', N'Công ty TNHH', N'Thương mại', N'321 Đường Võ Văn Tần, Quận 3', N'TP.HCM', N'Quận 3', N'Phường 6', '028-98765432', 'info@ghi.com.vn', 'www.ghi.com.vn', 15000000000, 'VND', N'Phạm Văn D', N'Giám đốc', '987654321098', '2023-05-10', N'Chờ duyệt', N'Đăng ký mới');

-- Thêm đăng ký kinh doanh mẫu
INSERT INTO BusinessLicenses (UserId, BusinessName, BusinessType, BusinessLine, Address, Province, District, Ward, PhoneNumber, Email, Website, Representative, Position, IdNumber, EstablishmentDate, Status, Notes)
VALUES 
(1, N'HỘ KINH DOANH JKL', N'Hộ kinh doanh', N'Bán lẻ', N'654 Đường 3/2, Quận 10', N'TP.HCM', N'Quận 10', N'Phường 15', '028-11223344', 'jkl@example.com', '', N'Hoàng Thị E', N'Chủ hộ', '112233445566', '2023-08-15', N'Chờ duyệt', N'Đăng ký hộ kinh doanh');

PRINT N'Database DoanhNghiepPortal đã được tạo thành công!';
PRINT N'Các bảng đã được tạo:';
PRINT N'- Users (Người dùng)';
PRINT N'- BusinessRegistrations (Đăng ký doanh nghiệp)';
PRINT N'- BusinessLicenses (Đăng ký kinh doanh)';
PRINT N'- Businesses (Doanh nghiệp đã duyệt)';
PRINT N'- UserProfiles (Hồ sơ cá nhân)';
PRINT N'- AuditLogs (Nhật ký hoạt động)';
PRINT N'Dữ liệu mẫu đã được thêm vào!';
GO 