using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrPositionService : IBaseService<HrPosition>
    {
        void Create(HrPosition model);
        void Update(HrPosition model);
    }
}
