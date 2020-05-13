using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Students.Commands.CreateStudent
{
    public class CreateStudentCommandValidator: AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(v => v.ExternalId).NotNull();
            RuleFor(v => v.FacultyId).NotNull();
            RuleFor(v => v.ProgrameId).NotNull();
            RuleFor(v => v.Language).NotNull();
            RuleFor(v => v.Form).NotNull();
            RuleFor(v => v.Phones).NotEmpty();
            RuleFor(v => v.Emails).NotEmpty();
        }
    }
}
