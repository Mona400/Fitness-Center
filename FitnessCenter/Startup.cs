using FitnessCenter.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitnessCenter.Startup))]
namespace FitnessCenter
{
    public partial class Startup
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void CreateDefaultRolesAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();

            if (!roleManager.RoleExists("Coach"))
            {
                role.Name = "Coach";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "MonaA";
                user.Email = "MonaA@gmail.com";
                var Check = userManager.Create(user, "Mm123456@");
                if (Check.Succeeded)
                {
                    userManager.AddToRoles(user.Id, "Coach");
                }
            }
        }
    }
}
