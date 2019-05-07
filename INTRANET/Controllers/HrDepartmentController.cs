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
        public ActionResult Create(HrDepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                _hrDepartmentService.Create(MapFrom(model));
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(MapTo(_hrDepartmentService.GetByID(id)));
        }

        [HttpPost]
        public ActionResult Edit(HrDepartmentVM model)
        {
            _hrDepartmentService.Update(MapFrom(model));
            return RedirectToAction("Index");
        }

        public HrDepartment MapFrom(HrDepartmentVM model)
        {
            return new HrDepartment
            {
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
                Code_1C = model.Code_1C,
                TitleRu = model.TitleRu,
                TitleUz = model.TitleUz,
                TitleEn = model.TitleEn
            };
        }

    }
}
