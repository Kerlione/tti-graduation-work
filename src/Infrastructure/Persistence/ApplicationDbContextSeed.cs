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
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Faculties.Any())
            {
                context.Faculties.Add(new Faculty
                {
                    ExternalId = 1,
                    Title_EN = "Computer Sciences",
                    Title_LV = "Datorzinātnes",
                    Title_RU = "Компьютерные науки",
                    ShortTitle_EN = "CS",
                    ShortTitle_LV = "DF",
                    ShortTitle_RU = "КН",
                });
                context.Faculties.Add(new Faculty
                {
                    ExternalId = 2,
                    Title_EN = "Electronics",
                    Title_LV = "Elektronika",
                    Title_RU = "Электроника",
                    ShortTitle_EN = "EL",
                    ShortTitle_LV = "EL",
                    ShortTitle_RU = "ЭЛ",
                });
                context.Faculties.Add(new Faculty
                {
                    ExternalId = 3,
                    Title_EN = "Robotics",
                    Title_LV = "Robototehnika",
                    Title_RU = "Робототехника",
                    ShortTitle_EN = "RB",
                    ShortTitle_LV = "RT",
                    ShortTitle_RU = "РТ",
                });
                await context.SaveChangesAsync();
            }
            if (!context.Programes.Any())
            {
                context.Programes.Add(new Programe
                {
                    ExternalId = 1,
                    Title_EN = "Software Development",
                    Title_RU = "Datorprogramu Izstrāde",
                    Title_LV = "Разработка программного обеспечения",
                    FacultyId = context.Faculties.FirstOrDefault(x => x.ShortTitle_EN == "CS").Id
                });

                context.Programes.Add(new Programe
                {
                    ExternalId = 2,
                    Title_EN = "Artificial Intelligence",
                    Title_RU = "Markslīgā intelekte",
                    Title_LV = "Искусственный интеллект",
                    FacultyId = context.Faculties.FirstOrDefault(x => x.ShortTitle_EN == "CS").Id
                });

                context.Programes.Add(new Programe
                {
                    ExternalId = 3,
                    Title_EN = "Networking Engineering",
                    Title_RU = "Tīklu inženierzinātņu speciālists",
                    Title_LV = "Разработка программного обеспечения",
                    FacultyId = context.Faculties.FirstOrDefault(x => x.ShortTitle_EN == "EL").Id
                });

                context.Programes.Add(new Programe
                {
                    ExternalId = 4,
                    Title_EN = "Kuka Robot programing",
                    Title_RU = "Kuka robotu programmēšana",
                    Title_LV = "Программирование роботов Kuka",
                    FacultyId = context.Faculties.FirstOrDefault(x => x.ShortTitle_EN == "RB").Id
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
                        Title_EN = "Dean of Faculty",
                        Title_LV = "Fakultātes dekāns",
                        Title_RU = "Декан факультета"
                    });
                context.JobPositions.Add(
                    new JobPosition
                    {
                        Title_EN = "Lecturer",
                        Title_LV = "Lektors",
                        Title_RU = "Лектор"
                    });
                context.JobPositions.Add(
                    new JobPosition
                    {
                        Title_EN = "Professor",
                        Title_LV = "Profesors",
                        Title_RU = "Профессор"
                    });
                context.JobPositions.Add(
                    new JobPosition
                    {
                        Title_EN = "Guest lecturer",
                        Title_LV = "Vieslektors",
                        Title_RU = "Гостевой лектор"
                    });
                await context.SaveChangesAsync();
            }
            if (!context.Users.Any(x => x.Role == Domain.Enums.Role.Administrator))
            {
                context.Users.Add(
                    new User
                    {
                        Role = Domain.Enums.Role.Administrator,
                        Status = Domain.Enums.UserStatus.Active,
                        Username = "Administrator",
                        Password = "10000.WsFlw9FiQ4M4NIxqyjfEnw==.8JOIksQ0HMeWSLPP2/HSbbG3wwLbkl5sZg519ISvY/k="
                    });
                await context.SaveChangesAsync();
            }
        }
    }
}
