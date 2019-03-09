using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;
using INTRANET.Service.Interfaces;

namespace INTRANET.Service
{
    public class HrDepartmentService : BaseService<HrDepartment>, IHrDepartmentService
    {
        public HrDepartmentService(IHrDepartmentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Create(HrDepartment model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Update(HrDepartment model)
        {
            this._repository.Update(model);
            Save();
        }
    }
}