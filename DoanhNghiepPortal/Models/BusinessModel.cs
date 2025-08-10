using System.ComponentModel.DataAnnotations;

namespace DoanhNghiepPortal.Models
{
    public class BusinessModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã số doanh nghiệp là bắt buộc")]
        [Display(Name = "Mã số doanh nghiệp")]
        [RegularExpression(@"^\d{10}|\d{13}$", ErrorMessage = "Mã số doanh nghiệp phải có 10 hoặc 13 số")]
        public string BusinessCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên doanh nghiệp là bắt buộc")]
        [Display(Name = "Tên doanh nghiệp")]
        public string BusinessName { get; set; } = string.Empty;

        [Display(Name = "Tên tiếng Anh")]
        public string EnglishName { get; set; } = string.Empty;

        [Display(Name = "Tên viết tắt")]
        public string ShortName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Địa chỉ trụ sở là bắt buộc")]
        [Display(Name = "Địa chỉ trụ sở")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Tỉnh/Thành phố")]
        public string Province { get; set; } = string.Empty;

        [Display(Name = "Quận/Huyện")]
        public string District { get; set; } = string.Empty;

        [Display(Name = "Phường/Xã")]
        public string Ward { get; set; } = string.Empty;

        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Website")]
        [Url(ErrorMessage = "Website không hợp lệ")]
        public string Website { get; set; } = string.Empty;

        [Display(Name = "Loại hình doanh nghiệp")]
        public string BusinessType { get; set; } = string.Empty;

        [Display(Name = "Ngành nghề kinh doanh")]
        public string BusinessLine { get; set; } = string.Empty;

        [Display(Name = "Vốn điều lệ")]
        public decimal? CharterCapital { get; set; }

        [Display(Name = "Đơn vị vốn")]
        public string CapitalUnit { get; set; } = string.Empty;

        [Display(Name = "Ngày thành lập")]
        [DataType(DataType.Date)]
        public DateTime? EstablishmentDate { get; set; }

        [Display(Name = "Ngày cấp giấy phép")]
        [DataType(DataType.Date)]
        public DateTime? LicenseDate { get; set; }

        [Display(Name = "Số giấy phép")]
        public string LicenseNumber { get; set; } = string.Empty;

        [Display(Name = "Người đại diện")]
        public string Representative { get; set; } = string.Empty;

        [Display(Name = "Chức vụ")]
        public string Position { get; set; } = string.Empty;

        [Display(Name = "Trạng thái hoạt động")]
        public string Status { get; set; } = "Đang hoạt động";

        [Display(Name = "Ghi chú")]
        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }

    public class BusinessSearchModel
    {
        [Display(Name = "Mã số doanh nghiệp")]
        public string? BusinessCode { get; set; }

        [Display(Name = "Tên doanh nghiệp")]
        public string? BusinessName { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Tỉnh/Thành phố")]
        public string? Province { get; set; }

        [Display(Name = "Trạng thái")]
        public string? Status { get; set; }
    }
} 