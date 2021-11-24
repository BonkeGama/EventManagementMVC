using OlwandleHotel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OlwandleHotel.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Allusers()
        {
            return View(db.RegisterAllViewModel.ToList());
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Gallery()
        {
            ViewBag.Message = "Services we offer";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AdminDashBord()
        {

            @ViewBag.userIS = "Admin";
            return View();
        }
        public ActionResult ClerkDashBord()
        {

            @ViewBag.userIS = "Clerk";
            return View();
        }

        public ActionResult Admission()
        {
            return View();
        }
        public ActionResult Fees()
        {
            return View();
        }
      

        public ActionResult DashBordPage()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (user != null)
            {
              
                if (manager.IsInRole(user.Id, "Admin"))
                {

                    return RedirectToAction("AdminDashBord", "Home");
                }
                //Clerk
                else if (manager.IsInRole(user.Id, "Clerk"))
                {
                    return RedirectToAction("ClerkDashBord", "Home");
                }
                //Student
                else if (manager.IsInRole(user.Id, "Student"))
                {
                    return RedirectToAction("StudentDashBord", "Students");
                }
                //Parent
                else if (manager.IsInRole(user.Id, "Customer"))
                {
                    return RedirectToAction("CustomerDashBord", "Customers");
                }
                //Teacher
                else if (manager.IsInRole(user.Id, "Staff "))
                {
                    return RedirectToAction("StaffDashBord", "Staffs");
                }
                //Counselor
                else if (manager.IsInRole(user.Id, "Counselor"))
                {
                    return RedirectToAction("CounselorDashBord", "Counselors");
                }
                //unknown
                else
                { return RedirectToAction("Index", "Home"); }
            }
            else
            { return RedirectToAction("Index", "Home"); }




        }

        [ChildActionOnly]
        public PartialViewResult _RightPane()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            string usertyp = "";

            if(user != null)
            {
                //Admin
                if (manager.IsInRole(user.Id, "Admin"))
                {
                    usertyp = "Admin";

                }
                //Clerk
                else if (manager.IsInRole(user.Id, "Clerk"))
                {
                    usertyp = "Clerk";

                }
                //Student
                else if (manager.IsInRole(user.Id, "Student"))
                {
                    usertyp = "Student";

                }
                //Parent
                else if (manager.IsInRole(user.Id, "Customer"))
                {
                    usertyp = "Customer";
                }
                //Teacher
                else if (manager.IsInRole(user.Id, "Staff"))
                {
                    usertyp = "Staff";
                }
                //Counselor
                else if (manager.IsInRole(user.Id, "Counselor"))
                {
                    ViewBag.CourseCode = "Admin";
                }

                //unknown
                else
                { usertyp = "Allusers"; }
            }else
            { usertyp = "Allusers"; }

            ViewBag.User = usertyp;
            return PartialView("_RightPane");
           

        }
    }
}