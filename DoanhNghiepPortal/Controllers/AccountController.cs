using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DoanhNghiepPortal.Models;
using DoanhNghiepPortal.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DoanhNghiepPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Cập nhật thời gian đăng nhập cuối
                    user.LastLoginAt = DateTime.Now;
                    await _context.SaveChangesAsync();

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("FullName", user.FullName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
                    };

                    await HttpContext.SignInAsync(
                        "Cookies",
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
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

                    // Tạo user mới với thông tin cơ bản
                    var newUser = new UserModel
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Password = model.Password, // Trong thực tế nên hash password
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };

                    // Lưu vào database
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    // Tự động đăng nhập sau khi đăng ký
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, newUser.Username),
                        new Claim(ClaimTypes.Email, newUser.Email),
                        new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()),
                        new Claim("FullName", newUser.FullName),
                        new Claim(ClaimTypes.Role, newUser.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
                    };

                    await HttpContext.SignInAsync(
                        "Cookies",
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng cập nhật thông tin hồ sơ chi tiết.";
                    return RedirectToAction("Profile", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi đăng ký. Vui lòng thử lại.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous] // Tạm thời cho phép truy cập không cần đăng nhập để test
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var username = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            
            // Nếu chưa đăng nhập, lấy user mặc định để test
            if (user == null)
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Id == 1);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
            }

            return View(user);
        }

        [AllowAnonymous] // Tạm thời cho phép truy cập không cần đăng nhập để test
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var username = User.Identity?.Name;
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    
                    // Nếu chưa đăng nhập, sử dụng user có Id từ model hoặc user mặc định
                    if (user == null)
                    {
                        user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
                        if (user == null)
                        {
                            user = await _context.Users.FirstOrDefaultAsync(u => u.Id == 1);
                        }
                    }
                    
                    if (user != null)
                    {
                        // Cập nhật thông tin chi tiết
                        user.FullName = model.FullName;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;
                        user.Age = model.Age;
                        user.CurrentAddress = model.CurrentAddress;
                        user.PermanentAddress = model.PermanentAddress;
                        user.IdCardNumber = model.IdCardNumber;
                        user.Hometown = model.Hometown;
                        user.TaxCode = model.TaxCode;
                        user.UpdatedAt = DateTime.Now;

                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "✅ Cập nhật thông tin thành công! Dữ liệu đã được lưu vào hệ thống.";
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.";
                        return RedirectToAction("Login");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật thông tin. Vui lòng thử lại sau.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Thông tin không hợp lệ. Vui lòng kiểm tra lại các trường đã nhập.";
            }

            return RedirectToAction("Profile");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var username = User.Identity?.Name;
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    
                    if (user != null)
                    {
                        // Kiểm tra mật khẩu hiện tại
                        if (user.Password != model.CurrentPassword)
                        {
                            ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
                            return View(model);
                        }

                        // Cập nhật mật khẩu mới
                        user.Password = model.NewPassword;
                        user.UpdatedAt = DateTime.Now;
                        
                        await _context.SaveChangesAsync();
                        
                        TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                        return RedirectToAction("Profile");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi đổi mật khẩu. Vui lòng thử lại.");
                }
            }

            return View(model);
        }
    }
} 