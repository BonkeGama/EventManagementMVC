//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Hotel_Project.Models;

//namespace Hotel_Project.Controllers
//{
//    public class SendMailViewModelsController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: SendMailViewModels
//        public ActionResult Index()
//        {
//            return View(db.SendMailViewModels.ToList());
//        }

//        // GET: SendMailViewModels/Details/5
//        public ActionResult Details(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SendMailViewModel sendMailViewModel = db.SendMailViewModels.Find(id);
//            if (sendMailViewModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(sendMailViewModel);
//        }

//        // GET: SendMailViewModels/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: SendMailViewModels/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Email,FirstName")] SendMailViewModel sendMailViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                db.SendMailViewModels.Add(sendMailViewModel);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(sendMailViewModel);
//        }

//        // GET: SendMailViewModels/Edit/5
//        public ActionResult Edit(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SendMailViewModel sendMailViewModel = db.SendMailViewModels.Find(id);
//            if (sendMailViewModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(sendMailViewModel);
//        }

//        // POST: SendMailViewModels/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Email,FirstName")] SendMailViewModel sendMailViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(sendMailViewModel).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(sendMailViewModel);
//        }

//        // GET: SendMailViewModels/Delete/5
//        public ActionResult Delete(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SendMailViewModel sendMailViewModel = db.SendMailViewModels.Find(id);
//            if (sendMailViewModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(sendMailViewModel);
//        }

//        // POST: SendMailViewModels/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(string id)
//        {
//            SendMailViewModel sendMailViewModel = db.SendMailViewModels.Find(id);
//            db.SendMailViewModels.Remove(sendMailViewModel);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
