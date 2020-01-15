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

        [BindProperty]
        public string nameFilter { get; set; }
        [BindProperty]
        public string attendFilter { get; set; }
        [BindProperty]
        public DateTime dateFilter { get; set; }

        public DateTime compDate = new DateTime(0001, 01, 01, 0, 0, 0);
        public bool attFiltered;
        public bool dayFiltered;
        public bool filterVal;

        public RegisterModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            RegisterViews = new List<RegisterView>();
            Cadets = _context.Cadets.Include(c => c.Registers).OrderBy(s => s.Surname).ToList();
            Registers = _context.Registers.ToList();

            foreach (var cadet in Cadets)
            {
                //if no registers for a cadet, display their name
                if (0 == cadet.Registers.Count)
                {
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

        public async Task<IActionResult> OnPostAsync()
        {
            RegisterViews = new List<RegisterView>();
            Cadets = _context.Cadets.Include(c => c.Registers).OrderBy(s => s.Surname).ToList();
            Registers = _context.Registers.ToList();

            attFiltered = false;
            dayFiltered = false;

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                Cadets = Cadets.Where(
                    s => 0 == string.Compare((s.KnownAs + " " + s.Surname), nameFilter, 
                    StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Surname).ToList();
            }

            if (!string.IsNullOrWhiteSpace(attendFilter))
            {
                if(0 == string.Compare("Yes", attendFilter,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    attFiltered = true;
                    filterVal = true;
                }
                if (0 == string.Compare("No", attendFilter,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    attFiltered = true;
                    filterVal = false;
                }
            }

            if(0 != DateTime.Compare(dateFilter, compDate))
            {
                dayFiltered = true;
            }

            foreach (var cadet in Cadets)
            {
                //if no registers for a cadet, display their name
                if (0 == cadet.Registers.Count)
                {
                }
                //move data into the viewmodel
                else
                {
                    //first reg is to only show name first time
                    var firstRegistration = true;
                    if (dayFiltered == true && attFiltered == true)
                    {
                        foreach (var register in cadet.Registers.Where(s => s.Register.DateOfReg == dateFilter 
                        && s.Register.Attended == filterVal))
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
                    else if(dayFiltered == true && attFiltered == false)
                    {
                        foreach (var register in cadet.Registers.Where(s => s.Register.DateOfReg == dateFilter))
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
                    else if(dayFiltered == false && attFiltered == true)
                    {
                        foreach (var register in cadet.Registers.Where(s => s.Register.Attended == filterVal))
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
                    else
                    {
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
            return Page();
        }
    }
}
