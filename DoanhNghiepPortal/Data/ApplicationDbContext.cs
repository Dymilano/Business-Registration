using Microsoft.EntityFrameworkCore;
using DoanhNghiepPortal.Models;

namespace DoanhNghiepPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BusinessModel> Businesses { get; set; }
        public DbSet<BusinessRegistrationModel> BusinessRegistrations { get; set; }
        public DbSet<BusinessLicenseModel> BusinessLicenses { get; set; }
        public DbSet<ArticleModel> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình UserModel
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.CurrentAddress).HasMaxLength(500);
                entity.Property(e => e.PermanentAddress).HasMaxLength(500);
                entity.Property(e => e.IdCardNumber).HasMaxLength(12);
                entity.Property(e => e.Hometown).HasMaxLength(100);
                entity.Property(e => e.TaxCode).HasMaxLength(13);
                
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Cấu hình BusinessModel
            modelBuilder.Entity<BusinessModel>(entity =>
            {
                entity.ToTable("Businesses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BusinessCode).IsRequired().HasMaxLength(13);
                entity.Property(e => e.BusinessName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.EnglishName).HasMaxLength(200);
                entity.Property(e => e.ShortName).HasMaxLength(50);
                entity.Property(e => e.BusinessType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BusinessLine).HasMaxLength(200);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Province).HasMaxLength(50);
                entity.Property(e => e.District).HasMaxLength(50);
                entity.Property(e => e.Ward).HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Website).HasMaxLength(200);
                entity.Property(e => e.CharterCapital).HasColumnType("decimal(18,2)");
                entity.Property(e => e.CapitalUnit).HasMaxLength(10);
                entity.Property(e => e.LicenseNumber).HasMaxLength(50);
                entity.Property(e => e.Representative).HasMaxLength(100);
                entity.Property(e => e.Position).HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.Property(e => e.CreatedBy).HasMaxLength(50);
                entity.Property(e => e.UpdatedBy).HasMaxLength(50);
                
                entity.HasIndex(e => e.BusinessCode).IsUnique();
                entity.HasIndex(e => e.BusinessName);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.Province);
            });

            // Cấu hình BusinessRegistrationModel
            modelBuilder.Entity<BusinessRegistrationModel>(entity =>
            {
                entity.ToTable("BusinessRegistrations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BusinessName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.BusinessType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BusinessLine).HasMaxLength(200);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Province).HasMaxLength(50);
                entity.Property(e => e.District).HasMaxLength(50);
                entity.Property(e => e.Ward).HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Website).HasMaxLength(200);
                entity.Property(e => e.CharterCapital).HasColumnType("decimal(18,2)");
                entity.Property(e => e.CapitalUnit).HasMaxLength(10);
                entity.Property(e => e.Representative).HasMaxLength(100);
                entity.Property(e => e.Position).HasMaxLength(50);
                entity.Property(e => e.IdNumber).HasMaxLength(12);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.Property(e => e.ApprovedBy).HasMaxLength(50);
                
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CreatedAt);
                
                // Relationship với User
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình BusinessLicenseModel
            modelBuilder.Entity<BusinessLicenseModel>(entity =>
            {
                entity.ToTable("BusinessLicenses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BusinessName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.BusinessType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BusinessLine).HasMaxLength(200);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Province).HasMaxLength(50);
                entity.Property(e => e.District).HasMaxLength(50);
                entity.Property(e => e.Ward).HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Website).HasMaxLength(200);
                entity.Property(e => e.Representative).HasMaxLength(100);
                entity.Property(e => e.Position).HasMaxLength(50);
                entity.Property(e => e.IdNumber).HasMaxLength(12);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.Property(e => e.ApprovedBy).HasMaxLength(50);
                
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CreatedAt);
                
                // Relationship với User
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình ArticleModel
            modelBuilder.Entity<ArticleModel>(entity =>
            {
                entity.ToTable("Articles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Summary).HasMaxLength(500);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Author).HasMaxLength(100);
                entity.Property(e => e.Status).HasMaxLength(20);
                
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.Category);
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.IsFeatured);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "123456",
                    FullName = "Nguyễn Văn Admin",
                    PhoneNumber = "0123456789",
                    Age = 30,
                    CurrentAddress = "123 Đường ABC, Quận 1, TP.HCM",
                    PermanentAddress = "456 Đường XYZ, Quận 3, TP.HCM",
                    IdCardNumber = "123456789012",
                    Hometown = "TP.HCM",
                    TaxCode = "1234567890",
                    IsActive = true,
                    CreatedAt = new DateTime(2023, 1, 1)
                }
            );

            // Seed Businesses
            modelBuilder.Entity<BusinessModel>().HasData(
                new BusinessModel
                {
                    Id = 1,
                    BusinessCode = "DN000000001",
                    BusinessName = "CÔNG TY TNHH ABC",
                    EnglishName = "ABC COMPANY LIMITED",
                    ShortName = "ABC",
                    BusinessType = "Công ty TNHH",
                    BusinessLine = "Thương mại điện tử",
                    Address = "123 Đường Nguyễn Huệ, Quận 1",
                    Province = "TP.HCM",
                    District = "Quận 1",
                    Ward = "Phường Bến Nghé",
                    PhoneNumber = "028-12345678",
                    Email = "info@abc.com.vn",
                    Website = "www.abc.com.vn",
                    CharterCapital = 10000000000,
                    CapitalUnit = "VND",
                    EstablishmentDate = new DateTime(2020, 1, 15),
                    LicenseDate = new DateTime(2020, 1, 20),
                    LicenseNumber = "GP123456",
                    Representative = "Nguyễn Văn A",
                    Position = "Giám đốc",
                    Status = "Đang hoạt động",
                    CreatedAt = new DateTime(2023, 1, 1),
                    CreatedBy = "admin"
                },
                new BusinessModel
                {
                    Id = 2,
                    BusinessCode = "DN000000002",
                    BusinessName = "CÔNG TY CỔ PHẦN XYZ",
                    EnglishName = "XYZ JOINT STOCK COMPANY",
                    ShortName = "XYZ",
                    BusinessType = "Công ty cổ phần",
                    BusinessLine = "Công nghệ thông tin",
                    Address = "456 Đường Lê Lợi, Quận 3",
                    Province = "TP.HCM",
                    District = "Quận 3",
                    Ward = "Phường Bến Thành",
                    PhoneNumber = "028-87654321",
                    Email = "contact@xyz.com.vn",
                    Website = "www.xyz.com.vn",
                    CharterCapital = 50000000000,
                    CapitalUnit = "VND",
                    EstablishmentDate = new DateTime(2019, 6, 10),
                    LicenseDate = new DateTime(2019, 6, 15),
                    LicenseNumber = "GP654321",
                    Representative = "Trần Thị B",
                    Position = "Chủ tịch HĐQT",
                    Status = "Đang hoạt động",
                    CreatedAt = new DateTime(2022, 6, 1),
                    CreatedBy = "admin"
                },
                new BusinessModel
                {
                    Id = 3,
                    BusinessCode = "DN000000003",
                    BusinessName = "DOANH NGHIỆP TƯ NHÂN DEF",
                    EnglishName = "DEF PRIVATE ENTERPRISE",
                    ShortName = "DEF",
                    BusinessType = "Doanh nghiệp tư nhân",
                    BusinessLine = "Dịch vụ tài chính",
                    Address = "789 Đường Trần Hưng Đạo, Quận 5",
                    Province = "TP.HCM",
                    District = "Quận 5",
                    Ward = "Phường 1",
                    PhoneNumber = "028-11223344",
                    Email = "info@def.com.vn",
                    Website = "www.def.com.vn",
                    CharterCapital = 20000000000,
                    CapitalUnit = "VND",
                    EstablishmentDate = new DateTime(2021, 3, 20),
                    LicenseDate = new DateTime(2021, 3, 25),
                    LicenseNumber = "GP112233",
                    Representative = "Lê Văn C",
                    Position = "Chủ doanh nghiệp",
                    Status = "Tạm ngừng hoạt động",
                    CreatedAt = new DateTime(2023, 6, 1),
                    CreatedBy = "admin"
                }
            );
        }
    }
} 