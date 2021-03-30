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
    public class DoorItemsController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: DoorItems
        public ActionResult Index()
        {
            var doorItems = db.DoorItems.Include(d => d.ColorType).Include(d => d.DesignType).Include(d => d.DoorType).Include(d => d.LockType).Include(d => d.Quote);
            return View(doorItems.ToList());
        }

        //public ActionResult GetTrackTypes()
        //{
        //    var query = db.TrackTypes.ToList();

        //    return Json(new { data = query.ToArray() }, JsonRequestBehavior.AllowGet);
        //}

        // GET: DoorItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }
            return View(doorItem);
        }

        public ActionResult DetailsDoorItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }
            return View(doorItem);
        }

        public ActionResult DetailsSliderItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.SliderTop = db.TrackTypes.Where(x => x.TrackTypeID == doorItem.SliderTopTrackType).Select(x => x.TrackTypeName).First(); 
            ViewBag.SliderBottom = db.TrackTypes.Where(x => x.TrackTypeID == doorItem.SliderBottomTrackType).Select(x => x.TrackTypeName).First();
            ViewBag.SliderSideChannel = db.TrackTypes.Where(x => x.TrackTypeID == doorItem.SliderSideChannelType).Select(x => x.TrackTypeName).First();

            return View(doorItem);
        }

        // GET: DoorItems/Create
        public ActionResult Create()
        {
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName");
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName");
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes, "DoorTypeID", "DoorTypeName");

            //ViewBag.DoorTypeID = new SelectList(new[] { new KeyValuePair<int, string>(-1, "please select") }).Union(new SelectList(db.DoorTypes, "DoorTypeID", "DoorTypeName"));


            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName");
            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer");
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            ViewBag.HingeTypeID = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName");
            ViewBag.SliderTopTrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderBottomTrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderSideChannelTrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            return View();
        }

        // POST: DoorItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoorItemID,QuoteID,Description,ColorTypeID,LockTypeID,Height,Width1,Width2,Width3,SliderTopTrack,SliderBottomTrack,SliderSideChannel,SliderFs,SliderZs,SliderBugStrip,Cost,DoorTypeID,DesignTypeID,LockHeight,SliderTopTrackWidth,SliderBottomTrackWidth,SliderSideChannelWidth,TrackTypes,F,Z,SliderTopTrackType,SliderBottomTrackType,SliderSideChannelType,HingeType,BugStrip,Required")] DoorItem doorItem)
        {
            if (ModelState.IsValid)
            {
                db.DoorItems.Add(doorItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes, "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer", doorItem.QuoteID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            ViewBag.HingeTypeID = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName");
            ViewBag.SliderTopTrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderBottomTrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderSideChannelTrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            return View(doorItem);
        }

        // GET: DoorItems/Create
        public ActionResult CreateDoorItem(int? id)
        {
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == id).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName");
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName");
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x => x.DoorTypeName.ToUpper().Contains("DOOR")), "DoorTypeID", "DoorTypeName");


            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName");
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName");
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderSideChannelTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");


            ViewBag.QuoteID = id;
            return View();
        }

        // POST: DoorItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoorItem([Bind(Include = "DoorItemID,QuoteID,Description,ColorTypeID,LockTypeID,Height,Width1,Width2,Width3,SliderTopTrack,SliderBottomTrack,SliderSideChannel,SliderFs,SliderZs,SliderBugStrip,Cost,DoorTypeID,DesignTypeID,LockHeight,SliderTopTrackWidth,SliderBottomTrackWidth,SliderSideChannelWidth,TrackTypes,F,Z,SliderTopTrackType,SliderBottomTrackType,SliderSideChannelType,HingeType,BugStrip,Required")] DoorItem doorItem)
        {
            if (ModelState.IsValid)
            {
                db.DoorItems.Add(doorItem);
                db.SaveChanges();
                return RedirectToAction("QuoteDetails/" + doorItem.QuoteID, "Quotes");
            }

            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == doorItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x => x.DoorTypeName.ToUpper().Contains("DOOR")), "DoorTypeID", "DoorTypeName");
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName");
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderSideChannelType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");


            ViewBag.QuoteID = doorItem.QuoteID;

            return View(doorItem);
        }


        // GET: DoorItems/Edit/5
        public ActionResult EditDoorItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == doorItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x => x.DoorTypeName.ToUpper().Contains("DOOR")), "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.TrackTypes);

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName",doorItem.HingeType);
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName",doorItem.SliderTopTrackType);
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName",doorItem.SliderBottomTrackType);
            ViewBag.SliderSideChannelType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName",doorItem.SliderSideChannelType);

            ViewBag.QuoteID = doorItem.QuoteID;
            return View(doorItem);
        }

        // POST: DoorItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoorItem([Bind(Include = "DoorItemID,QuoteID,Description,ColorTypeID,LockTypeID,Height,Width1,Width2,Width3,SliderTopTrack,SliderBottomTrack,SliderSideChannel,SliderFs,SliderZs,SliderBugStrip,Cost,DoorTypeID,DesignTypeID,LockHeight,SliderTopTrackWidth,SliderBottomTrackWidth,SliderSideChannelWidth,TrackTypes,F,Z,SliderTopTrackType,SliderBottomTrackType,SliderSideChannelType,HingeType,BugStrip,Required")] DoorItem doorItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doorItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuoteDetails/" + doorItem.QuoteID, "Quotes");
            }
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == doorItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x => x.DoorTypeName.ToUpper().Contains("DOOR")), "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.TrackTypes);

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName", doorItem.HingeType);
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderTopTrackType);
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderBottomTrackType);
            ViewBag.SliderSideChannelType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderSideChannelType);

            ViewBag.QuoteID = doorItem.QuoteID;
            return View(doorItem);
        }











        // GET: DoorItems/Create
        public ActionResult CreateSliderItem(int? id)
        {
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == id).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName");
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName");
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x =>x.DoorTypeName.ToUpper().Contains("SLIDER")), "DoorTypeID", "DoorTypeName");
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName");
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName");
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderSideChannelType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");


            ViewBag.QuoteID = id;
            return View();
        }

        // POST: DoorItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSliderItem([Bind(Include = "DoorItemID,QuoteID,Description,ColorTypeID,LockTypeID,Height,Width1,Width2,Width3,SliderTopTrack,SliderBottomTrack,SliderSideChannel,SliderFs,SliderZs,SliderBugStrip,Cost,DoorTypeID,DesignTypeID,LockHeight,SliderTopTrackWidth,SliderBottomTrackWidth,SliderSideChannelWidth,TrackTypes,F,Z,SliderTopTrackType,SliderBottomTrackType,SliderSideChannelType,HingeType,BugStrip,Required")] DoorItem doorItem)
        {
            if (ModelState.IsValid)
            {
                db.DoorItems.Add(doorItem);
                db.SaveChanges();
                return RedirectToAction("QuoteDetails/" + doorItem.QuoteID, "Quotes");
            }

            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == doorItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x => x.DoorTypeName.ToUpper().Contains("SLIDER")), "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName");
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
            ViewBag.SliderSideChannelType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName");
                    

            ViewBag.QuoteID = doorItem.QuoteID;

            return View(doorItem);
        }


        // GET: DoorItems/Edit/5
        public ActionResult EditSliderItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == doorItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x => x.DoorTypeName.ToUpper().Contains("SLIDER")), "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.TrackTypes);

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName", doorItem.HingeType);
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderTopTrackType);
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderBottomTrackType);
            ViewBag.SliderSideChannelType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderSideChannelType);

            ViewBag.QuoteID = doorItem.QuoteID;
            return View(doorItem);
        }

        // POST: DoorItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSliderItem([Bind(Include = "DoorItemID,QuoteID,Description,ColorTypeID,LockTypeID,Height,Width1,Width2,Width3,SliderTopTrack,SliderBottomTrack,SliderSideChannel,SliderFs,SliderZs,SliderBugStrip,Cost,DoorTypeID,DesignTypeID,LockHeight,SliderTopTrackWidth,SliderBottomTrackWidth,SliderSideChannelWidth,TrackTypes,F,Z,SliderTopTrackType,SliderBottomTrackType,SliderSideChannelType,HingeType,BugStrip,Required")] DoorItem doorItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doorItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuoteDetails/" + doorItem.QuoteID, "Quotes");
            }
            ViewBag.Customer = db.Quotes.Where(x => x.QuoteID == doorItem.QuoteID).Select(x => x.Customer).First();
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes.Where(x => x.DoorTypeName.ToUpper().Contains("SLIDER")), "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.TrackTypes);

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName", doorItem.HingeType);
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderTopTrackType);
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderBottomTrackType);
            ViewBag.SliderSideChannelType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderSideChannelType);

            ViewBag.QuoteID = doorItem.QuoteID;
            return View(doorItem);
        }


        // GET: DoorItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes, "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.TrackTypes);

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName", doorItem.HingeType);
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderTopTrackType);
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderBottomTrackType);
            ViewBag.SliderSideChannelTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderSideChannelType);

            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer", doorItem.QuoteID);
            return View(doorItem);
        }

        // POST: DoorItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoorItemID,QuoteID,Description,ColorTypeID,LockTypeID,Height,Width1,Width2,Width3,SliderTopTrack,SliderBottomTrack,SliderSideChannel,SliderFs,SliderZs,SliderBugStrip,Cost,DoorTypeID,DesignTypeID,LockHeight,SliderTopTrackWidth,SliderBottomTrackWidth,SliderSideChannelWidth,TrackTypes,F,Z,SliderTopTrackType,SliderBottomTrackType,SliderSideChannelType,HingeType,BugStrip,Required")] DoorItem doorItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doorItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorTypeID = new SelectList(db.ColorTypes, "ColorTypeID", "ColorTypeName", doorItem.ColorTypeID);
            ViewBag.DesignTypeID = new SelectList(db.DesignTypes, "DesignTypeID", "DesignTypeName", doorItem.DesignTypeID);
            ViewBag.DoorTypeID = new SelectList(db.DoorTypes, "DoorTypeID", "DoorTypeName", doorItem.DoorTypeID);
            ViewBag.LockTypeID = new SelectList(db.LockTypes, "LockTypeID", "LockTypeName", doorItem.LockTypeID);
            ViewBag.TrackTypeID = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.TrackTypes);

            ViewBag.HingeType = new SelectList(db.HingeTypes, "HingeTypeID", "HingeTypeName", doorItem.HingeType);
            ViewBag.SliderTopTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderTopTrackType);
            ViewBag.SliderBottomTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderBottomTrackType);
            ViewBag.SliderSideChannelTrackType = new SelectList(db.TrackTypes, "TrackTypeID", "TrackTypeName", doorItem.SliderSideChannelType);

            ViewBag.QuoteID = new SelectList(db.Quotes, "QuoteID", "Customer", doorItem.QuoteID);
            return View(doorItem);
        }


        // GET: DoorItems/Delete/5
        public ActionResult DeleteDoorItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }
            return View(doorItem);
        }

        // POST: DoorItems/Delete/5
        public ActionResult DeleteDoorItemConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DoorItem doorItem = db.DoorItems.Find(id);
            var quoteid = doorItem.QuoteID;
            db.DoorItems.Remove(doorItem);
            db.SaveChanges();

            return RedirectToAction("QuoteDetails/" + quoteid, "Quotes");
        }




        // GET: DoorItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorItem doorItem = db.DoorItems.Find(id);
            if (doorItem == null)
            {
                return HttpNotFound();
            }
            return View(doorItem);
        }

        // POST: DoorItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoorItem doorItem = db.DoorItems.Find(id);
            db.DoorItems.Remove(doorItem);
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
