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
    public class FlyScreenItemsController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: FlyScreenItems
        public ActionResult Index()
        {
            var flyScreenItems = db.FlyScreenItems.Include(f => f.FlysScreenType).Include(f => f.Quote);
            return View(flyScreenItems.ToList());
        }

        // GET: FlyScreenItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);
            if (flyScreenItem == null)
            {
                return HttpNotFound();
            }
            return View(flyScreenItem);
        }

        public ActionResult DetailsFlyScreenItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);
            if (flyScreenItem == null)
            {
                return HttpNotFound();
            }
            return View(flyScreenItem);
        }

        // GET: FlyScreenItems/Create
        public ActionResult Create()
        {
            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName");
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName");
            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer");
            return View();
        }

        // POST: FlyScreenItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlyScreenItemsID,QuoteID,ColorTypeID,FlyScreenTypeID,Width,Depth,Qty,Cost,W1,D1,Q1,W2,D2,Q2,W3,D3,Q3,W4,D4,Q4,W5,D5,Q5,W6,D6,Q6,W7,D7,Q7,W8,D8,Q8,W9,D9,Q9,W10,D10,Q10,W11,D11,W12,D12,Q12,W13,D13,Q13,W14,D14,Q14,W15,D15,Q15,Description,Required")] FlyScreenItem flyScreenItem)
        {
            if (ModelState.IsValid)
            {
                db.FlyScreenItems.Add(flyScreenItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName", flyScreenItem.FlyScreenTypeID);
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName");
            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer", flyScreenItem.QuoteID);
            return View(flyScreenItem);
        }



        // GET: FlyScreenItems/Create
        public ActionResult CreateFlyScreenItem(int? id)
        {
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == id).Select(x => x.Customer).First();


            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName");
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName");
            ViewBag.QuoteID = id;
            return View();
        }

        // POST: FlyScreenItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFlyScreenItem([Bind(Include = "FlyScreenItemsID,QuoteID,ColorTypeID,FlyScreenTypeID,Width,Depth,Qty,Cost,W1,D1,Q1,W2,D2,Q2,W3,D3,Q3,W4,D4,Q4,W5,D5,Q5,W6,D6,Q6,W7,D7,Q7,W8,D8,Q8,W9,D9,Q9,W10,D10,Q10,W11,D11,W12,D12,Q12,W13,D13,Q13,W14,D14,Q14,W15,D15,Q15,Description,Required")] FlyScreenItem flyScreenItem)
        {
            if (ModelState.IsValid)
            {
                db.FlyScreenItems.Add(flyScreenItem);
                db.SaveChanges();
                return RedirectToAction("QuoteDetails/" + flyScreenItem.QuoteID, "Quotes");
            }

            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == flyScreenItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName", flyScreenItem.FlyScreenTypeID);
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName",flyScreenItem.ColorTypeID);
            ViewBag.QuoteID = flyScreenItem.QuoteID;
            return View(flyScreenItem);
        }


        // GET: FlyScreenItems/Edit/5
        public ActionResult EditFlyScreenItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);
            if (flyScreenItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == flyScreenItem.QuoteID).Select(x => x.Customer).First();

            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName", flyScreenItem.FlyScreenTypeID);
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", flyScreenItem.ColorTypeID);
            ViewBag.QuoteID = flyScreenItem.QuoteID;
            return View(flyScreenItem);
        }

        // POST: FlyScreenItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFlyScreenItem([Bind(Include = "FlyScreenItemsID,QuoteID,ColorTypeID,FlyScreenTypeID,Width,Depth,Qty,Cost,W1,D1,Q1,W2,D2,Q2,W3,D3,Q3,W4,D4,Q4,W5,D5,Q5,W6,D6,Q6,W7,D7,Q7,W8,D8,Q8,W9,D9,Q9,W10,D10,Q10,W11,D11,W12,D12,Q12,W13,D13,Q13,W14,D14,Q14,W15,D15,Q15,Description,Required")] FlyScreenItem flyScreenItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flyScreenItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuoteDetails/" + flyScreenItem.QuoteID, "Quotes");
            }
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == flyScreenItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName", flyScreenItem.FlyScreenTypeID);
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", flyScreenItem.ColorTypeID);
            ViewBag.QuoteID = flyScreenItem.QuoteID;
            return View(flyScreenItem);
        }




        // GET: FlyScreenItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);
            if (flyScreenItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName", flyScreenItem.FlyScreenTypeID);
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", flyScreenItem.ColorTypeID);
            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer", flyScreenItem.QuoteID);
            return View(flyScreenItem);
        }

        // POST: FlyScreenItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlyScreenItemsID,QuoteID,ColorTypeID,FlyScreenTypeID,Width,Depth,Qty,Cost,W1,D1,Q1,W2,D2,Q2,W3,D3,Q3,W4,D4,Q4,W5,D5,Q5,W6,D6,Q6,W7,D7,Q7,W8,D8,Q8,W9,D9,Q9,W10,D10,Q10,W11,D11,W12,D12,Q12,W13,D13,Q13,W14,D14,Q14,W15,D15,Q15,Description,Required")] FlyScreenItem flyScreenItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flyScreenItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlyScreenTypeID = new SelectList(db.FlysScreenTypes, "FlyScreenTypeID", "FlyScreenName", flyScreenItem.FlyScreenTypeID);
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", flyScreenItem.ColorTypeID);
            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer", flyScreenItem.QuoteID);
            return View(flyScreenItem);
        }


        // GET: FlyScreenItems/Delete/5
        public ActionResult DeleteFlyScreenItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);
            if (flyScreenItem == null)
            {
                return HttpNotFound();
            }
            return View(flyScreenItem);
        }


        // GET: FlyScreenItems/Delete/5
        public ActionResult DeleteFlyScreenItemConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);

            var quoteid = flyScreenItem.QuoteID;
            db.FlyScreenItems.Remove(flyScreenItem);
            db.SaveChanges();
            return RedirectToAction("QuoteDetails/" + quoteid, "Quotes");
        }


        // GET: FlyScreenItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);
            if (flyScreenItem == null)
            {
                return HttpNotFound();
            }
            return View(flyScreenItem);
        }

        // POST: FlyScreenItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlyScreenItem flyScreenItem = db.FlyScreenItems.Find(id);
            db.FlyScreenItems.Remove(flyScreenItem);
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
