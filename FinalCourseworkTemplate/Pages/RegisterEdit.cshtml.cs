using System;
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

        //variable that stores message to be shown on button input
        [TempData]
        public string returnedString { get; set; }

        [TempData]
        public string useDate { get; set; }

        public string Surname;
        public string KnownAs;

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
            DateTime inputDate = date;
            var cadets = _context.Cadets.ToList();
            var registers = _context.Registers.ToList();
            var cadetRegisters = _context.CadetRegisters.Where(r => r.Register.DateOfReg == inputDate).ToList();

            string firstName = cadetRegisters[0].Cadet.KnownAs;
            string lastName = cadetRegisters[0].Cadet.Surname;
            string attend = cadetRegisters[0].Register.Attended.ToString();
            string newDate = cadetRegisters[0].Register.DateOfReg.ToShortDateString();

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
            returnedString = $"{firstName} {lastName}'s Attendance on {newDate} is {attend}.";
            useDate = date.ToShortDateString();
            return RedirectToPage("./RegisterEdit");
        }
        
        public string testFunc()
        {
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
            return "";
        }
    }
}