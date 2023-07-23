using FluentValidation;
using Guide.Book.Application.Dto.ContactsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Helper.Validations
{
    public class ContactUpdateValidator : AbstractValidator<UpdateContactDto>
    {
        public ContactUpdateValidator()
        {
            RuleFor(p => p.PersonId).NotNull().NotEmpty().WithMessage("PersonId is required");
            RuleFor(p => p.PersonId).NotNull().NotEmpty().MaximumLength(36).WithMessage("PersonId max value is 36");
            
            RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("Contact Id is required");
            RuleFor(p => p.Id).NotNull().NotEmpty().MaximumLength(36).WithMessage("Contact Id max value is 36");

            RuleFor(p => p.Content).NotNull().NotEmpty().WithMessage("Content is required");
            RuleFor(p => p.InfoType).IsInEnum().WithMessage("Info type is invalid value, you must use Info Type List!");
        }
    }
}