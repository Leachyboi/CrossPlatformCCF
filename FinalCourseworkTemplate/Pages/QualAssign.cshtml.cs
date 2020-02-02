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

        public bool resultPass = false;


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
                string inpName = qualEntry.cadetName;
                
                string[] splitName = inpName.Split(',');
                string realName = $"{splitName[1]} {splitName[0]}".Trim();

                var cadetQuer = _context.Cadets
                    .Where(s => s.Surname == splitName[0].Trim() 
                    && s.KnownAs == splitName[1].Trim())
                    .ToList();
                var qualQuer = _context.Qualifications.Where(s => s.Name == qualEntry.qualName).ToList();
                
                if(cadetQuer.Count > 0 && qualQuer.Count > 0)
                {
                    var cadQual = _context.CadetQualifications
                        .Where(c => c.CadetId == cadetQuer[0].CadetId && c.QualificationId == qualQuer[0].QualificationId)
                        .ToList();

                    //pass mark check
                    if(qualEntry.cadMark >= qualQuer[0].PassMark)
                    {
                        resultPass = true;
                    }

                    if (cadQual.Count > 0)
                    {
                        for (var i = 0; i < cadQual.Count; i++)
                        {
                            cadQual[i].CadetId = cadetQuer[0].CadetId;
                            cadQual[i].QualificationId = qualQuer[0].QualificationId;
                            cadQual[i].cadMark = qualEntry.cadMark;
                            cadQual[i].Passed = resultPass;
                            _context.Update(cadQual[i]);
                        }
                    }
                    if (cadQual.Count == 0)
                    {
                        _context.CadetQualifications.Add(
                            new CadetQualification 
                            { 
                                CadetId = cadetQuer[0].CadetId, 
                                QualificationId =  qualQuer[0].QualificationId,
                                cadMark = qualEntry.cadMark,
                                Passed = resultPass
                            });
                    }
                    await _context.SaveChangesAsync();
                }
            }

            return Page();
        }
    }
}