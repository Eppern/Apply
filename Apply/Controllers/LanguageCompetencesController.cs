using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apply.Models;

namespace Apply.Controllers
{
    public class LanguageCompetencesController : Controller
    {
        private ApplyEntities db = new ApplyEntities();

        // GET: LanguageCompetences
        public ActionResult Index()
        {
            var languageCompetences = db.LanguageCompetences.Include(l => l.AspNetUser).Include(l => l.AspNetUser1).Include(l => l.LanguageCompetenceLevel);
            ViewBag.currentUser = db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault();
            return View(languageCompetences.ToList());
        }

        // GET: LanguageCompetences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageCompetence languageCompetence = db.LanguageCompetences.Find(id);
            if (languageCompetence == null)
            {
                return HttpNotFound();
            }
            return View(languageCompetence);
        }

        // GET: LanguageCompetences/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.LanguageCompetenceLevelId = new SelectList(db.LanguageCompetenceLevels, "LanguageCompetenceLevelId", "LevelName");
            return View();
        }

        // POST: LanguageCompetences/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LanguageCompetenceId,LanguageName,CreatedById,ModifiedById,DateCreated,DateModified,LanguageCompetenceLevelId")] LanguageCompetence languageCompetence)
        {
            if (ModelState.IsValid)
            {
                languageCompetence.CreatedById = (db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault());
                languageCompetence.ModifiedById = languageCompetence.CreatedById;
                languageCompetence.DateCreated = DateTime.Now;
                languageCompetence.DateModified = languageCompetence.DateCreated;
                db.LanguageCompetences.Add(languageCompetence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", languageCompetence.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", languageCompetence.ModifiedById);
            ViewBag.LanguageCompetenceLevelId = new SelectList(db.LanguageCompetenceLevels, "LanguageCompetenceLevelId", "LevelName", languageCompetence.LanguageCompetenceLevelId);
            return View(languageCompetence);
        }

        // GET: LanguageCompetences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageCompetence languageCompetence = db.LanguageCompetences.Find(id);
            if (languageCompetence == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", languageCompetence.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", languageCompetence.ModifiedById);
            ViewBag.LanguageCompetenceLevelId = new SelectList(db.LanguageCompetenceLevels, "LanguageCompetenceLevelId", "LevelName", languageCompetence.LanguageCompetenceLevelId);
            return View(languageCompetence);
        }

        // POST: LanguageCompetences/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LanguageCompetenceId,LanguageName,LanguageCompetenceLevelId")] LanguageCompetence languageCompetence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(languageCompetence).State = EntityState.Modified;
                db.Entry(languageCompetence).Property(x => x.CreatedById).IsModified = false;
                db.Entry(languageCompetence).Property(x => x.DateCreated).IsModified = false;
                languageCompetence.ModifiedById = (db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault());
                languageCompetence.DateModified = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", languageCompetence.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", languageCompetence.ModifiedById);
            ViewBag.LanguageCompetenceLevelId = new SelectList(db.LanguageCompetenceLevels, "LanguageCompetenceLevelId", "LevelName", languageCompetence.LanguageCompetenceLevelId);
            return View(languageCompetence);
        }

        // GET: LanguageCompetences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageCompetence languageCompetence = db.LanguageCompetences.Find(id);
            if (languageCompetence == null)
            {
                return HttpNotFound();
            }
            return View(languageCompetence);
        }

        // POST: LanguageCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LanguageCompetence languageCompetence = db.LanguageCompetences.Find(id);
            db.LanguageCompetences.Remove(languageCompetence);
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
