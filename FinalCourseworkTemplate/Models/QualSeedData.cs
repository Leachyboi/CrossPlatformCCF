using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCourseworkTemplate.Models
{
    public class QualSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FinalCourseworkTemplateContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FinalCourseworkTemplateContext>>()))
            {
                // Look for any movies.
                if (context.Qualification.Any())
                {
                    return;   // DB has been seeded
                }

                context.Qualification.AddRange(
                    new Qualification
                    {
                        QualName = "APC-B",
                        PassMark = 0,
                        ParOrChi = "Parent",
                        NumChi = 5,
                        MinChiPass = 5
                    },

                    new Qualification
                    {
                        QualName = "APC-B Drill",
                        PassMark = 0,
                        ParOrChi = "Parent",
                        Parent ="APC-B"
                    },

                    new Qualification
                    {
                        QualName = "APC-B Shooting",
                        PassMark = 0,
                        ParOrChi = "Parent",
                        Parent = "APC-B"
                    },

                    new Qualification
                    {
                        QualName = "APC-B Map and Compass",
                        PassMark = 0,
                        ParOrChi = "Parent",
                        Parent = "APC-B"
                    },
                    new Qualification
                    {
                        QualName = "APC-B Fieldcraft",
                        PassMark = 0,
                        ParOrChi = "Parent",
                        Parent = "APC-B"
                    },
                    new Qualification
                    {
                        QualName = "APC-B First Aid",
                        PassMark = 0,
                        ParOrChi = "Parent",
                        Parent = "APC-B"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
