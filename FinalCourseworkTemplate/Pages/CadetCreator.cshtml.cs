using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using FinalCourseworkTemplate.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate
{
    public class CadetCreatorModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        public IList<Cadet> Cadets { get; set; }

        [BindProperty]
        public IList<CadetView> CadetViews { get; set; }

        int counttest;

        [TempData]
        public string tempString { get; set; }

        public CadetCreatorModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            CadetViews = new List<CadetView>();
            for (var i = 0; i < 10; i++)
            {
                CadetViews.Add(
                    new CadetView
                    {
                        firstName = "",
                        lastName = "",
                        yearsOld = 13,
                        cadGender = "",
                        schoolYear = 9,
                        schoolForm = "",
                        cadRank = "",
                        cadPlatoon = 1,
                        cadSection =1,
                    }
                );
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var cadEntry in CadetViews)
            {

                bool emptyVal = false;

                if (string.IsNullOrEmpty(cadEntry.firstName) || string.IsNullOrEmpty(cadEntry.lastName) ||
                    string.IsNullOrEmpty(cadEntry.schoolForm) || string.IsNullOrEmpty(cadEntry.cadGender) ||
                    string.IsNullOrEmpty(cadEntry.cadRank))
                {
                    tempString = "Enter a value";
                    emptyVal = true;
                }

                if (emptyVal == false)
                {
                    var cadet = _context.Cadets
                        .Where(r => r.Surname == cadEntry.lastName 
                        && r.KnownAs == cadEntry.firstName && r.Gender == cadEntry.cadGender && r.Year == cadEntry.schoolYear).ToList();

                    if (cadet.Count > 0)
                    {
                        tempString = "Cadet already exists";
                    }
                    else
                    {
                        _context.Add(new Cadet
                        {
                            Surname = cadEntry.lastName,
                            KnownAs = cadEntry.firstName,
                            Age = cadEntry.yearsOld,
                            Year = cadEntry.schoolYear,
                            Form = cadEntry.schoolForm,
                            Gender = cadEntry.cadGender,
                            Rank = cadEntry.cadRank,
                            Platoon = cadEntry.cadPlatoon,
                            Section = cadEntry.cadSection
                        });
                        counttest++;
                        await _context.SaveChangesAsync();
                        tempString = counttest + "Entries Created";
                    }
                }
            }
            return RedirectToPage("./CadetCreator");
        }
    }
}