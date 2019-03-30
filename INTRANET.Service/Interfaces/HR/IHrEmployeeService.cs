using System.Linq;
using INTRANET.Common;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrEmployeeService : IBaseService<HrEmployee>
    {
        void Create(HrEmployee model);
        void Update(HrEmployee model);

        void ChangeCvCompletionStatus(HrCvChangeCompletionStatusMode mode, int[] selectedEmployees);

        IQueryable<HrEmployee> GetAllQueryable();
    }
}