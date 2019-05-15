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
            var hints = _hrCvHintService.GetAllQueryable();

            //lets be proactive and add defaults
            //so that CV will not be initially empty
            if(!hints.Any())
            {
                _hrCvHintService.CreateDefaults(HrCvLanguage.Ru);
                _hrCvHintService.CreateDefaults(HrCvLanguage.Uz);

                hints = _hrCvHintService.GetAllQueryable();
            }

            var model = hints.Select(MapTo).ToList();
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
        public ActionResult Edit(int id)
        {
            var hint = _hrCvHintService.GetByID(id);

            if (hint == null)//safety check
                return RedirectToAction("Index");

            return View(MapTo(hint));
        }

        [HttpPost]
        public ActionResult Edit(HrCvHintVM model)
        {
            if(ModelState.IsValid)
            {
                _hrCvHintService.Update(MapFrom(model));
                return RedirectToAction("Index");
            }
            return View(model);

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