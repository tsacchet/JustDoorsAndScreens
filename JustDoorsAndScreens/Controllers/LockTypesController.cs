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
    public class LockTypesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: LockTypes
        public ActionResult Index()
        {
            return View(db.LockTypes.ToList());
        }

        // GET: LockTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LockType lockType = db.LockTypes.Find(id);
            if (lockType == null)
            {
                return HttpNotFound();
            }
            return View(lockType);
        }

        // GET: LockTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LockTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LockTypeID,LockTypeName")] LockType lockType)
        {
            if (ModelState.IsValid)
            {
                db.LockTypes.Add(lockType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lockType);
        }

        // GET: LockTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LockType lockType = db.LockTypes.Find(id);
            if (lockType == null)
            {
                return HttpNotFound();
            }
            return View(lockType);
        }

        // POST: LockTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LockTypeID,LockTypeName")] LockType lockType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lockType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lockType);
        }

        // GET: LockTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LockType lockType = db.LockTypes.Find(id);
            if (lockType == null)
            {
                return HttpNotFound();
            }
            return View(lockType);
        }

        // POST: LockTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LockType lockType = db.LockTypes.Find(id);
            db.LockTypes.Remove(lockType);
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
