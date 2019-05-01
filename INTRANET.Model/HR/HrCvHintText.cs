using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Model
{
    public class HrCvHintText
    {
        public int Id { get; set; }
        [Required]
        public HrCvLanguage Language { get; set; }
        [Required]
        public HrCvField Field { get; set; }
        public string Text { get; set; }
        public string FieldName { get; set; }
    }

    public enum HrCvField
    {
        Photo = 1,
        Phone = 2,
        ExternalPhone = 3,
        PlaceOfBirth = 4,
        Nationality = 5,
        PartyMembership = 6,
        EducationDegree = 7,
        EducationSpeciality = 8,
        EducationDetails = 9,
        AcademicDegree = 10,
        AcademicTitle = 11,
        Awards = 12,
        Membership = 13,
        Labor = 14,
        Relatives = 15
    }
}
