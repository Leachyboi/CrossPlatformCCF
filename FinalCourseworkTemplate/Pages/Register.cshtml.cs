using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using FinalCourseworkTemplate.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalCourseworkTemplate.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;
        public IList<RegisterView> RegisterViews { get; set; }
        public IList<Cadet> Cadets { get; set; }
        public IList<Register> Registers { get; set; }

        public RegisterModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            RegisterViews = new List<RegisterView>();
            Cadets = _context.Cadets.Include(c => c.Registers) .ToList();
            Registers = _context.Registers.ToList();

            foreach (var cadet in Cadets)
            {
                //if no registers for a cadet, display their name
                if (0 == cadet.Registers.Count)
                {
                    RegisterViews.Add(
                        new RegisterView
                        {
                            FullName = cadet.Surname + cadet.KnownAs,
                        } 
                    );
                }
                //move data into the viewmodel
                else
                {
                    //first reg is to only show name first time
                    var firstRegistration = true;
                    foreach (var register in cadet.Registers)
                    {
                        RegisterViews.Add(
                            new RegisterView
                            {
                                FullName = firstRegistration ? cadet.Surname + ", " + cadet.KnownAs : "",
                                Attendance = register.Register.Attended.ToString(),// ? "Yes" : "No",
                                RegDate = register.Register.DateOfReg.Date,
                            }
                        );
                        firstRegistration = false;
                    }
                }
            }
        }
    }
}