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

        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Cadet> Cadets { get; set; }
        public DbSet<CadetQualification> CadetQualifications { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<CadetRegister> CadetRegisters { get; set; }
    }
}
