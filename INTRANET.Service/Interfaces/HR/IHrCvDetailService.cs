using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrCvDetailService : IBaseService<HrCvDetail>
    {
        void Create(HrCvDetail model);
        void Update(HrCvDetail model);

        HrCvDetail GetForCv(int employeeId, HrCvLanguage language);
    }
}
