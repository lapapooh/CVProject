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
using INTRANET.Model;
using System.IO;

namespace INTRANET.Controllers
{
    public class HrEmployeeDocumentController : Controller
    {
        public IHrEmployeeDocumentService _hrEmployeeDocumentService { get; set; }
        public IHrEmployeeService _hrEmployeeService { get; set; }
        public HrEmployeeDocumentController(IHrEmployeeDocumentService hrEmployeeDocumentService, IHrEmployeeService hrEmployeeService)
        {
            _hrEmployeeDocumentService = hrEmployeeDocumentService;
            _hrEmployeeService = hrEmployeeService;
        }
        public ActionResult Index(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index", "HrCv");

            var employee = _hrEmployeeService.GetByID(id.Value);

            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            var model = PrepareVM(employee, true);


            return View(model);

        }

        [HttpGet]
        public ActionResult View()
        {
            //for now hardcoded. 
            //on integration - should get email of logged in user
            var email = "mshpirko";

            var employee = _hrEmployeeService.GetByEmail(email);

            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            var model = PrepareVM(employee, false);


            return View(model);
        }

        [HttpGet]
        public ActionResult DownloadDocument(int documentId)
        {
            var document=_hrEmployeeDocumentService.GetByID(documentId);
            if (document == null)
                return RedirectToAction("Index", "HrCv");
            return File(document.FileContent.ToArray(), document.FileContentType, document.FileName);

        }

        [HttpPost]
        public ActionResult DeleteDocument(int documentId)
        {
            try
            {
                _hrEmployeeDocumentService.Delete(documentId);
                return Json(new { IsSuccess = true });
            }
            catch(Exception ex)
            {
                //logging goes here
                return Json(new { IsSuccess = false });
            }
            

        }

        [HttpPost]
        public ActionResult UploadDocument(int employeeId, string documentTitle, HttpPostedFileBase fileItem)
        {
            var model = new HrEmployeeDocument();
            if (fileItem != null)
            {
                byte[] data;
                using (var inputStream = fileItem.InputStream)
                {
                    var memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }

                model.FileContent = data;
                model.FileName = fileItem.FileName;
                model.FileContentType = fileItem.ContentType;
                model.EmployeeId = employeeId;
                model.Title = string.IsNullOrEmpty(documentTitle) ? fileItem.FileName : documentTitle; //or will give arror if user did not type title

                _hrEmployeeDocumentService.Create(model);

            }
            return RedirectToAction("Index", new {Id = employeeId });
        }


        private HrEmployeeDocumentListVM PrepareVM(HrEmployee employee, bool isHr)
        {
            return new HrEmployeeDocumentListVM()
            {
                IsHrUser=isHr,
                EmployeeId = employee.Id,
                EmployeeName = employee.FullName,
                HrEmployeeDocuments = _hrEmployeeDocumentService
                                    .GetByEmployeeQueryable(employee.Id)
                                    .Select(d => new HrEmployeeDocumentVM
                                    {
                                        Id = d.Id,
                                        FileName = d.FileName,
                                        Title = d.Title
                                    }).ToList(),
                CompletedRuCv = employee.ComplietedRuCv,
                CompletedUzCv = employee.ComplietedUzCv
            };
        }
    }
}