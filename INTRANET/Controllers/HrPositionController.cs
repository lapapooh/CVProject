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
    public class HrPositionController : Controller
    {
        IHrPositionService _hrPositionService { get; set; }
        public HrPositionController(IHrPositionService hrPositionService)
        {
            _hrPositionService = hrPositionService;
        }

        public ActionResult Index()
        {
            var model = _hrPositionService.GetAll().Select(MapTo).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new HrPositionVM());
        }

        [HttpPost]
        public ActionResult Create(HrPositionVM model)
        {
            if (ModelState.IsValid)
            {
                _hrPositionService.Create(MapFrom(model));
                return RedirectToAction("Index");
            }
            else
            {

                return View(model);
            }

        }
        [HttpGet]
        public ActionResult Edit(int id) {
            return View(MapTo(_hrPositionService.GetByID(id)));
        }

        [HttpPost]
        public ActionResult Edit(HrPositionVM model) {
            _hrPositionService.Update(MapFrom(model));
            return RedirectToAction("Index");
        }       

        public HrPositionVM MapTo(HrPosition model)
        {
            return new HrPositionVM
            {
                Id = model.Id,
                Code_1C = model.Code_1C,
                TitleRu = model.TitleRu,
                TitleEn = model.TitleEn,
                TitleUz = model.TitleUz
            };
        }

        public HrPosition MapFrom(HrPositionVM model)
        {
            return new HrPosition
            {
                Id = model.Id,
                Code_1C = model.Code_1C,
                TitleRu = model.TitleRu,
                TitleEn = model.TitleEn,
                TitleUz = model.TitleUz
            };
        }
    }
}