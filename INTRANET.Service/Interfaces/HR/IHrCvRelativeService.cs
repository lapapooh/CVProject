using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrCvRelativeService : IBaseService<HrCvRelative>
    {
        void Save(IList<HrCvRelative> models, int cvDetailId);

        IEnumerable<HrCvRelative> GetForCvDetail(int cvDetailId);
    }
}
