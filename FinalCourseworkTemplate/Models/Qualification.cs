using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCourseworkTemplate.Models
{
    public class Qualification
    {
        public int QualificationId { get; set; }
        public string Name { get; set; }
        public int PassMark { get; set; }
        public string Parent { get; set; }
        public string ParOrChi { get; set; }
        public int NumOfChi { get; set; }
        public int MinChiPass { get; set; }
    }
}
