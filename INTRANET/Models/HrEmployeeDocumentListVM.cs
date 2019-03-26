using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrEmployeeDocumentListVM
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}