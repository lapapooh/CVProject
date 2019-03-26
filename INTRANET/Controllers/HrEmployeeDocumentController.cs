using INTRANET.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INTRANET.Models;
using INTRANET.Service;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Data.Repository;
using INTRANET.Data.Infrastructure;
using INTRANET.Service.Interfaces;
using INTRANET.ViewModels;
using INTRANET.Model;
using System.IO;

namespace INTRANET.Controllers
{
    public class HrEmployeeDocumentController : Controller
    {
        public IHrEmployeeDocumentService _hrEmployeeDocumentService { get; set; }
        public HrEmployeeDocumentController(IHrEmployeeDocumentService hrEmployeeDocumentService)
        {
            _hrEmployeeDocumentService = hrEmployeeDocumentService;
        }
        public ActionResult Index(int Id)
        {   var result = _hrEmployeeDocumentService.GetByEmployeeQueryable(Id);
           
            var list = result.Select(r => new HrEmployeeDocumentVM { FullName = r.Employee.FullName, EmployeeId=r.EmployeeId, Id=r.Id, Title=r.Title}).ToList();
            return View(list.OrderBy(o => o.Title).ToList());

        }

        [HttpGet]
        public FileResult DownloadDocument(int DocumentId)
        {
            var model=_hrEmployeeDocumentService.GetByID(DocumentId);
            //return File(model.FileContent.ToArray(), MimeMapping.GetMimeMapping(model.FileName), model.FileName);
            return File(model.FileContent.ToArray(), model.FileContentType, model.FileName);

        }
         [HttpGet]
        public ActionResult DeleteDocument(int DocumentId, int EmployeeId) {
            _hrEmployeeDocumentService.Delete(DocumentId);
            return RedirectToRoute(new
            {
                controller = "HrEmployeeDocument",
                action = "Index",
                id = EmployeeId
            });
        }
     }
}