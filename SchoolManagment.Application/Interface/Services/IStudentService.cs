using SchoolManagment.Application.student;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Interface.Services
{
    public interface IStudentService
    {
        Task AddStudentAsync(StudentCreateDto dto);
        Task<IEnumerable<SchoolManagment.Domain.Entities.Student>> GetAllStudentsAsync();
        Task<SchoolManagment.Domain.Entities.Student?> GetStudentByIdAsync(int id);
        Task UpdateStudentAsync(SchoolManagment.Domain.Entities.Student student);
        Task DeleteStudentAsync(int id);
    }
}
