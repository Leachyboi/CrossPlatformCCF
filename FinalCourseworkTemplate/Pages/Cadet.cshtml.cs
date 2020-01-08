using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate
{
    public class CadetModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        public IList<Cadet> Cadets { get; set; }

        public CadetModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Cadets = _context.Cadets.ToList();
        }
    }
}