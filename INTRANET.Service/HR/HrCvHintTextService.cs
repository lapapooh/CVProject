using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;
using INTRANET.Service.Interfaces;

namespace INTRANET.Service
{
    public class HrCvHintTextService : BaseService<HrCvHintText>, IHrCvHintTextService
    {
        public HrCvHintTextService(IHrCvHintTextRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Create(HrCvHintText model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Update(HrCvHintText model)
        {
            this._repository.Update(model);
            Save();
        }

        public void Delete(int id)
        {
            this._repository.Delete(c=> c.Id == id);
            Save();
        }

        public IEnumerable<HrCvHintText> GetByLanguage(HrCvLanguage language)
        {
            return GetAll().Where(c => c.Language == language);
        }

        public IQueryable<HrCvHintText> GetAllQueryable()
        {
            return this._repository.GetAny();
        }

        public void CreateDefauls(HrCvLanguage language)
        {

        }

        private void CreateDefaultsRu()
        {

        }

        private void CreateDefaultsUz()
        {

        }
    }
}