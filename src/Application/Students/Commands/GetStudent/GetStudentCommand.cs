using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Students.Commands.GetStudent
{

	public class GetStudentCommand : IRequest<Student>
	{
		public string Username { get; set; }
	}

	public class GetStudentCommandHandler : IRequestHandler<GetStudentCommand, Student>
	{
		public async Task<Student> Handle(GetStudentCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
