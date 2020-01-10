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

        public QualificationModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Qualifications = _context.Qualifications.ToList();
        }
    }
}