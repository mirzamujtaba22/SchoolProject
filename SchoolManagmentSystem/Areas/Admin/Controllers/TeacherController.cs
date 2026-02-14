using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Domain.Entities;
using SchoolManagment.UI.Areas.Admin.ViewModel.TeacherViewModel;

namespace SchoolManagment.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ================= LIST =================
        public async Task<IActionResult> Index()
        {
            var teachers = (await _unitOfWork.Users.GetAllAsync())
                .Where(u => u.Role == "Teacher" && u.IsActive);

            return View(teachers);
        }

        // ================= CREATE GET =================
        public IActionResult Create()
        {
            return View();
        }

        // ================= CREATE POST =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeacherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Role = "Teacher",
                IsActive = true,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
