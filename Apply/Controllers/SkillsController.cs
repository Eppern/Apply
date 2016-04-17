using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apply.Models;
using Microsoft.AspNet.Identity;

namespace Apply.Controllers
{
    public class SkillsController : Controller
    {
        private ApplyEntities db = new ApplyEntities();

        // GET: Skills
        public ActionResult Index()
        {
            var skills = db.Skills.Include(s => s.AspNetUser).Include(s => s.AspNetUser1).Include(s => s.SkillLevel);
            ViewBag.currentUser = db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault();
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
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "SkillLevelId", "LevelName");
            return View();
        }

        // POST: Skills/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkillId,SkillName,CreatedById,ModifiedById,DateCreated,DateModified,SkillLevelId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.CreatedById = (db.AspNetUsers.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault());
                skill.ModifiedById = skill.CreatedById;
                skill.DateCreated = DateTime.Now;
                skill.DateModified = skill.DateCreated;
                db.Skills.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", skill.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", skill.ModifiedById);
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "SkillLevelId", "LevelName", skill.SkillLevelId);
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
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "SkillLevelId", "LevelName", skill.SkillLevelId);
            return View(skill);
        }

        // POST: Skills/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
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
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", skill.CreatedById);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", skill.ModifiedById);
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "SkillLevelId", "LevelName", skill.SkillLevelId);
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
            db.Skills.Remove(skill);
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
