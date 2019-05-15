using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrCvAwardVM
    {
        //if no awards - no year required
        public int? Year { get; set; }

        //will not be empty anyway - dropdown
        public string Award { get; set; }
    }
}