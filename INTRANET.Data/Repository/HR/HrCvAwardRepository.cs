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
    public class HrCvAwardRepository : RepositoryBase<HrCvAward>, IHrCvAwardRepository
    {
        public HrCvAwardRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
