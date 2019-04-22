using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrEmployeeDocumentListVM
    {
        public bool IsHrUser { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<HrEmployeeDocumentVM> HrEmployeeDocuments { get; set; }
        public bool CompletedRuCv { get; set; }
        public bool CompletedUzCv { get; set; }
    }

    public class HrEmployeeDocumentVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
    }
}