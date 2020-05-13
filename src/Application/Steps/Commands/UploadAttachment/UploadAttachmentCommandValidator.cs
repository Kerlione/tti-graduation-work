using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Steps.Commands.UploadAttachment
{
    public class UploadAttachmentCommandValidator : AbstractValidator<UploadAttachmentCommand>
    {
        public UploadAttachmentCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(256);
            RuleFor(v => v.Data)
                .NotEmpty();
        }
    }
}
