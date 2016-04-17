using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Apply.Models;
using Apply.Helpers;
using Microsoft.AspNet.Identity;

namespace Apply.Controllers
{
    public class EducationsController : Controller
    {
        private readonly ApplyEntities db = new ApplyEntities();

        // GET: Educations
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var educations = db.Educations
                .Where(e => e.CreatedById == userId)
                .OrderByDescending(e => e.YearEnd);
            return View(educations.ToList());
        }

        // GET: Educations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // GET: Educations/Create
        public ActionResult Create()
        {
            ViewBag.Month = UserHelpers.GetMonths();
            ViewBag.Year = UserHelpers.GetYears();
            return View();
        }

        // POST: Educations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MonthStart,MonthEnd,YearStart,YearEnd,InstitutionName,Notes")] Education education)
        {
            if (ModelState.IsValid)
            {
                education.CreatedById = User.Identity.GetUserId();
                education.ModifiedById = education.CreatedById;
                education.DateCreated = DateTime.Now;
                education.DateModified = education.DateCreated;
                try
                {
                    db.Educations.Add(education);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Educations", "Create");
                }
                return RedirectToAction("Index");
            }

            ViewBag.Month = UserHelpers.GetMonths();
            ViewBag.Year = UserHelpers.GetYears();
            return View(education);
        }

        // GET: Educations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            ViewBag.Month = UserHelpers.GetMonths();
            ViewBag.Year = UserHelpers.GetYears();
            return View(education);
        }

        // POST: Educations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EducationId,MonthStart,MonthEnd,YearStart,YearEnd,InstitutionName,Notes")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Entry(education).State = EntityState.Modified;
                db.Entry(education).Property(x => x.CreatedById).IsModified = false;
                db.Entry(education).Property(x => x.DateCreated).IsModified = false;
                education.ModifiedById = User.Identity.GetUserId();
                education.DateModified = DateTime.Now;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Educations", "Edit", new { id = education.EducationId });
                }
                return RedirectToAction("Index");
            }
            ViewBag.Month = UserHelpers.GetMonths();
            ViewBag.Year = UserHelpers.GetYears();
            return View(education);
        }

        // GET: Educations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Education education = db.Educations.Find(id);
            try
            {
                db.Educations.Remove(education);
                db.SaveChanges();
            }
            catch (DbUpdateException ex) {
                var errorHelper = new ControllerHelpers();
                return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Educations", "Delete", new { id = education.EducationId });
            }
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
