using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Domain.Entities;
using SchoolManagment.UI.Areas.Admin.ViewModel.ClassViewModel;
using SchoolManagment.UI.Areas.Admin.ViewModels;

namespace SchoolManagment.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ClassController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ===== LIST =====
        public async Task<IActionResult> Index()
        {
            var classes = await _unitOfWork.Classes.GetAllAsync();
            return View(classes);
        }

        // ===== CREATE GET =====
        public IActionResult Create()
        {
            return View();
        }

        // ===== CREATE POST =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newClass = new Class
            {
                ClassName = model.Name,
                //Sections = model.Section
            };

            await _unitOfWork.Classes.AddClassAsync(newClass);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
