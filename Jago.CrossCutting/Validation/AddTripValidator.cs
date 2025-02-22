using FluentValidation;
using Jago.CrossCutting.Dto;

namespace Jago.CrossCutting.Validation
{
    public class AddTripValidator : AbstractValidator<TripViewModel>
    {
        public AddTripValidator()
        {
            RuleFor(j => j.PassengerId).NotEmpty().WithMessage("Select a passenger");
            RuleFor(j => j.Origin).NotNull();
            RuleFor(j => j.Origin).NotEmpty().Matches("^[A-Za-z]+$");
            RuleFor(j => j.Destine).NotNull();
            RuleFor(j => j.Destine).NotEmpty().Matches("^[A-Za-z]+$");
            RuleFor(j => j.Departure).NotNull();
            RuleFor(j => j.Departure).NotEmpty();
            RuleFor(j => j.Arrival).NotEmpty();
        }
    }
}
