using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagment.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagment.UI.Areas.Admin.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GuardianName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public int? ClassId { get; set; }


        public string? ClassName { get; set; }

        //public DateTime AdmissionDate { get; set; }

        [Display(Name = "Roll Number")]
        public string RollNumber { get; set; } // Unique roll number

        public string? SectionName { get; set; }

        [Required(ErrorMessage = "Section is required")]
        public int SectionId { get; set; }

        [Display(Name = "Student Full Name")]
        public string? FullName { get; set; }
     
        //public string PasswordHash { get; set; }

        //public List<SelectListItem> Classes { get; set; } = new();
        //public List<SelectListItem> Sections { get; set; } = new();
    }
}
