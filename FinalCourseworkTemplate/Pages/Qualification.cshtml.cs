using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate
{
    public class QualificationModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        public IList<Qualification> Qualifications { get; set; }

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

        public QualificationModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Qualifications = _context.Qualifications.OrderBy(s => s.Name).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
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
            return Page();// RedirectToPage("./Qualification");
        }
    }
}