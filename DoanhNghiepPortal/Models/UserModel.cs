using System.ComponentModel.DataAnnotations;

namespace DoanhNghiepPortal.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Tuổi")]
        [Range(18, 100, ErrorMessage = "Tuổi phải từ 18-100")]
        public int? Age { get; set; }

        [Display(Name = "Nơi ở hiện tại")]
        public string CurrentAddress { get; set; } = string.Empty;

        [Display(Name = "Địa chỉ thường trú")]
        public string PermanentAddress { get; set; } = string.Empty;

        [Display(Name = "Căn cước công dân")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Căn cước công dân phải có 12 số")]
        public string IdCardNumber { get; set; } = string.Empty;

        [Display(Name = "Quê quán")]
        public string Hometown { get; set; } = string.Empty;

        [Display(Name = "Mã số thuế")]
        [RegularExpression(@"^\d{10}|\d{13}$", ErrorMessage = "Mã số thuế phải có 10 hoặc 13 số")]
        public string TaxCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        
        [Display(Name = "Vai trò")]
        public string Role { get; set; } = "User"; // User, Admin
    }
} 