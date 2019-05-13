using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Model
{
    public class HrCvAward
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("CvDetail")]
        public int HrCvDetailId { get; set; }
        public string Award { get; set; }
        public int Year { get; set; }

        public virtual HrCvDetail CvDetail { get; set; }
    }
}
