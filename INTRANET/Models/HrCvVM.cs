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
                Value = "Отаси",
                Text = "Отаси"
            },
            new SelectListItem
            {
                Value = "Онаси",
                Text = "Онаси"
            },
            new SelectListItem
            {
                Value = "Опаси",
                Text = "Опаси"
            },
            new SelectListItem
            {
                Value = "Акаси",
                Text = "Акаси"
            },
             new SelectListItem
            {
                Value = "Синглиси",
                Text = "Синглиси"
            },
              new SelectListItem
            {
                Value = "Укаси",
                Text = "Укаси"
            },
            new SelectListItem
            {
                Value = "Турмуш ўртоғи",
                Text = "Турмуш ўртоғи"
            },
            new SelectListItem
            {
                Value = "Ўғли",
                Text = "Ўғли"
            },
            new SelectListItem
            {
                Value = "Қизи",
                Text = "Қизи"
            },
            new SelectListItem
            {
                Value = "Қайнотаси",
                Text = "Қайнотаси"
            },
            new SelectListItem
            {
                Value = "Қайнонаси",
                Text = "Қайнонаси"
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
                Value = "нет",
                Text = "нет"
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
                Value = "йўқ",
                Text = "йўқ"
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

        #region "Awards"

        private readonly List<SelectListItem> AwardsRu = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "нет",
                Text = "нет"
            },
            //new SelectListItem
            //{
            //    Value = "Звание «Ўзбекистон Қаҳрамони»",
            //    Text = "Звание «Ўзбекистон Қаҳрамони»"
            //},
            //new SelectListItem
            //{
            //    Value = "Почетные звания Республики Узбекистан",
            //    Text = "Почетные звания Республики Узбекистан"
            //},
            //new SelectListItem
            //{
            //    Value = "Ордена Республики Узбекистан",
            //    Text = "Ордена Республики Узбекистан"
            //},
            //new SelectListItem
            //{
            //    Value = "Медали Республики Узбекистан",
            //    Text = "Медали Республики Узбекистан"
            //},
            //new SelectListItem
            //{
            //    Value = "Почетная грамота Республики Узбекистан",
            //    Text = "Почетная грамота Республики Узбекистан"
            //},
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси санъат арбоби»",
                Text = "«Ўзбекистон Республикаси санъат арбоби»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси фан арбоби»",
                Text = "«Ўзбекистон Республикаси фан арбоби»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон ифтихори»",
                Text = "«Ўзбекистон ифтихори»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ артисти»",
                Text = "«Ўзбекистон Республикаси халқ артисти»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ бахшиси»",
                Text = "«Ўзбекистон Республикаси халқ бахшиси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ ёзувчиси»",
                Text = "«Ўзбекистон Республикаси халқ ёзувчиси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ рассоми»",
                Text = "«Ўзбекистон Республикаси халқ рассоми»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ устаси»",
                Text = "«Ўзбекистон Республикаси халқ устаси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ шоири»",
                Text = "«Ўзбекистон Республикаси халқ шоири»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ ўқитувчиси»",
                Text = "«Ўзбекистон Республикаси халқ ўқитувчиси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ ҳофизи»",
                Text = "«Ўзбекистон Республикаси халқ ҳофизи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида коммунал, маиший ва савдо соҳасида хизмат кўрсатган ходим»",
                Text = "«Ўзбекистон Республикасида коммунал, маиший ва савдо соҳасида хизмат кўрсатган ходим»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган алоқа ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган алоқа ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган артист»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган артист»"
            },
             new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган ёшлар мураббийси»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган ёшлар мураббийси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган журналист»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган журналист»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган ирригатор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган ирригатор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган ихтирочи ва рационализатор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган ихтирочи ва рационализатор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган иқтисодчи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган иқтисодчи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган маданият ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган маданият ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган меъмор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган меъмор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган пахтакор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган пахтакор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган пиллачи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган пиллачи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган саноат ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган саноат ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган соқлиқни сақлаш ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган соқлиқни сақлаш ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган спорт устози»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган спорт устози»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган спортчи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган спортчи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган транспорт ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган транспорт ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган фуқаро авиацияси ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган фуқаро авиацияси ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган халқ таълими ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган халқ таълими ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган чорвадор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган чорвадор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган юрист»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган юрист»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган қишлоқ хўжалик ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган қишлоқ хўжалик ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган қурувчи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган қурувчи»"
            },
            new SelectListItem
            {
                Value = "Орден «Мустақиллик»",
                Text = "Орден «Мустақиллик»"
            },
            new SelectListItem
            {
                Value = "Орден «Амир Темур»",
                Text = "Орден «Амир Темур»"
            },
            new SelectListItem
            {
                Value = "Орден «Жалолиддин Мангуберди»",
                Text = "Орден «Жалолиддин Мангуберди»"
            },
            new SelectListItem
            {
                Value = "Орден «Буюк хизматлари учун»",
                Text = "Орден «Буюк хизматлари учун»"
            },
            new SelectListItem
            {
                Value = "Орден «Эл-юрт ҳурмати»",
                Text = "Орден «Эл-юрт ҳурмати»"
            },
            new SelectListItem
            {
                Value = "Орден «Фидокорона хизматлари учун»",
                Text = "Орден «Фидокорона хизматлари учун»"
            },
            new SelectListItem
            {
                Value = "Орден «Меҳнат шуҳрати»",
                Text = "Орден «Меҳнат шуҳрати»"
            },
            new SelectListItem
            {
                Value = "Орден «Соқлом авлод учун» I и II степени",
                Text = "Орден «Соқлом авлод учун» I и II степени"
            },
            new SelectListItem
            {
                Value = "Орден «Шон-шараф» I и II степени",
                Text = "Орден «Шон-шараф» I и II степени"
            },
            new SelectListItem
            {
                Value = "Орден «Дўстлик»",
                Text = "Орден «Дўстлик»"
            },
            new SelectListItem
            {
                Value = "Орден «Мардлик»",
                Text = "Орден «Мардлик»"
            },
            new SelectListItem
            {
                Value = "Медаль «Жасорат»",
                Text = "Медаль «Жасорат»"
            },
            new SelectListItem
            {
                Value = "Медаль «Содиқ хизматлари учун»",
                Text = "Медаль «Содиқ хизматлари учун»"
            },
            new SelectListItem
            {
                Value = "Медаль «Келажак бунёдкори»",
                Text = "Медаль «Келажак бунёдкори»"
            },
            new SelectListItem
            {
                Value = "Медаль «Шуҳрат»",
                Text = "Медаль «Шуҳрат»"
            },
        };

        private readonly List<SelectListItem> AwardsUz = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "йўқ",
                Text = "йўқ"
            },
            //new SelectListItem
            //{
            //    Value = "«Ўзбекистон Қаҳрамони» унвонлари",
            //    Text = "«Ўзбекистон Қаҳрамони» унвонлари"
            //},
            //new SelectListItem
            //{
            //    Value = "Ўзбекистон Республикаси фахрий унвонлари",
            //    Text = "Ўзбекистон Республикаси фахрий унвонлари"
            //},
            //new SelectListItem
            //{
            //    Value = "Ўзбекистон Республикаси орденлари",
            //    Text = "Ўзбекистон Республикаси орденлари"
            //},
            //new SelectListItem
            //{
            //    Value = "Ўзбекистон Республикаси медаллари",
            //    Text = "Ўзбекистон Республикаси медаллари"
            //},
            //new SelectListItem
            //{
            //    Value = "Ўзбекистон Республикаси фахрий ёрлиқлари",
            //    Text = "Ўзбекистон Республикаси фахрий ёрлиқлари"
            //},
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси санъат арбоби»",
                Text = "«Ўзбекистон Республикаси санъат арбоби»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси фан арбоби»",
                Text = "«Ўзбекистон Республикаси фан арбоби»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон ифтихори»",
                Text = "«Ўзбекистон ифтихори»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ артисти»",
                Text = "«Ўзбекистон Республикаси халқ артисти»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ бахшиси»",
                Text = "«Ўзбекистон Республикаси халқ бахшиси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ ёзувчиси»",
                Text = "«Ўзбекистон Республикаси халқ ёзувчиси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ рассоми»",
                Text = "«Ўзбекистон Республикаси халқ рассоми»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ устаси»",
                Text = "«Ўзбекистон Республикаси халқ устаси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ шоири»",
                Text = "«Ўзбекистон Республикаси халқ шоири»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ ўқитувчиси»",
                Text = "«Ўзбекистон Республикаси халқ ўқитувчиси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикаси халқ ҳофизи»",
                Text = "«Ўзбекистон Республикаси халқ ҳофизи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида коммунал, маиший ва савдо соҳасида хизмат кўрсатган ходим»",
                Text = "«Ўзбекистон Республикасида коммунал, маиший ва савдо соҳасида хизмат кўрсатган ходим»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган алоқа ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган алоқа ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган артист»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган артист»"
            },
             new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган ёшлар мураббийси»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган ёшлар мураббийси»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган журналист»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган журналист»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган ирригатор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган ирригатор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган ихтирочи ва рационализатор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган ихтирочи ва рационализатор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган иқтисодчи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган иқтисодчи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган маданият ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган маданият ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган меъмор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган меъмор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган пахтакор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган пахтакор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган пиллачи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган пиллачи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган саноат ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган саноат ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган соқлиқни сақлаш ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган соқлиқни сақлаш ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган спорт устози»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган спорт устози»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган спортчи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган спортчи»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган транспорт ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган транспорт ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган фуқаро авиацияси ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган фуқаро авиацияси ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган халқ таълими ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган халқ таълими ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган чорвадор»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган чорвадор»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган юрист»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган юрист»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган қишлоқ хўжалик ходими»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган қишлоқ хўжалик ходими»"
            },
            new SelectListItem
            {
                Value = "«Ўзбекистон Республикасида хизмат кўрсатган қурувчи»",
                Text = "«Ўзбекистон Республикасида хизмат кўрсатган қурувчи»"
            },
            new SelectListItem
            {
                Value = "«Мустақиллик» ордени",
                Text = "«Мустақиллик» ордени"
            },
            new SelectListItem
            {
                Value = "«Амир Темур» ордени",
                Text = "«Амир Темур» ордени"
            },
            new SelectListItem
            {
                Value = "«Жалолиддин Мангуберди» ордени",
                Text = "«Жалолиддин Мангуберди» ордени"
            },
            new SelectListItem
            {
                Value = "«Буюк хизматлари учун» ордени",
                Text = "«Буюк хизматлари учун» ордени"
            },
            new SelectListItem
            {
                Value = "«Эл-юрт ҳурмати» ордени",
                Text = "«Эл-юрт ҳурмати» ордени"
            },
            new SelectListItem
            {
                Value = "«Фидокорона хизматлари учун» ордени",
                Text = "«Фидокорона хизматлари учун» ордени"
            },
            new SelectListItem
            {
                Value = "Орден «Меҳнат шуҳрати»",
                Text = "Орден «Меҳнат шуҳрати»"
            },
            new SelectListItem
            {
                Value = "I ва II даражали «Соқлом авлод учун» ордени",
                Text = "I ва II даражали «Соқлом авлод учун» ордени"
            },
            new SelectListItem
            {
                Value = "I ва II даражали «Шон-шараф» ордени",
                Text = "I ва II даражали «Шон-шараф» ордени"
            },
            new SelectListItem
            {
                Value = "«Дўстлик» ордени",
                Text = "«Дўстлик» ордени"
            },
            new SelectListItem
            {
                Value = "«Мардлик» ордени",
                Text = "«Мардлик» ордени"
            },
            new SelectListItem
            {
                Value = "«Жасорат» медали",
                Text = "«Жасорат» медали"
            },
            new SelectListItem
            {
                Value = "«Содиқ хизматлари учун» медали",
                Text = "«Содиқ хизматлари учун» медали"
            },
            new SelectListItem
            {
                Value = "«Келажак бунёдкори» медали",
                Text = "«Келажак бунёдкори» медали"
            },
            new SelectListItem
            {
                Value = "«Шуҳрат» медали",
                Text = "«Шуҳрат» медали"
            },
        };

        public List<SelectListItem> AwardsList
        {
            get
            {
                if (Language == HrCvLanguage.Ru)
                    return AwardsRu;

                return AwardsUz;
            }
        }


        #endregion

        #endregion

    }
}