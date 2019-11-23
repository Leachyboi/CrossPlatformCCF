using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCourseworkTemplate.Models
{
    public class CadetRegister
    {
        public int CadetRegisterId { get; set; }
        public int CadetId { get; set; }
        public int RegisterId { get; set; }
        public Cadet Cadet { get; set; }
        public Register Register { get; set; }
    }
}
