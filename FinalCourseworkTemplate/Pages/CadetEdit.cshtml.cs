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
    public class CadetEditModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        [BindProperty]
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
            Cadets = _context.Cadets.OrderBy(s => s.Surname).ToList();
        }

        public async Task<IActionResult> OnPostFilterAsync()
        {
            Cadets = _context.Cadets.ToList();
            if (!string.IsNullOrWhiteSpace(forNameFilter))
            {
                Cadets = Cadets
                    .Where(s => 0 == string
                    .Compare(s.KnownAs, forNameFilter, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(surNameFilter))
            {
                Cadets = Cadets
                    .Where(s => 0 == string
                    .Compare(s.Surname, surNameFilter, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(s => s.Surname).ToList();
            }
            if (ageFilter != -1)
            {
                Cadets = Cadets
                    .Where(s => s.Age == ageFilter)
                    .OrderBy(s => s.Surname).ToList();
            }
            if (yearFilter != -1)
            {
                Cadets = Cadets.Where(s => s.Year == yearFilter).OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(formFilter))
            {
                Cadets = Cadets
                    .Where(s => 0 == string
                    .Compare(s.Form, formFilter, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(genderFilter))
            {
                Cadets = Cadets
                    .Where(s => 0 == string
                    .Compare(s.Gender, genderFilter, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(s => s.Surname).ToList();
            }
            if (!string.IsNullOrWhiteSpace(rankFilter))
            {
                Cadets = Cadets
                    .Where(s => 0 == string
                    .Compare(s.Rank, rankFilter, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(s => s.Surname).ToList();
            }
            if (platoonFilter != -1)
            {
                Cadets = Cadets
                    .Where(s => s.Platoon == platoonFilter)
                    .OrderBy(s => s.Surname).ToList();
            }
            if (yearFilter != -1)
            {
                Cadets = Cadets
                    .Where(s => s.Section == sectionFilter)
                    .OrderBy(s => s.Surname).ToList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            foreach (var cadEntry in Cadets)
            {
                bool emptyVal = false;

                if (string.IsNullOrEmpty(cadEntry.KnownAs) 
                    || string.IsNullOrEmpty(cadEntry.Surname) 
                    || string.IsNullOrEmpty(cadEntry.Form) 
                    || string.IsNullOrEmpty(cadEntry.Gender) 
                    || string.IsNullOrEmpty(cadEntry.Rank))
                {
                    emptyVal = true;
                }

                if (emptyVal == false)
                {
                    Cadets = _context.Cadets
                        .Where(r => r.CadetId == cadEntry.CadetId)
                        .OrderBy(s => s.Surname).ToList();

                    //var cadet = _context.Cadets.Attach(new Cadet { CadetId = cadEntry.CadetId });

                    if (Cadets.Count > 0)
                    {
                        for (var i = 0; i < Cadets.Count; i++)
                        {
                            Cadets[i].KnownAs = cadEntry.KnownAs;
                            Cadets[i].Surname = cadEntry.Surname;
                            Cadets[i].Age = cadEntry.Age;
                            Cadets[i].Year = cadEntry.Year;
                            Cadets[i].Form = cadEntry.Form;
                            Cadets[i].Gender = cadEntry.Gender;
                            Cadets[i].Rank = cadEntry.Rank;
                            Cadets[i].Platoon = cadEntry.Platoon;
                            Cadets[i].Section = cadEntry.Section;
                            _context.Update(Cadets[i]);
                            _context.SaveChanges();   
                        }
                        //_context.Add(new Cadet
                        //{
                        //    Surname = cadEntry.lastName,
                        //    KnownAs = cadEntry.firstName,
                        //    Age = cadEntry.yearsOld,
                        //    Year = cadEntry.schoolYear,
                        //    Form = cadEntry.schoolForm,
                        //    Gender = cadEntry.cadGender,
                        //    Rank = cadEntry.cadRank,
                        //    Platoon = cadEntry.cadPlatoon,
                        //    Section = cadEntry.cadSection
                        //});
                        //counttest++;
                        //await _context.SaveChangesAsync();
                        //tempString = counttest + "Entries Created";
                    }
                }
            }
            return Page();
        }
    }
}