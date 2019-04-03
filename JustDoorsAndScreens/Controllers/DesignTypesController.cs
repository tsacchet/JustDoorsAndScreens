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
    public class DesignTypesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: DesignTypes
        public ActionResult Index()
        {
            return View(db.DesignTypes.ToList());
        }

        // GET: DesignTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignType designType = db.DesignTypes.Find(id);
            if (designType == null)
            {
                return HttpNotFound();
            }
            return View(designType);
        }

        // GET: DesignTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DesignTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DesignTypeID,DesignTypeName")] DesignType designType)
        {
            if (ModelState.IsValid)
            {
                db.DesignTypes.Add(designType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(designType);
        }

        // GET: DesignTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignType designType = db.DesignTypes.Find(id);
            if (designType == null)
            {
                return HttpNotFound();
            }
            return View(designType);
        }

        // POST: DesignTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignTypeID,DesignTypeName")] DesignType designType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(designType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(designType);
        }

        // GET: DesignTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignType designType = db.DesignTypes.Find(id);
            if (designType == null)
            {
                return HttpNotFound();
            }
            return View(designType);
        }

        // POST: DesignTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DesignType designType = db.DesignTypes.Find(id);
            db.DesignTypes.Remove(designType);
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
