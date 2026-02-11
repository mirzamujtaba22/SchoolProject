using AutoMapper;
using SchoolManagment.Application.Dtos;
using SchoolManagment.Domain.Entities; // Ensure this contains the Student class, not a namespace named Student
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Student
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.ClassName,
                    opt => opt.MapFrom(src => src.Class.ClassName))
                .ForMember(dest => dest.SectionName,
                    opt => opt.MapFrom(src => src.Section.SectionName));

            CreateMap<StudentDto, Student>();

          
            CreateMap<Class, ClassDto>().ReverseMap();

        
            CreateMap<Section, SectionDto>().ReverseMap();
        }
    }
}

