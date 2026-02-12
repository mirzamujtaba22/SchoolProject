using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Application.student;
using SchoolManagment.UI.ViewModels; // Add this using directive for LoginViewModel

namespace SchoolManagment.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = (await _unitOfWork.Users.GetAllAsync())
                        .FirstOrDefault(u => u.Email == model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                model.ErrorMessage = "Invalid email or password";
                return View(model);
            }

            // Role check (Admin for now)
            if (user.Role != "Admin")
            {
                model.ErrorMessage = "Only Admin can login";
                return View(model);
            }

            // TODO: Set Session / Claims / Cookie
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}

