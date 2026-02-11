using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Services;
using SchoolManagment.Application;
using SchoolManagment.Application.Dtos;
using SchoolManagment.Application.Interface.Services;
using SchoolManagment.Application.student;
using SchoolManagment.Domain.Entities;
using SchoolManagment.UI.Areas.Admin.ViewModels;
using System.Threading.Tasks;
using System.Web.Helpers;
using static System.Collections.Specialized.BitVector32;

namespace SchoolManagment.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly ISectionService _sectionService;
        private string passwordHash;

        public StudentController(
        IStudentService studentService,
        IClassService classService,
        ISectionService sectionService)
        {
            _studentService = studentService;
            _classService = classService;
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var studentViewModels = students.Select(s => new StudentViewModel
            {
                Id = s.StudentId,
                RollNumber = s.RollNumber,
                FullName = s.User.FullName,
                Email = s.User.Email,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                ClassName = s.Class?.ClassName,  // check null
                SectionName = s.Section?.SectionName   // check null
            }).ToList();

            return View(studentViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // 1️⃣ Get entities from DB
            var classEntities = await _classService.GetClassesAsync(); // returns List<Class> 
            var sectionEntities = await _sectionService.GetSectionsAsync(); // returns List<Section>

            // 2️⃣ Map to DTOs
            var classDtos = classEntities
                .Select(c => new ClassDto
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName
                })
                .ToList();

            var sectionDtos = sectionEntities
                .Select(s => new SectionDto
                {
                    SectionId = s.SectionId,
                    SectionName = s.SectionName
                })
                .ToList();



            // 2️⃣ Map to SelectList (anonymous objects with Value/Text)
            ViewBag.Classes = new SelectList(
                classEntities.Select(c => new { Value = c.ClassId, Text = c.ClassName }),
                "Value",
                "Text"
            );

            ViewBag.Sections = new SelectList(
                sectionEntities.Select(s => new { Value = s.SectionId, Text = s.SectionName }),
                "Value",
                "Text"
            );

            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)));
            //ViewBag.Classes = new SelectList(classDtos, "ClassId", "ClassName");
            //ViewBag.Sections = new SelectList(sectionDtos, "SectionId", "SectionName");



            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(model);
                return View(model);
            }

            var dto = new StudentCreateDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                RollNumber = model.RollNumber,
                ClassId = (int)model.ClassId,
                SectionId = model.SectionId,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = model.Address,
                GuardianName = model.GuardianName
            };

            await _studentService.AddStudentAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns(StudentViewModel model)
        {
            ViewBag.Classes = new SelectList(
                await _classService.GetClassesAsync(),
                "ClassId", "ClassName", model.ClassId);

            ViewBag.Sections = new SelectList(
                await _sectionService.GetSectionsAsync(),
                "SectionId", "SectionName", model.SectionId);

            ViewBag.Genders = new SelectList(
                Enum.GetValues(typeof(Gender)), model.Gender);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentViewModel
            {
                RollNumber = student.RollNumber,
                ClassId = student.ClassId,
                SectionId = (int)student.SectionId,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
                GuardianName = student.GuardianName,
                FullName = student.User.FullName,
                Email = student.User.Email
            };

            // FullName split karna
            if (!string.IsNullOrWhiteSpace(student.User?.FullName))
            {
                var names = student.User.FullName.Split(' ', 2);
                model.FirstName = names[0]; 
                model.LastName = names.Length > 1 ? names[1] : ""; 
            }
            // Populate dropdowns
            var classes = await _classService.GetClassesAsync();
            var sections = await _sectionService.GetSectionsAsync();

            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)), model.Gender);

            ViewBag.Classes = new SelectList(
                classes.Select(c => new { Value = c.ClassId, Text = c.ClassName }),
                "Value",
                "Text",
                model.ClassId
            );

            ViewBag.Sections = new SelectList(
                sections.Select(s => new { Value = s.SectionId, Text = s.SectionName }),
                "Value",
                "Text",
                model.SectionId
            );


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                student.RollNumber = model.RollNumber;
                student.ClassId = model.ClassId;
                student.SectionId = model.SectionId;
                student.DateOfBirth = model.DateOfBirth;
                student.Gender = model.Gender;
                student.Address = model.Address;
                student.GuardianName = model.GuardianName;
                student.User.FullName = model.FirstName + " " + model.LastName;
                student.User.Email = model.Email;

                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction("Index");
            }
            ViewBag.Classes = await _classService.GetClassesAsync();
            ViewBag.Sections = await _sectionService.GetSectionsAsync();
            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)), model.Gender);
            return View(model);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentViewModel
            {
                Id = student.StudentId,
                RollNumber = student.RollNumber,
                FullName = student.User.FullName,
                Email = student.User.Email,
                ClassName = student.Class?.ClassName,
                SectionName = student.Section?.SectionName
            };
            return View(model); // Confirm delete view
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }



    }
}
