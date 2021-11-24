using OlwandleHotel.Models;
using OlwandleHotel.Models;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OlwandleHotel.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index()
        {
            var payment = db.payment.Include(p => p.reserve);
            return View(payment.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payments payments = db.payment.Find(id);
            if (payments == null)
            {
                return HttpNotFound();
            }
            return View(payments);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.bookingId = new SelectList(db.reservations, "BookingID", "userID");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,estCost,paidEstCost,date_PaidEstCost,refNo,addDescription,addCost,totalCost,paidTotCost,date_PaidTotCost,bookingId")] Payments payments)
        {
            if (ModelState.IsValid)
            {
                Reservation rm = new Reservation();
                //payments.totalCost = rm.CalcBasicPrice();
                db.payment.Add(payments);
                db.SaveChanges();
                return RedirectToAction("");
            }

            ViewBag.bookingId = new SelectList(db.reservations, "BookingID", "userID", payments.BookingID);
            return View(payments);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payments payments = db.payment.Find(id);
            if (payments == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookingId = new SelectList(db.reservations, "BookingID", "userID", payments.BookingID);
            return View(payments);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,estCost,paidEstCost,date_PaidEstCost,refNo,addDescription,addCost,totalCost,paidTotCost,date_PaidTotCost,bookingId")] Payments payments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookingId = new SelectList(db.reservations, "BookingID", "userID", payments.BookingID);
            return View(payments);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payments payments = db.payment.Find(id);
            if (payments == null)
            {
                return HttpNotFound();
            }
            return View(payments);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payments payments = db.payment.Find(id);
            db.payment.Remove(payments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Pay(int? BookingID)
        {
            if (BookingID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.reservations.Find(BookingID);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            Rooms rooms = db.Rooms.Where(x => x.RoomNo == reservation.RoomNo).FirstOrDefault();
            GuestDetails details = new GuestDetails()
            {
                checkInDate = reservation.checkInDate,
                checkOutDate = reservation.checkOutDate,
                numGuests = reservation.numGuests,
                DayNo = reservation.DayNo,
                RoomNo = reservation.RoomNo,
                FloorNum = rooms.FloorNum,
                Type = rooms.Roomtype.Type,
                BasicCharge = rooms.Roomtype.BasicCharge,
                TotalPrice = reservation.TotalPrice
            };
            return Redirect(PaymentLink(details.TotalPrice.ToString("C"), "Reservation Payment For : Room No." + reservation.RoomNo, reservation.BookingID));
        }


        public ActionResult Payment_Successfull(int id)
        {
            Reservation reservation = db.reservations.Find(id);
            db.payment.Add(new Payments()
            {
                payTypes = "PayFast Online",
                deposit = 0,
                totalCost = reservation.TotalPrice,
                paidTotCost = true,
                date_PaidTotCost = DateTime.Now,
                BookingID = reservation.BookingID,
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult SPayFast()
        //{
        //    var userId = User.Identity.Name;

        //    var email = User.Identity.Name;
        //    //var FindEmail = dc.Users.Where(m => m.UserName == email);
        //    //var Del = db.DeliveryOptions.Count(m => m.UserName == email);
        //    //double delivery = 0;
        //    //if (Del > 0)
        //    //{
        //    //    delivery = 100;
        //    //}
        //    //string EmailFound = "";
        //    //foreach (var item in FindEmail)
        //    //{
        //    //    EmailFound = item.Email;
        //    //}


        //    string amount = "";
        //    string orderId = new Random().Next(1, 99999).ToString();
        //    string name = "Blissful Kiddies, Order#" + orderId;
        //    string description = "Blissful Kiddies";


        //    string site = "";
        //    string merchant_id = "";
        //    string merchant_key = "";

        //    // Check if we are using the mmor live system
        //    string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

        //    if (paymentMode == "test")
        //    {
        //        site = "https://sandbox.payfast.co.za/eng/process?";
        //        merchant_id = "10000100";
        //        merchant_key = "46f0cd694581a";
        //    }
        //    else if (paymentMode == "live")
        //    {
        //        site = "https://www.payfast.co.za/eng/process?";
        //        merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
        //        merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
        //    }
        //    // Build the query string for payment site

        //    StringBuilder str = new StringBuilder();
        //    str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
        //    str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
        //    str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
        //    str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
        //    //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

        //    str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
        //    str.Append("&amount=" + HttpUtility.UrlEncode(amount));
        //    str.Append("&item_name=" + HttpUtility.UrlEncode(name));
        //    str.Append("&item_description=" + HttpUtility.UrlEncode(description));

        //    // Redirect to PayFast
        //    Response.Redirect(site + str.ToString());

        //    return View();
        //}


        public string PaymentLink(string totalCost, string paymentSubjetc, int BookingID)
        {

            string paymentMode = ConfigurationManager.AppSettings["PaymentMode"], site, merchantId, merchantKey, returnUrl, cancelUrl, PF_NotifyURL;

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchantId = "10005631";
                merchantKey = "znbndc034nb6b";
            }
            else if (paymentMode == "live")
            {
                site = "https://www.payfast.co.za/eng/process?";
                merchantId = ConfigurationManager.AppSettings["PF_MerchantID"];
                merchantKey = ConfigurationManager.AppSettings["PF_MerchantKey"];
            }
            else
            {
                throw new InvalidOperationException("Payment method unknown.");
            }
            var stringBuilder = new StringBuilder();
            PF_NotifyURL = Url.Action("Payment_Successfull", "Payments",
                new System.Web.Routing.RouteValueDictionary(new { id = BookingID }),
                "http", Request.Url.Host);
            returnUrl = Url.Action("ConfirmReservation", "Reservations1",
                new System.Web.Routing.RouteValueDictionary(new { id = BookingID }),
                "http", Request.Url.Host);
            cancelUrl = Url.Action("ConfirmReservation", "Reservations1",
                new System.Web.Routing.RouteValueDictionary(new { id = BookingID }),
                "http", Request.Url.Host);

            /* mechant details */
            stringBuilder.Append("&merchant_id=" + HttpUtility.HtmlEncode(merchantId));
            stringBuilder.Append("&merchant_key=" + HttpUtility.HtmlEncode(merchantKey));
            stringBuilder.Append("&return_url=" + HttpUtility.HtmlEncode(returnUrl));
            stringBuilder.Append("&cancel_url=" + HttpUtility.HtmlEncode(cancelUrl));
            stringBuilder.Append("&notify_url=" + HttpUtility.HtmlEncode(PF_NotifyURL));
            /* buyer details */
            Reservation reservation = db.reservations.Find(BookingID);

            Rooms rooms = db.Rooms.Where(x => x.RoomNo == reservation.RoomNo).FirstOrDefault();
            GuestDetails details = new GuestDetails()
            {
                checkInDate = reservation.checkInDate,
                checkOutDate = reservation.checkOutDate,
                numGuests = reservation.numGuests,
                DayNo = reservation.DayNo,
                //bookings = 0,
                RoomNo = reservation.RoomNo,
                //BookingID = reservation.BookingID,
                FloorNum = rooms.FloorNum,
                Type = rooms.Roomtype.Type,
                BasicCharge = rooms.Roomtype.BasicCharge,
                TotalPrice = reservation.TotalPrice
            };
            var customer = db.Users.FirstOrDefault(x => x.Id == reservation.Id);
            if (customer != null)
            {
                stringBuilder.Append("&name_first=" + HttpUtility.HtmlEncode(customer.FirstName));
                stringBuilder.Append("&name_last=" + HttpUtility.HtmlEncode(customer.LastName));
                stringBuilder.Append("&email_address=" + HttpUtility.HtmlEncode(customer.Email));
                //stringBuilder.Append("&cell_number=" + HttpUtility.HtmlEncode(customer.Phone));
            }
            /* Transaction details */

            if (reservation != null)
            {
                stringBuilder.Append("&m_payment_id=" + HttpUtility.HtmlEncode(reservation.BookingID));
                stringBuilder.Append("&amount=" + HttpUtility.HtmlEncode(reservation.TotalPrice));
                stringBuilder.Append("&item_name=" + HttpUtility.HtmlEncode(paymentSubjetc));
                stringBuilder.Append("&item_description=" + HttpUtility.HtmlEncode(paymentSubjetc));

                stringBuilder.Append("&email_confirmation=" + HttpUtility.HtmlEncode("1"));
                stringBuilder.Append("&confirmation_address=" + HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["PF_ConfirmationAddress"]));
            }

            return (site + stringBuilder);
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
