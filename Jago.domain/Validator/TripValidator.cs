using FluentValidation;
using Jago.domain.Core.Entities;

namespace Jago.domain.Validator
{
    public class TripValidator : AbstractValidator<Trip>
    {
        public TripValidator()
        {
            RuleFor(j => j.Id).NotEmpty();
        }
    }
}
