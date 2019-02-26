using System;
using System.Collections.Generic;
using INTRANET.Model;
using System.Web.Mvc;

namespace INTRANET.Service.Interfaces
{
    public interface IAcademicYearService : IBaseService<AcademicYear>
    {
        void Create(AcademicYear model);
        void Update(AcademicYear model);
        void Delete(AcademicYear model);

        List<SelectListItem> GetAllAsSelectList(int selected = 0);
    }
}