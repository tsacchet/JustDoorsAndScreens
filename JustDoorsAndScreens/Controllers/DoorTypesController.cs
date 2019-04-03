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
    public class DoorTypesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: DoorTypes
        public ActionResult Index()
        {
            return View(db.DoorTypes.ToList());
        }

        // GET: DoorTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorType doorType = db.DoorTypes.Find(id);
            if (doorType == null)
            {
                return HttpNotFound();
            }
            return View(doorType);
        }

        // GET: DoorTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoorTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoorTypeID,DoorTypeName")] DoorType doorType)
        {
            if (ModelState.IsValid)
            {
                db.DoorTypes.Add(doorType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doorType);
        }

        // GET: DoorTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorType doorType = db.DoorTypes.Find(id);
            if (doorType == null)
            {
                return HttpNotFound();
            }
            return View(doorType);
        }

        // POST: DoorTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoorTypeID,DoorTypeName")] DoorType doorType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doorType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doorType);
        }

        // GET: DoorTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorType doorType = db.DoorTypes.Find(id);
            if (doorType == null)
            {
                return HttpNotFound();
            }
            return View(doorType);
        }

        // POST: DoorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoorType doorType = db.DoorTypes.Find(id);
            db.DoorTypes.Remove(doorType);
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
