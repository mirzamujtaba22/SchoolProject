using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RollNumber { get; set; }

        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public int SectionId { get; set; }
        public string SectionName { get; set; }
    }
}
