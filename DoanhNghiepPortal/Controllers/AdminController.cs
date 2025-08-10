using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DoanhNghiepPortal.Models;
using DoanhNghiepPortal.Data;
using System.Security.Claims;

namespace DoanhNghiepPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dashboard - chỉ Admin mới truy cập được
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                // Thống kê tổng quan
                var totalUsers = await _context.Users.CountAsync();
                var totalBusinessRegistrations = await _context.BusinessRegistrations.CountAsync();
                var totalBusinessLicenses = await _context.BusinessLicenses.CountAsync();
                var totalArticles = await _context.Articles.CountAsync();

                // Thống kê theo trạng thái
                var pendingRegistrations = await _context.BusinessRegistrations.CountAsync(r => r.Status == "Chờ duyệt");
                var pendingLicenses = await _context.BusinessLicenses.CountAsync(l => l.Status == "Chờ duyệt");
                var approvedRegistrations = await _context.BusinessRegistrations.CountAsync(r => r.Status == "Đã duyệt");
                var approvedLicenses = await _context.BusinessLicenses.CountAsync(l => l.Status == "Đã duyệt");

                ViewBag.TotalUsers = totalUsers;
                ViewBag.TotalBusinessRegistrations = totalBusinessRegistrations;
                ViewBag.TotalBusinessLicenses = totalBusinessLicenses;
                ViewBag.TotalArticles = totalArticles;
                ViewBag.PendingRegistrations = pendingRegistrations;
                ViewBag.PendingLicenses = pendingLicenses;
                ViewBag.ApprovedRegistrations = approvedRegistrations;
                ViewBag.ApprovedLicenses = approvedLicenses;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra khi tải dashboard: " + ex.Message;
                return View();
            }
        }

        // Quản lý hồ sơ đăng ký doanh nghiệp
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BusinessRegistrations(string status = "", string search = "")
        {
            try
            {
                var query = _context.BusinessRegistrations
                    .Include(r => r.User)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(r => r.Status == status);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(r => r.BusinessName.Contains(search) || 
                                           r.Representative.Contains(search) ||
                                           r.Email.Contains(search));
                }

                var registrations = await query
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                ViewBag.Status = status;
                ViewBag.Search = search;
                return View(registrations);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<BusinessRegistrationModel>());
            }
        }

        // Quản lý hồ sơ đăng ký kinh doanh
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BusinessLicenses(string status = "", string search = "")
        {
            try
            {
                var query = _context.BusinessLicenses
                    .Include(l => l.User)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(l => l.Status == status);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(l => l.BusinessName.Contains(search) || 
                                           l.Representative.Contains(search) ||
                                           l.Email.Contains(search));
                }

                var licenses = await query
                    .OrderByDescending(l => l.CreatedAt)
                    .ToListAsync();

                ViewBag.Status = status;
                ViewBag.Search = search;
                return View(licenses);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<BusinessLicenseModel>());
            }
        }

        // Quản lý người dùng
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users(string search = "")
        {
            try
            {
                var query = _context.Users.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(u => u.Username.Contains(search) || 
                                           u.FullName.Contains(search) ||
                                           u.Email.Contains(search));
                }

                var users = await query
                    .OrderByDescending(u => u.CreatedAt)
                    .ToListAsync();

                ViewBag.Search = search;
                return View(users);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<UserModel>());
            }
        }

        // Quản lý bài viết
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Articles(string status, string search)
        {
            try
            {
                var query = _context.Articles.AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(a => a.Status == status);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(a => a.Title.Contains(search) || a.Content.Contains(search));
                }

                var articles = await query.OrderByDescending(a => a.CreatedAt).ToListAsync();

                ViewBag.Status = status;
                ViewBag.Search = search;

                return View(articles);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra khi tải danh sách bài viết: " + ex.Message;
                return View(new List<ArticleModel>());
            }
        }

        // Tạo bài viết mới
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateArticle(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var article = new ArticleModel
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Author = model.Author,
                        Category = model.Category,
                        Status = model.Status ?? "Draft",
                        ViewCount = 0,
                        CreatedAt = DateTime.Now
                    };

                    _context.Articles.Add(article);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Tạo bài viết thành công!";
                    return RedirectToAction("Articles");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo bài viết: " + ex.Message);
                }
            }

            return View(model);
        }

        // Chỉnh sửa bài viết
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditArticle(int id, ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var article = await _context.Articles.FindAsync(id);
                    if (article != null)
                    {
                        article.Title = model.Title;
                        article.Content = model.Content;
                        article.Author = model.Author;
                        article.Category = model.Category;
                        article.Status = model.Status;
                        article.UpdatedAt = DateTime.Now;

                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Cập nhật bài viết thành công!";
                        return RedirectToAction("Articles");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật bài viết: " + ex.Message);
                }
            }

            return View(model);
        }

        // Chi tiết bài viết
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ArticleDetail(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // Xóa bài viết
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article != null)
                {
                    _context.Articles.Remove(article);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Xóa bài viết thành công" });
                }

                return Json(new { success = false, message = "Không tìm thấy bài viết" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }



        // Chi tiết hồ sơ đăng ký doanh nghiệp
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BusinessRegistrationDetail(int id)
        {
            try
            {
                var registration = await _context.BusinessRegistrations
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (registration == null)
                {
                    return NotFound();
                }

                return View(registration);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View();
            }
        }

        // Chi tiết hồ sơ đăng ký kinh doanh
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BusinessLicenseDetail(int id)
        {
            try
            {
                var license = await _context.BusinessLicenses
                    .Include(l => l.User)
                    .FirstOrDefaultAsync(l => l.Id == id);

                if (license == null)
                {
                    return NotFound();
                }

                return View(license);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View();
            }
        }

        // Xóa hồ sơ đăng ký doanh nghiệp
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBusinessRegistration(int id)
        {
            try
            {
                var registration = await _context.BusinessRegistrations.FindAsync(id);
                if (registration != null)
                {
                    _context.BusinessRegistrations.Remove(registration);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Xóa hồ sơ thành công" });
                }

                return Json(new { success = false, message = "Không tìm thấy hồ sơ" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Xóa hồ sơ đăng ký kinh doanh
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBusinessLicense(int id)
        {
            try
            {
                var license = await _context.BusinessLicenses.FindAsync(id);
                if (license != null)
                {
                    _context.BusinessLicenses.Remove(license);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Xóa hồ sơ thành công" });
                }

                return Json(new { success = false, message = "Không tìm thấy hồ sơ" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Xóa người dùng
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    // Không cho phép xóa admin
                    if (user.Role == "Admin")
                    {
                        return Json(new { success = false, message = "Không thể xóa tài khoản Admin" });
                    }

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Xóa người dùng thành công" });
                }

                return Json(new { success = false, message = "Không tìm thấy người dùng" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Thay đổi vai trò người dùng
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(int userId, string newRole)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    user.Role = newRole;
                    user.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Cập nhật vai trò thành công" });
                }

                return Json(new { success = false, message = "Không tìm thấy người dùng" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Tạo người dùng mới
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra username đã tồn tại chưa
                    if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                    {
                        ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                        return View(model);
                    }

                    // Kiểm tra email đã tồn tại chưa
                    if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                    {
                        ModelState.AddModelError("Email", "Email đã được sử dụng.");
                        return View(model);
                    }

                    // Tạo user mới
                    var newUser = new UserModel
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Password = model.Password,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Role = model.Role ?? "User",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Tạo người dùng thành công!";
                    return RedirectToAction("Users");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo người dùng: " + ex.Message);
                }
            }

            return View(model);
        }

        // Chỉnh sửa người dùng
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(int id, UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FindAsync(id);
                    if (user != null)
                    {
                        // Cập nhật thông tin
                        user.FullName = model.FullName;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;
                        user.Role = model.Role;
                        user.IsActive = model.IsActive;
                        user.UpdatedAt = DateTime.Now;

                        // Cập nhật mật khẩu nếu có
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            user.Password = model.Password;
                        }

                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Cập nhật người dùng thành công!";
                        return RedirectToAction("Users");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật người dùng: " + ex.Message);
                }
            }

            return View(model);
        }

        // Chi tiết người dùng
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserDetail(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Thống kê
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var stats = new
                {
                    TotalUsers = await _context.Users.CountAsync(),
                    TotalBusinessRegistrations = await _context.BusinessRegistrations.CountAsync(),
                    TotalBusinessLicenses = await _context.BusinessLicenses.CountAsync(),
                    TotalArticles = await _context.Articles.CountAsync(),
                    PendingRegistrations = await _context.BusinessRegistrations.CountAsync(r => r.Status == "Chờ duyệt"),
                    PendingLicenses = await _context.BusinessLicenses.CountAsync(l => l.Status == "Chờ duyệt"),
                    ApprovedRegistrations = await _context.BusinessRegistrations.CountAsync(r => r.Status == "Đã duyệt"),
                    ApprovedLicenses = await _context.BusinessLicenses.CountAsync(l => l.Status == "Đã duyệt")
                };

                return Json(new { success = true, data = stats });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Thống kê theo tháng
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMonthlyStats()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var monthlyStats = new List<object>();

                for (int month = 1; month <= 12; month++)
                {
                    var startDate = new DateTime(currentYear, month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    var registrations = await _context.BusinessRegistrations
                        .CountAsync(r => r.CreatedAt >= startDate && r.CreatedAt <= endDate);

                    var licenses = await _context.BusinessLicenses
                        .CountAsync(l => l.CreatedAt >= startDate && l.CreatedAt <= endDate);

                    var users = await _context.Users
                        .CountAsync(u => u.CreatedAt >= startDate && u.CreatedAt <= endDate);

                    monthlyStats.Add(new
                    {
                        Month = month,
                        MonthName = startDate.ToString("MMM"),
                        Registrations = registrations,
                        Licenses = licenses,
                        Users = users
                    });
                }

                return Json(new { success = true, data = monthlyStats });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Tạo Admin mặc định - không cần authorization
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        // API tạo Admin mặc định
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDefaultAdmin()
        {
            try
            {
                // Kiểm tra xem đã có admin chưa
                var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Role == "Admin");
                if (existingAdmin != null)
                {
                    return Json(new { 
                        success = false, 
                        message = "Đã có tài khoản Admin",
                        existingAdmin = new {
                            username = existingAdmin.Username,
                            email = existingAdmin.Email
                        }
                    });
                }

                // Tạo tài khoản Admin mặc định
                var adminUser = new UserModel
                {
                    Username = "admin",
                    Email = "admin@doanhnghiep.gov.vn",
                    Password = "admin123", // Trong thực tế nên hash password
                    FullName = "Quản trị viên",
                    PhoneNumber = "0123456789",
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    message = "Đã tạo tài khoản Admin thành công!",
                    credentials = new { 
                        username = "admin", 
                        password = "admin123" 
                    },
                    loginUrl = "/Account/Login"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // Tạo Admin và hiển thị thông tin (trang view)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SetupAdmin()
        {
            try
            {
                // Kiểm tra xem đã có admin chưa
                var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Role == "Admin");
                if (existingAdmin != null)
                {
                    ViewBag.AdminExists = true;
                    ViewBag.AdminInfo = new
                    {
                        Username = existingAdmin.Username,
                        Email = existingAdmin.Email,
                        FullName = existingAdmin.FullName
                    };
                }
                else
                {
                    // Kiểm tra xem có tài khoản admin nào chưa có role Admin không
                    var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "admin");
                    if (adminUser != null)
                    {
                        // Cập nhật role thành Admin
                        adminUser.Role = "Admin";
                        await _context.SaveChangesAsync();
                        
                        ViewBag.AdminExists = true;
                        ViewBag.AdminInfo = new
                        {
                            Username = adminUser.Username,
                            Email = adminUser.Email,
                            FullName = adminUser.FullName
                        };
                    }
                    else
                    {
                        // Tạo tài khoản Admin mới
                        var newAdminUser = new UserModel
                        {
                            Username = "admin",
                            Email = "admin@doanhnghiep.gov.vn",
                            Password = "admin123",
                            FullName = "Quản trị viên",
                            PhoneNumber = "0123456789",
                            Role = "Admin",
                            IsActive = true,
                            CreatedAt = DateTime.Now
                        };

                        _context.Users.Add(newAdminUser);
                        await _context.SaveChangesAsync();
                        
                        ViewBag.AdminExists = true;
                        ViewBag.AdminInfo = new
                        {
                            Username = newAdminUser.Username,
                            Email = newAdminUser.Email,
                            FullName = newAdminUser.FullName
                        };
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View();
            }
        }

        // Sửa lỗi database
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FixDatabase()
        {
            try
            {
                // Kiểm tra và tạo admin nếu chưa có
                var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Role == "Admin");
                if (existingAdmin == null)
                {
                    var adminUser = new UserModel
                    {
                        Username = "admin",
                        Email = "admin@doanhnghiep.gov.vn",
                        Password = "admin123",
                        FullName = "Quản trị viên",
                        PhoneNumber = "0123456789",
                        Role = "Admin",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    };

                    _context.Users.Add(adminUser);
                    await _context.SaveChangesAsync();
                }

                return Json(new { 
                    success = true, 
                    message = "Đã sửa lỗi database thành công!",
                    adminCredentials = new {
                        username = "admin",
                        password = "admin123"
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }
    }
} 