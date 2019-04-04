using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using JustDoorsAndScreens;
using SelectPdf;

namespace JustDoorsAndScreens.Controllers
{
    public class QuotesController : Controller
    {
        private JustDoorsAndScreensEntities db = new JustDoorsAndScreensEntities();

        // GET: Quotes
        public ActionResult Index()
        {
            var quotes = db.Quotes.Include(q => q.StagesType);
            return View(quotes.ToList());
        }




        public class JobList
        {
            public int id { get; set; }
            public string Customer { get; set; }
            public string Address { get; set; }
            public string Date { get; set; }
            public string Telephone { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Deposit { get; set; }
            public string TotalCost { get; set; }
            public string Balance { get; set; }
            public string Stage { get; set; }
            public Boolean Paid { get; set; }
            public string OrderDate { get; set; }
            public string CompletedDate { get; set; }
        }




        public ActionResult GetList()
        {
            var myList = new List<JobList>();

            var query = db.vwQuoteReports.OrderByDescending(x => x.Date).ToList();

            foreach (var item in query)
            {
                JobList itm = new JobList();

                itm.id = (int)item.QuoteID;
                itm.Customer = item.Customer;
                itm.Address = item.Address;
                itm.Date = item.Date.ToString().Substring(0,10);
                itm.Telephone = item.Telephone;
                itm.Mobile = item.Mobile;
                itm.Email = item.Email;
                itm.Deposit = item.Deposit.ToString();
                itm.TotalCost = item.TotalCost.ToString();
                itm.Balance = (item.TotalCost - item.Deposit).ToString();
                itm.Stage = item.Stage;

                if (!string.IsNullOrEmpty(item.CompletedDate.ToString()))
                    itm.CompletedDate = item.CompletedDate.ToString().Substring(0, 10);
                else
                    itm.CompletedDate = item.CompletedDate.ToString();

                if (!string.IsNullOrEmpty(item.OrderDate.ToString()))
                    itm.OrderDate = item.OrderDate.ToString().Substring(0, 10);
                else
                    itm.OrderDate = item.OrderDate.ToString();

                if (item.Paid == null)
                {
                    itm.Paid = false;
                }
                else
                {
                    itm.Paid = (Boolean)item.Paid;
                }

                myList.Add(itm);
            }

            return Json(new { data = myList.ToArray() }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetDoors(int Quote )
        {
            var myList = new List<vwDoorItem>();

            var doorItems = db.vwDoorItems.Where(x =>x.QuoteID == Quote && x.DoorTypeName.ToUpper().Contains("DOOR"));

            foreach (var item in doorItems)
            {
                vwDoorItem itm = new vwDoorItem();

                itm.id = item.id;
                itm.QuoteID = item.QuoteID;
                itm.Description = item.Description;
                itm.ColorTypeName = item.ColorTypeName;
                itm.LockTypeName = item.LockTypeName;
                itm.LockHeight = item.LockHeight;
                itm.Height = item.Height;
                itm.Width1 = item.Width1;
                itm.Width2 = item.Width2;
                itm.Width3 = item.Width3;
                itm.SliderTopTrack = item.SliderTopTrack;
                itm.SliderBottomTrack = item.SliderBottomTrack;
                itm.SliderSideChannel = item.SliderSideChannel;
                itm.SliderFs = item.SliderFs;
                itm.SliderZs = item.SliderZs;
                itm.SliderBugStrip = item.SliderBugStrip;
                itm.Cost = item.Cost;
                itm.DoorTypeName = item.DoorTypeName;
                itm.DesignTypeName = item.DesignTypeName;

                itm.HingeTypeName = item.HingeTypeName;
                itm.SliderTopTrackTypeName = item.SliderTopTrackTypeName;
                itm.SliderBottomTrackTypeName = item.SliderBottomTrackTypeName;
                itm.SliderSideChannelTypeName = item.SliderSideChannelTypeName;

                myList.Add(itm);
            }

            return Json(new { data = myList.ToArray() }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSliders(int Quote)
        {
            var myList = new List<vwDoorItem>();

            var doorItems = db.vwDoorItems.Where(x => x.QuoteID == Quote && x.DoorTypeName.ToUpper().Contains("SLIDER"));

            foreach (var item in doorItems)
            {
                vwDoorItem itm = new vwDoorItem();

                itm.id = item.id;
                itm.QuoteID = item.QuoteID;
                itm.Description = item.Description;
                itm.ColorTypeName = item.ColorTypeName;
                itm.LockTypeName = item.LockTypeName;
                itm.LockHeight = item.LockHeight;
                itm.Height = item.Height;
                itm.Width1 = item.Width1;
                itm.Width2 = item.Width2;
                itm.Width3 = item.Width3;
                itm.SliderTopTrackTypeName = item.SliderBottomTrackTypeName;
                itm.SliderBottomTrackTypeName = item.SliderBottomTrackTypeName;
                itm.SliderSideChannelTypeName = item.SliderSideChannelTypeName;
                itm.F = item.F;
                itm.Z = item.Z;
                itm.SliderBugStrip = item.SliderBugStrip;
                itm.Cost = item.Cost;
                itm.DoorTypeName = item.DoorTypeName;
                itm.DesignTypeName = item.DesignTypeName;

                itm.HingeTypeName = item.HingeTypeName;
                itm.SliderTopTrackWidth = item.SliderTopTrackWidth;
                itm.SliderBottomTrackWidth = item.SliderBottomTrackWidth;
                itm.SliderSideChannelWidth = item.SliderSideChannelWidth;

                myList.Add(itm);
            }

            return Json(new { data = myList.ToArray() }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetFlyScreens(int Quote)
        {
            var myList = new List<vwFlyScreenItem>();

            var query = db.vwFlyScreenItems.Where(x => x.QuoteID == Quote).ToList();

            foreach (var item in query)
            {
                vwFlyScreenItem itm = new vwFlyScreenItem();

                itm.id = item.id;
                itm.QuoteID = item.QuoteID;
                itm.FlyScreenName = item.FlyScreenName;
                itm.ColorTypeName = item.ColorTypeName;
                itm.Width = item.Width;
                itm.Depth = item.Depth;
                itm.Qty = item.Qty;
                itm.Cost = item.Cost;

                myList.Add(itm);
            }

            return Json(new { data = myList.ToArray() }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCompletedQuotes()
        {
            var query = db.vwQuoteReports.Where(x => x.Stage == "Completed").Count();
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCancelledQuotes()
        {
            var query = db.vwQuoteReports.Where(x => x.Stage == "Cancelled").Count();
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuotedQuotes()
        {
            var query = db.vwQuoteReports.Where(x => x.Stage == "Quoted").Count();
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderedQuotes()
        {
            var query = db.vwQuoteReports.Where(x => x.Stage == "Ordered").Count();
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }




        // GET: Quotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: Quotes/Details/5
        public ActionResult QuoteDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwQuoteReport quote = db.vwQuoteReports.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }


        public ActionResult SendSMS(int QuoteID, string MobileNo)
        {
            //var client = new Client(creds: new Nexmo.Api.Request.Credentials
            //{
            //    ApiKey = "e4a62e38",
            //    ApiSecret = "pUtvKbdA7R096lUP"
            //});
            //var results = client.SMS.Send(request: new SMS.SMSRequest
            //{
            //    from = "JustDoorsAndScreens",
            //    //to = "61412437060",
            //    to = MobileNo,
            //    text = "Hello from JustDoorsAndScreens"
            //});

            return Json(new { data = "SMS Sent" }, JsonRequestBehavior.AllowGet);
        }

        //********************************************************************************************
        // 
        //********************************************************************************************
        public ActionResult SendEmailTo(int id)
        {
            SavePDF(id);

            var emailto = db.vwQuoteReports.Where(t => t.QuoteID == id).Select(t => t.Email).First();

            SendEmail(emailto, id);

            //var quote = db.vwQuoteReports.Where(t => t.QuoteID == id);
            
            //Redirect("QuoteDetails/" + id);

            return RedirectToAction("QuoteDetails/" + id);
        }



        public ActionResult PrintPDF(int id)
        {
            // read parameters from the webpage
            string url = Request.Url.Authority + Request.ApplicationPath  + "/Quotes/QuoteDetailsPDF/" + id;

            // instantiate a html to pdf converter object
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
            converter.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Portrait;
            //converter.Options.WebPageWidth = webPageWidth;
            //converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;

            //return RedirectToAction("QuoteDetails/" + id);
        }

        //********************************************************************************************
        // Save PDF
        //********************************************************************************************
        private void SavePDF(int id)
        {
            try
            { 
                var tempFilesFolder = Server.MapPath(ConfigurationManager.AppSettings["TempFilesRoot"]);
                var filename = id + "_Quote.pdf";
                var fileloc = tempFilesFolder + "\\";
                var fullPath = fileloc + filename;

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                string url = Request.Url.Authority + Request.ApplicationPath + "/Quotes/QuoteDetailsPDF/" + id;
                converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
                converter.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Portrait;

                SelectPdf.PdfDocument doc = converter.ConvertUrl(url);

                doc.Save(fullPath);
                doc.Close();

            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

        }


        //********************************************************************************************
        // Send Email
        //********************************************************************************************
        private void SendEmail(string ToEmail, int QuoteID)
        {
            try
            {
                var EmailFrom = ConfigurationManager.AppSettings["FromEmail"];
                var CustomerName = db.vwQuoteReports.Where(t => t.QuoteID == QuoteID).Select(t => t.Customer).First();
                string Status = db.vwQuoteReports.Where(t => t.QuoteID == QuoteID).Select(t => t.Stage).First();
                var FilesFolder = Server.MapPath(ConfigurationManager.AppSettings["TempFilesRoot"]);

                //string img = "~/images/JustDoorsAndScreenTitle.PNG";

                using (MailMessage mm = new MailMessage(EmailFrom, ToEmail))
                {
                    mm.Subject = " JustDoorsAndScreens : " + QuoteID;
                    string body = "Hello " + CustomerName + ",<br />";
                    body += "<br /> Please find attached the quote. ";
                    body += "<br />";
                    body += "<br /><br /><strong>Thank you</strong>";
                    body += "<br /><h3><strong>JustDoorsAndScreens </strong></h3>";
                    mm.Body = body.Replace('*','"');
                    mm.IsBodyHtml = true;

                    // Get All Attachments
                    try
                    {
                        string qpdf = FilesFolder + "\\" + QuoteID + "_Quote.pdf";
                        if (System.IO.File.Exists(qpdf))
                        {
                            var pdf = new Attachment(FilesFolder + "\\" + QuoteID + "_Quote.pdf");
                            mm.Attachments.Add(pdf);
                        }

                    }
                    catch (Exception ex)
                    {
                        //throw new Exception("Email Class unable to add Attachments.", ex);
                        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    // Send Mail
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(mm);
                        Console.Write("Mail Sent: Email sent Sucessfully: Function Mail() :");
                    }

                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        //********************************************************************************************
        // Display Quote PDF
        //********************************************************************************************
        public ActionResult QuoteDetailsPDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwQuoteReport quote = db.vwQuoteReports.Find(id);

            if (quote == null)
            {
                return HttpNotFound();
            }


            var doors = db.vwDoorItems.Where(t => t.QuoteID == id && t.DoorTypeName.ToUpper().Contains("DOOR")).ToList();
            ViewBag.Doors = doors;
            ViewBag.DoorsCnt = doors.Count();

            var sliders = db.vwDoorItems.Where(t => t.QuoteID == id && t.DoorTypeName.ToUpper().Contains("SLIDER")).ToList();
            ViewBag.Sliders = sliders;
            ViewBag.SlidersCnt = sliders.Count();

            var flyscreens = db.vwFlyScreenItems.Where(t => t.QuoteID == id).ToList();
            ViewBag.FlyScreen = flyscreens;
            ViewBag.FlyScreenCnt = flyscreens.Count();

            return View(quote);
        }


        // GET: Quotes/Create
        public ActionResult Create()
        {
            ViewBag.TodaysDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.StageID = new SelectList(db.StagesTypes, "StageID", "StageName");
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuoteID,Customer,Date,Telephone,Mobile,Email,Deposit,TotalCost,StageID,Address,OrderDate,CompletedDate")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Quotes.Add(quote);
                db.SaveChanges();

                return RedirectToAction("QuoteDetails/"+ quote.QuoteID);
            }

            ViewBag.StageID = new SelectList(db.StagesTypes, "StageID", "StageName", quote.StageID);
            return View(quote);
        }

        // GET: Quotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            ViewBag.StageID = new SelectList(db.StagesTypes, "StageID", "StageName", quote.StageID);
            return View(quote);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuoteID,Customer,Date,Telephone,Mobile,Email,Deposit,TotalCost,StageID,Paid,Address,OrderDate,CompletedDate")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StageID = new SelectList(db.StagesTypes, "StageID", "StageName", quote.StageID);
            return View(quote);
        }

        // GET: Quotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote quote = db.Quotes.Find(id);
            db.Quotes.Remove(quote);
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
