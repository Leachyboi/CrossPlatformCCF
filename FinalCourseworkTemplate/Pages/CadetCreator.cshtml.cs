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
        public int schoolYear { get; set; } = 9;
        [BindProperty]
        public string schoolForm { get; set; }
        [BindProperty]
        public string gender { get; set; }
        [BindProperty]
        public string rank { get; set; }
        [BindProperty]
        public int platoon { get; set; } = 1;
        [BindProperty]
        public int section { get; set; } = 1;
        
        [TempData]
        public string tempString { get; set; }

        public CadetCreatorModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Cadets = _context.Cadets.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //string fName = firstName;
            //string lName = lastName;
            //int sYear = schoolYear;
            //string sForm = schoolForm.ToString();
            //string gen = gender;
            //string iRank = rank;
            //int plat = platoon;
            //int sect = section;
            bool emptyVal = false;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(schoolForm) || string.IsNullOrEmpty(gender) ||
                string.IsNullOrEmpty(rank))
            {
                tempString = "Enter a value";
                emptyVal = true;
            }

            if(emptyVal == false)
            {
                var cadet = _context.Cadets
                    .Where(r => r.Surname == lastName && r.KnownAs == firstName && r.Gender == gender && r.Year == schoolYear).ToList();

                if (cadet.Count > 0)
                {
                    tempString = "Cadet already exists";
                }
                else
                {
                    _context.Add(new Cadet
                    {
                        Surname = lastName,
                        KnownAs = firstName,
                        Year = schoolYear,
                        Form = schoolForm,
                        Gender = gender,
                        Rank = rank,
                        Platoon = platoon,
                        Section = section
                    });
                    await _context.SaveChangesAsync();
                    tempString = $"Name: {firstName} {lastName}, " +
                        $"School Info: Year {schoolYear}, {schoolForm.ToString()}, Gender: {gender}, Rank: {rank}, Group: {platoon}, {section}";
                }
            }
            return RedirectToPage("./CadetCreator");
        }
    }
}