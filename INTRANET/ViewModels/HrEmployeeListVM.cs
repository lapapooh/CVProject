using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.ViewModels
{
    public class HrEmployeeListVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public bool HasFilledRuCV { get; set; }
        public bool HasFilledUzCV { get; set; }
    }
}