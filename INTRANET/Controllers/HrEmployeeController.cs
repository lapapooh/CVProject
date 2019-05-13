using INTRANET.Model;
using INTRANET.Models;
using INTRANET.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INTRANET.Controllers
{
    public class HrEmployeeController : Controller
    {
        IHrEmployeeService _hrEmployeeService { get; set; }
        IHrDepartmentService _hrDepartmentService { get; set; }
        IHrPositionService _hrPositionService { get; set; }
        public HrEmployeeController(IHrEmployeeService hrEmployeeService, 
            IHrDepartmentService hrDepartmentService, IHrPositionService hrPositionService)
        {
            _hrEmployeeService = hrEmployeeService;
            _hrDepartmentService = hrDepartmentService;
            _hrPositionService = hrPositionService;
        }

        // GET: HrEmployee
        public ActionResult Index()
        {
            var model = _hrEmployeeService.GetAll().Select(MapTo).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.departmentList = _hrDepartmentService
                                    .GetAll()
                                    .Select(a => new SelectListItem
                                    {
                                        Text = a.TitleEn,
                                        Value = a.Id.ToString()
                                    }).ToList();

            ViewBag.positionsList = _hrPositionService
                                    .GetAll()
                                    .Select(a => new SelectListItem
                                    {
                                        Text = a.TitleEn,
                                        Value = a.Id.ToString()
                                    }).ToList();
            return View(new HrEmployeeVM());
        }

        
        [HttpPost]
        public ActionResult Create(HrEmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                _hrEmployeeService.Create(MapFrom(model));
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(MapTo(_hrEmployeeService.GetByID(id)));
        }

        [HttpPost]
        public ActionResult Edit(HrEmployeeVM model)
        {
            _hrEmployeeService.Update(MapFrom(model));
            return RedirectToAction("Index");
        }   

        public HrEmployeeVM MapTo(HrEmployee model)
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
            return new HrEmployeeVM
            {
                Id = model.Id,
                FullName = model.FullName,
                Code_1C = model.Code_1C,
                ID_1C = model.ID_1C,
                DateOfBirth = model.DateOfBirth,
                PlaceOfBirth = model.PlaceOfBirth,
                Gender = model.Gender,
                Address = model.Address,
                EmailLogin = model.EmailLogin,
                EntryDate = model.EntryDate,
                LeaveDate = model.LeaveDate,
                PositionId = model.PositionId,
                DepartmentId = model.DepartmentId,
                PositionStartDate = model.PositionStartDate,
                IsActive = model.IsActive,
                PassportNo = model.PassportNo,
                PassportIssueDate = model.PassportIssueDate,
                PassportIssuePlace = model.PassportIssuePlace,
                Departments = departmentsList,
                Positions = positionsList
            };
        }

        public HrEmployee MapFrom(HrEmployeeVM model)
        {
            return new HrEmployee
            {
                Id = model.Id,
                FullName = model.FullName,
                Code_1C = model.Code_1C,
                ID_1C = model.ID_1C,
                DateOfBirth = model.DateOfBirth,
                PlaceOfBirth = model.PlaceOfBirth,
                Gender = model.Gender,
                Address = model.Address,
                EmailLogin = model.EmailLogin,
                EntryDate = model.EntryDate,
                LeaveDate = model.LeaveDate,
                PositionId = model.PositionId,
                DepartmentId = model.DepartmentId,
                PositionStartDate = model.PositionStartDate,
                IsActive = model.IsActive,
                PassportNo = model.PassportNo,
                PassportIssueDate = model.PassportIssueDate,
                PassportIssuePlace = model.PassportIssuePlace
            };
        }
    }
}
