using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tti_graduation_work.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {            
            // Seed, if necessary
            if (!context.Faculties.Any())
            {
                context.Faculties.Add(new Faculty
                {
                    ExternalId = 1,
                    Title_EN = "Computer Sciences",
                    Title_LV = "Datorzinatnes",
                    Title_RU = "Компьютерные науки",
                    ShortTitle_EN = "CPS",
                    ShortTitle_LV = "DFT",
                    ShortTitle_RU = "ККНИТ",
                });

                await context.SaveChangesAsync();
            }
            if (!context.Programes.Any())
            {
                context.Programes.Add(new Programe
                {
                    ExternalId = 1,
                    Title_EN = "Test1",
                    Title_RU = "Test1",
                    Title_LV = "Test1",
                    FacultyId = context.Faculties.First().Id
                });

                await context.SaveChangesAsync();
            }
            if (!context.Languages.Any())
            {
                context.Languages.Add(new Language
                {
                    Title_EN = "English",
                    Title_LV = "Anglu",
                    Title_RU = "Английский"
                });
                context.Languages.Add(new Language
                {
                    Title_EN = "Latvian",
                    Title_LV = "Latviesu",
                    Title_RU = "Латышский"
                });
                context.Languages.Add(new Language
                {
                    Title_EN = "Russian",
                    Title_LV = "Krievu",
                    Title_RU = "Русский"
                });
                await context.SaveChangesAsync();
            }
            if (!context.JobPositions.Any())
            {
                context.JobPositions.Add(
                    new JobPosition
                    {
                        Title_EN = "Job 1",
                        Title_LV = "Pos 1",
                        Title_RU = "Титул 1"
                    });
                await context.SaveChangesAsync();
            }
            if(!context.Users.Any(x=>x.Role == Domain.Enums.Role.Administrator))
            {
                context.Users.Add(
                    new User
                    {
                        Role = Domain.Enums.Role.Administrator,
                        Status = Domain.Enums.UserStatus.Active,
                        Username = "Administrator"
                    });
                await context.SaveChangesAsync();
            }
        }
    }
}
