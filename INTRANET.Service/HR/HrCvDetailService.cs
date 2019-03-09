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
    public class HrCvDetailService : BaseService<HrCvDetail>, IHrCvDetailService
    {
        public HrCvDetailService(IHrCvDetailRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Create(HrCvDetail model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Update(HrCvDetail model)
        {
            this._repository.Update(model);
            Save();
        }

        public HrCvDetail GetForCv(int employeeId, HrCvLanguage language)
        {
            return this._repository.GetAny().FirstOrDefault(c => c.EmployeeId == employeeId && c.Language == language);
        }
    }
}
