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
    public class IndexModel : PageModel
    {
        private readonly FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext _context;

        public IndexModel(FinalCourseworkTemplate.Models.FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public IList<Qualification> Qualification { get;set; }

        public async Task OnGetAsync()
        {
            Qualification = await _context.Qualification.ToListAsync();
        }
    }
}
