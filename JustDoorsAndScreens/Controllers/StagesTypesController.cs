using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustDoorsAndScreens;

namespace JustDoorsAndScreens.Controllers
{
    public class StagesTypesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: StagesTypes
        public ActionResult Index()
        {
            return View(db.StagesTypes.ToList());
        }

        // GET: StagesTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StagesType stagesType = db.StagesTypes.Find(id);
            if (stagesType == null)
            {
                return HttpNotFound();
            }
            return View(stagesType);
        }

        // GET: StagesTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StagesTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StageID,StageName")] StagesType stagesType)
        {
            if (ModelState.IsValid)
            {
                db.StagesTypes.Add(stagesType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stagesType);
        }

        // GET: StagesTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StagesType stagesType = db.StagesTypes.Find(id);
            if (stagesType == null)
            {
                return HttpNotFound();
            }
            return View(stagesType);
        }

        // POST: StagesTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StageID,StageName")] StagesType stagesType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stagesType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stagesType);
        }

        // GET: StagesTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StagesType stagesType = db.StagesTypes.Find(id);
            if (stagesType == null)
            {
                return HttpNotFound();
            }
            return View(stagesType);
        }

        // POST: StagesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StagesType stagesType = db.StagesTypes.Find(id);
            db.StagesTypes.Remove(stagesType);
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
