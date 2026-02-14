using SchoolManagment.Application.Interface.Repositories;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Interface.StudentRepository
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
        Task<Class> GetClassByIdAsync(int id);
        Task<Class> GetClassWithSectionsAsync(int classId);
        Task AddClassAsync(Class entity);
        void UpdateClass(Class entity);
        void DeleteClass(Class entity);
    }
}

