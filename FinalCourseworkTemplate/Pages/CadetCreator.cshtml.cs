using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate
{
    public class CadetCreatorModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        public IList<Cadet> Cadets { get; set; }

        [BindProperty]
        public string lastName { get; set; }
        [BindProperty]
        public string firstName { get; set; }
        [BindProperty]
        public string schoolYear { get; set; }
        [BindProperty]
        public string schoolForm { get; set; }
        [BindProperty]
        public string gender { get; set; }
        [BindProperty]
        public string rank { get; set; }
        [BindProperty]
        public string platoon { get; set; }
        [BindProperty]
        public string section { get; set; }

        public CadetCreatorModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Cadets = _context.Cadets.ToList();
        }
    }
}