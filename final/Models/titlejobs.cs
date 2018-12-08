using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class titlejobs
    {


        public string title { get; set; }

        public IEnumerable<ApplayForJob> items { get; set; }
    }
}