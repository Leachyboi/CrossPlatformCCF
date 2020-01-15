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

        [BindProperty]
        public string nameFilter { get; set; }
        [BindProperty]
        public int ageFilter { get; set; }
        [BindProperty]
        public int yearFilter { get; set; }
        [BindProperty]
        public string formFilter { get; set; }
        [BindProperty]
        public string genderFilter { get; set; }
        [BindProperty]
        public string rankFilter { get; set; }
        [BindProperty]
        public int platoonFilter { get; set; }
        [BindProperty]
        public int sectionFilter { get; set; }

        public void OnGet()
        {
            Cadets = _context.Cadets.OrderBy(s => s.Platoon).ThenBy(s => s.Section).ThenBy(s => s.Surname).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }
    }
}