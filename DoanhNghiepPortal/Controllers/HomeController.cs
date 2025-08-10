using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoanhNghiepPortal.Models;
using DoanhNghiepPortal.Data;

namespace DoanhNghiepPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        ViewData["Title"] = "Giới Thiệu";
        return View();
    }

    public IActionResult Guide()
    {
        ViewData["Title"] = "Hướng Dẫn";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AgencyOffices()
    {
        ViewData["Title"] = "Cơ quan Đăng ký kinh doanh các tỉnh, thành phố";
        return View();
    }

    public IActionResult NationalPortal()
    {
        ViewData["Title"] = "Cổng thông tin quốc gia về đăng ký doanh nghiệp";
        return View();
    }

    public IActionResult PrivateEnterprise()
    {
        ViewData["Title"] = "Cục Phát triển doanh nghiệp tư nhân và kinh tập thể";
        return View();
    }

    public IActionResult InfoCenter()
    {
        ViewData["Title"] = "Trung tâm Hỗ trợ đăng ký kinh doanh";
        return View();
    }

    public IActionResult Vision()
    {
        ViewData["Title"] = "Tầm nhìn - Sứ mệnh - Giá trị";
        return View();
    }

    public IActionResult Reform()
    {
        ViewData["Title"] = "Cải cách đăng ký kinh doanh";
        return View();
    }

    public async Task<IActionResult> News(string category, string search)
    {
        ViewData["Title"] = "Tin Tức";
        
        try
        {
            var query = _context.Articles.AsQueryable();

            // Lọc theo danh mục
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(a => a.Category == category);
            }

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Title.Contains(search) || a.Content.Contains(search));
            }

            // Chỉ lấy bài viết đã xuất bản
            query = query.Where(a => a.Status == "Published");

            var articles = await query.OrderByDescending(a => a.CreatedAt).ToListAsync();

            ViewBag.Category = category;
            ViewBag.Search = search;

            return View(articles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi tải trang tin tức");
            return View(new List<ArticleModel>());
        }
    }

    public async Task<IActionResult> ArticleDetail(int id)
    {
        try
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            // Tăng lượt xem
            article.ViewCount++;
            await _context.SaveChangesAsync();

            ViewData["Title"] = article.Title;
            return View(article);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi tải chi tiết bài viết");
            return NotFound();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
