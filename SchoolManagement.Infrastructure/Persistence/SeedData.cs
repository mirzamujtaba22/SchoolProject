using Microsoft.Extensions.DependencyInjection;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // 🔒 Check if Admin already exists (Idempotent)
            var existingAdmin = (await unitOfWork.Users.GetAllAsync())
                .FirstOrDefault(u => u.Role == "Admin");

            if (existingAdmin != null)
                return;

            var admin = new User
            {
                FullName = "Super Admin",
                Email = "admin@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                IsActive = true
            };

            await unitOfWork.Users.AddAsync(admin);
            await unitOfWork.CompleteAsync();
        }
    }
}

