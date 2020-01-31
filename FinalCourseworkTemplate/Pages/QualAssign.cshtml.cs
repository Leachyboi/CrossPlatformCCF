using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using FinalCourseworkTemplate.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalCourseworkTemplate
{
    public class QualAssignModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        [BindProperty]
        public IList<QualAssignView> QualAssignViews { get; set; }
        public IList<Cadet> Cadets { get; set; }
        public IList<Qualification> Qualifications { get; set; }

        [BindProperty]
        public string qualComp { get; set; }

        public QualAssignModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            QualAssignViews = new List<QualAssignView>();
            Cadets = _context.Cadets.Include(s => s.Qualifications).OrderBy(s => s.Surname).ToList();
            Qualifications = _context.Qualifications.OrderBy(s => s.Name).ToList();

            foreach(var cadet in Cadets)
            {
                QualAssignViews.Add(new QualAssignView
                    {
                        cadetName = cadet.Surname + ", " + cadet.KnownAs,
                        qualName = "",
                        cadMark = 0,
                        passFail = "",
                    }
                );
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Cadets = _context.Cadets.Include(s => s.Qualifications).OrderBy(s => s.Surname).ToList();
            Qualifications = _context.Qualifications.OrderBy(s => s.Name).ToList();

            foreach(var qualEntry in QualAssignViews)
            {
                string inpName
            }

            return Page();
        }
    }
}