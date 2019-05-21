using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Model
{
    public class HrEmployee
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Code_1C { get; set; }

        [Required]
        public string ID_1C { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string PlaceOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string EmailLogin { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        [ForeignKey("Position")]
        public int? PositionId { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public DateTime? PositionStartDate { get; set; }

        public bool IsActive { get; set; }

        public bool ComplietedRuCv { get; set; }

        public bool ComplietedUzCv { get; set; }

        public string ImageName { get; set; }


        public string ImageNameContentType { get; set; }


        [Column(TypeName = "varbinary(max)")]
        public byte[] ImageNameContent { get; set; }

        public string PhoneNo { get; set; }
        public string ExternalPhoneNo { get; set; }

        public string PassportNo { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public string PassportIssuePlace { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public string FullNameGenitive { get; set; }


        public virtual HrDepartment Department { get; set; }
        public virtual HrPosition Position { get; set; }

        public virtual ICollection<HrCvDetail> CvDetails { get; set; }
        public virtual ICollection<HrEmployeeDocument> Documents { get; set; }
    }
}
