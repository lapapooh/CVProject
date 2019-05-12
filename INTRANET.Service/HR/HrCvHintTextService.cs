using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository.Interfaces;
using INTRANET.Model;
using INTRANET.Service.Interfaces;

namespace INTRANET.Service
{
    public class HrCvHintTextService : BaseService<HrCvHintText>, IHrCvHintTextService
    {
        public HrCvHintTextService(IHrCvHintTextRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }

        public void Create(HrCvHintText model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Update(HrCvHintText model)
        {
            this._repository.Update(model);
            Save();
        }

        public void Delete(int id)
        {
            this._repository.Delete(c=> c.Id == id);
            Save();
        }

        public IEnumerable<HrCvHintText> GetByLanguage(HrCvLanguage language)
        {
            return GetAll().Where(c => c.Language == language);
        }

        public IQueryable<HrCvHintText> GetAllQueryable()
        {
            return this._repository.GetAny();
        }

        //methods to add default translation for CV fields and annotations
        public void CreateDefaults(HrCvLanguage language)
        {
            if (language == HrCvLanguage.Ru)
                CreateDefaultsRu();
            else
                CreateDefaultsUz();
        }

        private void CreateDefaultsRu()
        {
            this.Create(new HrCvHintText
            {
                Field = HrCvField.Photo,      
                Language = HrCvLanguage.Ru,
                FieldName = "Фото",
                Text = "3х4, белый фон",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Phone,
                Language = HrCvLanguage.Ru,
                FieldName = "Телефон",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.ExternalPhone,
                Language = HrCvLanguage.Ru,
                FieldName = "Телефон (внешний)",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.PlaceOfBirth,
                Language = HrCvLanguage.Ru,
                FieldName = "Место рождения",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Nationality,
                Language = HrCvLanguage.Ru,
                FieldName = "Национальность",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.PartyMembership,
                Language = HrCvLanguage.Ru,
                FieldName = "Партийность",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationDegree,
                Language = HrCvLanguage.Ru,
                FieldName = "Образование",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationSpeciality,
                Language = HrCvLanguage.Ru,
                FieldName = "Специальность по образованию",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationList,
                Language = HrCvLanguage.Ru,
                FieldName = "Окончил (когда и что)",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AcademicDegree,
                Language = HrCvLanguage.Ru,
                FieldName = "Ученая степень",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AcademicTitle,
                Language = HrCvLanguage.Ru,
                FieldName = "Ученое звание",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AwardsList,
                Language = HrCvLanguage.Ru,
                FieldName = "Правительственные награды",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.MembershipList,
                Language = HrCvLanguage.Ru,
                FieldName = "Является ли народным депутатом, членом центральных, республиканских, областнх, городских, районных и других выборных органов",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.LaborList,
                Language = HrCvLanguage.Ru,
                FieldName = "Трудовая деятельность",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList,
                Language = HrCvLanguage.Ru,
                FieldName = "Сведения о близких родственниках",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Department,
                Language = HrCvLanguage.Ru,
                FieldName = "Отдел",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Position,
                Language = HrCvLanguage.Ru,
                FieldName = "Должность",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.StartDate,
                Language = HrCvLanguage.Ru,
                FieldName = "Дата начала работы в должности",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.DateOfBirth,
                Language = HrCvLanguage.Ru,
                FieldName = "Дата рождения",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationList_Educations,
                Language = HrCvLanguage.Ru,
                FieldName = "",
                Text = "Например: 1982, Ташкентский государственный университет",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AwardsList_Year,
                Language = HrCvLanguage.Ru,
                FieldName = "Год",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AwardsList_Award,
                Language = HrCvLanguage.Ru,
                FieldName = "Награда",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.MembershipList_Membership,
                Language = HrCvLanguage.Ru,
                FieldName = "",
                Text = "Например: 2010 - н.в. - депутат Ташкентского областного Совета народных депутатов Узбекистана, Член Сената Олий Мажлиса Республики",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.LaborList_Years,
                Language = HrCvLanguage.Ru,
                FieldName = "Годы занятости",
                Text = "Например: 1977-1982",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.LaborList_Labor,
                Language = HrCvLanguage.Ru,
                FieldName = "Место работы и должность",
                Text = "Например: студент Ташкентского государственного университета",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_FullName,
                Language = HrCvLanguage.Ru,
                FieldName = "ФИО",
                Text = "Если фамилия была изменена, фамилия также должна отображаться в скобках. Например: Каримова(Азизова) Сайёра Тургуновна",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_BirthDateAndPlace,
                Language = HrCvLanguage.Ru,
                FieldName = "Год и место рождения",
                Text = "год рождения, район (город) должны быть четко указаны. Например: 1935 г.,Ташкентская область, Кибрайский район",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_Degree,
                Language = HrCvLanguage.Ru,
                FieldName = "Отношения",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_Labor,
                Language = HrCvLanguage.Ru,
                FieldName = "Место работы и должность",
                Text = "Место работы и должность должны быть полностью указаны. Надо обратить внимание на своевременность. В случае смерти, год смерти и предыдущая должность(даже на пенсии) (без слов «до», «с»). Например: Пенсионер(доцент Ташкентского государственного экономического университета) 2000 г.умер(преподаватель Ташкентского государственного экономического университета)",
            });


            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_Address,
                Language = HrCvLanguage.Ru,
                FieldName = "Место жительства",
                Text = "Названия городов, поселков, улицы и массива должно быть указано. Например: Андижанская область, Асакинский район, собрание жителей села Ипаклик, дом 5",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Languages,
                Language = HrCvLanguage.Ru,
                FieldName = "Владение языками",
                Text = "Например: Английский, Русский",
            });

        }

