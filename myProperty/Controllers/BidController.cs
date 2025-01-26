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
    public class BidController : Controller
    {
        private myPropertyDBContext db = new myPropertyDBContext();

        // GET: Bids
        public ActionResult Index()
        {
            // Include Auction and Buyer details
            var bids = db.Bid.Include(b => b.Auction).Include(b => b.User);
            return View(bids.ToList());
        }

        // GET: Bids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Include related Auction and Buyer details
            Bid bid = db.Bid.Include(b => b.Auction).Include(b => b.User).FirstOrDefault(b => b.BuyerID == id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // GET: Bids/Create
        public ActionResult Create()
        {
            // Populate dropdowns for Auctions and Buyers
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID");
            ViewBag.BuyerID = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: Bids/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BidID,AuctionID,BuyerID,BidAmount,BidDate")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                // Set BidDate to current date if not provided
                if (bid.BidDate == default(DateTime))
                {
                    bid.BidDate = DateTime.Now;
                }

                db.Bid.Add(bid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate dropdowns if validation fails
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID", bid.AuctionID);
            ViewBag.BuyerID = new SelectList(db.Users, "UserID", "FullName", bid.BuyerID);
            return View(bid);
        }

        // GET: Bids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bid.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }

            // Populate dropdowns for Auctions and Buyers
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID", bid.AuctionID);
            ViewBag.BuyerID = new SelectList(db.Users, "UserID", "FullName", bid.BuyerID);
            return View(bid);
        }

        // POST: Bids/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BidID,AuctionID,BuyerID,BidAmount,BidDate")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate dropdowns if validation fails
            ViewBag.AuctionID = new SelectList(db.Auction, "AuctionID", "AuctionID", bid.AuctionID);
            ViewBag.BuyerID = new SelectList(db.Users, "UserID", "FullName", bid.BuyerID);
            return View(bid);
        }

        // GET: Bids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Include related Auction and Buyer details
            Bid bid = db.Bid.Include(b => b.Auction).Include(b => b.User).FirstOrDefault(b => b.BuyerID == id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bid bid = db.Bid.Find(id);
            db.Bid.Remove(bid);
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

