using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;

namespace INTRANET.Data.Repository
{
    public class HrEmployeeRepository : RepositoryBase<HrEmployee>, IHrEmployeeRepository
    {
        public HrEmployeeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
