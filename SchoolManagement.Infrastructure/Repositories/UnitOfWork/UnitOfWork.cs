using SchoolManagement.Infrastructure.Persistence;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Application.Interface.StudentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _context;

        public IStudentRepository Students { get; }
        public IClassRepository Classes { get; }
        public ISectionRepository Sections { get; }

        public UnitOfWork(
            StudentDbContext context,
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            ISectionRepository sectionRepository)
        {
            _context = context;
            Students = studentRepository;
            Classes = classRepository;
            Sections = sectionRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

