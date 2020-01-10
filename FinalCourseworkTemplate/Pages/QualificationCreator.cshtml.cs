using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using FinalCourseworkTemplate.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate
{
    public class QualificationCreatorModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;

        public IList<Qualification> Qualifications { get; set; }

        [BindProperty]
        public IList<QualificationView> QualificationViews { get; set; }

        int counttest;

        [TempData]
        public string tempString { get; set; }

        public QualificationCreatorModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            QualificationViews = new List<QualificationView>();
            for (var i = 0; i < 10; i++)
            {
                QualificationViews.Add(
                    new QualificationView
                    {
                        qualName="",
                        passMark = 0,
                        chiPar = "",
                        parent = "",
                        chiNum = 0,
                        passNum = 0,
                    }
                );
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var qualEntry in QualificationViews)
            {
                bool emptyVal = false;

                if (string.IsNullOrEmpty(qualEntry.qualName))
                { 
                    emptyVal = true;
                }

                if (emptyVal == false)
                {
                    var qual = _context.Qualifications.Where(r => r.Name == qualEntry.qualName).ToList();

                    if (qual.Count > 0)
                    {
                        tempString = "Cadet already exists";
                    }

                    else
                    {
                        _context.Add(new Qualification
                        {
                            Name = qualEntry.qualName,
                            PassMark = qualEntry.passMark,
                            ParOrChi = qualEntry.chiPar,
                            Parent = qualEntry.parent,
                            NumOfChi = qualEntry.chiNum,
                            MinChiPass = qualEntry.passNum
                        });
                        counttest++;
                        await _context.SaveChangesAsync();
                        tempString = counttest + "Entries Created";
                    }
                }
            }
            return RedirectToPage("./QualificationCreator");
        }
    }
}