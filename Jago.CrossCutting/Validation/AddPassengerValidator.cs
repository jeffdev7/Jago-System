using FluentValidation;
using Jago.CrossCutting.Dto;

namespace Jago.CrossCutting.Validation
{
    public class AddPassengerValidator : AbstractValidator<PassengerViewModel>
    {
        public AddPassengerValidator()
        {
            RuleFor(j => j.Name).NotEmpty();
            RuleFor(j => j.Name).NotNull();

            RuleFor(j => j.Phone).NotEmpty();
            RuleFor(j => j.Phone).NotNull();

            RuleFor(j => j.Email).NotNull();
            RuleFor(j => j.Email).NotEmpty().EmailAddress();

            RuleFor(j => j.RG).NotNull();
            RuleFor(j => j.RG).NotEmpty()
                .Matches(@"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)")
                .WithMessage("RG inválido.");

            RuleFor(j => j.CPF).NotNull();
            RuleFor(j => j.CPF).NotEmpty()
                .Matches(@"(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)")
                .WithMessage("CPF inválido.");
        }

    }
}
