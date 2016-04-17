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
    [Authorize]
    public class PersonalInfoController : Controller
    {
        private ApplyEntities db = new ApplyEntities();

        // GET: PersonalInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // GET: PersonalInfo/Edit/5
        public ActionResult Edit()
        {
            PersonalInfoViewModel model = new PersonalInfoViewModel();
            model.Applicant = db.Applicants.Find(User.Identity.GetUserId());
            model.Salutations = new SelectList(db.Salutations, "SalutationId", "ShortName", model.Applicant.SalutationId);
            if (model.Applicant == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: PersonalInfo/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicantId,ForeName,SurName,DOB,Title,AddressId,SalutationId,CVId,CreatedById,ModifiedById,DateCreated,DateModified")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Street", applicant.AddressId);
            ViewBag.CreatedById = new SelectList(db.AspNetUsers, "Id", "Email", applicant.CreatedById);
            ViewBag.CVId = new SelectList(db.CVs, "CVId", "CreatedById", applicant.CVId);
            ViewBag.ModifiedById = new SelectList(db.AspNetUsers, "Id", "Email", applicant.ModifiedById);
            ViewBag.SalutationId = new SelectList(db.Salutations, "SalutationId", "ShortName", applicant.SalutationId);
            return View(applicant);
        }

        // GET: PersonalInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // POST: PersonalInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            db.Applicants.Remove(applicant);
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
