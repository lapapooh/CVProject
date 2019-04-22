﻿using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrCvVM
    {
        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public HrCvLanguage Language { get; set; }

        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string ImageContent { get; set; }

        //fr easy access
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

    }
}