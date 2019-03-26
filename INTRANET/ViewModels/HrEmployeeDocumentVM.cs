using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.ViewModels
{
    public class HrEmployeeDocumentVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<HrEmployeeDocument> HrEmployeeDocuments { get; set; }
    }
}