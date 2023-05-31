using FluentValidation;
using SaaV.SaaS.WebApi.Models;

namespace SaaV.SaaS.Api.Validators
{
    public class CreateDummyValidator: AbstractValidator<CreateDummyModel>
    {
        public CreateDummyValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithErrorCode("ERR-001").WithMessage("Name is required")
                .NotEmpty().WithErrorCode("ERR-001").WithMessage("Name is required")
                .MaximumLength(50).WithErrorCode("ERR-002").WithMessage("Name have a maximum length of 50 characters");
        }
    }
}
