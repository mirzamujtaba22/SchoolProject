using SchoolManagment.Application.Interface.Repositories;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Interface.StudentRepository
{
    public interface ISectionRepository : IGenericRepository<Section>
    {
        Task<IEnumerable<Section>> GetAllSectionsAsync();
        Task<Section> GetSectionByIdAsync(int id);
        Task<IEnumerable<Section>> GetSectionsByClassIdAsync(int classId);
        Task AddSectionAsync(Section entity);
        void UpdateSection(Section entity);
        void DeleteSection(Section entity);
    }
}

