using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using INTRANET.Service.Interfaces;

namespace INTRANET.Controllers.Api
{
    [System.Web.Http.RoutePrefix("AcademicYear")]
    public class AcademicYearController : BaseApiController
    {
        public IAcademicYearService AcademicYearService { get; set; }

        public AcademicYearController(IAcademicYearService academicYearService)
        {
            AcademicYearService = academicYearService;
        }

        // GET: AcademicYear
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AcademicYearService.GetAllAsSelectList(selected));
        }
    }
}