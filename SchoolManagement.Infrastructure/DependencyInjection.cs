using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Infrastructure.Repositories;
using SchoolManagement.Infrastructure.Repositories.SectionRepository;
using SchoolManagement.Infrastructure.Repositories.UnitOfWork;
using SchoolManagement.Infrastructure.Services;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Application.Interface.Services;
using SchoolManagment.Application.Interface.StudentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)

        {
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ISectionService, SectionService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
