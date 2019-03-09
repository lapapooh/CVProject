using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrCvAwardService : IBaseService<HrCvAward>
    {
        void Save(IList<HrCvAward> models, int cvDetailId);

        IEnumerable<HrCvAward> GetForCvDetail(int cvDetailId);
    }
}
