using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalCourseworkTemplate.Models;

namespace FinalCourseworkTemplate.Pages.QualificationPages
{
    public class DeleteModel : PageModel
    {
        private readonly FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext _context;

        public DeleteModel(FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Qualification Qualification { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Qualification = await _context.Qualification.FirstOrDefaultAsync(m => m.ID == id);

            if (Qualification == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Qualification = await _context.Qualification.FindAsync(id);

            if (Qualification != null)
            {
                _context.Qualification.Remove(Qualification);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
