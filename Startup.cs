using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OlwandleHotel.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(OlwandleHotel.Startup))]
namespace OlwandleHotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRolersandUsers();
        }
        //private void CreateRolersandUsers()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        //    //Procedure p = new Procedure() { ProcedurID = 1,Price = 200,ProcedureName = "Consultation", };
        //    if (!roleManager.RoleExists("Admin"))
        //    {
        //        roleManager.Create(new IdentityRole("Admin"));

        //        var user = new ApplicationUser();

        //        user.UserName = "DEquipment@gmail.com";
        //        user.Email = "DEquipment@gmail.com";
        //        string pwd = "Password01";

        //        var newuser = userManager.Create(user, pwd);
        //        if (newuser.Succeeded)
        //        {
        //            userManager.AddToRole(user.Id, "Admin");
        //        }

        //    }

        //}
    }
}
