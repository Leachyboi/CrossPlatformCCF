using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate
{
    public class CadetEditModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        public IList<Cadet> Cadets { get; set; }

        public CadetEditModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string forNameFilter { get; set; }
        [BindProperty]
        public string surNameFilter { get; set; }
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
        }

        public async Task<IActionResult> OnPostFilterAsync()
        {
            Cadets = _context.Cadets.ToList();
            if (!string.IsNullOrWhiteSpace(forNameFilter))
            {
                Cadets = Cadets.Where(s =>
                0 == string.Compare(s.KnownAs, forNameFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(surNameFilter))
            {
                Cadets = Cadets.Where(s =>
                0 == string.Compare(s.Surname, surNameFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Surname).ToList();
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
                Cadets = Cadets.Where(s =>
                0 == string.Compare(s.Form, formFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(genderFilter))
            {
                Cadets = Cadets.Where(s =>
                0 == string.Compare(s.Gender, genderFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(rankFilter))
            {
                Cadets = Cadets.Where(s =>
                0 == string.Compare(s.Rank, rankFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Surname).ToList();
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

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            foreach (var cadEntry in Cadets)
            {
                bool emptyVal = false;

                if (string.IsNullOrEmpty(cadEntry.KnownAs) || string.IsNullOrEmpty(cadEntry.Surname) ||
                    string.IsNullOrEmpty(cadEntry.Form) || string.IsNullOrEmpty(cadEntry.Gender) ||
                    string.IsNullOrEmpty(cadEntry.Rank))
                {
                    emptyVal = true;
                }

                if (emptyVal == false)
                {
                    var cadet = _context.Cadets
                        .Where(r => r.Surname == cadEntry.Surname
                        && r.KnownAs == cadEntry.KnownAs && r.Gender == cadEntry.Gender && r.Year == cadEntry.Year).ToList();

                    if (cadet.Count > 0)
                    {
                        
                    }
                }
            }
            return Page();
        }
    }
}