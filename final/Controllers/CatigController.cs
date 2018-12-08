using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using final.Models;

namespace final.Controllers
{
    [Authorize(Roles="Admin")]
    public class CatigController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

                 
        // GET: /Catig/
        public ActionResult Index()
        {
            return View(db.Catigs.ToList());
        }

        // GET: /Catig/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Catig catig = db.Catigs.Find(id);
        //    if (catig == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(catig);
        //}

        // GET: /Catig/Create
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,Name,Details")] Catig catig)
        {
            if (ModelState.IsValid)
            {
                db.Catigs.Add(catig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catig);
        }

        // GET: /Catig/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catig catig = db.Catigs.Find(id);
            if (catig == null)
            {
                return HttpNotFound();
            }
            return View(catig);
        }

        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit([Bind(Include="id,Name,Details")] Catig catig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catig);
        }

        // GET: /Catig/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catig catig = db.Catigs.Find(id);
            if (catig == null)
            {
                return HttpNotFound();
            }
            return View(catig);
        }

        // POST: /Catig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catig catig = db.Catigs.Find(id);
            db.Catigs.Remove(catig);
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
