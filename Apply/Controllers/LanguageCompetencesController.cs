using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Apply.Helpers;
using Apply.Models;
using Microsoft.AspNet.Identity;

namespace Apply.Controllers
{
    public class LanguageCompetencesController : Controller
    {
        private readonly ApplyEntities db = new ApplyEntities();

        // GET: LanguageCompetences
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var languageCompetences = db.LanguageCompetences.Where(l => l.CreatedById == userId);
            return View(languageCompetences.ToList());
        }

        // GET: LanguageCompetences/Create
        public ActionResult Create()
        {
            ViewBag.LanguageCompetencesLevels = db.LanguageCompetenceLevels.ToList();
            return View();
        }

        // POST: LanguageCompetences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LanguageName,LanguageCompetenceLevelId")] LanguageCompetence languageCompetence)
        {
            if (ModelState.IsValid)
            {
                languageCompetence.CreatedById = User.Identity.GetUserId();
                languageCompetence.ModifiedById = languageCompetence.CreatedById;
                languageCompetence.DateCreated = DateTime.Now;
                languageCompetence.DateModified = languageCompetence.DateCreated;
                try
                {
                    db.LanguageCompetences.Add(languageCompetence);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "LanguageCompetences", "Create");
                }
                return RedirectToAction("Index");
            }
            ViewBag.LanguageCompetencesLevels = db.LanguageCompetenceLevels.ToList();
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
            ViewBag.LanguageCompetencesLevels = db.LanguageCompetenceLevels.ToList();
            return View(languageCompetence);
        }

        // POST: LanguageCompetences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LanguageCompetenceId,LanguageName,LanguageCompetenceLevelId")] LanguageCompetence languageCompetence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(languageCompetence).State = EntityState.Modified;
                db.Entry(languageCompetence).Property(x => x.CreatedById).IsModified = false;
                db.Entry(languageCompetence).Property(x => x.DateCreated).IsModified = false;
                languageCompetence.ModifiedById = User.Identity.GetUserId();
                languageCompetence.DateModified = DateTime.Now;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Educations", "Edit", new {id = languageCompetence.LanguageCompetenceId});
                }
                return RedirectToAction("Index");
            }
            ViewBag.LanguageCompetencesLevels = new SelectList(db.LanguageCompetenceLevels, "LanguageCompetenceLevelId", "LevelName", languageCompetence.LanguageCompetenceLevelId);
            return View(languageCompetence);
        }

        // GET: LanguageCompetences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var languageCompetence = db.LanguageCompetences.Find(id);
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
            try
            {
                db.LanguageCompetences.Remove(languageCompetence);
                db.SaveChanges();
            }
            catch (DbUpdateException ex) {
                var errorHelper = new ControllerHelpers();
                return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Educations", "Create", new { id = id });
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
