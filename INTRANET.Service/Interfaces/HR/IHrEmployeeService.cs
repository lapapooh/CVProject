using System.Linq;
using INTRANET.Common;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrEmployeeService : IBaseService<HrEmployee>
    {
        void Create(HrEmployee model);
        void Update(HrEmployee model);
        HrEmployee GetByEmail(string emailLogin);

        void ChangeCvCompletionStatus(HrCvChangeCompletionStatusMode mode, int[] selectedEmployees);

        IQueryable<HrEmployee> GetAllQueryable();
    }
}