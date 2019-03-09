using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrEmployeeDocumentService : IBaseService<HrEmployeeDocument>
    {
        void Create(HrEmployeeDocument model);
        void Delete(int id);
        IQueryable<HrEmployeeDocument> GetByEmployeeQueryable(int employeeId);
    }
}
