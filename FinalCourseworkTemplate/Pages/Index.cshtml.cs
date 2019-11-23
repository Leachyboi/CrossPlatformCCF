using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalCourseworkTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FinalCourseworkTemplate.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FinalCourseworkTemplateContext _context;
        public List<Cadet> Cadets { get; set; }
        public IndexModel(ILogger<IndexModel> logger, FinalCourseworkTemplateContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<CadetQualification> CadetQualifications { get; set; }

        public async Task OnGetAsync()
        {
            Cadets = _context.Cadets.ToList();
            var qualification = _context.Qualifications.ToList();
            var cadetqualifications = _context.CadetQualifications.ToList();
        }


        public string ConcatenameQualifications(Cadet cadet)
        {
            var sb = new StringBuilder();
            cadet.Qualifications.ToList().ForEach(qual => sb.Append(qual.Qualification.Name + " "));

            return sb.ToString();
        }
    }
}
