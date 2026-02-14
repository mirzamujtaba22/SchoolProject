using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SchoolManagment.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (User?.Identity?.Name is not null)
            {
                ViewBag.Email = User.Identity.Name;
            }
            else
            {
                ViewBag.Email = string.Empty;
            }

            ViewBag.Role = User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
            ViewBag.FullName = User?.FindFirst("FullName")?.Value ?? string.Empty;

            return View();
        }
    }
}
