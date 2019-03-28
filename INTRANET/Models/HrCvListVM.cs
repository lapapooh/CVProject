using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INTRANET.Models
{
    public class HrCvListVM
    {
        public HrCvListVM()
        {
            //safety, avoid null reference exceptions
            Departments = new List<SelectListItem>();
            Positions = new List<SelectListItem>();
        }
        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Positions { get; set; }
    }
}