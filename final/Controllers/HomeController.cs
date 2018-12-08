using final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net.Mail;

namespace final.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext dp = new ApplicationDbContext();

        public ActionResult Index()
        {
           
            return View(dp.Catigs.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
          
            return View();
        }

          [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mail = new MailMessage();
                    var logininfo = new NetworkCredential("kerolesatef200@gmail.com", "passsword");
                    mail.From = new MailAddress(model.Email);
                    mail.To.Add(new MailAddress("kerolesatef200@gmail.com"));
                    mail.Subject = model.Subject;
                    mail.IsBodyHtml = true;
                    string body =
                        "name of sender :" + model.Name + "<br>" +
                        "Email  :" + model.Email + "<br>" +
                        "subject of Message :" + model.Subject + "<br>" +
                         "Message is  :" + model.Message + "<br>";
                    mail.Body = body;
                    var smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.Credentials = logininfo;
                    smtp.Send(mail);
                    return RedirectToAction("Index");
                }
                catch (Exception err)
                {
                    ViewBag.Error = err.Message.ToString();
                    return View("Error");
                }
            }
            return View(model);
        }
      

         
      
       
       
        public ActionResult Details(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = dp.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            Session["jobsid"] =id;
            return View(jobs);

        }

         [Authorize(Roles = "employee")]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "employee")]
         public ActionResult Apply(HttpPostedFileBase cvupload)
        {
            {
            if (ModelState.IsValid)
            {
                

                var userid = User.Identity.GetUserId();
                var jobid = (int)Session["jobsid"];
                var time = DateTime.Now;
                var check = dp.ApplayForJobs.Where(a => a.userid == userid && a.jobid == jobid).ToList();
                if (check.Count() < 1)
                {
                    string pathcv = Path.Combine(Server.MapPath("~/uploaded/Cvs"), cvupload.FileName);
                    cvupload.SaveAs(pathcv);
                    var ApplyJob = new ApplayForJob() { userid = userid, jobid = jobid, Cv = cvupload.FileName, ApplayDate = time };
                    dp.ApplayForJobs.Add(ApplyJob);
                    dp.SaveChanges();
                    Session["T"]= "successful applied";
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewBag.Mes = "you have alredy applied for this job in advance";
                    return View();
                }
                
            }
            
            return View(cvupload);

                }
        }






        //     creave profile for users 

        // Get
        public ActionResult CreateProfile()
        {

            return View();
        }

        //[Authorize(Roles="employee")]
        //[HttpPost]
        //public ActionResult CreateProfile(CvViewModels model, HttpPostedFileBase upload )
        //{
        //    if (ModelState.IsValid)
        //    {    // file
        //        string path = Path.Combine(Server.MapPath("~/uploaded/Cvs"),upload.FileName);
        //        model.Cv = upload.FileName;
        //        upload.SaveAs(path);
        //        model.Id = User.Identity.GetUserId();
        //        dp.CvViewModels.Add(model);
        //        dp.SaveChanges();
        //        return RedirectToAction("Index");

        //    }
        //    return View(model);
        //}

        // show profile 

        [Authorize(Roles="employee")]
        public ActionResult ShowJObsApllayIt()
        {

            var CurUser = User.Identity.GetUserId();

            var j = dp.ApplayForJobs.Where(m => m.user.Id == CurUser).ToList();
            if (j.Count() < 1)
            {
                ViewBag.ms = "you didn't applied for any job yet";
                return View();
            }

            else
            {
                return View(j);
            }
        }















        [HttpGet]
        public ActionResult Search(string word)
        {

            var job = dp.jobs.Where(m => m.name.Contains(word)
                || m.catig.Name.Contains(word)
                || m.details.Contains(word)
                || m.catig.Details.Contains(word)).ToList();
            if (job.Count() < 1)
            {
                ViewBag.report = "no job like "+ word ;
                return View();
            }
            else
            {
                return View(job);
            }
        }




       // [Authorize(Roles="employee")]

        //public ActionResult Jobs()
        //{
            
        //    var userid = User.Identity.GetUserId();
        //    var check = dp.ApplayForJobs.Where(m => m.userid == userid).ToList();

        //    return View(check);     
 
        //}


        //public ActionResult Detailsjob(int? id)
        //{
        //    if (id == null)
        //    {

        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var job = dp.jobs.Find(id);

        //    if (job == null)
        //    {
        //        return HttpNotFound();
        //    }
            
        //    return View(job);
        //}

        //public ActionResult Delete(int? id)
        //{

        //    if (id == null)
        //    {

        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var d = dp.ApplayForJobs.Find(id);
        //    if(d==null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(d);
        //}

        //[HttpPost]
        //public ActionResult Delete(int id, ApplayForJob apply)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var job = dp.ApplayForJobs.Find(id);
        //        dp.ApplayForJobs.Remove(job);
        //        dp.SaveChanges();
        //        var userid = User.Identity.GetUserId();
        //        var check = dp.ApplayForJobs.Where(m => m.userid == userid).ToList();
        //        return View("Jobs", check);
        //    }

        //    return View(apply);
        //}

       
      
    }
}