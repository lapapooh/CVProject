using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrCvLaborService : IBaseService<HrCvLabor>
    {
        void Save(IList<HrCvLabor> models, int cvDetailId);

        IEnumerable<HrCvLabor> GetForCvDetail(int cvDetailId);
    }
}
