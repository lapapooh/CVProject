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

namespace INTRANET.Controllers
{
    public class HrCvController : Controller
    {
        public IHrEmployeeService _hrEmployeeService { get; set; }
        public IHrDepartmentService _hrDepartmentService { get; set; }
        public IHrPositionService _hrPositionService { get; set; }
        public IHrCvDetailService _hrCvDetailService { get; set; }

        public HrCvController(IHrEmployeeService hrEmployeeService,
            IHrDepartmentService hrDepartmentService, IHrPositionService hrPositionService, IHrCvDetailService hrCvDetailService)
        {
            _hrEmployeeService = hrEmployeeService;
            _hrDepartmentService = hrDepartmentService;
            _hrPositionService = hrPositionService;
            _hrCvDetailService = hrCvDetailService;
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

            var details = _hrCvDetailService.GetForCv(employeeId, language);

            //did not fill CV yet, crate record
            if (details == null)
            {
                details = new HrCvDetail
                {
                    EmployeeId = employeeId,
                    Language = language
                };

                _hrCvDetailService.Create(details);
            }

            var model = new HrCvVM
            {
                EmployeeId = employeeId,
                Language = language,
                EmployeeName = employee.FullName
            };


            return View(model);
        }

        [HttpPost]
        //create update a view model HrCvVM, add the necessary fields (phone, external phone,etc.) except fileItem
        //there will be tones of fields - do you want to list them all here? 
        //answer: not anymore :)
        public ActionResult FillCv(HrCvVM model, HttpPostedFileBase fileItem)
        {
            //validation for .png and jpg files will be added
            //if there is no file - it crashes
            if (ModelState.IsValid)
            {
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

                    HrEmployee employee = _hrEmployeeService.GetByID(model.EmployeeId);
                    if (employee != null)
                    {
                        employee.PhoneNo = model.Phone;
                        employee.ExternalPhoneNo = model.ExternalPhone;
                        employee.ImageNameContent = data;

                        try
                        {
                            HrCvDetail _model = new HrCvDetail { EmployeeId = model.EmployeeId, Employee = employee };
                            _hrCvDetailService.Update(_model);
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("", "Form filled incorrectly");

                            //throw;
                        }
                    }


                }
                //else
                //{

                //}
            }


            return RedirectToAction("FillCv", new { employeeId = model.EmployeeId, language = model.Language }); //TODO: fix 
            //return View(model);

        }

        [HttpGet]
        public ActionResult DownloadCv(int employeeId, HrCvLanguage language)
        {
            var employee = _hrEmployeeService.GetByID(employeeId);

            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            return RedirectToAction("Index");
        }

    }
}