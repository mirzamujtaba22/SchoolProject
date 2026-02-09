using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Domain.Entities
{
    public class Section
    {
            public int SectionId { get; set; }
            public string SectionName { get; set; }
            public int ClassId { get; set; }

            public Class Class { get; set; }
            public ICollection<Student> Students { get; set; }
        }

    }

