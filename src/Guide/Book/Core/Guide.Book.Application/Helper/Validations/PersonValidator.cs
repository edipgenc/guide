using FluentValidation;
using Guide.Book.Application.Dto.ContactsDto.PersonsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Helper.Validations
{
    public class PersonValidator : AbstractValidator<CreatePersonDto>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Name).NotNull().NotEmpty().NotEqual("string").MaximumLength(50).WithMessage("Name is can be max 50 character"); 
            RuleFor(p => p.Surname).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Surname).NotNull().NotEmpty().NotEqual("string").MaximumLength(50).WithMessage("Surname is can be max 50 character"); 
        }
    }
}
