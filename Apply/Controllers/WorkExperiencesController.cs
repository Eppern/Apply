using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apply.Models;
using Apply.Helpers;

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
            ViewBag.Month = UserHelpers.Month();
            ViewBag.Year = UserHelpers.Year();
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: WorkExperiences/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkExperienceId,CreatedById,ModifiedById,DateCreated,DateModified,MonthStart,MonthEnd,YearStart,YearEnd,CompanyName,PositionHeld,Notes")] WorkExperience workExperience)
        {
            if (ModelState.IsValid)
            {
                workExperience.CreatedById = (db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault());
                workExperience.ModifiedById = workExperience.CreatedById;
                workExperience.DateCreated = DateTime.Now;
                workExperience.DateModified = workExperience.DateCreated;
                db.WorkExperiences.Add(workExperience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", workExperience.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", workExperience.ModifiedById);
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
            ViewBag.Month = UserHelpers.Month();
            ViewBag.Year = UserHelpers.Year();
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", workExperience.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", workExperience.ModifiedById);
            return View(workExperience);
        }

        // POST: WorkExperiences/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkExperienceId,CreatedById,ModifiedById,DateCreated,DateModified,MonthStart,MonthEnd,YearStart,YearEnd,CompanyName,PositionHeld,Notes")] WorkExperience workExperience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workExperience).Property(x => x.CreatedById).IsModified = false;
                db.Entry(workExperience).Property(x => x.DateCreated).IsModified = false;
                workExperience.ModifiedById = (db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault());
                workExperience.DateModified = DateTime.Now;
                db.Entry(workExperience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", workExperience.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", workExperience.ModifiedById);
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
            db.WorkExperiences.Remove(workExperience);
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
