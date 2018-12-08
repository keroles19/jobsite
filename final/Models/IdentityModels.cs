using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace final.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
     

        public string memType { get; set; }

        public string Email { get; set; }

        public virtual ICollection<jobs> jobs { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<final.Models.Catig> Catigs { get; set; }

        public System.Data.Entity.DbSet<final.Models.jobs> jobs { get; set; }

        public System.Data.Entity.DbSet<final.Models.ApplayForJob> ApplayForJobs { get; set; }

        

   // public System.Data.Entity.DbSet<final.Models.ApplicationUser> IdentityUsers { get; set; }

       
    }
}