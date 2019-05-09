using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrPositionVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Code from 1C cannot be empty")]
        [DisplayName("Code from 1C")]
        public string Code_1C { get; set; }
        [Required(ErrorMessage = "Title in Russian cannot be empty")]
        [DisplayName("Title in Russian")]
        public string TitleRu { get; set; }
        [Required(ErrorMessage = "Title in Uzbek cannot be empty")]
        [DisplayName("Title in Uzbek")]
        public string TitleUz { get; set; }
        [Required(ErrorMessage = "Title in Enlish cannot be empty")]
        [DisplayName("Title in English")]
        public string TitleEn { get; set; }

    }
}