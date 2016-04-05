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
    public class EducationsController : Controller
    {
        private ApplyEntities db = new ApplyEntities();

        // GET: Educations
        public ActionResult Index()
        {
            var educations = db.Educations.Include(e => e.AspNetUser).Include(e => e.AspNetUser1).OrderByDescending(e => e.YearEnd);
            ViewBag.currentUser = db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault();
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
            ViewBag.Month = UserHelpers.Month();
            ViewBag.Year = UserHelpers.Year();
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Educations/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EducationId,MonthStart,MonthEnd,YearStart,YearEnd,InstitutionName,Notes,CreatedById,ModifiedById,DateCreated,DateModified")] Education education)
        {
            if (ModelState.IsValid)
            {
                education.CreatedById = (db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault());
                education.ModifiedById = education.CreatedById;
                education.DateCreated = DateTime.Now;
                education.DateModified = education.DateCreated;
                db.Educations.Add(education);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", education.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", education.ModifiedById);
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
            ViewBag.Month = UserHelpers.Month();
            ViewBag.Year = UserHelpers.Year();
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", education.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", education.ModifiedById);
            return View(education);
        }

        // POST: Educations/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EducationId,MonthStart,MonthEnd,YearStart,YearEnd,InstitutionName,Notes")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Entry(education).Property(x => x.CreatedById).IsModified = false;
                db.Entry(education).Property(x => x.DateCreated).IsModified = false;
                education.ModifiedById = (db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault());
                education.DateModified = DateTime.Now;
                db.Entry(education).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", education.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", education.ModifiedById);
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
            db.Educations.Remove(education);
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
