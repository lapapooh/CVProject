using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrCvVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public HrCvLanguage Language { get; set; }
        
    }
}