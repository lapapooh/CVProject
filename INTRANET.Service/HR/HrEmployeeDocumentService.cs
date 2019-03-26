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
    public class HrEmployeeDocumentService : BaseService<HrEmployeeDocument>, IHrEmployeeDocumentService
    {
            public HrEmployeeDocumentService(IHrEmployeeDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Create(HrEmployeeDocument model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Delete(int id)
        {
            this._repository.Delete(c => c.Id == id);
            Save();
        }

        public IQueryable<HrEmployeeDocument> GetByEmployeeQueryable(int employeeId)
        {
            return this._repository.GetAny().Where(c => c.EmployeeId == employeeId);
        }
    }
}
