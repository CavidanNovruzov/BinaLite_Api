using Application.Dtos.PropertyMedia;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.PropertyMedia;

public class UpdatePropertyMediaRequestValidator:AbstractValidator<UpdatePropertyMediaRequest>
{
    public UpdatePropertyMediaRequestValidator()
    {
        RuleFor(x => x.MediaName)
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(x => x.MediaUrl)
            .NotEmpty()
            .MaximumLength(350);
        RuleFor(x => x.Order)
            .NotEmpty()
            .MaximumLength(10);
    }

}
