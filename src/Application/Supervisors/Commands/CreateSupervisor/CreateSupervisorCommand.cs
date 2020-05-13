using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Supervisors.Commands.CreateSupervisor
{

	public class CreateSupervisorCommand : IRequest<int>
	{
		public int StaffId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int FacultyId { get; set; }
		public int JobPositionId { get; set; }
		public List<int> Languages { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Degree { get; set; }
		public int StudentLimit { get; set; }
		public int UserId { get; set; }
	}

	public class CreateSupervisorCommandHandler : IRequestHandler<CreateSupervisorCommand, int>
	{
		private IApplicationDbContext _context;

		public CreateSupervisorCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<int> Handle(CreateSupervisorCommand request, CancellationToken cancellationToken)
		{
			var entity = new Supervisor
			{
				Degree = request.Degree,
				Email = request.Email,
				ExternalId = request.StaffId,
				FacultyId = request.FacultyId,
				JobPositionId = request.JobPositionId,
				Name = request.Name,
				Phone = request.Phone,
				Surname = request.Surname,
				StudentLimit = request.StudentLimit,
				UserId = request.UserId
			};

			_context.Supervisors.Add(entity);

			await _context.SaveChangesAsync(cancellationToken);

			foreach (var language in request.Languages)
			{
				var supervisorLanguage = new SupervisorLanguage
				{
					SupervisorId = entity.Id,
					LanguageId = language
				};

				_context.SupervisorLanguages.Add(supervisorLanguage);
				await _context.SaveChangesAsync(cancellationToken);
			}

			return entity.Id;
		}
	}
}
