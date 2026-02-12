using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagment.Application.Interface.Repositories;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly StudentDbContext _context;

        public UserRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
        
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}

