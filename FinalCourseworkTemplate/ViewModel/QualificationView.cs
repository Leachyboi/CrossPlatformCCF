using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCourseworkTemplate.ViewModel
{
    public class QualificationView
    {
        public string qualName { get; set; }
        public int passMark { get; set; }
        public string parent { get; set; }
        public string chiPar { get; set; }
        public int chiNum { get; set; }
        public int passNum { get; set; }
    }
}
