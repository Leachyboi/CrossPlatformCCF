using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalCourseworkTemplate.Models;

namespace FinalCourseworkTemplate.Pages.QualificationPages
{
    public class CreateModel : PageModel
    {
        private readonly FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext _context;

        public CreateModel(FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Qualification Qualification { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Qualification.Add(Qualification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
