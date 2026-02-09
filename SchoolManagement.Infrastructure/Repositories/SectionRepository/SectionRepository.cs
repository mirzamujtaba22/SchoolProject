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

namespace SchoolManagement.Infrastructure.Repositories.SectionRepository
{
    internal class SectionRepository : GenericRepository<Section>, ISectionRepository
    {
        public SectionRepository(StudentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Section>> GetAllSectionsAsync()
        {
            return await _context.Sections
                .Include(s => s.Class)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Section> GetSectionByIdAsync(int id)
        {
            return await _context.Sections
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.SectionId == id);
        }

        public async Task<IEnumerable<Section>> GetSectionsByClassIdAsync(int classId)
        {
            return await _context.Sections
                .Where(s => s.ClassId == classId)
                .ToListAsync();
        }

        public async Task AddSectionAsync(Section entity)
        {
            await _context.Sections.AddAsync(entity);
        }

        public void UpdateSection(Section entity)
        {
            _context.Sections.Update(entity);
        }

        public void DeleteSection(Section entity)
        {
            _context.Sections.Remove(entity);
        }
    }
}

