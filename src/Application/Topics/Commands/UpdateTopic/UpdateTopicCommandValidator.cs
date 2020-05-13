using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Topics.Commands.UpdateTopic
{
    public class UpdateTopicCommandValidator: AbstractValidator<UpdateTopicCommand>
    {
        public UpdateTopicCommandValidator()
        {
            RuleFor(f => f.SupervisorId)
                .NotNull();
            RuleFor(t => t.TopicId)
                .NotNull();
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
