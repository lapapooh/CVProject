using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Model
{
    public class HrCvDetail
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [Required]
        public HrCvLanguage Language { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PartyMembership { get; set; }
        public string EducationDegree { get; set; }
        public string EducationSpeciality { get; set; }
        public string AcademicDegree { get; set; }
        public string AcademicTitle { get; set; }
        public string Languages { get; set; }

        public virtual HrEmployee Employee { get; set; }
        public virtual ICollection<HrCvAward> Awards { get; set; }
        public virtual ICollection<HrCvEduction> Educations { get; set; }
        public virtual ICollection<HrCvLabor> Labors { get; set; }
        public virtual ICollection<HrCvMembership> Memberships { get; set; }
        public virtual ICollection<HrCvRelative> Relatives { get; set; }
    }

    public enum HrCvLanguage
    {
        Ru = 1,
        Uz = 2
    }
}
