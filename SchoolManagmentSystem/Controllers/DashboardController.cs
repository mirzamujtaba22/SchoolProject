using Microsoft.AspNetCore.Mvc;

namespace SchoolManagment.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
         
            return View();
        }
    }
}
