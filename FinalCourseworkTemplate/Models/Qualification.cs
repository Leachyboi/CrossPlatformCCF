using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCourseworkTemplate.Models
{
    public class Qualification
    {
        public int ID { get; set; }
        public string QualName { get; set; }
        public int PassMark { get; set; }
        public string ParOrChi { get; set; }
        public string Parent { get; set; }
        public int NumChi { get; set; }
        public int MinChiPass { get; set; }
    }
}
