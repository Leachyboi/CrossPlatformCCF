using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalCourseworkTemplate.Pages
{
    public class RegisterEditModel : PageModel
    {
        [BindProperty]
        public bool Attend { get; set; }

        [TempData]
        public string answer { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Attend == true)
            {
                answer = "Yes";
            }
            else
            {
                answer = "No";
            }
            return RedirectToPage("./RegisterEdit");
        }
    }
}