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


        [HttpGet]
        public ActionResult Create()
        {
            var dob = new DateTime(1990, 1, 1);
            var model = new HrEmployeeVM
            {
                DateOfBirth = dob,
                EntryDate = DateTime.Now,
                PositionStartDate = DateTime.Now,
                IsActive = true
            };
            AddDefaultsToModel(model);


            return View(model);
        }

        
        [HttpPost]
        public ActionResult Create(HrEmployeeVM model)
        {

            AddDefaultsToModel(model);
            if (ModelState.IsValid)
            {
                _hrEmployeeService.Create(MapFrom(model));
                return RedirectToAction("Index", "HrCv");
            }
            else
                return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var employee = _hrEmployeeService.GetByID(id);
            //safety check
            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            var model = MapTo(employee);
            AddDefaultsToModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(HrEmployeeVM model)
        {
            if(ModelState.IsValid)
            {
                var employee = _hrEmployeeService.GetByID(model.Id);

                //safety check
                if (employee == null)
                    return RedirectToAction("Index", "HrCv");

                //do not update all fields - will override image and other fields in database
                employee.FullName = model.FullName;
                employee.Code_1C = model.Code_1C;
                employee.ID_1C = model.ID_1C;
                employee.DateOfBirth = model.DateOfBirth;
                employee.PlaceOfBirth = model.PlaceOfBirth;
                employee.Gender = model.Gender;
                employee.Address = model.Address;
                employee.EmailLogin = model.EmailLogin;
                employee.EntryDate = model.EntryDate;
                employee.LeaveDate = model.LeaveDate;
                employee.PositionId = model.PositionId;
                employee.DepartmentId = model.DepartmentId;
                employee.PositionStartDate = model.PositionStartDate;
                employee.IsActive = model.IsActive;
                employee.PassportNo = model.PassportNo;
                employee.PassportIssueDate = model.PassportIssueDate;
                employee.PassportIssuePlace = model.PassportIssuePlace;

                _hrEmployeeService.Update(employee);
                return RedirectToAction("Index", "HrCv");
            }

            //wrong user input - show page back
            AddDefaultsToModel(model);
            return View(model);
            
        }   

        public HrEmployeeVM MapTo(HrEmployee model)
        {

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
                PassportIssuePlace = model.PassportIssuePlace
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

        private void AddDefaultsToModel(HrEmployeeVM model)
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
            model.Departments = departmentsList;
            model.Positions = positionsList;
        }

    }
}
