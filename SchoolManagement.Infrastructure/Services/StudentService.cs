using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Application.Interface.Services;
using SchoolManagment.Application.student;
using SchoolManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace SchoolManagement.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
       
            private readonly IUnitOfWork _unitOfWork;

            public StudentService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Student>> GetAllStudentsAsync()
            {
                return await _unitOfWork.Students.GetAllStudentsAsync();
            }

            public async Task<Student?> GetStudentByIdAsync(int id)
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid Student ID");

                return await _unitOfWork.Students.GetStudentByIdAsync(id);
            }

        public async Task AddStudentAsync(StudentCreateDto dto)
        {
            var defaultPassword = "Student@123";

            var user = new User
            {
                FullName = dto.FirstName + " " + dto.LastName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                Role = "Student",
                IsActive = true
            };

            var student = new Student
            {
                RollNumber = dto.RollNumber,
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                GuardianName = dto.GuardianName,
                User = user
            };

            await _unitOfWork.Students.AddStudentAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateStudentAsync(Student student)
            {
                if (student.StudentId <= 0)
                    throw new ArgumentException("Invalid Student ID");

                await _unitOfWork.Students.UpdateStudentAsync(student);
                await _unitOfWork.CompleteAsync();
            }

            public async Task DeleteStudentAsync(int id)
            {
                var student = await _unitOfWork.Students.GetStudentByIdAsync(id);
                if (student != null)
                {
                    await _unitOfWork.Students.DeleteStudentAsync(id);
                    await _unitOfWork.CompleteAsync();
                }
            }
        }
    }

    

