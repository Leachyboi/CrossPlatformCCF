﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public IList<CadetQualification> CadetQualifications { get; set; }

        public async Task OnGetAsync()
        {
            var cadetqualifications = _context.CadetQualifications.ToList();
            var qualification = _context.Qualifications.ToList();
            Cadets = _context.Cadets.Where(s => s.Qualifications.Count > 0).ToList();
        }
    }
}