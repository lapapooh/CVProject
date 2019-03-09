using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Model
{
    public class HrDepartment
    {
        public int Id { get; set; }

        [Required]
        public string Code_1C { get; set; }

        public string TitleUz { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }

        public virtual List<HrEmployee> Employees { get; set; }
    }
}
