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
        EducationList = 9,
        AcademicDegree = 10,
        AcademicTitle = 11,
        AwardsList = 12,
        MembershipList = 13,
        LaborList = 14,
        RelativesList = 15,
        Department = 16,
        Position = 17,
        StartDate = 18,
        DateOfBirth = 19,
        EducationList_Educations = 20,
        AwardsList_Year = 21,
        AwardsList_Award = 22,
        MembershipList_Membership = 23,
        LaborList_Years = 24,
        LaborList_Labor = 25,
        RelativesList_FullName = 26,
        RelativesList_BirthDateAndPlace = 27,
        RelativesList_Degree = 28,
        RelativesList_Labor = 29,
        RelativesList_Address = 30,
        Languages = 31
    }
}
