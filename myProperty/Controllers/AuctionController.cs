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
    public class AuctionController : Controller
    {
        private myPropertyDBContext db = new myPropertyDBContext();

        // GET: Auction
        public ActionResult Index()
        {
            using (var db = new myPropertyDBContext())
            {
                var auction = db.Auction.Include(a => a.Property).ToList(); // Include related Property
                return View(auction);
            }
        }

        // GET: Auction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = db.Auction.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            return View(auction);
        }

        // GET: Auction/Create
        public ActionResult Create()
        {
            ViewBag.PropertyID = new SelectList(db.Property, "PropertyID", "Title");
            return View();
        }

        // POST: Auction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuctionID,PropertyID,StartDate,EndDate,MinBidIncrement,ReservePrice")] Auction auction)
        {
            if (ModelState.IsValid)
            {
                db.Auction.Add(auction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PropertyID = new SelectList(db.Property, "PropertyID", "Title", auction.PropertyID);
            return View(auction);
        }

        // GET: Auction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = db.Auction.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyID = new SelectList(db.Property, "PropertyID", "Title", auction.PropertyID);
            return View(auction);
        }

        // POST: Auction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuctionID,PropertyID,StartDate,EndDate,MinBidIncrement,ReservePrice")] Auction auction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyID = new SelectList(db.Property, "PropertyID", "Title", auction.PropertyID);
            return View(auction);
        }

        // GET: Auction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = db.Auction.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            return View(auction);
        }

        // POST: Auction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auction auction = db.Auction.Find(id);
            db.Auction.Remove(auction);
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
