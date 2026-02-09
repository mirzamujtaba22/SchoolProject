using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagment.Application.Dtos;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Application.Interface.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SectionDto>> GetSectionsAsync()
        {
            var sections = await _unitOfWork.Sections.GetAllAsync();
            return sections.Select(s => new SectionDto
            {
                SectionId = s.SectionId,
                SectionName = s.SectionName
            }).ToList();
        }
    }
}