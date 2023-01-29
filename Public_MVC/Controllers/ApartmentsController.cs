using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Public_MVC.Context;
using Public_MVC.Extensions;
using Public_MVC.Models;
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
            var apartments = db.Apartments.Where(a => a.IsDeleted == false);

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
        public ActionResult LeaveReview(int apartmentId, ApartmentReview review)
        {
            try
            {

                review.UserId = 1;
                review.ApartmentId = apartmentId;
                db.ApartmentReviews.Add(review);
                db.SaveChanges();
                return Json(new { success = true, message = "Review submitted successfully!" });

            }
            catch (Exception)
            {
                return Json(new { success = true, message = "An error occurred while submitting the review. Please try again later!" });
            }
        }

        [HttpPost]
        public ActionResult ReserveApartment(string email, string phone, string address, int apartmentId)
        {
            var apartment = db.Apartments.Find(apartmentId);

            var recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                return Json(new { success = false, message = "Captcha answer cannot be empty!" });
            }

            var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (!recaptchaResult.Success)
            {
                return Json(new { success = false, message = "Incorrect captcha answer!" });
            }

            try
            {
                var user = new AspNetUser
                {
                    CreatedAt = DateTime.Now,
                    Guid = Guid.NewGuid(),
                    IsAdmin = false,
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumber = phone,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Address = address,
                    UserName = email
                };


                db.AspNetUsers.Add(user);

                apartment.ApartmentStatus = db.ApartmentStatus.FirstOrDefault(s => s.Id == 2);



                db.ApartmentReservations.Add(new ApartmentReservation
                {
                    ApartmentId = apartmentId,
                    Guid = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    UserAddress = user.Address,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    UserPhone = user.PhoneNumber
                });

                db.SaveChanges();


                return Json(new { success = true, message = "Apartment reserved successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while reserving the apartment." });
            }
        }


        //Situacija kada nema povezanih slika
        public FileResult GetImage(int imageId)
        {
            var image = db.ApartmentPictures.FirstOrDefault(p => p.Id == imageId);


            byte[] imageBytes = image.Base64Content.FromBase64String();
            return File(imageBytes, "image/png");
        }


    }
}
