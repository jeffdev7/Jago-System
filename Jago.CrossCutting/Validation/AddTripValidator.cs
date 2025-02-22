using FluentValidation;
using Jago.CrossCutting.Dto;

namespace Jago.CrossCutting.Validation
{
    public class AddTripValidator : AbstractValidator<TripViewModel>
    {
        public AddTripValidator()
        {
            RuleFor(j => j.PaxName).NotEmpty().WithMessage("Select a passenger");
            RuleFor(j => j.Origin).NotEmpty();
            RuleFor(j => j.Origin).NotNull();
            RuleFor(j => j.Departure).NotNull();
            RuleFor(j => j.Departure).NotNull();
            RuleFor(j => j.Arrival).NotEmpty();
            RuleFor(j => j.Destine).NotEmpty();
            RuleFor(j => j.Destine).NotNull();
        }
    }
}
