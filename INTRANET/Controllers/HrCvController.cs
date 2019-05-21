//using DataTables.AspNet.Core;
//using DataTables.AspNet.Mvc5;
using INTRANET.Data;
using INTRANET.Data.Infrastructure;
using INTRANET.Data.Repository;
using INTRANET.Model;
using INTRANET.Service;
using INTRANET.Service.Interfaces;
using INTRANET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INTRANET.Common;
using System.Reflection;
using Spire.Doc;
using System.Web.Hosting;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using System.Globalization;

namespace INTRANET.Controllers
{
    public class HrCvController : Controller
    {
        public IHrEmployeeService _hrEmployeeService { get; set; }
        public IHrDepartmentService _hrDepartmentService { get; set; }
        public IHrPositionService _hrPositionService { get; set; }
        public IHrCvDetailService _hrCvDetailService { get; set; }
        public IHrCvEductionService _hrCvEducationService { get; set; }
        public IHrCvLaborService _hrCvLaborService { get; set; }
        public IHrCvRelativeService _hrCvRelativeService { get; set; }
        public IHrCvAwardService _hrCvAwardService { get; set; }
        public IHrCvMembershipService _hrCvMembershipService { get; set; }
        public IHrCvHintTextService _hrCvHintService { get; set; }


        public HrCvController(IHrEmployeeService hrEmployeeService,
            IHrDepartmentService hrDepartmentService, IHrPositionService hrPositionService, IHrCvDetailService hrCvDetailService,
            IHrCvEductionService hrCvEducationService, IHrCvLaborService hrCvLaborService, IHrCvRelativeService hrCvRelativeService,
            IHrCvAwardService hrCvAwardService, IHrCvMembershipService hrCvMembershipService, IHrCvHintTextService hrCvHintService)
        {
            _hrEmployeeService = hrEmployeeService;
            _hrDepartmentService = hrDepartmentService;
            _hrPositionService = hrPositionService;
            _hrCvDetailService = hrCvDetailService;
            _hrCvEducationService = hrCvEducationService;
            _hrCvLaborService = hrCvLaborService;
            _hrCvRelativeService = hrCvRelativeService;
            _hrCvAwardService = hrCvAwardService;
            _hrCvMembershipService = hrCvMembershipService;
            _hrCvHintService = hrCvHintService;

        }


        #region "Index methods"
        // GET: HrCv
        public ActionResult Index()
        {
            var departmentsList = _hrDepartmentService
                                    .GetAll()
                                    .Select(a => new SelectListItem
                                    {
                                        Text = a.TitleEn,
                                        Value = a.Id.ToString()
                                    }).ToList();

            var positionsList = _hrPositionService
                                    .GetAll()
                                    .Select(a => new SelectListItem
                                    {
                                        Text = a.TitleEn,
                                        Value = a.Id.ToString()
                                    }).ToList();
            var model = new HrCvListVM
            {
                Departments = departmentsList,
                Positions = positionsList
            };

            return View(model);
        }

        

