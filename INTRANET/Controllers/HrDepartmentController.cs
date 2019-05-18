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
    public class HrDepartmentController : Controller
    {
        IHrDepartmentService _hrDepartmentService { get; set; }

        public HrDepartmentController(IHrDepartmentService hrDepartmentService)
        {
            _hrDepartmentService = hrDepartmentService;
        }

        public ActionResult Index()
        {
            var model = _hrDepartmentService.GetAll().Select(MapTo).ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new HrDepartmentVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HrDepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var department = MapFrom(model);
                department.CreatedAt = DateTime.Now;
                //TODO: add logic for WHO modified
                _hrDepartmentService.Create(department);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var department = _hrDepartmentService.GetByID(id);
            if (department == null)
                return RedirectToAction("Index");

            return View(MapTo(department));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HrDepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var department = _hrDepartmentService.GetByID(model.Id);
                if (department == null)
                    return RedirectToAction("Index");

                //do not update all fields with MapFrom method - will override created fields in database
                department.Code_1C = model.Code_1C;
                department.TitleEn = model.TitleEn;
                department.TitleRu = model.TitleRu;
                department.TitleUz = model.TitleUz;

                department.ModifiedAt = DateTime.Now;
                //TODO: add logic for WHO created

                _hrDepartmentService.Update(department);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public HrDepartment MapFrom(HrDepartmentVM model)
        {
            return new HrDepartment
            {
                Id = model.Id,
                Code_1C = model.Code_1C,
                TitleRu = model.TitleRu,
                TitleUz = model.TitleUz,
                TitleEn = model.TitleEn
            };
        }
        public HrDepartmentVM MapTo(HrDepartment model)
        {
            return new HrDepartmentVM
            {
                Id = model.Id,
                Code_1C = model.Code_1C,
                TitleRu = model.TitleRu,
                TitleUz = model.TitleUz,
                TitleEn = model.TitleEn
            };
        }

    }
}
