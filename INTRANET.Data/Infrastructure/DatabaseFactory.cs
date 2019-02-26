using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private ApplicationDbContext _dataContext;
        public ApplicationDbContext Get()
        {
            return _dataContext ?? (_dataContext = new ApplicationDbContext());
        }
        protected override void DisposeCore()
        {
            _dataContext?.Dispose();
        }
    }
}
