using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Supervisors.Commands.UpdateStudentCount;

namespace tti_graduation_work.Application.Supervisors.Commands.UpdateStudentLimit
{
    public class UpdateStudentLimitCommandValidator: AbstractValidator<UpdateStudentLimitCommand>
    {
        public UpdateStudentLimitCommandValidator()
        {

        }
    }
}
