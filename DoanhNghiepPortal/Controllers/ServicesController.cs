using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DoanhNghiepPortal.Models;
using DoanhNghiepPortal.Data;
using System.Security.Claims;

namespace DoanhNghiepPortal.Controllers;

[Authorize]
public class ServicesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ServicesController(ApplicationDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    public IActionResult DangKyDoanhNghiep()
    {
        ViewData["Title"] = "Đăng Ký Doanh Nghiệp";
        return View();
    }

    [AllowAnonymous]
    public IActionResult DangKyKinhDoanh()
    {
        ViewData["Title"] = "Đăng Ký Kinh Doanh";
        return View();
    }

    [HttpPost]
    [AllowAnonymous] // Cho phép truy cập không cần đăng nhập
    public async Task<IActionResult> DangKyDoanhNghiep(BusinessRegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
            ViewData["Title"] = "Đăng Ký Doanh Nghiệp";
            return View(model);
        }

        try
        {
            // Sử dụng UserId mặc định (có thể thay đổi theo logic authentication của bạn)
            int userId = 1;

            // Tạo đối tượng BusinessRegistration
            var registration = new BusinessRegistrationModel
            {
                UserId = userId,
                BusinessName = model.BusinessName,
                BusinessType = model.BusinessType,
                BusinessLine = model.BusinessLine,
                Address = model.Address,
                Province = model.Province,
                District = model.District,
                Ward = model.Ward,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Website = model.Website,
                CharterCapital = model.CharterCapital,
                CapitalUnit = model.CapitalUnit,
                Representative = model.Representative,
                Position = model.Position,
                IdNumber = model.IdNumber,
                EstablishmentDate = model.EstablishmentDate,
                Status = "Chờ duyệt",
                Notes = model.Notes,
                CreatedAt = DateTime.Now
            };

            // Lưu vào database
            _context.BusinessRegistrations.Add(registration);
            await _context.SaveChangesAsync();

            // Lưu thông tin vào session để hiển thị ở trang Success
            HttpContext.Session.SetString("RegistrationType", "Doanh nghiệp");
            HttpContext.Session.SetString("BusinessName", model.BusinessName);
            HttpContext.Session.SetString("RegistrationId", registration.Id.ToString());
            HttpContext.Session.SetString("CreatedAt", registration.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"));

            return RedirectToAction("Success");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Có lỗi xảy ra khi lưu hồ sơ. Vui lòng thử lại.");
            ViewData["Title"] = "Đăng Ký Doanh Nghiệp";
            return View(model);
        }
    }

    [HttpPost]
    [AllowAnonymous] // Cho phép truy cập không cần đăng nhập
    public async Task<IActionResult> DangKyKinhDoanh(BusinessLicenseModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin đã nhập.");
            ViewData["Title"] = "Đăng Ký Kinh Doanh";
            return View(model);
        }

        try
        {
            // Sử dụng UserId mặc định (có thể thay đổi theo logic authentication của bạn)
            int userId = 1;

            // Tạo đối tượng BusinessLicense
            var license = new BusinessLicenseModel
            {
                UserId = userId,
                BusinessName = model.BusinessName,
                BusinessType = model.BusinessType,
                BusinessLine = model.BusinessLine,
                Address = model.Address,
                Province = model.Province,
                District = model.District,
                Ward = model.Ward,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Website = model.Website,
                Representative = model.Representative,
                Position = model.Position,
                IdNumber = model.IdNumber,
                EstablishmentDate = model.EstablishmentDate,
                Status = "Chờ duyệt",
                Notes = model.Notes,
                CreatedAt = DateTime.Now
            };

            // Lưu vào database
            _context.BusinessLicenses.Add(license);
            await _context.SaveChangesAsync();

            // Lưu thông tin vào session để hiển thị ở trang Success
            HttpContext.Session.SetString("RegistrationType", "Hộ kinh doanh");
            HttpContext.Session.SetString("BusinessName", model.BusinessName);
            HttpContext.Session.SetString("RegistrationId", license.Id.ToString());
            HttpContext.Session.SetString("CreatedAt", license.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"));

            return RedirectToAction("Success");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Có lỗi xảy ra khi lưu hồ sơ. Vui lòng thử lại.");
            ViewData["Title"] = "Đăng Ký Kinh Doanh";
            return View(model);
        }
    }

    public IActionResult Success()
    {
        ViewData["Title"] = "Đăng Ký Thành Công";
        
        // Lấy thông tin từ Session
        var registrationType = HttpContext.Session.GetString("RegistrationType");
        var businessName = HttpContext.Session.GetString("BusinessName");
        var registrationId = HttpContext.Session.GetString("RegistrationId");
        var createdAt = HttpContext.Session.GetString("CreatedAt");
        
        ViewData["RegistrationType"] = registrationType ?? "Hồ sơ";
        ViewData["BusinessName"] = businessName ?? "";
        ViewData["RegistrationId"] = registrationId ?? "";
        ViewData["CreatedAt"] = createdAt ?? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        
        // Xóa session sau khi đã lấy để tránh hiển thị lại
        HttpContext.Session.Remove("RegistrationType");
        HttpContext.Session.Remove("BusinessName");
        HttpContext.Session.Remove("RegistrationId");
        HttpContext.Session.Remove("CreatedAt");
        
        return View();
    }





    // API để cập nhật trạng thái hồ sơ
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateRegistrationStatus(int registrationId, string status, string type)
    {
        try
        {
            if (type == "business")
            {
                var registration = await _context.BusinessRegistrations.FindAsync(registrationId);
                if (registration != null)
                {
                    registration.Status = status;
                    registration.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }
            else if (type == "license")
            {
                var license = await _context.BusinessLicenses.FindAsync(registrationId);
                if (license != null)
                {
                    license.Status = status;
                    license.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }

            return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
        }
    }

    // API để lấy thông tin hồ sơ
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRegistrationInfo(int registrationId, string type)
    {
        try
        {
            if (type == "business")
            {
                var registration = await _context.BusinessRegistrations.FindAsync(registrationId);
                if (registration != null)
                {
                    return Json(new { 
                        success = true, 
                        data = new {
                            id = registration.Id,
                            businessName = registration.BusinessName,
                            status = registration.Status,
                            createdAt = registration.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                            updatedAt = registration.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss")
                        }
                    });
                }
            }
            else if (type == "license")
            {
                var license = await _context.BusinessLicenses.FindAsync(registrationId);
                if (license != null)
                {
                    return Json(new { 
                        success = true, 
                        data = new {
                            id = license.Id,
                            businessName = license.BusinessName,
                            status = license.Status,
                            createdAt = license.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                            updatedAt = license.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss")
                        }
                    });
                }
            }

            return Json(new { success = false, message = "Không tìm thấy hồ sơ" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
        }
    }
} 