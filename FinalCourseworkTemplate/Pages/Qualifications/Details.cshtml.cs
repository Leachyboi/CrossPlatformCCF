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
    public class DetailsModel : PageModel
    {
        private readonly FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext _context;

        public DetailsModel(FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

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
    }
}
