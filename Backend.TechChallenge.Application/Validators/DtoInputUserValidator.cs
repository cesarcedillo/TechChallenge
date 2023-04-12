using Backend.TechChallenge.Application.Dtos;
using FluentValidation;

namespace Backend.TechChallenge.Application.Validators
{
    public class DtoInputUserValidator : AbstractValidator<DtoInputUser>
    {
        public DtoInputUserValidator()
        {
            RuleFor(user => user.Name)
                .NotNull().WithMessage("The name is required");
            RuleFor(user => user.Email)
                .NotNull().WithMessage("The email is required");
            RuleFor(user => user.Address)
                .NotNull().WithMessage("The address is required");
            RuleFor(user => user.Phone)
                .NotNull().WithMessage("The phone is required");
        }
    }
}
