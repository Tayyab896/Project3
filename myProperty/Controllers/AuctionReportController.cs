using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myProperty.Models;

namespace myProperty.Controllers
{
    public class AuctionReportController : Controller
    {
        private myPropertyDBContext db = new myPropertyDBContext();

        // GET: AuctionReports
        public ActionResult Index()
        {
            // Include related Auction details
            var auctionReports = db.AuctionReports.Include(a => a.Auction);
            return View(auctionReports.ToList());
        }

        // GET: AuctionReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Include related Auction details
            AuctionReport auctionReport = db.AuctionReports.Include(a => a.Auction).FirstOrDefault(a => a.ReportID == id);
            if (auctionReport == null)
            {
                return HttpNotFound();
            }
            return View(auctionReport);
        }

        // GET: AuctionReports/Create
        public ActionResult Create()
        {
            // Populate dropdown for Auctions
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID");
            return View();
        }

        // POST: AuctionReports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,AuctionID,WinningBid,FinalSalePrice")] AuctionReport auctionReport)
        {
            if (ModelState.IsValid)
            {
                db.AuctionReports.Add(auctionReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate dropdown in case of validation errors
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID", auctionReport.AuctionID);
            return View(auctionReport);
        }

        // GET: AuctionReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuctionReport auctionReport = db.AuctionReports.Find(id);
            if (auctionReport == null)
            {
                return HttpNotFound();
            }
            // Populate dropdown for Auctions
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID", auctionReport.AuctionID);
            return View(auctionReport);
        }

        // POST: AuctionReports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,AuctionID,WinningBid,FinalSalePrice")] AuctionReport auctionReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auctionReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Repopulate dropdown in case of validation errors
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID", auctionReport.AuctionID);
            return View(auctionReport);
        }

        // GET: AuctionReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Include related Auction details
            AuctionReport auctionReport = db.AuctionReports.Include(a => a.Auction).FirstOrDefault(a => a.ReportID == id);
            if (auctionReport == null)
            {
                return HttpNotFound();
            }
            return View(auctionReport);
        }

        // POST: AuctionReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuctionReport auctionReport = db.AuctionReports.Find(id);
            db.AuctionReports.Remove(auctionReport);
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
