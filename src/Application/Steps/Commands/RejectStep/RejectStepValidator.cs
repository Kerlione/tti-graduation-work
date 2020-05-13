using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Steps.Commands.RejectStep
{
    public class RejectStepValidator : AbstractValidator<RejectStepCommand>
    {
        public RejectStepValidator()
        {
            RuleFor(r => r.Reason)
                .NotEmpty();
        }
    }
}
