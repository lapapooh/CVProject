using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrCvEductionService : IBaseService<HrCvEduction>
    {
        void Save(IList<HrCvEduction> models, int cvDetailId);

        IEnumerable<HrCvEduction> GetForCvDetail(int cvDetailId);
    }
}
