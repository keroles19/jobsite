using final.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace final.Controllers
{
    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {

        ApplicationDbContext dp = new ApplicationDbContext();

        // GET: /Roles/
        public ActionResult Index()
        {
            return View(dp.Roles.Where(m=>!m.Name.Contains("Admin")).ToList());
        }

        //
        // GET: /Roles/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var role = dp.Roles.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(role);
        //}

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {

                dp.Roles.Add(role);
                dp.SaveChanges();                    
                return RedirectToAction("Index");
            }

                return View(role);
           
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string id)
        {

            if(id==null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = dp.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, IdentityRole role)
        {
            if (ModelState.IsValid)
            {

                dp.Entry(role).State = EntityState.Modified;
                dp.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
               
          
              
            
        }

        //
        // GET: /Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if(id==null)
            {
                return new  HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = dp.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(string id,IdentityRole role)
        {
            try
            {
                var r = dp.Roles.Find(id);
                dp.Roles.Remove(r);
                dp.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception Error)
            {
                return View(Error.Message.ToString(),"Error");

            }
          
        }
    }
}
