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

namespace tti_graduation_work.Application.Steps.Commands.NotifyStudent
{
    public class NotifyStudentCommand : IRequest
    {
        public int StudentId { get; set; }
        public int GraduationPaperId { get; set; }
        public int StepId { get; set; }
    }

    public class NotifyStudentCommandHandler : IRequestHandler<NotifyStudentCommand>
    {
        private IApplicationDbContext _context;
        private INotificationService _notificationService;
        public NotifyStudentCommandHandler(IApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }
        public async Task<Unit> Handle(NotifyStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(request.StudentId);
            if (student == null)
            {
                throw new NotFoundException($"Student not found");
            }

            if (request.GraduationPaperId != student.GranduationPaperId)
            {
                throw new NotAccessibleEntityException($"No graduation paper with id {request.GraduationPaperId} is available for student");
            }

            var graduationPaper = await _context.GraduationPapers.FindAsync(request.GraduationPaperId);

            var step = graduationPaper.Steps.FirstOrDefault(x => x.Id == request.StepId);
            if (step == null)
            {
                throw new NotFoundException($"No step with id {request.StepId} is attached to graduation paper with id {request.GraduationPaperId}");
            }

            var studentEmails = new List<string>
            {
                student.Email1,
                student.Email2
            };

            foreach (var email in studentEmails)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var notification = new NotificationModel
                    {
                        Recepient = email,
                        Subject = $"Graduation paper notification",
                        Content = $"Your Graduation paper step #{step.Id} requires update from {graduationPaper.Supervisor.Name} {graduationPaper.Supervisor.Surname}"
                    };
                    _notificationService.Notify(notification);
                }
            }

            return Unit.Value;

        }
    }
}
