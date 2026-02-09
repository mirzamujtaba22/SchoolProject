using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SchoolManagment.Domain.Entities
{
    public class Class
    {
        
            public int ClassId { get; set; }
            public string ClassName { get; set; }

            public ICollection<Section> Sections { get; set; }
            public ICollection<Student> Students { get; set; }
        }

    }

