using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final.Models
{
    public class jobs
    {

        public int id { get; set; }

        public string name { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string AddressCompany { get; set; }
        [AllowHtml]
        public string details { get; set; }
       
        public string image { get; set; }
         [Display(Name = "groub of job")]
        public int catigid { get; set; }

        public Catig catig { get; set; }

        public  string  userid { get; set; }

        public virtual ApplicationUser user { get; set; }

    }
}