using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTRANET.Model
{
    public class HrEmployeeDocument
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileContentType { get; set; }

        [Required]
        [Column(TypeName = "varbinary(max)")]
        public byte[] FileContent { get; set; }
        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public virtual HrEmployee Employee { get; set; }
    }
}
