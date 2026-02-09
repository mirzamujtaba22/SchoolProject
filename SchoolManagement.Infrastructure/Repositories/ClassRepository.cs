using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Repositories.GenericRepository;
using SchoolManagment.Application.Interface.StudentRepository;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories
{
    internal class ClassRepository :GenericRepository<Class>, IClassRepository
    {
        public ClassRepository(StudentDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Class>> GetAllClassesAsync()
    {
        return await _context.Classes
            .Include(c => c.Sections)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Class> GetClassByIdAsync(int id)
    {
        return await _context.Classes
            .Include(c => c.Sections)
            .FirstOrDefaultAsync(c => c.ClassId == id);
    }

    public async Task<Class> GetClassWithSectionsAsync(int classId)
    {
        return await _context.Classes
            .Include(c => c.Sections)
            .FirstOrDefaultAsync(c => c.ClassId == classId);
    }

    public async Task AddClassAsync(Class entity)
    {
        await _context.Classes.AddAsync(entity);
    }

    public void UpdateClass(Class entity)
    {
        _context.Classes.Update(entity);
    }

    public void DeleteClass(Class entity)
    {
        _context.Classes.Remove(entity);
    }
}
}
   