        public ActionResult LoadData(int[] selectedDepartments, int[] selectedPositions)
        {
            try
            {
                //tried to use nuget library for DataTables, but it conflicts with Autofac and dependency injection
                //so this solution is done

                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form.GetValues("start").FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form.GetValues("length").FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var employeeData = _hrEmployeeService.GetAllQueryable();

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "FullName":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.FullName);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.FullName);
                            break;
                        case "DepartmentName":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.Department.TitleEn);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.Department.TitleEn);
                            break;
                        case "Position":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.Position.TitleEn);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.Position.TitleEn);
                            break;
                        case "HasFilledRuCV":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.ComplietedRuCv);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.ComplietedRuCv);
                            break;
                        case "HasFilledUzCV":
                            if (sortColumnDirection == "asc")
                                employeeData = employeeData.OrderBy(c => c.ComplietedUzCv);
                            else
                                employeeData = employeeData.OrderByDescending(c => c.ComplietedUzCv);
                            break;
                        default:
                            employeeData = employeeData.OrderBy(c => c.FullName);
                            break;
                    }
                }

                //fileting by departments and positions
                if (selectedDepartments != null && selectedDepartments.Any())
                    employeeData = employeeData.Where(
                        e => e.DepartmentId.HasValue &&
                        selectedDepartments.Contains(e.DepartmentId.Value));


                if (selectedPositions != null && selectedPositions.Any())
                    employeeData = employeeData.Where(
                        e => e.PositionId.HasValue &&
                        selectedPositions.Contains(e.PositionId.Value));



                ////Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    employeeData = employeeData.Where(m => m.FullName.Contains(searchValue));
                }

                //total number of rows count   
                recordsTotal = employeeData.Count();
                //Paging   
                //var data = employeeData.Skip(skip).Take(pageSize).ToList();
                var data = employeeData.Skip(skip).Take(pageSize).Select(MapToModel);
                //var data = employeeData.Take(pageSize).Select(MapToModel);

                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                //logging goes here
                return Json(new { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = new object[0] });
            }
        }

        [HttpPost]
        public ActionResult ChangeCvCompletionStatus(HrCvChangeCompletionStatusMode mode, int[] selectedEmployees)
        {
            try
            {
                _hrEmployeeService.ChangeCvCompletionStatus(mode, selectedEmployees);
                return Json(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                //logging goes here
                return Json(new { IsSuccess = false });
            }
        }


        public ActionResult SendEmail(string text, int[] selectedEmployees)
        {
            //validation
            if (!selectedEmployees.Any() || string.IsNullOrWhiteSpace(text))
                return Json(new { IsSuccess = false });


            List<string> emails = new List<string> { };

            foreach (var employee in selectedEmployees)
            {
                var email = _hrEmployeeService.GetByID(employee).EmailLogin;
                emails.Add(email);
            }

            if (!emails.Any())
                return Json(new { IsSuccess = false });

            try
            {
                foreach (var email in emails)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email + "@wiut.uz"); //from 1C we know only login of the email                
                    mail.From = new MailAddress("hrcvwiut@yandex.com"); //TODO: Add real user email
                    mail.Subject = "HR CV issue";

                    mail.Body = text;

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.yandex.com"; //TODO: use real credentials of sender account
                    smtp.Credentials = new System.Net.NetworkCredential
                         ("hrcvwiut@yandex.com", "wiut2019");
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return Json(new { IsSuccess = true });

            }
            catch (Exception e)
            {
                //TODO: logging of the exception
                return Json(new { IsSuccess = false });
            }

        }

        #endregion

        #region "FillCV"

        [HttpGet]
        public ActionResult FillCv(int employeeId, HrCvLanguage language)
        {
            var employee = _hrEmployeeService.GetByID(employeeId);

            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            var details = GetCvDetail(employeeId, language);

            var model = new HrCvVM
            {
                EmployeeId = employeeId,
                Language = language,
                Phone = employee.PhoneNo,
                ExternalPhone = employee.ExternalPhoneNo,
                EntryDate = employee.EntryDate,
                PlaceOfBirth = details.PlaceOfBirth,
                Nationality = details.Nationality,
                PartyMembership = details.PartyMembership,
                EducationDegree = details.EducationDegree,
                EducationSpeciality = details.EducationSpeciality,
                AcademicDegree = details.AcademicDegree,
                AcademicTitle = details.AcademicTitle,
                Languages = details.Languages,
                EducationList = _hrCvEducationService.GetForCvDetail(details.Id).Select(c => c.Education).ToList(),
                AwardList = _hrCvAwardService.GetForCvDetail(details.Id).Select(c => new HrCvAwardVM { Year=c.Year, Award=c.Award}).ToList(),
                MembershipList = _hrCvMembershipService.GetForCvDetail(details.Id).Select(c => c.Membership).ToList(),
                LaborDetailList = _hrCvLaborService.GetForCvDetail(details.Id).Select(m => new HrCvLaborVM { Years = m.Years, Description = m.Description }).ToList(),
                RelativesDetailsList = _hrCvRelativeService.GetForCvDetail(details.Id).Select(c => new HrCvRelativesVM { Degree = c.Degree, FullName = c.FullName, BirthDateAndPlace = c.BirthDateAndPlace, LaborDetails = c.LaborDetails, Address = c.Address, }).ToList()
            };

            //add default first row
            if (!model.EducationList.Any())
                model.EducationList.Add("");
            if (!model.LaborDetailList.Any())
                model.LaborDetailList.Add(new HrCvLaborVM());
            if (!model.RelativesDetailsList.Any())
                model.RelativesDetailsList.Add(new HrCvRelativesVM());
            if (!model.AwardList.Any())
                model.AwardList.Add(new HrCvAwardVM());
            if (!model.MembershipList.Any())
                model.MembershipList.Add("");

            AddDefaultsToModel(model, employee);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillCv(HrCvVM model, HttpPostedFileBase fileItem)
        {

            var employee = _hrEmployeeService.GetByID(model.EmployeeId);
            if (employee == null)
                return RedirectToAction("Index");

            


            //safety check to avoid null reference exceptions
            if (model.EducationList == null)
                model.EducationList = new List<string>();

            if (model.LaborDetailList == null)
                model.LaborDetailList = new List<HrCvLaborVM>();

            if (model.RelativesDetailsList == null)
                model.RelativesDetailsList = new List<HrCvRelativesVM>();

            var hasImageFile = fileItem != null && fileItem.ContentLength > 0;
            //validation for .png and jpg files 
            if (!hasImageFile)
            {
                //do not require image if previously there was one
                if (employee.ImageNameContent == null || employee.ImageNameContent.Length == 0) //previously no image
                    ModelState.AddModelError(string.Empty, "Photo was not selected");
            }
            else
            {
                var ex = Path.GetExtension(fileItem.FileName);
                var acceptedExtensions = new List<string> { ".png", ".jpg" };

                if (!acceptedExtensions.Contains(ex))
                {
                    ModelState.AddModelError(string.Empty, "Photo must be in .png or .jpg format");
                }
            }

            if (ModelState.IsValid)
            {
                if (hasImageFile)
                {
                    byte[] data;
                    using (var inputStream = fileItem.InputStream)
                    {
                        var memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        data = memoryStream.ToArray();
                        employee.ImageNameContent = data;
                        employee.ImageName = fileItem.FileName;
                        employee.ImageNameContentType = fileItem.ContentType;
                    }
                }
                employee.PhoneNo = model.Phone;
                employee.ExternalPhoneNo = model.ExternalPhone;

                //set check for completion of CV
                if (model.Language == HrCvLanguage.Ru)
                    employee.ComplietedRuCv = true;
                else
                    employee.ComplietedUzCv = true;

                try
                {
                    _hrEmployeeService.Update(employee);
                    var details = GetCvDetail(model.EmployeeId, model.Language);

                    details.Nationality = model.Nationality;
                    details.Languages = model.Languages;
                    details.PlaceOfBirth = model.PlaceOfBirth;
                    details.PartyMembership = model.PartyMembership;
                    details.AcademicDegree = model.AcademicDegree;
                    details.AcademicTitle = model.AcademicTitle;
                    details.EducationDegree = model.EducationDegree;
                    details.EducationSpeciality = model.EducationSpeciality;

                    _hrCvDetailService.Update(details);

                    //custom saving logic for all lists is in services
                    _hrCvEducationService.Save(model.EducationList.Where(e => !string.IsNullOrWhiteSpace(e)).Select(e => new HrCvEduction
                    {
                        Education = e
                    }).ToList(), details.Id);


                    _hrCvAwardService.Save(model.AwardList.Where(e => !string.IsNullOrWhiteSpace(e.Award) && e.Year>0).Select(e => new HrCvAward
                    {
                        Award = e.Award,
                        Year=e.Year
                    }).ToList(), details.Id);


                    _hrCvLaborService.Save(model.LaborDetailList.Where(e =>
                        !string.IsNullOrWhiteSpace(e.Years) &&
                        !string.IsNullOrWhiteSpace(e.Description)
                    ).Select(e => new HrCvLabor
                    {
                        Years = e.Years,
                        Description = e.Description,
                    }).ToList(), details.Id);


                    _hrCvRelativeService.Save(model.RelativesDetailsList.Where(e =>
                        !string.IsNullOrWhiteSpace(e.Address) &&
                        !string.IsNullOrWhiteSpace(e.BirthDateAndPlace) &&
                        !string.IsNullOrWhiteSpace(e.Degree) &&
                        !string.IsNullOrWhiteSpace(e.FullName) &&
                        !string.IsNullOrWhiteSpace(e.LaborDetails)
                    ).Select(e => new HrCvRelative
                    {
                        Degree = e.Degree,
                        FullName = e.FullName,
                        BirthDateAndPlace = e.BirthDateAndPlace,
                        LaborDetails = e.LaborDetails,
                        Address = e.Address

                    }).ToList(), details.Id);

                    _hrCvMembershipService.Save(model.MembershipList.Where(e => !string.IsNullOrWhiteSpace(e)).Select(e => new HrCvMembership
                    {
                        Membership = e
                    }).ToList(), details.Id);

                    ViewBag.ImportResult = "Thank you for CV completion!!!";
                }
                catch (Exception)
                {
                    //TODO: log the exception
                    ModelState.AddModelError("", "Form filled incorrectly");

                }
            }

            AddDefaultsToModel(model, employee);
            return View(model);
        }

        #endregion

        [HttpGet]
        public ActionResult DownloadCv(int employeeId, HrCvLanguage language)
        {
            var employee = _hrEmployeeService.GetByID(employeeId);

            if (employee == null)
                return RedirectToAction("Index", "HrCv");

            var employeeCV = GetCvDetail(employeeId, language);

            //preload ones and avoid nulls
            var awards = (employeeCV.Awards != null) ? employeeCV.Awards : new List<HrCvAward>();
            var memberships = (employeeCV.Memberships != null) ? employeeCV.Memberships : new List<HrCvMembership>();
            var educations = (employeeCV.Educations != null) ? employeeCV.Educations : new List<HrCvEduction>();
            var labors = (employeeCV.Labors != null) ? employeeCV.Labors : new List<HrCvLabor>();
            var relatives = (employeeCV.Relatives != null) ? employeeCV.Relatives : new List<HrCvRelative>();

            var doc = new Document();
            string path;
            var filename = employee.FullName + ".doc"; //always like this for both ru and uz

            if (language == HrCvLanguage.Uz)
            {
                path = HostingEnvironment.MapPath("~/Content/HrCvTemplates/cv_template_uz.doc");
            }
            else //only 2 possible cases
            {
                path = HostingEnvironment.MapPath("~/Content/HrCvTemplates/cv_template_ru.doc");
            }

            //for date formatting
            var dateCulture = new CultureInfo(language == HrCvLanguage.Uz ? "uz-UZ" : "ru-RU");

            doc.LoadFromFile(path);
            doc.Replace("{FULLNAME}", employee.FullName, true, false);
            doc.Replace("{CURRENTPOSITION}", language == HrCvLanguage.Ru ? employee.Position.TitleRu : employee.Position.TitleUz, true, true);
            doc.Replace("{CURRENTPOSITIONDATE}", employee.PositionStartDate?.ToString("dd MMMM yyyy", dateCulture), true, true);
            doc.Replace("{DATEOFBIRTH}", employee.DateOfBirth.ToString("dd.MM.yyyy"), true, true);
            doc.Replace("{PLACEOFBIRTH}", employee.PlaceOfBirth, true, true);
            doc.Replace("{NATIONALITY}", employeeCV?.Nationality ?? "", true, true);
            doc.Replace("{PARTYMEMBERSHIP}", employeeCV?.PartyMembership ?? "", true, true);
            doc.Replace("{EDUCATIONDEGREE}", employeeCV?.EducationDegree ?? "", true, true);
            doc.Replace("{EDUCATIONSPECIALITY}", employeeCV?.EducationSpeciality ?? "", true, true);
            doc.Replace("{ACADEMICDEGREE}", employeeCV?.AcademicDegree ?? "", true, true);
            doc.Replace("{ACADEMICTITLE}", employeeCV?.AcademicTitle ?? "", true, true);
            doc.Replace("{LANGUAGES}", employeeCV?.Languages ?? "", true, true);
            doc.Replace("{FULLNAMEGENITIVE}", employee.FullNameGenitive ?? employee.FullName, true, false);

            //awards
            //copy initial pargraph, insert data in old paragraph
            foreach (var award in awards)
            {
                var ap = GetAwardsParagraph(doc);
                var api = GetAwardsParagraphIndex(doc);

                var newP = (Paragraph)ap.Clone();
                ap.Text = string.Format("{0}, {1} {2}", award.Award, award.Year, language == HrCvLanguage.Ru ? "год" : "йил");
                doc.Sections[0].Paragraphs.Insert(api + 1, newP);
            }

            //finally remove previously duplicated paragraph or initial paragprah with placeholder
            var dapi = GetAwardsParagraphIndex(doc);
            doc.Sections[0].Paragraphs.RemoveAt(dapi);


            //memberships
            //copy initial pargraph, insert data in old paragraph
            foreach (var membership in memberships)
            {
                var mp = GetMembershipsParagraph(doc);
                var mpi = GetMembershipsParagraphIndex(doc);

                var newP = (Paragraph)mp.Clone();
                mp.Text = membership.Membership;
                doc.Sections[0].Paragraphs.Insert(mpi + 1, newP);
            }

            //finally remove previously duplicated paragraph or initial paragprah with placeholder
            var dmpi = GetMembershipsParagraphIndex(doc);
            doc.Sections[0].Paragraphs.RemoveAt(dmpi);

            //educations
            //tricky as fisrt line is same as nationality and the rest goes on separate lines
            if (!educations.Any())
            {
                //no education - remove placeholder
                doc.Replace("{EDUCATIONS1}", "", true, true);
                
            }
            else 
            {
                var isFirst = true;
                foreach (var education in educations)
                {
                    //first one is on the same line as education degree
                    //but the rest goes to separate lines
                    if (isFirst)
                    {
                        doc.Replace("{EDUCATIONS1}", education.Education, true, true);
                        isFirst = false;
                        continue;
                    }

                    //duplicate the paragraph with placeholder, insert in old one
                    var p = GetEducationSecondParagraph(doc);
                    var i = GetEducationSecondParagraphIndex(doc);

                    var newP = (Paragraph)p.Clone();
                    p.Text = education.Education;
                    doc.Sections[0].Paragraphs.Insert(i + 1, newP);
                    //again tricky part
                    //between each paragraph with education - after spacing is 0
                    //but for the last one - after spacing is 8 (already set in template)
                    //so change spacing of previous paragraph
                    doc.Sections[0].Paragraphs[i - 1].Format.AfterSpacing = 0;
                }

            }

            //finally remove previously duplicated paragraph 
            //or initial paragprah with placeholder if no or 1 education
            RemoveEducationSecondParagraph(doc);


            //labor
            //copy initial pargraph, insert data in old paragraph
            foreach(var labor in labors)
            {
                var lp = GetLaborParagraph(doc);
                var lpi = GetLaborParagraphIndex(doc);

                var newP = (Paragraph)lp.Clone();
                lp.Text = string.Format("{0}-\t{1}", labor.Years, labor.Description);
                doc.Sections[0].Paragraphs.Insert(lpi + 1, newP);
            }

            //finally remove previously duplicated paragraph or initial paragprah with placeholder
            var dlpi = GetLaborParagraphIndex(doc);
            doc.Sections[0].Paragraphs.RemoveAt(dlpi);

            //replace image
            //the only way to find image is
            //Loop through the paragraphs of the section
            foreach (Paragraph paragraph in doc.Sections[0].Paragraphs)
            {
                //Loop through the child elements of paragraph
                foreach (DocumentObject docObj in paragraph.ChildObjects)
                {
                    if (docObj.DocumentObjectType == DocumentObjectType.Picture)
                    {
                        var picture = docObj as DocPicture;
                        var w = picture.Width;
                        var h = picture.Height;
                        if (employee.ImageNameContent != null && employee.ImageNameContent.Length > 0)
                        {
                            picture.LoadImage(Image.FromStream(new MemoryStream(employee.ImageNameContent)));
                        }
                        else //no image loaded
                        {
                            picture.LoadImage(Image.FromFile(HostingEnvironment.MapPath("~/Content/HrCvImages/no-image.PNG")));
                        }
                        //set back to initial image size
                        picture.Width = w;
                        picture.Height = h;
                    }
                }
            }
         

            //for savety
            if (doc.Sections.Count > 0 && doc.Sections[0].Tables.Count > 0)
            {
                Table table = doc.Sections[0].Tables[0] as Spire.Doc.Table;

                //by default there is row with template for data which is initially last row
                //duplicate it, change text in cell, insert it before template
                foreach (var r in relatives)
                {
                    var lastRowIndex = table.Rows.Count - 1;
                    if (lastRowIndex < 0) continue; //for savety

                    TableRow lastRow = table.Rows[lastRowIndex];
                    TableRow newRow = lastRow.Clone();

                    if (newRow.Cells.Count < 5) continue;//for savety

                    newRow.Cells[0].Paragraphs[0].Text = r.Degree;
                    newRow.Cells[1].Paragraphs[0].Text = r.FullName;
                    newRow.Cells[2].Paragraphs[0].Text = r.BirthDateAndPlace;
                    newRow.Cells[3].Paragraphs[0].Text = r.LaborDetails;
                    newRow.Cells[4].Paragraphs[0].Text = r.Address;

                    table.Rows.Insert(lastRowIndex, newRow);
                }

                //remove last row as it contains just a template
                table.Rows.RemoveAt(table.Rows.Count - 1);
            }


            //for savety, replate chars that are invalid
            filename = filename.Replace(@"\", " ")
                            .Replace(@"/", " ")
                            .Replace(@":", " ")
                            .Replace(@"*", " ")
                            .Replace(@"?", " ")
                            .Replace("\"", " ")
                            .Replace(@"<", " ")
                            .Replace(@">", " ")
                            .Replace(@"|", " ")
                            .Replace(@"  ", " ");

            byte[] data = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                doc.SaveToStream(memoryStream, FileFormat.Doc);
                //save to byte array to return it
                data = memoryStream.ToArray();
            }

            return File(data, "application/msword", filename);

        }

        #region "ImportFromDocument"

        [HttpGet]
        public ActionResult ImportCV(int? employeeId, HrCvLanguage? language)
        {
            var model = new HrImportCvVM
            {
                EmployeeId = employeeId ?? 0,
                Language = language ?? HrCvLanguage.Ru
            };

            AddEmployeeList(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportCV(HrImportCvVM model, HttpPostedFileBase fileItem)
        {
            var hasFile = fileItem != null && fileItem.ContentLength > 0;
            if (!hasFile)
                ModelState.AddModelError("", "File Required");
            else
            {
                var ex = Path.GetExtension(fileItem.FileName);
                var acceptedExtensions = new List<string> { ".doc" };

                if (!acceptedExtensions.Contains(ex))
                {
                    ModelState.AddModelError(string.Empty, "CV must be in .doc format");
                }
            }
            HrEmployee employee = null;
            if(hasFile)
            {

                employee = _hrEmployeeService.GetByID(model.EmployeeId);

                if(employee == null)
                    ModelState.AddModelError("", "Employee does not exist");
            }

            if(ModelState.IsValid)
            {
                

                var doc = new Document();
                doc.LoadFromStream(fileItem.InputStream, FileFormat.Auto);

                if (doc.Sections.Count > 0)//safety check
                {
                    var details = GetCvDetail(model.EmployeeId, model.Language);
                    //overwrite old values
                    var awards = new List<HrCvAward>();
                    var memberships = new List<HrCvMembership>();
                    var educations = new List<HrCvEduction>();
                    var labors = new List<HrCvLabor>();
                    var relatives = new List<HrCvRelative>();

                    string previousPlaceholder = string.Empty;
                    //set placeholders list
                    var placeholders = model.Language == HrCvLanguage.Ru ? _importLinePlaceholdersRu : _importLinePlaceholdersUz;
                    foreach (Paragraph paragraph in doc.Sections[0].Paragraphs)
                    {
                        var hasPlaceholder = HasPlaceholder(paragraph.Text, placeholders, ref previousPlaceholder);

                        var cleanText = paragraph.Text.Trim(' ', '\t');
                        //here goes tones of IFs and ELSEs for parsing....
                        if (hasPlaceholder && !string.IsNullOrEmpty(cleanText) && !string.IsNullOrWhiteSpace(cleanText)) //check cases where values are on the same line
                        {
                            //apperatnly only 1 case
                            if(previousPlaceholder == placeholders[4])
                            {
                                var values = cleanText.Split(new[] { ":"}, StringSplitOptions.RemoveEmptyEntries);
                                if (values.Length > 1)
                                    details.EducationSpeciality = values[1].Trim(' ', '\t');
                            }

                        }
                        else if(!string.IsNullOrEmpty(cleanText) && !string.IsNullOrWhiteSpace(cleanText)) //check cases when values are on next line
                        {
                            if (previousPlaceholder == placeholders[0])//full name
                            {
                                employee.FullName = cleanText;
                                previousPlaceholder = ""; //prevent for further setting of next lines as names
                            }
                            else if (previousPlaceholder == placeholders[1])
                            {
                                //tricky one
                                //single line contains values for both date of birth and place of birth
                                //separated by multiple spaces and tabs
                                //date in format dd.MM.yyyy
                                var values = cleanText.Split(new[] { "\t", " \t", "\t ", " \t " }, StringSplitOptions.RemoveEmptyEntries);
                                if (values.Length > 1) //safety check
                                {

                                    DateTime d;
                                    if (DateTime.TryParseExact(values[0].Trim(' ', '\t'), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                                        employee.DateOfBirth = d;

                                    details.PlaceOfBirth = values[1].Trim(' ', '\t');

                                    previousPlaceholder = ""; //prevent for further parsing of line as next date of birth and place of birth
                                }
                            }
                            else if (previousPlaceholder == placeholders[2])
                            {
                                //same pattern as above
                                //nationality and party membership are in the same parahraph
                                //separated by multiple spaces and tabs
                                var values = cleanText.Split(new[] { "\t", " \t", "\t ", " \t " }, StringSplitOptions.RemoveEmptyEntries);
                                if (values.Length > 1) //safety check
                                {
                                    details.Nationality = values[0].Trim(' ', '\t');
                                    details.PartyMembership = values[1].Trim(' ', '\t');
                                }

                                previousPlaceholder = ""; //prevent for further parsing of line as next nationality and party membership
                            }
                            else if(previousPlaceholder == placeholders[5])
                            {
                                //same pattern as above
                                //academic degree and academic title are in the same parahraph
                                //separated by multiple spaces and tabs

                                var values = cleanText.Split(new[] { "\t", " \t", "\t ", " \t " }, StringSplitOptions.RemoveEmptyEntries);
                                if (values.Length > 1) //safety check
                                {
                                    details.AcademicDegree = values[0].Trim(' ', '\t');
                                    details.AcademicTitle = values[1].Trim(' ', '\t');
                                }

                                previousPlaceholder = ""; //prevent for further parsing of line as next academic degree and academic title
                            }
                            else if (previousPlaceholder == placeholders[6])
                            {
                                //almost same pattern as above
                                //languages are on next line
                                //single paragraph for all languages
                                details.Languages = cleanText;

                                previousPlaceholder = ""; //prevent for further parsing of line as next language
                            }
                            else if(previousPlaceholder == placeholders[3])
                            {
                                //perhaps the trickiest one
                                //first line after contains both education degree and education line
                                //separate by multiple spaces and tabs
                                //whereas all below contains only education
                                //so try to split, see how many values and assign accordingly
                                var values = cleanText.Split(new[] { "\t", " \t", "\t ", " \t " }, StringSplitOptions.RemoveEmptyEntries);
                                if (values.Length > 1) //safety check
                                {
                                    details.EducationDegree = values[0].Trim(' ', '\t');
                                    educations.Add(new HrCvEduction
                                    {
                                        Education = values[1].Trim(' ', '\t')
                                    });
                                }
                                else if(values.Length == 1)//again safety
                                {
                                    var value = values[0].Trim(' ', '\t');

                                    if(!string.IsNullOrEmpty(value))
                                        educations.Add(new HrCvEduction
                                        {
                                            Education = value
                                        });
                                }

                                //do not recet previous placeholder - can be many lines for education
                                //will continue parsing until next placeholder if lines are not empty
                            }
                            else if(previousPlaceholder == placeholders[7])
                            {
                                //almost same as above
                                //can be multiple awards, each starts with new line
                                //awards are in format: award, year
                                var values = cleanText.Split(new[] { ", ", " , ", " ," }, StringSplitOptions.RemoveEmptyEntries);
                                var award = new HrCvAward();
                                if (values.Length > 0)
                                {
                                    award.Award = values[0].Trim(' ', '\t');

                                    if(values.Length > 1)
                                    {
                                        int year;
                                        if (int.TryParse(values[1].Substring(0, 4), out year))
                                            award.Year = year;
                                    }

                                    awards.Add(award);
                                }

                                //do not recet previous placeholder - can be many lines for awards
                                //will continue parsing until next placeholder if lines are not empty
                            }
                            else if (previousPlaceholder == placeholders[8])
                            {
                                //same as above
                                //can be multiple memberships, each starts with new line
                                memberships.Add(new HrCvMembership
                                {
                                    Membership = cleanText
                                });


                                //do not recet previous placeholder - can be many lines for memberships
                                //will continue parsing until next placeholder if lines are not empty
                            }
                            else if (previousPlaceholder == placeholders[9])
                            {
                                //same as above
                                //can be multiple labor details, each starts with new line
                                //format years, dash, description
                                //dash can have tab or multiple spaces or both
                                var values = cleanText.Split(new[] { "-\t", "-  ", "- \t" }, StringSplitOptions.RemoveEmptyEntries);
                                if(values.Length > 1)
                                {
                                    labors.Add(new HrCvLabor
                                    {
                                        Years = values[0].Trim(' ', '\t'),
                                        Description = values[1].Trim(' ', '\t')
                                    });
                                }

                                //do not recet previous placeholder - can be many lines for memberships
                                //will continue parsing until next placeholder if lines are not empty

                            }
                            else if(previousPlaceholder == placeholders[10] 
                                && model.Language == HrCvLanguage.Ru
                                && cleanText.Contains("о близких родственниках"))
                            {
                                //take full name in genitive
                                //applicable only for russian CV
                                employee.FullNameGenitive = cleanText.Replace("о близких родственниках", "").Trim(' ', '\t');

                            }
                        }



                        //Loop through the child elements of paragraph to find image
                        foreach (DocumentObject docObj in paragraph.ChildObjects)
                        {
                            if (docObj.DocumentObjectType == DocumentObjectType.Picture)
                            {
                                //replace image
                                var picture = docObj as DocPicture;
                                employee.ImageNameContent = picture.ImageBytes;
                                employee.ImageName = "imageFromCv.png";
                                employee.ImageNameContentType = "image/png";
                            }
                        }
                    }

                    //relatives are in separate table
                    if (doc.Sections[0].Tables.Count > 0)
                    {
                        Table table = doc.Sections[0].Tables[0] as Spire.Doc.Table;

                        if(table.Rows.Count > 1)//not only header
                        {
                            for(var i = 1; i < table.Rows.Count; i++)
                            {
                                var row = table.Rows[i];
                                if (row.Cells.Count < 5) continue;//for savety

                                relatives.Add(new HrCvRelative
                                {
                                    Degree = GetMultipleParagraphsText(row.Cells[0].Paragraphs),
                                    FullName = GetMultipleParagraphsText(row.Cells[1].Paragraphs),
                                    BirthDateAndPlace = GetMultipleParagraphsText(row.Cells[2].Paragraphs),
                                    LaborDetails = GetMultipleParagraphsText(row.Cells[3].Paragraphs),
                                    Address = GetMultipleParagraphsText(row.Cells[4].Paragraphs),
                                });
                            }
                        }

                    }

                    _hrEmployeeService.Update(employee);
                    _hrCvAwardService.Save(awards, details.Id);
                    _hrCvMembershipService.Save(memberships, details.Id);
                    _hrCvEducationService.Save(educations, details.Id);
                    _hrCvLaborService.Save(labors, details.Id);
                    _hrCvRelativeService.Save(relatives, details.Id);

                    ViewBag.ImportResult = "Imported successfully!!!";
                }
                else
                    ModelState.AddModelError("", "Wrong document format");
            }

            AddEmployeeList(model);

            return View(model);
        }

        #endregion


        #region "Private Utility methods"
        //newly added employees do not have HrCvDetail record
        //method handles it
        private HrCvDetail GetCvDetail(int employeeId, HrCvLanguage language)
        {
            var details = _hrCvDetailService.GetForCv(employeeId, language);

            //did not fill CV yet, create record
            if (details == null)
            {
                details = new HrCvDetail
                {
                    EmployeeId = employeeId,
                    Language = language
                };

                _hrCvDetailService.Create(details);
            }
            return details;
        }
        //add readonly fields, hints and image for preview
        private void AddDefaultsToModel(HrCvVM model, HrEmployee employee)
        {
            model.EmployeeName = employee.FullName;
            model.DepartmentName = model.Language == HrCvLanguage.Ru ? employee.Department.TitleRu : employee.Department.TitleUz;
            model.PositionName = model.Language == HrCvLanguage.Ru ? employee.Position.TitleRu : employee.Position.TitleUz;
            model.DateOfBirth = employee.DateOfBirth;
            if (employee.ImageNameContent != null && employee.ImageNameContent.Length > 0)
            {
                model.ImageContent = Convert.ToBase64String(employee.ImageNameContent);
            }

            var hints = _hrCvHintService.GetByLanguage(model.Language);
            //lets be proactive and add defaults
            //so that employee does not empty field names and hints
            if(!hints.Any())
            {
                _hrCvHintService.CreateDefaults(model.Language);
                hints = _hrCvHintService.GetByLanguage(model.Language);
            }
            model.HintTexts = hints.ToList();
        }

        private HrEmployeeListVM MapToModel(HrEmployee hrEmployee)
        {
            return new HrEmployeeListVM
            {
                Id = hrEmployee.Id,
                FullName = hrEmployee.FullName,
                DepartmentName = hrEmployee.Department?.TitleEn,
                PositionName = hrEmployee.Position?.TitleEn,
                HasFilledRuCV = hrEmployee.ComplietedRuCv,
                HasFilledUzCV = hrEmployee.ComplietedUzCv
            };
        }

        //to follow DRY - these are separated into submethods
        private void RemoveEducationSecondParagraph(Document doc)
        {
            var i = GetEducationSecondParagraphIndex(doc);
            doc.Sections[0].Paragraphs.RemoveAt(i);
        }

        private int GetEducationSecondParagraphIndex(Document doc)
        {
            var paragraph = GetEducationSecondParagraph(doc);
            return doc.Sections[0].Paragraphs.IndexOf(paragraph);
        }

        private Paragraph GetEducationSecondParagraph(Document doc)
        {
            var text = GetTextSelection(doc, "{EDUCATIONS2}");
            return text.GetAsOneRange().OwnerParagraph;
        }

        private int GetLaborParagraphIndex(Document doc)
        {
            var paragraph = GetLaborParagraph(doc);
            return doc.Sections[0].Paragraphs.IndexOf(paragraph);
        }

        private Paragraph GetLaborParagraph(Document doc)
        {
            var text = GetTextSelection(doc, "{LABOR}");
            return text.GetAsOneRange().OwnerParagraph;
        }

        private int GetAwardsParagraphIndex(Document doc)
        {
            var paragraph = GetAwardsParagraph(doc);
            return doc.Sections[0].Paragraphs.IndexOf(paragraph);
        }

        private Paragraph GetAwardsParagraph(Document doc)
        {
            var text = GetTextSelection(doc, "{AWARDS}");
            return text.GetAsOneRange().OwnerParagraph;
        }

        private int GetMembershipsParagraphIndex(Document doc)
        {
            var paragraph = GetMembershipsParagraph(doc);
            return doc.Sections[0].Paragraphs.IndexOf(paragraph);
        }

        private Paragraph GetMembershipsParagraph(Document doc)
        {
            var text = GetTextSelection(doc, "{MEMBERSHIPS}");
            return text.GetAsOneRange().OwnerParagraph;
        }

        private TextSelection GetTextSelection(Document doc, string query)
        {
            return doc.FindString(query, true, true);
        }

        private void AddEmployeeList(HrImportCvVM model)
        {
            model.Employees = _hrEmployeeService.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(),Text = e.FullName }).ToList();
        }

        private readonly List<string> _importLinePlaceholdersRu = new List<string>
        {
            "СПРАВКА".ToLower(),
            "Год рождения:".ToLower(),
            "Национальность:".ToLower(),
            "Образование:".ToLower(),
            "Специальность по образованию:".ToLower(),
            "Ученая степень:".ToLower(),
            "Какими иностранными языками владеет".ToLower(),
            "Имеет ли правительственные награды:".ToLower(),
            "Является ли народным депутатом,".ToLower(),
            "ТРУДОВАЯ ДЕЯТЕЛЬНОСТЬ".ToLower(),
            "СВЕДЕНИЯ".ToLower()
        };

        private readonly List<string> _importLinePlaceholdersUz = new List<string>
        {
            "МАЪЛУМОТНОМА".ToLower(),
            "Туғилган йили:".ToLower(),
            "Миллати:".ToLower(),
            "Маълумоти:".ToLower(),
            "Маълумоти бўйича мутахассислиги:".ToLower(),
            "Илмий даражаси:".ToLower(),
            "Қайси чет тилларини билади".ToLower(),
            "Давлат мукофотлари билан тақдирланганми:".ToLower(),
            "Халқ депутатлари, республика, вилоят,".ToLower(),
            "МЕҲНАТ ФАОЛИЯТИ".ToLower(),
            "қариндошлари ҳақида".ToLower()
        };

        //check if line has placeholder for further parsing
        private bool HasPlaceholder(string text, List<string> placeholders, ref string placeholder)
        {
            foreach (var p in placeholders)
            {
                if (text.ToLower().Contains(p))
                {
                    placeholder = p;
                    return true;
                }
            }

            return false;
        }

        //mainly for tables as there coult be multiple paragraphs in a single cell
        private string GetMultipleParagraphsText(Spire.Doc.Collections.ParagraphCollection paragraphs)
        {
            var result = string.Empty;
            foreach(Paragraph p in paragraphs)
            {
                result += p.Text.Trim(' ', '\t') + " ";
            }

            return result.Trim(' ', '\t');
        }

        #endregion

    }
}