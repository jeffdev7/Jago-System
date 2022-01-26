using FluentValidation;
using Jago.domain.Core.Entities;

namespace Jago.domain.Validator
{
    public class PassengerValidator : AbstractValidator<Passenger>
    {
        public PassengerValidator()
        {
            RuleFor(j => j.Id).NotEmpty();
            RuleFor(j => j.Name).NotEmpty();
            RuleFor(j => j.Name).NotNull();
            RuleFor(j => j.Celular).NotEmpty();
            RuleFor(j => j.Celular).NotNull();
            RuleFor(j => j.Email).NotNull();
            RuleFor(j => j.Email).NotNull();
        }
    
    }
}
