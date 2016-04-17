using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Apply.Helpers;
using Apply.Models;
using Microsoft.AspNet.Identity;

namespace Apply.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ApplyEntities db = new ApplyEntities();

        // GET: Skills
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var skills = db.Skills.Where(s => s.CreatedById == userId);
            return View(skills.ToList());
        }

        // GET: Skills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            ViewBag.SkillLevels = db.SkillLevels.ToList();
            return View();
        }

        // POST: Skills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkillName,SkillLevelId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.CreatedById = User.Identity.GetUserId();
                skill.ModifiedById = skill.CreatedById;
                skill.DateCreated = DateTime.Now;
                skill.DateModified = skill.DateCreated;
                try
                {
                    db.Skills.Add(skill);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Skills", "Create");
                }
                return RedirectToAction("Index");
            }

            ViewBag.SkillLevels = db.SkillLevels.ToList();
            return View(skill);
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewBag.SkillLevels = db.SkillLevels.ToList();
            return View(skill);
        }

        // POST: Skills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SkillId,SkillName,SkillLevelId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                skill.DateModified = DateTime.Now;
                skill.ModifiedById = User.Identity.GetUserId();
                db.Entry(skill).Property(x => x.CreatedById).IsModified = false;
                db.Entry(skill).Property(x => x.DateCreated).IsModified = false;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    var errorHelper = new ControllerHelpers();
                    return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Skills", "Edit", new { id = skill.SkillId });
                }
                return RedirectToAction("Index");
            }
            ViewBag.SkillLevels = db.SkillLevels.ToList();
            return View(skill);
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skills.Find(id);
            try
            {
                db.Skills.Remove(skill);
                db.SaveChanges();
            }
            catch (DbUpdateException ex) {
                var errorHelper = new ControllerHelpers();
                return errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Skills", "Delete", new { id = id });
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
