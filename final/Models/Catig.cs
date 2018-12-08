using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final.Models
{
    public class Catig
    {
       
    
       
        public int id { get; set; }

        public string Name { get; set; }

        [AllowHtml]
        public string Details { get; set; }

        public virtual ICollection<jobs> jobs { get; set; }

       
    }
}