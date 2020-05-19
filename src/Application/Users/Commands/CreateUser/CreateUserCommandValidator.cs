using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.Username).NotNull();
            RuleFor(v => v.Username).NotEmpty();
            RuleFor(v => v.Password).NotNull();
            RuleFor(v => v.Password).NotEmpty();
        }
    }
}
