using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrDepartmentService : IBaseService<HrDepartment>
    {
        void Create(HrDepartment model);
        void Update(HrDepartment model);
    }
}
