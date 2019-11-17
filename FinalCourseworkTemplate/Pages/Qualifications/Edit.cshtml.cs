using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalCourseworkTemplate.Models;

namespace FinalCourseworkTemplate.Pages.QualificationPages
{
    public class EditModel : PageModel
    {
        private readonly FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext _context;

        public EditModel(FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Qualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(Qualification.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool QualificationExists(int id)
        {
            return _context.Qualification.Any(e => e.ID == id);
        }
    }
}
