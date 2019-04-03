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
    public class HingeTypesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: HingeTypes
        public ActionResult Index()
        {
            return View(db.HingeTypes.ToList());
        }

        // GET: HingeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HingeType hingeType = db.HingeTypes.Find(id);
            if (hingeType == null)
            {
                return HttpNotFound();
            }
            return View(hingeType);
        }

        // GET: HingeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HingeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HingeTypeID,HingeTypeName")] HingeType hingeType)
        {
            if (ModelState.IsValid)
            {
                db.HingeTypes.Add(hingeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hingeType);
        }

        // GET: HingeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HingeType hingeType = db.HingeTypes.Find(id);
            if (hingeType == null)
            {
                return HttpNotFound();
            }
            return View(hingeType);
        }

        // POST: HingeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HingeTypeID,HingeTypeName")] HingeType hingeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hingeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hingeType);
        }

        // GET: HingeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HingeType hingeType = db.HingeTypes.Find(id);
            if (hingeType == null)
            {
                return HttpNotFound();
            }
            return View(hingeType);
        }

        // POST: HingeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HingeType hingeType = db.HingeTypes.Find(id);
            db.HingeTypes.Remove(hingeType);
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
