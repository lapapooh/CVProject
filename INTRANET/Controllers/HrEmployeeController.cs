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
            //AddDefaultsToModel(model, employee);

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
            var employee = _hrEmployeeService.GetByID(model.Id);

            AddDefaultsToModel(model, employee);
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
            var employee = _hrEmployeeService.GetByID(id);
            var model = new HrEmployeeVM
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Code_1C = employee.Code_1C,
                ID_1C = employee.ID_1C,
                DateOfBirth = employee.DateOfBirth,
                PlaceOfBirth = employee.PlaceOfBirth,
                Gender = employee.Gender,
                Address = employee.Address,
                EmailLogin = employee.EmailLogin,
                EntryDate = employee.EntryDate,
                LeaveDate = employee.LeaveDate,
                PositionId = employee.PositionId,
                DepartmentId = employee.DepartmentId,
                PositionStartDate = employee.PositionStartDate,
                IsActive = employee.IsActive,
                PassportNo = employee.PassportNo,
                PassportIssueDate = employee.PassportIssueDate,
                PassportIssuePlace = employee.PassportIssuePlace
            };
            AddDefaultsToModel(model, employee);
            return View(MapTo(_hrEmployeeService.GetByID(id)));
        }

        [HttpPost]
        public ActionResult Edit(HrEmployeeVM model)
        {
            var employee = _hrEmployeeService.GetByID(model.Id);
            AddDefaultsToModel(model, employee);
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

        private void AddDefaultsToModel(HrEmployeeVM model, HrEmployee employee)
        {
            model.FullName = employee.FullName;
            model.Code_1C = employee.Code_1C;
            model.ID_1C = employee.ID_1C;
            model.DateOfBirth = employee.DateOfBirth;
            model.PlaceOfBirth = employee.PlaceOfBirth;
            model.Gender = employee.Gender;
            model.Address = employee.Address;
            model.EmailLogin = employee.EmailLogin;
            model.EntryDate = employee.EntryDate;
            model.LeaveDate = employee.LeaveDate;
            model.PositionId = employee.PositionId;
            model.DepartmentId = employee.DepartmentId;
            model.PositionStartDate = employee.PositionStartDate;
            model.IsActive = employee.IsActive;
            model.PassportNo = employee.PassportNo;
            model.PassportIssueDate = employee.PassportIssueDate;
            model.PassportIssuePlace = employee.PassportIssuePlace;

        }

        }
}
