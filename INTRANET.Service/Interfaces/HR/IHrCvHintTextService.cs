using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Model;

namespace INTRANET.Service.Interfaces
{
    public interface IHrCvHintTextService : IBaseService<HrCvHintText>
    {
        void Create(HrCvHintText model);
        void Update(HrCvHintText model);
        void Delete(int id);

        IEnumerable<HrCvHintText> GetByLanguage(HrCvLanguage language);
        IQueryable<HrCvHintText> GetAllQueryable();

        void CreateDefauls(HrCvLanguage language);
    }
}
