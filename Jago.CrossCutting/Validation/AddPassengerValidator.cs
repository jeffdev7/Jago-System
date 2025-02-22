using FluentValidation;
using Jago.CrossCutting.Dto;
using Jago.domain.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace Jago.CrossCutting.Validation
{
    public class AddPassengerValidator : AbstractValidator<PassengerViewModel>
    {
        private readonly IPassengerRepository _passengerRepository;

        public AddPassengerValidator(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;

            RuleFor(j => j.Name).NotNull();
            RuleFor(j => j.Name).NotEmpty()
                .Matches("^[A-Za-z ]+$")
                .WithMessage("Only alphabetical letters");

            RuleFor(j => j.Phone).NotNull();
            RuleFor(j => j.Phone).NotEmpty();

            RuleFor(j => j.Email).NotNull();
            RuleFor(j => j.Email).NotEmpty()
                .EmailAddress();

            RuleFor(j => j.DocumentNumber).NotNull();
            RuleFor(j => j.DocumentNumber)
                .Must(ValidDocument)
                .WithMessage("Invalid Document");

            RuleFor(j => j.DocumentNumber)
                .Must(IsDocumentUnique)
                .WithMessage("This document already exists in our base");

        }
        private static bool ValidDocument(string document)//EXTENT THIS METHOD
        {
            var patternRg = @"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)";
            var patternCpf = @"(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)";

            if (Regex.IsMatch(document, patternRg) || Regex.IsMatch(document, patternCpf))
                return true;

            return false;
        }
        private bool IsDocumentUnique(string document)
        {
            var result = _passengerRepository.GetPax().Where(_ => _.DocumentNumber == document);
            if (!result.Any())
                return true;

            return false;
        }
    }

}
