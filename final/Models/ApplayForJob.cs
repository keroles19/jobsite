using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class ApplayForJob
    {


        public int id { get; set; }
        [Required]
        [Display(Name = "upload your cv")]
        public string Cv { get; set; }
        public DateTime ApplayDate { get; set; }

        public int jobid { get; set; }

        public virtual jobs job { get; set; }

        public string userid    { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}