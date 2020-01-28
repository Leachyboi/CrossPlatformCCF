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
        public int ageFilter { get; set; } = -1;
        [BindProperty]
        public int yearFilter { get; set; } = -1;
        [BindProperty]
        public string formFilter { get; set; }
        [BindProperty]
        public string genderFilter { get; set; }
        [BindProperty]
        public string rankFilter { get; set; }
        [BindProperty]
        public int platoonFilter { get; set; } = -1;
        [BindProperty]
        public int sectionFilter { get; set; } = -1;

        public void OnGet()
        {
            Cadets = _context.Cadets.OrderBy(s => s.Platoon).ThenBy(s => s.Section).ThenBy(s => s.Surname).ToList();
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            Cadets = _context.Cadets.ToList();
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                Cadets = Cadets.Where(s => s.Surname.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(s => s.Surname).ToList();
            }
            if (ageFilter != -1)
            {
                Cadets = Cadets.Where(s => s.Age == ageFilter).OrderBy(s => s.Surname).ToList();
            }
            if (yearFilter != -1)
            {
                Cadets = Cadets.Where(s => s.Year == yearFilter).OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(formFilter))
            {
                Cadets = Cadets.Where(s => s.Form.Contains(formFilter, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(genderFilter))
            {
                Cadets = Cadets.Where(s => s.Gender.Contains(genderFilter, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(rankFilter))
            {
                Cadets = Cadets.Where(s => s.Rank.Contains(rankFilter, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(s => s.Surname).ToList();
            }
            if (platoonFilter != -1)
            {
                Cadets = Cadets.Where(s => s.Platoon == platoonFilter).OrderBy(s => s.Surname).ToList();
            }
            if (yearFilter != -1)
            {
                Cadets = Cadets.Where(s => s.Section == sectionFilter).OrderBy(s => s.Surname).ToList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int DeleId)
        {
            var removeItem = _context.Cadets.SingleOrDefault(s => s.CadetId == DeleId);
            if (removeItem != null)
            {
                _context.Cadets.Remove(removeItem);
                _context.SaveChanges();
            }
            Cadets = _context.Cadets.OrderBy(s => s.Surname).ToList();
            return Page();
        }
    }
}