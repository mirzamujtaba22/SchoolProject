using SchoolManagment.Application.Interface.StudentRepository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Application.Interface.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        IClassRepository Classes { get; }
        ISectionRepository Sections { get; }

        Task<int> CompleteAsync();
    }
}
