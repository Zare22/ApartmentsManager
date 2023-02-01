using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Public_MVC.Context;
using Public_MVC.Models;
using Public_MVC.Resources;
using Recaptcha.Web.Mvc;

namespace Public_MVC.Controllers
{
    public class ApartmentsController : Controller
    {
        private ApartmentsContext db = new ApartmentsContext();


        public ActionResult Index()
        {
            return View(db);
        }


        public ActionResult Details(int? id)
        {
            Apartment apartment = db.Apartments.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Filter(int? totalRooms, int? maxAdults, int? maxChildren, string city, string sortByPrice)
        {
            var apartments = db.Apartments.Where(a => a.IsDeleted == false).Where(a => a.ApartmentStatus.Name == "Slobodno");


            if (totalRooms.HasValue || maxAdults.HasValue || maxChildren.HasValue || !string.IsNullOrEmpty(city))
            {
                apartments = apartments.Where(a => (totalRooms == null || a.TotalRooms == totalRooms)
                                                            && (maxAdults == null || a.MaxAdults == maxAdults)
                                                            && (maxChildren == null || a.MaxChildren == maxChildren)
                                                            && (string.IsNullOrEmpty(city) || a.City.Name == city));
            }

            switch (sortByPrice)
            {
                case "default":
                    break;
                case "ascending":
                    apartments = apartments.OrderBy(a => a.Price);
                    break;
                case "descending":
                    apartments = apartments.OrderByDescending(a => a.Price);
                    break;
            }

            HttpCookie cityCookie = new HttpCookie("city");
            cityCookie.Value = city;
            HttpContext.Response.Cookies.Remove("city");
            HttpContext.Response.SetCookie(cityCookie);

            HttpCookie roomsCookie = new HttpCookie("rooms");
            roomsCookie.Value = totalRooms.ToString();
            HttpContext.Response.Cookies.Remove("rooms");
            HttpContext.Response.SetCookie(roomsCookie)
                ;
            HttpCookie adultsCookie = new HttpCookie("adults");
            adultsCookie.Value = maxAdults.ToString();
            HttpContext.Response.Cookies.Remove("adults");
            HttpContext.Response.SetCookie(adultsCookie);


            HttpCookie childrenCookie = new HttpCookie("children");
            childrenCookie.Value = maxChildren.ToString();
            HttpContext.Response.Cookies.Remove("children");
            HttpContext.Response.SetCookie(childrenCookie);

            HttpCookie sortCookie = new HttpCookie("sort");
            sortCookie.Value = sortByPrice;
            HttpContext.Response.Cookies.Remove("sort");
            HttpContext.Response.SetCookie(sortCookie);

            return PartialView("_FilteredApartments", apartments.ToList());
        }


        [HttpPost]
        public ActionResult LeaveReview(int apartmentId, string userName, ApartmentReview review)
        {
            
            try
            {
                var user = db.AspNetUsers.Where(u => u.Email == userName).FirstOrDefault();
                review.UserId = user.Id;
                review.CreatedAt = DateTime.Now;
                review.Guid = Guid.NewGuid();
                review.ApartmentId = apartmentId;
                db.ApartmentReviews.Add(review);
                db.SaveChanges();
                return Json(new { success = true, message = Resource.ajaxReviewMessageSuccess });

            }
            catch (Exception)
            {
                return Json(new { success = true, message = Resource.ajaxReviewMessageFailure });
            }
        }

        [HttpPost]
        public ActionResult ReserveApartment(string email, string phone, string address, int apartmentId, int? userId)
        {
            var apartment = db.Apartments.Find(apartmentId);

            var recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (recaptchaHelper.Response != null)
            {
                if (String.IsNullOrEmpty(recaptchaHelper.Response))
                {
                    return Json(new { success = false, message = Resource.captchaEmpty });
                }

                var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
                if (!recaptchaResult.Success)
                {
                    return Json(new { success = false, message = Resource.captchaWrong });
                }
            }
            

            

            try
            {
                ApartmentReservation reservation = new ApartmentReservation
                {
                    ApartmentId = apartmentId,
                    Guid = Guid.NewGuid(),
                    CreatedAt = DateTime.Now
                };

                if (userId != null)
                {
                    reservation.UserId = userId;
                }
                else
                {
                    reservation.UserName = email;
                    reservation.UserEmail = email;
                    reservation.UserAddress = address;
                    reservation.UserPhone = phone;
                }
                
                //Set to reserved
                apartment.ApartmentStatus = db.ApartmentStatus.FirstOrDefault(s => s.Id == 2);

                db.SaveChanges();


                return Json(new { success = true, message = Resource.ajaxReserveMessageSuccess });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = Resource.ajaxReserveMessageFailure });
            }
        }

        [HttpPost]
        public ActionResult ChangeLanguage(string culture)
        {
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            HttpCookie cookie = new HttpCookie("culture", culture);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
    }
}
