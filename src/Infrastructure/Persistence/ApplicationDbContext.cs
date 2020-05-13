using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Common;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace tti_graduation_work.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }
        public DbSet<Attachment> Attachements { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<GraduationPaper> GraduationPapers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Programe> Programes { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }

        // TODO investigate User DB model and mapping to UserIdentity
        public DbSet<User> Users { get; set; }

        public DbSet<FieldOfInterest> FieldsOfInterest { get; set; }
        public DbSet<ThesisTopic> ThesisTopics { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<SupervisorLanguage> SupervisorLanguages { get; set; }

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = _currentUserService.UserId;
        //                entry.Entity.Created = _dateTime.Now;
        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedBy = _currentUserService.UserId;
        //                entry.Entity.LastModified = _dateTime.Now;
        //                break;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //public async Task BeginTransactionAsync()
        //{
        //    if (_currentTransaction != null)
        //    {
        //        return;
        //    }

        //    _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        //}

        //public async Task CommitTransactionAsync()
        //{
        //    try
        //    {
        //        await SaveChangesAsync().ConfigureAwait(false);

        //        _currentTransaction?.Commit();
        //    }
        //    catch
        //    {
        //        RollbackTransaction();
        //        throw;
        //    }
        //    finally
        //    {
        //        if (_currentTransaction != null)
        //        {
        //            _currentTransaction.Dispose();
        //            _currentTransaction = null;
        //        }
        //    }
        //}

        //public void RollbackTransaction()
        //{
        //    try
        //    {
        //        _currentTransaction?.Rollback();
        //    }
        //    finally
        //    {
        //        if (_currentTransaction != null)
        //        {
        //            _currentTransaction.Dispose();
        //            _currentTransaction = null;
        //        }
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
