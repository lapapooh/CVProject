using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
