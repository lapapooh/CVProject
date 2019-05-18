using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INTRANET.Models
{
    public class HrImportCvVM
    {
        public int EmployeeId { get; set; }
        public HrCvLanguage Language { get; set; }

        public List<SelectListItem> Employees { get; set; }
    }
}