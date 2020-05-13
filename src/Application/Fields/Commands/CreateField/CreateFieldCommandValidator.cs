using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Fields.Commands.CreateField
{
    public class CreateFieldCommandValidator: AbstractValidator<CreateFieldCommand>
    {
        public CreateFieldCommandValidator()
        {
            RuleFor(f => f.Title_EN)
                .NotEmpty()
                .MaximumLength(1024);
            RuleFor(f => f.Title_LV)
                .NotEmpty()
                .MaximumLength(1024);
            RuleFor(f => f.Title_RU)
                .NotEmpty()
                .MaximumLength(1024);
        }
    }
}
