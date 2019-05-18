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
        [ValidateAntiForgeryToken]
        public ActionResult Create(HrPositionVM model)
        {
            if (ModelState.IsValid)
            {
                var position = MapFrom(model);
                position.CreatedAt = DateTime.Now;
                //TODO: add logic for WHO created
                _hrPositionService.Create(position);
                return RedirectToAction("Index");
            }

            return View(model);


        }
        [HttpGet]
        public ActionResult Edit(int id) {

            var position = _hrPositionService.GetByID(id);
            if(position == null)
                return RedirectToAction("Index");

            return View(MapTo(position));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HrPositionVM model) {

            if (ModelState.IsValid)
            {
                var position = _hrPositionService.GetByID(model.Id);
                if (position == null)
                    return RedirectToAction("Index");

                //do not update all fields with MapFrom method - will override created fields in database
                position.Code_1C = model.Code_1C;
                position.TitleEn = model.TitleEn;
                position.TitleRu = model.TitleRu;
                position.TitleUz = model.TitleUz;

                position.ModifiedAt = DateTime.Now;
                //TODO: add logic for WHO modified

                _hrPositionService.Update(position);
                return RedirectToAction("Index");
            }

            return View(model);
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