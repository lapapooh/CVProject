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
    public class HrEmployeeService : BaseService<HrEmployee>, IHrEmployeeService
    {
        public HrEmployeeService(IHrEmployeeRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Create(HrEmployee model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Update(HrEmployee model)
        {
            this._repository.Update(model);
            Save();
        }

        //use this method for index page
        //this will consume less memory 
        //as pagination + filtering + mapping to model will retrieve limited rows and columns, not all rows and columns       
        public IQueryable<HrEmployee> GetAllQueryable()
        {
            return this._repository.GetAny();
        }
    }
}
