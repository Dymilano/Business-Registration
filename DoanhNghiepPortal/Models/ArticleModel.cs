using System.ComponentModel.DataAnnotations;

namespace DoanhNghiepPortal.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề bài viết là bắt buộc")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nội dung bài viết là bắt buộc")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Mô tả ngắn")]
        public string Summary { get; set; } = string.Empty;

        [Display(Name = "Hình ảnh")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Danh mục")]
        public string Category { get; set; } = string.Empty;

        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Draft"; // Draft, Published, Archived

        [Display(Name = "Hiển thị trang chủ")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Thứ tự hiển thị")]
        public int DisplayOrder { get; set; } = 0;

        [Display(Name = "Lượt xem")]
        public int ViewCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
} 