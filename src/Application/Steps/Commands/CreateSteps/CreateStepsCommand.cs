using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Steps.Commands.CreateSteps
{

    public class CreateStepsCommand : IRequest
    {
        public int GraduationPaperId { get; set; }
        public int PaperType { get; set; }
    }

    public class CreateStepsCommandHandler : IRequestHandler<CreateStepsCommand>
    {
        private IApplicationDbContext _context;

        public CreateStepsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateStepsCommand request, CancellationToken cancellationToken)
        {
            var paper = _context.GraduationPapers.FindAsync(request.GraduationPaperId);

            if (paper == null)
            {
                throw new NotFoundException($"Graduation paper not found");
            }
            var steps = GenerateSteps(request);
            await _context.Steps.AddRangeAsync(steps);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private List<Step> GenerateSteps(CreateStepsCommand request)
        {
            var steps = new List<Step>();
            switch ((PaperType)request.PaperType)
            {
                case PaperType.Bachelor:
                    {
                        steps.AddRange(new List<Step> {
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisTopicApproval),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisTopicDefence),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisPreparation),
                            Step.Generate(request.GraduationPaperId, PaperStep.RectorsOrder),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisPreDefence),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisUpload),
                            Step.Generate(request.GraduationPaperId, PaperStep.PlagiarismCheck),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisDelivery),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisDefence)                            
                        });
                        break;
                    }
                case PaperType.Master:
                    {
                        steps.AddRange(new List<Step>
                        {
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisTopicApproval),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisTopicDefence),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisPreparation),
                            Step.Generate(request.GraduationPaperId, PaperStep.RatSifConference),
                            Step.Generate(request.GraduationPaperId, PaperStep.RectorsOrder),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisPreDefence),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisUpload),
                            Step.Generate(request.GraduationPaperId, PaperStep.PlagiarismCheck),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisReview),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisDelivery),
                            Step.Generate(request.GraduationPaperId, PaperStep.ThesisDefence)
                        });
                        break;
                    }
                default:
                    {
                        throw new NotSupportedException($"{request.PaperType} is not supported");
                    }
            }
            return steps;
        }
    }
}
