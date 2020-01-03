﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using FinalCourseworkTemplate.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
        public DateTime date { get; set; } = DateTime.Today.Date;

        [BindProperty]
        public string name { get; set; }

        [BindProperty]
        public bool Attend { get; set; }

        //variable that stores message to be shown on button input
        [TempData]
        public string returnedString { get; set; }

        [TempData]
        public string useDate { get; set; }

        DateTime day;


        public void OnGet()
        {
            RegisterViews = new List<RegisterView>();
            Cadets = _context.Cadets.Include(c => c.Registers).ToList();

            if(DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
            {
                day = DateTime.Today;
            }
            else
            {
                day = DateTime.Today.AddDays(-1);
                while (day.DayOfWeek != DayOfWeek.Tuesday)
                {
                    day = day.AddDays(-1);
                }
            }

            Registers = _context.Registers.Where(c => c.DateOfReg == day).ToList();
            
            if(Registers.Count == 0)
            {
                if(DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
                {
                    day = DateTime.Today;
                }
                else
                {
                    day = DateTime.Today.AddDays(-1);
                    while(day.DayOfWeek != DayOfWeek.Tuesday) 
                    {
                        day = day.AddDays(-1);
                    }
                }
                _context.Registers.Add(new Register { Attended = true, DateOfReg = day });
                _context.Registers.Add(new Register { Attended = false, DateOfReg = day });
                _context.SaveChanges();
            }

            Registers = _context.Registers.Where(c => c.DateOfReg == day).ToList();

            foreach (var cadet in Cadets)
            {
                if (0 == cadet.Registers.Count)
                {
                    RegisterViews.Add(
                        new RegisterView
                        {
                            FullName = cadet.Surname + ", " + cadet.KnownAs,
                            Attendance = false.ToString(),
                            RegDate = day.ToShortDateString(),
                        }
                    );
                }
                else
                {
                    var firstRegistration = true;
                    foreach (var register in cadet.Registers)
                    {
                        if (register.Register == null)
                        {
                            RegisterViews.Add(
                                new RegisterView
                                {
                                    FullName = firstRegistration ? cadet.Surname + ", " + cadet.KnownAs : "",
                                    Attendance = false.ToString(),
                                    RegDate = day.ToShortDateString(),
                                }
                            );
                        }
                        else
                        {
                            RegisterViews.Add(
                                new RegisterView
                                {
                                    FullName = firstRegistration ? cadet.Surname + ", " + cadet.KnownAs : "",
                                    Attendance = register.Register.Attended.ToString(),// ? "Yes" : "No",
                                    RegDate = register.Register.DateOfReg.Date.ToShortDateString(),
                                }
                            );
                        }
                        firstRegistration = false;
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DateTime inputDate = date;
            string inputName = name;
            bool attendance = Attend;
            var cadets = _context.Cadets.ToList();
            var registers = _context.Registers.ToList();
            var cadetRegisters = _context.CadetRegisters
                .Where(r => r.RegisterId == r.Register.RegisterId 
                && r.Register.DateOfReg == inputDate && r.Cadet.Surname == inputName).ToList();


            foreach (var regEntry in RegisterViews)
            {
                string inpName = regEntry.FullName;
                string[] splitName = inpName.Split(',');
                string realName = $"{splitName[1]} {splitName[0]}";
                var cadetQuer = _context.Cadets.Where(c => c.Surname == realName).ToList();
                var regQuer = _context.Registers
                    .Where(r => r.DateOfReg == day && r.Attended.ToString() == regEntry.Attendance).ToList();
            }
            returnedString = "Return";
            return RedirectToPage("./RegisterEdit");
            /*if (cadetRegisters.Count > 0)
            {
                string firstName = cadetRegisters[0].Cadet.KnownAs;
                string lastName = cadetRegisters[0].Cadet.Surname;
                string attend = cadetRegisters[0].Register.Attended.ToString();
                string newDate = cadetRegisters[0].Register.DateOfReg.ToShortDateString();
                returnedString = stringCreate(firstName, lastName, newDate, attend);
            }

            else
            {
                var cadetQuer = _context.Cadets.Where(c => c.Surname == inputName).ToList();
                var regQuer = _context.Registers.Where(r => r.DateOfReg == inputDate && r.Attended == true).ToList();
                if (regQuer.Count > 0 && cadetQuer.Count > 0)
                {
                    int cadID = cadetQuer[0].CadetId;
                    int regID = regQuer[0].RegisterId;
                    _context.CadetRegisters.Add(new CadetRegister { CadetId = cadID, RegisterId = regID });
                    string firstName = cadetQuer[0].KnownAs;
                    string lastName = cadetQuer[0].Surname;
                    string attend = regQuer[0].Attended.ToString();
                    string newDate = inputDate.ToShortDateString();
                    returnedString = stringCreate(firstName, lastName, newDate, attend);
                }
                if (cadetQuer.Count > 0)
                {
                    //run new date creation
                    _context.Registers.Add(new Register { Attended = true, DateOfReg = inputDate });
                    _context.Registers.Add(new Register { Attended = false, DateOfReg = inputDate });
                    returnedString = 
                        stringCreate(cadetQuer[0].KnownAs, cadetQuer[0].Surname, inputDate.ToShortDateString(), attendance.ToString());
                }
                else
                {
                    returnedString = "Cadet doesn't exist";
                }
            }

            useDate = date.ToShortDateString();
            return RedirectToPage("./RegisterEdit");*/


            //_context.Registers.Add(new Register {Attended = true, 
            //DateOfReg = new DateTime(),
            //});

            //await _context.SaveChangesAsync();

            /*assigns value to variable depending on input
            foreach (var cadet in RegisterViews)
            {
                var s = $"{cadet.FullName}, {cadet.Attendance}, {cadet.RegDate}";
                if (cadet.Attendance == "true")
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
            }
            string queryString =
            "SELECT CadetId, RegisterId FROM dbo.CadetRegisters;";

            string queryStringAttend =
            "SELECT Attended, DateOfReg FROM dbo.Register;";

            using (SqlConnection connection =
                       new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=CourseworkDatabase;Trusted_Connection=True;"))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    string regId = Convert.ToString(reader[0]);

                    SqlCommand commandAttended =
                    new SqlCommand(queryStringAttend, connection);
                    connection.Open();
                    SqlDataReader readerAttended = commandAttended.ExecuteReader();

                    returnedString = testFunc(regId, Convert.ToString(readerAttended[0]), Convert.ToDateTime(readerAttended[1]).Date);
                }

                // Call Close when done reading.
                reader.Close();
                connection.Close();
            }
            using (v
                var register = DbConnection.Database.SqlQuery("SELECT * FROM Registers").ToList;

            */
        }
        


        public string stringCreate(string fname, string sname, string shortdate, string attended)
        {
            
            string newString = $"{fname} {sname}'s Attendance on {shortdate} is {attended}.";
            return newString;
            
            /*string newQuery = $"SELECT CadetId, Surname, KnownAs FROM dbo.Cadets WHERE CadetId = {cadId};";

            using (SqlConnection connection
                = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=CourseworkDatabase;Trusted_Connection=True;"))
            {
                SqlCommand command =
                    new SqlCommand(newQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    Surname = Convert.ToString(reader[1]);
                    KnownAs = Convert.ToString(reader[2]);
                }

                // Call Close when done reading.
                reader.Close();
                connection.Close();
            }

            string sentence = $"{KnownAs} {Surname} Attendance on {dateReg} is {attendOrNot}.";
            return sentence;*/
            
        }
    }
}