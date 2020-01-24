using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate
{
    public class QualificationEditModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        [BindProperty]
        public IList<Qualification> Qualifications { get; set; }

        public QualificationEditModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string nameFilter { get; set; }
        [BindProperty]
        public int passMarkFilter { get; set; } = -1;
        [BindProperty]
        public string parChiFilter { get; set; }
        [BindProperty]
        public string parentFilter { get; set; }
        [BindProperty]
        public int numChiFilter { get; set; } = -1;
        [BindProperty]
        public int minChiFilter { get; set; } = -1;

        public void OnGet()
        {
            Qualifications = _context.Qualifications.OrderBy(s => s.Name).ToList();
        }

        public async Task<IActionResult> OnPostFilterAsync()
        {
            Qualifications = _context.Qualifications.ToList();
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                Qualifications = Qualifications.Where(s =>
                0 == string.Compare(s.Name, nameFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Name).ToList();
            }
            if (passMarkFilter != -1)
            {
                Qualifications = Qualifications.Where(s => s.PassMark == passMarkFilter).OrderBy(s => s.Name).ToList();
            }
            if (!string.IsNullOrWhiteSpace(parChiFilter))
            {
                Qualifications = Qualifications.Where(s =>
                0 == string.Compare(s.ParOrChi, parChiFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Name).ToList();
            }
            if (!string.IsNullOrWhiteSpace(parentFilter))
            {
                Qualifications = Qualifications.Where(s =>
                0 == string.Compare(s.Parent, parentFilter,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Name).ToList();
            }
            if (numChiFilter != -1)
            {
                Qualifications = Qualifications.Where(s => s.NumOfChi == numChiFilter).OrderBy(s => s.Name).ToList();
            }
            if (minChiFilter != -1)
            {
                Qualifications = Qualifications.Where(s => s.MinChiPass == minChiFilter).OrderBy(s => s.Name).ToList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            foreach (var qualEntry in Qualifications)
            {
                bool emptyVal = false;

                if (string.IsNullOrEmpty(qualEntry.Name))
                {
                    emptyVal = true;
                }

                if (emptyVal == false)
                {
                    Qualifications = _context.Qualifications
                        .Where(r => r.QualificationId == qualEntry.QualificationId)
                        .OrderBy(s => s.Name).ToList();

                    //var cadet = _context.Cadets.Attach(new Cadet { CadetId = cadEntry.CadetId });

                    if (Qualifications.Count > 0)
                    {
                        for (var i = 0; i < Qualifications.Count; i++)
                        {
                            Qualifications[i].Name = qualEntry.Name;
                            Qualifications[i].PassMark = qualEntry.PassMark;
                            Qualifications[i].ParOrChi = qualEntry.ParOrChi;
                            Qualifications[i].Parent = qualEntry.Parent;
                            Qualifications[i].NumOfChi = qualEntry.NumOfChi;
                            Qualifications[i].MinChiPass = qualEntry.MinChiPass;
                            _context.Update(Qualifications[i]);
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return Page();
        }
    }
}