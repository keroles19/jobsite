using final.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(final.Startup))]
namespace final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AddRoleUserManager();
        }

      
        //  add roles and Admin manager  by coding    to save that  an admin login if website has attacker by any one 
              // create role manager and usermanager  by coding in startup to creating when i open website firstly  


        private ApplicationDbContext dp = new Models.ApplicationDbContext();
        private void AddRoleUserManager()
        {

            var Rolemanage = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dp));
            var Usermange = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dp));

            if (!Rolemanage.RoleExists("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                Rolemanage.Create(role);

                ApplicationUser user = new ApplicationUser() { UserName = "keroles200", Email = "kerolesatef200@gmail.com"};
              var check =  Usermange.Create(user, "kero2020");
              if (check.Succeeded)
              {
                  Usermange.AddToRole(user.Id, "Admin");   
                
              }

            }
        
        
        }

    }
}
