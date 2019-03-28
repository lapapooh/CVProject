using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrEmployeeListVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public bool HasFilledRuCV { get; set; }
        public bool HasFilledUzCV { get; set; }
    }
}