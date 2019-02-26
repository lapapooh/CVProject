using System.Collections.Generic;

namespace INTRANET.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        T GetByID(int id);
        IEnumerable<T> GetAll();
    }
}