        private void CreateDefaultsUz()
        {
            this.Create(new HrCvHintText
            {
                Field = HrCvField.Photo,
                Language = HrCvLanguage.Uz,
                FieldName = "Rasm",
                Text = "Oq fondagi 3x4 rasm, Oxirgi 3 oy davomida olingan rangli fotosurat, Elektron ko’rinishda(rasmiy kiyimda)",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Phone,
                Language = HrCvLanguage.Uz,
                FieldName = "Telefon (mobile)",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.ExternalPhone,
                Language = HrCvLanguage.Uz,
                FieldName = "Telefon (external)",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.PlaceOfBirth,
                Language = HrCvLanguage.Uz,
                FieldName = "Tug’ilgan joyi (viloyat, tuman)",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Nationality,
                Language = HrCvLanguage.Uz,
                FieldName = "Millati",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.PartyMembership,
                Language = HrCvLanguage.Uz,
                FieldName = "Partiyaviyligi",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationDegree,
                Language = HrCvLanguage.Uz,
                FieldName = "Ma’lumoti",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationSpeciality,
                Language = HrCvLanguage.Uz,
                FieldName = "Ma’lumoti bo’yicha mutaxassisligi",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationList,
                Language = HrCvLanguage.Uz,
                FieldName = "Tamomlagan",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AcademicDegree,
                Language = HrCvLanguage.Uz,
                FieldName = "Ilmiy darajasi",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AcademicTitle,
                Language = HrCvLanguage.Uz,
                FieldName = "Ilmiy unvoni",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AwardsList,
                Language = HrCvLanguage.Uz,
                FieldName = "Davlat mukofotlari bilan taqdirlanganmi (qanaqa)",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.MembershipList,
                Language = HrCvLanguage.Uz,
                FieldName = "Xalq deputatlari respublika, viloyat, shahar va tuman Kengashi deputatimi yoki boshqa saylanadigan organlarning a’zosimi(to’liq ko’rsatilishi lozim)",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.LaborList,
                Language = HrCvLanguage.Uz,
                FieldName = "MEHNAT FAOLIYATI",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList,
                Language = HrCvLanguage.Uz,
                FieldName = "QARINDOSHLARI HAQIDA",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Department,
                Language = HrCvLanguage.Uz,
                FieldName = "Bo’lim",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Position,
                Language = HrCvLanguage.Uz,
                FieldName = "Lavozimi",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.StartDate,
                Language = HrCvLanguage.Uz,
                FieldName = "Ish boshlagan sanasi",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.DateOfBirth,
                Language = HrCvLanguage.Uz,
                FieldName = "Tug’ilgan yili",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.EducationList_Educations,
                Language = HrCvLanguage.Uz,
                FieldName = "",
                Text = "Masalam: 1982 y, Toshkent davlat universiteti (kunduzgi)",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AwardsList_Year,
                Language = HrCvLanguage.Uz,
                FieldName = "Yil",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.AwardsList_Award,
                Language = HrCvLanguage.Uz,
                FieldName = "Mukofot",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.MembershipList_Membership,
                Language = HrCvLanguage.Uz,
                FieldName = "",
                Text = "Mas: 2010 yil – h.v. – Xalq deputatlari Toshkent viloyati Kengashi deputati, O’zbekiston Respublikasi Oliy Majlisi Senati a’zosi",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.LaborList_Years,
                Language = HrCvLanguage.Uz,
                FieldName = "Mehnat faoliyati",
                Text = "Muddati: 1977-1982",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.LaborList_Labor,
                Language = HrCvLanguage.Uz,
                FieldName = "Ish joyi va lavozimi",
                Text = "Mas: Toshkent davlat uniersiteti talabasi",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_FullName,
                Language = HrCvLanguage.Uz,
                FieldName = "Familiyasi, ismi, otasining ismi",
                Text = "Agar familiyasi o’zgartirilgan bo’lsa, avvalgi familiyasi ham qavs ichida ko’rsatilishi zarur. Mas: Karimova(Azizova) Sayyora Turg’unovna",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_BirthDateAndPlace,
                Language = HrCvLanguage.Uz,
                FieldName = "Tug’ilgan yili va joyi",
                Text = "tug’ilgan yili, tumani (shahri) aniq ko’rsatilishi zarur. Mas: 1935 yil, Toshkent viloyati, Qibray tumani",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_Degree,
                Language = HrCvLanguage.Uz,
                FieldName = "Qarindoshligi",
                Text = "",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_Labor,
                Language = HrCvLanguage.Uz,
                FieldName = "Ish joyi va lavozimi",
                Text = @"ish joyi va lavozimi to’liq ko’rsatilishi zarur. Ma’lumotlar taqdim etilayotgan muddatda to’g’riligiga e’tabor qaratish zarur. Vafot etgan bo’lsa, vafot etgan yili va(pensiyada bo’lsa ham) avvalgi lavozimi to’liq ko’rsatilishi(“ilgari”, “bo’lgan” so’zlarisiz) zarur.
Mas: Pensiyada(Toshkent davlat iqtisodiyot universiteti dotsenti) 2000 yil, vafot etgan(Toshkent davlat iqtisodiyot universiteti o’qituvchisi)",
            });


            this.Create(new HrCvHintText
            {
                Field = HrCvField.RelativesList_Address,
                Language = HrCvLanguage.Uz,
                FieldName = "Turar joyi",
                Text = "Turar joyida viloyat, shahar, tuman, ko’cha nomi, daha so’zlarining nomlari to’liq ko’rsatilishi zarur. Mas: Andijon viloyati, Asaka tumani, Ipakchilik qishloq fuqarolari yig’ini, 5 - uy",
            });

            this.Create(new HrCvHintText
            {
                Field = HrCvField.Languages,
                Language = HrCvLanguage.Uz,
                FieldName = "Qaysi chet tillarini biladi",
                Text = "Mas: Ingliz, rus",
            });
        }
    }
}