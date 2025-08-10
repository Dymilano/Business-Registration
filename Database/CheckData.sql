-- =============================================
-- Script kiểm tra dữ liệu trong Database
-- =============================================

USE DoanhNghiepPortal;
GO

-- 1. Kiểm tra bảng Users (Thông tin đăng nhập/đăng ký)
PRINT N'=== KIỂM TRA BẢNG USERS ===';
SELECT 
    Id,
    Username,
    Email,
    FullName,
    PhoneNumber,
    Age,
    CurrentAddress,
    IdCardNumber,
    Hometown,
    TaxCode,
    IsActive,
    CreatedAt,
    LastLoginAt
FROM Users;

-- 2. Kiểm tra bảng Businesses (Doanh nghiệp đã duyệt)
PRINT N'=== KIỂM TRA BẢNG BUSINESSES ===';
SELECT 
    Id,
    BusinessCode,
    BusinessName,
    BusinessType,
    Address,
    Province,
    PhoneNumber,
    Email,
    Representative,
    Status,
    CreatedAt,
    CreatedBy
FROM Businesses;

-- 3. Kiểm tra bảng BusinessRegistrations (Đăng ký doanh nghiệp)
PRINT N'=== KIỂM TRA BẢNG BUSINESSREGISTRATIONS ===';
SELECT 
    Id,
    UserId,
    BusinessName,
    BusinessType,
    Address,
    Status,
    CreatedAt,
    ApprovedBy
FROM BusinessRegistrations;

-- 4. Kiểm tra bảng BusinessLicenses (Đăng ký kinh doanh)
PRINT N'=== KIỂM TRA BẢNG BUSINESSLICENSES ===';
SELECT 
    Id,
    UserId,
    BusinessName,
    BusinessType,
    Address,
    Status,
    CreatedAt,
    ApprovedBy
FROM BusinessLicenses;

-- 5. Thống kê tổng quan
PRINT N'=== THỐNG KÊ TỔNG QUAN ===';
SELECT 
    (SELECT COUNT(*) FROM Users) AS TotalUsers,
    (SELECT COUNT(*) FROM Businesses) AS TotalBusinesses,
    (SELECT COUNT(*) FROM BusinessRegistrations) AS TotalRegistrations,
    (SELECT COUNT(*) FROM BusinessLicenses) AS TotalLicenses,
    (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS ActiveUsers,
    (SELECT COUNT(*) FROM Businesses WHERE Status = 'Đang hoạt động') AS ActiveBusinesses;

-- 6. Kiểm tra Indexes
PRINT N'=== KIỂM TRA INDEXES ===';
SELECT 
    t.name AS TableName,
    i.name AS IndexName,
    i.type_desc AS IndexType,
    c.name AS ColumnName
FROM sys.tables t
INNER JOIN sys.indexes i ON t.object_id = i.object_id
INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE t.name IN ('Users', 'Businesses', 'BusinessRegistrations', 'BusinessLicenses')
ORDER BY t.name, i.name;

-- 7. Kiểm tra Foreign Keys
PRINT N'=== KIỂM TRA FOREIGN KEYS ===';
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS ColumnName,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTableName,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ReferencedColumnName
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
WHERE OBJECT_NAME(fk.parent_object_id) IN ('BusinessRegistrations', 'BusinessLicenses');

-- 8. Test kết nối và truy vấn
PRINT N'=== TEST KẾT NỐI ===';
SELECT 
    DB_NAME() AS DatabaseName,
    @@VERSION AS SQLServerVersion,
    GETDATE() AS CurrentDateTime,
    'Kết nối thành công!' AS Status; 