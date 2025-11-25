using FluentValidation;
using Jago.CrossCutting.Dto;
using Jago.CrossCutting.Helper;
using Jago.domain.Interfaces.Repositories;

namespace Jago.CrossCutting.Validation
{
    public class AddPassengerValidator : AbstractValidator<PassengerViewModel>
    {
        private readonly IPassengerRepository _passengerRepository;

        public AddPassengerValidator(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;

            RuleFor(j => j.Name)
                .NotEmpty()
                .Matches("^[A-Za-z ]+$")
                .WithMessage("It must not have special characters.");

            RuleFor(j => j.Phone)
                .NotNull()
                .NotEmpty();

            RuleFor(j => j.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(j => j.DocumentNumber)
                .Must(ValidDocument)
                .WithMessage("Invalid Document.");

            RuleFor(j => j.DocumentNumber)
                .Must(IsDocumentUnique)
                .WithMessage("This document already exists in our base.");

        }
        private static bool ValidDocument(string document) =>
            document.IsValidDocument();

        private bool IsDocumentUnique(string document)
        {
            var result = _passengerRepository.GetPax().Where(_ => _.DocumentNumber == document);
            if (!result.Any())
                return true;

            return false;
        }
    }

}
