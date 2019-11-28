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
    public class RegisterEditModel : PageModel
    {
        private readonly FinalCourseworkTemplateContext _context;
        [BindProperty]
        public IList<RegisterView> RegisterViews { get; set; }
        public IList<Cadet> Cadets { get; set; }
        public IList<Register> Registers { get; set; }

        public RegisterEditModel(FinalCourseworkTemplateContext context)
        {
            _context = context;
        }

        //[BindProperty]
        //public string Attend { get; set; }

        [BindProperty]
        public DateTime Date { get; set; } = DateTime.Today;

        //variable that stores message to be shown on button input
        [TempData]
        public string answer { get; set; }

        [TempData]
        public string useDate { get; set; }

        public void OnGet()
        {
            RegisterViews = new List<RegisterView>();
            Cadets = _context.Cadets.Include(c => c.Registers).ToList();
            Registers = _context.Registers.ToList();

            foreach (var cadet in Cadets)
            {
                if (0 == cadet.Registers.Count)
                {
                    RegisterViews.Add(
                        new RegisterView
                        {
                            FullName = cadet.Surname + cadet.KnownAs,
                        }
                    );
                }
                else
                {
                    var firstRegistration = true;
                    foreach (var register in cadet.Registers)
                    {
                        RegisterViews.Add(
                            new RegisterView
                            {
                                FullName = firstRegistration ? cadet.Surname + ", " + cadet.KnownAs : "",
                                Attendance = register.Register.Attended.ToString(),// ? "Yes" : "No",
                                RegDate = register.Register.DateOfReg.Date.ToShortDateString(),
                            }
                        );
                        firstRegistration = false;
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //int countReg = _context.CadetRegisters.ToList().Count;
            //for (int i=0; i<countReg; i++)
            //{
            //    if(Attend.[i] == "true")
            //    {
            //        ("answer" + i) = "Yes";
            //    }
            //}
            //assigns value to variable depending on input
            //if (Attend == "true")
            //{
            //    answer = "Yes";
            //}
            //else
            //{
            //    answer = "No";
            //}
            foreach(var cadet in RegisterViews)
            {
                var s = $"{cadet.FullName}, {cadet.Attendance}, {cadet.RegDate}";
                if (cadet.Attendance == "true")
                {
                    Console.WriteLine("Yes");
                }
            }
            useDate = Date.ToShortDateString();
            return RedirectToPage("./RegisterEdit");
        }
    }
}