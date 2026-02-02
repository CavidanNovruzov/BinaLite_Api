using Application.Dtos.PropertyAd;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;

namespace Application.Validations.PropertyAd;

public class UpdatePropertyAdRequestValidator:AbstractValidator<UpdatePropertyAdRequest>
{
    public UpdatePropertyAdRequestValidator()
    {
        RuleFor(x=>x.Title)
            .NotEmpty()
            .MaximumLength(220)
            .When(x=>x.Title!=null);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000)
            .When(x => x.Description != null);
        RuleFor(x => x.RoomCount)
            .GreaterThan(0)
            .LessThanOrEqualTo(50)
            .When(x => x.RoomCount != 0);
        RuleFor(x => x.Area)
            .GreaterThan(0)
            .LessThanOrEqualTo(50)
            .When(x => x.Area != 0);
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .When(x => x.Price != 0);
    }
}
