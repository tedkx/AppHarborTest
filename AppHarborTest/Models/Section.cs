using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppHarborTest.Models
{
    public class Section
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int OrderNum { get; set; }

        ICollection<Step> Steps { get; set; }
    }
}