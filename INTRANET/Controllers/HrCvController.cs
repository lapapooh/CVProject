//using DataTables.AspNet.Core;
//using DataTables.AspNet.Mvc5;
using INTRANET.Data;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository;
using INTRANET.Model;
using INTRANET.Service;
using INTRANET.Service.Interfaces;
using INTRANET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INTRANET.Common;
using Xceed.Words.NET;
using System.Reflection;
using Spire.Doc;
using System.Web.Hosting;

namespace INTRANET.Controllers
{
    public class HrCvController : Controller
    {
        public IHrEmployeeService _hrEmployeeService { get; set; }
        public IHrDepartmentService _hrDepartmentService { get; set; }
        public IHrPositionService _hrPositionService { get; set; }
        public IHrCvDetailService _hrCvDetailService { get; set; }
        public IHrCvEductionService _hrCvEducationService { get; set; }

        public HrCvController(IHrEmployeeService hrEmployeeService,
            IHrDepartmentService hrDepartmentService, IHrPositionService hrPositionService, IHrCvDetailService hrCvDetailService,
            IHrCvEductionService hrCvEducationService)
        {
            _hrEmployeeService = hrEmployeeService;
            _hrDepartmentService = hrDepartmentService;
            _hrPositionService = hrPositionService;
            _hrCvDetailService = hrCvDetailService;
            _hrCvEducationService = hrCvEducationService;
        }

        // GET: HrCv
        public ActionResult Index()
        {
            var departmentsList = _hrDepartmentService
                                    .GetAll()
                                    .Select(a => new SelectListItem
                                    {
                                        Text = a.TitleEn,
                                        Value = a.Id.ToString()
                                    }).ToList();

            var positionsList = _hrPositionService
                                    .GetAll()
                                    .Select(a => new SelectListItem
                                    {
                                        Text = a.TitleEn,
                                        Value = a.Id.ToString()
                                    }).ToList();
            var model = new HrCvListVM
            {
                Departments = departmentsList,
                Positions = positionsList
            };

            return View(model);
        }

        private HrEmployeeListVM MapToModel(HrEmployee hrEmployee)
        {
            return new HrEmployeeListVM
            {
                Id = hrEmployee.Id,
                FullName = hrEmployee.FullName,
                DepartmentName = hrEmployee.Department?.TitleEn,
                PositionName = hrEmployee.Position?.TitleEn,
                HasFilledRuCV = hrEmployee.ComplietedRuCv,
                HasFilledUzCV = hrEmployee.ComplietedUzCv
            };
        }


