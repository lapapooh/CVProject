using System.Linq;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrEmployeeService : IBaseService<HrEmployee>
    {
        void Create(HrEmployee model);
        void Update(HrEmployee model);

        IQueryable<HrEmployee> GetAllQueryable();
    }
}