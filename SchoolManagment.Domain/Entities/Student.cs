using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SchoolManagment.Domain.Entities
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
    public class Student
    {

        public int StudentId { get; set; }
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Roll Number")]
        public string RollNumber { get; set; }

        [Display(Name = "Class")]
        public int? ClassId { get; set; }

        [Display(Name = "Section")]
        public int? SectionId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Class Class { get; set; }
        public virtual Section Section { get; set; }
    }

}




