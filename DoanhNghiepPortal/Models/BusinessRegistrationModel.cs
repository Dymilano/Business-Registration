using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoanhNghiepPortal.Models
{
    public class BusinessRegistrationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        // Navigation property
        public virtual UserModel? User { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên doanh nghiệp")]
        [Display(Name = "Tên Doanh Nghiệp")]
        public string BusinessName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn loại hình doanh nghiệp")]
        [Display(Name = "Loại Hình Doanh Nghiệp")]
        public string BusinessType { get; set; } = string.Empty;

        [Display(Name = "Ngành Nghề Kinh Doanh")]
        public string BusinessLine { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ trụ sở")]
        [Display(Name = "Địa Chỉ Trụ Sở")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Tỉnh/Thành phố")]
        public string Province { get; set; } = string.Empty;

        [Display(Name = "Quận/Huyện")]
        public string District { get; set; } = string.Empty;

        [Display(Name = "Phường/Xã")]
        public string Ward { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Số Điện Thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Website (nếu có)")]
        public string Website { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập vốn điều lệ")]
        [Display(Name = "Vốn Điều Lệ")]
        [Range(0, double.MaxValue, ErrorMessage = "Vốn điều lệ không được âm")]
        public decimal CharterCapital { get; set; }

        [Display(Name = "Đơn vị vốn")]
        public string CapitalUnit { get; set; } = "VND";

        [Required(ErrorMessage = "Vui lòng nhập tên người đại diện")]
        [Display(Name = "Tên Người Đại Diện")]
        public string Representative { get; set; } = string.Empty;

        [Display(Name = "Chức vụ")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số CMND/CCCD")]
        [Display(Name = "Số CMND/CCCD")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Số CMND/CCCD phải có 12 chữ số")]
        public string IdNumber { get; set; } = string.Empty;

        [Display(Name = "Ngày thành lập")]
        [DataType(DataType.Date)]
        public DateTime? EstablishmentDate { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Chờ duyệt";

        [Display(Name = "Ghi Chú")]
        public string Notes { get; set; } = string.Empty;

        [Display(Name = "Người duyệt")]
        public string ApprovedBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
} 