using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //for easy access
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
        public List<HrCvAwardVM> AwardList { get; set; }
        public List<string> MembershipList { get; set; }
        public List<HrCvLaborVM> LaborDetailList { get; set; }
        public List<HrCvRelativesVM> RelativesDetailsList { get; set; }


        //to display translation of field labels and hint texts

        public List<HrCvHintText> HintTexts { get; set; }

        public string GetFieldName(HrCvField field)
        {
            return HintTexts.FirstOrDefault(f => f.Field == field)?.FieldName ?? "";
        }

        public string GetFieldHint(HrCvField field)
        {
            return HintTexts.FirstOrDefault(f => f.Field == field)?.Text ?? "";
        }

        #region "Dropdowns"

        #region "Relationship Degree"

        private readonly List<SelectListItem> RelationshipDegreeRu = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "",
                Text = "Выберите из списка"
            },
            new SelectListItem
            {
                Value = "Отец",
                Text = "Отец"
            },
            new SelectListItem
            {
                Value = "Мать",
                Text = "Мать"
            },
            new SelectListItem
            {
                Value = "Сестра",
                Text = "Сестра"
            },
            new SelectListItem
            {
                Value = "Брат",
                Text = "Брат"
            },
            new SelectListItem
            {
                Value = "Супруга",
                Text = "Супруга"
            },
            new SelectListItem
            {
                Value = "Сын",
                Text = "Сын"
            },
            new SelectListItem
            {
                Value = "Дочь",
                Text = "Дочь"
            },
            new SelectListItem
            {
                Value = "Отец супруги",
                Text = "Отец супруги"
            },
            new SelectListItem
            {
                Value = "Мать супруги",
                Text = "Мать супруги"
            },
        };

        private readonly List<SelectListItem> RelationshipDegreeUz = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "",
                Text = ""
            },
            new SelectListItem
            {
                Value = "Отец",
                Text = "Отец"
            },
            new SelectListItem
            {
                Value = "Мать",
                Text = "Мать"
            },
            new SelectListItem
            {
                Value = "Сестра",
                Text = "Сестра"
            },
            new SelectListItem
            {
                Value = "Брат",
                Text = "Брат"
            },
            new SelectListItem
            {
                Value = "Супруга",
                Text = "Супруга"
            },
            new SelectListItem
            {
                Value = "Сын",
                Text = "Сын"
            },
            new SelectListItem
            {
                Value = "Дочь",
                Text = "Дочь"
            },
            new SelectListItem
            {
                Value = "Отец супруги",
                Text = "Отец супруги"
            },
            new SelectListItem
            {
                Value = "Мать супруги",
                Text = "Мать супруги"
            },
        };

        public List<SelectListItem> RelationshipDegrees
        {
            get
            {
                if (Language == HrCvLanguage.Ru)
                    return RelationshipDegreeRu;

                return RelationshipDegreeUz;
            }
        }

        #endregion

        #region "Education Degree"

        private readonly List<SelectListItem> EducationDegreesRu = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "",
                Text = ""
            },
            new SelectListItem
            {
                Value = "среднее",
                Text = "среднее"
            },
            new SelectListItem
            {
                Value = "средное специальное",
                Text = "средное специальное"
            },
            new SelectListItem
            {
                Value = "высшее",
                Text = "высшее"
            }
        };

        private readonly List<SelectListItem> EducationDegreesUz = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "",
                Text = ""
            },
            new SelectListItem
            {
                Value = "ўрта",
                Text = "ўрта"
            },
            new SelectListItem
            {
                Value = "ўрта-маҳсус",
                Text = "ўрта-маҳсус"
            },
            new SelectListItem
            {
                Value = "олий",
                Text = "олий"
            }
        };

        public List<SelectListItem> EducationDegrees
        {
            get
            {
                if (Language == HrCvLanguage.Ru)
                    return EducationDegreesRu;

                return EducationDegreesUz;
            }
        }


        #endregion

        #region "Academic Title"

        private readonly List<SelectListItem> AcademicTitlesRu = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "нет",
                Text = "нет"
            },
            new SelectListItem
            {
                Value = "профессор",
                Text = "профессор"
            },
            new SelectListItem
            {
                Value = "доцент",
                Text = "доцент"
            }
        };

        private readonly List<SelectListItem> AcademicTitlesUz = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "йўқ",
                Text = "йўқ"
            },
            new SelectListItem
            {
                Value = "профессор",
                Text = "профессор"
            },
            new SelectListItem
            {
                Value = "доцент",
                Text = "доцент"
            }
        };

        public List<SelectListItem> AcademicTitles
        {
            get
            {
                if (Language == HrCvLanguage.Ru)
                    return AcademicTitlesRu;

                return AcademicTitlesUz;
            }
        }

        #endregion

        #endregion

    }
}