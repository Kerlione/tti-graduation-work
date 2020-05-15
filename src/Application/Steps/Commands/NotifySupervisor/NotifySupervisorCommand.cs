using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Application.Common.Models;

namespace tti_graduation_work.Application.Steps.Commands.NotifySupervisor
{

    public class NotifySupervisorCommand : IRequest
    {
        public int GraduationPaperId { get; set; }
        public int StepId { get; set; }
    }

    public class NotifySupervisorCommandHandler : IRequestHandler<NotifySupervisorCommand>
    {
        private IApplicationDbContext _context;
        private INotificationService _notificationService;
        public NotifySupervisorCommandHandler(IApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }
        public async Task<Unit> Handle(NotifySupervisorCommand request, CancellationToken cancellationToken)
        {
            var graduationPaper = await _context.GraduationPapers.FindAsync(request.GraduationPaperId);

            var step = graduationPaper.Steps.FirstOrDefault(x => x.Id == request.StepId);
            if (step == null)
            {
                throw new NotFoundException($"No step with id {request.StepId} is attached to graduation paper with id {request.GraduationPaperId}");
            }

            if (!graduationPaper.SupervisorId.HasValue)
            {
                throw new NotFoundException($"No supervisor is attached to the graduation paper");
            }

            var supervisor = await _context.Supervisors.FindAsync(graduationPaper.SupervisorId);

            if (!string.IsNullOrEmpty(supervisor.Email))
            {
                var notification = new NotificationModel
                {
                    Recepient = supervisor.Email,
                    Subject = $"Student's '{graduationPaper.Student.Surname} {graduationPaper.Student.Name}' Graduation paper notification",
                    Content = $"Student's '{graduationPaper.Student.Surname} {graduationPaper.Student.Name}' Graduation paper step #{step.StepType} requires Your review"
                };
                _notificationService.Notify(notification);
            }

            return Unit.Value;
        }
    }
}
