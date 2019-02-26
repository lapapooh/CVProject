using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;

namespace INTRANET.Data.Repository
{
    public class AcademicYearRepository : RepositoryBase<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}