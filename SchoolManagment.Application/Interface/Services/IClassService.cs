using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagment.Application.Dtos;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Interface.Services
{
    public interface IClassService
    {

        Task<List<ClassDto>> GetClassesAsync();

    }
}
