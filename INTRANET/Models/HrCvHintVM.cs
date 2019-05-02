using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrCvHintVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Language cannot be empty")]
        public HrCvLanguage Language { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        public HrCvField Field { get; set; }
        [Required(ErrorMessage = "Field Name cannot be empty")]
        [Display(Name = "Field Name")]
        public string FieldName { get; set; }
        [Required(ErrorMessage = "Text cannot be empty")]
        public string Text { get; set; }
    }
}