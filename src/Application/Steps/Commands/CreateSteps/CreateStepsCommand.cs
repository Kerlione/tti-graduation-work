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
            var paper = await _context.GraduationPapers.FindAsync(request.GraduationPaperId);

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
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisTopicApproval),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisTopicDefence),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisPreparation),
                            GenerateStep(request.GraduationPaperId, PaperStep.RectorsOrder),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisPreDefence),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisUpload),
                            GenerateStep(request.GraduationPaperId, PaperStep.PlagiarismCheck),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisDelivery),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisDefence)                            
                        });
                        break;
                    }
                case PaperType.Master:
                    {
                        steps.AddRange(new List<Step>
                        {
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisTopicApproval),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisTopicDefence),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisPreparation),
                            GenerateStep(request.GraduationPaperId, PaperStep.RatSifConference),
                            GenerateStep(request.GraduationPaperId, PaperStep.RectorsOrder),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisPreDefence),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisUpload),
                            GenerateStep(request.GraduationPaperId, PaperStep.PlagiarismCheck),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisReview),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisDelivery),
                            GenerateStep(request.GraduationPaperId, PaperStep.ThesisDefence)
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

        private Step GenerateStep(int paperId, PaperStep step)
        {
            return new Step
            {
                StepStatus = StepStatus.ToDo,
                StepType = step,
                GraduationPaperId = paperId,
            };
        }
    }
}
