using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OlwandleHotel;
using OlwandleHotel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace TaskA.Controllers
{
    public class DriversController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: Drivers
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Drivers
        public ActionResult Index()
        {
            return View(db.Drivers.ToList());
        }
        public ActionResult DriverDashBord()
        {
            using (var context = new ApplicationDbContext())
            {
                ViewBag.Count = context.GuestDetails.SqlQuery(" SELECT * FROM dbo.GuestDetails").Count();
                //ViewBag.Taskers = context.Taskers.SqlQuery(" SELECT * FROM dbo.Taskers").Count();
                //ViewBag.Drivers = context.Drivers.SqlQuery(" SELECT * FROM dbo.Drivers").Count();
                //ViewBag.Clients = context.Clients.SqlQuery(" SELECT * FROM dbo.Clients").Count();

            }

            string s = User.Identity.GetUserName();
            var UsCustomers = db.Drivers.Where(p => p.Driver_Email.Equals(s)).FirstOrDefault();

            if (UsCustomers == null)
            {
                @ViewBag.ResultMessage = "the is no user logged in";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                @ViewBag.userIS = "Driver_ID";
                Driver aCustomers = db.Drivers.Find(UsCustomers.Driver_ID);

                return View(aCustomers);
            }

           

        }
        // GET: Drivers/Details/5
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Driver_ID,Driver_IDNo,Driver_Name,Driver_Surname,Diver_Image,Driver_CellNo,Driver_Address,Driver_Email,Vehiclename")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                var pass = "Password@01";

                var user = new ApplicationUser { UserName = driver.Driver_Email, Email = driver.Driver_Email };
                await UserManager.CreateAsync(user, pass);
                driver.Driver_ID = user.Id;
                UserManager.AddToRole(driver.Driver_ID, "Driver");

                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(driver);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }
        // GET: Drivers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Driver_ID,Driver_IDNo,Driver_Name,Driver_Surname,Diver_Image,Driver_CellNo,Driver_Address,Driver_Email,Vehiclename")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(driver);
        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Drivers.Remove(driver);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}