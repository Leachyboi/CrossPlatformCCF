using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCourseworkTemplate.Models
{
    public class CadetQualification
    {
        public int CadetQualificationId { get; set; }
        public int CadetId { get; set; }
        public int QualificationId { get; set; }
        public int cadMark { get; set; }
        public string Passed { get; set; }
        public Cadet Cadet { get; set; }
        public Qualification Qualification { get; set; }
    }
}
