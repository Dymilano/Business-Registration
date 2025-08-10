using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DoanhNghiepPortal.Models;

namespace DoanhNghiepPortal.Controllers
{
    public class BusinessController : Controller
    {
        // Demo data - trong thực tế sẽ dùng database
        private static List<BusinessModel> _businesses = new List<BusinessModel>
        {
            new BusinessModel
            {
                Id = 1,
                BusinessCode = "0123456789",
                BusinessName = "CÔNG TY TNHH ABC",
                EnglishName = "ABC COMPANY LIMITED",
                ShortName = "ABC",
                Address = "123 Đường Nguyễn Huệ, Quận 1",
                Province = "TP.HCM",
                District = "Quận 1",
                Ward = "Phường Bến Nghé",
                PhoneNumber = "028-12345678",
                Email = "info@abc.com.vn",
                Website = "www.abc.com.vn",
                BusinessType = "Công ty TNHH",
                BusinessLine = "Thương mại điện tử",
                CharterCapital = 10000000000,
                CapitalUnit = "VND",
                EstablishmentDate = new DateTime(2020, 1, 15),
                LicenseDate = new DateTime(2020, 1, 20),
                LicenseNumber = "GP123456",
                Representative = "Nguyễn Văn A",
                Position = "Giám đốc",
                Status = "Đang hoạt động",
                CreatedAt = DateTime.Now.AddDays(-365),
                CreatedBy = "admin"
            },
            new BusinessModel
            {
                Id = 2,
                BusinessCode = "0987654321",
                BusinessName = "CÔNG TY CỔ PHẦN XYZ",
                EnglishName = "XYZ JOINT STOCK COMPANY",
                ShortName = "XYZ",
                Address = "456 Đường Lê Lợi, Quận 3",
                Province = "TP.HCM",
                District = "Quận 3",
                Ward = "Phường Bến Thành",
                PhoneNumber = "028-87654321",
                Email = "contact@xyz.com.vn",
                Website = "www.xyz.com.vn",
                BusinessType = "Công ty cổ phần",
                BusinessLine = "Công nghệ thông tin",
                CharterCapital = 50000000000,
                CapitalUnit = "VND",
                EstablishmentDate = new DateTime(2019, 6, 10),
                LicenseDate = new DateTime(2019, 6, 15),
                LicenseNumber = "GP654321",
                Representative = "Trần Thị B",
                Position = "Chủ tịch HĐQT",
                Status = "Đang hoạt động",
                CreatedAt = DateTime.Now.AddDays(-730),
                CreatedBy = "admin"
            },
            new BusinessModel
            {
                Id = 3,
                BusinessCode = "1122334455",
                BusinessName = "DOANH NGHIỆP TƯ NHÂN DEF",
                EnglishName = "DEF PRIVATE ENTERPRISE",
                ShortName = "DEF",
                Address = "789 Đường Trần Hưng Đạo, Quận 5",
                Province = "TP.HCM",
                District = "Quận 5",
                Ward = "Phường 1",
                PhoneNumber = "028-11223344",
                Email = "info@def.com.vn",
                Website = "www.def.com.vn",
                BusinessType = "Doanh nghiệp tư nhân",
                BusinessLine = "Dịch vụ tài chính",
                CharterCapital = 20000000000,
                CapitalUnit = "VND",
                EstablishmentDate = new DateTime(2021, 3, 20),
                LicenseDate = new DateTime(2021, 3, 25),
                LicenseNumber = "GP112233",
                Representative = "Lê Văn C",
                Position = "Chủ doanh nghiệp",
                Status = "Tạm ngừng hoạt động",
                CreatedAt = DateTime.Now.AddDays(-180),
                CreatedBy = "admin"
            }
        };

        [HttpGet]
        public IActionResult Search()
        {
            ViewData["Title"] = "Tra Cứu Doanh Nghiệp";
            
            // Thống kê
            ViewBag.TotalBusinesses = _businesses.Count;
            ViewBag.ActiveBusinesses = _businesses.Count(b => b.Status == "Đang hoạt động");
            
            return View(new BusinessSearchModel());
        }

