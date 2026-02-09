using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Persistence
{
    public class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
    {

        
            public StudentDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();
                optionsBuilder.UseSqlServer("Server=.;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;");

                return new StudentDbContext(optionsBuilder.Options);
            }
        }
    }



