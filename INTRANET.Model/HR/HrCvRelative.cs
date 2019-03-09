using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Model
{
    public class HrCvRelative
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("CvDetail")]
        public int HrCvDetailId { get; set; }
        public string Degree { get; set; }
        public string FullName { get; set; }
        public string BirthDateAndPlace { get; set; }
        public string LaborDetails { get; set; }
        public string Address { get; set; }

        public virtual HrCvDetail CvDetail { get; set; }
    }
}
