using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using final.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace final.Controllers
{
    [Authorize(Roles="employer")]
    public class jobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /jobs/

        public ActionResult Index()
        {
            var U = User.Identity.GetUserId();
            var jobs = db.jobs.Where(m=>m.userid==U).Include(j => j.catig);

            if (jobs.Count() < 1)
            {
                ViewBag.ms = "No Exist any job yet";
                return View();
            }
            else
            {
                return View(jobs.ToList());
            }
        }

        // GET: /jobs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    jobs jobs = db.jobs.Find(id);
        //    if (jobs == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(jobs);
        //}

        // GET: /jobs/Create
        public ActionResult Create()
        {
            ViewBag.catigid = new SelectList(db.Catigs, "id", "Name");
            return View();
        }

        [Authorize(Roles = "employer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(jobs jobs, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                ViewBag.catigid = new SelectList(db.Catigs, "id", "Name", jobs.catigid);
                string path = Path.Combine(Server.MapPath("~/uploaded"),upload.FileName);
                upload.SaveAs(path);
                jobs.image = upload.FileName;
                jobs.userid = User.Identity.GetUserId();
                db.jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index");
              
            }

            
            return View(jobs);
        }

        // GET: /jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            jobs jobs = db.jobs.Find(id);
            
            if (jobs == null)
            {
                return HttpNotFound();
            }
            ViewBag.catigid = new SelectList(db.Catigs, "id", "Name");
            return View(jobs);
        }

        // POST: /jobs/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(jobs jobs, HttpPostedFileBase upload)
        {
            var curUser = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string oldpath = Path.Combine(Server.MapPath("~/uploaded"), jobs.image);
                    System.IO.File.Delete(oldpath);
                    string NewPath = Path.Combine(Server.MapPath("~/uploaded"), upload.FileName);
                    upload.SaveAs(NewPath);
                    jobs.image = upload.FileName;
                   
                }
                    jobs.userid = curUser;
                    db.Entry(jobs).State = EntityState.Modified;
                    db.SaveChanges();
               
               
                    return RedirectToAction("Index"); 
            }
            ViewBag.catigid = new SelectList(db.Catigs, "id", "Name", jobs.catigid);
            return View(jobs);
        }

        // GET: /jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: /jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jobs jobs = db.jobs.Find(id);
            db.jobs.Remove(jobs);
            db.SaveChanges();
            string patheimage = Path.Combine(Server.MapPath("~/uploaded"), jobs.image);
           if (System.IO.File.Exists(patheimage))
            {
               System.IO.File.Delete(patheimage);
            }   
            return RedirectToAction("Index");
        }

        //[Authorize(Roles="employer")]
       
        //public ActionResult uersapplied()
        //{
        //    var userid = User.Identity.GetUserId();

        //    var req = from app in db.ApplayForJobs
        //              join a in db.jobs
        //              on app.jobid equals a.id 
        //              where a.user.Id == userid
        //              select app;
        //    if (req.Count() < 1)
        //    {
        //        ViewBag.ms = "No One Aplly for your Job";
        //        return View();

        //    }
        //    else
        //    return View(req.ToList());
        //}



        [Authorize(Roles = "employer")]

        public ActionResult uersapplied()
        {

            var curentuser = User.Identity.GetUserId();
            var query = from j in db.ApplayForJobs join p in db.jobs on j.jobid equals p.id where (p.userid == curentuser) select j;

            var groubed = from q in query
                          group q by q.job.name
                              into gr
                              select new titlejobs
                              {

                                  title = gr.Key,
                                  items = gr
                              };

                if(groubed.Count() < 1)
                {
                    ViewBag.ms = "No One Apply For Your Jobs ";
                    return View();
                }


            return View(groubed.ToList());
        }

        // download cv
        [Authorize(Roles="employer")]
        public ActionResult DownloadFile(string id)
        {

            var cv = db.ApplayForJobs.Where(m=>m.userid==id).SingleOrDefault();
            if (cv.Cv == null)
            { 
                return RedirectToAction("uersapplied");
            }
            else
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/uploaded/Cvs/";
                byte[] fileBytes = System.IO.File.ReadAllBytes(path + cv.Cv.ToString());
                string fileName = cv.Cv;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
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
