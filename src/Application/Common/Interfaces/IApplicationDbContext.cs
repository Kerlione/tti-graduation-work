using tti_graduation_work.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Attachment> Attachements { get; set; }
        DbSet<Faculty> Faculties { get; set; }
        DbSet<GraduationPaper> GraduationPapers { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Programe> Programes { get; set; }
        DbSet<Step> Steps { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Supervisor> Supervisors { get; set; }

        //TODO investigate User DB model and mapping to UserIdentity
        DbSet<User> Users { get; set; }

        DbSet<FieldOfInterest> FieldsOfInterest { get; set; }
        DbSet<ThesisTopic> ThesisTopics { get; set; }
        DbSet<JobPosition> JobPositions { get; set; }
        DbSet<SupervisorLanguage> SupervisorLanguages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
