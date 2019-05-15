using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTRANET.Models
{
    public class HrCvLaborVM
    {
        //can contain dash and "н.в.", so string
        public string Years { get; set; }
        public string Description { get; set; }
    }
}