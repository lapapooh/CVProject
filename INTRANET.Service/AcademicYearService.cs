using System;
using System.Collections.Generic;
using System.Linq;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;
using INTRANET.Service.Interfaces;
using System.Web.Mvc;

namespace INTRANET.Service
{
    public class AcademicYearService : BaseService<AcademicYear>, IAcademicYearService
    {
        public AcademicYearService(IAcademicYearRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public void Create(AcademicYear model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Update(AcademicYear model)
        {
            this._repository.Update(model);
            Save();
        }

        public void Delete(AcademicYear model)
        {
            this._repository.Delete(model);
            Save();
        }

        public List<SelectListItem> GetAllAsSelectList(int selected = 0)
        {
            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Text.ToString(),
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }
    }
}
