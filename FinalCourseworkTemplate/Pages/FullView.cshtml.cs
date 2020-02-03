using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalCourseworkTemplate
{
    public class FullViewModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;
        public List<Cadet> Cadets { get; set; }
        
        public FullViewModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public IList<Qualification> Qualifications { get; set; }
        public IList<Register> Registers { get; set; }
        public IList<CadetQualification> CadetQualifications { get; set; }
        public IList<CadetRegister> CadetRegisters { get; set; }

        public async Task OnGetAsync()
        {
            Cadets = _context.Cadets.Include(s => s.Qualifications).Include(s => s.Registers).OrderBy(s => s.Surname).ToList();
            Qualifications = _context.Qualifications.ToList();
            Registers = _context.Registers.ToList();
            CadetQualifications = _context.CadetQualifications.ToList();
            CadetRegisters = _context.CadetRegisters.ToList();
        }
    }
}