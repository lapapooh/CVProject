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
    public class HrCvHintController : Controller
    {
        IHrCvHintTextService _hrCvHintService { get; set; }

        public HrCvHintController(IHrCvHintTextService hrCvHintService)
        {
            _hrCvHintService = hrCvHintService;
        }

        public ActionResult Index()
        {
            var model = _hrCvHintService.GetAll().Select(MapTo).ToList();
            return View(model);

        }
        [HttpGet]
        public ActionResult Create()
        {

            return View(new HrCvHintVM());
        }

        [HttpPost]
        public ActionResult Create(HrCvHintVM model)
        {
            if (ModelState.IsValid)
            {
                _hrCvHintService.Create(MapFrom(model));
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            _hrCvHintService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(MapTo(_hrCvHintService.GetByID(id)));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(MapTo(_hrCvHintService.GetByID(id)));
        }

        [HttpPost]
        public ActionResult Edit(HrCvHintVM model)
        {
            _hrCvHintService.Update(MapFrom(model));
            return RedirectToAction("Index");
        }

        public HrCvHintText MapFrom(HrCvHintVM model)
        {
            return new HrCvHintText
            {
                Id = model.Id,
                Field = model.Field,
                FieldName = model.FieldName,
                Language = model.Language,
                Text = model.Text
            };
        }
        public HrCvHintVM MapTo(HrCvHintText model)
        {
            return new HrCvHintVM
            {
                Id = model.Id,
                Language = model.Language,
                Field = model.Field,
                FieldName = model.FieldName,
                Text = model.Text
            };
        }

    }
}