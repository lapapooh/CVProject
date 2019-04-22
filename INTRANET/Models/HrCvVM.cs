using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrCvVM
    {
        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        [Required]
        public HrCvLanguage Language { get; set; }

        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string ImageContent { get; set; }

        //fr easy access
        public bool HasImage
        {
            get
            {
                return !string.IsNullOrEmpty(ImageContent);
            }
        }

        public DateTime EntryDate { get; set; }

        [Required(ErrorMessage = "Enter phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Enter external phone number")]
        public string ExternalPhone { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PlaceOfBirth { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string PartyMembership { get; set; }
        [Required]
        public string EducationDegree { get; set; }
        [Required]
        public string EducationSpeciality { get; set; }
        [Required]
        public string AcademicDegree { get; set; }
        [Required]
        public string AcademicTitle { get; set; }
        [Required]
        public string Languages { get; set; }

        public List<string> EducationList { get; set; }
        public List<string> AwardList { get; set; }
        public List<string> MembershipList { get; set; }
    }
}