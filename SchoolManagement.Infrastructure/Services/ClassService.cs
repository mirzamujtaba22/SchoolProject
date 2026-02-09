using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagment.Application.Dtos;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Application.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{

    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<List<ClassDto>> GetClassesAsync()
        {
            var classes = await _unitOfWork.Classes.GetAllClassesAsync();
            return classes.Select(c => new ClassDto
            {
                ClassId = c.ClassId,
                ClassName = c.ClassName
            }).ToList();
        }


    }

}