        public ActionResult LoadData(int[] selectedDepartments, int[] selectedPositions)
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form.GetValues("start").FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form.GetValues("length").FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var employeeData = _hrEmployeeService.GetAllQueryable();

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "FullName":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.FullName);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.FullName);
                            break;
                        case "DepartmentName":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.Department.TitleEn);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.Department.TitleEn);
                            break;
                        case "Position":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.Position.TitleEn);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.Position.TitleEn);
                            break;
                        case "HasFilledRuCV":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.ComplietedRuCv);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.ComplietedRuCv);
                            break;
                        case "HasFilledUzCV":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.ComplietedUzCv);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.ComplietedUzCv);
                            break;
                        default:
                            employeeData = employeeData.OrderBy(c => c.FullName);
                            break;
                    }
                }

                if (selectedDepartments != null && selectedDepartments.Any())
                    employeeData = employeeData.Where(
                        e => e.DepartmentId.HasValue &&
                        selectedDepartments.Contains(e.DepartmentId.Value));


                if (selectedPositions != null && selectedPositions.Any())
                    employeeData = employeeData.Where(
                        e => e.PositionId.HasValue &&
                        selectedPositions.Contains(e.PositionId.Value));



                ////Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    employeeData = employeeData.Where(m => m.FullName.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = employeeData.Count();
                //Paging   
                //var data = employeeData.Skip(skip).Take(pageSize).ToList();
                var data = employeeData.Skip(skip).Take(pageSize).Select(MapToModel);
                //var data = employeeData.Take(pageSize).Select(MapToModel);

                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                //logging goes here
                return Json(new { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = new object[0] });
            }
        }

        [HttpPost]
        public ActionResult ChangeCvCompletionStatus(HrCvChangeCompletionStatusMode mode, int[] selectedEmployees)
        {
            try
            {
                _hrEmployeeService.ChangeCvCompletionStatus(mode, selectedEmployees);
                return Json(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                //logging goes here
                return Json(new { IsSuccess = false });
            }
        }

        [HttpGet]
        public ActionResult FillCv(int employeeId, HrCvLanguage language)
        {
            var employee = _hrEmployeeService.GetByID(employeeId);

            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            var details = GetCvDetail(employeeId, language);

            var model = new HrCvVM
            {
                EmployeeId = employeeId,
                Language = language,
                Phone = employee.PhoneNo,
                ExternalPhone = employee.ExternalPhoneNo,
                EntryDate = employee.EntryDate,
                PlaceOfBirth = details.PlaceOfBirth,
                Nationality = details.Nationality,
                PartyMembership = details.PartyMembership,
                EducationDegree = details.EducationDegree,
                EducationSpeciality = details.EducationSpeciality,
                AcademicDegree = details.AcademicDegree,
                AcademicTitle = details.AcademicTitle,
                Languages = details.Languages,
                EducationList = _hrCvEducationService.GetForCvDetail(details.Id).Select(c=> c.Education).ToList()

            };

            AddDefaultsToModel(model, employee);

            return View(model);
        }

        [HttpPost]
        public ActionResult FillCv(HrCvVM model, HttpPostedFileBase fileItem)
        {

            var employee = _hrEmployeeService.GetByID(model.EmployeeId);
            if (employee == null)
                return RedirectToAction("Index");

            AddDefaultsToModel(model, employee);


            //safety check to avoid null reference exceptions
            if (model.EducationList == null)
                model.EducationList = new List<string>();

            var hasImageFile = fileItem != null && fileItem.ContentLength > 0;
            //validation for .png and jpg files 
            if (!hasImageFile)
            {
                //do not require image if previously there was one
                if(employee.ImageNameContent == null || employee.ImageNameContent.Length == 0) //previously no image
                    ModelState.AddModelError(string.Empty, "Photo was not selected");
            }
            else
            {
                string ex = Path.GetExtension(fileItem.FileName);
                List<string> acceptedExtensions = new List<string> { ".png", ".jpg" };

                if (!acceptedExtensions.Contains(ex))
                {
                    ModelState.AddModelError(string.Empty, "Photo must be in .png or .jpg format");
                }
            }

            if (ModelState.IsValid)
            {
                if (hasImageFile)
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
                        employee.ImageNameContent = data;
                        employee.ImageName = fileItem.FileName;
                        employee.ImageNameContentType = fileItem.ContentType;
                    }
                }
                employee.PhoneNo = model.Phone;
                employee.ExternalPhoneNo = model.ExternalPhone;


                try
                {
                    _hrEmployeeService.Update(employee);
                    var details = GetCvDetail(model.EmployeeId, model.Language);

                    details.Nationality = model.Nationality;

                    _hrCvDetailService.Update(details);

                    

                    _hrCvEducationService.Save(model.EducationList.Where(e=> !string.IsNullOrWhiteSpace(e)).Select(e => new HrCvEduction
                    {
                        Education = e
                    }).ToList(), details.Id);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Form filled incorrectly");

                }
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult DownloadCv(int employeeId, HrCvLanguage language)
        {
            var employee = _hrEmployeeService.GetByID(employeeId);

            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            var path = "";
            if (language == HrCvLanguage.Uz)
            {
                path = HostingEnvironment.MapPath("~/Content/HrCvTemplates/cv_template_uz.doc");
                var employeeCV = _hrCvDetailService.GetForCv(employeeId, HrCvLanguage.Uz);

                Document doc = new Document();
                doc.LoadFromFile(path);
                doc.Replace("{FULLNAME}", employee.FullName, true, true);
                doc.Replace("{CURRENTPOSITIONDATE}", employee.PositionStartDate.Value.ToString("dd MMMM yyyy"), true, true);
                doc.Replace("{DATEOFBIRTH}", employee.DateOfBirth.ToString(), true, true);
                doc.Replace("{PLACEOFBIRTH}", employee.PlaceOfBirth.ToString(), true, true);
                doc.Replace("{NATIONALITY}", employeeCV?.Nationality ?? "", true, true);
                doc.Replace("{PARTYMEMBERSHIP}", employeeCV?.PartyMembership ?? "", true, true);
                doc.Replace("{EDUCATIONDEGREE}", employeeCV?.EducationDegree ?? "", true, true);
                doc.Replace("{EDUCATIONSPECIALITY}", employeeCV?.EducationSpeciality ?? "", true, true);
                doc.Replace("{ACADEMICDEGREE}", employeeCV?.AcademicDegree ?? "", true, true);
                doc.Replace("{ACADEMICTITLE}", employeeCV?.AcademicTitle ?? "", true, true);
                doc.Replace("{LANGUAGES}", employeeCV?.Languages ?? "", true, true);
                doc.Replace("{AWARDS}", employeeCV?.Awards.ToString() ?? "", true, true);
                doc.Replace("{MEMBERSHIPS}", employeeCV?.Memberships.ToString() ?? "", true, true);
                doc.SaveToFile("Doc.doc", FileFormat.Doc);

                byte[] data = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    doc.SaveToStream(memoryStream, FileFormat.Doc);
                    //save to byte array
                    data = memoryStream.ToArray();

                }
                //Write it back to the client
                Response.ContentType = "application/msword";
                Response.AddHeader("content-disposition", "attachment;  filename=Doc.doc");
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();

            }
            else if (language == HrCvLanguage.Ru)
            {
                path = HostingEnvironment.MapPath("~/Content/HrCvTemplates/cv_template_ru.doc");

                var employeeCV = _hrCvDetailService.GetForCv(employeeId, HrCvLanguage.Ru);
                var now = DateTime.Now;
                Document doc = new Document();
                doc.LoadFromFile(path);
                doc.Replace("{FULLNAME}", employee.FullName, true, true);
                doc.Replace("{CURRENTPOSITIONDATE}", employee.PositionStartDate.Value.ToString("dd MMMM yyyy"), true, true);
                doc.Replace("{DATEOFBIRTH}", employee.DateOfBirth.ToString("MM/dd/yyyy"), true, true);
                doc.Replace("{PLACEOFBIRTH}", employee.PlaceOfBirth.ToString(), true, true);
                doc.Replace("{NATIONALITY}", employeeCV?.Nationality ?? "", true, true);
                doc.Replace("{PARTYMEMBERSHIP}", employeeCV?.PartyMembership ?? "", true, true);
                doc.Replace("{EDUCATIONDEGREE}", employeeCV?.EducationDegree ?? "", true, true);
                doc.Replace("{EDUCATIONSPECIALITY}", employeeCV?.EducationSpeciality ?? "", true, true);
                doc.Replace("{ACADEMICDEGREE}", employeeCV?.AcademicDegree ?? "", true, true);
                doc.Replace("{ACADEMICTITLE}", employeeCV?.AcademicTitle ?? "", true, true);
                doc.Replace("{LANGUAGES}", employeeCV?.Languages ?? "", true, true);
                doc.Replace("{AWARDS}", employeeCV?.Awards.ToString() ?? "", true, true);
                doc.Replace("{MEMBERSHIPS}", employeeCV?.Memberships.ToString() ?? "", true, true);
                doc.SaveToFile("Doc.doc", FileFormat.Doc);

                byte[] data = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    doc.SaveToStream(memoryStream, FileFormat.Doc);
                    //save to byte array
                    data = memoryStream.ToArray();
                }
                //Write it back to the client
                Response.ContentType = "application/msword";
                Response.AddHeader("content-disposition", "attachment;  filename=Doc.doc");
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }

        private HrCvDetail GetCvDetail(int employeeId, HrCvLanguage language)
        {
            var details = _hrCvDetailService.GetForCv(employeeId, language);

            //did not fill CV yet, create record
            if (details == null)
            {
                details = new HrCvDetail
                {
                    EmployeeId = employeeId,
                    Language = language
                };

                _hrCvDetailService.Create(details);
            }
            return details;
        }

        //add readonly fields
        private void AddDefaultsToModel(HrCvVM model, HrEmployee employee)
        {
            model.EmployeeName = employee.FullName;
            model.DepartmentName = model.Language == HrCvLanguage.Ru ? employee.Department.TitleRu : employee.Department.TitleUz;
            model.PositionName = model.Language == HrCvLanguage.Ru ? employee.Position.TitleRu : employee.Position.TitleUz;
            model.DateOfBirth = employee.DateOfBirth;
            if (employee.ImageNameContent != null && employee.ImageNameContent.Length > 0)
            {
                model.ImageContent = Convert.ToBase64String(employee.ImageNameContent);
            }
        }

    }
}