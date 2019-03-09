using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrCvMembershipService : IBaseService<HrCvMembership>
    {
        void Save(IList<HrCvMembership> models, int cvDetailId);

        IEnumerable<HrCvMembership> GetForCvDetail(int cvDetailId);
    }
}