        [HttpPost]
        public IActionResult Search(BusinessSearchModel model)
        {
            ViewData["Title"] = "Tra Cứu Doanh Nghiệp";
            
            var results = _businesses.AsQueryable();

            if (!string.IsNullOrEmpty(model.BusinessCode))
            {
                results = results.Where(b => b.BusinessCode.Contains(model.BusinessCode));
            }

            if (!string.IsNullOrEmpty(model.BusinessName))
            {
                results = results.Where(b => b.BusinessName.Contains(model.BusinessName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(model.Address))
            {
                results = results.Where(b => b.Address.Contains(model.Address, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(model.Province))
            {
                results = results.Where(b => b.Province.Contains(model.Province, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(model.Status))
            {
                results = results.Where(b => b.Status == model.Status);
            }

            ViewBag.SearchResults = results.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var business = _businesses.FirstOrDefault(b => b.Id == id);
            if (business == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"Chi Tiết Doanh Nghiệp - {business.BusinessName}";
            return View(business);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Thêm Doanh Nghiệp Mới";
            return View(new BusinessModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(BusinessModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mã số doanh nghiệp đã tồn tại
                if (_businesses.Any(b => b.BusinessCode == model.BusinessCode))
                {
                    ModelState.AddModelError("BusinessCode", "Mã số doanh nghiệp đã tồn tại.");
                    return View(model);
                }

                model.Id = _businesses.Count + 1;
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = User.Identity?.Name ?? "admin";
                model.Status = "Đang hoạt động";

                _businesses.Add(model);

                TempData["SuccessMessage"] = "Thêm doanh nghiệp thành công!";
                return RedirectToAction("Details", new { id = model.Id });
            }

            ViewData["Title"] = "Thêm Doanh Nghiệp Mới";
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var business = _businesses.FirstOrDefault(b => b.Id == id);
            if (business == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"Cập Nhật Doanh Nghiệp - {business.BusinessName}";
            return View(business);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, BusinessModel model)
        {
            if (ModelState.IsValid)
            {
                var existingBusiness = _businesses.FirstOrDefault(b => b.Id == id);
                if (existingBusiness == null)
                {
                    return NotFound();
                }

                // Kiểm tra mã số doanh nghiệp đã tồn tại (trừ chính nó)
                if (_businesses.Any(b => b.BusinessCode == model.BusinessCode && b.Id != id))
                {
                    ModelState.AddModelError("BusinessCode", "Mã số doanh nghiệp đã tồn tại.");
                    return View(model);
                }

                // Cập nhật thông tin
                existingBusiness.BusinessCode = model.BusinessCode;
                existingBusiness.BusinessName = model.BusinessName;
                existingBusiness.EnglishName = model.EnglishName;
                existingBusiness.ShortName = model.ShortName;
                existingBusiness.Address = model.Address;
                existingBusiness.Province = model.Province;
                existingBusiness.District = model.District;
                existingBusiness.Ward = model.Ward;
                existingBusiness.PhoneNumber = model.PhoneNumber;
                existingBusiness.Email = model.Email;
                existingBusiness.Website = model.Website;
                existingBusiness.BusinessType = model.BusinessType;
                existingBusiness.BusinessLine = model.BusinessLine;
                existingBusiness.CharterCapital = model.CharterCapital;
                existingBusiness.CapitalUnit = model.CapitalUnit;
                existingBusiness.EstablishmentDate = model.EstablishmentDate;
                existingBusiness.LicenseDate = model.LicenseDate;
                existingBusiness.LicenseNumber = model.LicenseNumber;
                existingBusiness.Representative = model.Representative;
                existingBusiness.Position = model.Position;
                existingBusiness.Status = model.Status;
                existingBusiness.Notes = model.Notes;
                existingBusiness.UpdatedAt = DateTime.Now;

                TempData["SuccessMessage"] = "Cập nhật doanh nghiệp thành công!";
                return RedirectToAction("Details", new { id = id });
            }

            ViewData["Title"] = $"Cập Nhật Doanh Nghiệp - {model.BusinessName}";
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var business = _businesses.FirstOrDefault(b => b.Id == id);
            if (business == null)
            {
                return NotFound();
            }

            _businesses.Remove(business);
            TempData["SuccessMessage"] = "Xóa doanh nghiệp thành công!";
            return RedirectToAction("Search");
        }
    }
} 