using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Apply.Models;
using Apply.Helpers;
using Microsoft.AspNet.Identity;

namespace Apply.Controllers
{
    public class WorkExperiencesController : Controller
    {
        private ApplyEntities db = new ApplyEntities();

        // GET: WorkExperiences
        public ActionResult Index()
        {
            var workExperiences = db.WorkExperiences.Include(w => w.AspNetUser).Include(w => w.AspNetUser1).OrderByDescending(e => e.YearEnd);
            ViewBag.currentUser = db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault();
            return View(workExperiences.ToList());
        }

        // GET: WorkExperiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkExperience workExperience = db.WorkExperiences.Find(id);
            if (workExperience == null)
            {
                return HttpNotFound();
            }
            return View(workExperience);
        }

        // GET: WorkExperiences/Create
        public ActionResult Create()
        {
            ViewBag.Month = UserHelpers.GetMonths();
            ViewBag.Year = UserHelpers.GetYears();
            return View();
        }

        // POST: WorkExperiences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MonthStart,MonthEnd,YearStart,YearEnd,CompanyName,PositionHeld,Notes")] WorkExperience workExperience)
        {
            if (ModelState.IsValid)
            {
                workExperience.CreatedById = User.Identity.GetUserId();
                workExperience.ModifiedById = workExperience.CreatedById;
                workExperience.DateCreated = DateTime.Now;
                workExperience.DateModified = workExperience.DateCreated;
                try
                {
                    db.WorkExperiences.Add(workExperience);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "WorkExperiences", "Create");
                }
                return RedirectToAction("Index");
            }

            ViewBag.Month = UserHelpers.GetMonths();
            ViewBag.Year = UserHelpers.GetYears();
            return View(workExperience);
        }

        // GET: WorkExperiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkExperience workExperience = db.WorkExperiences.Find(id);
            if (workExperience == null)
            {
                return HttpNotFound();
            }
            ViewBag.Month = UserHelpers.GetMonths();
            ViewBag.Year = UserHelpers.GetYears();
            return View(workExperience);
        }

        // POST: WorkExperiences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkExperienceId,MonthStart,MonthEnd,YearStart,YearEnd,CompanyName,PositionHeld,Notes")] WorkExperience workExperience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workExperience).State = EntityState.Modified;
                db.Entry(workExperience).Property(x => x.CreatedById).IsModified = false;
                db.Entry(workExperience).Property(x => x.DateCreated).IsModified = false;
                workExperience.ModifiedById = User.Identity.GetUserId();
                workExperience.DateModified = DateTime.Now;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "WorkExperiences", "Edit", new {id = workExperience.WorkExperienceId});
                }
                return RedirectToAction("Index");
            }
            return View(workExperience);
        }

        // GET: WorkExperiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkExperience workExperience = db.WorkExperiences.Find(id);
            if (workExperience == null)
            {
                return HttpNotFound();
            }
            return View(workExperience);
        }

        // POST: WorkExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkExperience workExperience = db.WorkExperiences.Find(id);
            try
            {
                db.WorkExperiences.Remove(workExperience);
                db.SaveChanges();
            }
            catch (DbUpdateException ex) {
                var errorHelper = new ControllerHelpers();
                return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "WorkExperiences", "Delete", new { id = workExperience.WorkExperienceId });
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
