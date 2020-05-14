using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.GraduationPapers.Queries.PaperExists
{

    public class PaperExistsQuery : IRequest<bool>
    {
        public int StudentId { get; set; }
    }

    public class PaperExistsQueryHandler : IRequestHandler<PaperExistsQuery, bool>
    {
        private IApplicationDbContext _context;

        public PaperExistsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(PaperExistsQuery request, CancellationToken cancellationToken)
        {
            return await _context.GraduationPapers
                .FirstOrDefaultAsync(x => x.StudentId == request.StudentId,
                cancellationToken: cancellationToken) != null;
        }
    }
}
