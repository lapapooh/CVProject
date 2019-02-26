using INTRANET.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INTRANET.Controllers
{
    public class AcademicYearController : Controller
    {
        public IAcademicYearService AcademicYearService { get; set; }

        public AcademicYearController(IAcademicYearService academicYearService)
        { }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            return Json(AcademicYearService.GetAllAsSelectList(selected), JsonRequestBehavior.AllowGet);
        }
    }
}