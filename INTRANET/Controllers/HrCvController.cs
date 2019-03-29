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

namespace INTRANET.Controllers
{
    public class HrCvController : Controller
    {
        public IHrEmployeeService _hrEmployeeService { get; set; }
        public IHrDepartmentService _hrDepartmentService { get; set; }
        public IHrPositionService _hrPositionService { get; set; }

        public HrCvController(IHrEmployeeService hrEmployeeService,
            IHrDepartmentService hrDepartmentService, IHrPositionService hrPositionService)
        {
            _hrEmployeeService = hrEmployeeService;
            _hrDepartmentService = hrDepartmentService;
            _hrPositionService = hrPositionService;
        }

        // GET: HrCv
        public ActionResult Index()
        {            
            var departmentsList = _hrDepartmentService
                                    .GetAll()
                                    .Select(a => new SelectListItem {
                                        Text = a.TitleEn,
                                        Value = a.Id.ToString()
                                    }).ToList();

            var positionsList = _hrPositionService
                                    .GetAll()
                                    .ToList()
                                    .Select(a => new SelectListItem {Text = a.TitleEn, Value = a.Id.ToString()
                                    }).ToList();
            var model = new HrCvListVM {
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


        public ActionResult LoadData()
        {
            try
            {
                var draw = int.Parse(HttpContext.Request.Form["draw"].FirstOrDefault().ToString());
                // Skiping number of Rows count  
                var start = int.Parse(Request.Form["start"].FirstOrDefault().ToString());
                // Paging Length 10,20  
                var length = int.Parse(Request.Form["length"].FirstOrDefault().ToString());
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault().ToString();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault().ToString();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

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
                ////Search  
                //if (!string.IsNullOrEmpty(searchValue.ToString()))
                //{
                //    use contains instead of ==
                //    employeeData = employeeData.Where(m => m.FullName == searchValue);
                //}

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
                return Json(new { draw = 0, recordsFiltered = new object[0], recordsTotal = 0, data = 0 });
            }
        }

    }
}