using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinalCourseworkTemplate.Models
{
    public class FinalCourseworkTemplateContext : DbContext
    {
        public FinalCourseworkTemplateContext (DbContextOptions<FinalCourseworkTemplateContext> options)
            : base(options)
        {
        }

        public DbSet<FinalCourseworkTemplate.Models.Qualification> Qualification { get; set; }
    }
}
