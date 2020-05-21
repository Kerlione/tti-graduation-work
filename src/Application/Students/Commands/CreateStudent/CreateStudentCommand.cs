using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Students.Commands
{
    public class CreateStudentCommand : IRequest<int>
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Skype { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Phones { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public int ProgrameId { get; set; }
        public int FacultyId { get; set; }
        public int Form { get; set; }
        public string Language { get; set; }
        public int UserId { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Student
            {
                ExternalId = request.ExternalId,
                Email1 = request.Emails.FirstOrDefault(),
                Email2 = request.Emails.LastOrDefault(),
                Phone1 = request.Phones.FirstOrDefault(),
                Phone2 = request.Phones.LastOrDefault(),
                Language = _context.Languages.FirstOrDefault(x => x.Title_EN.Equals(request.Language)),
                ProgrameId = request.ProgrameId,
                Skype = request.Skype,
                Comment = request.Comments,
                UserId = request.UserId,
                Name = request.Name,
                Surname = request.Surname
            };

            _context.Students.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
