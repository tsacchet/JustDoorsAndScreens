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
    public class ColorTypesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: ColorTypes
        public ActionResult Index()
        {
            return View(db.ColorTypes.ToList());
        }

        // GET: ColorTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorType colorType = db.ColorTypes.Find(id);
            if (colorType == null)
            {
                return HttpNotFound();
            }
            return View(colorType);
        }

        // GET: ColorTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ColorTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorTypeID,ColorTypeName")] ColorType colorType)
        {
            if (ModelState.IsValid)
            {
                db.ColorTypes.Add(colorType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(colorType);
        }

        // GET: ColorTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorType colorType = db.ColorTypes.Find(id);
            if (colorType == null)
            {
                return HttpNotFound();
            }
            return View(colorType);
        }

        // POST: ColorTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorTypeID,ColorTypeName")] ColorType colorType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colorType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colorType);
        }

        // GET: ColorTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorType colorType = db.ColorTypes.Find(id);
            if (colorType == null)
            {
                return HttpNotFound();
            }
            return View(colorType);
        }

        // POST: ColorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ColorType colorType = db.ColorTypes.Find(id);
            db.ColorTypes.Remove(colorType);
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
