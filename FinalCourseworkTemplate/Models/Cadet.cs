using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCourseworkTemplate.Models
{
    public class Cadet
    {
        public int CadetId { get; set; }
        public string Surname { get; set; }
        public string KnownAs { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
        public string Rank { get; set; }
        public string Form { get; set; }
        public int Platoon { get; set; }
        public int Section { get; set; }
        public List<CadetQualification> Qualifications { get; set; }
    }
}
