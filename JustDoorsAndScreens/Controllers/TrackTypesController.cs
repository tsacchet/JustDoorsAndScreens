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
    public class TrackTypesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: TrackTypes
        public ActionResult Index()
        {
            return View(db.TrackTypes.ToList());
        }

        // GET: TrackTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackType trackType = db.TrackTypes.Find(id);
            if (trackType == null)
            {
                return HttpNotFound();
            }
            return View(trackType);
        }

        // GET: TrackTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrackTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrackTypeID,TrackTypeName")] TrackType trackType)
        {
            if (ModelState.IsValid)
            {
                db.TrackTypes.Add(trackType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trackType);
        }

        // GET: TrackTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackType trackType = db.TrackTypes.Find(id);
            if (trackType == null)
            {
                return HttpNotFound();
            }
            return View(trackType);
        }

        // POST: TrackTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrackTypeID,TrackTypeName")] TrackType trackType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trackType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trackType);
        }

        // GET: TrackTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackType trackType = db.TrackTypes.Find(id);
            if (trackType == null)
            {
                return HttpNotFound();
            }
            return View(trackType);
        }

        // POST: TrackTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrackType trackType = db.TrackTypes.Find(id);
            db.TrackTypes.Remove(trackType);
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
