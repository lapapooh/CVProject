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
        [Required(ErrorMessage ="Code 1C cannot be empty")]
        [DisplayName("Code in 1C")]
        public string Code_1C { get; set; }
        [Required(ErrorMessage = "Title Ru cannot be empty")]
        [DisplayName("Position in Russian")]
        public string TitleRu { get; set; }
        [Required(ErrorMessage = "Title Uz cannot be empty")]
        [DisplayName("Position in Uzbek")]
        public string TitleUz { get; set; }
        [Required(ErrorMessage = "Title En cannot be empty")]
        [DisplayName("Position in English")]
        public string TitleEn { get; set; }

    }
}