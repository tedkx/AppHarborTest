using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppHarborTest.Models
{
    public class Step
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Tip { get; set; }
        public int SectionID { get; set; }
        public int OrderNum { get; set; }
    }
}