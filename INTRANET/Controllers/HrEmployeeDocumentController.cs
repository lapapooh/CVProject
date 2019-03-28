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

            var model = new HrEmployeeDocumentListVM()
            {
                EmployeeId = id.Value,
                EmployeeName = employee.FullName,
                HrEmployeeDocuments = _hrEmployeeDocumentService
                                    .GetByEmployeeQueryable(id.Value)
                                    .Select(d=> new HrEmployeeDocumentVM
                                    {
                                        Id = d.Id,
                                        FileName = d.FileName,
                                        Title = d.Title
                                    }).ToList()
            };


            return View(model);

        }

        [HttpGet]
        public FileResult DownloadDocument(int documentId)
        {
            var model=_hrEmployeeDocumentService.GetByID(documentId);
            return File(model.FileContent.ToArray(), model.FileContentType, model.FileName);

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
                model.Title = documentTitle;

                _hrEmployeeDocumentService.Create(model);

            }
            return RedirectToAction("Index", new {Id = employeeId });
        }
    }
}