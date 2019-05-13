using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INTRANET.Models
{
    public class HrEmployeeVM
    {
     
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Code from 1C cannot be empty")]
        [DisplayName("Code from 1C")]
        public string Code_1C { get; set; }
        [Required]
        public string ID_1C { get; set; }
        [Required(ErrorMessage = "Date of birth cannot be empty")]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Place of birth cannot be empty")]
        [DisplayName("Place of birth")]
        public string PlaceOfBirth { get; set; }
        [Required(ErrorMessage = "Gender cannot be empty")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Address cannot be empty")]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email login cannot be empty")]
        [DisplayName("Email login")]
        public string EmailLogin { get; set; }
        [Required(ErrorMessage = "Entry date cannot be empty")]
        [DisplayName("Entry date")]
        public DateTime EntryDate { get; set; }
        [DisplayName("Leave date")]
        public DateTime? LeaveDate { get; set; }
        [DisplayName("Position Id")]
        public int? PositionId { get; set; }
        [DisplayName("Department Id")]
        public int? DepartmentId { get; set; }
        [DisplayName("Position start date")]
        public DateTime? PositionStartDate { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DisplayName("Passport Number")]
        public string PassportNo { get; set; }
        [DisplayName("Passport Issue Date")]
        public DateTime? PassportIssueDate { get; set; }
        [DisplayName("Passport Issue Place")]
        public string PassportIssuePlace { get; set; }

        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Positions { get; set; }

    }
}