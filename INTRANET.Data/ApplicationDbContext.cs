using INTRANET.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace INTRANET.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ApplicationDbContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AcademicYear> AcademicYears { get; set; }

        public DbSet<HrCvAward> HrCvAwards { get; set; }
        public DbSet<HrCvDetail> HrCvDetails { get; set; }
        public DbSet<HrCvEduction> HrCvEducations { get; set; }
        public DbSet<HrCvHintText> HrCvHintTexts { get; set; }
        public DbSet<HrCvLabor> HrCvLabors { get; set; }
        public DbSet<HrCvMembership> HrCvMemberships { get; set; }
        public DbSet<HrCvRelative> HrCvRelatives { get; set; }
        public DbSet<HrDepartment> HrDepartments { get; set; }
        public DbSet<HrEmployee> HrEmployees { get; set; }
        public DbSet<HrPosition> HrPositions { get; set; }
        public DbSet<HrEmployeeDocument> HrEmployeeDocuments { get; set; }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
