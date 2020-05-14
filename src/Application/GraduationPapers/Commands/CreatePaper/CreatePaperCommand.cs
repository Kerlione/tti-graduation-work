using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.GraduationPapers.Commands.CreatePaper
{

    public class CreatePaperCommand : IRequest<int>
    {
        public int StudentId { get; set; }
        public int PaperType { get; set; }
    }

    public class CreatePaperCommandHandler : IRequestHandler<CreatePaperCommand, int>
    {
        private IApplicationDbContext _context;

        public CreatePaperCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePaperCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.Include(s => s.Programe).FirstOrDefaultAsync(x => x.Id == request.StudentId);

            if (student == null)
            {
                throw new NotFoundException($"Student not found");
            }

            var entity = new GraduationPaper
            {
                StudentId = request.StudentId,
                FacultyId = student.Programe.FacultyId,
                PaperType = (PaperType)request.PaperType,
                PaperStatus = PaperStatus.InProgress,
                Year = DateTime.Today.Year
            };

            _context.GraduationPapers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            student.GranduationPaperId = entity.Id;

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
