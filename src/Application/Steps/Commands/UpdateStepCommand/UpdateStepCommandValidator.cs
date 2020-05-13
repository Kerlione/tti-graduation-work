using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Steps.Commands.UpdateStepCommand
{
    public class UpdateStepCommandValidator: AbstractValidator<UpdateStepCommand>
    {
        public UpdateStepCommandValidator()
        {
            RuleFor(s => s.Data)
                .NotEmpty();
        }
    }
}
